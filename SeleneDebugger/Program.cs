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

            string input = System.IO.File.ReadAllText("test.lua");

            test.CreateProcess(new String[] {input},"test.lua");

            for (int i = 0; i < 100;i++ )
            {
                test.ExecuteProcess();
            }
            //test.CreateProcess("test.lua");
            
        }
    }
}
