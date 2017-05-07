using System;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	public int updateOrder;

	public float speed = 10f;

	public bool ignoreTimeScale;

	private Transform mTrans;

	private Quaternion mRelative;

	private Quaternion mAbsolute;

	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	private void Update()
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			float num = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, num * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}
}
