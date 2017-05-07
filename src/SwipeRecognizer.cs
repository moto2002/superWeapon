using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Gestures/Swipe Recognizer")]
public class SwipeRecognizer : DiscreteGestureRecognizer<SwipeGesture>
{
	private FingerGestures.SwipeDirection ValidDirections = FingerGestures.SwipeDirection.All;

	public float MinDistance = 1f;

	public float MaxDistance;

	public float MinVelocity = 100f;

	public float MaxDeviation = 25f;

	public override string GetDefaultEventMessageName()
	{
		return "OnSwipe";
	}

	protected override bool CanBegin(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		return base.CanBegin(gesture, touches) && touches.GetAverageDistanceFromStart() >= 0.5f && touches.AllMoving() && touches.MovingInSameDirection(0.35f);
	}

	protected override void OnBegin(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.Position = touches.GetAveragePosition();
		gesture.Move = Vector3.zero;
		gesture.MoveCounter = 0;
		gesture.Deviation = 0f;
		gesture.Direction = FingerGestures.SwipeDirection.None;
	}

	protected override GestureRecognitionState OnRecognize(SwipeGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count > this.RequiredFingerCount)
			{
				return GestureRecognitionState.Failed;
			}
			if (FingerGestures.GetAdjustedPixelDistance(gesture.Move.magnitude) < Mathf.Max(1f, this.MinDistance))
			{
				return GestureRecognitionState.Failed;
			}
			gesture.Direction = FingerGestures.GetSwipeDirection(gesture.Move);
			return GestureRecognitionState.Ended;
		}
		else
		{
			Vector2 move = gesture.Move;
			gesture.Position = touches.GetAveragePosition();
			gesture.Move = gesture.Position - gesture.StartPosition;
			float adjustedPixelDistance = FingerGestures.GetAdjustedPixelDistance(gesture.Move.magnitude);
			if (this.MaxDistance > this.MinDistance && adjustedPixelDistance > this.MaxDistance)
			{
				return GestureRecognitionState.Failed;
			}
			if (gesture.ElapsedTime > 0f)
			{
				gesture.Velocity = adjustedPixelDistance / gesture.ElapsedTime;
			}
			else
			{
				gesture.Velocity = 0f;
			}
			if (gesture.MoveCounter > 2 && gesture.Velocity < this.MinVelocity)
			{
				return GestureRecognitionState.Failed;
			}
			if (adjustedPixelDistance > 50f && gesture.MoveCounter > 2)
			{
				gesture.Deviation += 57.29578f * FingerGestures.SignedAngle(move, gesture.Move);
				if (Mathf.Abs(gesture.Deviation) > this.MaxDeviation)
				{
					return GestureRecognitionState.Failed;
				}
			}
			gesture.MoveCounter++;
			return GestureRecognitionState.InProgress;
		}
	}

	public bool IsValidDirection(FingerGestures.SwipeDirection dir)
	{
		return dir != FingerGestures.SwipeDirection.None && (this.ValidDirections & dir) == dir;
	}
}
