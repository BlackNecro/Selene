using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public enum CallbackType
    {
        Tick,
        PhysicsUpdate,
        Control,
        Save,
        Load,
        VesselChange
    }
}
