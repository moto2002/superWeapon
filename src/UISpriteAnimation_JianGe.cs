using System;
using UnityEngine;

public class UISpriteAnimation_JianGe : UISpriteAnimation
{
	[HideInInspector, SerializeField]
	protected float JianGeTime;

	private Transform tr;

	private float endTime;

	private void Awake()
	{
		this.tr = base.transform;
	}

	protected override void Update()
	{
		if (Time.time < this.startTime + this.DelayTime)
		{
			this.tr.localScale = Vector3.zero;
			return;
		}
		if (this.tr.localScale == Vector3.zero)
		{
			this.tr.localScale = Vector3.one;
		}
		if (Time.time < this.endTime + this.JianGeTime && this.endTime > 0f)
		{
			this.tr.localScale = Vector3.zero;
			return;
		}
		if (this.tr.localScale == Vector3.zero)
		{
			this.tr.localScale = Vector3.one;
		}
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.mFPS > 0f)
		{
			this.mDelta += RealTime.deltaTime;
			float num = 1f / (float)this.mFPS;
			if (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.mIndex >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
					this.mActive = base.loop;
					this.endTime = Time.time;
					return;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
					if (this.mSnap)
					{
						this.mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}
}
