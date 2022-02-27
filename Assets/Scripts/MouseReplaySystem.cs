using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MouseReplaySystem : MonoBehaviour
{
	[DllImport("user32.dll")]
	static extern bool SetCursorPos(int X, int Y);

	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

	private const int MOUSEEVENTF_LEFTDOWN = 0x02;
	private const int MOUSEEVENTF_LEFTUP = 0x04;
	private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
	private const int MOUSEEVENTF_RIGHTUP = 0x10;

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
		if (isInRecordMode) Record();
		else if (isInReplayMode) Replay();
	}

	public void StartRecord()
	{
		isInRecordMode = true;
		isInReplayMode = false;
		replayIndex = 0;
	}

	public void StartReplay()
	{
		isInRecordMode = false;
		isInReplayMode = true;
		replayIndex = 0;
	}

	public void StopRecord()
	{
		isInRecordMode = false;
		isInReplayMode = false;
		replayIndex = 0;
	}

	void Record()
	{
		MouseRecord NewMouseRecord = new MouseRecord();
		NewMouseRecord.MousePosition = Input.mousePosition;
		NewMouseRecord.bIsLeftClickingDown = Input.GetMouseButtonDown(0);
		NewMouseRecord.bIsLeftClickingUp = Input.GetMouseButtonUp(0);
		NewMouseRecord.bIsRightClickingDown = Input.GetMouseButtonDown(1);
		NewMouseRecord.bIsRightClickingUp = Input.GetMouseButtonUp(1);
		mouseRecords.Add(NewMouseRecord);
	}

	void Replay()
	{
		if (mouseRecords.Count == 0) return;

		MouseRecord mouseRecord = mouseRecords[replayIndex];
		int mouseX = (int)mouseRecord.MousePosition.x;
		int mouseY = (int)mouseRecord.MousePosition.y;

		SetCursorPos(mouseX, monitorResolution.height - mouseY);
		if(mouseRecord.bIsLeftClickingDown) mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)mouseX, (uint)mouseY, 0, 0);
		if(mouseRecord.bIsLeftClickingUp) mouse_event(MOUSEEVENTF_LEFTUP, (uint)mouseX, (uint)mouseY, 0, 0);
		if(mouseRecord.bIsRightClickingDown) mouse_event(MOUSEEVENTF_RIGHTDOWN, (uint)mouseX, (uint)mouseY, 0, 0);
		if(mouseRecord.bIsRightClickingUp) mouse_event(MOUSEEVENTF_RIGHTUP, (uint)mouseX, (uint)mouseY, 0, 0);

		if (++replayIndex >= mouseRecords.Count)
		{
			StopRecord();
		}
	}
}
