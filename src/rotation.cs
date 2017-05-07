using System;
using UnityEngine;

public class rotation : MonoBehaviour
{
	private int speed = 60;

	private Transform tr;

	private float y = 150f;

	private void Awake()
	{
		this.tr = base.transform;
	}

	private void Update()
	{
		this.y += (float)this.speed * Time.deltaTime;
		this.tr.localRotation = Quaternion.Euler(0f, this.y, 0f);
	}
}
