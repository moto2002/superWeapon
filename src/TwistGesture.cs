using System;

public class TwistGesture : ContinuousGesture
{
	private float deltaRotation;

	private float totalRotation;

	public float DeltaRotation
	{
		get
		{
			return this.deltaRotation;
		}
		internal set
		{
			this.deltaRotation = value;
		}
	}

	public float TotalRotation
	{
		get
		{
			return this.totalRotation;
		}
		internal set
		{
			this.totalRotation = value;
		}
	}
}
