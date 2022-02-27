using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    [SerializeField] InteractiveButton[] interactiveButtons;

	InteractiveButton interactiveButton;
	int currentButtonIndex = 0;

	private void Start()
	{
		interactiveButton = Instantiate(interactiveButtons[currentButtonIndex], transform);
	}

	private void Update()
	{
		HandleSwitchingButtons();
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
}
