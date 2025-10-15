using UnityEngine;

namespace uTerminal
{
    /// <summary>
    /// Class for managing uTerminal console settings.
    /// </summary> 
    public class ConsoleSettings : ScriptableObject
    {
        /// <summary>
        /// Gets the version of the console.
        /// </summary>
        public static string Version
        {
            get
            {
                return "1.0";
            }
        }

        /// <summary>
        /// The static instance of ConsoleSettings.
        /// </summary>
        private static ConsoleSettings _Instance;

        /// <summary>
        /// Gets the static instance of ConsoleSettings.
        /// </summary>
        public static ConsoleSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = Resources.Load<ConsoleSettings>("ConsoleSettings");

                return _Instance;
            }
        }

        /// <summary>
        /// Flag indicating whether to use namespace.
        /// </summary>
        public bool useNamespace = true;

        /// <summary>
        /// Flag indicating whether to show the start message.
        /// </summary>
        public bool showStartMessage = true;

        /// <summary>
        /// Flag indicating whether to show the version.
        /// </summary>
        public bool showVersion = true;

        /// <summary>
        /// The start message to display.
        /// </summary>
        public string startMessage;

        /// <summary>
        /// The chat command prefix.
        /// </summary>
        public string chatCommandPrefix = "/";

        /// <summary>
        /// The button to open terminal.
        /// </summary>
        public KeyCode openTerminalKey = KeyCode.F1;
    }
}
