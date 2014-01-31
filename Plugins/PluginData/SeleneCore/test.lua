local vessel = Selene:GetExecutingVessel()
local throttle = 0
local override = true;
local steering = Vector(0,0,0)
local translation = Vector(0,0,0)
local engines = vessel:GetEngines()
--[[
for k,v in pairs(engines) do
	print(k)
	print(" Offset",v:GetOffset())
	print(" Enabled",v:GetEnabled())
	print(" Percentage",v:GetThrustPercentage())
	print(" Max Thrust",v:GetMaxThrust())
end]]

local StopToGetReady = 1
local Approach = 2
local AssistedTranslation = 3 

local mode = 0
local setspeed = 10
local maxspeed = 20

local proc2 = Selene:CreateProcessFromString("test proc 2","Debug.Log('1') function Selene:OnTick(delta) Debug.Log('tick b') return 500 end"); 
proc2.Active = true
function Selene:OnTick(delta)	
	--proc2:Reload()
	Debug.Log('tick a')
	do return 100 end
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

