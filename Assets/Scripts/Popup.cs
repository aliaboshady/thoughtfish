using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] Text messageText;

    public void SetPopupMessage(string message)
	{
        messageText.text = message;
	}

    public void ClosePopup()
	{
        Destroy(gameObject);
	}
}
