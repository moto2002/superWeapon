using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 axis;

	public float rate;

	private void Update()
	{
		base.transform.Rotate(this.axis * Time.deltaTime * this.rate);
	}
}
