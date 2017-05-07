using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class TextureAni : MonoBehaviour
{
	private UITexture texture;

	private Texture2D[] allTexture;

	private int nowFram;

	private int mFrameCount;

	private float fps = 30f;

	private float time;

	private void Start()
	{
		this.texture = base.GetComponent<UITexture>();
		this.allTexture = Resources.LoadAll<Texture2D>("Texture/SettmentPanel/Win/");
		from a in this.allTexture
		orderby a.name
		select a;
	}

	private void Update()
	{
		this.time += Time.deltaTime;
		if ((double)this.time >= 1.0 / (double)this.fps)
		{
			this.time = 0f;
			if (this.nowFram < this.allTexture.Length)
			{
				this.texture.mainTexture = this.allTexture[this.nowFram];
			}
			this.nowFram++;
		}
	}
}
