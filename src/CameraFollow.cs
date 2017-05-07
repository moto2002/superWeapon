using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform _target;

	private bool follow;

	private Transform tr;

	public float dis = 30f;

	private void Start()
	{
		this.tr = base.transform;
	}

	private void Update()
	{
		if (this.follow && this._target != null)
		{
			this.tr.LookAt(this._target);
			this.tr.position = this._target.position - this._target.forward * this.dis;
		}
	}

	public void Follow(Transform target)
	{
		this._target = target;
		this.follow = true;
	}
}
