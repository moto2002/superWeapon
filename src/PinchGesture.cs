using System;

public class PinchGesture : ContinuousGesture
{
	private float delta;

	private float gap;

	public float Delta
	{
		get
		{
			return this.delta;
		}
		internal set
		{
			this.delta = value;
		}
	}

	public float Gap
	{
		get
		{
			return this.gap;
		}
		internal set
		{
			this.gap = value;
		}
	}
}
