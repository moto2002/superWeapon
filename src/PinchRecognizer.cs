using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Gestures/Pinch Recognizer")]
public class PinchRecognizer : ContinuousGestureRecognizer<PinchGesture>
{
	public float MinDOT = -0.7f;

	public float MinDistance = 5f;

	public float DeltaScale = 1f;

	public override int RequiredFingerCount
	{
		get
		{
			return 2;
		}
		set
		{
			Debug.LogWarning("Not Supported");
		}
	}

	public override bool SupportFingerClustering
	{
		get
		{
			return false;
		}
	}

	public override string GetDefaultEventMessageName()
	{
		return "OnPinch";
	}

	protected override GameObject GetDefaultSelectionForSendMessage(PinchGesture gesture)
	{
		return gesture.StartSelection;
	}

	public override GestureResetMode GetDefaultResetMode()
	{
		return GestureResetMode.NextFrame;
	}

	protected override bool CanBegin(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		if (!base.CanBegin(gesture, touches))
		{
			return false;
		}
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		if (!FingerGestures.AllFingersMoving(new FingerGestures.Finger[]
		{
			finger,
			finger2
		}))
		{
			return false;
		}
		if (!this.FingersMovedInOppositeDirections(finger, finger2))
		{
			return false;
		}
		float num = Vector2.SqrMagnitude(finger.StartPosition - finger2.StartPosition);
		float num2 = Vector2.SqrMagnitude(finger.Position - finger2.Position);
		return FingerGestures.GetAdjustedPixelDistance(Mathf.Abs(num - num2)) >= this.MinDistance * this.MinDistance;
	}

	protected override void OnBegin(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		FingerGestures.Finger finger = touches[0];
		FingerGestures.Finger finger2 = touches[1];
		gesture.StartPosition = 0.5f * (finger.StartPosition + finger2.StartPosition);
		gesture.Position = 0.5f * (finger.Position + finger2.Position);
		gesture.Gap = Vector2.Distance(finger.StartPosition, finger2.StartPosition);
		float num = Vector2.Distance(finger.Position, finger2.Position);
		gesture.Delta = FingerGestures.GetAdjustedPixelDistance(this.DeltaScale * (num - gesture.Gap));
		gesture.Gap = num;
	}

	protected override GestureRecognitionState OnRecognize(PinchGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			gesture.Delta = 0f;
			if (touches.Count < this.RequiredFingerCount)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			FingerGestures.Finger finger = touches[0];
			FingerGestures.Finger finger2 = touches[1];
			gesture.Position = 0.5f * (finger.Position + finger2.Position);
			if (!FingerGestures.AllFingersMoving(new FingerGestures.Finger[]
			{
				finger,
				finger2
			}))
			{
				return GestureRecognitionState.InProgress;
			}
			float num = Vector2.Distance(finger.Position, finger2.Position);
			float adjustedPixelDistance = FingerGestures.GetAdjustedPixelDistance(this.DeltaScale * (num - gesture.Gap));
			gesture.Gap = num;
			if (Mathf.Abs(adjustedPixelDistance) > 0.001f)
			{
				if (!this.FingersMovedInOppositeDirections(finger, finger2))
				{
					return GestureRecognitionState.InProgress;
				}
				gesture.Delta = adjustedPixelDistance;
				base.RaiseEvent(gesture);
			}
			return GestureRecognitionState.InProgress;
		}
	}

	private bool FingersMovedInOppositeDirections(FingerGestures.Finger finger0, FingerGestures.Finger finger1)
	{
		return FingerGestures.FingersMovedInOppositeDirections(finger0, finger1, this.MinDOT);
	}
}
