using System;
using UnityEngine;

public class TransRotate : MonoBehaviour
{
	public enum RotateXYZ
	{
		X,
		Y,
		Z
	}

	public Transform Bg;

	public TransRotate.RotateXYZ xyz;

	public float FuDu;

	private void Start()
	{
	}

	private void Update()
	{
		switch (this.xyz)
		{
		case TransRotate.RotateXYZ.X:
			if (this.Bg)
			{
				base.transform.RotateAround(this.Bg.transform.position, this.Bg.transform.right, this.FuDu * Time.deltaTime);
			}
			break;
		case TransRotate.RotateXYZ.Y:
			if (this.Bg)
			{
				base.transform.RotateAround(this.Bg.transform.position, this.Bg.transform.up, this.FuDu * Time.deltaTime);
			}
			break;
		case TransRotate.RotateXYZ.Z:
			if (this.Bg)
			{
				base.transform.RotateAround(this.Bg.transform.position, this.Bg.transform.forward, this.FuDu * Time.deltaTime);
			}
			break;
		}
	}
}
