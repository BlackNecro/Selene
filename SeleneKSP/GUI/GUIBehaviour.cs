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
            toolbarButton.TexturePath = "Selene/icon";
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
    }   
}
