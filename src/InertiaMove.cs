using System;
using UnityEngine;

public class InertiaMove : MonoBehaviour
{
	private float speed;

	private void Update()
	{
		this.speed -= 10f * Time.deltaTime;
		if (this.speed < 0f)
		{
			base.enabled = false;
		}
		DragMgr.inst.ConversionFormulas(this.speed);
	}

	public void MoveCamera(float _speed)
	{
		this.speed = _speed * 0.01f;
		base.enabled = true;
	}
}
