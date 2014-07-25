using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KPart = Part;
using Selene.DataTypes;

namespace SeleneKSP.DataTypes
{
    class Part : Selene.DataTypes.IPart
    {
        protected KPart part;


        public Part(KPart toUse)
        {
            part = toUse;                                
        }

        public Vector3d GetOffset()
        {
            return part.orgPos;
        }

        public string GetName()
        {
            return part.partName;
        }

        public string GetTitle()
        {
            return part.partInfo.title;
        }


        public double GetMass()
        {
            return part.mass + part.GetResourceMass();
        }

        public double GetDryMass()
        {
            return part.mass;
        }


        public QuaternionD GetRotation()
        {
            return part.orgRot;
        }
    }
}
