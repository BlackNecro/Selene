using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IControls
    {
        Vector GetTranslation();
        void SetTranslation(Vector newValue);
        Vector GetRotation();
        void SetRotation(Vector newValue);

        double GetThrottle();
        void SetThrottle(double newValue);

    }
}
