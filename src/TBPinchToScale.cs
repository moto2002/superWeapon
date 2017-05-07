using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Pinch To Scale")]
public class TBPinchToScale : MonoBehaviour
{
	public Vector3 scaleWeights = Vector3.one;

	public float minScaleAmount = 0.5f;

	public float maxScaleAmount = 2f;

	public float sensitivity = 1f;

	public float smoothingSpeed = 12f;

	private float idealScaleAmount = 1f;

	private float scaleAmount = 1f;

	private Vector3 baseScale = Vector3.one;

	public float ScaleAmount
	{
		get
		{
			return this.scaleAmount;
		}
		set
		{
			value = Mathf.Clamp(value, this.minScaleAmount, this.maxScaleAmount);
			if (value != this.scaleAmount)
			{
				this.scaleAmount = value;
				Vector3 localScale = this.scaleAmount * this.baseScale;
				localScale.x *= this.scaleWeights.x;
				localScale.y *= this.scaleWeights.y;
				localScale.z *= this.scaleWeights.z;
				base.transform.localScale = localScale;
			}
		}
	}

	public float IdealScaleAmount
	{
		get
		{
			return this.idealScaleAmount;
		}
		set
		{
			this.idealScaleAmount = Mathf.Clamp(value, this.minScaleAmount, this.maxScaleAmount);
		}
	}

	private void Start()
	{
		this.baseScale = base.transform.localScale;
		this.IdealScaleAmount = this.ScaleAmount;
	}

	private void Update()
	{
		if (this.smoothingSpeed > 0f)
		{
			this.ScaleAmount = Mathf.Lerp(this.ScaleAmount, this.IdealScaleAmount, Time.deltaTime * this.smoothingSpeed);
		}
		else
		{
			this.ScaleAmount = this.IdealScaleAmount;
		}
	}

	private void OnPinch(PinchGesture gesture)
	{
		this.IdealScaleAmount += 0.01f * this.sensitivity * gesture.Delta;
	}
}
