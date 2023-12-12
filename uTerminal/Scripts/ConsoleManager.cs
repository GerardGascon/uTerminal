using uTerminal.UI;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace uTerminal
{
    public class ConsoleManager : MonoBehaviour
    {
        private GameObject _consoleUI;

        private static ConsoleManager _instance;

        private static ConsoleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("ConsoleBehaviour").AddComponent<ConsoleManager>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        private static void InitializeGraphics()
        {
            if (Instance._consoleUI == null)
            {
                Instance._consoleUI = Instantiate(Resources.Load<GameObject>("CanvasConsoleBehaviour"));
                DontDestroyOnLoad(Instance._consoleUI);
            }
        }

        public static void Initialize(bool initializeGUI = false)
        {
            if (initializeGUI)
            {
                InitializeGraphics();
                Console.Init(Instance._consoleUI.GetComponent<uTerminal.UI.UIManager>());
            }

            Terminal.Initialize();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1) && _consoleUI)
            {
                _consoleUI.SetActive(!_consoleUI.activeSelf);
            }
        }
    }
}