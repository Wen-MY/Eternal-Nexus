using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipManager : MonoBehaviour
{
    public Tooltip tooltipPrefab;
    private Tooltip currentTooltip;

    void Awake()
    {
        tooltipPrefab.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentTooltip != null)
            currentTooltip.transform.position = Input.mousePosition;
    }

    public void ShowTooltip(string text)
    {
        if (currentTooltip == null)
            currentTooltip = Instantiate(tooltipPrefab, transform);
        currentTooltip.SetText(text);
        currentTooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        if (currentTooltip != null)
        {
            currentTooltip.gameObject.SetActive(false);
            Destroy(currentTooltip.gameObject);
            currentTooltip = null;
        }
    }
}
