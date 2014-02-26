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
        GUIStyle VariableStyle = new GUIStyle();

        Vector2 LogScrollPos = new Vector2();

        Vector2 VariableScrollPos = new Vector2();
        string debugInput = "";

        TreeViewHelper CallbackTreeView = new TreeViewHelper();
        TreeViewHelper VariableTreeView = new TreeViewHelper();

        bool resizing = false;
        Rect resizeStartRect = new Rect();
        Vector2 resizeStartPos = new Vector2();

        float resizeSize = 10;

        Rect lastLogScrollSize;
        Rect lastLogContentSize;
        Rect lastVariableContentSize;
        public ProcessWindow(int ID, Selene.SeleneProcess proc)
        {
            windowID = ID;
            process = proc;
            windowPos.x = Screen.width / 2 - 200;
            windowPos.y = Screen.height / 2 - 150;
        }

        int lastLogCounter = 0;


        public Rect ResizeHandle
        {
            get
            {
                return new Rect(windowPos.x + windowPos.width - resizeSize - 3, windowPos.y + windowPos.height - resizeSize - 3, resizeSize, resizeSize);
            }
        }


        public void Draw()
        {
            if (draw)
            {
                windowPos = GUILayout.Window(windowID, windowPos, DrawWindow, process.path, GUILayout.MinHeight(300), GUILayout.MinWidth(400));
                UnityEngine.GUI.Box(ResizeHandle, "", HighLogic.Skin.box);
                HandleResize();
            }
        }

        public void Toggle()
        {
            draw = !draw;
            if (draw)
            {
                LogStyle = new GUIStyle(UnityEngine.GUI.skin.box);
                LogStyle.alignment = TextAnchor.MiddleLeft;
                LogStyle.padding.left += 5;

                VariableStyle = new GUIStyle(UnityEngine.GUI.skin.button);
                VariableStyle.alignment = TextAnchor.MiddleLeft;
                VariableStyle.padding.left += 5;

            }
        }

        public void DrawWindow(int id)
        {

            GUILayout.BeginHorizontal();

            #region Variable List
            GUILayout.BeginVertical();


            
            VariableScrollPos = GUILayout.BeginScrollView(VariableScrollPos,false,true,GUILayout.Width(lastVariableContentSize.width + 25));
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            CallbackTreeView.Start();
            CallbackTreeView.CollapsibleButton("Hooks", "Hooks", VariableStyle);
            if (CallbackTreeView.Expanded("Hooks"))
            {
                CallbackTreeView.Indent();
                foreach (var type in Enum.GetValues(typeof(Selene.CallbackType)))
                {
                    var callbacks = process.Callbacks[(int)type];
                    CallbackTreeView.CollapsibleButton(type.ToString() + " (" + callbacks.Count + ")", callbacks, VariableStyle);
                    if (CallbackTreeView.Expanded(callbacks))
                    {
                        CallbackTreeView.Indent();
                        foreach (var callback in callbacks)
                        {
                            GUILayout.BeginHorizontal();
                            CallbackTreeView.DrawSpacer();
                            if (GUILayout.Button(callback.CallCounter + " " + callback.CallDelay, VariableStyle))
                            {

                            }
                            GUILayout.EndHorizontal();
                        }
                        CallbackTreeView.Unindent();
                    }
                }
                CallbackTreeView.Unindent();
            }



            var tab = process.Env;
            string keyPath = "";


            VariableTreeView.Start();
            VariableTreeView.CollapsibleButton("Variables", keyPath, VariableStyle);
            if (VariableTreeView.Expanded(keyPath))
            {
                ListVariable(tab, keyPath);
            }

            GUILayout.EndVertical();
            if (Event.current.type == EventType.Repaint)
            {
                lastVariableContentSize = GUILayoutUtility.GetLastRect();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            
            GUILayout.EndScrollView();

            GUILayout.EndVertical();


            #endregion



            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            #region log list

            if (lastLogCounter != process.LogList.Count)
            {
                lastLogCounter = process.LogList.Count;
                float scrollableArea = (lastLogContentSize.height - lastLogScrollSize.height);
                if (scrollableArea > 0)
                {
                    if (LogScrollPos.y > scrollableArea)
                    {
                        LogScrollPos.y = float.MaxValue;
                    }
                }
                else
                {
                    LogScrollPos.y = float.MaxValue;
                }
            }                               

            
            LogScrollPos = GUILayout.BeginScrollView(LogScrollPos,false,true, GUILayout.ExpandWidth(true));
                GUILayout.BeginVertical();
                    foreach (var entry in process.LogList)
                    {
                        GUILayout.Label(entry.message, LogStyle, GUILayout.ExpandWidth(true));
                    }
                    GUILayout.FlexibleSpace();
                GUILayout.EndVertical();

                if (Event.current.type == EventType.Repaint)
                {
                    lastLogContentSize = GUILayoutUtility.GetLastRect();
                }

            GUILayout.EndScrollView();

            if (Event.current.type == EventType.Repaint)
            {
                lastLogScrollSize = GUILayoutUtility.GetLastRect();                
            }
            
            #endregion

            #region debug input line            
            if (Event.current.type == EventType.keyUp && Event.current.keyCode == KeyCode.Return && UnityEngine.GUI.GetNameOfFocusedControl() == "DebugInput")
            {
                process.Log(String.Format("Executing: {0}", debugInput), 0);
                if (process.RunString(debugInput, "Console Input"))
                {
                    debugInput = string.Empty;
                }
                LogScrollPos.y = float.MaxValue;
            }
            UnityEngine.GUI.SetNextControlName("DebugInput");
            debugInput = GUILayout.TextField(debugInput, GUILayout.ExpandWidth(true));
            #endregion

            
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            HandleResize();

            if (!resizing)
            {
                UnityEngine.GUI.DragWindow();
            }
        }

        private void HandleResize()
        {
            if (Event.current.isMouse)
            {
                Vector2 MousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                if (Event.current.type == EventType.MouseDown)
                {
                    if (ResizeHandle.Contains(MousePos))
                    {
                        resizing = true;
                        resizeStartPos = MousePos;
                        resizeStartRect = windowPos;
                    }
                }
                else if (Event.current.type == EventType.MouseUp)
                {
                    resizing = false;
                }
                else if (resizing)
                {
                    Vector2 delta = MousePos - resizeStartPos;
                    windowPos.width = resizeStartRect.width + delta.x;
                    windowPos.height = resizeStartRect.height + delta.y;
                }
            }
        }

        private void ListVariable(NLua.LuaTable tab, string keyPath)
        {
            VariableTreeView.Indent();
            List<object> Keys = new List<object>();
            foreach (var key in tab.Keys)
            {
                if (keyPath != "" || process.IsCustomVariable(key))
                {
                    Keys.Add(key);
                }
            }

            var numbers = (Keys.OfType<double>().OrderBy(a=>a)).Cast<object>();
            var others = Keys.Except(numbers).OrderBy(a => a.ToString());

            foreach (var key in numbers.Union(others))
            {
                object value = tab[key];
                if (value is NLua.LuaTable)
                {

                    string newPath = keyPath + "/" + key;
                    VariableTreeView.CollapsibleButton(key.ToString() + " (Table)", newPath, VariableStyle);
                    if (VariableTreeView.Expanded(newPath))
                    {
                        ListVariable((NLua.LuaTable)value, newPath);
                    }
                }
                else if (value is Selene.DataTypes.SeleneVector)
                {
                    string newPath = keyPath + "/" + key;
                    VariableTreeView.CollapsibleButton(key.ToString() + " (Vector)", newPath, VariableStyle);
                    if (VariableTreeView.Expanded(newPath))
                    {
                        VariableTreeView.Indent();
                        Selene.DataTypes.SeleneVector vec = (Selene.DataTypes.SeleneVector)value;
                        VariableTreeView.SpacedButton("x = " + vec.x, VariableStyle);
                        VariableTreeView.SpacedButton("y = " + vec.y, VariableStyle);
                        VariableTreeView.SpacedButton("z = " + vec.z, VariableStyle);
                        VariableTreeView.SpacedButton("length = " + vec.Length, VariableStyle);
                        VariableTreeView.Unindent();
                    }
                }
                else if (value is Selene.DataTypes.SeleneQuaternion)
                {
                    string newPath = keyPath + "/" + key;
                    VariableTreeView.CollapsibleButton(key.ToString() + " (Quaternion)", newPath, VariableStyle);
                    if (VariableTreeView.Expanded(newPath))
                    {
                        VariableTreeView.Indent();
                        Selene.DataTypes.SeleneQuaternion quat = (Selene.DataTypes.SeleneQuaternion)value;
                        VariableTreeView.SpacedButton("x = " + quat.x, VariableStyle);
                        VariableTreeView.SpacedButton("y = " + quat.y, VariableStyle);
                        VariableTreeView.SpacedButton("z = " + quat.z, VariableStyle);
                        VariableTreeView.SpacedButton("w= " + quat.w, VariableStyle);
                        VariableTreeView.Unindent();
                    }
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    VariableTreeView.DrawSpacer();

                    //GUILayout.Button(key.ToString(), LogStyle);
                    //GUILayout.Button(tab[key].ToString());                    
                    //GUILayout.Button(tab[key].ToString(), ValueStyle);
                    GUILayout.Button(key.ToString() + " = " + value.ToString(), VariableStyle);
                    GUILayout.EndHorizontal();
                }
            }
            VariableTreeView.Unindent();
        }





    }
}
