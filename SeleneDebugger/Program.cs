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

            test.CreateProcess(new String[] {@"
print('start')
local args = {...}
print('assembly')
luanet.load_assembly('SeleneCore')
luanet.load_assembly('SeleneDebugger')
luanet.load_assembly('UnityEngine')
print('types')
Vector = luanet.import_type('Selene.DataTypes.Vector')
local val = luanet.import_type('SeleneDebugger.DebugImplementations.DebugDataProvider')
Quaternion = luanet.import_type('UnityEngine.Quaternion')
local QuaternionUtil = luanet.import_type('Selene.DataTypes.QuaternionUtil')
print('metatable stuff')
local quat = Quaternion(0,0,0,0)
local qmt = getmetatable(quat)
qmt.__mul = QuaternionUtil.Multiply

local vec = Vector()
local vmt = getmetatable(vec)
vmt.__add = Vector.Add
vmt.__sub = Vector.Substract
vmt.__mul = Vector.Multiply
vmt.__div = Vector.Divide
print('cleanup')
Selene = args[1]
quat = nil
val = nil
vec = nil
print('yield')
coroutine.yield()",
                          input},"test.lua");

            test.ExecuteProcess();
            test.ExecuteProcess();
            test.ExecuteProcess();
            //test.CreateProcess("test.lua");
            
        }
    }
}
