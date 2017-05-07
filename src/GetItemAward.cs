using System;
using UnityEngine;

public class GetItemAward : MonoBehaviour
{
	public UILabel num;

	public UISprite icon;

	public UISprite quality;

	private void Awake()
	{
		this.num = base.transform.FindChild("num").GetComponent<UILabel>();
		this.icon = base.transform.FindChild("icon").GetComponent<UISprite>();
		this.quality = base.transform.FindChild("icon/iconBg").GetComponent<UISprite>();
	}
}
