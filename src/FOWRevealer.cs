using System;
using UnityEngine;

[AddComponentMenu("Fog of War/Revealer")]
public class FOWRevealer : MonoBehaviour
{
	private Transform mTrans;

	public Vector2 range = new Vector2(2f, 30f);

	public FOWSystem.LOSChecks lineOfSightCheck;

	public bool isActive = true;

	private FOWSystem.Revealer mRevealer;

	private void Awake()
	{
		this.mTrans = base.transform;
		this.mRevealer = FOWSystem.CreateRevealer();
	}

	private void OnDisable()
	{
		this.mRevealer.isActive = false;
	}

	private void OnDestroy()
	{
		FOWSystem.DeleteRevealer(this.mRevealer);
		this.mRevealer = null;
	}

	private void LateUpdate()
	{
		if (this.isActive)
		{
			if (this.lineOfSightCheck != FOWSystem.LOSChecks.OnlyOnce)
			{
				this.mRevealer.cachedBuffer = null;
			}
			this.mRevealer.pos = this.mTrans.position;
			this.mRevealer.inner = this.range.x;
			this.mRevealer.outer = this.range.y;
			this.mRevealer.los = this.lineOfSightCheck;
			this.mRevealer.isActive = true;
		}
		else
		{
			this.mRevealer.isActive = false;
			this.mRevealer.cachedBuffer = null;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (this.lineOfSightCheck != FOWSystem.LOSChecks.None && this.range.x > 0f)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(base.transform.position, this.range.x);
		}
		Gizmos.color = Color.grey;
		Gizmos.DrawWireSphere(base.transform.position, this.range.y);
	}

	public void Rebuild()
	{
		this.mRevealer.cachedBuffer = null;
	}
}
