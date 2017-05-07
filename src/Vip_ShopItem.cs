using System;
using UnityEngine;

public class Vip_ShopItem : MonoBehaviour
{
	public UILabel price;

	public UILabel des;

	public UISprite icon;

	private void Start()
	{
	}

	public void InitInfo(ShopItem shop)
	{
		this.price.text = string.Format("[FFFD37]{0}{1}[-][FFFFFF]{2}{3}[-]", new object[]
		{
			shop.diamonds,
			LanguageManage.GetTextByKey("钻石", "ResIsland"),
			shop.price,
			LanguageManage.GetTextByKey("元", "Vip")
		});
		if (!string.IsNullOrEmpty(shop.extDiamonds) && int.Parse(shop.extDiamonds) > 0)
		{
			this.des.text = string.Format("[FFFFFF]{0}[-] [FFFD37]{1}{2}[-]", LanguageManage.GetTextByKey("额外赠送", "ResIsland"), shop.extDiamonds, LanguageManage.GetTextByKey("钻石", "ResIsland"));
		}
		else
		{
			this.des.text = string.Empty;
		}
		this.icon.spriteName = shop.icon;
	}
}
