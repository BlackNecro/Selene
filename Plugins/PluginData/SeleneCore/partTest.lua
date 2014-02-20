vessel = Selene:GetExecutingVessel()
parts = vessel:GetParts()
partsTable = parts:GetPartTable()

panels = parts:GetPartsByMod("ModuleDeployableSolarPanel")
panelsTable = panels:GetPartTable()

chutes = parts:GetPartsByMod("ModuleParachute")
chuteEvents = chutes:GetEvents()

engines = parts:GetPartsByMod("ModuleEngines")
engineTable = engines:GetPartTable()
values = {}
events = parts:GetEvents()
actions = parts:GetActions()

function Selene:OnTick(delta)
	tickDelta = delta
	for k,v in pairs(panelsTable) do
		values[k] = v:GetField("Sun Exposure")
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