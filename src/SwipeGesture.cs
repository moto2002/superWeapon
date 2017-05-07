using System;
using UnityEngine;

[Serializable]
public class SwipeGesture : DiscreteGesture
{
	private Vector2 move = Vector2.zero;

	private float velocity;

	private FingerGestures.SwipeDirection direction;

	internal int MoveCounter;

	internal float Deviation;

	public Vector2 Move
	{
		get
		{
			return this.move;
		}
		internal set
		{
			this.move = value;
		}
	}

	public float Velocity
	{
		get
		{
			return this.velocity;
		}
		internal set
		{
			this.velocity = value;
		}
	}

	public FingerGestures.SwipeDirection Direction
	{
		get
		{
			return this.direction;
		}
		internal set
		{
			this.direction = value;
		}
	}
}
