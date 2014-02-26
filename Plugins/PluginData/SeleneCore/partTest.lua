vessel = Selene:GetExecutingVessel()
parts = vessel:GetParts()
partsTable = parts:GetPartTable()

panels = parts:GetPartsByModule("ModuleDeployableSolarPanel")
panelsTable = panels:GetPartTable()

chutes = parts:GetPartsByModule("ModuleParachute")
chuteTable = chutes:GetPartTable()
chuteEvents = chutes:GetEvents()

engines = parts:GetPartsByModule("ModuleEngines")
engineTable = engines:GetPartTable()
values = {}
events = parts:GetEvents()
actions = parts:GetActions()
deployed = false
function Selene:OnTick(delta)
	tickDelta = delta
	for k,v in pairs(panelsTable) do
		values[k] = v:GetField("Sun Exposure")
	end
	
	altitude = vessel:GetAltitude()
	speed = vessel:WorldToLocal(vessel:GetSurfaceVelocity())
	
	if not deployed and altitude > 500 and speed.y < 0 then
		chutes:Event("Deploy Chute")
		deployed = true
		print("RELEASE THE QUACKEN!")
	end
	
	return 50
end

--[[

function Selene:OnPhysicsUpdate(delta)
	physDelta = delta
	return 0
end

function Selene:OnControl(ctrl,delta)
	ctrlDelta = delta
	return 0
end]]