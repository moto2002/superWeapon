using System;
using UnityEngine;

public class ShopItemManage : MonoBehaviour
{
	public UILabel price;

	public ShopItem shop;

	public UISprite giftPicture;

	public GameObject buyBtn;

	public UILabel name_Client;

	public int type;

	public UISprite typePicture;

	public Body_Model model;

	public Body_Model backModel;

	public GameObject bottom;

	public UILabel count;

	public UISprite resType;

	public GameObject back;

	public GameObject backParent;

	public UILabel des;

	public UILabel firstTime;

	private void Awake()
	{
	}

	public void ShowUI()
	{
		this.name_Client.text = this.shop.diamonds + LanguageManage.GetTextByKey("钻石", "ResIsland");
		this.resType.spriteName = "新钻石";
		AtlasManage.SetShopAtals(this.giftPicture, this.shop.icon);
		this.type = this.shop.type;
	}

	private void Start()
	{
	}
}
