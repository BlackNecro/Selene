using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPart = Part;

namespace SeleneKSP.DataTypes
{
    class Part : Selene.DataTypes.IPart
    {
        protected KPart part;
        public Part(KPart toUse)
        {
            part = toUse;                                
        }

        public Selene.DataTypes.Vector GetOffset()
        {
            return new Selene.DataTypes.Vector(part.orgPos);
        }

        public string GetName()
        {
            return part.partName;
        }

        public string GetTitle()
        {
            return part.partInfo.title;
        }
    }
}
