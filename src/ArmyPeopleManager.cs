using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyPeopleManager : FuncUIPanel
{
	public static ArmyPeopleManager ins;

	public UIScrollView memberScroll;

	public UIScrollView submitScroll;

	public UITable memeberTabel;

	public UITable submitTabel;

	public ArmyMemberPanel armyMemberPanel;

	public GameObject armyMemberPrefab;

	public Dictionary<int, ArmyMemberPre> armyMemberDic = new Dictionary<int, ArmyMemberPre>();

	public Dictionary<int, ArmySubmitPrefab> amrySubmit = new Dictionary<int, ArmySubmitPrefab>();

	public ShowArmyMemberSure showArmyMember;

	public GameObject btnArmySubmit;

	public GameObject btnExitArmy;

	public GameObject btnDissloveArmy;

	public GameObject btnClearSubmit;

	public GameObject btnChnageLegionIcon;

	public GameObject btnEditLegionTip;

	public GameObject btnChangeLegionOpenType;

	public UILabel legionName;

	public UILabel legionLevel;

	public UILabel legionNum;

	public UILabel legionRank;

	public UILabel legionMedla;

	public UILabel legionHead;

	public UILabel legionCon;

	public UILabel legionTip;

	public ArmySubmitPanel submitPanel;

	public GameObject submitPrefab;

	public UISprite legionIcon;

	public int Deputyleader;

	public ChangeArmyIconPanel changArmyIconPanel;

	public EditLegionTipPanel editLegionTipPanel;

	public ChangeOpenTypePanel changeOpenTypePanel;

	public List<GameObject> memberGa = new List<GameObject>();

	public List<GameObject> subGAa = new List<GameObject>();

	private int count;

	public bool isReset;

	public new void Awake()
	{
		ArmyPeopleManager.ins = this;
	}

	public new void OnEnable()
	{
		this.Init();
		this.memeberTabel.Reposition();
		this.memberScroll.ResetPosition();
		this.HideBtns();
		this.ShowLegionMemberRight();
		this.ShowLegionDetialInfo();
		this.ChangePanel(1);
		this.ShowArmyMemberInfo();
		NetMgr.inst.NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		this.FindCount();
		base.OnEnable();
	}

	private void FindCount()
	{
		foreach (KeyValuePair<int, ArmyRight> current in UnitConst.GetInstance().armyRight)
		{
			if (current.Key == 2)
			{
				this.count = current.Value.num;
			}
		}
	}

	private void ShowLegionDetialInfo()
	{
		foreach (KeyValuePair<int, ArmyGroupInfo> current in HeroInfo.GetInstance().armyGroupDataData.armyInfo)
		{
			if ((long)current.Key == HeroInfo.GetInstance().playerGroupId)
			{
				this.legionNum.text = string.Concat(new object[]
				{
					LanguageManage.GetTextByKey("军团人数:", "Battle"),
					current.Value.memeberCount,
					"/",
					current.Value.memberLimit
				});
				this.legionRank.text = LanguageManage.GetTextByKey("军团等级:", "Battle") + current.Value.rank.ToString();
				this.legionMedla.text = LanguageManage.GetTextByKey("奖牌数:", "Battle") + current.Value.medal.ToString();
				this.legionIcon.spriteName = current.Value.logoId.ToString();
				this.legionLevel.text = LanguageManage.GetTextByKey("军团等级:", "Battle") + "Lv." + current.Value.level;
				this.legionName.text = current.Value.armyName;
				if (!string.IsNullOrEmpty(current.Value.notic))
				{
					this.legionTip.text = current.Value.notic;
				}
				else
				{
					this.legionTip.text = LanguageManage.GetTextByKey("还未编辑公告", "Battle");
				}
			}
		}
		foreach (KeyValuePair<int, ArmyPeopleInfo> current2 in HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo)
		{
			if (current2.Value.job == 1)
			{
				this.legionHead.text = LanguageManage.GetTextByKey("军团长:", "Battle") + current2.Value.name;
			}
			if ((long)current2.Value.playerId == HeroInfo.GetInstance().userId)
			{
				this.legionCon.text = LanguageManage.GetTextByKey("我的贡献:", "Battle") + current2.Value.contriBution.ToString();
			}
		}
	}

	private void HideBtns()
	{
		this.btnEditLegionTip.SetActive(false);
		this.btnChnageLegionIcon.SetActive(false);
		this.btnDissloveArmy.SetActive(false);
		this.btnExitArmy.SetActive(false);
		this.btnArmySubmit.SetActive(false);
		this.btnChangeLegionOpenType.SetActive(false);
	}

	private void ShowLegionMemberRight()
	{
		foreach (KeyValuePair<int, ArmyRight> current in UnitConst.GetInstance().armyRight)
		{
			foreach (KeyValuePair<int, ArmyPeopleInfo> current2 in from a in HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo
			where (long)a.Value.playerId == HeroInfo.GetInstance().userId
			select a)
			{
				if (current.Value.id == current2.Value.job)
				{
					foreach (KeyValuePair<int, int> current3 in current.Value.right)
					{
						switch (current3.Key)
						{
						case 3:
							this.btnEditLegionTip.SetActive(true);
							break;
						case 4:
							this.btnChnageLegionIcon.SetActive(true);
							break;
						case 5:
							this.btnDissloveArmy.SetActive(true);
							break;
						case 6:
							this.btnArmySubmit.SetActive(true);
							break;
						case 7:
							this.btnExitArmy.SetActive(true);
							break;
						case 8:
							this.btnChangeLegionOpenType.SetActive(true);
							break;
						}
					}
				}
			}
		}
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10104)
		{
			this.ShowLegionDetialInfo();
		}
		if (opcodeCMD == 10112)
		{
			this.ShowLegionMemberRight();
			this.ShowArmyMemberInfo();
			this.ShowLegionDetialInfo();
			this.ShowSubmitInfo();
		}
		if (opcodeCMD == 10105)
		{
			this.ShowSubmitInfo();
		}
	}

	public new void OnDisable()
	{
		NetMgr.inst.NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		base.OnDisable();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnDeleltMember, new EventManager.VoidDelegate(this.BtnDelete));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnVisite, new EventManager.VoidDelegate(this.BtnVisiteMember));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnChangeJob, new EventManager.VoidDelegate(this.BtnChangeJobClick));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnArmyAward, new EventManager.VoidDelegate(this.BtnShowArmyAward));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnArmyMember, new EventManager.VoidDelegate(this.BtnShowArmyMember));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnArmyDuplicate, new EventManager.VoidDelegate(this.BtnShowArmyDiuplate));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnArmySubmit, new EventManager.VoidDelegate(this.BtnShowArmySubmint));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnDissolve, new EventManager.VoidDelegate(this.BtnDissloveArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnExitArmy, new EventManager.VoidDelegate(this.BtnExitArmy));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnAgreeSubmit, new EventManager.VoidDelegate(this.BtnAgrrSubmit));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnNoAgreeSubmit, new EventManager.VoidDelegate(this.BtnNoAgreeSubmit));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnVisitSubmit, new EventManager.VoidDelegate(this.BtnVisitSubmit));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_BtnClearSubmit, new EventManager.VoidDelegate(this.BtnClearSubmit));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_ChangeLegionIcon, new EventManager.VoidDelegate(this.ChangeLegionIconClick));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_ChangeLegionOpenType, new EventManager.VoidDelegate(this.ChangLeigonTypeClick));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_EditLegionDes, new EventManager.VoidDelegate(this.EditLegionTipClick));
	}

	private void ShowArmyMemberInfo()
	{
		this.Deputyleader = 0;
		this.memberGa.Clear();
		int num = 1;
		this.DesArmyMemberChild();
		this.memeberTabel.DestoryChildren(true);
		this.memeberTabel.Reposition();
		this.memberScroll.ResetPosition();
		foreach (ArmyPeopleInfo current in from a in HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo.Values
		orderby a.contriBution
		select a)
		{
			GameObject gameObject = NGUITools.AddChild(this.memeberTabel.gameObject, this.armyMemberPrefab);
			gameObject.name = current.playerId.ToString();
			this.memberGa.Add(gameObject);
			ArmyMemberPre component = gameObject.GetComponent<ArmyMemberPre>();
			component.btnDelete.name = current.playerId.ToString();
			component.btnVisite.name = current.playerId.ToString();
			component.btnChangeJob.name = current.playerId.ToString();
			component.legionId = current.legionId;
			component.rank.text = num.ToString();
			component.memberName.text = current.name;
			component.memberContribution.text = current.contriBution.ToString();
			component.memberMedal.text = current.selfMedal.ToString();
			component.job = current.job;
			if (current.job == 2)
			{
				this.Deputyleader++;
			}
			num++;
			if ((long)current.playerId == HeroInfo.GetInstance().userId)
			{
				component.btnDelete.SetActive(false);
				component.btnChangeJob.SetActive(false);
				component.btnVisite.SetActive(false);
				if (current.job == 1)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("军团长", "Battle");
				}
				if (current.job == 2)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("副军团长", "Battle");
				}
				if (current.job == 3)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("普通成员", "Battle");
				}
			}
			else
			{
				if (current.job == 1)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("军团长", "Battle");
					component.btnVisite.SetActive(true);
					component.btnDelete.SetActive(false);
					component.btnChangeJob.SetActive(false);
				}
				if (current.job == 2 && current.job > HeroInfo.GetInstance().playerGroupJob)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("副军团长", "Battle");
					component.btnDelete.SetActive(true);
					component.btnChangeJob.SetActive(true);
					component.btnVisite.SetActive(true);
					component.changJobText.name = "2";
					component.changJobText.text = LanguageManage.GetTextByKey("降职", "Battle");
				}
				else
				{
					if (current.job != 1 && current.job != 3)
					{
						component.memberJob.text = LanguageManage.GetTextByKey("副军团长", "Battle");
					}
					component.btnDelete.SetActive(false);
					component.btnChangeJob.SetActive(false);
					component.btnVisite.SetActive(true);
				}
				if (current.job == 3 && current.job > HeroInfo.GetInstance().playerGroupJob)
				{
					component.memberJob.text = LanguageManage.GetTextByKey("普通成员", "Battle");
					component.btnDelete.SetActive(true);
					if (HeroInfo.GetInstance().playerGroupJob == 1)
					{
						component.btnChangeJob.SetActive(true);
						component.changJobText.text = LanguageManage.GetTextByKey("升职", "Battle");
						component.changJobText.name = "1";
					}
					else
					{
						component.btnChangeJob.SetActive(false);
					}
					component.btnVisite.SetActive(true);
				}
				if (current.job == 3 && current.job == HeroInfo.GetInstance().playerGroupJob)
				{
					component.btnChangeJob.SetActive(false);
					component.btnDelete.SetActive(false);
					component.btnVisite.SetActive(true);
					if (current.job != 2 && current.job != 1)
					{
						component.memberJob.text = LanguageManage.GetTextByKey("普通成员", "Battle");
					}
				}
			}
		}
		this.memeberTabel.Reposition();
		this.memberScroll.ResetPosition();
	}

	private void ShowSubmitInfo()
	{
		this.DestorySubmit();
		this.subGAa.Clear();
		this.submitTabel.DestoryChildren(true);
		this.submitTabel.Reposition();
		this.submitScroll.ResetPosition();
		foreach (KeyValuePair<int, ArmySubmintInfo> current in HeroInfo.GetInstance().armyGroupDataData.armySubmitDic)
		{
			GameObject gameObject = NGUITools.AddChild(this.submitTabel.gameObject, this.submitPrefab);
			gameObject.name = current.Value.id.ToString();
			this.subGAa.Add(gameObject);
			ArmySubmitPrefab component = gameObject.GetComponent<ArmySubmitPrefab>();
			component.btnAgree.name = current.Value.id.ToString();
			component.btnNoAgree.name = current.Value.id.ToString();
			component.btnVisitSubmit.name = current.Value.playerId.ToString();
			component.level.text = current.Value.level.ToString();
			component.palyerName.text = current.Value.playerName.ToString();
			component.medal.text = current.Value.medal.ToString();
		}
		this.submitTabel.Reposition();
		this.submitScroll.ResetPosition();
	}

	private void BtnDelete(GameObject ga)
	{
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("你确定要删除此成员吗？", "Battle"), delegate
		{
			ArmyGroupHandler.CG_CSDisMissLegionMember(HeroInfo.GetInstance().playerGroupId, long.Parse(ga.name), delegate(bool isError)
			{
				if (!isError)
				{
					this.isReset = true;
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你已成功删除该成员", "Battle"), HUDTextTool.TextUITypeEnum.Num1);
					this.DeleteMember(int.Parse(ga.name));
				}
			});
		}, null);
	}

	private void DeleteMember(int playerId)
	{
		for (int i = 0; i < this.memberGa.Count; i++)
		{
			if (int.Parse(this.memberGa[i].name) == playerId)
			{
				UnityEngine.Object.Destroy(this.memberGa[i].gameObject);
			}
		}
		foreach (KeyValuePair<int, ArmyPeopleInfo> current in HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo)
		{
			if (current.Value.playerId == playerId)
			{
				HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo.Remove(playerId);
			}
		}
	}

	private void BtnChangeJobClick(GameObject ga)
	{
		foreach (KeyValuePair<int, ArmyRight> current in UnitConst.GetInstance().armyRight)
		{
			if (current.Key == 2)
			{
				this.count = current.Value.num;
			}
		}
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("你确定要改变该成员的职务吗？", "Battle"), delegate
		{
			if (int.Parse(ga.transform.GetChild(0).name) == 1 && this.Deputyleader >= this.count)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("副军长数目已达上线不能增加了", "Battle"), HUDTextTool.TextUITypeEnum.Num1);
				return;
			}
			ArmyGroupHandler.CG_CSLegionJobUpOrDown(HeroInfo.GetInstance().playerGroupId, long.Parse(ga.name), int.Parse(ga.transform.GetChild(0).name), delegate(bool isError)
			{
				if (!isError)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("该成员职务变更成功", "Battle"), HUDTextTool.TextUITypeEnum.Num1);
					this.ShowArmyMemberInfo();
					return;
				}
			});
		}, null);
	}

	private void BtnVisiteMember(GameObject ga)
	{
		UIManager.curState = SenceState.Visit;
		SenceInfo.CurBattle = null;
		SenceHandler.CG_GetMapData(int.Parse(ga.name), 4, 0, null);
	}

	private void BtnShowArmyAward(GameObject ga)
	{
		ArmyManager.ins.OpenLegionGiftPanel(true);
	}

	private void BtnShowArmyDiuplate(GameObject ga)
	{
		if (HeroInfo.GetInstance().playerGroupId != 0L)
		{
			ArmyGroupHandler.CG_CSLegionData(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
			{
				if (!isError)
				{
					LegionMapManager._inst.Init(LegionMapManager.BattleType.军团副本);
				}
			});
		}
	}

	private void BtnShowArmySubmint(GameObject ga)
	{
		ArmyGroupHandler.CG_CSGetLegionApply(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
		{
			if (!isError)
			{
				this.ChangePanel(2);
				this.ShowSubmitInfo();
			}
		});
	}

	private void BtnExitArmy(GameObject ga)
	{
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("退出军团后两小时内不能加入其他军团或者创建军团,且军团贡献消失。", "Battle"), delegate
		{
			ArmyGroupHandler.CG_CSLegionOut(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
			{
				LogManage.LogError("退出军团成功");
				ArmyManager.ins.ChangePanel();
			});
		}, null);
	}

	private void BtnDissloveArmy(GameObject ga)
	{
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("解散军团后两小时内不能加入其他军团或者创建军团,且军团贡献消失。", "Battle"), delegate
		{
			ArmyGroupHandler.CG_CSLegionOut(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
			{
				ArmyManager.ins.ChangePanel();
			});
		}, null);
	}

	private void BtnShowArmyMember(GameObject ga)
	{
		this.ChangePanel(1);
		this.ShowArmyMemberInfo();
		this.memeberTabel.Reposition();
		this.memberScroll.ResetPosition();
	}

	private void BtnAgrrSubmit(GameObject ga)
	{
		foreach (KeyValuePair<int, ArmyGroupInfo> current in HeroInfo.GetInstance().armyGroupDataData.armyInfo)
		{
			if (HeroInfo.GetInstance().playerGroupId == (long)current.Key)
			{
				if (current.Value.memeberCount >= current.Value.memberLimit)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团人数已达上限！", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
					break;
				}
				ArmyGroupHandler.CG_CSAgreeLegionApply(long.Parse(ga.name), delegate(bool isError)
				{
					if (!isError)
					{
						this.HideApplyInfo(int.Parse(ga.name));
					}
				});
			}
		}
	}

	private void BtnNoAgreeSubmit(GameObject ga)
	{
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("你确定要拒绝该玩家的申请吗？", "Battle"), delegate
		{
			ArmyGroupHandler.CG_CSIgnoreLegionApply(long.Parse(ga.name), delegate(bool isError)
			{
				if (!isError)
				{
					this.HideApplyInfo(int.Parse(ga.name));
				}
			});
		}, null);
	}

	private void HideApplyInfo(int id)
	{
		for (int i = 0; i < this.subGAa.Count; i++)
		{
			if (int.Parse(this.subGAa[i].name) == id)
			{
				UnityEngine.Object.Destroy(this.subGAa[i]);
			}
		}
		foreach (KeyValuePair<int, ArmySubmintInfo> current in HeroInfo.GetInstance().armyGroupDataData.armySubmitDic)
		{
			if (current.Key == id)
			{
				HeroInfo.GetInstance().armyGroupDataData.armySubmitDic.Remove(id);
			}
		}
		this.submitTabel.Reposition();
		this.submitScroll.ResetPosition();
	}

	private void BtnVisitSubmit(GameObject ga)
	{
		UIManager.curState = SenceState.Visit;
		SenceInfo.CurBattle = null;
		SenceHandler.CG_GetMapData(int.Parse(ga.name), 4, 0, null);
	}

	private void BtnClearSubmit(GameObject ga)
	{
		if (this.submitTabel.transform.childCount <= 0)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("当前没有申请", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		this.showArmyMember.gameObject.SetActive(true);
		this.showArmyMember.ShowArmyMember(LanguageManage.GetTextByKey("你确定要删除所有申请吗？", "Battle"), delegate
		{
			ArmyGroupHandler.CG_CSIgnoreAllLegionApply(1, delegate(bool isError)
			{
				if (!isError)
				{
					this.ClearAllSub();
				}
			});
		}, null);
	}

	private void ClearAllSub()
	{
		for (int i = 0; i < this.subGAa.Count; i++)
		{
			UnityEngine.Object.Destroy(this.subGAa[i]);
		}
		HeroInfo.GetInstance().armyGroupDataData.armySubmitDic.Clear();
	}

	public void ChangePanel(int id)
	{
		switch (id)
		{
		case 1:
			this.armyMemberPanel.gameObject.SetActive(true);
			this.submitPanel.gameObject.SetActive(false);
			break;
		case 2:
			this.armyMemberPanel.gameObject.SetActive(false);
			this.submitPanel.gameObject.SetActive(true);
			break;
		}
	}

	private void DesArmyMemberChild()
	{
		GameObject gameObject = this.memeberTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.memberScroll.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void DestorySubmit()
	{
		GameObject gameObject = this.submitTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.submitScroll.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void ChangeLegionIconClick(GameObject ga)
	{
		ArmyManager.ins.OpenChangeLegionIconPanel();
	}

	private void ChangLeigonTypeClick(GameObject ga)
	{
		this.changeOpenTypePanel.gameObject.SetActive(true);
	}

	private void EditLegionTipClick(GameObject ga)
	{
		this.editLegionTipPanel.gameObject.SetActive(true);
	}

	public void Update()
	{
		if (this.isReset)
		{
			this.memeberTabel.Reposition();
			this.memberScroll.ResetPosition();
			this.isReset = false;
		}
	}
}
