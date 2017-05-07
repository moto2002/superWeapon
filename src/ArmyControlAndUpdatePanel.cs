using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

public class ArmyControlAndUpdatePanel : FuncUIPanel
{
	public static ArmyControlAndUpdatePanel Inst;

	public UIGrid centerGrid;

	public UIGrid bottomGrid;

	public GameObject centerPrefab;

	public GameObject bottomPrefab;

	public GameObject centerGa;

	public GameObject bottomGa;

	public UISprite leftSprite;

	public UISprite rightSprite;

	public UICenterOnChild centerGridOnChild;

	public GameObject rightTranform;

	public GameObject showRestraint;

	public UILabel armyName;

	public UILabel curArmyLV;

	public UILabel updateArmyLV;

	public UILabel costMoney;

	public UILabel updateTime;

	public UILabel updateCostMoney;

	public UILabel showOthers;

	public GameObject time;

	public UITable resTextTable;

	public resTextInfo[] life_att_def_exp;

	public Transform showLight;

	public Transform leftPos;

	public Transform rightPos;

	public UILabel leftName;

	public UILabel rightName;

	public GameObject updateBtn;

	public GameObject weijiesuo;

	public UILabel tiaoJian;

	public UILabel manji;

	[HideInInspector]
	public bool isAddDataEnd;

	public GameObject AltaPanel;

	public GameObject Waiting;

	public GameObject updatingTime;

	public GameObject updateingBtnGa;

	public GameObject updateBtnGa;

	public GameObject moveLeftBtn;

	public GameObject moveRightBtn;

	public UISprite updatingTimeSprite;

	public UILabel updatingTimeUIlabel;

	public UILabel buyTimeUIlabel;

	public GameObject armyFuncTimeGa;

	public UISprite armyFuncTimeSprite;

	public UILabel armyFuncTimeUIlabel;

	public Transform bottomTarget;

	public Transform Left;

	public GameObject updateEnd;

	public UILabel updateEndMoney;

	public GameObject funcArmyBtn;

	public static bool isFeijiChang;

	public static int posIndex;

	public static int index;

	public static int lv;

	public static long towerID;

	private ButtonClick updateArmy;

	public float protect_time;

	private DieBall shengji;

	private bool loadEnd;

	[HideInInspector]
	public ArmyControlJiJieDianItem SelTowerInfo;

	public Dictionary<int, ArmyControlAndUpdateCenterItem> AllSolider = new Dictionary<int, ArmyControlAndUpdateCenterItem>();

	private static string des;

	private static int num;

	private static Vector3 pianyi;

	private static Vector3 scale;

	public GameObject centerGa_Select;

	[HideInInspector]
	public int selectArmyIndex;

	[HideInInspector]
	public int tuozhuaiArmyIndex;

	[HideInInspector]
	public int selJiJieDian;

	private DateTime endTime;

