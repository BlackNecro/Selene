using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface IPart
    {
        Vector3d GetOffset();
        QuaternionD GetRotation();
        string GetName();
        string GetTitle();
        double GetMass();
        double GetDryMass();
    }
}
