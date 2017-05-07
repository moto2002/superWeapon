using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class DieBall : IMonoBehaviour
{
	private Body_Model dieRes;

	[HideInInspector]
	public string effectRes;

	private float lifeTime;

	public ParticleSystem Particle;

	public bool IsAutoDes = true;

	public float LifeTime
	{
		get
		{
			return this.lifeTime;
		}
		set
		{
			this.lifeTime = value;
			this.SetLifeTime();
		}
	}

	public void OnDisable()
	{
		if (this.lifeTime > 0f)
		{
			UnityEngine.Object.Destroy(this.ga);
		}
	}

	private void CreatBody()
	{
		this.lifeTime = 0f;
		this.dieRes = PoolManage.Ins.GetEffectByName(this.effectRes, this.tr);
		if (this.dieRes != null)
		{
			EffectLifeTime componentInChildren = base.GetComponentInChildren<EffectLifeTime>();
			this.Particle = base.GetComponentInChildren<ParticleSystem>();
			if (this.effectRes == "loading_dian")
			{
			}
			if (this.effectRes == "xinshou_zhiyin")
			{
				Transform[] componentsInChildren = this.dieRes.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.layer = 8;
				}
			}
			if (componentInChildren != null)
			{
				this.lifeTime = componentInChildren.effectLifeTime;
			}
			else if (this.Particle != null && !this.Particle.loop)
			{
				this.lifeTime = this.Particle.duration;
			}
		}
		else
		{
			LogManage.Log("没有这个特效文件" + this.effectRes);
		}
	}

	private void Start()
	{
	}

	public void SetInfo()
	{
		this.CreatBody();
		this.SetLifeTime();
	}

	private void SetLifeTime()
	{
		if (this.lifeTime > 0f)
		{
			base.StopAllCoroutines();
			if (this.ga.activeInHierarchy)
			{
				base.StartCoroutine(this.DestoryInPoolIE(this.lifeTime));
			}
		}
		else
		{
			base.StopAllCoroutines();
		}
	}

	[DebuggerHidden]
	public IEnumerator DestoryInPoolIE(float _lifeTime)
	{
		DieBall.<DestoryInPoolIE>c__Iterator2B <DestoryInPoolIE>c__Iterator2B = new DieBall.<DestoryInPoolIE>c__Iterator2B();
		<DestoryInPoolIE>c__Iterator2B._lifeTime = _lifeTime;
		<DestoryInPoolIE>c__Iterator2B.<$>_lifeTime = _lifeTime;
		<DestoryInPoolIE>c__Iterator2B.<>f__this = this;
		return <DestoryInPoolIE>c__Iterator2B;
	}

	public void DesInPool()
	{
		if (this.dieRes)
		{
			this.dieRes.DesInsInPool();
		}
		UnityEngine.Object.Destroy(this.ga);
	}
}
