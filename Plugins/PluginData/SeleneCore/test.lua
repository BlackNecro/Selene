Debug.Log("Hello World")

local controls = Selene:GetExecutingVessel():GetControls()
local toggle = false
for i = 1,10 do
	toggle = not toggle
	if toggle then
		controls:SetThrottle(1)
	else
		controls:SetThrottle(0)
	end
	coroutine.yield()
end