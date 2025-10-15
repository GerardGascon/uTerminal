/*
 * init Class
 * 
 * This script initializes the uTerminal console manager in the Unity scene.
 * The console manager is initialized with GUI settings for displaying the console window.
 *  
 */

using UnityEngine;

namespace uTerminal.Samples
{
    public class init : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            uTerminal.ConsoleManager.Initialize(true); // Initialize the uTerminal console manager with GUI
        }

        // Update is called once per frame
        void Update()
        {
            // Update method left empty for now
        }
    }
}