using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyFuncItemUI : MonoBehaviour
{
	public GameObject sumGa;

	public UILabel numUILabel;

	public UILabel nameUILabel;

	public UILabel moneyUILabel;

	public UISprite black;

	public UISprite armyIcon;

	public UISprite BJ;

	public UISprite Lock;

	public bool isUnLock;

	private KVStruct armyFuncing;

	private bool isFixedUpdate;

	private int towerLV;

	private int towerIndex;

	private long towerId;

	private NewUnInfo armyInfo_Planner;

	private Soldier soliderInfo_Planner;

	private bool isPressing;

	private int longPressAddNum;

	private float pressTime;

	public Soldier SoliderInfo_Planner
	{
		get
		{
			return this.soliderInfo_Planner;
		}
	}

	public NewUnInfo ArmyInfo_Planner
	{
		get
		{
			return this.armyInfo_Planner;
		}
	}

	private armyInfoInBuilding armyInfo_Building
	{
		get
		{
			if (HeroInfo.GetInstance().AllArmyInfo.ContainsKey(this.towerId))
			{
				return HeroInfo.GetInstance().AllArmyInfo[this.towerId];
			}
			return null;
		}
	}

	public void Update()
	{
		if (this.isPressing && Time.time > this.pressTime + 0.5f)
		{
			this.pressTime = Time.time;
			this.LongPress();
		}
	}

	private void LongPress()
	{
		if (this.ArmyInfo_Planner != null)
		{
			if (this.ArmyInfo_Planner.peopleNum * (this.longPressAddNum + 1) + T_CommandPanelManage._instance.armyFuncPanel.armyFuncedPeopleNum <= T_CommandPanelManage._instance.armyFuncPanel.buildingPeopleNum)
			{
				this.longPressAddNum++;
				HUDTextTool.inst.SetText(string.Format("{1}+{0}", this.longPressAddNum, LanguageManage.GetTextByKey("配兵", "Army")), HUDTextTool.TextUITypeEnum.Num1);
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已配兵达到数量上限", "Army"), HUDTextTool.TextUITypeEnum.Num1);
			}
		}
		if (this.SoliderInfo_Planner != null)
		{
			if (T_CommandPanelManage._instance.armyFuncPanel.armyFuncedPeopleNum + this.longPressAddNum < 1)
			{
				this.longPressAddNum++;
				HUDTextTool.inst.SetText(string.Format("{1}+{0}", this.longPressAddNum, LanguageManage.GetTextByKey("配兵", "Army")), HUDTextTool.TextUITypeEnum.Num1);
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已配兵达到数量上限", "Army"), HUDTextTool.TextUITypeEnum.Num1);
			}
		}
	}

	private void Start()
	{
	}

	public void RrefshData()
	{
		if (this.isUnLock)
		{
			if (this.SoliderInfo_Planner != null)
			{
				if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd != null && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId == this.SoliderInfo_Planner.id && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime > 0L)
				{
					this.isFixedUpdate = true;
					this.sumGa.SetActive(true);
					this.numUILabel.text = "1";
					this.armyIcon.ShaderToNormal();
					this.BJ.ShaderToNormal();
					this.Lock.gameObject.SetActive(false);
				}
				else
				{
					this.isFixedUpdate = false;
					this.sumGa.SetActive(false);
					this.numUILabel.text = string.Empty;
					this.black.fillAmount = 0f;
					this.armyIcon.ShaderToNormal();
					this.BJ.ShaderToNormal();
					this.Lock.gameObject.SetActive(false);
				}
			}
			if (this.armyInfo_Planner != null)
			{
				if (this.armyInfo_Building != null && this.armyInfo_Building.armyFuncing.Count((KVStruct a) => a.value == (long)this.armyInfo_Planner.unitId) > 0)
				{
					this.armyFuncing = this.armyInfo_Building.GetArmyNearestDataFuncing();
					if (this.armyFuncing != null && this.armyFuncing.value == (long)this.armyInfo_Planner.unitId)
					{
						this.isFixedUpdate = true;
						this.numUILabel.text = this.armyInfo_Building.armyFuncing.Count((KVStruct a) => a.value == (long)this.armyInfo_Planner.unitId).ToString();
						this.sumGa.SetActive(true);
						this.armyIcon.ShaderToNormal();
						this.BJ.ShaderToNormal();
						this.Lock.gameObject.SetActive(false);
					}
					else
					{
						this.isFixedUpdate = false;
						this.sumGa.SetActive(true);
						this.numUILabel.text = this.armyInfo_Building.armyFuncing.Count((KVStruct a) => a.value == (long)this.armyInfo_Planner.unitId).ToString();
						this.black.fillAmount = 1f;
						this.armyIcon.ShaderToNormal();
						this.BJ.ShaderToNormal();
						this.Lock.gameObject.SetActive(false);
					}
				}
				else
				{
					this.armyFuncing = null;
					this.isFixedUpdate = false;
					this.sumGa.SetActive(false);
					this.numUILabel.text = string.Empty;
					this.black.fillAmount = 0f;
					this.armyIcon.ShaderToNormal();
					this.BJ.ShaderToNormal();
					this.Lock.gameObject.SetActive(false);
				}
			}
		}
	}

	public void FixedUpdate()
	{
		if (this.isFixedUpdate)
		{
			if (this.armyFuncing != null)
			{
				if (TimeTools.GetNowTimeSyncServerToDateTime() > TimeTools.ConvertLongDateTime(this.armyFuncing.key))
				{
					this.black.fillAmount = 0f;
				}
				else
				{
					float fillAmount = (float)(TimeTools.ConvertLongDateTime(this.armyFuncing.key) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds / this.armyInfo_Planner.funcTime;
					this.black.fillAmount = fillAmount;
				}
			}
			if (this.SoliderInfo_Planner != null)
			{
				if (TimeTools.GetNowTimeSyncServerToDateTime() > TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime))
				{
					this.black.fillAmount = 0f;
				}
				else
				{
					float fillAmount2 = (float)(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds / (float)UnitConst.GetInstance().soldierExpSetConst[HeroInfo.GetInstance().PlayerCommandoes[this.SoliderInfo_Planner.id].level].reliveTime[HeroInfo.GetInstance().PlayerCommandoes[this.SoliderInfo_Planner.id].star];
					this.black.fillAmount = fillAmount2;
				}
			}
		}
	}

	public void InitData(int _towerLV, int _towerIndex, long _towerId, NewUnInfo armyInfo)
	{
		this.towerLV = _towerLV;
		this.towerIndex = _towerIndex;
		this.towerId = _towerId;
		this.armyInfo_Planner = armyInfo;
		List<int> armsIds = UnitConst.GetInstance().OtherBuildingOpenSetDataConst[new Vector2((float)this.towerIndex, (float)this.towerLV)].armsIds;
		this.nameUILabel.text = LanguageManage.GetTextByKey(this.armyInfo_Planner.name, "Army");
		AtlasManage.SetArmyIconSpritName(this.armyIcon, this.armyInfo_Planner.unitId);
		this.moneyUILabel.text = string.Format("{0} /{1}", this.armyInfo_Planner.lvInfos[HeroInfo.GetInstance().GetArmyLevelByID(this.armyInfo_Planner.unitId)].BuyCost, LanguageManage.GetTextByKey("辆", "Army"));
		if (armsIds.Contains(this.armyInfo_Planner.unitId))
		{
			this.isUnLock = true;
			this.RrefshData();
		}
		else
		{
			this.isUnLock = false;
			this.sumGa.SetActive(false);
			this.numUILabel.text = string.Empty;
			this.black.fillAmount = 0f;
			this.armyIcon.ShaderToGray();
			this.BJ.ShaderToGray();
			this.Lock.gameObject.SetActive(true);
		}
	}

	public void InitData(int _towerLV, int _towerIndex, long _towerId, Soldier armyInfo)
	{
		this.towerLV = _towerLV;
		this.towerIndex = _towerIndex;
		this.towerId = _towerId;
		this.soliderInfo_Planner = armyInfo;
		this.nameUILabel.text = LanguageManage.GetTextByKey(this.soliderInfo_Planner.name, "soldier");
		AtlasManage.SetSpritName(this.armyIcon, this.soliderInfo_Planner.icon);
		if (HeroInfo.GetInstance().PlayerCommandoes.ContainsKey(this.soliderInfo_Planner.id))
		{
			this.isUnLock = true;
			this.moneyUILabel.text = string.Format("{0}  ", UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)this.soliderInfo_Planner.id, (float)HeroInfo.GetInstance().PlayerCommandoes[this.soliderInfo_Planner.id].star)].FuncMoney);
			this.RrefshData();
		}
		else
		{
			this.isUnLock = false;
			this.moneyUILabel.text = string.Format("{0}  ", UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)this.soliderInfo_Planner.id, 0f)].FuncMoney);
			this.sumGa.SetActive(false);
			this.numUILabel.text = string.Empty;
			this.black.fillAmount = 0f;
			this.armyIcon.ShaderToGray();
			this.BJ.ShaderToGray();
			this.Lock.gameObject.SetActive(true);
		}
	}

	private void OnHover(bool isHover)
	{
	}

	private void OnPress(bool isPress)
	{
		this.isPressing = isPress;
		if (this.isPressing)
		{
			this.pressTime = Time.time;
			this.longPressAddNum = 0;
		}
	}

	public void ArmyFuncPanel_ArmyFunc()
	{
		int funcArmyNum = 1;
		if (this.longPressAddNum > 0)
		{
			funcArmyNum = this.longPressAddNum;
			this.longPressAddNum = 0;
		}
		if (this.armyInfo_Planner != null)
		{
			CalcMoneyHandler.CSCalcMoney(9, 0, 0, this.towerId, this.armyInfo_Planner.unitId, 0, funcArmyNum, delegate(bool isBuy, int money)
			{
				if (isBuy)
				{
					ArmyFuncHandler.CG_ArmsConfigure(this.towerId, this.armyInfo_Planner.unitId, funcArmyNum, delegate
					{
						HUDTextTool.inst.NextLuaCall("配兵完成", new object[0]);
					});
				}
			});
		}
		if (this.SoliderInfo_Planner != null)
		{
			CalcMoneyHandler.CSCalcMoney(12, 0, 0, HeroInfo.GetInstance().PlayerCommandoes[this.SoliderInfo_Planner.id].id, this.SoliderInfo_Planner.id, 0, 1, delegate(bool isBuy, int money)
			{
				if (isBuy)
				{
					ArmyFuncHandler.CG_SoliderConfigure(this.SoliderInfo_Planner.id, delegate
					{
						HUDTextTool.inst.NextLuaCall("配兵完成", new object[0]);
					});
				}
			});
		}
	}

	public void ArmyFuncPanel_ArmyCancle()
	{
		if (this.armyInfo_Planner != null)
		{
			ArmyFuncHandler.CG_CancelConfigureArmy(this.towerId, this.armyInfo_Planner.unitId, delegate
			{
				HUDTextTool.inst.NextLuaCall("取消配兵", new object[0]);
			});
		}
		if (this.SoliderInfo_Planner != null)
		{
			ArmyFuncHandler.CG_CancelConfigureSolider(this.SoliderInfo_Planner.id, delegate
			{
				HUDTextTool.inst.NextLuaCall("取消配兵", new object[0]);
			});
		}
	}
}
