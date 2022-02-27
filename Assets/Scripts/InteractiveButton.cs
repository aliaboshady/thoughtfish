using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Tooltip tooltip;
    [SerializeField] Vector3 tooltipOffset;

	bool mouseIsHovering;

	private void Update()
	{
		HandleTooltipVisibility();
	}

	void HandleTooltipVisibility()
	{
		if (mouseIsHovering)
		{
			tooltip.gameObject.SetActive(true);
			tooltip.transform.position = Input.mousePosition + tooltipOffset;
		}
		else
		{
			tooltip.gameObject.SetActive(false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseIsHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseIsHovering = false;
	}
}
