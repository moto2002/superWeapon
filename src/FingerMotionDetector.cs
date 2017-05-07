using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Finger Events/Finger Motion Detector")]
public class FingerMotionDetector : FingerEventDetector<FingerMotionEvent>
{
	private enum EventType
	{
		Move,
		Stationary
	}

	public string MoveMessageName = "OnFingerMove";

	public string StationaryMessageName = "OnFingerStationary";

	public bool TrackMove = true;

	public bool TrackStationary = true;

	public event FingerEventDetector<FingerMotionEvent>.FingerEventHandler OnFingerMove
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnFingerMove = (FingerEventDetector<FingerMotionEvent>.FingerEventHandler)Delegate.Combine(this.OnFingerMove, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnFingerMove = (FingerEventDetector<FingerMotionEvent>.FingerEventHandler)Delegate.Remove(this.OnFingerMove, value);
		}
	}

	public event FingerEventDetector<FingerMotionEvent>.FingerEventHandler OnFingerStationary
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnFingerStationary = (FingerEventDetector<FingerMotionEvent>.FingerEventHandler)Delegate.Combine(this.OnFingerStationary, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnFingerStationary = (FingerEventDetector<FingerMotionEvent>.FingerEventHandler)Delegate.Remove(this.OnFingerStationary, value);
		}
	}

	private bool FireEvent(FingerMotionEvent e, FingerMotionDetector.EventType eventType, FingerMotionPhase phase, Vector2 position, bool updateSelection)
	{
		if ((!this.TrackMove && eventType == FingerMotionDetector.EventType.Move) || (!this.TrackStationary && eventType == FingerMotionDetector.EventType.Stationary))
		{
			return false;
		}
		e.Phase = phase;
		e.Position = position;
		if (e.Phase == FingerMotionPhase.Started)
		{
			e.StartTime = Time.time;
		}
		if (updateSelection)
		{
			base.UpdateSelection(e);
		}
		if (eventType == FingerMotionDetector.EventType.Move)
		{
			e.Name = this.MoveMessageName;
			if (this.OnFingerMove != null)
			{
				this.OnFingerMove(e);
			}
			base.TrySendMessage(e);
		}
		else
		{
			if (eventType != FingerMotionDetector.EventType.Stationary)
			{
				Debug.LogWarning("Unhandled FingerMotionDetector event type: " + eventType);
				return false;
			}
			e.Name = this.StationaryMessageName;
			if (this.OnFingerStationary != null)
			{
				this.OnFingerStationary(e);
			}
			base.TrySendMessage(e);
		}
		return true;
	}

	protected override void ProcessFinger(FingerGestures.Finger finger)
	{
		FingerMotionEvent @event = base.GetEvent(finger);
		bool flag = false;
		if (finger.Phase != finger.PreviousPhase)
		{
			FingerGestures.FingerPhase fingerPhase = finger.PreviousPhase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Ended, finger.PreviousPosition, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Ended, finger.Position, !flag);
			}
			fingerPhase = finger.Phase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Started, finger.Position, !flag);
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Updated, finger.Position, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Started, finger.PreviousPosition, !flag);
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Updated, finger.Position, !flag);
			}
		}
		else
		{
			FingerGestures.FingerPhase fingerPhase = finger.Phase;
			if (fingerPhase != FingerGestures.FingerPhase.Moving)
			{
				if (fingerPhase == FingerGestures.FingerPhase.Stationary)
				{
					flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Stationary, FingerMotionPhase.Updated, finger.Position, !flag);
				}
			}
			else
			{
				flag |= this.FireEvent(@event, FingerMotionDetector.EventType.Move, FingerMotionPhase.Updated, finger.Position, !flag);
			}
		}
	}
}
