using System;
using UnityEngine;

public class UVAnimation : MonoBehaviour
{
	public float scrollSpeed_X = 0.5f;

	public float scrollSpeed_Y = 0.5f;

	private float offsetX;

	private float offsetY;

	private Material material;

	private void Start()
	{
		this.material = GameTools.GetMaterial(base.renderer);
	}

	private void Update()
	{
		this.offsetX = Time.time * this.scrollSpeed_X;
		this.offsetY = Time.time * this.scrollSpeed_Y;
		this.material.mainTextureOffset = new Vector2(this.offsetX, this.offsetY);
	}
}
