using System;

public class FingerUpEvent : FingerEvent
{
	private float timeHeldDown;

	public float TimeHeldDown
	{
		get
		{
			return this.timeHeldDown;
		}
		internal set
		{
			this.timeHeldDown = value;
		}
	}
}
