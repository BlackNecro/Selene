Debug.Log("Hello World")
local vessel = Selene:GetExecutingVessel()
Debug.Log(tostring(vessel))
local controls = vessel:GetControls()
Debug.Log(tostring(controls))
local toggle = false
local steering = Vector()
steering.x = 100
function Selene:OnTick()
	Debug.Log(tostring(vessel:GetRadarHeight()))
	--Debug.Log(tostring(vessel:GetSurfaceVelocity()))
	if vessel:GetRadarHeight() > 1000 then
		Debug.Log("OFF!")
		controls:SetThrottle(0)
		controls:SetRotational(steering)
	end
end