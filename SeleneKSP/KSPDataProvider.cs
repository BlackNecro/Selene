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
    class KSPDataProvider : SeleneLuaState
    {
        ModuleSelene parentModule;

        public KSPDataProvider(ModuleSelene parent) : base()
        {
            parentModule = parent;         
        }

        protected override void FinalizeLuaState()
        {
            luaState.DoString(@"luanet.load_assembly('SeleneKSP')");
        }
        
        public override double GetUniverseTime()
        {
            return Planetarium.fetch.time;
        }

        public override IVessel GetExecutingVessel()
        {
            return new SVessel(parentModule.part.vessel,this);
        }

        public override IVessel GetCommandedVessel()
        {
            throw new NotImplementedException();
        }

        public override ITarget GetCurrentTarget()
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

        public override ICelestialBody GetCelestialBody(string name)
        {
            throw new NotImplementedException();
        }

        public override IManeuverNode GetNextManeuverNode()
        {
            throw new NotImplementedException();
        }

        public override NLua.LuaTable GetManeuverNodes()
        {
            throw new NotImplementedException();
        }

        public override IManeuverNode CreateNewManeuverNode()
        {
            throw new NotImplementedException();
        }

        public override IControls CreateControlState(FlightCtrlState toCreate)
        {
            return (Selene.DataTypes.IControls)new Controls(toCreate);
        }


        public override string ReadFile(string path)
        {
            return KSP.IO.File.ReadAllText<SeleneInterpreter>(path);
        }     

        public override NLua.LuaTable GetVessels()
        {
            throw new NotImplementedException();
        }
    }
}
