using System;
using UnityEngine;

public class UISpriteNewAni : MonoBehaviour
{
	public UIAtlas[] UiAtals;

	public UISprite aniSprite;

	private float fRate = 0.1f;

	private float fNextTime;

	private int i;

	private int j;

	public int aniCount = 60;

	private bool bLoop;

	private void Start()
	{
	}

	private void Update()
	{
		if (Time.time > this.fNextTime && this.i < this.aniCount)
		{
			this.fNextTime = Time.time + this.fRate;
			this.aniSprite.spriteName = this.i.ToString();
			this.j = this.i / 10;
			this.aniSprite.atlas = this.UiAtals[this.j];
			this.i++;
		}
	}
}
