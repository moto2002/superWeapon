using System;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
	public float scrollSpeed_X;

	public float scrollSpeed_Y = 0.3f;

	private Material material;

	private void Awake()
	{
		this.material = base.renderer.material;
	}

	private void Update()
	{
		float x = Time.time * this.scrollSpeed_X;
		float y = Time.time * this.scrollSpeed_Y;
		this.material.mainTextureOffset = new Vector2(x, y);
	}
}
