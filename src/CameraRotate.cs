using System;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public Camera cam;

	public Transform cameraPos;

	public Vector3 centerPos;

	private float pinchTurnRatio = 1.57079637f;

	private float minTurnAngle;

	public float turnAngleDelta;

	public float turnAngle;

	private Vector2 nowTouch1;

	private Vector2 nowTouch2;

	private Vector2 oldTouch1;

	private Vector2 oldTouch2;

	private float nowK;

	private float oldK;

	public float time;

	public static bool ismove;

	private float ang;

	private float theta;

	private float shazhe;

	private bool isFist = true;

	private float r;

	private Vector3 campos;

	public PinchRecognizer pinch;

	public TwistRecognizer twist;

	public static bool Rotating;

	public static bool Pinching;

	private void Awake()
	{
		this.cameraPos = base.transform;
		if (this.pinch)
		{
			this.pinch.OnGesture += new GestureRecognizer<PinchGesture>.GestureEventHandler(this.pinch_OnGesture);
		}
	}

	public void LateUpdate()
	{
	}

	private void RotateCamera(float angle)
	{
		if (CameraRotate.Rotating)
		{
			this.campos = base.transform.position;
			float x = this.campos.x;
			float z = this.campos.z;
			float x2 = HUDTextTool.inst.centerPos.x;
			float z2 = HUDTextTool.inst.centerPos.z;
			float num = x - x2;
			float num2 = z - z2;
			if (this.isFist)
			{
				this.isFist = false;
				this.r = Mathf.Sqrt(Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f));
				this.theta = Mathf.Atan2(num2, num);
				this.shazhe = this.theta + angle;
			}
			else
			{
				this.shazhe += angle;
			}
			if (this.shazhe < 0f)
			{
				this.shazhe = 0f;
			}
			if (this.shazhe > 1.57079637f)
			{
				this.shazhe = 1.57079637f;
			}
			float x3 = this.r * Mathf.Cos(this.shazhe) + x2;
			float z3 = this.r * Mathf.Sin(this.shazhe) + z2;
			this.cameraPos.LookAt(HUDTextTool.inst.centerPos);
			this.cameraPos.position = new Vector3(x3, this.cameraPos.position.y, z3);
		}
	}

	private float Angle(Vector2 pos1, Vector2 pos2)
	{
		Vector2 vector = pos2 - pos1;
		Vector2 vector2 = new Vector2(1f, 0f);
		float num = Vector2.Angle(vector, vector2);
		if (Vector3.Cross(vector, vector2).z > 0f)
		{
			num = 360f - num;
		}
		return num;
	}

	private void twist_OnGesture(TwistGesture gesture)
	{
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			CameraRotate.Rotating = true;
			HUDTextTool.inst.GetCenterPos();
		}
		else if (gesture.Phase == ContinuousGesturePhase.Updated)
		{
			if (CameraRotate.Rotating)
			{
				LogManage.Log("DeltaRotation" + gesture.DeltaRotation);
				this.RotateCamera(gesture.DeltaRotation * 0.0174532924f);
			}
		}
		else if (CameraRotate.Rotating)
		{
			CameraRotate.Rotating = false;
		}
	}

	private float ClampAngle(float angle, float min, float max)
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

	private void pinch_OnGesture(PinchGesture gesture)
	{
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			CameraRotate.Pinching = true;
		}
		else if (gesture.Phase == ContinuousGesturePhase.Updated)
		{
			if (CameraRotate.Pinching && !CameraRotate.Rotating)
			{
				CameraControl.inst.MoveCamera(gesture.Delta * 20f);
			}
		}
		else if (CameraRotate.Pinching)
		{
			CameraRotate.Pinching = false;
		}
	}
}
