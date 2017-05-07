using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmsDealerPanel : FuncUIPanel
{
	public enum ShopType
	{
		军火商 = 1,
		商店
	}

	public enum ArmsDealerBtnType
	{
		Refesh,
		Buy
	}

	public ArmsDealerPanel.ShopType NowShopType = ArmsDealerPanel.ShopType.军火商;

	private static int refreshNum;

	public static ArmsDealerPanel ins;

	public GameObject closeArms;

	public GameObject ConfirmPanel;

	public LangeuageLabel refreshBtn;

	public LangeuageLabel title;

	public UILabel refreshUIlable;

	public UILabel refreshTimes;

	public UILabel times;

	public UILabel refreshCostUIlabel;

	public UILabel playerDiaCount;

	public UILabel refreshMoney;

	public UIScrollView itemSV;

	public UIGrid itemSVGrid;

	public UITable itemTable;

	public GameObject itemParentPrefab;

	public List<GameObject> itemParentPrefabList = new List<GameObject>();

	public GameObject itemPrefab;

	public GameObject refreshBtnClick;

	public GameObject item2;

	public GameObject TypeBtnA;

	public GameObject TypeBtnB;

	public GameObject PageBtn_Left;

	public GameObject PageBtn_Right;

	public int PageNum;

	public int NowPageNum;

	private bool isRefresh = true;

	public void OnDestroy()
	{
		ArmsDealerPanel.ins = null;
	}

	public override void Awake()
	{
		this.ConfirmPanel = base.transform.FindChild(" ConfirmPanel").gameObject;
		this.ConfirmPanel.AddComponent<ConfilrmPanelManage>();
		this.closeArms = base.transform.FindChild("ExitBtn").gameObject;
		this.closeArms.AddComponent<ButtonClick>();
		ButtonClick component = this.closeArms.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.ArmsDearlPanel_CloseArms;
		this.title = base.transform.FindChild("CenterAnchor/Title/Label").GetComponent<LangeuageLabel>();
		this.title.text = LanguageManage.GetTextByKey("军火商", "others");
		this.refreshBtnClick = base.transform.FindChild("CenterAnchor/RefeshBtn").gameObject;
		this.refreshBtnClick.AddComponent<ButtonClick>();
		ButtonClick component2 = this.refreshBtnClick.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.ArmsDearPanel_RereshBtnClick;
		this.times = base.transform.FindChild("CenterAnchor/times").GetComponent<UILabel>();
		this.refreshTimes = base.transform.FindChild("CenterAnchor/Label").GetComponent<UILabel>();
		this.refreshTimes.text = LanguageManage.GetTextByKey("下次刷新倒计时", "others") + ":";
		this.refreshUIlable = base.transform.FindChild("CenterAnchor/RefeshBtn/LabelMain").GetComponent<UILabel>();
		this.refreshUIlable.text = LanguageManage.GetTextByKey("刷新次数", "others") + ":";
		this.refreshCostUIlabel = base.transform.FindChild("CenterAnchor/RefeshBtn/Label").GetComponent<UILabel>();
		this.playerDiaCount = base.transform.FindChild("CenterAnchor/PlayerDia/PlayerDiaCount").GetComponent<UILabel>();
		this.refreshBtn = base.transform.FindChild("CenterAnchor/RefeshBtn/Sprite/Label2").GetComponent<LangeuageLabel>();
		this.refreshBtn.text = LanguageManage.GetTextByKey("刷新", "others");
		this.refreshMoney = base.transform.FindChild("CenterAnchor/RefeshBtn/Sprite/Label").GetComponent<UILabel>();
		this.refreshMoney.text = UnitConst.GetInstance().DesighConfigDic[13].value;
		ArmsDealerPanel.ins = this;
		this.TypeBtnA = base.transform.FindChild("CenterAnchor/TypeBtnA").gameObject.AddComponent<ButtonClick>().ga;
		this.TypeBtnB = base.transform.FindChild("CenterAnchor/TypeBtnB").gameObject.AddComponent<ButtonClick>().ga;
		this.PageBtn_Left = base.transform.FindChild("CenterAnchor/PageBtn_Left").gameObject.AddComponent<ButtonClick>().ga;
		this.PageBtn_Right = base.transform.FindChild("CenterAnchor/PageBtn_Right").gameObject.AddComponent<ButtonClick>().ga;
		this.TypeBtnA.GetComponent<ButtonClick>().eventType = EventManager.EventType.Shop_TypeBtnA;
		this.TypeBtnB.GetComponent<ButtonClick>().eventType = EventManager.EventType.Shop_TypeBtnB;
		this.PageBtn_Left.GetComponent<ButtonClick>().eventType = EventManager.EventType.Shop_PageBtn_Left;
		this.PageBtn_Right.GetComponent<ButtonClick>().eventType = EventManager.EventType.Shop_PageBtn_Right;
		this.ConfirmPanel.SetActive(false);
		EventManager.Instance.AddEvent(EventManager.EventType.ArmsDearlPanel_CloseArms, new EventManager.VoidDelegate(this.CloseArms));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmsDearPanel_RereshBtnClick, new EventManager.VoidDelegate(this.ArmsReshBtn));
		EventManager.Instance.AddEvent(EventManager.EventType.Shop_TypeBtnA, new EventManager.VoidDelegate(this.TypeBtnA_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.Shop_TypeBtnB, new EventManager.VoidDelegate(this.TypeBtnB_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.Shop_PageBtn_Left, new EventManager.VoidDelegate(this.PageBtn_Left_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.Shop_PageBtn_Right, new EventManager.VoidDelegate(this.PageBtn_Right_CallBack));
	}

	private void TypeBtnA_CallBack(GameObject ga)
	{
		this.itemSV.GetComponent<UIPanel>().clipping = UIDrawCall.Clipping.None;
		if (this.NowShopType != ArmsDealerPanel.ShopType.军火商)
		{
			this.ChangeShopType(ArmsDealerPanel.ShopType.军火商);
		}
	}

	private void TypeBtnB_CallBack(GameObject ga)
	{
		this.itemSV.GetComponent<UIPanel>().clipping = UIDrawCall.Clipping.SoftClip;
		if (this.NowShopType != ArmsDealerPanel.ShopType.商店)
		{
			this.ChangeShopType(ArmsDealerPanel.ShopType.商店);
		}
	}

	private void PageBtn_Left_CallBack(GameObject ga)
	{
		this.SetPage(this.NowPageNum - 1);
	}

	private void PageBtn_Right_CallBack(GameObject ga)
	{
		this.SetPage(this.NowPageNum + 1);
	}

	private void SetPage(int num)
	{
		if (this.NowPageNum == num)
		{
			return;
		}
		this.NowPageNum = num;
		this.PageBtn_Left.SetActive(false);
		this.PageBtn_Right.SetActive(false);
		this.itemSVGrid.transform.DOLocalMoveX(this.itemSVGrid.cellWidth * (float)(1 - this.NowPageNum), 0.2f, false).OnComplete(new TweenCallback(this.PageBtnShow));
	}

	private void ChangeShopType(ArmsDealerPanel.ShopType shopType)
	{
		this.NowShopType = shopType;
		if (this.NowShopType == ArmsDealerPanel.ShopType.军火商)
		{
			this.TypeBtnA.GetComponent<UISprite>().spriteName = "页签1";
			this.TypeBtnB.GetComponent<UISprite>().spriteName = "页签";
			this.TypeBtnA.GetComponentInChildren<UILabel>().color = Color.white;
			this.TypeBtnA.GetComponentInChildren<UILabel>().effectStyle = UILabel.Effect.Outline;
			this.TypeBtnA.GetComponentInChildren<UILabel>().effectColor = new Color(0.619607866f, 0.5137255f, 1f, 1f);
			this.TypeBtnB.GetComponentInChildren<UILabel>().color = new Color(0.5803922f, 0.5803922f, 0.5803922f, 1f);
			this.TypeBtnB.GetComponentInChildren<UILabel>().effectStyle = UILabel.Effect.Shadow;
			this.TypeBtnB.GetComponentInChildren<UILabel>().effectColor = Color.black;
		}
		else if (this.NowShopType == ArmsDealerPanel.ShopType.商店)
		{
			this.TypeBtnA.GetComponent<UISprite>().spriteName = "页签";
			this.TypeBtnB.GetComponent<UISprite>().spriteName = "页签1";
			this.TypeBtnB.GetComponentInChildren<UILabel>().color = Color.white;
			this.TypeBtnB.GetComponentInChildren<UILabel>().effectStyle = UILabel.Effect.Outline;
			this.TypeBtnB.GetComponentInChildren<UILabel>().effectColor = new Color(0.619607866f, 0.5137255f, 1f, 1f);
			this.TypeBtnA.GetComponentInChildren<UILabel>().color = new Color(0.5803922f, 0.5803922f, 0.5803922f, 1f);
			this.TypeBtnA.GetComponentInChildren<UILabel>().effectStyle = UILabel.Effect.Shadow;
			this.TypeBtnA.GetComponentInChildren<UILabel>().effectColor = Color.black;
			if (this.NowShopType == ArmsDealerPanel.ShopType.商店)
			{
				foreach (KeyValuePair<int, ArmsDealer> current in UnitConst.GetInstance().ArmsDealerConst)
				{
					if (current.Value.type == 1 && !ArmsDealerPanelData.ArmsDealers_Mall.Contains(current.Value))
					{
						ArmsDealerPanelData.ArmsDealers_Mall.Add(current.Value);
					}
				}
			}
		}
		this.title.text = ((this.NowShopType != ArmsDealerPanel.ShopType.军火商) ? LanguageManage.GetTextByKey("商城", "others") : LanguageManage.GetTextByKey("军火商", "others"));
		bool active = this.NowShopType == ArmsDealerPanel.ShopType.军火商;
		this.refreshUIlable.gameObject.SetActive(active);
		this.refreshTimes.gameObject.SetActive(active);
		this.times.gameObject.SetActive(active);
		this.refreshCostUIlabel.gameObject.SetActive(active);
		this.refreshBtnClick.gameObject.SetActive(active);
		this.RefreshDataToUI();
	}

	private void PageBtnShow()
	{
		if (this.NowPageNum == 1)
		{
			this.PageBtn_Left.gameObject.SetActive(false);
		}
		else
		{
			this.PageBtn_Left.gameObject.SetActive(true);
		}
		if (this.NowPageNum == this.PageNum)
		{
			this.PageBtn_Right.gameObject.SetActive(false);
		}
		else
		{
			this.PageBtn_Right.gameObject.SetActive(true);
		}
	}

	public override void OnEnable()
	{
		base.OnEnable();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		this.RefreshDataToUI();
		this.TypeBtnA_CallBack(null);
		this.SetPage(1);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10057)
		{
			this.RefreshDataToUI();
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void CloseArms(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("ArmsDealerPanel");
	}

	private void RefreshDataToUI()
	{
		this.itemSV.ResetPosition();
		this.NowPageNum = 1;
		this.itemSVGrid.ClearChild();
		this.itemSVGrid.transform.localPosition = Vector3.zero;
		this.itemParentPrefabList.Clear();
		this.playerDiaCount.text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
		this.refreshCostUIlabel.text = string.Format("{0}/{1}", int.Parse(UnitConst.GetInstance().DesighConfigDic[14].value) - ArmsDealerPanelData.useMoneyRefreshTimes, UnitConst.GetInstance().DesighConfigDic[14].value);
		if (int.Parse(UnitConst.GetInstance().DesighConfigDic[14].value) - ArmsDealerPanelData.useMoneyRefreshTimes == 0)
		{
			this.refreshBtnClick.GetComponent<UISprite>().spriteName = "hui";
			this.refreshBtnClick.GetComponent<UISprite>().ShaderToGray();
			this.refreshBtnClick.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			this.refreshBtnClick.GetComponent<UISprite>().spriteName = "blue";
			this.refreshBtnClick.GetComponent<UISprite>().ShaderToNormal();
			this.refreshBtnClick.GetComponent<BoxCollider>().enabled = true;
		}
		if (this.NowShopType == ArmsDealerPanel.ShopType.商店)
		{
			ArmsDealerPanelData.ArmsDealers = (from a in ArmsDealerPanelData.ArmsDealers_Mall
			where a.show == 1
			orderby a.sort descending
			select a).ToList<ArmsDealer>();
		}
		else
		{
			ArmsDealerPanelData.ArmsDealers = (from a in ArmsDealerPanelData.ArmsDealers_Net
			where a.show == 1
			orderby a.sort descending
			select a).ToList<ArmsDealer>();
		}
		this.PageNum = (int)((float)ArmsDealerPanelData.ArmsDealers.Count / 6f - 0.1f) + 1;
		for (int i = 0; i < this.PageNum; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.itemParentPrefab) as GameObject;
			gameObject.transform.parent = this.itemSVGrid.transform;
			gameObject.transform.localPosition = new Vector3(this.itemSVGrid.cellWidth * (float)i, 0f, 0f);
			gameObject.transform.localScale = Vector3.one;
			this.itemParentPrefabList.Add(gameObject);
		}
		for (int j = 0; j < ArmsDealerPanelData.ArmsDealers.Count; j++)
		{
			GameObject gameObject2 = null;
			for (int k = 0; k < this.PageNum; k++)
			{
				if (j < (k + 1) * 6)
				{
					gameObject2 = this.itemParentPrefabList[k];
					break;
				}
			}
			this.itemTable = gameObject2.GetComponentInChildren<UITable>();
			this.itemTable.ItemModel = this.itemPrefab;
			GameObject gameObject3 = this.itemTable.CreateChildren(ArmsDealerPanelData.ArmsDealers[j].id.ToString(), true);
			gameObject3.GetComponent<ArmsDelaerItem>().curArmsDealer = ArmsDealerPanelData.ArmsDealers[j];
			gameObject3.GetComponent<ArmsDelaerItem>().Display();
			ArmsDelaerItem component = gameObject3.GetComponent<ArmsDelaerItem>();
			if (UnitConst.GetInstance().ArmsDealerConst[ArmsDealerPanelData.ArmsDealers[j].id].ItemSeller.Count != 0)
			{
				foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().ArmsDealerConst[ArmsDealerPanelData.ArmsDealers[j].id].ItemSeller)
				{
					AtlasManage.SetUiItemAtlas(component.ItemIcon2, UnitConst.GetInstance().ItemConst[current.Key].IconId);
					component.ItemIcon2.gameObject.AddComponent<ItemTipsShow2>();
					if (j % 4 == 0)
					{
						component.ItemIcon2.GetComponent<ItemTipsShow2>().JianTouPostion = 3;
					}
					else
					{
						component.ItemIcon2.GetComponent<ItemTipsShow2>().JianTouPostion = 4;
					}
					component.ItemIcon2.GetComponent<ItemTipsShow2>().Type = 1;
					component.ItemIcon2.GetComponent<ItemTipsShow2>().Index = current.Key;
					component.tittleName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[current.Key].Name, "item");
					component.backSprite.GetComponent<UISprite>().spriteName = this.SetQuSprite((int)UnitConst.GetInstance().ItemConst[current.Key].Quality);
				}
			}
			else if (UnitConst.GetInstance().ArmsDealerConst[ArmsDealerPanelData.ArmsDealers[j].id].SkillSeller.Count != 0)
			{
				foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().ArmsDealerConst[ArmsDealerPanelData.ArmsDealers[j].id].SkillSeller)
				{
					AtlasManage.SetSkillSpritName(component.ItemIcon2, UnitConst.GetInstance().skillList[current2.Key].icon);
					component.ItemIcon2.gameObject.AddComponent<ItemTipsShow2>();
					if (j % 4 == 0)
					{
						component.ItemIcon2.GetComponent<ItemTipsShow2>().JianTouPostion = 3;
					}
					else
					{
						component.ItemIcon2.GetComponent<ItemTipsShow2>().JianTouPostion = 4;
					}
					component.ItemIcon2.GetComponent<ItemTipsShow2>().Type = 3;
					component.ItemIcon2.GetComponent<ItemTipsShow2>().Index = current2.Key;
					component.tittleName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillList[current2.Key].name, "skill");
					component.backSprite.GetComponent<UISprite>().spriteName = this.SetQuSprite((int)UnitConst.GetInstance().skillList[current2.Key].skillQuality);
				}
			}
			this.itemTable.Reposition();
		}
		this.PageBtnShow();
		this.itemSV.GetComponent<UIPanel>().clipOffset = new Vector2(-159f, 0f);
		this.itemSV.transform.localPosition = new Vector3(68f, 0f, 0f);
	}

	public string SetQuSprite(int quaty)
	{
		switch (quaty)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}

	private void FixedUpdate()
	{
		this.refreshMoney.color = ((HeroInfo.GetInstance().playerRes.RMBCoin >= int.Parse(UnitConst.GetInstance().DesighConfigDic[13].value)) ? Color.white : Color.red);
		this.refreshBtn.color = this.refreshMoney.color;
		this.playerDiaCount.text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
		if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), ArmsDealerPanelData.nextRefreshTime) > 0.0 && this.times != null)
		{
			this.isRefresh = true;
			this.times.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), ArmsDealerPanelData.nextRefreshTime);
		}
		else if (this.isRefresh)
		{
			ArmsDealerHandler.CG_CSRefreshArmsDealer();
			this.RefreshDataToUI();
			this.ConfirmPanel.SetActive(false);
			this.isRefresh = false;
			this.times.text = string.Empty;
		}
	}

	public void HiseGameobj()
	{
		ArmsDelaerItem._inst.buyBtn.SetActive(false);
		ArmsDelaerItem._inst.NoBuy.SetActive(false);
		ArmsDelaerItem._inst.backLayer.SetActive(false);
	}

	public void ArmsReshBtn(GameObject go)
	{
		if (HeroInfo.GetInstance().playerRes.RMBCoin < 20)
		{
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("消费提醒", "others"), LanguageManage.GetTextByKey("钻石不足请前往充值", "others"), LanguageManage.GetTextByKey("前往充值", "others"), delegate
			{
				FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
				if (!ShopPanelManage.shop.gameObject.activeSelf)
				{
					ShopPanelManage.shop.gameObject.SetActive(true);
				}
			}, LanguageManage.GetTextByKey("取消", "others"), null);
			return;
		}
		if (ArmsDealerPanelData.useMoneyRefreshTimes < int.Parse(UnitConst.GetInstance().DesighConfigDic[14].value))
		{
			ArmsDealerHandler.CG_CSRefreshArmsDealerUseMoney();
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("今日次数已用完", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	public void EventBtn(ArmsDealerPanel.ArmsDealerBtnType type, GameObject ga)
	{
		if (type != ArmsDealerPanel.ArmsDealerBtnType.Refesh)
		{
			if (type == ArmsDealerPanel.ArmsDealerBtnType.Buy)
			{
				ArmsDelaerItem componentInParent = ga.GetComponentInParent<ArmsDelaerItem>();
				if (!componentInParent.curArmsDealer.isSelled)
				{
					if (componentInParent.curArmsDealer.priceType == 1)
					{
						foreach (KeyValuePair<ResType, int> current in componentInParent.curArmsDealer.ResBuyer)
						{
							switch (current.Key)
							{
							case ResType.金币:
								if (current.Value > 0 && HeroInfo.GetInstance().playerRes.resCoin < current.Value)
								{
									HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币不足!", "others"), HUDTextTool.TextUITypeEnum.Num5);
									return;
								}
								break;
							case ResType.石油:
								if (current.Value > 0 && HeroInfo.GetInstance().playerRes.resOil < current.Value)
								{
									HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("石油不足!", "others"), HUDTextTool.TextUITypeEnum.Num5);
									return;
								}
								break;
							case ResType.钢铁:
								if (current.Value > 0 && HeroInfo.GetInstance().playerRes.resSteel < current.Value)
								{
									HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("钢铁不足!", "others"), HUDTextTool.TextUITypeEnum.Num5);
									return;
								}
								break;
							case ResType.稀矿:
								if (current.Value > 0 && HeroInfo.GetInstance().playerRes.resRareEarth < current.Value)
								{
									HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("稀矿不足!", "others"), HUDTextTool.TextUITypeEnum.Num5);
									return;
								}
								break;
							case ResType.钻石:
								if (current.Value > 0 && HeroInfo.GetInstance().playerRes.RMBCoin < current.Value)
								{
									MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("消费提醒", "others"), LanguageManage.GetTextByKey("钻石不足请前往充值", "others"), LanguageManage.GetTextByKey("前往充值", "others"), delegate
									{
										FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
										if (!ShopPanelManage.shop.gameObject.activeSelf)
										{
											ShopPanelManage.shop.gameObject.SetActive(true);
										}
									}, LanguageManage.GetTextByKey("取消", "others"), null);
									return;
								}
								break;
							}
						}
					}
					else if (componentInParent.curArmsDealer.priceType == 2)
					{
						foreach (KeyValuePair<int, int> current2 in componentInParent.curArmsDealer.ItemBuyer)
						{
							if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current2.Key) || HeroInfo.GetInstance().PlayerItemInfo[current2.Key] < current2.Value)
							{
								HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足!", "others"), HUDTextTool.TextUITypeEnum.Num5);
								return;
							}
						}
					}
					this.ConfirmPanel.SetActive(true);
					this.ConfirmPanel.GetComponent<ConfilrmPanelManage>().id = componentInParent.curArmsDealer.id;
				}
				else
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("商品售完请等待下次刷新提示··", "others"), HUDTextTool.TextUITypeEnum.Num5);
				}
			}
		}
		else
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < 20)
			{
				MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("消费提醒", "others"), LanguageManage.GetTextByKey("钻石不足请前往充值", "others"), LanguageManage.GetTextByKey("前往充值", "others"), delegate
				{
					FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
					if (!ShopPanelManage.shop.gameObject.activeSelf)
					{
						ShopPanelManage.shop.gameObject.SetActive(true);
					}
				}, LanguageManage.GetTextByKey("取消", "others"), null);
				return;
			}
			if (ArmsDealerPanelData.useMoneyRefreshTimes < int.Parse(UnitConst.GetInstance().DesighConfigDic[14].value))
			{
				ArmsDealerHandler.CG_CSRefreshArmsDealerUseMoney();
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("今日次数已用完", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
	}
}
