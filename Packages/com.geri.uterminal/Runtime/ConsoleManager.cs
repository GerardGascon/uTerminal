using System;
using uTerminal.Graphics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

		public static event Action OnConsoleOpen;
		public static event Action OnConsoleClose;
		public static bool IsConsoleVisible { get; private set; }

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

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoStart() {
			Initialize(true);
			SceneManager.activeSceneChanged += (_, _) => Initialize(true);
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
			if (InputAbstraction.OpenKeyDown() && _consoleUI)
			{
				_consoleUI.SetActive(!_consoleUI.activeSelf);
				if (_consoleUI.activeSelf)
					OnConsoleOpen?.Invoke();
				else
					OnConsoleClose?.Invoke();
				IsConsoleVisible = _consoleUI.activeSelf;

			}
		}
	}
}