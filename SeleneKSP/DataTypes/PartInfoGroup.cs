using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KVessel = Vessel;
using KPart = Part;
using Selene;
using Selene.DataTypes;
using NLua;
namespace SeleneKSP.DataTypes
{
    class PartInfoGroup : IPartInfoGroup
    {
        KSPDataProvider dataProvider;
        HashSet<KPart> includedParts = new HashSet<KPart>();
        HashSet<PartModule> includedModules = new HashSet<PartModule>();


        public PartInfoGroup(KSPDataProvider prov)
        {
            dataProvider = prov;            
        }

        public PartInfoGroup(KSPDataProvider prov, KVessel vessel) 
        {
            dataProvider = prov;
            foreach(KPart part in vessel.Parts)
            {
                AddPart(part);
            }
        }

        public void AddPart(KPart toAdd)
        {
            includedParts.Add(toAdd);
            foreach (PartModule module in toAdd.Modules)
            {
                includedModules.Add(module);
            }
        }

        public void AddModule(PartModule toAdd)
        {
            includedModules.Add(toAdd);
            includedParts.Add(toAdd.part);
        }

        public IPartInfoGroup PartsIncludingModule(string moduleName)
        {
            PartInfoGroup toReturn = new PartInfoGroup(dataProvider);

            Type moduleType = AssemblyLoader.GetClassByName(typeof(PartModule), moduleName);
            
            if(moduleType == null)
            {
                return null;
            }                                                   
            foreach(PartModule module in includedModules)
            {
                if (moduleType == module.GetType())
                {
                    toReturn.AddModule(module);
                }
            }
            return toReturn;
        }
        public LuaTable GetPartTable()
        {
            
            LuaTable toReturn = dataProvider.GetNewTable();

            int num = 1;
            foreach(KPart part in includedParts)
            {
                PartInfo newPart = new PartInfo(dataProvider, part);
                toReturn[num++] = newPart;                
            }
            return toReturn;
        }


        public bool Action(string actionName, bool activate)
        {
            bool success = true;
            foreach (var module in includedModules)
            {
                success &= Util.ActionHelper.ExecuteActionInClass(module, actionName, new KSPActionParam(KSPActionGroup.None, activate ? KSPActionType.Activate : KSPActionType.Deactivate));
            }
            return success;
        }

        public bool Event(string eventName)
        {
            bool success = true;
            foreach (var module in includedModules)
            {
                success &= Util.ActionHelper.ExecuteEventInClass(module, eventName);
            }
            return success;
        }


        public LuaTable ListEvents()
        {
            LuaTable toReturn = dataProvider.GetNewTable();

            HashSet<string> toMerge = new HashSet<string>();

            foreach (var mod in includedModules)
            {
                toMerge.UnionWith(Util.ActionHelper.ListEvents(mod));
            }

            int cnt = 1;
            foreach (var str in toMerge)
            {
                toReturn[cnt++] = str;
            }

            return toReturn;
        }

        public LuaTable ListActions()
        {
            LuaTable toReturn = dataProvider.GetNewTable();

            HashSet<string> toMerge = new HashSet<string>();

            foreach (var mod in includedModules)
            {
                toMerge.UnionWith(Util.ActionHelper.ListActions(mod));
            }

            int cnt = 1;
            foreach (var str in toMerge)
            {
                toReturn[cnt++] = str;
            }

            return toReturn;
        }
    }
}
