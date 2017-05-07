using System;
using UnityEngine;

public class AttachCamera : MonoBehaviour
{
	private Transform myTransform;

	public Transform target;

	public Vector3 offset = new Vector3(0f, 5f, -5f);

	private void Start()
	{
		this.myTransform = base.transform;
	}

	private void FixedUpdate()
	{
		if (this.target != null)
		{
			this.myTransform.position = this.target.position + this.offset;
			this.myTransform.LookAt(this.target.position, Vector3.up);
		}
	}
}
