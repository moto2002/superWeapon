using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Camera mainCamera;

	private Transform tr;

	private void Start()
	{
		this.mainCamera = Camera.main;
		this.tr = base.transform;
	}

	private void Update()
	{
		if (this.mainCamera != null)
		{
			this.tr.LookAt(this.mainCamera.transform);
		}
	}
}
