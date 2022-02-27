using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Windows;

public class MouseReplaySystem : MonoBehaviour
{
	[DllImport("user32.dll")]
	static extern bool SetCursorPos(int X, int Y);

	private List<MouseRecord> mouseRecords;
	bool isInRecordMode;
	bool isInReplayMode;
	int replayIndex;

	Resolution monitorResolution;

	private void Start()
	{
		monitorResolution = Screen.currentResolution;
		mouseRecords = new List<MouseRecord>();
	}

	private void Update()
	{
		HandleRecordReplayInput();
	}

	private void FixedUpdate()
	{
		if (isInRecordMode) Record();
		else if (isInReplayMode) Replay();
	}

	void HandleRecordReplayInput()
	{
		if (Input.GetKey(KeyCode.A)) // Recording
		{
			isInRecordMode = true;
			isInReplayMode = false;
			replayIndex = 0;
		}
		if (Input.GetKey(KeyCode.D)) // Replaying
		{
			isInRecordMode = false;
			isInReplayMode = true;
		}
		if (Input.GetKey(KeyCode.X)) // Cancel
		{
			Cancel();
		}
	}

	void Record()
	{
		MouseRecord NewMouseRecord = new MouseRecord();
		NewMouseRecord.MousePosition = Input.mousePosition;
		NewMouseRecord.bIsLeftClicking = Input.GetMouseButton(0);
		NewMouseRecord.bIsRightClicking = Input.GetMouseButton(1);
		mouseRecords.Add(NewMouseRecord);
	}

	void Replay()
	{
		if (mouseRecords.Count == 0) return;

		MouseRecord mouseRecord = mouseRecords[replayIndex];
		int mouseX = (int)mouseRecord.MousePosition.x;
		int mouseY = (int)mouseRecord.MousePosition.y;

		SetCursorPos(mouseX, monitorResolution.height - mouseY);
		if(++replayIndex >= mouseRecords.Count)
		{
			Cancel();
		}
		
	}

	void Cancel()
	{
		isInRecordMode = false;
		isInReplayMode = false;
		mouseRecords.Clear();
		replayIndex = 0;
	}
}
