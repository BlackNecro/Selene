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

        LuaTable baseEnvironment;

        public LuaTable BaseEnvironment
        {
            get { return baseEnvironment; }
        }

        LuaFunction luaToStringFunction;
        public SeleneLuaState()
        {
            luaState = new Lua();
            luaToStringFunction = luaState.LoadString("return tostring(...)","Lua tostring");
            CreateBaseLibrary();

            baseEnvironment = GetNewTable();
            foreach (string global in ((LuaTable)luaState["_G"]).Keys)
            {
                baseEnvironment[global] = luaState[global];
            }
        }

        private void CreateBaseLibrary()
        {
            luaState.DoString(@"                                
                luanet.load_assembly('UnityEngine')
                luanet.load_assembly('Assembly-CSharp')
                luanet.load_assembly('Assembly-CSharp-firstpass')
                luanet.load_assembly('SeleneCore')
                Vector = luanet.import_type('Selene.DataTypes.SeleneVector')
                Quaternion = luanet.import_type('UnityEngine.QuaternionD')
                Debug = luanet.import_type('UnityEngine.Debug')
                local util = luanet.import_type('Selene.DataTypes.VectorQuaternionUtil')
                
                local quat = Quaternion(0,0,0,0)
                local qmt = getmetatable(quat)
                qmt.__mul = util.MultiplyQuat

                local vec = Vector(0,0,0)
                local vmt = getmetatable(vec)
                vmt.__add = util.AddVec
                vmt.__sub = util.SubstractVec
                vmt.__mul = util.MultiplyVec
                vmt.__div = util.DivideVec
                quat = nil
                ve = nil
                qmt = nil
                vmt = nil
                util = nil
            ", "Library Initialization");
            luaState["Selene"] = this;
            luaState.DoString(@"
                function print(...)
                    local args = {...}
                    local sorted = {}
                    local max = 0
                    for k,v in pairs(args) do                        
                        if k > max then
                            max = k
                        end
                    end
                    for i = 1,max do
                        sorted[i] = tostring(args[i])
                    end
                    Selene:Log(table.concat(sorted,' '))
                end");
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
        public void RegisterPhysicsUpdate(NLua.LuaFunction toCall)
        {
            CurrentProcess.AddCallback(CallbackType.PhysicsUpdate, toCall);
        }

        public NLua.LuaFunction OnPhysicsUpdate
        {
            set
            {
                RegisterPhysicsUpdate(value);
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

        public void RegisterSave(LuaFunction toCall)
        {
            CurrentProcess.AddCallback(CallbackType.Save, toCall);
        }

        public LuaFunction OnSave
        {
            set
            {
                RegisterSave(value);
            }
        }

        public void RegisterLoad(LuaFunction toCall)
        {
            CurrentProcess.AddCallback(CallbackType.Load, toCall);
        }

        public LuaFunction OnLoad
        {
            set
            {
                RegisterLoad(value);
            }
        }
        public LuaTable GetNewTable()
        {
            return (LuaTable)luaState.DoString("return {}")[0];
        }

        public LuaTable GetNewEnvironment()
        {
            LuaTable toReturn = GetNewTable();
            foreach (string key in baseEnvironment.Keys)
            {
                toReturn[key] = baseEnvironment[key];
            }
            toReturn["_G"] = toReturn;
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
            if (CurrentProcess != null)
            {
                CurrentProcess.Log(toLog, (int)priority);
            }
            else
            {
                UnityEngine.Debug.Log("No Process: " + toLog);
            }            
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

        public void Persist(string varName)
        {
            CurrentProcess.Persist(varName);
        }

        public string LuaToString(object obj)
        {
            return (string)luaToStringFunction.Call(obj)[0];
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


        abstract public bool AppendFile(string path, string content);

        abstract public bool WriteFile(string path, string content);
    }
}
