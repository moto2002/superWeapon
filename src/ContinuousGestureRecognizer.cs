using System;

public abstract class ContinuousGestureRecognizer<T> : GestureRecognizer<T> where T : ContinuousGesture, new()
{
	protected override void Reset(T gesture)
	{
		base.Reset(gesture);
	}

	protected override void OnStateChanged(Gesture sender)
	{
		base.OnStateChanged(sender);
		T gesture = (T)((object)sender);
		switch (gesture.State)
		{
		case GestureRecognitionState.Started:
			base.RaiseEvent(gesture);
			break;
		case GestureRecognitionState.Failed:
			if (gesture.PreviousState != GestureRecognitionState.Ready)
			{
				base.RaiseEvent(gesture);
			}
			break;
		case GestureRecognitionState.Ended:
			base.RaiseEvent(gesture);
			break;
		}
	}
}
