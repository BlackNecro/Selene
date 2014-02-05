MyVar = 15
Selene:Log("Test 2 Says Hello")
do return end

Selene:Log("test2 " .. tostring(_G))
testVar = "Hello"
unique = 42


Debug.Log(string.dump(function() print(42) end))

Debug.Log("Other namespace")
Debug.Log(tostring(maxspeed))

local variables = {}
local idx = 1
local func = debug.getinfo(2, "f").func
while true do
local ln, lv = debug.getupvalue(func, idx)
if ln ~= nil then
  variables[ln] = lv
else
  break
end
idx = 1 + idx
end
for k,v in pairs(variables) do
	Debug.Log(tostring(k) .. " " .. tostring(v))
end

function Selene:OnTick(delta) 
	Debug.Log('tick b'.. testVar) 
	return 50 
end

repeat

until stuff 