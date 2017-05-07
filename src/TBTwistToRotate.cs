using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Twist To Rotate")]
public class TBTwistToRotate : MonoBehaviour
{
	public enum RotationAxis
	{
		WorldX,
		WorldY,
		WorldZ,
		ObjectX,
		ObjectY,
		ObjectZ,
		CameraX,
		CameraY,
		CameraZ
	}

	public float Sensitivity = 1f;

	public TBTwistToRotate.RotationAxis Axis = TBTwistToRotate.RotationAxis.WorldY;

	public Camera ReferenceCamera;

	private void Start()
	{
		if (!this.ReferenceCamera)
		{
			this.ReferenceCamera = Camera.main;
		}
	}

	public Vector3 GetRotationAxis()
	{
		switch (this.Axis)
		{
		case TBTwistToRotate.RotationAxis.WorldX:
			return Vector3.right;
		case TBTwistToRotate.RotationAxis.WorldY:
			return Vector3.up;
		case TBTwistToRotate.RotationAxis.WorldZ:
			return Vector3.forward;
		case TBTwistToRotate.RotationAxis.ObjectX:
			return base.transform.right;
		case TBTwistToRotate.RotationAxis.ObjectY:
			return base.transform.up;
		case TBTwistToRotate.RotationAxis.ObjectZ:
			return base.transform.forward;
		case TBTwistToRotate.RotationAxis.CameraX:
			return this.ReferenceCamera.transform.right;
		case TBTwistToRotate.RotationAxis.CameraY:
			return this.ReferenceCamera.transform.up;
		case TBTwistToRotate.RotationAxis.CameraZ:
			return this.ReferenceCamera.transform.forward;
		default:
			Debug.LogWarning("Unhandled rotation axis: " + this.Axis);
			return Vector3.forward;
		}
	}

	private void OnTwist(TwistGesture gesture)
	{
		Quaternion lhs = Quaternion.AngleAxis(this.Sensitivity * gesture.DeltaRotation, this.GetRotationAxis());
		base.transform.rotation = lhs * base.transform.rotation;
	}
}
