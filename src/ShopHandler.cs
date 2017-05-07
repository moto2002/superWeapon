using LitJson;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
	private static Action BuyGold;

	public static JsonData json = new JsonData();

	public static void CS_ShopBuyGold(int id, Action func = null)
	{
		ShopHandler.BuyGold = func;
		CSBuyItem cSBuyItem = new CSBuyItem();
		cSBuyItem.id = id;
		ClientMgr.GetNet().SendHttp(1906, cSBuyItem, new DataHandler.OpcodeHandler(ShopHandler.OnBuyGold), null);
	}

	public static void OnBuyGold(bool isError, Opcode opcode)
	{
		if (ShopHandler.BuyGold != null)
		{
			ShopHandler.BuyGold();
		}
	}

	public static void CS_ShopBuyRMB(int id, int fromId, Action func = null)
	{
		ShopHandler.BuyGold = func;
		CSGetOrderId cSGetOrderId = new CSGetOrderId();
		ShopHandler.json["AppleProductId"] = GameSetting.BundleIdentifier + "." + id;
		ShopHandler.json["paramPrice"] = UnitConst.GetInstance().shopItem[id].price.ToString();
		ShopHandler.json["paramZoneId"] = User.GetServerName().ToString();
		ShopHandler.json["paramUserId"] = HeroInfo.GetInstance().platformId;
		ShopHandler.json["paramRoleId"] = HeroInfo.GetInstance().userId.ToString();
		ShopHandler.json["paramBillTitle"] = UnitConst.GetInstance().shopItem[id].name;
		ShopHandler.json["paramProductId"] = id.ToString();
		ShopHandler.json["paramRoleLevel"] = HeroInfo.GetInstance().playerlevel.ToString();
		ShopHandler.json["paramNoteTwo"] = ClientMgr.GetNet().http.httpSession.StrURL;
		ShopHandler.json["paramZoneName"] = HeroInfo.GetInstance().ServerID.ToString();
		ShopHandler.json["paramUserName"] = HeroInfo.GetInstance().userName;
		ShopHandler.json["paramProductDes"] = UnitConst.GetInstance().shopItem[id].desciption;
		ShopHandler.json["paramProductOrignalPrice"] = UnitConst.GetInstance().shopItem[id].diamonds;
		ShopHandler.json["paramProductCount"] = "1";
		ShopHandler.json["paramNoteOne"] = ClientMgr.GetNet().http.httpSession.StrURL;
		cSGetOrderId.goodsId = id;
		cSGetOrderId.goodsCount = 1;
		cSGetOrderId.activityId = fromId;
		cSGetOrderId.appleProductId = ShopHandler.json["AppleProductId"].ToString();
		ClientMgr.GetNet().SendHttp(2502, cSGetOrderId, new DataHandler.OpcodeHandler(ShopHandler.OnBuyRMB), null);
	}

	public static void OnBuyRMB(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			List<SCGetOrderId> list = opcode.Get<SCGetOrderId>(10101);
			if (list.Count > 0)
			{
				ShopHandler.json["paramBillNo"] = list[0].orderId;
				HDSDKInit.Pay(ShopHandler.json.ToJson());
			}
			if (ShopHandler.BuyGold != null)
			{
				ShopHandler.BuyGold();
			}
		}
		else
		{
			LogManage.LogError("请求失败");
		}
	}
}
