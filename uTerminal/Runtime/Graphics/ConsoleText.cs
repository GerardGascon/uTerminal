using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace uTerminal.Graphics
{
    /// <summary>
    /// Class for handling uTerminal console text.
    /// </summary>
    public class ConsoleText : MonoBehaviour
    {
        /// <summary>
        /// The count of the console text.
        /// </summary>
        public int count = 1;

        /// <summary>
        /// The TextMeshProUGUI component for displaying text.
        /// </summary>
        public TextMeshProUGUI text, counter;

        /// <summary>
        /// The Button component for interaction.
        /// </summary>
        public Button button;

        /// <summary>
        /// Copies the console text to the system clipboard.
        /// </summary>
        public void Copy()
        {
            GUIUtility.systemCopyBuffer = text.text;
        }

        /// <summary>
        /// Deletes the console text.
        /// </summary>
        public void Delete()
        {
            gameObject.SetActive(false);
        }
    }
}