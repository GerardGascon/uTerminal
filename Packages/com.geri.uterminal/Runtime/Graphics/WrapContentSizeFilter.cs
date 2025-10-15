using UnityEngine;

namespace uTerminal.Graphics
{
    /// <summary>
    /// Class for wrapping content size in uTerminal graphics.
    /// </summary>
    public class WrapContentSizeFilter : MonoBehaviour
    {
        /// <summary>
        /// The offset to apply to the size.
        /// </summary>
        [SerializeField] Vector2 offset;

        /// <summary>
        /// The child RectTransform.
        /// </summary>
        [SerializeField] RectTransform child;

        private void LateUpdate()
        {
            ((RectTransform)transform).sizeDelta = child.sizeDelta + offset;
        }
    }
}