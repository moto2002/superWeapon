using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Gestures/Drag Recognizer")]
public class DragRecognizer : ContinuousGestureRecognizer<DragGesture>
{
	public float MoveTolerance = 5f;

	public bool ApplySameDirectionConstraint;

	public override string GetDefaultEventMessageName()
	{
		return "OnDrag";
	}

	protected override GameObject GetDefaultSelectionForSendMessage(DragGesture gesture)
	{
		return gesture.StartSelection;
	}

	protected override bool CanBegin(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		return base.CanBegin(gesture, touches) && touches.GetAverageDistanceFromStart() >= this.MoveTolerance && touches.AllMoving() && (this.RequiredFingerCount < 2 || !this.ApplySameDirectionConstraint || touches.MovingInSameDirection(0.35f));
	}

	protected override void OnBegin(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.Position = touches.GetAveragePosition();
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.DeltaMove = gesture.Position - gesture.StartPosition;
		gesture.LastDelta = Vector2.zero;
		gesture.LastPos = gesture.Position;
	}

	protected override GestureRecognitionState OnRecognize(DragGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count < this.RequiredFingerCount)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			if (this.RequiredFingerCount >= 2 && this.ApplySameDirectionConstraint && touches.AllMoving() && !touches.MovingInSameDirection(0.35f))
			{
				return GestureRecognitionState.Failed;
			}
			gesture.Position = touches.GetAveragePosition();
			gesture.LastDelta = gesture.DeltaMove;
			gesture.DeltaMove = gesture.Position - gesture.LastPos;
			if (gesture.DeltaMove.sqrMagnitude > 0f || gesture.LastDelta.sqrMagnitude > 0f)
			{
				gesture.LastPos = gesture.Position;
			}
			base.RaiseEvent(gesture);
			return GestureRecognitionState.InProgress;
		}
	}
}
