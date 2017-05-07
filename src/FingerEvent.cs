using System;
using UnityEngine;

public class FingerEvent
{
	private FingerEventDetector detector;

	private FingerGestures.Finger finger;

	private string name = string.Empty;

	private GameObject selection;

	private RaycastHit hit = default(RaycastHit);

	public string Name
	{
		get
		{
			return this.name;
		}
		internal set
		{
			this.name = value;
		}
	}

	public FingerEventDetector Detector
	{
		get
		{
			return this.detector;
		}
		internal set
		{
			this.detector = value;
		}
	}

	public FingerGestures.Finger Finger
	{
		get
		{
			return this.finger;
		}
		internal set
		{
			this.finger = value;
		}
	}

	public virtual Vector2 Position
	{
		get
		{
			return this.finger.Position;
		}
		internal set
		{
			throw new NotSupportedException("Setting position is not supported on " + base.GetType());
		}
	}

	public GameObject Selection
	{
		get
		{
			return this.selection;
		}
		internal set
		{
			this.selection = value;
		}
	}

	public RaycastHit Hit
	{
		get
		{
			return this.hit;
		}
		internal set
		{
			this.hit = value;
		}
	}
}
