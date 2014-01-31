using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public abstract class SeleneLuaState : ILuaDataProvider
    {

        //private SeleneProcess CurrentProcess;

        private Stack<SeleneProcess> callStack = new Stack<SeleneProcess>();
        public SeleneProcess CurrentProcess
        {
            get { return callStack.Peek(); }
            set { callStack.Push(value); }
        }


        public void revertCallStack()
        {
            callStack.Pop();
        }
        protected Lua luaState;

        public Lua LuaState
        {
            get { return luaState; }
        }

        LuaTable BaseEnvironment;

        public SeleneLuaState()
        {
            luaState = new Lua();
            CreateBaseLibrary();

            BaseEnvironment = GetNewTable();
            foreach (string global in ((LuaTable)luaState["_G"]).Keys)
            {
                BaseEnvironment[global] = luaState[global];
            }
        }

        private void CreateBaseLibrary()
        {
            luaState.DoString(@"
                luanet.load_assembly('SeleneCore')
                luanet.load_assembly('UnityEngine')
                Vector = luanet.import_type('Selene.DataTypes.Vector')
                Quaternion = luanet.import_type('UnityEngine.Quaternion')
                Debug = luanet.import_type('UnityEngine.Debug')
                local QuaternionUtil = luanet.import_type('Selene.DataTypes.QuaternionUtil')

                local quat = Quaternion(0,0,0,0)
                local qmt = getmetatable(quat)
                qmt.__mul = QuaternionUtil.Multiply

                local vec = Vector()
                local vmt = getmetatable(vec)
                vmt.__add = Vector.Add
                vmt.__sub = Vector.Substract
                vmt.__mul = Vector.Multiply
                vmt.__div = Vector.Divide
                quat = nil
                ve = nil
                qmt = nil
                vmt = nil
                QuaternionUnil = nil
            ");
            luaState["Selene"] = this;
            FinalizeLuaState();
        }

        abstract protected void FinalizeLuaState();

        public void RegisterTick(NLua.LuaFunction toCall)
        {
            CurrentProcess.AddCallback(CallbackType.Tick, toCall);
        }

        public NLua.LuaFunction OnTick
        {
            set
            {
                RegisterTick(value);
            }
        }
        public void RegisterControl(NLua.LuaFunction toCall)
        {
            CurrentProcess.AddCallback(CallbackType.Control, toCall);
        }
        public NLua.LuaFunction OnControl
        {
            set
            {
                RegisterControl(value);
            }
        }
        public LuaTable GetNewTable()
        {
            return (LuaTable)luaState.DoString("return {}")[0];
        }

        public LuaTable GetNewEnvironment()
        {
            LuaTable toReturn = GetNewTable();
            foreach (string key in BaseEnvironment.Keys)
            {
                toReturn[key] = BaseEnvironment[key];
            }
            //TODO Potentially reimporting base library
            return toReturn;
        }

        public void Log(string toLog)
        {
            if (CurrentProcess != null)
            {
                CurrentProcess.Log(toLog, 0);
            }
            else
            {
                UnityEngine.Debug.Log("No Process: " + toLog);
            }
        }

        public void Log(string toLog, double priority)
        {
            CurrentProcess.Log(toLog, (int)priority);
        }

        public SeleneProcess CreateProcessFromFile(string file)
        {
            SeleneProcess toReturn = new SeleneProcess(this);
            SeleneProcess parent = CurrentProcess;
            if(toReturn.LoadFromFile(file))
            {
                parent.AddChildProcess(toReturn);
                return toReturn;
            }
            return null;
        }

        public SeleneProcess CreateProcessFromString(string name,string source)
        {
            SeleneProcess toReturn = new SeleneProcess(this);
            SeleneProcess parent = CurrentProcess;
            if (toReturn.LoadFromString(source,name))
            {
                parent.AddChildProcess(toReturn);                
                return toReturn;
            }
            return null;
        } 

        abstract public double GetUniverseTime();

        abstract public DataTypes.IVessel GetExecutingVessel();

        abstract public DataTypes.IVessel GetCommandedVessel();

        abstract public LuaTable GetVessels();

        abstract public DataTypes.ITarget GetCurrentTarget();

        abstract public DataTypes.ICelestialBody GetCelestialBody(string name);

        abstract public DataTypes.IManeuverNode GetNextManeuverNode();

        abstract public LuaTable GetManeuverNodes();

        abstract public DataTypes.IManeuverNode CreateNewManeuverNode();

        abstract public string ReadFile(string path);

        abstract public DataTypes.IControls CreateControlState(FlightCtrlState toCreate);
    }
}
