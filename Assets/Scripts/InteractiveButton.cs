using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Tooltip tooltip;
    [SerializeField] Popup popup;
    [SerializeField] Vector3 tooltipOffset;
    [SerializeField] float tooltipShowAfter = 0.5f;
    [SerializeField] string popupMessage;

	bool mouseIsHovering;
	bool showTooltip;
	float hoverTimePassed;

	private void Update()
	{
		HandleMouseHoverTimer();
		HandleTooltipVisibility();
	}

	void HandleTooltipVisibility()
	{
		if (showTooltip)
		{
			tooltip.gameObject.SetActive(true);
			tooltip.transform.position = Input.mousePosition + tooltipOffset;
		}
		else
		{
			tooltip.gameObject.SetActive(false);
		}
	}

	void HandleMouseHoverTimer()
	{
		if (mouseIsHovering)
		{
			hoverTimePassed += Time.deltaTime;
			if (hoverTimePassed > tooltipShowAfter) showTooltip = true;
		}
		else
		{
			hoverTimePassed = 0;
			showTooltip = false;
		}
	}

	public void OpenPopup()
	{
		Canvas canvas = FindObjectOfType<Canvas>();
		if (canvas == null) return;

		Popup spawnedPopup = Instantiate(popup, canvas.transform);
		if (spawnedPopup == null) return;
		
		spawnedPopup.SetPopupMessage(popupMessage);
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
