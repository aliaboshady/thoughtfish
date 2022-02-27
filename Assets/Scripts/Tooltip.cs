using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Text textObject;
    [SerializeField] string content;

	private void Start()
	{
		textObject.text = content;
	}
}
