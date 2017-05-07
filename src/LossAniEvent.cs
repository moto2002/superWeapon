using System;
using UnityEngine;

public class LossAniEvent : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void LossAniEnd()
	{
		CameraControl.inst.Shake(0.3f, 10f);
	}
}
