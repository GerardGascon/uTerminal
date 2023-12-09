using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleText : MonoBehaviour
{
    public int count = 1;
    public TextMeshProUGUI text, counter;
    public Button button;

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = text.text;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}
