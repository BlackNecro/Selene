using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IControls
    {
        Vector GetTranslational();
        void SetTranslational(Vector newValue);
        Vector GetRotational();
        void SetRotational(Vector newValue);

        double GetThrottle();
        void SetThrottle(double newValue);

    }
}
