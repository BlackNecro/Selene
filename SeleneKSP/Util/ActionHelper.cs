using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SeleneKSP.Util
{
    class ActionHelper
    {


        static Dictionary<Type, Dictionary<string, MethodInfo>> cachedActionMethods = new Dictionary<Type, Dictionary<string, MethodInfo>>();
        static Dictionary<Type, Dictionary<string, MethodInfo>> cachedEventMethods = new Dictionary<Type, Dictionary<string, MethodInfo>>();
        static Dictionary<Type, Dictionary<string, FieldInfo>> cachedFieldInfo = new Dictionary<Type, Dictionary<string, FieldInfo>>();
        public static bool ExecuteActionInClass(object objectToUse,string methodName,KSPActionParam param)
        {
            Type type = objectToUse.GetType();
            MethodInfo methodToUse = null;
            if(cachedActionMethods.ContainsKey(type) && cachedActionMethods[type].ContainsKey(methodName))
            {                
                    methodToUse = cachedActionMethods[type][methodName];            
            } 
            else
            {
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is KSPAction)
                        {
                            if (((KSPAction)attribute).guiName == methodName)
                            {
                                methodToUse = method;
                                if(!cachedActionMethods.ContainsKey(type))
                                {
                                    cachedActionMethods[type] = new Dictionary<string, MethodInfo>();
                                }
                                cachedActionMethods[type][methodName] = method;
                            }
                        }
                    }
                }
                if(methodToUse == null)
                {
                    if (!cachedActionMethods.ContainsKey(type))
                    {
                        cachedActionMethods[type] = new Dictionary<string, MethodInfo>();
                    }
                    cachedActionMethods[type][methodName] = null;
                }
            }

            if(methodToUse != null)
            {
                methodToUse.Invoke(objectToUse, new object[] { param });
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ExecuteEventInClass(object objectToUse, string methodName)
        {
            Type type = objectToUse.GetType();
            MethodInfo methodToUse = null;
            if (cachedEventMethods.ContainsKey(type) && cachedEventMethods[type].ContainsKey(methodName))
            {
                methodToUse = cachedEventMethods[type][methodName];
            }
            else
            {
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is KSPEvent)
                        {
                            if (((KSPEvent)attribute).guiName == methodName)
                            {
                                methodToUse = method;
                                if (!cachedEventMethods.ContainsKey(type))
                                {
                                    cachedEventMethods[type] = new Dictionary<string, MethodInfo>();
                                }
                                cachedEventMethods[type][methodName] = method;
                            }
                        }
                    }
                }
                if(methodToUse == null)
                {
                    if (!cachedEventMethods.ContainsKey(type))
                    {
                        cachedEventMethods[type] = new Dictionary<string, MethodInfo>();
                    }
                    cachedEventMethods[type][methodName] = null;
                }
            }

            if (methodToUse != null)
            {
                methodToUse.Invoke(objectToUse, new object[] { });
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SetFieldValueInClass(object objectToUse,string fieldName, object value)
        {
            Type type = objectToUse.GetType();
            FieldInfo fieldToUse = null;
            if (cachedFieldInfo.ContainsKey(type) && cachedFieldInfo[type].ContainsKey(fieldName))
            {
                fieldToUse = cachedFieldInfo[type][fieldName];
            }
            else
            {
                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    var attributes = field.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is KSPField)
                        {
                            if (((KSPField)attribute).guiName == fieldName)
                            {
                                fieldToUse = field;
                                if (!cachedFieldInfo.ContainsKey(type))
                                {
                                    cachedFieldInfo[type] = new Dictionary<string, FieldInfo>();
                                }
                                cachedFieldInfo[type][fieldName] = field;
                            }
                        }
                    }
                }
                if(fieldToUse == null)
                {
                    if (!cachedFieldInfo.ContainsKey(type))
                    {
                        cachedFieldInfo[type] = new Dictionary<string, FieldInfo>();
                    }
                    cachedFieldInfo[type][fieldName] = null;
                }
            }

            if (fieldToUse != null)
            {
                if (fieldToUse.FieldType == typeof(bool) && value is bool)
                {
                    fieldToUse.SetValue(objectToUse, value);
                }
                else if (fieldToUse.FieldType == typeof(Single) && value is double)
                {
                    Single newVal = Convert.ToSingle(value);
                    fieldToUse.SetValue(objectToUse, newVal);
                }
                else if (fieldToUse.FieldType == typeof(double) && value is double)
                {
                    fieldToUse.SetValue(objectToUse, value);
                }
                else if(fieldToUse.FieldType == typeof(string) && value is string)
                {
                    fieldToUse.SetValue(objectToUse, value);    
                } 
                else
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static object GetFieldValueInClass(object objectToUse, string fieldName)
        {
            Type type = objectToUse.GetType();
            FieldInfo fieldToUse = null;
            if (cachedFieldInfo.ContainsKey(type) && cachedFieldInfo[type].ContainsKey(fieldName))
            {
                fieldToUse = cachedFieldInfo[type][fieldName];
            }
            else
            {
                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    var attributes = field.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is KSPField)
                        {
                            if (((KSPField)attribute).guiName == fieldName)
                            {
                                fieldToUse = field;
                                if (!cachedFieldInfo.ContainsKey(type))
                                {
                                    cachedFieldInfo[type] = new Dictionary<string, FieldInfo>();
                                }
                                cachedFieldInfo[type][fieldName] = field;
                            }
                        }
                    }
                }
                if(fieldToUse == null)
                {
                    if (!cachedFieldInfo.ContainsKey(type))
                    {
                        cachedFieldInfo[type] = new Dictionary<string, FieldInfo>();
                    }
                    cachedFieldInfo[type][fieldName] = null;
                }
            }

            if (fieldToUse != null)
            {
                return fieldToUse.GetValue(objectToUse);
            }
            else
            {
                return null;
            }
        }
        

        static Dictionary<Type, HashSet<string>> cachedFields = new Dictionary<Type, HashSet<string>>();

        public static HashSet<string> ListFields(object toUse)
        {

            Type type = toUse.GetType();

            if (cachedFields.ContainsKey(type))
            {
                return cachedFields[type];
            }


            HashSet<string> toReturn = new HashSet<string>();
            cachedFields[type] = toReturn;

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute is KSPField)
                    {
                        string fieldName = ((KSPField)attribute).guiName;

                        toReturn.Add(fieldName);                        
                        if (!cachedFieldInfo.ContainsKey(type))
                        {
                            cachedFieldInfo[type] = new Dictionary<string, FieldInfo>();
                        }
                        cachedFieldInfo[type][fieldName] = field;                        
                    }
                }
            }

            return toReturn;
        }              


        static Dictionary<Type, HashSet<string>> cachedActions = new Dictionary<Type, HashSet<string>>();

        public static HashSet<string> ListActions(object toUse)
        {

            Type type = toUse.GetType();

            if (cachedActions.ContainsKey(type))
            {
                return cachedActions[type];
            }


            HashSet<string> toReturn = new HashSet<string>();
            cachedActions[type] = toReturn;

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute is KSPAction)
                    {
                        string actionName = ((KSPAction)attribute).guiName;

                        toReturn.Add(actionName);
                        if (!cachedActionMethods.ContainsKey(type))
                        {
                            cachedActionMethods[type] = new Dictionary<string, MethodInfo>();
                        }
                        cachedActionMethods[type][actionName] = method;
                    }
                }
            }

            return toReturn;
        }
                                   
        static Dictionary<Type, HashSet<string>> cachedEvents = new Dictionary<Type, HashSet<string>>();

        public static HashSet<string> ListEvents(object toUse)
        {

            
            Type type = toUse.GetType();
            UnityEngine.Debug.Log("Getting Events for " + type.ToString());

            if (cachedEvents.ContainsKey(type))
            {
                UnityEngine.Debug.Log(" Was cached already returning old value");
                return cachedEvents[type];
            }


            HashSet<string> toReturn = new HashSet<string>();
            cachedEvents[type] = toReturn;
            UnityEngine.Debug.Log(" Fresh list");

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(true);
                UnityEngine.Debug.Log(" checking method " + method.Name);
                foreach (var attribute in attributes)
                {
                    if (attribute is KSPEvent)
                    {
                        UnityEngine.Debug.Log("  method is event");
                        string eventName = ((KSPEvent)attribute).guiName;

                        toReturn.Add(eventName);
                        
                        if (!cachedEventMethods.ContainsKey(type))
                        {
                            UnityEngine.Debug.Log("   type wasn't cached, adding new dict");
                            cachedEventMethods[type] = new Dictionary<string, MethodInfo>();
                        }
                        cachedEventMethods[type][eventName] = method;
                        UnityEngine.Debug.Log("   caching method");
                    }
                }
            }
                       
            return toReturn;
        }
                                    
    }
}
