using System;
using UnityEngine;

public class FGTouchInputProvider : FGInputProvider
{
	public int maxTouches = 5;

	public bool fixAndroidTouchIdBug = true;

	private int touchIdOffset;

	private Touch nullTouch = default(Touch);

	private int[] finger2touchMap;

	public override int MaxSimultaneousFingers
	{
		get
		{
			return this.maxTouches;
		}
	}

	private void Start()
	{
		this.finger2touchMap = new int[this.maxTouches];
	}

	private void Update()
	{
		this.UpdateFingerTouchMap();
	}

	private void UpdateFingerTouchMap()
	{
		for (int i = 0; i < this.finger2touchMap.Length; i++)
		{
			this.finger2touchMap[i] = -1;
		}
		if (this.fixAndroidTouchIdBug && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
		{
			this.touchIdOffset = Input.touches[0].fingerId;
		}
		for (int j = 0; j < Input.touchCount; j++)
		{
			int num = Input.touches[j].fingerId - this.touchIdOffset;
			if (num < this.finger2touchMap.Length)
			{
				this.finger2touchMap[num] = j;
			}
		}
	}

	private bool HasValidTouch(int fingerIndex)
	{
		return this.finger2touchMap[fingerIndex] != -1;
	}

	private Touch GetTouch(int fingerIndex)
	{
		int num = this.finger2touchMap[fingerIndex];
		if (num == -1)
		{
			return this.nullTouch;
		}
		return Input.touches[num];
	}

	public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
	{
		down = false;
		position = Vector2.zero;
		if (this.HasValidTouch(fingerIndex))
		{
			Touch touch = this.GetTouch(fingerIndex);
			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				down = false;
			}
			else
			{
				down = true;
				position = touch.position;
			}
		}
	}
}
