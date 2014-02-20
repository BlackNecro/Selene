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
    class PartInfo : IPartInfo
    {
        KSPDataProvider dataProvider;
        KPart part;
        HashSet<PartModule> includedModules = new HashSet<PartModule>();



        public PartInfo(KSPDataProvider provider, KPart toUse)
        {
            dataProvider = provider;
            part = toUse;
            foreach (PartModule mod in toUse.Modules)
            {
                includedModules.Add(mod);
            }                               
        }

        public SeleneVector GetOffset()
        {
            return new SeleneVector(part.orgPos);
        }

        public string GetName()
        {
            return part.partInfo.name;
        }

        public string GetTitle()
        {
            return part.partInfo.title;
        }


        public double GetMass()
        {
            return part.mass + part.GetResourceMass();
        }

        public double GetDryMass()
        {
            return part.mass;
        }

        public SeleneQuaternion GetRotation()
        {
            return part.orgRot;
        }

        public object GetField(string fieldName)
        {
            foreach(var module in includedModules)
            {
                object val = Util.ActionHelper.GetFieldValueInClass(module,fieldName);
                if(val != null)
                {
                    return val;
                }                
            }
            return null;
        }

        public bool SetField(string fieldName, object value)
        {
            bool success = true;
            foreach (var module in includedModules)
            {
                success &= Util.ActionHelper.SetFieldValueInClass(module, fieldName, value);
            }
            return success;
        }


        public bool Action(string actionName, bool activate)
        {
            bool success = true;
            foreach(var module in includedModules)
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

        public override string ToString()
        {
            return GetName();
        }





        public LuaTable GetActions()
        {
            LuaTable toReturn = dataProvider.GetNewTable();

            HashSet<string> toMerge = new HashSet<string>();

            foreach(var mod in includedModules)
            {
                toMerge.UnionWith(Util.ActionHelper.ListActions(mod));
            }

            int cnt = 1;
            foreach(var str in toMerge)
            {
                toReturn[cnt++] = str;
            }

            return toReturn;
        }

        public LuaTable GetEvents()
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

        public LuaTable GetFields()
        {
            LuaTable toReturn = dataProvider.GetNewTable();

            HashSet<string> toMerge = new HashSet<string>();

            foreach (var mod in includedModules)
            {
                toMerge.UnionWith(Util.ActionHelper.ListFields(mod));
            }

            int cnt = 1;
            foreach (var str in toMerge)
            {
                toReturn[cnt++] = str;
            }

            return toReturn;
        }


        public void Activate()
        {
            part.force_activate();
        }
    }
}
