using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class LineCtrl : MonoBehaviour
{
	private LineRenderer line;

	private Transform tr;

	private Material material;

	private float x;

	private float y;

	private LineEffect effect;

	private void Awake()
	{
		this.tr = base.transform;
		Transform transform = this.tr.Find("Line");
		if (transform != null)
		{
			this.line = transform.GetComponent<LineRenderer>();
			this.material = this.line.renderer.material;
			this.x = this.material.mainTextureScale.x;
			this.y = this.material.mainTextureScale.y;
			this.effect = this.line.GetComponent<LineEffect>();
			return;
		}
		UnityEngine.Object.Destroy(this);
	}

	public void SetLineEffect(Vector3 from, Vector3 to, float dis, float lifeTime, float power)
	{
		if (this.line == null)
		{
			LogManage.Log("该技能没有所需的线性特效！");
		}
		else
		{
			this.line.SetPosition(0, from);
			this.line.SetPosition(1, to);
			this.material.mainTextureScale = new Vector2(dis * 0.1f, this.y);
			if (lifeTime != 0f)
			{
				base.StartCoroutine(this.CloseThisBullet(lifeTime));
			}
		}
		if (this.effect != null)
		{
			this.effect.NewUse(power, lifeTime);
		}
	}

	public void NewLineEffect()
	{
		if (this.line != null)
		{
			this.line.SetPosition(0, Vector3.zero);
			this.line.SetPosition(1, Vector3.zero);
		}
	}

	[DebuggerHidden]
	private IEnumerator CloseThisBullet(float lifeTime)
	{
		LineCtrl.<CloseThisBullet>c__Iterator9B <CloseThisBullet>c__Iterator9B = new LineCtrl.<CloseThisBullet>c__Iterator9B();
		<CloseThisBullet>c__Iterator9B.lifeTime = lifeTime;
		<CloseThisBullet>c__Iterator9B.<$>lifeTime = lifeTime;
		<CloseThisBullet>c__Iterator9B.<>f__this = this;
		return <CloseThisBullet>c__Iterator9B;
	}
}
