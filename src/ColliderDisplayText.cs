using System;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Collider - Display Text")]
public class ColliderDisplayText : MonoBehaviour
{
	public GameObject prefab;

	public Transform target;

	private HUDText mText;

	private bool mHover;

	private void Start()
	{
		if (HUDRoot.go == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		GameObject gameObject = NGUITools.AddChild(HUDRoot.go, this.prefab);
		this.mText = gameObject.GetComponentInChildren<HUDText>();
		gameObject.AddComponent<UIFollowTarget>().target = this.target;
	}

	private void OnHover(bool isOver)
	{
		if (this.mText != null && isOver && !this.mHover)
		{
			this.mHover = true;
			this.mText.Add("Left-click, right-click", Color.cyan, 2f);
		}
		else if (!isOver)
		{
			this.mHover = false;
		}
	}

	private void OnClick()
	{
		if (this.mText != null)
		{
			if (UICamera.currentTouchID == -1)
			{
				this.mText.Add(-10f + UnityEngine.Random.value * -10f, Color.green, 0f);
			}
			else if (UICamera.currentTouchID == -2)
			{
				this.mText.Add(10f + UnityEngine.Random.value * 10f, Color.green, 0f);
			}
		}
	}
}
