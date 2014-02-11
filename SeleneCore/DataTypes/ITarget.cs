using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface ITarget
    {
        SeleneVector GetPosition();
        QuaternionD GetRotation();
        string GetName();

        SeleneVector WorldToLocal(SeleneVector toTransform);
        SeleneVector LocalToWorld(SeleneVector toTransform);
    }
}
