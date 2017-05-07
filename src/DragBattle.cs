using System;
using UnityEngine;

public class DragBattle : MonoBehaviour
{
	public Transform target;

	private Vector3 offset;

	private Bounds bounds;

	private float Maxwidth;

	private float Maxheight;

	private float Minwidth;

	private float Minheight;

	private void Awake()
	{
		UISprite component = this.target.GetComponent<UISprite>();
		if (component != null)
		{
			if (component.width > Screen.width)
			{
				this.Maxwidth = (float)(Screen.width + (component.width - Screen.width)) + 88.5f;
				this.Minwidth = (float)(-(float)(component.width - Screen.width)) - 88.5f;
			}
			else
			{
				this.Maxwidth = (float)Screen.width + 88.5f;
				this.Minwidth = 0f;
			}
			if (component.height > Screen.height)
			{
				this.Maxheight = (float)(Screen.height + (component.height - Screen.height));
				this.Minheight = (float)(-(float)(component.height - Screen.height));
			}
			else
			{
				this.Maxheight = (float)Screen.height;
				this.Minheight = 0f;
			}
		}
		else
		{
			this.Maxwidth = (float)Screen.width;
			this.Maxheight = (float)Screen.height;
			this.Minwidth = 0f;
			this.Minheight = 0f;
		}
	}

	private void OnPress(bool pressed)
	{
		if (this.target == null)
		{
			return;
		}
		if (pressed)
		{
			this.bounds = NGUIMath.CalculateRelativeWidgetBounds(this.target.transform);
			Vector3 vector = UICamera.currentCamera.WorldToScreenPoint(this.target.position);
			this.offset = new Vector3(Input.mousePosition.x - (vector.x - this.bounds.size.x / 2f), Input.mousePosition.y - (vector.y - this.bounds.size.y / 2f), 0f);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		Vector3 position = new Vector3(Input.mousePosition.x - this.offset.x, Input.mousePosition.y - this.offset.y, 0f);
		if (position.x < this.Minwidth)
		{
			position.x = this.Minwidth;
		}
		if (position.x + this.bounds.size.x > this.Maxwidth)
		{
			position.x = this.Maxwidth - this.bounds.size.x;
		}
		if (position.y < this.Minheight)
		{
			position.y = this.Minheight;
		}
		if (position.y + this.bounds.size.y > this.Maxheight)
		{
			position.y = this.Maxheight - this.bounds.size.y;
		}
		position.x += this.bounds.size.x / 2f;
		position.y += this.bounds.size.y / 2f;
		this.target.position = UICamera.currentCamera.ScreenToWorldPoint(position);
	}
}
