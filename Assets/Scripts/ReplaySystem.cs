using System;
using UnityEngine;
using UnityEngine.UI;

public class ReplaySystem : MonoBehaviour
{
    [SerializeField] Button startStopRecordButton;
    [SerializeField] Button loadPlayButton;
    [SerializeField] InputField recordNameField;
    [SerializeField] Sprite StartRecordSprite;
    [SerializeField] Sprite StopRecordSprite;

    public event Action OnStartRecord;
    public event Action OnStoptRecord;
    public event Action OnStartReplay;
    public event Action OnClearRecord;

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
            OnStartReplay?.Invoke();
        }
    }

	public void HandleRecordButton()
	{
        isRecording = !isRecording;
        if (isRecording)
		{
            mouseReplaySystem.ClearRecord();
            OnClearRecord?.Invoke();
            mouseReplaySystem.StartRecord();
            OnStartRecord?.Invoke();
            startStopRecordButton.image.sprite = StopRecordSprite;
        }
		else
		{
            mouseReplaySystem.StopRecord();
            OnStoptRecord?.Invoke();
            startStopRecordButton.image.sprite = StartRecordSprite;
        }
    }
}
