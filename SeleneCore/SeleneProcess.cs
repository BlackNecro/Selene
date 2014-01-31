using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public class SeleneProcess
    {
        private int priority = 0;

        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                if (Parent != null)
                {
                    Parent.Children = Parent.Children.OrderBy(proc => proc.priority).ToList();
                }
            }
        }

        public SeleneProcess Parent = null;
        public List<SeleneProcess> Children = new List<SeleneProcess>();

        public List<List<SeleneCallback>> Callbacks = new List<List<SeleneCallback>>();
        SeleneLuaState luaState;
        LuaTable environment;

        public LuaTable Env
        {
            get { return environment; }
        }

        SeleneCallback currentCallback;
        public string fileName = "";
        public string source = "";
        bool fromFile = false;
        bool run = false;

        public bool toDelete = false;

        public bool Active
        {
            get { return run; }
            set { run = value; }
        }

        public void AddChildProcess(SeleneProcess proc)
        {
            Children.Add(proc);
            Children = Children.OrderBy(child => child.Priority).ToList();
            proc.Parent = this;
        }

        public void AddCallback(CallbackType type, LuaFunction callback)
        {
            Callbacks[(int)type].Add(new SeleneCallback(callback, luaState, type));
        }

        public SeleneProcess(SeleneLuaState state)
        {
            luaState = state;
            foreach (var type in Enum.GetValues(typeof(CallbackType)))
            {
                Callbacks.Add(new List<SeleneCallback>());
            }
        }

        public bool LoadFromFile(string path)
        {
            fileName = path;
            source = ((ILuaDataProvider)luaState).ReadFile(path);
            fromFile = true;

            LoadSource();
            return true;
        }

        public bool LoadFromString(string newSource, string file)
        {
            fromFile = false;
            fileName = file;
            source = newSource;

            LoadSource();
            return true;
        }

        private bool LoadSource()
        {
            Children.Clear();
            foreach(var callbackList in Callbacks)
            {
                callbackList.Clear();
            }
            luaState.CurrentProcess = this;
            environment = luaState.GetNewEnvironment();
            try
            {
                LuaFunction originalFunction = luaState.LuaState.LoadString(source, fileName);
                LuaTable newEnvironment = luaState.GetNewEnvironment();
                LuaFunction envChanger = (LuaFunction)luaState.LuaState.LoadString("local args = {...} debug.setupvalue(args[1],1,args[2])", "Environment Changer");
                envChanger.Call((object)originalFunction, (object)newEnvironment);
                originalFunction.Call();
            }
            catch (NLua.Exceptions.LuaException ex)
            {
                luaState.Log(string.Format("Error Loading Source {0}:\n{1} ", fileName, ex.Message));
                luaState.revertCallStack();
                return false;
            }
            luaState.revertCallStack();
            return true;
        }

        public bool Reload()
        {
            if (fromFile)
            {
                return LoadFromFile(fileName);
            }
            else
            {
                return LoadFromString(source, fileName);
            }
        }

        public void Delete()
        {
            Parent.Children.Remove(this);
        }


        public void Execute(CallbackType type, object[] parameters)
        {
            if (run)
            {
                foreach (var child in Children)
                {
                    child.Execute(type, parameters);
                }
                luaState.CurrentProcess = this;
                foreach (var callback in Callbacks[(int)type])
                {
                    currentCallback = callback;
                    if (!callback.Execute(parameters))
                    {
                        run = false;
                        luaState.revertCallStack();
                        return;
                    }
                }
                luaState.revertCallStack();
            }
        }

        public void Log(string message, int priority)
        {
            UnityEngine.Debug.Log("***");
            UnityEngine.Debug.Log("Process " + this.fileName);
            if (currentCallback != null)
            {
                UnityEngine.Debug.Log("Callback" + this.currentCallback.CallbackType);
            }
            UnityEngine.Debug.Log(message);
            UnityEngine.Debug.Log("***");
        }
    }
}
