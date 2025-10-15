using uTerminal.Graphics;
using UnityEngine;

namespace uTerminal {
	/// <summary>
	/// Class for managing the uTerminal console.
	/// </summary>
	public class ConsoleManager : MonoBehaviour {
		/// <summary>
		/// The GameObject for the console UI.
		/// </summary>
		private GameObject _consoleUI;

		/// <summary>
		/// The static instance of the ConsoleManager.
		/// </summary>
		private static ConsoleManager _instance;

		/// <summary>
		/// Gets the static instance of the ConsoleManager.
		/// </summary>
		private static ConsoleManager Instance {
			get {
				if (_instance == null) {
					_instance = new GameObject("ConsoleBehaviour").AddComponent<ConsoleManager>();
					DontDestroyOnLoad(_instance);
				}

				return _instance;
			}
		}

		/// <summary>
		/// Initializes the graphics for the console.
		/// </summary>
		private static void InitializeGraphics() {
			if (Instance._consoleUI == null) {
				Instance._consoleUI = Instantiate(Resources.Load<GameObject>("Prefabs/CanvasConsoleBehaviour"));
				DontDestroyOnLoad(Instance._consoleUI);
			}
		}

		/// <summary>
		/// Initializes the uTerminal console.
		/// </summary>
		/// <param name="initializeGUI">Whether to initialize the GUI.</param>
		public static void Initialize(bool initializeGUI = false) {
			if (initializeGUI) {
				InitializeGraphics();
				uTerminalDebug.Init(Instance._consoleUI.GetComponent<uTerminalGraphics>());
			}

			Terminal.Initialize();
		}

		/// <summary>
		/// Updates the console UI visibility on key press.
		/// </summary>
		public void Update() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current[ConsoleSettings.Instance.openTerminalKey].wasPressedThisFrame && _consoleUI)
			{
				_consoleUI.SetActive(!_consoleUI.activeSelf);
			}
#else
			if (Input.GetKeyDown(ConsoleSettings.Instance.openTerminalKey) && _consoleUI) {
				_consoleUI.SetActive(!_consoleUI.activeSelf);
			}
#endif
		}
	}
}