	private List<KVStruct> allArmyCDTime
	{
		get
		{
			if (ArmyControlAndUpdatePanel.isFeijiChang)
			{
				return (from a in HeroInfo.GetInstance().PlayerArmy_AirDataCDTime
				orderby a.value
				select a).ToList<KVStruct>();
			}
			return (from a in HeroInfo.GetInstance().PlayerArmy_LandDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	public void OnDestroy()
	{
		ArmyControlAndUpdatePanel.Inst = null;
	}

	public override void Awake()
	{
		ArmyControlAndUpdatePanel.Inst = this;
		this.centerGridOnChild.CenterOnBack = new Action<GameObject>(this.CenterChildCallBack);
		this.InitEvent();
		DieBall dieBall = PoolManage.Ins.CreatEffect("xuanzhong_glow", Vector3.zero, Quaternion.identity, base.transform);
		dieBall.tr.localScale = Vector3.one;
		dieBall.tr.localPosition = new Vector3(-47f, 50f, 0f);
		Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 31;
		}
	}

	public override void OnEnable()
	{
		this.HideRightTrans();
		this.ShowRestriantMethod();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
		if (!this.loadEnd)
		{
			base.StartCoroutine(this.ShowArmy());
		}
		else
		{
			this.centerGridOnChild.CenterOn(this.AllSolider[this.selectArmyIndex].transform);
		}
	}

	public void SetRightPanelTo3D(bool True)
	{
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10091)
		{
			List<KVStruct> allArmyCDTime = this.allArmyCDTime;
			if (allArmyCDTime.Count > 0)
			{
				this.updateEnd.SetActive(true);
			}
			else
			{
				this.updateEnd.SetActive(false);
			}
		}
	}

	private void HideRightTrans()
	{
		this.updateBtn.SetActive(true);
	}

	public override void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
		base.OnDisable();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyNewControlpanel_Close, new EventManager.VoidDelegate(this.Close));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyNewControlpanel_Update, new EventManager.VoidDelegate(this.UpdateArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyNewControlpanel_UpdateEnd, new EventManager.VoidDelegate(this.UpdateArmyEnd));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyNewControlpanel_MoveLeft, new EventManager.VoidDelegate(this.MoveLeft));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyNewControlpanel_MoveRight, new EventManager.VoidDelegate(this.MoveRight));
	}

	public void MoveLeft(GameObject ga)
	{
		this.centerGridOnChild.CenterOn(this.AllSolider[this.AllSolider[this.selectArmyIndex].preItemid].transform);
	}

	public void MoveRight(GameObject ga)
	{
		this.centerGridOnChild.CenterOn(this.AllSolider[this.AllSolider[this.selectArmyIndex].nextItemId].transform);
	}

	private void Close(GameObject ga)
	{
		FuncUIManager.inst.DestoryFuncUI("NewArmyControlPanel");
	}

	private void UpdateArmy(GameObject ga)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		this.protect_time = 1f;
		this.updateArmy = ga.GetComponent<ButtonClick>();
		this.updateArmy.isSendLua = false;
		CalcMoneyHandler.CSCalcMoney(4, 0, this.selectArmyIndex, (long)this.selectArmyIndex, this.selectArmyIndex, 0, new Action<bool, int>(this.CalcMoneyUpdateCallBack));
	}

	private void UpdateArmyEnd(GameObject ga)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		this.protect_time = 1f;
		this.updateArmy = ga.GetComponent<ButtonClick>();
		this.updateArmy.isSendLua = false;
		CalcMoneyHandler.CSCalcMoney(1, 2, this.selectArmyIndex, (long)this.selectArmyIndex, this.selectArmyIndex, 0, new Action<bool, int>(this.CalcMoneyEndUpdateCallBack));
	}

	private void CalcMoneyUpdateCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.ShowBuyMoney();
				return;
			}
			this.updateArmy.IsCanDoEvent = false;
			ArmyHandler.CG_ArmsUpdateLevel(this.selectArmyIndex, money, delegate
			{
				this.updateArmy.IsCanDoEvent = true;
				this.showLight.gameObject.SetActive(true);
				this.showLight.transform.localScale = Vector3.one;
				if (this.shengji == null)
				{
					this.shengji = PoolManage.Ins.CreatEffect("shengji_glow", Vector3.zero, Quaternion.identity, base.transform);
					this.shengji.transform.parent = this.showLight;
					this.shengji.transform.localPosition = Vector3.zero;
					this.shengji.IsAutoDes = false;
					this.shengji.LifeTime = 0f;
					Transform[] componentsInChildren = this.shengji.GetComponentsInChildren<Transform>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].gameObject.layer = 31;
					}
				}
				else
				{
					this.shengji.ga.SetActive(true);
				}
				CoroutineInstance.DoJob(this.ShowKuang(2f));
				this.ShowArmyUpdateInfo();
				HUDTextTool.inst.NextLuaCall("兵种升级回调· ·", new object[]
				{
					true
				});
			});
		}
	}

	[DebuggerHidden]
	public IEnumerator ShowKuang(float seconds)
	{
		ArmyControlAndUpdatePanel.<ShowKuang>c__Iterator8F <ShowKuang>c__Iterator8F = new ArmyControlAndUpdatePanel.<ShowKuang>c__Iterator8F();
		<ShowKuang>c__Iterator8F.seconds = seconds;
		<ShowKuang>c__Iterator8F.<$>seconds = seconds;
		<ShowKuang>c__Iterator8F.<>f__this = this;
		return <ShowKuang>c__Iterator8F;
	}

	private void CalcMoneyEndUpdateCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.SetText("钻石不足", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			this.updateArmy.IsCanDoEvent = false;
			int[] array = new int[this.allArmyCDTime.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)this.allArmyCDTime[i].key;
			}
			ArmyHandler.CG_CSArmyLevelUpEnd(1, delegate(int[] iis)
			{
				this.updateArmy.IsCanDoEvent = true;
				AudioManage.inst.PlayAuido("upgrade", false);
				CoroutineInstance.DoJob(this.UpdateArmys(iis));
				this.ShowArmyUpdateInfo();
				HUDTextTool.inst.NextLuaCall("兵种升级购买冷却回调· ·", new object[]
				{
					true
				});
			}, array);
		}
	}

	[DebuggerHidden]
	private IEnumerator UpdateArmys(int[] itemIDs)
	{
		ArmyControlAndUpdatePanel.<UpdateArmys>c__Iterator90 <UpdateArmys>c__Iterator = new ArmyControlAndUpdatePanel.<UpdateArmys>c__Iterator90();
		<UpdateArmys>c__Iterator.itemIDs = itemIDs;
		<UpdateArmys>c__Iterator.<$>itemIDs = itemIDs;
		return <UpdateArmys>c__Iterator;
	}

	public void ShowRestriantMethod()
	{
		if (ArmyControlAndUpdatePanel.isFeijiChang)
		{
			this.showOthers.text = "/" + LanguageManage.GetTextByKey("架", "Army");
		}
		else
		{
			this.showOthers.text = "/" + LanguageManage.GetTextByKey("辆", "Army");
		}
	}

	[DebuggerHidden]
	private IEnumerator ShowArmy()
	{
		ArmyControlAndUpdatePanel.<ShowArmy>c__Iterator91 <ShowArmy>c__Iterator = new ArmyControlAndUpdatePanel.<ShowArmy>c__Iterator91();
		<ShowArmy>c__Iterator.<>f__this = this;
		return <ShowArmy>c__Iterator;
	}

	private void CenterChildCallBack(GameObject ga)
	{
		this.selectArmyIndex = ga.GetComponent<ArmyControlAndUpdateCenterItem>().itemID;
		this.ShowArmyUpdateInfo();
		if (ga.GetComponent<ArmyControlAndUpdateCenterItem>().taizi)
		{
			this.centerGa_Select = ga.GetComponent<ArmyControlAndUpdateCenterItem>().taizi.ga;
		}
	}

	public string GetTextTips(T_InfoTextType tipsType, NewUnLvInfo Info)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string value = string.Empty;
		string value2 = string.Empty;
		switch (tipsType)
		{
		case T_InfoTextType.生命值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("生命值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, Info));
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克生命值);
			}
			else
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机生命值);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(Info.fightInfo.life);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种生命值);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.攻击值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("攻击值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, Info));
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克攻击);
			}
			else
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机攻击);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(Info.fightInfo.breakArmor);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种攻击力);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.防御值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("防御值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, Info));
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克防御力);
			}
			else
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机防御力);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(Info.fightInfo.defBreak);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		}
		return stringBuilder.ToString();
	}

	public int GetAllData(T_InfoTextType type, NewUnLvInfo Info)
	{
		int num = 0;
		switch (type)
		{
		case T_InfoTextType.生命值:
			num = Info.fightInfo.life;
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				num += Technology.GetTechAddtion(Info.fightInfo.life, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克生命值);
			}
			else
			{
				num += Technology.GetTechAddtion(Info.fightInfo.life, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机生命值);
			}
			num += VipConst.GetVipAddtion((float)Info.fightInfo.life, HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种生命值);
			break;
		case T_InfoTextType.攻击值:
			num = Info.fightInfo.breakArmor;
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				num += Technology.GetTechAddtion(Info.fightInfo.breakArmor, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克攻击);
			}
			else
			{
				num += Technology.GetTechAddtion(Info.fightInfo.breakArmor, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机攻击);
			}
			num += VipConst.GetVipAddtion((float)Info.fightInfo.breakArmor, HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种攻击力);
			break;
		case T_InfoTextType.防御值:
			num = Info.fightInfo.defBreak;
			if (!ArmyControlAndUpdatePanel.isFeijiChang)
			{
				num += Technology.GetTechAddtion(Info.fightInfo.defBreak, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.坦克防御力);
			}
			else
			{
				num += Technology.GetTechAddtion(Info.fightInfo.defBreak, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.飞机防御力);
			}
			break;
		}
		return num;
	}

	public void ShowArmyUpdateInfo()
	{
		NewUnInfo newUnInfo = UnitConst.GetInstance().soldierConst[this.selectArmyIndex];
		this.armyName.text = LanguageManage.GetTextByKey(newUnInfo.name, "Army");
		if (ArmyControlAndUpdatePanel.isFeijiChang)
		{
			this.leftSprite.width = 70;
			this.leftSprite.height = 70;
			this.leftSprite.spriteName = UnitConst.GetInstance().soldierConst[this.selectArmyIndex].icon;
			this.leftName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[this.selectArmyIndex].name, "Army");
		}
		else
		{
			this.leftSprite.width = 100;
			this.leftSprite.height = 100;
			this.leftSprite.spriteName = UnitConst.GetInstance().soldierConst[this.selectArmyIndex].icon;
			this.leftName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[this.selectArmyIndex].name, "Army");
		}
		this.rightSprite.spriteName = UnitConst.GetInstance().buildingConst[UnitConst.GetInstance().soldierConst[this.selectArmyIndex].restraint].bigIcon;
		this.rightName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[UnitConst.GetInstance().soldierConst[this.selectArmyIndex].restraint].name, "build");
		if (this.AllSolider[this.selectArmyIndex].preItemid == 0)
		{
			this.moveLeftBtn.SetActive(false);
		}
		else
		{
			this.moveLeftBtn.SetActive(true);
		}
		if (this.AllSolider[this.selectArmyIndex].nextItemId == 0)
		{
			this.moveRightBtn.SetActive(false);
		}
		else
		{
			this.moveRightBtn.SetActive(true);
		}
		if ((ArmyControlAndUpdatePanel.isFeijiChang && UnitConst.GetInstance().OtherBuildingOpenSetDataConst[new Vector2((float)ArmyControlAndUpdatePanel.index, (float)ArmyControlAndUpdatePanel.lv)].armsIds.Contains(newUnInfo.unitId)) || (!ArmyControlAndUpdatePanel.isFeijiChang && HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(13) && UnitConst.GetInstance().OtherBuildingOpenSetDataConst[new Vector2(13f, (float)((!HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(13)) ? 0 : HeroInfo.GetInstance().PlayerBuildingLevel[13]))].armsIds.Contains(newUnInfo.unitId)))
		{
			List<KVStruct> allArmyCDTime = this.allArmyCDTime;
			ArmyInfo armyInfo = HeroInfo.GetInstance().PlayerArmyData[newUnInfo.unitId];
			int level = armyInfo.level;
			int num = level;
			this.updateArmyLV.gameObject.SetActive(false);
			if (this.updateArmyLV.gameObject.activeSelf)
			{
				this.curArmyLV.transform.localPosition = new Vector3(-21f, 230f, 0f);
				this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
			}
			else
			{
				this.curArmyLV.transform.localPosition = new Vector3(28f, 230f, 0f);
				this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
			}
			this.curArmyLV.text = string.Format("LV.{0}", level);
			if (allArmyCDTime.Count > 0)
			{
				for (int i = 0; i < allArmyCDTime.Count; i++)
				{
					if (allArmyCDTime[i].key == (long)newUnInfo.unitId)
					{
						this.updateArmyLV.gameObject.SetActive(true);
						num++;
						if (this.updateArmyLV.gameObject.activeSelf)
						{
							this.curArmyLV.transform.localPosition = new Vector3(-21f, 230f, 0f);
							this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
						}
						else
						{
							this.curArmyLV.transform.localPosition = new Vector3(28f, 230f, 0f);
							this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
						}
					}
				}
				this.updateEnd.SetActive(true);
			}
			else
			{
				this.updateEnd.SetActive(false);
			}
			this.updateArmyLV.text = string.Format("LV.{0}", num);
			NewUnLvInfo newUnLvInfo = newUnInfo.lvInfos[level];
			NewUnLvInfo newUnLvInfo2 = newUnInfo.lvInfos[num];
			if (newUnInfo.lvInfos.Count <= num + 1)
			{
				this.life_att_def_exp[0].SetTechUpdateResText(this.GetAllData(T_InfoTextType.生命值, newUnLvInfo), 0, string.Empty);
				this.life_att_def_exp[1].SetTechUpdateResText(this.GetAllData(T_InfoTextType.攻击值, newUnLvInfo), 0, string.Empty);
				this.life_att_def_exp[2].SetTechUpdateResText(this.GetAllData(T_InfoTextType.防御值, newUnLvInfo), 0, string.Empty);
				this.life_att_def_exp[3].gameObject.SetActive(false);
				resTextInfo component = this.life_att_def_exp[0].GetComponent<resTextInfo>();
				component.curT_InfoTextType = T_InfoTextType.生命值;
				component.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.生命值, newUnLvInfo).ToString(), 0, this.GetTextTips(T_InfoTextType.生命值, newUnLvInfo));
				resTextInfo component2 = this.life_att_def_exp[1].GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.攻击值;
				component2.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.攻击值, newUnLvInfo).ToString(), 0, this.GetTextTips(T_InfoTextType.攻击值, newUnLvInfo));
				resTextInfo component3 = this.life_att_def_exp[2].GetComponent<resTextInfo>();
				component3.curT_InfoTextType = T_InfoTextType.防御值;
				component3.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.防御值, newUnLvInfo).ToString(), 0, this.GetTextTips(T_InfoTextType.防御值, newUnLvInfo));
				this.time.SetActive(false);
				this.costMoney.text = newUnLvInfo.BuyCost.ToString();
				this.weijiesuo.SetActive(false);
				this.updateBtn.SetActive(false);
				this.manji.gameObject.SetActive(true);
				this.tiaoJian.text = string.Empty;
			}
			else
			{
				NewUnLvInfo newUnLvInfo3 = newUnInfo.lvInfos[num + 1];
				this.life_att_def_exp[0].SetTechUpdateResText(this.GetAllData(T_InfoTextType.生命值, newUnLvInfo), newUnLvInfo3.fightInfo.life - newUnLvInfo.fightInfo.life, string.Empty);
				this.life_att_def_exp[1].SetTechUpdateResText(this.GetAllData(T_InfoTextType.攻击值, newUnLvInfo), newUnLvInfo3.fightInfo.breakArmor - newUnLvInfo.fightInfo.breakArmor, string.Empty);
				this.life_att_def_exp[2].SetTechUpdateResText(this.GetAllData(T_InfoTextType.防御值, newUnLvInfo), newUnLvInfo3.fightInfo.defBreak - newUnLvInfo.fightInfo.defBreak, string.Empty);
				this.life_att_def_exp[3].SetTechUpdateResText(newUnLvInfo.playerExp, 0, string.Empty);
				this.life_att_def_exp[3].gameObject.SetActive(true);
				resTextInfo component4 = this.life_att_def_exp[0].GetComponent<resTextInfo>();
				component4.curT_InfoTextType = T_InfoTextType.生命值;
				component4.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.生命值, newUnLvInfo).ToString(), newUnLvInfo3.fightInfo.life - newUnLvInfo.fightInfo.life, this.GetTextTips(T_InfoTextType.生命值, newUnLvInfo));
				resTextInfo component5 = this.life_att_def_exp[1].GetComponent<resTextInfo>();
				component5.curT_InfoTextType = T_InfoTextType.攻击值;
				component5.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.攻击值, newUnLvInfo).ToString(), newUnLvInfo3.fightInfo.breakArmor - newUnLvInfo.fightInfo.breakArmor, this.GetTextTips(T_InfoTextType.攻击值, newUnLvInfo));
				resTextInfo component6 = this.life_att_def_exp[2].GetComponent<resTextInfo>();
				component6.curT_InfoTextType = T_InfoTextType.防御值;
				component6.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.防御值, newUnLvInfo).ToString(), newUnLvInfo3.fightInfo.defBreak - newUnLvInfo.fightInfo.defBreak, this.GetTextTips(T_InfoTextType.防御值, newUnLvInfo));
				this.time.SetActive(true);
				this.updateTime.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertSecondDateTime(newUnLvInfo.cdTime)).ToString();
				this.updateCostMoney.text = ((newUnLvInfo2.resCost[ResType.金币] <= HeroInfo.GetInstance().playerRes.resCoin) ? string.Empty : "[ff0000]") + newUnLvInfo2.resCost[ResType.金币];
				this.costMoney.text = newUnLvInfo.BuyCost.ToString();
				this.manji.gameObject.SetActive(false);
				if (allArmyCDTime.Count > 0)
				{
					this.updateBtnGa.SetActive(true);
					this.updateTime.gameObject.SetActive(false);
					this.updatingTime.SetActive(true);
				}
				else
				{
					this.updateBtnGa.SetActive(true);
					this.updatingTime.SetActive(false);
					this.updateTime.gameObject.SetActive(true);
				}
				if (num + 1 > HeroInfo.GetInstance().playerlevel)
				{
					this.tiaoJian.text = string.Format("{0}", LanguageManage.GetTextByKey("不能超过玩家等级！", "Army"));
					this.updateBtn.SetActive(false);
				}
				else
				{
					this.tiaoJian.text = string.Empty;
					this.updateBtn.SetActive(true);
				}
				this.weijiesuo.SetActive(false);
			}
		}
		else
		{
			this.curArmyLV.text = "LV.1";
			this.updateArmyLV.gameObject.SetActive(false);
			if (this.updateArmyLV.gameObject.activeSelf)
			{
				this.curArmyLV.transform.localPosition = new Vector3(-21f, 230f, 0f);
				this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
			}
			else
			{
				this.curArmyLV.transform.localPosition = new Vector3(28f, 230f, 0f);
				this.updateArmyLV.transform.localPosition = new Vector3(14f, 230f, 0f);
			}
			NewUnLvInfo newUnLvInfo4 = newUnInfo.lvInfos[1];
			NewUnLvInfo newUnLvInfo5 = newUnInfo.lvInfos[2];
			this.life_att_def_exp[0].SetTechUpdateResText(newUnLvInfo4.fightInfo.life, newUnLvInfo5.fightInfo.life - newUnLvInfo4.fightInfo.life, string.Empty);
			this.life_att_def_exp[1].SetTechUpdateResText(newUnLvInfo4.fightInfo.breakArmor, newUnLvInfo5.fightInfo.breakArmor - newUnLvInfo4.fightInfo.breakArmor, string.Empty);
			this.life_att_def_exp[2].SetTechUpdateResText(newUnLvInfo4.fightInfo.defBreak, newUnLvInfo5.fightInfo.defBreak - newUnLvInfo4.fightInfo.defBreak, string.Empty);
			this.life_att_def_exp[3].SetTechUpdateResText(newUnLvInfo4.playerExp, 0, string.Empty);
			this.life_att_def_exp[3].gameObject.SetActive(true);
			this.time.SetActive(true);
			this.updateTime.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertSecondDateTime(newUnLvInfo4.cdTime)).ToString();
			this.updateCostMoney.text = ((newUnLvInfo4.resCost[ResType.金币] <= HeroInfo.GetInstance().playerRes.resCoin) ? string.Empty : "[ff0000]") + newUnLvInfo4.resCost[ResType.金币];
			this.costMoney.text = newUnLvInfo4.BuyCost.ToString();
			this.manji.gameObject.SetActive(false);
			this.updateBtn.SetActive(false);
			this.tiaoJian.text = string.Empty;
			this.weijiesuo.SetActive(true);
			foreach (KeyValuePair<Vector2, BuildingUpOpenSet> current in UnitConst.GetInstance().OtherBuildingOpenSetDataConst)
			{
				if (current.Value.armsIds.Contains(newUnInfo.unitId))
				{
					if (ArmyControlAndUpdatePanel.isFeijiChang)
					{
						this.weijiesuo.GetComponent<UILabel>().text = string.Concat(new object[]
						{
							LanguageManage.GetTextByKey("飞机场", "Army"),
							current.Key.y,
							LanguageManage.GetTextByKey("级开放", "Army"),
							"!"
						});
					}
					else if (newUnLvInfo4.lv > 0 && this.selectArmyIndex != 1)
					{
						this.weijiesuo.GetComponent<UILabel>().text = string.Concat(new object[]
						{
							LanguageManage.GetTextByKey("兵工厂", "Army"),
							current.Key.y,
							LanguageManage.GetTextByKey("级开放", "Army"),
							"!"
						});
					}
					else
					{
						this.weijiesuo.SetActive(false);
					}
					break;
				}
			}
		}
	}

	private void Update()
	{
		if (this.protect_time > 0f)
		{
			this.protect_time -= Time.deltaTime;
		}
		if (this.rightTranform.activeSelf && this.updatingTime.activeSelf && this.allArmyCDTime.Count > 0)
		{
			this.endTime = TimeTools.ConvertLongDateTime(this.allArmyCDTime[this.allArmyCDTime.Count - 1].value);
			this.updatingTimeSprite.fillAmount = (float)((this.endTime - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds / (double)GameConst.ArmyUpdateCDTime);
			this.updatingTimeUIlabel.text = string.Format("{0}/{1}", TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime), TimeTools.ConvertFloatToTimeBySecond((float)GameConst.ArmyUpdateCDTime));
			int rmbNum = ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime));
			this.updateEndMoney.text = string.Format("[{0}]{1}", (rmbNum <= HeroInfo.GetInstance().playerRes.RMBCoin) ? "ffffff" : "ff0000", rmbNum);
		}
		if (this.armyFuncTimeGa.activeSelf)
		{
			this.bottomGrid.transform.localPosition = new Vector3(0f, 36f, 0f);
		}
		else
		{
			this.bottomGrid.transform.localPosition = new Vector3(0f, -50f, 0f);
		}
	}
}
