using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IControls
    {
        SeleneVector GetTranslation();
        void SetTranslation(SeleneVector newValue);
        SeleneVector GetRotation();
        void SetRotation(SeleneVector newValue);

        double GetThrottle();
        void SetThrottle(double newValue);

        double GetWheelSteer();
        void SetWheelSteer(double newValue);
        double GetWheelThrottle();
        void SetWheelThrottle(double newValue);
    }
}
