using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;
    public Vector3 tooltipOffset = new Vector3(0, 0, 0); 

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Vector3 tooltipPosition = transform.position + tooltipOffset;

        ToolTipManager._instance.ShowToolTip(message, tooltipPosition);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ToolTipManager._instance.HideToolTip();
    }
}
