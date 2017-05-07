using System;
using UnityEngine;

public class SendSolider : MonoBehaviour
{
	public UILabel num;

	public UISprite icon;

	public UISprite bg;

	private void Awake()
	{
		this.num = base.transform.FindChild("level").GetComponent<UILabel>();
		this.icon = base.transform.FindChild("icon").GetComponent<UISprite>();
		this.bg = base.transform.FindChild("bg").GetComponent<UISprite>();
	}
}
