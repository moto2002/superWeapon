using System;
using UnityEngine;

public class LineEffect : MonoBehaviour
{
	public float startWith = 2f;

	public float endWith = 5f;

	private float sWith;

	private float eWith;

	private float curWith;

	public float wSpeed = 1f;

	public float startTransparent = 1f;

	public float endTransparent;

	public float aSpeed = 5f;

	private float aDSpeed = 5f;

	private float curA;

	private LineRenderer line;

	private Transform tr;

	private Color color = Color.white;

	private void Awake()
	{
		this.tr = base.transform;
		this.line = this.tr.GetComponent<LineRenderer>();
	}

	public void NewUse(float power, float lifeTime)
	{
		this.sWith = this.startWith * power;
		this.eWith = this.endWith * power;
		this.color.a = this.startTransparent;
		this.aDSpeed = this.aSpeed / lifeTime;
		this.curWith = this.sWith;
	}

	private void Update()
	{
		if (this.curWith <= Mathf.Max(this.sWith, this.eWith) && this.curWith >= Mathf.Min(this.sWith, this.eWith))
		{
			this.curWith += (this.eWith - this.sWith) * this.wSpeed * Time.deltaTime;
		}
		if (this.color.a <= Mathf.Max(this.startTransparent, this.endTransparent) && this.curWith >= Mathf.Min(this.startTransparent, this.endTransparent))
		{
			this.color.a = this.color.a + (this.endTransparent - this.startTransparent) * this.aDSpeed * Time.deltaTime;
		}
		this.SetLineEffect();
	}

	private void SetLineEffect()
	{
		this.line.SetWidth(this.curWith, this.curWith);
		this.line.SetColors(this.color, this.color);
	}
}
