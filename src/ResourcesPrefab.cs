using System;
using UnityEngine;

public class ResourcesPrefab : MonoBehaviour
{
	public UILabel recNum;

	public UISprite resIcon;

	private void Awake()
	{
		this.recNum = base.transform.FindChild("value").GetComponent<UILabel>();
		this.resIcon = base.gameObject.GetComponent<UISprite>();
	}
}
