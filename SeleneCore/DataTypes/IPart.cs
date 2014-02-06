using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IPart
    {
        Vector3d GetOffset();
        UnityEngine.QuaternionD GetRotation();
        string GetName();
        string GetTitle();
        double GetMass();
        double GetDryMass();
    }
}
