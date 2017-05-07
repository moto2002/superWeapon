using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Camera/Orbit")]
public class TBOrbit : MonoBehaviour
{
	public enum PanMode
	{
		Disabled,
		OneFinger,
		TwoFingers
	}

	public Transform target;

	public float initialDistance = 10f;

	public float minDistance = 1f;

	public float maxDistance = 20f;

	public float yawSensitivity = 80f;

	public float pitchSensitivity = 80f;

	public bool clampYawAngle;

	public float minYaw = -75f;

	public float maxYaw = 75f;

	public bool clampPitchAngle = true;

	public float minPitch = -20f;

	public float maxPitch = 80f;

	public bool allowPinchZoom = true;

	public float pinchZoomSensitivity = 2f;

	public bool smoothMotion = true;

	public float smoothZoomSpeed = 3f;

	public float smoothOrbitSpeed = 4f;

	public bool allowPanning;

	public bool invertPanningDirections;

	public float panningSensitivity = 1f;

	public Transform panningPlane;

	public bool smoothPanning = true;

	public float smoothPanningSpeed = 8f;

	private float distance = 10f;

	private float yaw;

	private float pitch;

	private float idealDistance;

	private float idealYaw;

	private float idealPitch;

	private Vector3 idealPanOffset = Vector3.zero;

	private Vector3 panOffset = Vector3.zero;

	private float nextDragTime;

	public float Distance
	{
		get
		{
			return this.distance;
		}
	}

	public float IdealDistance
	{
		get
		{
			return this.idealDistance;
		}
		set
		{
			this.idealDistance = Mathf.Clamp(value, this.minDistance, this.maxDistance);
		}
	}

	public float Yaw
	{
		get
		{
			return this.yaw;
		}
	}

	public float IdealYaw
	{
		get
		{
			return this.idealYaw;
		}
		set
		{
			this.idealYaw = ((!this.clampYawAngle) ? value : TBOrbit.ClampAngle(value, this.minYaw, this.maxYaw));
		}
	}

	public float Pitch
	{
		get
		{
			return this.pitch;
		}
	}

	public float IdealPitch
	{
		get
		{
			return this.idealPitch;
		}
		set
		{
			this.idealPitch = ((!this.clampPitchAngle) ? value : TBOrbit.ClampAngle(value, this.minPitch, this.maxPitch));
		}
	}

	public Vector3 IdealPanOffset
	{
		get
		{
			return this.idealPanOffset;
		}
		set
		{
			this.idealPanOffset = value;
		}
	}

	public Vector3 PanOffset
	{
		get
		{
			return this.panOffset;
		}
	}

	private void InstallGestureRecognizers()
	{
		List<GestureRecognizer> list = new List<GestureRecognizer>(base.GetComponents<GestureRecognizer>());
		DragRecognizer dragRecognizer = list.Find((GestureRecognizer r) => r.EventMessageName == "OnDrag") as DragRecognizer;
		DragRecognizer dragRecognizer2 = list.Find((GestureRecognizer r) => r.EventMessageName == "OnTwoFingerDrag") as DragRecognizer;
		PinchRecognizer exists = list.Find((GestureRecognizer r) => r.EventMessageName == "OnPinch") as PinchRecognizer;
		if (!dragRecognizer)
		{
			dragRecognizer = base.gameObject.AddComponent<DragRecognizer>();
			dragRecognizer.RequiredFingerCount = 1;
			dragRecognizer.IsExclusive = true;
			dragRecognizer.MaxSimultaneousGestures = 1;
			dragRecognizer.SendMessageToSelection = GestureRecognizer.SelectionType.None;
		}
		if (!exists)
		{
			exists = base.gameObject.AddComponent<PinchRecognizer>();
		}
		if (!dragRecognizer2)
		{
			dragRecognizer2 = base.gameObject.AddComponent<DragRecognizer>();
			dragRecognizer2.RequiredFingerCount = 2;
			dragRecognizer2.IsExclusive = true;
			dragRecognizer2.MaxSimultaneousGestures = 1;
			dragRecognizer2.ApplySameDirectionConstraint = false;
			dragRecognizer2.EventMessageName = "OnTwoFingerDrag";
		}
	}

	private void Start()
	{
		this.InstallGestureRecognizers();
		if (!this.panningPlane)
		{
			this.panningPlane = base.transform;
		}
		Vector3 eulerAngles = base.transform.eulerAngles;
		float num = this.initialDistance;
		this.IdealDistance = num;
		this.distance = num;
		num = eulerAngles.y;
		this.IdealYaw = num;
		this.yaw = num;
		num = eulerAngles.x;
		this.IdealPitch = num;
		this.pitch = num;
		if (base.rigidbody)
		{
			base.rigidbody.freezeRotation = true;
		}
		this.Apply();
	}

	private void OnDrag(DragGesture gesture)
	{
		if (Time.time < this.nextDragTime)
		{
			return;
		}
		if (this.target)
		{
			this.IdealYaw += gesture.DeltaMove.x * this.yawSensitivity * 0.02f;
			this.IdealPitch -= gesture.DeltaMove.y * this.pitchSensitivity * 0.02f;
		}
	}

	private void OnPinch(PinchGesture gesture)
	{
		if (this.allowPinchZoom)
		{
			this.IdealDistance -= gesture.Delta * this.pinchZoomSensitivity;
			this.nextDragTime = Time.time + 0.25f;
		}
	}

	private void OnTwoFingerDrag(DragGesture gesture)
	{
		if (this.allowPanning)
		{
			Vector3 b = -0.02f * this.panningSensitivity * (this.panningPlane.right * gesture.DeltaMove.x + this.panningPlane.up * gesture.DeltaMove.y);
			if (this.invertPanningDirections)
			{
				this.IdealPanOffset -= b;
			}
			else
			{
				this.IdealPanOffset += b;
			}
			this.nextDragTime = Time.time + 0.25f;
		}
	}

	private void Apply()
	{
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.IdealDistance, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.Lerp(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.LerpAngle(this.pitch, this.IdealPitch, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance;
			this.yaw = this.IdealYaw;
			this.pitch = this.IdealPitch;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		base.transform.rotation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		base.transform.position = this.target.position + this.panOffset - this.distance * base.transform.forward;
	}

	private void LateUpdate()
	{
		this.Apply();
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public void ResetPanning()
	{
		this.IdealPanOffset = Vector3.zero;
	}
}
