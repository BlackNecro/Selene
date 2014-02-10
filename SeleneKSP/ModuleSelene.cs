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

        public void RunTestScript()
        {

        }
        public void Init()
        {
            if (interpreter == null)
            {
                interpreter = new SeleneInterpreter(new KSPDataProvider(this));
            }
            if (vessel != null)
            {
                vessel.OnFlyByWire += interpreter.OnFlyByWire;
            }
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
            interpreter.PhysicsUpdate();
        }

        public void Update()
        {
            if (interpreter == null) 
            {
                return;
            }
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

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            Init();
            if (node != null)
            {
                if (node.HasNode("SeleneInterpreter"))
                {
                    interpreter.LoadState(node.GetNode("SeleneInterpreter"));
                }
            }
        }

        public override void OnSave(ConfigNode node)
        {
            if (node != null)
            {
                ConfigNode saveInto = node.GetNode("SeleneInterpreter");
                if (saveInto == null)
                {
                    saveInto = new ConfigNode("SeleneInterpreter");
                    node.AddNode(saveInto);
                }
                interpreter.SaveState(saveInto);
            }
            base.OnSave(node);
        }
    }
}
