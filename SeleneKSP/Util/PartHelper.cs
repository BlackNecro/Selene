using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleneKSP.Util
{
    public static class PartHelper
    {
        public static bool IsDecoupler(this Part p)
        {
            //return (p is Decoupler || p is DecouplerGUI || p is RadialDecoupler || p.Modules.OfType<ModuleDecouple>().Count() > 0);
            return false;
        }
        public static bool IsLaunchClamp(this Part p)
        {
            return (p.Modules.OfType<ModuleAnchoredDecoupler>().Count() > 0 || p.Modules.OfType<LaunchClamp>().Count() > 0);
        }

        public static int GetDecoupleStage(this Part part)
        {
            int decoupledInStage = -1;
            Part p = part;
            while (true)
            {
                if (p.IsDecoupler() || p.IsLaunchClamp())
                {
                    if (p.inverseStage > decoupledInStage)
                        decoupledInStage = p.inverseStage;
                }
                if (p.parent == null) break;
                else p = p.parent;
            }
            return decoupledInStage;
        }
    }
}
