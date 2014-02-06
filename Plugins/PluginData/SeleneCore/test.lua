--test.lua
P = { 1,"String",true,
["key"] = "value",
["bool"] = true,
["vec"] = Vector(1,2,3),
["quat"] = Quaternion(1,2,3,4)
}
Selene:Persist("P")
do return end
Selene:Log("Hello World")
MyVar = 5
proc = Selene:CreateProcessFromFile("test2.lua")
proc.Env.otherVar = 1
Selene:Log("Other Process Var " .. tostring(proc.Env.MyVar))
tab = {
	derper = {1,2,3},
	value = 15,
	123	
	}
	

vessel = Selene:GetExecutingVessel()
throttle = 0
override = true;
steering = Vector(0,0,0)
translation = Vector(0,0,0)
engines = vessel:GetEngines()
Debug.Log("test "..tostring(_G))
Selene:Log("Test Messages")
--[[
for k,v in pairs(engines) do
	print(k)
	print(" Offset",v:GetOffset())
	print(" Enabled",v:GetEnabled())
	print(" Percentage",v:GetThrustPercentage())
	print(" Max Thrust",v:GetMaxThrust())
end]]

StopToGetReady = 1
Approach = 2
AssistedTranslation = 3 

mode = 0
setspeed = 10
maxspeed = 20
Debug.Log("Their Stuff")
for k,v in pairs(proc.Env) do
	if(type(v) ~= "table") and type(v) ~= "function" and false then
		Debug.Log(tostring(k) .. " " .. tostring(v))
	end
end
Debug.Log(unique)
Debug.Log("MyStuff")
for k,v in pairs(_G) do
	if(type(v) ~= "table") and type(v) ~= "function" and false then
		Debug.Log(tostring(k) .. " " .. tostring(v))
	end
end
--proc.Active = true


local proc2 = Selene:CreateProcessFromString("test proc 2",[[Selene:Log('Proc 2') function Selene:OnTick(delta) Selene:Log(tostring(testVar)) return 500 end]]); 
--proc2.Active = true

counter = 0
function Selene:OnTick(delta)	
	--Selene:Log("Test")
	--counter = counter + 1
	--Selene:Log(tostring(proc.Env.testVar))
	--proc.Env.testVar = tostring(counter)
	--proc2:Reload()
	--Selene:Log('tick a '..tostring(counter) )
	do return 10 end
	local other = Selene:GetCurrentTarget()
	if other ~= nil then
		local offset = (vessel:GetPosition() - other:GetPosition())
		local mySpeed = vessel:GetOrbitVelocity()
		local otherSpeed = other:GetOrbitVelocity()
		local speedOffset = mySpeed - otherSpeed
		local offsetLocal = Quaternion.Inverse(vessel:GetRotation()) * offset		
		if offset.Length > 20 then						
			if mode == 0 or speedOffset.Length > maxspeed then
				mode = StopToGetReady		
			end
		else	
			mode = AssistedTranslation
		end		
		if mode == StopToGetReady or mode == AssistedTranslation  then						
			if speedOffset.Length > 0.01 then
				local offsetLocal = Quaternion.Inverse(vessel:GetRotation()) * speedOffset
				translation = offsetLocal
				translation.Length = math.min(speedOffset.Length * 10,0.5)
				return 0
			else
				if mode == StopToGetReady then
					mode = Approach
				end
			end
		end
		if mode == Approach then
			if speedOffset.Length < setspeed then
				translation = offsetLocal				
				return 0
			end
		end
	end
	translation = Vector(0,0,0)
	return 50;
end
function Selene:OnControl(ctrl,delta)
	ctrl:SetTranslation(translation + ctrl:GetTranslation())
	ctrl:SetRotation(steering + ctrl:GetRotation())
	return 0
end

