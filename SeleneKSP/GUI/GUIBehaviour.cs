using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeleneKSP.GUI
{

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class GUIBehaviour : UnityEngine.MonoBehaviour
    {


        Toolbar.IButton toolbarButton;
        MainTreeView main;

        void Start()
        {
            main = new MainTreeView();
            toolbarButton = Toolbar.ToolbarManager.Instance.add("Selene", "selenebutton");
            toolbarButton.TexturePath = "WernherChecker/Data/icon";
            toolbarButton.ToolTip = "Selene";
            toolbarButton.OnClick += (e) => main.Toggle();
        }
        void OnDestroy()
        {
            toolbarButton.Destroy();
        }


        void OnGUI()
        {
            main.Draw();
        }

            /*
        void MenuWindow(int id)
        {

            curOffset = 0;
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

        
        

        void ListProcess(Selene.SeleneProcess proc)
        {
            curOffset += offset;
            if (WrapButton("GUI"))
            {

            }
            if (WrapButton("Debug"))
            {
                //ToggleExpanded(proc.Env);
            }
            if ( true)
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
            */
    }   
}
