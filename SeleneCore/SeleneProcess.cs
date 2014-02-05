using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public class SeleneProcess 
    {
        public class LogEntry
        {
            public string message;
            public int priority;
            public SeleneCallback callback;
            public LogEntry(SeleneCallback Callback, string Message, int Prio)
            {
                callback = Callback;
                message = Message;
                priority = Prio;
            }
                        
        }


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

        
        public LinkedList<LogEntry> LogList = new LinkedList<LogEntry>();

        public LuaTable Env
        {
            get { return environment; }
        }


        public bool IsCustomVariable(object key)
        {
            if(luaState.BaseEnvironment[key] == null)
            {
                return true;
            }
            return false;
        }

        public LuaTable CustomEnvironment
        {
            get
            {
                LuaTable toReturn = luaState.GetNewTable();
                LuaTable myEnv = Env;                
                foreach (object key in myEnv.Keys)
                {   
                    if(luaState.BaseEnvironment[key] == null)
                    {
                        toReturn[key] = myEnv[key];
                    }    
                }
                return toReturn;
            }            
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

        public bool IsActive
        {
            get
            {
                if(!Active)
                {
                    return false;
                }
                if(Parent != null)
                {
                    return Parent.IsActive;
                }
                return Active;
            }
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
            LogList.Clear();

            List<SeleneProcess> toDelete = new List<SeleneProcess>(Children);
            toDelete.ForEach(child => child.Delete());

            foreach(var callbackList in Callbacks)
            {
                callbackList.Clear();
            }
            luaState.CurrentProcess = this;
            environment = luaState.GetNewEnvironment();
            try
            {                
                LuaFunction originalFunction = luaState.LuaState.LoadString(source, fileName);                
                LuaFunction envChanger = (LuaFunction)luaState.LuaState.LoadString(@"
                    local args = {...}                                 
                    setfenv(args[1],args[2])                    
                    return args[1]
                        "
                    , "Environment Changer");
                LuaFunction newFunction = (LuaFunction)envChanger.Call(originalFunction,environment)[0];
                newFunction.Call();
                
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
            foreach (var proc in Children)
            {
                proc.Delete();
            }
            if (this.Parent != null)
            {
                Parent.Children.Remove(this);
                this.Parent = null;
            }
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


            LogList.AddLast(new LogEntry(this.currentCallback,message,priority));
                                 /*
            UnityEngine.Debug.Log("***");
            UnityEngine.Debug.Log("Process " + this.fileName);
            if (currentCallback != null)
            {
                UnityEngine.Debug.Log("Callback" + this.currentCallback.CallbackType);
            }
            UnityEngine.Debug.Log(message);
            UnityEngine.Debug.Log("***");          */
        }
    }
}
