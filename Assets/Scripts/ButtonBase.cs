using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InteractiveButton[] interactiveButtons;

	InteractiveButton interactiveButton;
	int currentButtonIndex = 0;
	bool mouseIsHovering;
	Vector3 offset;

	private void Start()
	{
		interactiveButton = Instantiate(interactiveButtons[currentButtonIndex], transform);
	}

	private void Update()
	{
		HandleMouseMovement();
		HandleSwitchingButtons();
	}

	void HandleMouseMovement()
	{
		if(Input.GetMouseButtonDown(0)) offset = transform.position - Input.mousePosition;
		if (Input.GetMouseButton(0) && mouseIsHovering)
		{
			transform.position = offset + Input.mousePosition;
		}
	}

	void HandleSwitchingButtons()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (interactiveButton != null) Destroy(interactiveButton.gameObject);
			if (++currentButtonIndex >= interactiveButtons.Length) currentButtonIndex = 0;
			interactiveButton = Instantiate(interactiveButtons[currentButtonIndex], transform);
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
