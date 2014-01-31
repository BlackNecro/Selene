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

        public SeleneInterpreter Interpreter
        {
            get { return interpreter; }
        }
        bool run = false;

        public void RunTestScript()
        {

        }
        public void Init()
        {
            DeleteInterpreter();
            interpreter = new SeleneInterpreter(new KSPDataProvider(this));
            vessel.OnFlyByWire += interpreter.OnFlyByWire;
            interpreter.CreateProcess("test.lua");
            run = true;
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
            interpreter.ExecuteProcess();
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
            foreach(var proc in interpreter.RootProcess.Children)
            {
                UnityEngine.Debug.Log("Reloading " + proc.fileName);
                proc.Reload();
            }
        }
        [KSPEvent(guiActive = true, guiName = "Run Program")]
        public void Activate()
        {
            bool set = true;
            foreach(var proc in interpreter.RootProcess.Children)
            {
                if(set)
                {
                    run = !proc.Active;
                    set = false;
                }
                proc.Active = run;
            }
        }
    }
}
