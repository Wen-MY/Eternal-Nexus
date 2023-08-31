using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipText;

    public void SetText(string text)
    {
        tooltipText.text = text;
    }
}
