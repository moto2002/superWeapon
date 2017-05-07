using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Finger Events/Finger Hover Detector")]
public class FingerHoverDetector : FingerEventDetector<FingerHoverEvent>
{
	public string MessageName = "OnFingerHover";

	public event FingerEventDetector<FingerHoverEvent>.FingerEventHandler OnFingerHover
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnFingerHover = (FingerEventDetector<FingerHoverEvent>.FingerEventHandler)Delegate.Combine(this.OnFingerHover, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnFingerHover = (FingerEventDetector<FingerHoverEvent>.FingerEventHandler)Delegate.Remove(this.OnFingerHover, value);
		}
	}

	protected override void Start()
	{
		base.Start();
		if (!this.Raycaster)
		{
			Debug.LogWarning("FingerHoverDetector component on " + base.name + " has no Raycaster set.");
		}
	}

	private bool FireEvent(FingerHoverEvent e, FingerHoverPhase phase)
	{
		e.Name = this.MessageName;
		e.Phase = phase;
		if (this.OnFingerHover != null)
		{
			this.OnFingerHover(e);
		}
		base.TrySendMessage(e);
		return true;
	}

	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		FingerHoverEvent @event = base.GetEvent(finger);
		GameObject previousSelection = @event.PreviousSelection;
		GameObject gameObject = (!finger.IsDown) ? null : base.PickObject(finger.Position);
		if (gameObject != previousSelection)
		{
			if (previousSelection)
			{
				this.FireEvent(@event, FingerHoverPhase.Exit);
			}
			if (gameObject)
			{
				@event.Selection = gameObject;
				@event.Hit = base.LastHit;
				this.FireEvent(@event, FingerHoverPhase.Enter);
			}
		}
		@event.PreviousSelection = gameObject;
	}
}
