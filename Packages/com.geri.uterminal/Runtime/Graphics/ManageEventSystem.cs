using UnityEngine;
using UnityEngine.EventSystems;

namespace uTerminal.Graphics
{
    public class ManageEventSystem : MonoBehaviour
    {
        void Start()
        {
            // Try to get the existing EventSystem in the scene
            EventSystem eventSystem = FindAnyObjectByType<EventSystem>();

            // If not found, create a new one
            if (eventSystem == null)
            {
                GameObject eventSystemObject = new GameObject("EventSystem");
                eventSystem = eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();

                Debug.Log("EventSystem created dynamically in the scene.");
            }
            else
            {
                Debug.Log("EventSystem found in the scene. Now you can check for events.");
            }
        }
    }
}