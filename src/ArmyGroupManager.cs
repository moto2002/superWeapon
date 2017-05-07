using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmyGroupManager : FuncUIPanel
{
	public static ArmyGroupManager ins;

	public UITable armyTabel;

	public GameObject armyPrefab;

	public CreatArmyPanel crateArmy;

	public GameObject mainPanel;

	public UIScrollView armyScrollView;

	public SearchArmyPanel searchPanel;

	public static Dictionary<int, ArmyPrefab> armyDic = new Dictionary<int, ArmyPrefab>();

	public static bool isSearch = false;

	public GameObject noLegion;

	public GameObject NullLegon;

	public GameObject LegionGift_Panel;

	public void OnDestroy()
	{
		ArmyGroupManager.ins = null;
	}

	private new void Awake()
	{
		ArmyGroupManager.ins = this;
	}

	private new void OnEnable()
	{
		this.Init();
		this.showPanel(3);
		NetMgr.inst.NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		base.OnEnable();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10104 || opcodeCMD == 10107)
		{
			this.ShowArmyGrop();
		}
	}

	public new void OnDisable()
	{
		NetMgr.inst.NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		base.OnDisable();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_ChangeArmy, new EventManager.VoidDelegate(this.ChangeArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_ClosePanel, new EventManager.VoidDelegate(this.ClosePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_CreatArmy, new EventManager.VoidDelegate(this.OpenCreateArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_EnterArmy, new EventManager.VoidDelegate(this.EnterArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_CloseCreatArmyPanel, new EventManager.VoidDelegate(this.CloseCreatPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_ClseeSearchArmyPanel, new EventManager.VoidDelegate(this.CloseSearchPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_OpenSearchArmyPanel, new EventManager.VoidDelegate(this.OpenSearchArmy));
	}

	public void ShowArmyGrop()
	{
		this.DestoryChildCont();
		this.armyTabel.Reposition();
		foreach (KeyValuePair<int, SearchArmyInfo> current in HeroInfo.GetInstance().armyGroupDataData.searchDic)
		{
			GameObject gameObject = NGUITools.AddChild(this.armyTabel.gameObject, this.armyPrefab);
			gameObject.name = current.Key.ToString();
			ArmyPrefab component = gameObject.GetComponent<ArmyPrefab>();
			component.armyRank.text = current.Value.rank.ToString();
			component.armyName.text = current.Value.name;
			component.armyMedalCount.text = current.Value.scroe.ToString();
			component.armyPeoNum.text = current.Value.count + "/" + current.Value.limit;
			component.btnEnterArmy.name = current.Key.ToString();
			component.nowCount = current.Value.count;
			component.limitCount = current.Value.limit;
			component.limitMedal = current.Value.minMedal;
			if (!ArmyGroupManager.armyDic.ContainsKey(current.Key))
			{
				ArmyGroupManager.armyDic.Add(current.Key, component);
			}
		}
		this.armyTabel.Reposition();
	}

	private void DestoryChildCont()
	{
		GameObject gameObject = this.armyTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.armyScrollView.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void ClosePanel(GameObject ga)
	{
		ArmyGroupManager.isSearch = false;
		FuncUIManager.inst.HideFuncUI("ArmyGroupMnagerPanel");
	}

	private void CloseCreatPanel(GameObject ga)
	{
		this.showPanel(3);
	}

	private void CloseSearchPanel(GameObject ga)
	{
		this.showPanel(3);
	}

	private void EnterArmy(GameObject ga)
	{
		ArmyPrefab componentInParent = ga.GetComponentInParent<ArmyPrefab>();
		if (HeroInfo.GetInstance().playerGroupId != 0L)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你已经加入过军团", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if ((TimeTools.GetNowTimeSyncServerToDateTime() - HeroInfo.GetInstance().LegionOutTime).TotalHours < 2.0)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你退出军团未到俩个小时无法加入军团", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (componentInParent.nowCount >= componentInParent.limitCount)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团人数已满 ，无法加入", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().playerRes.playermedal < componentInParent.limitMedal)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您的奖牌数量不足，加入申请未被允许", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		ArmyGroupHandler.CG_CSLegionAdd(long.Parse(ga.name), delegate(bool isError)
		{
			if (!isError)
			{
				ArmyManager.ins.ChangePanel();
			}
		});
	}

	private void RefreshArmyPre(int id)
	{
		foreach (KeyValuePair<int, ArmyPrefab> current in ArmyGroupManager.armyDic)
		{
			if (current.Key == id)
			{
				current.Value.isClickThis = false;
				current.Value.btnEnterArmy.SetActive(false);
			}
		}
	}

	private void OpenCreateArmy(GameObject ga)
	{
		LogManage.LogError(TimeTools.GetNowTimeSyncServerToDateTime());
		if ((TimeTools.GetNowTimeSyncServerToDateTime() - HeroInfo.GetInstance().LegionOutTime).TotalHours <= 2.0 && HeroInfo.GetInstance().LegionOutTime > DateTime.MinValue)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你退出军团未到俩个小时无法创建军团", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().playerGroupId != 0L)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你已经是军团成员，无法创建军团", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		this.showPanel(1);
	}

	private void ChangeArmy(GameObject ga)
	{
		ArmyGroupManager.isSearch = true;
		ArmyGroupHandler.CG_CSRandomLegion(1, delegate(bool isError)
		{
			if (!isError)
			{
				this.ShowArmyGrop();
			}
		});
	}

	private void OpenSearchArmy(GameObject ga)
	{
		this.showPanel(2);
	}

	public void showPanel(int id)
	{
		switch (id)
		{
		case 1:
			this.crateArmy.gameObject.SetActive(true);
			this.mainPanel.gameObject.SetActive(false);
			this.searchPanel.gameObject.SetActive(false);
			break;
		case 2:
			this.crateArmy.gameObject.SetActive(false);
			this.mainPanel.gameObject.SetActive(false);
			this.searchPanel.gameObject.SetActive(true);
			break;
		case 3:
			this.crateArmy.gameObject.SetActive(false);
			this.mainPanel.gameObject.SetActive(true);
			this.searchPanel.gameObject.SetActive(false);
			break;
		}
	}
}
