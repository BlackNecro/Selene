using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeleneKSP.GUI
{

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class MainGUIBehaviour : UnityEngine.MonoBehaviour
    {

        Toolbar.IButton toolbarButton;
        bool showMenu = false;
        Rect menuPos = new Rect();
        Vector2 menuScroll = new Vector2();


        Dictionary<Vessel, List<Selene.SeleneInterpreter>> loadedModules = new Dictionary<Vessel, List<Selene.SeleneInterpreter>>();



        void Start()
        {
            toolbarButton = Toolbar.ToolbarManager.Instance.add("Selene", "selenebutton");
            toolbarButton.TexturePath = "WernherChecker/Data/icon";
            toolbarButton.ToolTip = "Selene";
            toolbarButton.OnClick += (x) => showMenu = !showMenu;


        }
        void OnDestroy()
        {
            toolbarButton.Destroy();
        }


        void OnGUI()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (showMenu)
                {
                    menuPos = GUILayout.Window(42, menuPos, MenuWindow, "Selene");
                }
            }
        }


        void RebuildList()
        {
            loadedModules.Clear();
            foreach (var vessel in FlightGlobals.Vessels)
            {
                if (vessel != null && vessel.loaded)
                {
                    foreach (var part in vessel.parts)
                    {
                        foreach (ModuleSelene module in part.FindModulesImplementing<ModuleSelene>())
                        {
                            if (!loadedModules.ContainsKey(vessel))
                            {
                                loadedModules[vessel] = new List<Selene.SeleneInterpreter>();
                            }
                            loadedModules[vessel].Add(module.Interpreter);
                        }
                    }
                }
            }
        }

        HashSet<object> Expanded = new HashSet<object>();
        Selene.SeleneProcess toRemove;


        float curOffset = 0;
        float offset = 10;

        bool WrapButton(string title)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(curOffset);

            bool ret = GUILayout.Button(title);
            GUILayout.EndHorizontal();
            return ret;
        }

        void MenuWindow(int id)
        {

            curOffset = 0;
            if (WrapButton("Reload"))
            {
                RebuildList();
            }
            //menuScroll = GUILayout.BeginScrollView(menuScroll,GUILayout.ExpandWidth(true),GUILayout.ExpandHeight(true));

            Vessel vessel = FlightGlobals.ActiveVessel;

            if (loadedModules.ContainsKey(vessel))
            {
                WrapButton("Current Vessel");
                ListVessel(vessel);
            }
           // GUILayout.EndScrollView();
            if(toRemove != null)
            {
                toRemove.Delete();
                toRemove = null;
            }
            UnityEngine.GUI.DragWindow();
        }

        private void ListVessel(Vessel vessel)
        {
            curOffset += offset;
            if (vessel != null)
            {
                if (loadedModules.ContainsKey(vessel))
                {
                    if (loadedModules[vessel].Count > 1)
                    {
                        int i = 0;
                        foreach (var interp in loadedModules[vessel])
                        {

                            if (WrapButton("Module " + (++i).ToString()))
                            {
                                ToggleExpanded(interp);
                            }
                            if (Expanded.Contains(interp))
                            {
                                ListProcesses(interp);
                            }
                        }
                    }
                    else
                    {
                        ListProcesses(loadedModules[vessel][0]);
                    }
                }
            }
            curOffset -= offset;
        }

        private void ToggleExpanded(object toggle)
        {
            if (Expanded.Contains(toggle))
            {
                Expanded.Remove(toggle);
            }
            else
            {
                Expanded.Add(toggle);
            }
        }

        void ListProcesses(Selene.SeleneInterpreter interp)
        {
            curOffset += offset; 
            if (WrapButton("New"))
            {

            }                       
            foreach (var proc in interp.RootProcess.Children)
            {
                if (WrapButton(proc.fileName))
                {
                    ToggleExpanded(proc);

                }
                if (Expanded.Contains(proc))
                {
                    ListProcess(proc);
                }
            }
            curOffset -= offset;
        }
        void ListProcess(Selene.SeleneProcess proc)
        {
            curOffset += offset;
            if (WrapButton("GUI"))
            {

            }
            if (WrapButton("Debug"))
            {
                ToggleExpanded(proc.Env);
            }
            if (Expanded.Contains(proc.Env))
            {
                curOffset += offset;
                if (WrapButton("Log"))
                {

                }

                if (WrapButton("Hooks"))
                {
                    ToggleExpanded(proc.Callbacks);
                }
                if(Expanded.Contains(proc.Callbacks))
                {
                    curOffset += offset;
                    foreach(var type in Enum.GetValues(typeof(Selene.CallbackType)))
                    {
                        var callbacks = proc.Callbacks[(int)type];

                        if(WrapButton(type.ToString() + " (" + callbacks.Count.ToString() + ")"))
                        {
                            ToggleExpanded(callbacks);
                        }
                        if(Expanded.Contains(callbacks))
                        {
                            curOffset += offset;
                            foreach(var callback in callbacks)
                            {
                                WrapButton(Math.Max(0,callback.CallDelay - (callback.CallCounter)).ToString() + " " + callback.CallDelay);
                            }
                            curOffset -= offset;
                        }
                    }
                    curOffset -= offset;
                }
                if (WrapButton("Variables"))
                {

                }
                curOffset -= offset;
            }
            if (WrapButton("Children"))
            {
                ToggleExpanded(proc.Children);
            }
            if (Expanded.Contains(proc.Children))
            {
                foreach (var child in proc.Children)
                {
                    curOffset += offset;
                    if (WrapButton(child.fileName))
                    {
                        ToggleExpanded(child);
                    }
                    if (Expanded.Contains(child))
                    {
                        ListProcess(child);
                    }
                    curOffset -= offset;
                }
            }
            if (WrapButton("Reload"))
            {
                proc.Reload();
            }

            if (proc.Active)
            {
                if (WrapButton("Stop"))
                {
                    proc.Active = false;
                }
            }
            else
            {
                if (WrapButton("Start"))
                {
                    proc.Active = true;
                }
            }

            if (WrapButton("Delete"))
            {
                toRemove = proc;
            }
            curOffset -= offset;
        }
    }
}
