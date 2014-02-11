--test.lua

--Persistence test
P = { 1,"String",true,
["key"] = "value",
["bool"] = true,
["vec"] = Vector(1,2,3),
["quat"] = Quaternion(1,2,3,4)
}
Selene:Persist("P")

--Process interop Test
print("Hello World")
MyVar = 5
proc = Selene:CreateProcessFromFile("test2.lua")
proc.Env.otherVar = 1

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

StopToGetReady = 1
Approach = 2
AssistedTranslation = 3 

mode = 0
setspeed = 10
maxspeed = 20

--Dynamic Process creation from string
proc2 = Selene:CreateProcessFromString("test proc 2",[[Selene:Log('Proc 2') function Selene:OnTick(delta) Selene:Log(tostring(testVar)) return 500 end]]); 
proc2.Active = true

counter = 0
function Selene:OnTick(delta)	
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
	return 50; --Return value determines call frequency 50 -> wait 50 tick events until being called again
end

function Selene:OnControl(ctrl,delta)
	ctrl:SetTranslation(translation + ctrl:GetTranslation())
	ctrl:SetRotation(steering + ctrl:GetRotation())
	return 0
end

