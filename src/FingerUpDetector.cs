using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Finger Events/Finger Up Detector")]
public class FingerUpDetector : FingerEventDetector<FingerUpEvent>
{
	public string MessageName = "OnFingerUp";

	public event FingerEventDetector<FingerUpEvent>.FingerEventHandler OnFingerUp
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnFingerUp = (FingerEventDetector<FingerUpEvent>.FingerEventHandler)Delegate.Combine(this.OnFingerUp, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnFingerUp = (FingerEventDetector<FingerUpEvent>.FingerEventHandler)Delegate.Remove(this.OnFingerUp, value);
		}
	}

	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		if (!finger.IsDown && finger.WasDown)
		{
			FingerUpEvent @event = base.GetEvent(finger);
			@event.Name = this.MessageName;
			@event.TimeHeldDown = Mathf.Max(0f, Time.time - finger.StarTime);
			base.UpdateSelection(@event);
			if (this.OnFingerUp != null)
			{
				this.OnFingerUp(@event);
			}
			base.TrySendMessage(@event);
		}
	}
}
