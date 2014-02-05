using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface ITarget
    {
        Vector GetPosition();
        QuaternionD GetRotation();
        string GetName();

        Vector WorldToLocal(Vector toTransform);
        Vector LocalToWorld(Vector toTransform);
    }
}
