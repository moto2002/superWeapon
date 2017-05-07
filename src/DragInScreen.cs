using System;
using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class DragInScreen : MonoBehaviour
{
	private UIWidget widget;

	private Transform tr;

	public void Awake()
	{
		this.tr = base.transform;
		this.widget = base.GetComponent<UIWidget>();
	}

	private void OnDrag(Vector2 vc)
	{
		float x = (float)(Screen.width + this.widget.width) * 0.5f;
		float y = (float)(Screen.height + this.widget.height) * 0.5f;
		this.tr.localPosition = Input.mousePosition - new Vector3(x, y);
	}
}
