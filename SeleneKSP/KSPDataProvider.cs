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

        public KSPDataProvider(ModuleSelene parent)
            : base()
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
            if(parentModule == null)
            {
                return null;
            }
            return new SVessel(parentModule.part.vessel, this);
        }

        public override IVessel GetCommandedVessel()
        {
            return new SVessel(FlightGlobals.ActiveVessel, this);
        }

        public override ITarget GetCurrentTarget()
        {
            ITargetable target = FlightGlobals.fetch.VesselTarget;
            if (target != null)
            {
                if (target is Vessel)
                {
                    return new SVessel((Vessel)target, this);
                }
            }
            return null;
        }

        public override ICelestialBody GetCelestialBody(string name)
        {
            throw new NotImplementedException();
            //FlightGlobals.fetch.bodies.Find(body => body.name == name);
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
            path = System.IO.Path.Combine(AssemblyLoader.GetPathByType(typeof(Selene.SeleneInterpreter)),path);
            return System.IO.File.ReadAllText(path);
        }

        public override NLua.LuaTable GetVessels()
        {
            throw new NotImplementedException();
        }

        public override bool AppendFile(string path, string content)
        {
            try
            {
                KSP.IO.File.AppendAllText<SeleneInterpreter>(content, path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override bool WriteFile(string path, string content)
        {
            try
            {
                KSP.IO.File.WriteAllText<SeleneInterpreter>(content, path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public override void AdvanceStage()
        {
            Staging.ActivateNextStage();
        }
        public override int GetCurrentStage()
        {
            return Staging.CurrentStage;
        }
    }
}
