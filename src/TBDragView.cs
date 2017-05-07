using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Camera/Drag View"), RequireComponent(typeof(DragRecognizer))]
public class TBDragView : MonoBehaviour
{
	public bool allowUserInput = true;

	public float sensitivity = 8f;

	public float dragAcceleration = 40f;

	public float dragDeceleration = 10f;

	public bool reverseControls;

	public float minPitchAngle = -60f;

	public float maxPitchAngle = 60f;

	public float idealRotationSmoothingSpeed = 7f;

	private Transform cachedTransform;

	private Vector2 angularVelocity = Vector2.zero;

	private Quaternion idealRotation;

	private bool useAngularVelocity;

	private DragGesture dragGesture;

	public bool Dragging
	{
		get
		{
			return this.dragGesture != null;
		}
	}

	public Quaternion IdealRotation
	{
		get
		{
			return this.idealRotation;
		}
		set
		{
			this.idealRotation = value;
			this.useAngularVelocity = false;
		}
	}

	private void Awake()
	{
		this.cachedTransform = base.transform;
	}

	private void Start()
	{
		this.IdealRotation = this.cachedTransform.rotation;
		if (!base.GetComponent<DragRecognizer>())
		{
			Debug.LogWarning("No drag recognizer found on " + base.name + ". Disabling TBDragView.");
			base.enabled = false;
		}
	}

	private void OnDrag(DragGesture gesture)
	{
		if (gesture.Phase != ContinuousGesturePhase.Ended)
		{
			this.dragGesture = gesture;
		}
		else
		{
			this.dragGesture = null;
		}
	}

	private void Update()
	{
		if (this.Dragging && this.allowUserInput)
		{
			this.useAngularVelocity = true;
		}
		if (this.useAngularVelocity)
		{
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			Vector2 to = Vector2.zero;
			float num = this.dragDeceleration;
			if (this.Dragging)
			{
				to = this.sensitivity * this.dragGesture.DeltaMove;
				num = this.dragAcceleration;
			}
			this.angularVelocity = Vector2.Lerp(this.angularVelocity, to, Time.deltaTime * num);
			Vector2 a = Time.deltaTime * this.angularVelocity;
			if (this.reverseControls)
			{
				a = -a;
			}
			localEulerAngles.x = Mathf.Clamp(TBDragView.NormalizePitch(localEulerAngles.x + a.y), this.minPitchAngle, this.maxPitchAngle);
			localEulerAngles.y -= a.x;
			base.transform.localEulerAngles = localEulerAngles;
		}
		else if (this.idealRotationSmoothingSpeed > 0f)
		{
			this.cachedTransform.rotation = Quaternion.Slerp(this.cachedTransform.rotation, this.IdealRotation, Time.deltaTime * this.idealRotationSmoothingSpeed);
		}
		else
		{
			this.cachedTransform.rotation = this.idealRotation;
		}
	}

	private static float NormalizePitch(float angle)
	{
		if (angle > 180f)
		{
			angle -= 360f;
		}
		return angle;
	}

	public void LookAt(Vector3 pos)
	{
		this.IdealRotation = Quaternion.LookRotation(pos - this.cachedTransform.position);
	}
}
