Debug.Log("Hello World")
local function print(...)
	local args = {...}	
	for k,v in pairs(args) do
		args[k] = tostring(v)
	end
	Debug.Log(table.concat(args," "))
end
local vessel = Selene:GetExecutingVessel()
local throttle = 0
local override = true;
local steering = Vector(0,0,0)
local translation = Vector(0,0,0)
local engines = vessel:GetEngines()
for k,v in pairs(engines) do
	print(k)
	print(" Offset",v:GetOffset())
	print(" Enabled",v:GetEnabled())
	print(" Percentage",v:GetThrustPercentage())
	print(" Max Thrust",v:GetMaxThrust())
end
function Selene:OnTick(delta)
	local other = Selene:GetCurrentTarget()
	local offset = (vessel:GetPosition() - other:GetPosition())
	local mySpeed = vessel:GetOrbitVelocity()
	local otherSpeed = other:GetOrbitVelocity()
	local speedOffset = mySpeed - otherSpeed
	if offset.Length > 20 then		
		
		local offsetLocal = Quaternion.Inverse(vessel:GetRotation()) * offset
		translation = offsetLocal
		if speedOffset.Length > 2 then
			translation.Length = 0
		else
			translation.Length = 1
		end
		return 0
	else		
		if speedOffset.Length > 0.01 then
			local offsetLocal = Quaternion.Inverse(vessel:GetRotation()) * speedOffset
			translation = offsetLocal
			translation.Length = math.min(speedOffset.Length * 10,0.5)
			return 0
		end
	end
	translation = Vector(0,0,0)
	return 0;
end

function Selene:OnControl(ctrl,delta)
	--Debug.Log("control "..tostring(delta))	
	--ctrl:SetRotation(steering + ctrl:GetRotation())
	ctrl:SetTranslation(translation + ctrl:GetTranslation())
	ctrl:SetRotation(steering + ctrl:GetRotation())
	return 0
end