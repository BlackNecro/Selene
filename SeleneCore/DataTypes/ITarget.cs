using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface ITarget
    {
        Vector3d GetPosition();
        QuaternionD GetRotation();
        string GetName();

        Vector3d WorldToLocal(Vector3d toTransform);
        Vector3d LocalToWorld(Vector3d toTransform);
    }
}
