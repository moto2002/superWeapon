using System;
using UnityEngine;

public class UISimpleItem : MonoBehaviour
{
	public UISprite icon;

	public UISprite background;

	public UISprite quality;

	public UILabel name_Client;

	public UILabel num;

	public int exp;

	public UILabel Coin;

	public int CoinNum;

	public int itemId;

	public UILabel info;

	private void Awake()
	{
		if (base.transform.FindChild("icon/Sprite") != null)
		{
			this.icon = base.transform.FindChild("icon/Sprite").GetComponent<UISprite>();
		}
		else if (base.transform.FindChild("icon") != null)
		{
			this.icon = base.transform.FindChild("icon").GetComponent<UISprite>();
		}
		if (base.transform.FindChild("bg") != null)
		{
			this.background = base.transform.FindChild("bg").GetComponent<UISprite>();
		}
		if (base.transform.FindChild("icon/iconBg") != null)
		{
			this.quality = base.transform.FindChild("icon/iconBg").GetComponent<UISprite>();
		}
		else
		{
			this.quality = base.transform.FindChild("icon").GetComponent<UISprite>();
		}
		if (base.transform.FindChild("level") != null)
		{
			this.num = base.transform.FindChild("level").GetComponent<UILabel>();
		}
		else if (base.transform.FindChild("count") != null)
		{
			this.num = base.transform.FindChild("count").GetComponent<UILabel>();
		}
		else if (base.transform.FindChild("num") != null)
		{
			this.num = base.transform.FindChild("num").GetComponent<UILabel>();
		}
		else
		{
			this.num = base.transform.FindChild("icon/num").GetComponent<UILabel>();
		}
		if (base.transform.FindChild("Label") != null)
		{
			this.name_Client = base.transform.FindChild("Label").GetComponent<UILabel>();
		}
		else if (base.transform.FindChild("icon/Label") != null)
		{
			this.name_Client = base.transform.FindChild("icon/Label").GetComponent<UILabel>();
		}
		else if (base.transform.FindChild("level") != null)
		{
			this.name_Client = base.transform.FindChild("level").GetComponent<UILabel>();
		}
		else
		{
			this.name_Client = base.transform.FindChild("num").GetComponent<UILabel>();
		}
		if (base.transform.FindChild("CoinSet/Label") != null)
		{
			this.Coin = base.transform.FindChild("CoinSet/Label").GetComponent<UILabel>();
		}
		if (base.transform.FindChild("Label") != null)
		{
			this.info = base.transform.FindChild("Label").GetComponent<UILabel>();
		}
	}
}
