using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeleneKSP.GUI
{
    class MainTreeView
    {

        int windowID = 99010014;
        int windowCounter = 0;

        Dictionary<Vessel, HashSet<SeleneKSP.ModuleSelene>> loadedModules = new Dictionary<Vessel, HashSet<SeleneKSP.ModuleSelene>>();
        private bool Displayed = false;
        Rect WindowPosition = new Rect();

        float indentSize = 30;

        float buttonWidth = 25;

        float curIndent = 0;

        HashSet<object> ExpandedObjects = new HashSet<object>();
        HashSet<Selene.SeleneProcess> ProcessesToDelete = new HashSet<Selene.SeleneProcess>();

        Dictionary<Selene.SeleneProcess, ProcessWindow> ProcessWindows = new Dictionary<Selene.SeleneProcess,ProcessWindow>();

        public void ReloadModuleList()
        {
            loadedModules.Clear();
            foreach (var vessel in FlightGlobals.Vessels)
            {
                if (vessel != null && vessel.Parts != null)
                {
                    foreach (var part in vessel.Parts)
                    {
                        foreach (var module in part.FindModulesImplementing<SeleneKSP.ModuleSelene>())
                        {

                            if (!loadedModules.ContainsKey(vessel))
                            {
                                loadedModules[vessel] = new HashSet<SeleneKSP.ModuleSelene>();
                            }
                            loadedModules[vessel].Add(module);
                        }
                    }
                }
            }
        }

        public void Toggle()
        {
            Displayed = !Displayed;
            if (Displayed)
            {
                ReloadModuleList();
            }
        }

        public void ToggleWindow(Selene.SeleneProcess proc)
        {
            if (proc.Parent != null)
            {
                if (!ProcessWindows.ContainsKey(proc))
                {
                    ProcessWindows[proc] = new ProcessWindow(windowID + (++windowCounter), proc);
                }
                ProcessWindows[proc].Toggle();
            }
        }

        public void Draw()
        {
            if (Displayed)
            {
                WindowPosition = GUILayout.Window(windowID, WindowPosition, DrawWindow, "Selene", GUILayout.ExpandWidth(true));
            }

            int WindowID = windowID;
            foreach( var kv in ProcessWindows)
            {
                if(kv.Key.Parent == null)
                {
                    ProcessesToDelete.Add(kv.Key);
                }
                else
                {
                    kv.Value.Draw();   
                }
            }
            if (Event.current.type == EventType.Repaint)
            {
                WindowPosition.width = 0;
                WindowPosition.height = 0;
                foreach (var proc in ProcessesToDelete)
                {
                    proc.Delete();
                    ProcessWindows.Remove(proc);
                }
                ProcessesToDelete.Clear();
            }
        }


        private void ToggleExpanded(object toggle)
        {
            if (ExpandedObjects.Contains(toggle))
            {
                ExpandedObjects.Remove(toggle);
            }
            else
            {
                ExpandedObjects.Add(toggle);
            }
        }

        public void DrawWindow(int ID)
        {
            curIndent = 0;
            WrapCollapsibleButton("Current Vessel", FlightGlobals.ActiveVessel);
            if (ExpandedObjects.Contains(FlightGlobals.ActiveVessel))
            {
                ListVessel(FlightGlobals.ActiveVessel);
            }
            UnityEngine.GUI.DragWindow();
        }

        bool WrapCollapsibleButton(string title, object toggle)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(curIndent);
            DrawCollapseButton(toggle);
            bool ret = GUILayout.Button(title);
            GUILayout.EndHorizontal();
            return ret;
        }
        bool WrapButton(string title)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(curIndent);
            bool ret = GUILayout.Button(title);
            GUILayout.EndHorizontal();
            return ret;
        }


        private void DrawCollapseButton(object toToggle)
        {
            string caption = "▶";     
            if (ExpandedObjects.Contains(toToggle))
            {
                caption = "◢";
            }
            if (GUILayout.Button(caption, GUILayout.Width(buttonWidth)))
            {
                ToggleExpanded(toToggle);
            }
        }

        private void ListVessel(Vessel vessel)
        {
            if (vessel != null)
            {
                if (loadedModules.ContainsKey(vessel))
                {
                    if (loadedModules[vessel].Count > 1)
                    {
                        int i = 0;
                        foreach (var module in loadedModules[vessel])
                        {

                            if (WrapCollapsibleButton("Module " + (++i).ToString(), module))
                            {

                            }
                            if (ExpandedObjects.Contains(module))
                            {
                                curIndent += indentSize;
                                ListProcesses(module);
                                curIndent -= indentSize;
                            }
                        }
                    }
                    else
                    {
                        ListProcesses(loadedModules[vessel].First());
                    }
                }
            }
        }

        void ListProcesses(ModuleSelene mod)
        {
            var proc = mod.Interpreter.RootProcess;
            ListProcess(proc);
        }

        private void ListProcess(Selene.SeleneProcess proc)
        {
            curIndent += indentSize;

            GUILayout.BeginHorizontal();
            GUILayout.Space(curIndent);
            if (proc.Children.Count > 0)
            {
                DrawCollapseButton(proc);
            }
            if (GUILayout.Button(proc.fileName))
            {
                ToggleWindow(proc);
            }

            if (proc.Parent != null)
            {
                if (GUILayout.Button("✖", GUILayout.Width(buttonWidth)))
                {
                    ProcessesToDelete.Add(proc);                    
                }
            }
            else
            {
                if(GUILayout.Button("+",GUILayout.Width(buttonWidth)))
                {
                    //Todo Add Process
                }
            }
            string caption = "■";
            if (proc.Active)
            {
                if (proc.IsActive)
                {
                    caption = "▶";
                } 
                else
                {
                    caption = "▷";
                }
            }
            if (GUILayout.Button(caption, GUILayout.Width(buttonWidth)))
            {
                proc.Active = !proc.Active;
            }

            if (GUILayout.Button("↻", GUILayout.Width(buttonWidth)))
            {
                proc.Reload();
                proc.Active = true;
            }
            GUILayout.EndHorizontal();

            if (ExpandedObjects.Contains(proc))
            {
                foreach (var child in proc.Children)
                {
                    ListProcess(child);
                }
            }
            curIndent -= indentSize;
        }

    }
}
