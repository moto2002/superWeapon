using System;
using UnityEngine;

public class ResRotate : MonoBehaviour
{
	private Transform trans;

	public float angle;

	public Vector3 axis;

	public float speed;

	private void Awake()
	{
		this.trans = base.transform;
	}

	private void Update()
	{
		this.trans.Rotate(this.axis, this.angle * Time.deltaTime * this.speed, Space.World);
	}
}
