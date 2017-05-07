using System;
using UnityEngine;

public class GetResourceAward : MonoBehaviour
{
	public UILabel num;

	public UISprite icon;

	private void Awake()
	{
		this.num = base.transform.FindChild("num").GetComponent<UILabel>();
		this.icon = base.transform.FindChild("icon").GetComponent<UISprite>();
	}
}
