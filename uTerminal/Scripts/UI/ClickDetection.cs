using System; 
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetection : MonoBehaviour, IPointerClickHandler
{
    public Action onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
