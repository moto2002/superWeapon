using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyFuncPanleManage : MonoBehaviour
{
	public UITable armyTable;

	public UITable armyFuncedTable;

	public GameObject modelParent;

	public UILabel buildingPeopleNumUILabel;

	[HideInInspector]
	public List<ArmyFuncItemUI> allArmyList = new List<ArmyFuncItemUI>();

	private int towerLV;

	private int towerIndex;

	private long towerId;

	[HideInInspector]
	public int buildingPeopleNum;

	[HideInInspector]
	public int armyFuncedPeopleNum;

	private armyInfoInBuilding armyInfo_Building;

	public void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyFuncPanel_ArmyFunc, new EventManager.VoidDelegate(this.ArmyFuncPanel_ArmyFunc));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyFuncPanel_ArmyCancle, new EventManager.VoidDelegate(this.ArmyFuncPanel_ArmyCancle));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyFuncPanel_SellArmyFuncEnd, new EventManager.VoidDelegate(this.ArmyFuncPanel_SellArmyFuncEnd));
	}

	private void ArmyFuncPanel_ArmyFunc(GameObject ga)
	{
		T_CommandPanelManage._instance.click_protect_time = 1f;
		DieBall dieBall = PoolManage.Ins.CreatEffect("xuanzebingzhong", ga.GetComponent<ArmyFuncItemUI>().BJ.transform.position, Quaternion.identity, null);
		dieBall.LifeTime = 0.7f;
		Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 8;
		}
		ga.GetComponent<ButtonClick>().isSendLua = false;
		ArmyFuncItemUI funcItem = ga.GetComponent<ArmyFuncItemUI>();
		if (funcItem.isUnLock)
		{
			if (funcItem.ArmyInfo_Planner != null)
			{
				if (ga.GetComponent<ArmyFuncItemUI>().ArmyInfo_Planner.peopleNum + HeroInfo.GetInstance().All_PeopleNum_Occupy > this.buildingPeopleNum)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("人口不足", "Army"), HUDTextTool.TextUITypeEnum.Num5);
				}
				else
				{
					ga.GetComponent<ArmyFuncItemUI>().ArmyFuncPanel_ArmyFunc();
				}
			}
			else if (funcItem.SoliderInfo_Planner != null)
			{
				if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd == null || HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId == 0)
				{
					ga.GetComponent<ArmyFuncItemUI>().ArmyFuncPanel_ArmyFunc();
				}
				else
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("指挥官只能上阵一位！", "Army"), HUDTextTool.TextUITypeEnum.Num5);
				}
			}
		}
		else if (funcItem.ArmyInfo_Planner != null)
		{
			HUDTextTool.inst.SetText(string.Format("{0}{1}", UnitConst.GetInstance().GetArmyOpenToBuildingLV(funcItem.ArmyInfo_Planner.unitId, this.towerIndex), LanguageManage.GetTextByKey("级建筑可解锁此兵种", "Army")), HUDTextTool.TextUITypeEnum.Num5);
		}
		else if (funcItem.SoliderInfo_Planner != null)
		{
			if (funcItem.SoliderInfo_Planner.unlockType == 1)
			{
				HUDTextTool.inst.SetText(string.Format("{0}{1}", funcItem.SoliderInfo_Planner.unLock, LanguageManage.GetTextByKey("级主基地可解锁此兵种", "Army")), HUDTextTool.TextUITypeEnum.Num5);
			}
			else if (funcItem.SoliderInfo_Planner.unlockType == 2)
			{
				MessageBox.GetMessagePanel().ShowBtn(string.Empty, string.Format("{0}{1}\n{2}", LanguageManage.GetTextByKey("是否购买", "soldier"), LanguageManage.GetTextByKey(funcItem.SoliderInfo_Planner.name, "soldier"), LanguageManage.GetTextByKey("花费钻石", "others") + ":" + funcItem.SoliderInfo_Planner.unLock), LanguageManage.GetTextByKey("是", "soldier"), delegate
				{
					if (HeroInfo.GetInstance().playerRes.RMBCoin > funcItem.SoliderInfo_Planner.unLock)
					{
						SpecialSoliderHandler.CS_SoldierAdd(funcItem.SoliderInfo_Planner.id, 2, delegate
						{
							FuncUIManager.inst.ArmyFuncHandler_ArmyInfoNew(24);
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("恭喜您获得了新兵种", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num1);
							funcItem.isUnLock = true;
							funcItem.RrefshData();
						});
						return;
					}
					HUDTextTool.inst.ShowBuyMoney();
				}, LanguageManage.GetTextByKey("否", "soldier"), delegate
				{
				});
			}
			else if (funcItem.SoliderInfo_Planner.unlockType == 3)
			{
				HUDTextTool.inst.SetText(string.Format("{0}", LanguageManage.GetTextByKey("活动开启此兵种", "solider")), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
	}

	private void ArmyFuncPanel_ArmyCancle(GameObject ga)
	{
		T_CommandPanelManage._instance.click_protect_time = 1f;
		ga.GetComponent<ButtonClick>().isSendLua = false;
		ga.GetComponentInParent<ArmyFuncItemUI>().ArmyFuncPanel_ArmyCancle();
	}

	private void ArmyFuncPanel_SellArmyFuncEnd(GameObject ga)
	{
		T_CommandPanelManage._instance.click_protect_time = 1f;
		ga.GetComponent<ButtonClick>().isSendLua = false;
		int num = int.Parse(ga.GetComponentInParent<ArmyFuncItemUI>().gameObject.name);
		if (UnitConst.GetInstance().buildingConst[this.towerIndex].secType == 15)
		{
			int funcMoney = UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)num, (float)HeroInfo.GetInstance().PlayerCommandoes[num].star)].FuncMoney;
			Debug.LogError(funcMoney);
			if (funcMoney + HeroInfo.GetInstance().playerRes.resCoin > HeroInfo.GetInstance().playerRes.maxCoin)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币即将爆仓无法出售", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			}
			else
			{
				ArmyFuncHandler.CG_CancelConfigureSolider(num, delegate
				{
					HUDTextTool.inst.NextLuaCall("卖兵完成", new object[0]);
				});
			}
		}
		else
		{
			int buyCost = UnitConst.GetInstance().soldierConst[num].lvInfos[HeroInfo.GetInstance().GetArmyLevelByID(num)].BuyCost;
			Debug.LogError(buyCost);
			if (buyCost + HeroInfo.GetInstance().playerRes.resCoin > HeroInfo.GetInstance().playerRes.maxCoin)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币即将爆仓无法出售", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			}
			else
			{
				ArmyFuncHandler.CG_SellConfiguredArmy(this.towerId, num, delegate
				{
					HUDTextTool.inst.NextLuaCall("卖兵完成", new object[0]);
				});
			}
		}
	}

	public void OnEnable()
	{
		this.armyTable.Reposition();
		this.armyFuncedTable.Reposition();
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10116 || opcodeCMD == 10090)
		{
			this.RrefshFuncedArmyData();
			this.RrefshArmyListData();
		}
	}

	private void RrefshArmyListData()
	{
		for (int i = 0; i < this.allArmyList.Count; i++)
		{
			this.allArmyList[i].RrefshData();
		}
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void Start()
	{
	}

	public void InitData(int _towerLV, int _towerIndex, long _towerId)
	{
		base.gameObject.SetActive(true);
		this.towerLV = _towerLV;
		this.towerIndex = _towerIndex;
		this.towerId = _towerId;
		int armyType = 0;
		if (UnitConst.GetInstance().buildingConst[this.towerIndex].secType == 6)
		{
			armyType = 1;
		}
		else if (UnitConst.GetInstance().buildingConst[this.towerIndex].secType == 21)
		{
			armyType = 4;
		}
		else if (UnitConst.GetInstance().buildingConst[this.towerIndex].secType == 15)
		{
			armyType = 6;
		}
		this.armyTable.DestoryChildren(true);
		this.allArmyList.Clear();
		if (armyType == 1 || armyType == 4)
		{
			NewUnInfo[] array = (from a in UnitConst.GetInstance().soldierConst
			where a.presence == armyType
			orderby a.counts
			select a).ToArray<NewUnInfo>();
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = this.armyTable.CreateChildren(array[i].name, true);
				ArmyFuncItemUI component = gameObject.GetComponent<ArmyFuncItemUI>();
				this.allArmyList.Add(component);
				component.InitData(this.towerLV, this.towerIndex, this.towerId, array[i]);
			}
		}
		else if (armyType == 6)
		{
			foreach (Soldier current in UnitConst.GetInstance().soldierList.Values)
			{
				if (current.isUI)
				{
					GameObject gameObject2 = this.armyTable.CreateChildren(current.name, true);
					ArmyFuncItemUI component2 = gameObject2.GetComponent<ArmyFuncItemUI>();
					this.allArmyList.Add(component2);
					component2.InitData(this.towerLV, this.towerIndex, this.towerId, current);
				}
			}
		}
		this.armyTable.Reposition();
		this.RrefshFuncedArmyData();
	}

	public void RrefshFuncedArmyData()
	{
		this.armyFuncedTable.DestoryChildren(true);
		if (HeroInfo.GetInstance().AllArmyInfo.ContainsKey(this.towerId))
		{
			this.armyInfo_Building = HeroInfo.GetInstance().AllArmyInfo[this.towerId];
		}
		else
		{
			this.armyInfo_Building = null;
		}
		if (UnitConst.GetInstance().buildingConst[this.towerIndex].secType == 15)
		{
			this.buildingPeopleNum = 1;
			this.armyFuncedPeopleNum = 0;
			this.buildingPeopleNumUILabel.text = string.Format("{0}/{1}", 0, 1);
			if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd != null)
			{
				if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId > 0)
				{
					this.buildingPeopleNumUILabel.text = string.Format("{0}/{1}", 1, 1);
					this.armyFuncedPeopleNum = 1;
				}
				if (HeroInfo.GetInstance().Commando_Fight != null)
				{
					GameObject gameObject = this.armyFuncedTable.CreateChildren(HeroInfo.GetInstance().Commando_Fight.index.ToString(), true);
					ArmyFuncItemUI component = gameObject.GetComponent<ArmyFuncItemUI>();
					component.nameUILabel.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].name, "soldier");
					component.numUILabel.text = string.Format("X{0}", 1);
					AtlasManage.SetSpritName(component.armyIcon, UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].icon);
					this.armyFuncedPeopleNum = 1;
				}
			}
		}
		else
		{
			this.buildingPeopleNum = HeroInfo.GetInstance().All_PeopleNum;
			this.armyFuncedPeopleNum = 0;
			if (this.armyInfo_Building != null)
			{
				for (int i = 0; i < this.armyInfo_Building.armyFunced.Count; i++)
				{
					if (this.armyInfo_Building.armyFunced[i].value > 0L)
					{
						GameObject gameObject2 = this.armyFuncedTable.CreateChildren(this.armyInfo_Building.armyFunced[i].key.ToString(), true);
						ArmyFuncItemUI component2 = gameObject2.GetComponent<ArmyFuncItemUI>();
						component2.nameUILabel.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[(int)(checked((IntPtr)this.armyInfo_Building.armyFunced[i].key))].name, "Army");
						component2.numUILabel.text = string.Format("X{0}", this.armyInfo_Building.armyFunced[i].value);
						AtlasManage.SetArmyIconSpritName(component2.armyIcon, (int)this.armyInfo_Building.armyFunced[i].key);
						this.armyFuncedPeopleNum += (int)(this.armyInfo_Building.armyFunced[i].value * (long)UnitConst.GetInstance().soldierConst[(int)this.armyInfo_Building.armyFunced[i].key].peopleNum);
					}
				}
				for (int j = 0; j < this.armyInfo_Building.armyFuncing.Count; j++)
				{
					this.armyFuncedPeopleNum += UnitConst.GetInstance().soldierConst[(int)this.armyInfo_Building.armyFuncing[j].value].peopleNum;
				}
			}
			this.armyFuncedPeopleNum = HeroInfo.GetInstance().All_PeopleNum_Occupy;
			this.buildingPeopleNumUILabel.text = string.Format("{1}/{0}", this.buildingPeopleNum, this.armyFuncedPeopleNum);
		}
		this.armyFuncedTable.Reposition();
		ArmyFuncItemUI[] componentsInChildren = this.armyFuncedTable.GetComponentsInChildren<ArmyFuncItemUI>();
		for (int k = 0; k < componentsInChildren.Length; k++)
		{
			ArmyFuncItemUI armyFuncItemUI = componentsInChildren[k];
			if (!armyFuncItemUI.GetComponent<UISprite>())
			{
				DieBall dieBall = PoolManage.Ins.CreatEffect("peibingwancheng", armyFuncItemUI.BJ.transform.position, Quaternion.identity, null);
				dieBall.LifeTime = 0.7f;
				Transform[] componentsInChildren2 = dieBall.GetComponentsInChildren<Transform>();
				for (int l = 0; l < componentsInChildren2.Length; l++)
				{
					Transform transform = componentsInChildren2[l];
					transform.gameObject.layer = 8;
				}
			}
		}
	}
}
