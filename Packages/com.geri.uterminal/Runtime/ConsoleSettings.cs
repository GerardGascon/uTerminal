using UnityEngine;

namespace uTerminal {
	/// <summary>
	/// Class for managing uTerminal console settings.
	/// </summary>
	public class ConsoleSettings : ScriptableObject {
		/// <summary>
		/// Gets the version of the console.
		/// </summary>
		public static string Version => "1.0";

		/// <summary>
		/// The static instance of ConsoleSettings.
		/// </summary>
		private static ConsoleSettings _instance;

		/// <summary>
		/// Gets the static instance of ConsoleSettings.
		/// </summary>
		public static ConsoleSettings Instance {
			get {
				if (_instance == null)
					_instance = Resources.Load<ConsoleSettings>("ConsoleSettings");

				return _instance;
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
#if ENABLE_INPUT_SYSTEM
		public UnityEngine.InputSystem.Key openTerminalKey = UnityEngine.InputSystem.Key.F1;
#else
		public KeyCode openTerminalKey = KeyCode.F1;
#endif
	}
}