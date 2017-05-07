using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryRankGiftPanel : FuncUIPanel
{
	public static MilitaryRankGiftPanel _inst;

	public HeroInfo.MilitaryRank PlayerMilitaryRank;

	public UILabel Title;

	public UILabel Des;

	public UISprite GiftGet_Btn;

	public UILabel GiftGet_Label;

	public UISprite MilitaryRank_Icon_Now;

	public UISprite MilitaryRank_Icon_Next;

	public UILabel MilitaryRank_Name_Now;

	public UILabel MilitaryRank_Name_Next;

	public UILabel CommondLevel_Des;

	public UILabel Medal_Des;

	public GameObject ResPrefab;

	public GameObject ItemPrefab;

	public GameObject SkillPrefab;

	public UIGrid Now_Gift_Table;

	public UIGrid Next_Gift_Table;

	public UIGrid Next_MilitaryRank_Grid;

	public GameObject Next_MilitaryRank_Prefab;

	public bool CanGetMilitaryRankGiftTime;

	public void OnDestroy()
	{
		MilitaryRankGiftPanel._inst = null;
	}

	public override void Awake()
	{
		MilitaryRankGiftPanel._inst = this;
	}

	private void MainPanel_MilitaryRank_GiftGet_CallBack(GameObject ga)
	{
		if (!this.CanGetMilitaryRankGiftTime)
		{
			return;
		}
		MilitaryRankGiftPanel.CS_GetMilitaryRankGift();
	}

	public static void CS_GetMilitaryRankGift()
	{
		CSMilitaryRankPrize cSMilitaryRankPrize = new CSMilitaryRankPrize();
		cSMilitaryRankPrize.id = 1;
		ClientMgr.GetNet().SendHttp(3022, cSMilitaryRankPrize, new DataHandler.OpcodeHandler(MilitaryRankGiftPanel.SCGetMilitaryRankGiftCallBack), null);
	}

	public void SetGetMilitaryRankGiftTime()
	{
		if (TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().MilitaryRankGift_Time).Day != TimeTools.GetNowTimeSyncServerToDateTime().Day)
		{
			this.CanGetMilitaryRankGiftTime = true;
			this.GiftGet_Label.text = "领取奖励";
			this.GiftGet_Btn.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			this.CanGetMilitaryRankGiftTime = false;
			this.GiftGet_Label.text = "已领取";
			this.GiftGet_Btn.color = new Color(0f, 0f, 0f, 1f);
		}
	}

	public static void SCGetMilitaryRankGiftCallBack(bool Error, Opcode func)
	{
		Debug.Log("领取每日军衔奖励回调");
		if (!Error)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}

	private void MainPanel_MilitaryRank_GiftPanel_Close_CallBack(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("MilitaryRankGiftPanel");
	}

	private new void OnEnable()
	{
		this.SetGetMilitaryRankGiftTime();
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MilitaryRank_GiftGet, new EventManager.VoidDelegate(this.MainPanel_MilitaryRank_GiftGet_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MilitaryRank_GiftPanel_Close, new EventManager.VoidDelegate(this.MainPanel_MilitaryRank_GiftPanel_Close_CallBack));
		this.PlayerMilitaryRank = HeroInfo.GetInstance().PlayerMilitaryRank;
		this.Title.text = "每日军衔奖励";
		this.Des.text = "*每日凌晨0点之后可以领取";
		if (UnitConst.GetInstance().MilitaryRankDataList.ContainsKey((int)this.PlayerMilitaryRank))
		{
			this.MilitaryRank_Icon_Now.spriteName = UnitConst.GetInstance().MilitaryRankDataList[(int)this.PlayerMilitaryRank].icon;
			this.MilitaryRank_Name_Now.text = UnitConst.GetInstance().MilitaryRankDataList[(int)this.PlayerMilitaryRank].name.ToString();
			this.CreateGift(UnitConst.GetInstance().MilitaryRankDataList[(int)this.PlayerMilitaryRank], this.Now_Gift_Table.gameObject);
		}
		if (this.Next_MilitaryRank_Grid.transform.childCount == 0 && this.PlayerMilitaryRank < (HeroInfo.MilitaryRank)UnitConst.GetInstance().MilitaryRankDataList.Count)
		{
			for (int i = 0; i < UnitConst.GetInstance().MilitaryRankDataList.Count - (int)this.PlayerMilitaryRank; i++)
			{
				if (UnitConst.GetInstance().MilitaryRankDataList.ContainsKey((int)(this.PlayerMilitaryRank + 1 + i)))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(this.Next_MilitaryRank_Prefab) as GameObject;
					gameObject.transform.parent = this.Next_MilitaryRank_Grid.transform;
					UISprite component = gameObject.transform.FindChild("NextMilitaryRank").GetComponent<UISprite>();
					component.spriteName = UnitConst.GetInstance().MilitaryRankDataList[(int)(this.PlayerMilitaryRank + 1 + i)].icon;
					UILabel component2 = component.transform.FindChild("MilitaryRankName").GetComponent<UILabel>();
					component2.text = UnitConst.GetInstance().MilitaryRankDataList[(int)(this.PlayerMilitaryRank + 1 + i)].name.ToString();
					UILabel component3 = component2.transform.FindChild("LV").GetComponent<UILabel>();
					component3.text = "司令部LV:" + UnitConst.GetInstance().MilitaryRankDataList[(int)(this.PlayerMilitaryRank + 1 + i)].commondLevel.ToString();
					UILabel component4 = component2.transform.FindChild("CUP").GetComponent<UILabel>();
					component4.text = "奖杯:" + UnitConst.GetInstance().MilitaryRankDataList[(int)(this.PlayerMilitaryRank + 1 + i)].medal.ToString();
					this.CreateGift(UnitConst.GetInstance().MilitaryRankDataList[(int)(this.PlayerMilitaryRank + 1 + i)], gameObject.transform.FindChild("Scroll View").FindChild("Next_Grid").GetComponent<UIGrid>().gameObject);
				}
			}
		}
	}

	private void CreateGift(MilitaryRankData data, GameObject table)
	{
		if (table)
		{
			Transform[] componentsInChildren = table.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				if (transform && transform.gameObject != table)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
		int num = 0;
		if (data.money > 0)
		{
			num++;
			GameObject gameObject = UnityEngine.Object.Instantiate(this.ResPrefab) as GameObject;
			gameObject.transform.parent = table.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			ActivityRes component = gameObject.GetComponent<ActivityRes>();
			AtlasManage.SetResSpriteName(component.icon, ResType.钻石);
			component.count.text = data.money.ToString();
		}
		foreach (KeyValuePair<ResType, int> current in data.res)
		{
			num++;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(this.ResPrefab) as GameObject;
			gameObject2.transform.parent = table.transform;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localScale = Vector3.one;
			ActivityRes component2 = gameObject2.GetComponent<ActivityRes>();
			AtlasManage.SetResSpriteName(component2.icon, current.Key);
			component2.count.text = current.Value.ToString();
		}
		foreach (KeyValuePair<int, int> current2 in data.items)
		{
			num++;
			GameObject gameObject3 = UnityEngine.Object.Instantiate(this.ItemPrefab) as GameObject;
			gameObject3.transform.parent = table.transform;
			gameObject3.transform.localPosition = Vector3.zero;
			gameObject3.transform.localScale = Vector3.one;
			ActivityItemPre component3 = gameObject3.GetComponent<ActivityItemPre>();
			AtlasManage.SetUiItemAtlas(component3.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component3.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			component3.count.text = string.Format("X{0}", current2.Value.ToString());
			ItemTipsShow2 component4 = component3.GetComponent<ItemTipsShow2>();
			component4.Index = current2.Key;
			component4.Type = 1;
		}
		foreach (KeyValuePair<int, int> current3 in data.skill)
		{
			num++;
			GameObject gameObject4 = UnityEngine.Object.Instantiate(this.SkillPrefab) as GameObject;
			gameObject4.transform.parent = table.transform;
			gameObject4.transform.localPosition = Vector3.zero;
			gameObject4.transform.localScale = Vector3.one;
			ActivitySkillPrefab component5 = gameObject4.GetComponent<ActivitySkillPrefab>();
			AtlasManage.SetSkillSpritName(component5.icon, UnitConst.GetInstance().skillList[current3.Key].icon);
			AtlasManage.SetQuilitySpriteName(component5.bg, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
			component5.name.text = UnitConst.GetInstance().skillList[current3.Key].name;
			ItemTipsShow2 component6 = component5.GetComponent<ItemTipsShow2>();
			component6.Index = current3.Key;
			component6.Type = 3;
		}
		base.StartCoroutine(table.GetComponent<UIGrid>().RepositionAfterFrame());
		if (num <= 6)
		{
			table.transform.parent.GetComponent<UIScrollView>().enabled = false;
		}
		else
		{
			table.transform.parent.GetComponent<UIScrollView>().enabled = true;
		}
	}

	private void Update()
	{
	}
}
