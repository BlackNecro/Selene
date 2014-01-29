using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IPart
    {
        Vector GetOffset();
        string GetName();
        string GetTitle();
    }
}
