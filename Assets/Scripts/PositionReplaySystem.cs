using System.Collections.Generic;
using UnityEngine;

public class PositionReplaySystem : MonoBehaviour
{
	ReplaySystem replaySystem;
	private List<PositionRecord> positionRecords;
	bool isInRecordMode;
	bool isInReplayMode;
	int replayIndex;

	private void Start()
	{
		positionRecords = new List<PositionRecord>();
		replaySystem = FindObjectOfType<ReplaySystem>();
		replaySystem.OnStartRecord += StartRecord;
		replaySystem.OnStoptRecord += StopRecord;
		replaySystem.OnStartReplay += StartReplay;
		replaySystem.OnClearRecord += ClearRecord;
	}

	private void Update()
	{
		if (isInRecordMode) Record();
		else if (isInReplayMode) Replay();
	}

	void StartRecord()
	{
		isInRecordMode = true;
		isInReplayMode = false;
		replayIndex = 0;
	}

	void StartReplay()
	{
		isInRecordMode = false;
		isInReplayMode = true;
		replayIndex = 0;
	}

	void StopRecord()
	{
		isInRecordMode = false;
		isInReplayMode = false;
		replayIndex = 0;
	}

	void ClearRecord()
	{
		positionRecords.Clear();
	}

	void Record()
	{
		PositionRecord positionRecord = new PositionRecord();
		positionRecord.position = transform.position;
		positionRecords.Add(positionRecord);
	}

	void Replay()
	{
		if (positionRecords.Count == 0) return;

		PositionRecord positionRecord = positionRecords[replayIndex];
		transform.position = positionRecord.position;

		if (++replayIndex >= positionRecords.Count)
		{
			StopRecord();
		}
	}
}
