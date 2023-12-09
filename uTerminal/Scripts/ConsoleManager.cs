using uTerminal.UI;
using UnityEngine;

namespace uTerminal
{
    public class ConsoleManager : MonoBehaviour
    {
        private UIManager _consoleUI; 
         
        private static ConsoleManager _instance;

        public static ConsoleManager Instance
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

        private void InitializeGraphics()
        {
            if (!_consoleUI)
            {
                _consoleUI = Instantiate(Resources.Load<GameObject>("CanvasConsoleBehaviour")).GetComponent<UIManager>();  
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Initialize(bool initializeGUI = false)
        {
            if (initializeGUI) InitializeGraphics();

            Terminal.Initialize();
        } 
    }
}