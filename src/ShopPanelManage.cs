using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelManage : FuncUIPanel
{
	public static ShopPanelManage shop;

	public List<GameObject> itemList = new List<GameObject>();

	public LangeuageLabel title;

	public UITable shopTable;

	public GameObject shopItemPre;

	public GameObject closeShop;

	public ResLabel resCoin;

	public UILabel rmbLabel;

	public void OnDestroy()
	{
		ShopPanelManage.shop = null;
	}

	public override void Awake()
	{
		ShopPanelManage.shop = this;
		this.ShowShopItem();
		EventManager.Instance.AddEvent(EventManager.EventType.ShangChengClick, new EventManager.VoidDelegate(this.ClickBg));
		EventManager.Instance.AddEvent(EventManager.EventType.ShopPanel_ClosePanel, new EventManager.VoidDelegate(this.CloseThisPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.ShopPanel_BuyGiftPackege, new EventManager.VoidDelegate(this.BuyGift));
	}

	public void ClickBg(GameObject ga)
	{
	}

	public void CloseThisPanel(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("ShopPanel");
	}

	private void destTabel()
	{
		GameObject gameObject = this.shopTable.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = base.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	public void ShowShopItem()
	{
		this.destTabel();
		this.shopTable.DestoryChildren(true);
		foreach (KeyValuePair<int, ShopItem> current in UnitConst.GetInstance().shopItem)
		{
			if (current.Value.IsUIShow)
			{
				GameObject gameObject = NGUITools.AddChild(this.shopTable.gameObject, this.shopItemPre);
				gameObject.transform.parent = this.shopTable.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = Vector3.one;
				ShopItemManage component = gameObject.transform.GetComponent<ShopItemManage>();
				component.model = PoolManage.Ins.GetEffectByName("goumaizuanshi_star", null);
				component.model.ga.AddComponent<RenderQueueEdit>();
				component.model.tr.parent = component.back.transform;
				component.model.tr.localPosition = new Vector3(0f, 72.8f, 0f);
				component.backModel = PoolManage.Ins.GetEffectByName("goumaizuanshi_glow", null);
				component.backModel.ga.AddComponent<RenderQueueEdit>();
				component.backModel.tr.parent = component.backParent.transform;
				component.backModel.tr.localPosition = new Vector3(0f, 59f, 0f);
				if (current.Value.priceType == 1)
				{
					component.typePicture.spriteName = string.Empty;
					component.price.text = "￥" + current.Value.price;
					component.price.GetComponent<UIWidget>().pivot = UIWidget.Pivot.Center;
					component.price.transform.localPosition = new Vector3(-4f, -2f, 0f);
					component.typePicture.GetComponent<UISprite>().width = 42;
					component.typePicture.GetComponent<UISprite>().height = 42;
					component.des.gameObject.SetActive(false);
					component.firstTime.gameObject.SetActive(true);
					if (int.Parse(current.Value.extDiamonds) > 0)
					{
						component.firstTime.text = LanguageManage.GetTextByKey("额外赠送", "ResIsland") + current.Value.extDiamonds + LanguageManage.GetTextByKey("钻石", "ResIsland");
					}
					else
					{
						component.firstTime.text = string.Empty;
					}
				}
				if (current.Value.priceType == 0)
				{
					component.typePicture.spriteName = "新钻石";
					component.price.transform.localPosition = new Vector3(-12f, -2.12f, 0f);
					component.price.GetComponent<UIWidget>().pivot = UIWidget.Pivot.Left;
					component.price.text = current.Value.price.ToString();
					component.typePicture.GetComponent<UISprite>().width = 48;
					component.typePicture.GetComponent<UISprite>().height = 40;
					component.firstTime.gameObject.SetActive(false);
					component.des.gameObject.SetActive(true);
					component.des.text = LanguageManage.GetTextByKey(current.Value.desciption, "ResIsland");
				}
				component.buyBtn.name = current.Value.id.ToString();
				component.shop = current.Value;
				gameObject.name = current.Value.id.ToString();
				this.itemList.Add(gameObject);
				component.ShowUI();
			}
		}
		this.shopTable.Reposition();
	}

	public void BuyGift(GameObject go)
	{
		ShopItemManage componentInParent = go.GetComponentInParent<ShopItemManage>();
		if (componentInParent == null)
		{
			return;
		}
		ShopHandler.CS_ShopBuyRMB(int.Parse(go.name), 0, null);
	}

	public static void ShowHelp_NoRMB(Action buyRmb, Action noBuyRmb)
	{
		MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("消费提醒", "others"), LanguageManage.GetTextByKey("钻石不足请前往充值", "others"), LanguageManage.GetTextByKey("前往充值", "others"), delegate
		{
			if (buyRmb != null)
			{
				buyRmb();
			}
			FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
		}, LanguageManage.GetTextByKey("取消", "others"), noBuyRmb);
	}
}
