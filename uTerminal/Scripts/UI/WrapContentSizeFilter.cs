using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class WrapContentSizeFilter : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] RectTransform child;
     
    private void LateUpdate() 
    {
        ((RectTransform)transform).sizeDelta = child.sizeDelta + offset;
    }
}
