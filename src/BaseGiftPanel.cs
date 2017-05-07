using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseGiftPanel : FuncUIPanel
{
	public static BaseGiftPanel _inst;

	public UILabel Des_Label;

	public GameObject BuyBtn;

	public UILabel BuyBtn_Money;

	public GameObject ResPrefab;

	public GameObject ItemPrefab;

	public GameObject SkillPrefab;

	public UIGrid ThisGrid;

	public GameObject Next;

	public GameObject Pre;

	private int nowIndex;

	private ActivityClass NowPlayerBaseGiftData
	{
		get
		{
			return HeroInfo.GetInstance().BaseGiftClass[28][this.nowIndex];
		}
	}

	private new void Awake()
	{
		BaseGiftPanel._inst = this;
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BaseGiftPanel_Close, new EventManager.VoidDelegate(this.BaseGiftPanel_Close));
		EventManager.Instance.AddEvent(EventManager.EventType.BaseGiftPanel_GetBtn, new EventManager.VoidDelegate(this.BaseGiftPanel_GetBtn));
		UIEventListener.Get(this.Next).onClick = delegate(GameObject a)
		{
			this.nowIndex++;
			this.RreshData();
		};
		UIEventListener.Get(this.Pre).onClick = delegate(GameObject a)
		{
			this.nowIndex--;
			this.RreshData();
		};
	}

	private void BaseGiftPanel_Close(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("BaseGiftPanel");
	}

	private void BaseGiftPanel_GetBtn(GameObject ga)
	{
		ShopHandler.CS_ShopBuyRMB(this.NowPlayerBaseGiftData.shopID, this.NowPlayerBaseGiftData.activityId, delegate
		{
		});
	}

	public override void OnEnable()
	{
		this.nowIndex = 0;
		base.OnEnable();
		for (int i = 0; i < HeroInfo.GetInstance().BaseGiftClass[28].Count; i++)
		{
			if (HeroInfo.GetInstance().BaseGiftClass[28][i].conditionID >= HeroInfo.GetInstance().PlayerCommondLv)
			{
				this.nowIndex = i;
				break;
			}
		}
		this.RreshData();
	}

	private void RreshData()
	{
		this.ThisGrid.ClearChild();
		this.ThisGrid.Reposition();
		this.Des_Label.text = string.Format("司令部[FFFF00]{0}[-]级礼包", this.NowPlayerBaseGiftData.conditionID);
		this.BuyBtn_Money.text = string.Format("充值{0}元", UnitConst.GetInstance().shopItem[this.NowPlayerBaseGiftData.shopID].price.ToString());
		if (HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(this.NowPlayerBaseGiftData.activityId) && HeroInfo.GetInstance().ActivitiesData_RecieveState[this.NowPlayerBaseGiftData.activityId] == 2)
		{
			this.BuyBtn_Money.text = LanguageManage.GetTextByKey("已领取", "Activities");
			GameTools.RemoveComponent<BoxCollider>(this.BuyBtn);
		}
		else
		{
			GameTools.GetCompentIfNoAddOne<BoxCollider>(this.BuyBtn);
		}
		if (this.nowIndex == 0)
		{
			this.Pre.SetActive(false);
			this.Next.SetActive(true);
		}
		else if (this.nowIndex == HeroInfo.GetInstance().BaseGiftClass[28].Count - 1)
		{
			this.Next.SetActive(false);
			this.Pre.SetActive(true);
		}
		else
		{
			this.Next.SetActive(true);
			this.Pre.SetActive(true);
		}
		foreach (KeyValuePair<ResType, int> current in this.NowPlayerBaseGiftData.curActivityResReward)
		{
			GameObject gameObject = NGUITools.AddChild(this.ThisGrid.gameObject, this.ResPrefab);
			ActivityRes component = gameObject.GetComponent<ActivityRes>();
			switch (current.Key)
			{
			case ResType.金币:
				AtlasManage.SetResSpriteName(component.icon, ResType.金币);
				component.count.text = current.Value.ToString();
				break;
			case ResType.石油:
				AtlasManage.SetResSpriteName(component.icon, ResType.石油);
				component.count.text = current.Value.ToString();
				break;
			case ResType.钢铁:
				AtlasManage.SetResSpriteName(component.icon, ResType.钢铁);
				component.count.text = current.Value.ToString();
				break;
			case ResType.稀矿:
				AtlasManage.SetResSpriteName(component.icon, ResType.稀矿);
				component.count.text = current.Value.ToString();
				break;
			case ResType.钻石:
				AtlasManage.SetResSpriteName(component.icon, ResType.钻石);
				component.count.text = current.Value.ToString();
				break;
			}
		}
		foreach (KeyValuePair<int, int> current2 in this.NowPlayerBaseGiftData.curActivityItemReward)
		{
			GameObject gameObject2 = NGUITools.AddChild(this.ThisGrid.gameObject, this.ItemPrefab);
			ActivityItemPre component2 = gameObject2.GetComponent<ActivityItemPre>();
			AtlasManage.SetUiItemAtlas(component2.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component2.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			component2.count.gameObject.SetActive(false);
			component2.name.text = current2.Value.ToString();
			ItemTipsShow2 component3 = component2.GetComponent<ItemTipsShow2>();
			component3.Index = current2.Key;
			component3.Type = 1;
		}
		foreach (KeyValuePair<int, int> current3 in this.NowPlayerBaseGiftData.curActivitySkillReward)
		{
			GameObject gameObject3 = NGUITools.AddChild(this.ThisGrid.gameObject, this.SkillPrefab);
			ActivitySkillPrefab component4 = gameObject3.GetComponent<ActivitySkillPrefab>();
			AtlasManage.SetSkillSpritName(component4.icon, UnitConst.GetInstance().skillList[current3.Key].icon);
			AtlasManage.SetQuilitySpriteName(component4.bg, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
			component4.name.text = UnitConst.GetInstance().skillList[current3.Key].name;
			switch (UnitConst.GetInstance().skillList[current3.Key].skillQuality)
			{
			case Quility_ResAndItemAndSkill.白:
				component4.name.color = Color.white;
				break;
			case Quility_ResAndItemAndSkill.绿:
				component4.name.color = new Color(0.243137255f, 0.8862745f, 0.117647059f);
				break;
			case Quility_ResAndItemAndSkill.蓝:
				component4.name.color = new Color(0.007843138f, 0.8039216f, 1f);
				break;
			case Quility_ResAndItemAndSkill.紫:
				component4.name.color = new Color(0.7372549f, 0.007843138f, 0.870588243f);
				break;
			case Quility_ResAndItemAndSkill.红:
				component4.name.color = new Color(1f, 0.007843138f, 0.09019608f);
				break;
			}
			ItemTipsShow2 component5 = gameObject3.GetComponent<ItemTipsShow2>();
			component5.Index = current3.Key;
			component5.Type = 3;
		}
		base.StartCoroutine(this.ThisGrid.RepositionAfterFrame());
	}
}
