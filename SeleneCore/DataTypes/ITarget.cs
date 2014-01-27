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
        Quaternion GetRotation();
        string GetName();
    }
}
