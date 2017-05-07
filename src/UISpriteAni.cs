using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(UISprite))]
public class UISpriteAni : MonoBehaviour
{
	private int width;

	private int height;

	private UISprite sprite;

	private void Awake()
	{
		this.sprite = base.GetComponent<UISprite>();
		this.width = this.sprite.width;
		this.height = this.sprite.height;
	}

	private void OnMouseDown()
	{
		this.ChangeWidth(0.9f);
	}

	private void OnMouseUp()
	{
		this.ChangeWidth(1f);
	}

	private void ChangeWidth(float size)
	{
		this.sprite.width = (int)((float)this.width * size);
		this.sprite.height = (int)((float)this.height * size);
	}

	private void OnPress(bool isPress)
	{
		if (isPress)
		{
			this.ChangeWidth(0.8f);
		}
		else
		{
			this.ChangeWidth(1f);
		}
	}
}
