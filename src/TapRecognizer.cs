using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Gestures/Tap Recognizer")]
public class TapRecognizer : DiscreteGestureRecognizer<TapGesture>
{
	public int RequiredTaps = 1;

	public float MoveTolerance = 20f;

	public float MaxDuration;

	public float MaxDelayBetweenTaps = 0.5f;

	private bool IsMultiTap
	{
		get
		{
			return this.RequiredTaps > 1;
		}
	}

	private bool HasTimedOut(TapGesture gesture)
	{
		return (this.MaxDuration > 0f && gesture.ElapsedTime > this.MaxDuration) || (this.IsMultiTap && this.MaxDelayBetweenTaps > 0f && Time.time - gesture.LastTapTime > this.MaxDelayBetweenTaps);
	}

	protected override void Reset(TapGesture gesture)
	{
		gesture.Taps = 0;
		gesture.Down = false;
		gesture.WasDown = false;
		base.Reset(gesture);
	}

	protected override TapGesture MatchActiveGestureToCluster(FingerClusterManager.Cluster cluster)
	{
		if (this.IsMultiTap)
		{
			TapGesture tapGesture = this.FindClosestPendingGesture(cluster.Fingers.GetAveragePosition());
			if (tapGesture != null)
			{
				return tapGesture;
			}
		}
		return base.MatchActiveGestureToCluster(cluster);
	}

	private TapGesture FindClosestPendingGesture(Vector2 center)
	{
		TapGesture result = null;
		float num = float.PositiveInfinity;
		foreach (TapGesture current in base.Gestures)
		{
			if (current.State == GestureRecognitionState.InProgress)
			{
				if (!current.Down)
				{
					float num2 = Vector2.SqrMagnitude(center - current.Position);
					if (num2 < this.MoveTolerance * this.MoveTolerance && num2 < num)
					{
						result = current;
						num = num2;
					}
				}
			}
		}
		return result;
	}

	private GestureRecognitionState RecognizeSingleTap(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count != this.RequiredFingerCount)
		{
			if (touches.Count == 0)
			{
				return GestureRecognitionState.Ended;
			}
			return GestureRecognitionState.Failed;
		}
		else
		{
			if (this.HasTimedOut(gesture))
			{
				return GestureRecognitionState.Failed;
			}
			float num = Vector3.SqrMagnitude(touches.GetAveragePosition() - gesture.StartPosition);
			if (num >= this.MoveTolerance * this.MoveTolerance)
			{
				return GestureRecognitionState.Failed;
			}
			return GestureRecognitionState.InProgress;
		}
	}

	private GestureRecognitionState RecognizeMultiTap(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.WasDown = gesture.Down;
		gesture.Down = false;
		if (touches.Count == this.RequiredFingerCount)
		{
			gesture.Down = true;
			gesture.LastDownTime = Time.time;
		}
		else if (touches.Count == 0)
		{
			gesture.Down = false;
		}
		else if (touches.Count < this.RequiredFingerCount)
		{
			if (Time.time - gesture.LastDownTime > 0.25f)
			{
				return GestureRecognitionState.Failed;
			}
		}
		else if (!base.Young(touches))
		{
			return GestureRecognitionState.Failed;
		}
		if (this.HasTimedOut(gesture))
		{
			return GestureRecognitionState.Failed;
		}
		if (gesture.Down)
		{
			float num = Vector3.SqrMagnitude(touches.GetAveragePosition() - gesture.StartPosition);
			if (num >= this.MoveTolerance * this.MoveTolerance)
			{
				return GestureRecognitionState.Failed;
			}
		}
		if (gesture.WasDown != gesture.Down && !gesture.Down)
		{
			gesture.Taps++;
			gesture.LastTapTime = Time.time;
			if (gesture.Taps >= this.RequiredTaps)
			{
				return GestureRecognitionState.Ended;
			}
		}
		return GestureRecognitionState.InProgress;
	}

	public override string GetDefaultEventMessageName()
	{
		return "OnTap";
	}

	protected override void OnBegin(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.Position = touches.GetAveragePosition();
		gesture.StartPosition = gesture.Position;
		gesture.LastTapTime = Time.time;
	}

	protected override GestureRecognitionState OnRecognize(TapGesture gesture, FingerGestures.IFingerList touches)
	{
		return (!this.IsMultiTap) ? this.RecognizeSingleTap(gesture, touches) : this.RecognizeMultiTap(gesture, touches);
	}
}
