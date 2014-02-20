vessel = Selene:GetExecutingVessel()
parts = vessel:GetParts()
partsTable = parts:GetPartTable()

panels = parts:PartsIncludingModule("ModuleDeployableSolarPanel")
panelsTable = panels:GetPartTable()

engines = parts:PartsIncludingModule("ModuleEngines")
engineTable = engines:GetPartTable()
values = {}
events = parts:ListEvents()
actions = parts:ListActions()

function Selene:OnTick(delta)
	tickDelta = delta
	for k,v in pairs(panelsTable) do
		values[k] = v:Field("Sun Exposure")
	end
	return 10
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