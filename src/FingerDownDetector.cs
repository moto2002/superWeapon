using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Finger Events/Finger Down Detector")]
public class FingerDownDetector : FingerEventDetector<FingerDownEvent>
{
	public string MessageName = "OnFingerDown";

	public event FingerEventDetector<FingerDownEvent>.FingerEventHandler OnFingerDown
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnFingerDown = (FingerEventDetector<FingerDownEvent>.FingerEventHandler)Delegate.Combine(this.OnFingerDown, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnFingerDown = (FingerEventDetector<FingerDownEvent>.FingerEventHandler)Delegate.Remove(this.OnFingerDown, value);
		}
	}

	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		if (finger.IsDown && !finger.WasDown)
		{
			FingerDownEvent @event = this.GetEvent(finger.Index);
			@event.Name = this.MessageName;
			base.UpdateSelection(@event);
			if (this.OnFingerDown != null)
			{
				this.OnFingerDown(@event);
			}
			base.TrySendMessage(@event);
		}
	}
}
