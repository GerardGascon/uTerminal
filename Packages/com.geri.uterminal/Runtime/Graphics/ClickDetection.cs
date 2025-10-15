using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace uTerminal.Graphics
{
    /// <summary>
    /// Class for detecting click events on a UI element.
    /// </summary>
    public class ClickDetection : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// Action to be invoked on click.
        /// </summary>
        public Action onClick;

        /// <summary>
        /// Invoked when the UI element is clicked.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}
