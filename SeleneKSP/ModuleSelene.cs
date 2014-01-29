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
            DeleteInterpreter();
            interpreter = new SeleneInterpreter(new KSPDataProvider(this));
            vessel.OnFlyByWire += interpreter.OnFlyByWire;
            string input = KSP.IO.File.ReadAllText<Selene.SeleneInterpreter>("test.lua");
            
            interpreter.CreateProcess(new String[] { input }, "test.lua");
            //interpreter.CreateProcess("test.lua");
        }

        private void DeleteInterpreter()
        {
            if (interpreter != null)
            {
                vessel.OnFlyByWire -= interpreter.OnFlyByWire;
                interpreter = null;
            }
        }
        public void FixedUpdate()
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
            interpreter.ExecuteProcess();
            lastRun = DateTime.Now;
           // run = false;
        }
        public override void OnStart(PartModule.StartState state)
        {
            //Do not start from editor and at KSP first loading
            if (state == StartState.Editor || state == StartState.None)
            {
                return;
            }            
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
            run = !run;
            if(run)
            {                
                Init();
            }
            else
            {
                DeleteInterpreter();
            }
            
        }
    }
}
