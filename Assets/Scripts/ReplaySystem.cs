using UnityEngine;
using UnityEngine.UI;

public class ReplaySystem : MonoBehaviour
{
    [SerializeField] Button startStopRecordButton;
    [SerializeField] Button loadPlayButton;
    [SerializeField] InputField recordNameField;
    [SerializeField] Sprite StartRecordSprite;
    [SerializeField] Sprite StopRecordSprite;

    MouseReplaySystem mouseReplaySystem;
    bool isRecording;

	private void Start()
	{
		mouseReplaySystem = GetComponent<MouseReplaySystem>();
	}

    public void HandleReplayButton()
	{
        if (!isRecording)
		{
            mouseReplaySystem.StartReplay();
        }
    }

	public void HandleRecordButton()
	{
        isRecording = !isRecording;
        if (isRecording)
		{
            mouseReplaySystem.StartRecord();
            startStopRecordButton.image.sprite = StopRecordSprite;
        }
		else
		{
            mouseReplaySystem.StopRecord();
            startStopRecordButton.image.sprite = StartRecordSprite;
        }
    }
}
