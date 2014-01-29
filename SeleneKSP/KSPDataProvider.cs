using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene;
using Selene.DataTypes;

using SVessel = SeleneKSP.DataTypes.Vessel;
using SeleneKSP.DataTypes;
namespace SeleneKSP
{
    class KSPDataProvider : IDataProvider
    {
        ModuleSelene parentModule;
        NLua.Lua luaState;
        event RegisterCallback CreateCallback;

        public KSPDataProvider(ModuleSelene parent)
        {
            parentModule = parent;
            SetupLuaState();
        }

        private void SetupLuaState()
        {
            luaState = new NLua.Lua();
            luaState.DoString(@"
luanet.load_assembly('SeleneCore')
luanet.load_assembly('SeleneKSP')
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
");
            luaState["Selene"] = this;
        }

        public double GetUniverseTime()
        {
            return Planetarium.fetch.time;
        }

        public IVessel GetExecutingVessel()
        {
            return new SVessel(parentModule.part.vessel,this);
        }

        public IVessel GetCommandedVessel()
        {
            throw new NotImplementedException();
        }

        public ITarget GetCurrentTarget()
        {
            ITargetable target = FlightGlobals.fetch.VesselTarget;        
            if(target != null)
            {
                if(target is Vessel)
                {
                    return new SVessel((Vessel)target,this);
                }                
            }
            return null;
        }

        public ICelestialBody GetCelestialBody(string name)
        {
            throw new NotImplementedException();
        }

        public IManeuverNode GetNextManeuverNode()
        {
            throw new NotImplementedException();
        }

        public NLua.LuaTable GetManeuverNodes()
        {
            throw new NotImplementedException();
        }

        public IManeuverNode CreateNewManeuverNode()
        {
            throw new NotImplementedException();
        }

        public Selene.GUI.IButton CreateNewButton(string Name)
        {
            throw new NotImplementedException();
        }

        public NLua.Lua GetLuaState()
        {
            throw new NotImplementedException();
        }

        NLua.Lua IDataProvider.GetLuaState()
        {
            return luaState;
        }


        public void Log(string toLog)
        {
            UnityEngine.Debug.Log(toLog);
        }


        public void RegisterCallbackEvent(RegisterCallback toCall)
        {
            CreateCallback += toCall;
        }

        public void RegisterTick(NLua.LuaFunction toCall)
        {
            CreateCallback(CallbackType.Tick,toCall);
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
            CreateCallback(CallbackType.Control, toCall);
        }
        public NLua.LuaFunction OnControl
        {
            set
            {
                RegisterControl(value);
            }
        }


        Selene.DataTypes.IControls Selene.IDataProvider.CreateControlState(FlightCtrlState toCreate)
        {
            return (Selene.DataTypes.IControls)new Controls(toCreate);
        }


        public NLua.LuaTable GetNewTable()
        {
            return (NLua.LuaTable)luaState.DoString("return {}")[0];
        }
    }
}
