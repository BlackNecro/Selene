using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeleneKSP.GUI
{
    class TreeViewHandler
    {

        public Stack<float> indentStack = new Stack<float>();

        private float curIndent = 0;

        public float Indentation
        {
            get { return curIndent; }
        }
        public float buttonWidth = 25;
        public float indentSize = 30;

        HashSet<object> ExpandedObjects = new HashSet<object>();
        public void CollapseButton(object toToggle)
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

        public bool CollapsibleButton(string title, object toggle)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(curIndent);
            CollapseButton(toggle);
            bool ret = GUILayout.Button(title);
            GUILayout.EndHorizontal();
            return ret;
        }

        public void DrawSpacer()
        {
            GUILayout.Space(curIndent);
        }

        public void ToggleExpanded(object toggle)
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

        public bool Expanded(object toCheck)
        {
            return ExpandedObjects.Contains(toCheck);
        }        

        public void Indent(float toIndent)
        {
            indentStack.Push(toIndent);
            curIndent += toIndent;
        }
        public void Indent()
        {
            indentStack.Push(indentSize);
            curIndent += indentSize;
        }
        public void Unindent()
        {
            curIndent -= indentStack.Pop();
        }
    }
}
