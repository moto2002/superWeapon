using System;
using UnityEngine;

public class FingerMotionEvent : FingerEvent
{
	private FingerMotionPhase phase;

	private Vector2 position = Vector2.zero;

	internal float StartTime;

	public override Vector2 Position
	{
		get
		{
			return this.position;
		}
		internal set
		{
			this.position = value;
		}
	}

	public FingerMotionPhase Phase
	{
		get
		{
			return this.phase;
		}
		internal set
		{
			this.phase = value;
		}
	}

	public float ElapsedTime
	{
		get
		{
			return Mathf.Max(0f, Time.time - this.StartTime);
		}
	}
}
