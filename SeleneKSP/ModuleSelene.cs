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
            UnityEngine.Debug.Log("Init");
            if(interpreter == null)
            {
                UnityEngine.Debug.Log("Creating interpreter");
                interpreter = new SeleneInterpreter(new KSPDataProvider(this));
            }
            if(vessel != null)
            {
                vessel.OnFlyByWire += interpreter.OnFlyByWire;
            }                        
            UnityEngine.Debug.Log("End Init");
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
            UnityEngine.Debug.Log("On Start");
            if (state == StartState.Editor || state == StartState.None)
            {
                return;
            }
            Init();
            UnityEngine.Debug.Log("End Start");
        }
        
        public override void OnLoad(ConfigNode node)
        {
            UnityEngine.Debug.Log("On Load");
            base.OnLoad(node);
            Init();
            if (node != null)
            {
                UnityEngine.Debug.Log("got Node");
                if (node.HasNode("SeleneInterpreter"))
                {
                    UnityEngine.Debug.Log("has Interpreter node");
                    interpreter.LoadState(node.GetNode("SeleneInterpreter"));
                }
            }
            UnityEngine.Debug.Log("End Load");
        }

        public override void OnSave(ConfigNode node)
        {
            UnityEngine.Debug.Log("On Save");            
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
            UnityEngine.Debug.Log("End Save");
            base.OnSave(node);
        }        
    }
}
