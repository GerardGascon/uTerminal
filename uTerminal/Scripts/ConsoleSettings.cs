using UnityEngine;

namespace uTerminal
{
    public class ConsoleSettings : ScriptableObject
    { 
        public static string Version
        {
            get
            {
                return "1.0";
            }
        }

        static ConsoleSettings _Instance;

        public static ConsoleSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = Resources.Load<ConsoleSettings>("ConsoleSettings");

                return _Instance;
            }
        }

        public bool useNamespace = true;
        public bool showStartMessage = true;
        public bool showVersion = true; 
        public string startMessage;
        public string chatCommandPrefix = "/";  
    }
}