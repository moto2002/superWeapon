using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Fill Amount"), RequireComponent(typeof(UIBasicSprite))]
public class TweenFillAmount : UITweener
{
	[Range(0f, 1f)]
	public float from = 1f;

	[Range(0f, 1f)]
	public float to = 1f;

	private bool mCached;

	private UIBasicSprite mBasic;

	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return (!(this.mBasic != null)) ? 1f : this.mBasic.fillAmount;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mBasic != null)
			{
				this.mBasic.fillAmount = value;
			}
		}
	}

	private void Cache()
	{
		this.mCached = true;
		this.mBasic = base.GetComponent<UIBasicSprite>();
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}
}
