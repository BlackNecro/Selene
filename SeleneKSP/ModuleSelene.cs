using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene;
namespace SeleneKSP
{
    class ModuleSelene : PartModule
    {
        SeleneInterpreter interpreter;
        DateTime lastRun = DateTime.Now;
        bool run = false;

        public void RunTestScript()
        {

        }
        public void Init()
        {
            interpreter = new SeleneInterpreter(new KSPDataProvider(this));
            interpreter.CreateProcess("test.lua");
        }
        public void Update()
        {
            if (interpreter == null) return;

            if (part.State == PartStates.DEAD)
            {
                return;
            }
            if (!run )
            {
                return;
            }
            UnityEngine.Debug.Log("Executing Lua stuff");
            interpreter.ExecuteProcess();
            lastRun = DateTime.Now;
            run = false;
        }
        public override void OnStart(PartModule.StartState state)
        {
            //Do not start from editor and at KSP first loading
            if (state == StartState.Editor || state == StartState.None)
            {
                return;
            }
            Init();
        }
        [KSPEvent(guiActive = true, guiName = "Reload Program")]
        public void Reload()
        {
            UnityEngine.Debug.Log("Reloading");
            Init(); ;
        }
        [KSPEvent(guiActive = true, guiName = "Run Program")]
        public void Activate()
        {
            UnityEngine.Debug.Log("Clickity Click");
            run = true;
        }
    }
}
