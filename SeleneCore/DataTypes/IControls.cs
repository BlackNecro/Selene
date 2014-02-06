using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IControls
    {
        Vector3d GetTranslation();
        void SetTranslation(Vector3d newValue);
        Vector3d GetRotation();
        void SetRotation(Vector3d newValue);

        double GetThrottle();
        void SetThrottle(double newValue);

    }
}
