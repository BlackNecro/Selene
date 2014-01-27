using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleneDebugger.DebugImplementations
{
    class DebugDataProvider : Selene.IDataProvider
    {
        NLua.Lua luaState = new NLua.Lua();
        public DebugDataProvider()
        {
            LoadLuaClasses();
        }

        public double GetUniverseTime()
        {
            return 42;
        }
        public Selene.DataTypes.IVessel GetExecutingVessel()
        {
            return new DataTypes.Vessel(luaState);
        }

        public Selene.DataTypes.IVessel GetCommandedVessel()
        {
            throw new NotImplementedException();
        }
        public Selene.DataTypes.ITarget GetCurrentTarget()
        {
            throw new NotImplementedException();
        }

        public Selene.DataTypes.ICelestialBody GetCelestialBody(string name)
        {
            throw new NotImplementedException();
        }

        public Selene.DataTypes.IManeuverNode GetNextManeuverNode()
        {
            throw new NotImplementedException();
        }

        public List<Selene.DataTypes.IManeuverNode> GetManeuverNodes()
        {
            throw new NotImplementedException();
        }

        public Selene.DataTypes.IManeuverNode CreateNewManeuverNode()
        {
            throw new NotImplementedException();
        }


        public Selene.GUI.IButton CreateNewButton(string Name)
        {
            throw new NotImplementedException();
        }

        Selene.GUI.IButton Selene.IDataProvider.CreateNewButton(string Name)
        {
            throw new NotImplementedException();
        }


        public void LoadLuaClasses()
        {

            luaState.DoString(@"
luanet.load_assembly('SeleneCore')
luanet.load_assembly('SeleneDebugger')
luanet.load_assembly('UnityEngine')
Vector = luanet.import_type('Selene.DataTypes.Vector')
local val = luanet.import_type('SeleneDebugger.DebugImplementations.DebugDataProvider')
Quaternion = luanet.import_type('UnityEngine.Quaternion')
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


        NLua.LuaTable Selene.IDataProvider.GetManeuverNodes()
        {
            throw new NotImplementedException();
        }

        public NLua.Lua GetLuaState()
        {
            return luaState;
        }
    }
}
