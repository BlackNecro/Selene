using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeleneKSP.GUI
{
    class ProcessWindow
    {
        int windowID;
        bool draw;
        Selene.SeleneProcess process;

        Rect windowPos = new Rect(300, 300, 300, 300);
        GUIStyle LogStyle = new GUIStyle();

        Vector2 LogScrollPos = new Vector2();
        string debugInput = "";

        TreeViewHandler CallbackTreeView = new TreeViewHandler();
        TreeViewHandler VariableTreeView = new TreeViewHandler();


        public ProcessWindow(int ID, Selene.SeleneProcess proc)
        {
            windowID = ID;
            process = proc;
        }


        public void Draw()
        {
            if (draw)
            {
                windowPos = GUILayout.Window(windowID, windowPos, DrawWindow, process.fileName);
            }
        }

        public void Toggle()
        {
            draw = !draw;
            if (draw)
            {
                LogStyle = new GUIStyle(UnityEngine.GUI.skin.box);
                LogStyle.alignment = TextAnchor.MiddleLeft;
            }
        }

        public void DrawWindow(int id)
        {

            GUILayout.BeginHorizontal();

            //TODO Readd Log List
            
            GUILayout.BeginVertical();

            //LogScrollPos = GUILayout.BeginScrollView(LogScrollPos);
            foreach (var logentry in process.LogList)
            {
                GUILayout.Label(logentry.message, LogStyle);
            }
           // GUILayout.EndScrollView();
            debugInput = GUILayout.TextField(debugInput);
            GUILayout.EndVertical();
             
            GUILayout.BeginVertical();

            //TODO List Hooks
            CallbackTreeView.CollapsibleButton("Hooks", "Hooks");
            if (CallbackTreeView.Expanded("Hooks"))
            {
                CallbackTreeView.Indent();
                foreach (var type in Enum.GetValues(typeof(Selene.CallbackType)))
                {
                    var callbacks = process.Callbacks[(int)type];
                    CallbackTreeView.CollapsibleButton(type.ToString() + " (" + callbacks.Count + ")", callbacks);
                    if (CallbackTreeView.Expanded(callbacks))
                    {
                        CallbackTreeView.Indent();
                        foreach (var callback in callbacks)
                        {
                            GUILayout.BeginHorizontal();
                            CallbackTreeView.DrawSpacer();
                            if (GUILayout.Button(callback.CallCounter + " " + callback.CallDelay))
                            {

                            }
                            GUILayout.EndHorizontal();
                        }
                        CallbackTreeView.Unindent();
                    }
                }
                CallbackTreeView.Unindent();
            }

            //Todo List Vars            


            var tab = process.Env;
            string keyPath = "";


            //Todo fix variable listing
            VariableTreeView.CollapsibleButton("Variables", keyPath);
            if (VariableTreeView.Expanded(keyPath))
            {
                ListVariable(tab, keyPath);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            UnityEngine.GUI.DragWindow();
        }

        private void ListVariable(NLua.LuaTable tab, string keyPath)
        {
            VariableTreeView.Indent();
            List<object> Keys = new List<object>();
            foreach (var key in tab.Keys)
            {
                if(keyPath != "" || process.IsCustomVariable(key))
                {
                    Keys.Add(key);
                }                
            }
            Keys.OrderBy(key => key.ToString());
            foreach (var key in Keys)
            {

                if (tab[key] is NLua.LuaTable)
                {

                    string newPath = keyPath + "/" + key;
                    VariableTreeView.CollapsibleButton(key.ToString(), newPath);
                    if (VariableTreeView.Expanded(newPath))
                    {
                        ListVariable((NLua.LuaTable)tab[key], newPath);
                    }
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    VariableTreeView.DrawSpacer();
                    GUILayout.Button(key.ToString() + " = " + tab[key].ToString());
                    GUILayout.EndHorizontal();
                }
            }
            VariableTreeView.Unindent();
        }





    }
}
