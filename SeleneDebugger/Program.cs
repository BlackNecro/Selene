using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleneDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Selene.SeleneInterpreter test = new Selene.SeleneInterpreter(new DebugImplementations.DebugDataProvider());            
            test.CreateProcess("test.lua");
            
        }
    }
}
