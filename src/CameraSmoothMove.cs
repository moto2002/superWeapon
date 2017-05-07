using System;
using UnityEngine;

public class CameraSmoothMove : MonoBehaviour
{
	public float speed = 10f;

	private Transform currentTrans;

	private bool isMoving;

	private Vector3 targetPosition;

	private Vector3 dirctNormalized;

	public static CameraSmoothMove inst;

	private Action callBack;

	public void OnDestroy()
	{
		CameraSmoothMove.inst = null;
	}

	private void Awake()
	{
		CameraSmoothMove.inst = this;
	}

	private void Start()
	{
		this.currentTrans = base.transform;
		base.enabled = false;
	}

	private void LateUpdate()
	{
		if (Vector3.Distance(this.currentTrans.position, this.targetPosition) < this.speed)
		{
			this.currentTrans.position = this.targetPosition;
			base.enabled = false;
			if (this.callBack != null)
			{
				this.callBack();
			}
		}
		else
		{
			this.currentTrans.position += this.speed * this.dirctNormalized;
		}
	}

	public void MovePosition(Vector3 target, Action _callBack = null)
	{
		this.callBack = _callBack;
		this.targetPosition = target;
		this.isMoving = true;
		this.dirctNormalized = (target - this.currentTrans.position).normalized;
		base.enabled = true;
	}
}
