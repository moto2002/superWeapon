using DG.Tweening;
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MainUIPanelManage : MonoBehaviour
{
	public static MainUIPanelManage _instance;

	public GameObject downRight;

	public GameObject downLeft;

	public GameObject chatPanel;

	public Transform goToWorld;

	public Transform goToPVP;

	public GameObject showPVPTip;

	public PlayerRole playerRole;

	public GameObject showBUYbuLING;

	public GameObject messageRed;

	public UILabel topCount;

	public UISprite messageUISprite;

	public UISprite messageBackimg;

	public UILabel resCoinLabel;

	public UILabel resOilLabel;

	public UILabel resSteelLabel;

	public UILabel resRareEarthLabel;

	public UILabel rmbLabel;

	public UILabel count;

	public Transform coinTransform;

	public Transform oilTransform;

	public Transform steelTransform;

	public Transform rareEarthTransform;

	public Transform diamondTransform;

	public Transform peopleTransform;

	public UISprite resCoinSprite;

	public UISprite resOilSprite;

	public UISprite resSteelSprite;

	public UISprite resRareEarthSprite;

	public GameObject resInfoGameObject;

	public UILabel resInfoLabel;

	public GameObject goToWorldHongdian;

	public GameObject goToPVPHongdian;

	public GameObject buildingNotice;

	public ResTips restips;

	public GameObject electricityPow;

	public GameObject addArrmybtn;

	public GameObject takeAllRes;

	public UILabel tips;

	private bool first = true;

	public string ElectricityDes = string.Empty;

	public static int power = 0;

	public static int power_Home = 0;

	public static int electricUse = 0;

	public static int electricUse_Home = 0;

	public string OnLine_Time;

	public UILabel OnLine_Time_Label;

	public UISprite OnLine_Time_Notice;

	public bool IsOnline_Button;

	public List<T_Tower> resTower = new List<T_Tower>();

	private List<DailyTask> mainlineTask;

	private static bool isTaskbool;

	private int rmbNeed;

	public Tweener cc;

	private Vector3 cameraTaget = Vector3.zero;

	public int rmbNum;

	public static bool isHaveTaskNotice = false;

	public static bool IsTaskShow = true;

	public GameObject BuildingUpSpriteAnimation;

	public GameObject BuildingVIPUpSpriteAnimation;

	public static bool isbuildShow = false;

	private bool OnLineButton_CanGet;

	public GameObject EffectResCoinQueShi;

	public GameObject EffectResOilQueShi;

	public GameObject EffectResSteelQueShi;

	public GameObject EffectResRareEarthQueShi;

	public Animation TaskCollect;

	public int unReadChatMessage;

	public static float extraAttack = 0f;

	public static Color ElectricityDesLabelcolor = default(Color);

	public static bool oneLowPower = true;

	private bool IsFirst = true;

	private int ElectricityNum;

	public UIGrid DesignIconGrid;

	public GameObject Btn_ChongZhi;

	public GameObject Btn_LiBao;

	public GameObject Btn_GongGao;

	public GameObject Btn_HuoDong;

	public GameObject Btn_ZaiXian;

	public void OnDestroy()
	{
		MainUIPanelManage._instance = null;
	}

	private void Awake()
	{
		this.showBUYbuLING.gameObject.SetActive(false);
		this.showPVPTip.gameObject.SetActive(false);
		MainUIPanelManage._instance = this;
		this.OnGetObg();
		this.restips = HUDTextTool.inst.restip;
		this.RefreshBuildingStoreNotice();
		if (this.addArrmybtn.gameObject.activeSelf)
		{
			this.takeAllRes.transform.localPosition = new Vector3(-141.9f, 144.9f, 0f);
		}
		else
		{
			this.takeAllRes.transform.localPosition = new Vector3(-28.6f, 144.9f, 0f);
		}
		this.InitEvevt();
	}

	public void OnGetObg()
	{
		this.chatPanel = base.transform.FindChild("Panel/Message/MessageAni/Message").gameObject;
		this.goToWorld = base.transform.FindChild("Panel/DownRight/downRightAni/GoToWorld");
		this.goToPVP = base.transform.FindChild("Panel/DownRight/downRightAni/GoToPVP");
		this.goToWorldHongdian = this.goToWorld.FindChild("Sprite/Hongdian").gameObject;
		this.goToPVPHongdian = this.goToPVP.FindChild("Sprite/Hongdian").gameObject;
		this.messageUISprite = base.transform.FindChild("Panel/Message/MessageAni/Message").GetComponent<UISprite>();
		base.transform.FindChild("Panel/Message").gameObject.AddComponent<MessagePanel>();
		this.messageBackimg = base.transform.FindChild("Panel/Message/MessageAni/Message/messageBackimg").GetComponent<UISprite>();
		this.resCoinLabel = base.transform.FindChild("resource/resAni/coin/bac/scroll/Label").GetComponent<UILabel>();
		this.resOilLabel = base.transform.FindChild("resource/resAni/oil/bac/scroll/Label").GetComponent<UILabel>();
		this.resSteelLabel = base.transform.FindChild("resource/resAni/steel/bac/scroll/Label").GetComponent<UILabel>();
		this.resRareEarthLabel = base.transform.FindChild("resource/resAni/rareEarth/bac/scroll/Label").GetComponent<UILabel>();
		this.rmbLabel = base.transform.FindChild("resource/resAni/addRmbBtn/bac/scroll/Label").GetComponent<UILabel>();
		this.coinTransform = base.transform.FindChild("resource/resAni/coin");
		this.oilTransform = base.transform.FindChild("resource/resAni/oil");
		this.steelTransform = base.transform.FindChild("resource/resAni/steel");
		this.rareEarthTransform = base.transform.FindChild("resource/resAni/rareEarth");
		this.diamondTransform = base.transform.FindChild("resource/resAni/Rmb");
		this.resCoinSprite = base.transform.FindChild("resource/resAni/coin/bac/scroll").GetComponent<UISprite>();
		this.resOilSprite = base.transform.FindChild("resource/resAni/oil/bac/scroll").GetComponent<UISprite>();
		this.resSteelSprite = base.transform.FindChild("resource/resAni/steel/bac/scroll").GetComponent<UISprite>();
		this.resRareEarthSprite = base.transform.FindChild("resource/resAni/rareEarth/bac/scroll").GetComponent<UISprite>();
		this.resInfoGameObject = base.transform.FindChild("resource/resAni/coin/resInfo").gameObject;
		this.resInfoLabel = base.transform.FindChild("resource/resAni/coin/resInfo/resInfoLabel").GetComponent<UILabel>();
		this.buildingNotice = base.transform.FindChild("Panel/DownLeft/downLeftAni/BuildStore/notice").gameObject;
		this.electricityPow = base.transform.FindChild("player/playerAni/HavePowerPlayer/Electricity").gameObject;
		this.addArrmybtn = base.transform.FindChild("Panel/DownRight/AddArrnyBtn").gameObject;
		this.takeAllRes = base.transform.FindChild("Panel/DownRight/TakeAllRes").gameObject;
	}

	public void OpenPanelMian()
	{
		this.downLeft.SetActive(true);
		this.downRight.SetActive(true);
	}

	public void ShowHongdian()
	{
		this.goToWorldHongdian.SetActive(true);
	}

	public void ShowTopTenInfo()
	{
		if (HeroInfo.GetInstance().topScore < int.Parse(UnitConst.GetInstance().DesighConfigDic[72].value) && HeroInfo.GetInstance().topScore != 0)
		{
			this.topCount.text = LanguageManage.GetTextByKey("第", "others") + HeroInfo.GetInstance().topScore + LanguageManage.GetTextByKey("名", "others");
			this.topCount.color = new Color(0.196078435f, 0.972549f, 0.117647059f);
		}
		else
		{
			this.topCount.text = LanguageManage.GetTextByKey("未上榜", "others");
			this.topCount.color = new Color(1f, 0.1882353f, 0.101960786f);
		}
	}

	private void InitEvevt()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenMonthlyCard, new EventManager.VoidDelegate(this.OpenMonthlyCard));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Achievement, new EventManager.VoidDelegate(this.OpenAchievement));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ArmsDealer, new EventManager.VoidDelegate(this.OpenArmsDealer));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ClickCoin, new EventManager.VoidDelegate(this.CliclCoin));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ClickOil, new EventManager.VoidDelegate(this.ClickOil));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ClickRareEarth, new EventManager.VoidDelegate(this.ClickRareEarth));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ClickSteel, new EventManager.VoidDelegate(this.ClickSteel));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Email, new EventManager.VoidDelegate(this.OpenEmail));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_GoToWorld, new EventManager.VoidDelegate(this.GoToWorld));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_GoToPVP, new EventManager.VoidDelegate(this.GoToPVP));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_GoToBattle, new EventManager.VoidDelegate(this.GoToBattle));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenBattle, new EventManager.VoidDelegate(MainUIPanelManage.OpenBattle));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenPlayerInfo, new EventManager.VoidDelegate(this.OpenPlayerInfo));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenItemBox, new EventManager.VoidDelegate(this.OpenItemBox));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenTopTen, new EventManager.VoidDelegate(this.OpenTopTen));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenBuildingStore, new EventManager.VoidDelegate(this.OpenBuildingStore));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Sign, new EventManager.VoidDelegate(this.OpenSign));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Task, new EventManager.VoidDelegate(this.OpenTask));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_SynopsisTaskPanel, new EventManager.VoidDelegate(this.OnSynopsisTaskPanelShow));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_TaskTypePanel, new EventManager.VoidDelegate(this.TaskTypePanelShow));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_AddMilitary, new EventManager.VoidDelegate(this.AddMilitary));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_AddProde, new EventManager.VoidDelegate(this.AddProde));
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_OpenPanel, new EventManager.VoidDelegate(this.OpenEnemyPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OfficerDegree, new EventManager.VoidDelegate(this.OfficerDegreePanelShow));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_AddArrmyClick, new EventManager.VoidDelegate(this.OnArmyClick));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OnLine, new EventManager.VoidDelegate(this.IsOnlineButton_OpenActivities));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_TakeAllRes, new EventManager.VoidDelegate(this.TakeAllResClick));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenScretShop, new EventManager.VoidDelegate(this.OpenScret));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_GameAnnouncement, new EventManager.VoidDelegate(this.GameAnnouceMent));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_PlayerInfo, new EventManager.VoidDelegate(this.PlayerInfo));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_GotoBuildingState, new EventManager.VoidDelegate(this.GotoBuildingState));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenActivity, new EventManager.VoidDelegate(this.OpenActivities));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenExchangeGift, new EventManager.VoidDelegate(this.OpenExchangeGift));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenChargeActitys, new EventManager.VoidDelegate(this.OpenChargeActities));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenShop, new EventManager.VoidDelegate(this.MainPanel_OpenShop));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenDearShop, new EventManager.VoidDelegate(this.MainPanel_OpenDearShop));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenBaseGifePanel, new EventManager.VoidDelegate(this.MainPanel_OpenBaseGifePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenShouChong, new EventManager.VoidDelegate(this.OpenShouChong));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Open1YuanGou, new EventManager.VoidDelegate(this.Open1YuanGou));
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_Open, new EventManager.VoidDelegate(this.PVPMessage_Open));
	}

	private void OpenMonthlyCard(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("MonthlyCardPanel", SenceType.Island);
	}

	public void GoToBattle(GameObject ga)
	{
		this.LeaveHome();
		LegionMapManager._inst.Init(LegionMapManager.BattleType.普通副本);
	}

	public void TaskPanel_Skip(int task_id)
	{
		UnityEngine.Debug.Log("跳转！任务ID：" + task_id);
		UnityEngine.Debug.Log("跳转！任务类型：" + UnitConst.GetInstance().DailyTask[task_id].skipType);
		if (!string.IsNullOrEmpty(UnitConst.GetInstance().DailyTask[task_id].NewBieGroup))
		{
			if (NewTaskPanelManager.ins)
			{
				FuncUIManager.inst.DestoryFuncUI("NewTaskPanel");
			}
			NewbieGuideWrap.nextNewBi = UnitConst.GetInstance().DailyTask[task_id].NewBieGroup;
			string s = NewbieGuideWrap.nextNewBi.Substring(4, 1);
			string text = NewbieGuideWrap.nextNewBi.Substring(5, 1);
			int num = int.Parse(s);
			if (text != "_")
			{
				try
				{
					num = int.Parse(s) * 10 + int.Parse(text);
				}
				catch
				{
				}
			}
			UnityEngine.Debug.Log("GroundID:" + num);
			HUDTextTool.inst.NextLuaCall("任务 调用· ·", new object[]
			{
				true
			});
		}
		else
		{
			switch (UnitConst.GetInstance().DailyTask[task_id].skipType)
			{
			case DailyTask.taskSkilType.建造列表:
				FuncUIManager.inst.OpenFuncUI("BuildingStorePanel", SenceType.Island);
				break;
			case DailyTask.taskSkilType.兵种升级:
				if (UnitConst.GetInstance().DailyTask[task_id].skipValue == 9)
				{
					T_Tower t_Tower = SenceManager.inst.towers.FirstOrDefault((T_Tower a) => a.posIdx == -1);
					ArmyControlAndUpdatePanel.isFeijiChang = false;
					ArmyControlAndUpdatePanel.lv = t_Tower.lv;
					ArmyControlAndUpdatePanel.posIndex = t_Tower.posIdx;
					ArmyControlAndUpdatePanel.index = t_Tower.index;
					ArmyControlAndUpdatePanel.towerID = t_Tower.id;
					FuncUIManager.inst.OpenFuncUI("NewArmyControlPanel", SenceType.Island);
				}
				else if (UnitConst.GetInstance().DailyTask[task_id].skipValue == 10)
				{
					T_Tower t_Tower2 = SenceManager.inst.towers.FirstOrDefault((T_Tower a) => a.index == 91);
					ArmyControlAndUpdatePanel.isFeijiChang = true;
					ArmyControlAndUpdatePanel.lv = t_Tower2.lv;
					ArmyControlAndUpdatePanel.posIndex = t_Tower2.posIdx;
					ArmyControlAndUpdatePanel.index = t_Tower2.index;
					ArmyControlAndUpdatePanel.towerID = t_Tower2.id;
					FuncUIManager.inst.OpenFuncUI("NewArmyControlPanel", SenceType.Island);
				}
				break;
			case DailyTask.taskSkilType.作战图:
				if (HeroInfo.GetInstance().PlayerRadarLv > 0)
				{
					PlayerHandle.GOTO_WorldMap();
				}
				else
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("雷达未建造", "build"), HUDTextTool.TextUITypeEnum.Num5);
				}
				break;
			case DailyTask.taskSkilType.科技升级:
				if (HeroInfo.GetInstance().PlayerTechBuildingLv > 0)
				{
					FuncUIManager.inst.OpenFuncUI("TechnologyUpdateTreePanel", SenceType.Island);
				}
				else
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("未建造科研中心", "build"), HUDTextTool.TextUITypeEnum.Num5);
				}
				break;
			case DailyTask.taskSkilType.建筑升级:
			{
				T_Tower t_Tower3 = new T_Tower();
				int secondConditionId = UnitConst.GetInstance().DailyTask[task_id].secondConditionId;
				int num2 = UnitConst.GetInstance().DailyTask[task_id].step - 1;
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"跳转：目标建筑索引：",
					secondConditionId,
					"目标建筑当前等级：",
					num2,
					"期望等级：",
					UnitConst.GetInstance().DailyTask[task_id].step
				}));
				int num3 = -100;
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if (SenceManager.inst.towers[i].index == secondConditionId && SenceManager.inst.towers[i].lv == num2)
					{
						UnityEngine.Debug.Log("筛选成功");
						if (!HeroInfo.GetInstance().BuildCD.Contains(SenceManager.inst.towers[i].id))
						{
							num3 += 100;
							SenceManager.inst.towers[i].MouseUp();
							if (T_CommandPanelManage._instance)
							{
								T_CommandPanelManage._instance.BuildUpdateByTask(SenceManager.inst.towers[i]);
							}
							break;
						}
						t_Tower3 = SenceManager.inst.towers[i];
						num3++;
					}
				}
				if (num3 == -100)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("未找到目标建筑", "build"), HUDTextTool.TextUITypeEnum.Num5);
				}
				else if (num3 > -100 && num3 < 0)
				{
					if (t_Tower3 != null)
					{
						t_Tower3.MouseUp();
						if (ClickTimeUIBtn.inst)
						{
							ClickTimeUIBtn.inst.ClickTime(null);
						}
					}
					else
					{
						HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("未找到目标建筑", "build"), HUDTextTool.TextUITypeEnum.Num5);
					}
				}
				break;
			}
			case DailyTask.taskSkilType.技能制卡:
			{
				bool flag = false;
				for (int j = 0; j < SenceManager.inst.towers.Count; j++)
				{
					if (SenceManager.inst.towers[j].index == 11)
					{
						flag = true;
						SenceManager.inst.towers[j].MouseUp();
						if (T_CommandPanelManage._instance)
						{
							T_CommandPanelManage._instance.Chouqu(null);
						}
						break;
					}
				}
				if (!flag)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("未建造科研中心", "build"), HUDTextTool.TextUITypeEnum.Num5);
				}
				break;
			}
			case DailyTask.taskSkilType.副本:
				if (MainUIPanelManage._instance)
				{
					MainUIPanelManage._instance.GoToBattle(null);
				}
				break;
			case DailyTask.taskSkilType.兵种升级_战车工厂:
			case DailyTask.taskSkilType.兵种升级_飞机场:
			case DailyTask.taskSkilType.兵种升级_兵营:
			{
				int num4 = 0;
				if (UnitConst.GetInstance().DailyTask[task_id].skipType == DailyTask.taskSkilType.兵种升级_战车工厂)
				{
					num4 = 13;
				}
				else if (UnitConst.GetInstance().DailyTask[task_id].skipType == DailyTask.taskSkilType.兵种升级_飞机场)
				{
					num4 = 91;
				}
				else if (UnitConst.GetInstance().DailyTask[task_id].skipType == DailyTask.taskSkilType.兵种升级_兵营)
				{
					num4 = 13;
				}
				for (int k = 0; k < SenceManager.inst.towers.Count; k++)
				{
					if (SenceManager.inst.towers[k].index == num4)
					{
						SenceManager.inst.towers[k].MouseUp();
						if (T_CommandPanelManage._instance)
						{
							T_CommandPanelManage._instance.ChangeSoliderByTask(SenceManager.inst.towers[k]);
						}
						break;
					}
				}
				break;
			}
			}
		}
	}

	public void OpenActivities(GameObject ga)
	{
		this.IsOnline_Button = false;
		HUDTextTool.isGetActivitiesAward = true;
		HUDTextTool.isNewActivitie = true;
		ChargeActityPanel.GetRegCharges = HeroInfo.GetInstance().activityClass;
		FuncUIManager.inst.OpenFuncUI("ChargeActityPanel", SenceType.Island);
	}

	private void MainPanel_OpenBaseGifePanel(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("BaseGiftPanel", SenceType.Island);
	}

	public void IsOnlineButton_OpenActivities(GameObject ga)
	{
		if (!this.OnLineButton_CanGet)
		{
			this.IsOnline_Button = true;
			HUDTextTool.isGetActivitiesAward = true;
			FuncUIManager.inst.OpenFuncUI("ActivitiesPanel", SenceType.Island);
		}
		else
		{
			HUDTextTool.isGetActivitiesAward = false;
			int coin = 0;
			int oil = 0;
			int steel = 0;
			int earth = 0;
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().loadReward[OnLineAward.laod.step].res)
			{
				switch (current.Key)
				{
				case ResType.金币:
					coin = current.Value;
					break;
				case ResType.石油:
					oil = current.Value;
					break;
				case ResType.钢铁:
					steel = current.Value;
					break;
				case ResType.稀矿:
					earth = current.Value;
					break;
				}
			}
			if (SenceManager.inst.NoResSpace(coin, oil, steel, earth, true))
			{
				return;
			}
			OnLinwAwardHandler.CG_CSOnLine(OnLineAward.laod.step, delegate(bool isError)
			{
				if (!isError)
				{
					ShowAwardPanelManger.showAwardList();
				}
			});
		}
	}

	public void OpenArmyMemberPanel(GameObject ga)
	{
		bool isOpen = false;
		if (HeroInfo.GetInstance().playerGroupId == 0L)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你还未加入军团", "others"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		ArmyGroupHandler.CG_CSLegionMemberData(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
		{
			if (!isError)
			{
				this.StartCoroutine(this.openArmyMemberPanel());
				isOpen = true;
			}
		});
		if (isOpen)
		{
			return;
		}
		base.StartCoroutine(this.openArmyMemberPanel());
	}

	[DebuggerHidden]
	private IEnumerator openArmyMemberPanel()
	{
		return new MainUIPanelManage.<openArmyMemberPanel>c__Iterator86();
	}

	public void OpenChargeActities(GameObject ga)
	{
		ActivityHandler.CSGetActivityList(2, delegate(bool isError, Opcode opcode)
		{
			ChargeActityPanel.GetRegCharges = HeroInfo.GetInstance().reChargeClass;
			HUDTextTool.isNewActivitie = false;
			FuncUIManager.inst.OpenFuncUI("ChargeActityPanel", SenceType.Island);
		});
	}

	public void PVPMessage_Open(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("PVPMessage", Loading.senceType);
	}

	public void Open1YuanGou(GameObject ga)
	{
		ActivityHandler.CSGetActivityList(4, delegate(bool isError, Opcode opcode)
		{
			ChargeActityPanel.GetRegCharges = HeroInfo.GetInstance().OneYuanGouChargeClass;
			HUDTextTool.isNewActivitie = false;
			FuncUIManager.inst.OpenFuncUI("ChargeActityPanel", SenceType.Island);
		});
	}

	public void OpenShouChong(GameObject ga)
	{
		ActivityHandler.CSGetActivityList(3, delegate(bool isError, Opcode opcode)
		{
			ChargeActityPanel.GetRegCharges = HeroInfo.GetInstance().ShouChongChargeClass;
			HUDTextTool.isNewActivitie = false;
			FuncUIManager.inst.OpenFuncUI("ChargeActityPanel", SenceType.Island);
		});
	}

	private void MainPanel_OpenShop(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
	}

	private void MainPanel_OpenDearShop(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("ArmsDealerPanel", Loading.senceType);
	}

	public void OpenExchangeGift(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("ExchangeGiftPanel", SenceType.Island);
	}

	public void GotoBuildingState(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("BuilingStatePanel", SenceType.Island);
		CameraControl.inst.ChangeCameraBuildingState(true);
	}

	public void PlayerInfo(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("PlayerInfoPanel", SenceType.Island);
	}

	public void ShowPlayerInfoLevel()
	{
		if (HeroInfo.GetInstance().playerlevel < int.Parse(UnitConst.GetInstance().DesighConfigDic[71].value))
		{
			this.tips.text = string.Empty;
		}
		else
		{
			this.tips.text = LanguageManage.GetTextByKey("已满级", "others");
			this.playerRole.playerExp.gameObject.SetActive(false);
		}
	}

	public void GameAnnouceMent(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("GameAnnouncement", SenceType.Island);
		if (HeroInfo.GetInstance().gameAnnouncementData.isHaveNewAnounce)
		{
			GameAnnouncementManager.ins.ResfreshGmaeTips();
		}
	}

	public void OpenScret(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("NewConvertPanel", SenceType.Island);
	}

	public void OnOnLineAward(GameObject ga)
	{
		OnLinwAwardHandler.CG_CSOnLine(OnLineAward.laod.step, delegate(bool isError)
		{
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	public void TakeAllResClick(GameObject ga)
	{
		base.StartCoroutine(this.TakeAllResIE(ga));
	}

	[DebuggerHidden]
	private IEnumerator TakeAllResIE(GameObject btn)
	{
		MainUIPanelManage.<TakeAllResIE>c__Iterator87 <TakeAllResIE>c__Iterator = new MainUIPanelManage.<TakeAllResIE>c__Iterator87();
		<TakeAllResIE>c__Iterator.btn = btn;
		<TakeAllResIE>c__Iterator.<$>btn = btn;
		<TakeAllResIE>c__Iterator.<>f__this = this;
		return <TakeAllResIE>c__Iterator;
	}

	private void TakeAllResCallBack()
	{
		foreach (SCPlayerResource current in HeroInfo.GetInstance().addResouce)
		{
			switch (current.resId)
			{
			case 1:
				HUDTextTool.inst.SetText(string.Format("{0}:{1}", LanguageManage.GetTextByKey("收集金币", "ResIsland"), current.resVal), HUDTextTool.TextUITypeEnum.Num1);
				break;
			case 2:
				HUDTextTool.inst.SetText(string.Format("{0}:{1}", LanguageManage.GetTextByKey("收集石油", "ResIsland"), current.resVal), HUDTextTool.TextUITypeEnum.Num1);
				break;
			case 3:
				HUDTextTool.inst.SetText(string.Format("{0}:{1}", LanguageManage.GetTextByKey("收集钢铁", "ResIsland"), current.resVal), HUDTextTool.TextUITypeEnum.Num1);
				break;
			case 4:
				HUDTextTool.inst.SetText(string.Format("{0}:{1}", LanguageManage.GetTextByKey("收集稀矿", "ResIsland"), current.resVal), HUDTextTool.TextUITypeEnum.Num1);
				break;
			}
		}
	}

	private void OnSynopsisTaskPanelShow(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("SynopsisTaskPanel", SenceType.Island).GetComponent<SynopsisTaskPanelManager>().OnPanelInfoShow();
		this.ClearScree();
	}

	private void TaskTypePanelShow(GameObject go)
	{
		MainUIPanelManage.isTaskbool = false;
		for (int i = 0; i < UnitConst.GetInstance().ALLlineTask().Count; i++)
		{
			if (UnitConst.GetInstance().ALLlineTask()[i].mainTaskType == 4 && UnitConst.GetInstance().ALLlineTask()[i].isUIShow)
			{
				MainUIPanelManage.isTaskbool = true;
			}
		}
		if (MainUIPanelManage.isTaskbool)
		{
		}
	}

	public static void ShowOrCloseMainUIPanel(bool display)
	{
		if (MainUIPanelManage._instance)
		{
			if (CameraControl.inst && display && CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
			{
				return;
			}
			MainUIPanelManage._instance.gameObject.SetActive(display);
		}
	}

	private void OnArmyClick(GameObject go)
	{
		CalcMoneyHandler.CSCalcMoney(11, 0, 0, 0L, 0, 0, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				ShopPanelManage.ShowHelp_NoRMB(null, null);
				return;
			}
			AudioManage.inst.PlayAuido("yijianbubing", false);
			ArmyFuncHandler.CG_ArmsConfigureAuto(money, 1, delegate
			{
				this.ShowCostCoin();
			});
		}
	}

	private void ShowCostCoin()
	{
		for (int i = 0; i < HeroInfo.GetInstance().subResouce.Count; i++)
		{
			if (HeroInfo.GetInstance().subResouce[i] != null)
			{
				switch (HeroInfo.GetInstance().subResouce[i].resId)
				{
				case 1:
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("一键补兵花费", "ResIsland") + HeroInfo.GetInstance().subResouce[i].resVal + LanguageManage.GetTextByKey("金币", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
					break;
				}
			}
		}
	}

	private void OfficerDegreePanelShow(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("OfficerPanelDegreeLevelPanel", SenceType.Island);
	}

	private void OpenEnemyPanel(GameObject ga)
	{
		UIManager.inst.enemyPanel.gameObject.SetActive(true);
	}

	private void OpenAchievement(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("AchievementPanel", SenceType.Island);
	}

	private void OpenArmsDealer(GameObject go)
	{
		HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团暂未上线", "others"), HUDTextTool.TextUITypeEnum.Num1);
	}

	private void CliclCoin(GameObject go)
	{
		this.GetResInfo(ResType.金币);
	}

	private void ClickOil(GameObject go)
	{
		this.GetResInfo(ResType.石油);
	}

	private void ClickRareEarth(GameObject go)
	{
		this.GetResInfo(ResType.稀矿);
	}

	private void ClickSteel(GameObject go)
	{
		this.GetResInfo(ResType.钢铁);
	}

	private void OpenEmail(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("EmailPanel", SenceType.Island);
		if (!FuncUIManager.inst.OpenFuncUI("EmailPanel", SenceType.Island).GetComponent<EmailPanel>())
		{
			FuncUIManager.inst.OpenFuncUI("EmailPanel", SenceType.Island).AddComponent<EmailPanel>();
			FuncUIManager.inst.OpenFuncUI("EmailPanel", SenceType.Island).GetComponent<EmailPanel>().enabled = true;
		}
	}

	public void CheckPVPNewMessage()
	{
		if (HeroInfo.GetInstance().PVPReportDataList.Count > 0)
		{
			foreach (KeyValuePair<long, ReportData> current in HeroInfo.GetInstance().PVPReportDataList)
			{
				if (current.Value.fighterId != HeroInfo.GetInstance().userId)
				{
					if (current.Value.fighterWin)
					{
						UnityEngine.Debug.Log(string.Concat(new object[]
						{
							"防守失败战报：ID：",
							current.Value.id,
							"发起者：",
							current.Value.fighterName,
							" UnRead:",
							current.Value.UnRead
						}));
						if (current.Value.UnRead)
						{
							GameObject gameObject = FuncUIManager.inst.OpenFuncUI("PVPNewMessage", SenceType.Island);
							gameObject.GetComponent<PVPNewMessage>().ThisReportData = current.Value;
							gameObject.GetComponent<PVPNewMessage>().SetInfo();
							HeroInfo.GetInstance().PVPReportDataList[current.Key].UnRead = false;
							break;
						}
					}
				}
			}
		}
	}

	public void EnterHome()
	{
		Dictionary<int, int> cheakArmyList = HeroInfo.GetInstance().CheakArmyList;
		if (cheakArmyList.Count > 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/FuncUI/LossArmyMessage")) as GameObject;
			gameObject.transform.parent = base.transform.parent.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<LossArmyMessage>().LossArmyList = cheakArmyList;
			gameObject.GetComponent<LossArmyMessage>().SetInfo();
		}
		else
		{
			this.CheckPVPNewMessage();
		}
	}

	public void LeaveHome()
	{
		HeroInfo.GetInstance().CheakArmyList = null;
	}

	private void GoToWorld(GameObject go)
	{
		this.LeaveHome();
		SenceManager.inst.DelaySendHttp();
		go.GetComponent<ButtonClick>().isSendLua = false;
		this.goToWorldHongdian.SetActive(false);
		if (this.first)
		{
			this.first = false;
			SenceInfo.CurBattle = null;
			PlayerHandle.GOTO_WorldMap();
			this.first = true;
			ResourcePanelManage.inst.enabled = false;
			if (ResourItemUI._Inst != null)
			{
				ResourItemUI._Inst.OnDestryItemResUI();
			}
			return;
		}
	}

	private void GoToPVP(GameObject go)
	{
		SenceManager.inst.DelaySendHttp();
		this.goToPVPHongdian.SetActive(false);
		this.showPVPTip.gameObject.SetActive(true);
		this.showPVPTip.GetComponent<ShowPVPTips>().showPVPTips(LanguageManage.GetTextByKey("系统即将为您匹配到了一个对手，如果您确定要开始战斗，我们会从您的金库扣除", "others") + UnitConst.GetInstance().DesighConfigDic[65].value + LanguageManage.GetTextByKey("金币", "others"), delegate
		{
			if (HeroInfo.GetInstance().playerRes.resCoin > int.Parse(UnitConst.GetInstance().DesighConfigDic[65].value))
			{
				PVPHandler.CS_PVP();
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币不足", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
			}
		}, null);
	}

	private void DisplayMenuBtn(GameObject go)
	{
	}

	private void OpenTask(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("NewTaskPanel", SenceType.Island);
		NewTaskPanelManager.ins.ShowTaskNotice();
	}

	private void OpenSign(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("GetawardPanel", SenceType.Island);
		if (!FuncUIManager.inst.OpenFuncUI("GetawardPanel", SenceType.Island).GetComponent<GetawardPanelShow>())
		{
			FuncUIManager.inst.OpenFuncUI("GetawardPanel", SenceType.Island).AddComponent<GetawardPanelShow>();
			FuncUIManager.inst.OpenFuncUI("GetawardPanel", SenceType.Island).GetComponent<GetawardPanelShow>().enabled = true;
		}
	}

	private void OpenMenuBtn(GameObject ga)
	{
	}

	private static void OpenBattle(GameObject go)
	{
		GameObject gameObject = FuncUIManager.inst.OpenFuncUI("NBattlePanel", SenceType.Island);
		NBattleManage.inst.CreateBattleItem();
	}

	private void OpenTopTen(GameObject go)
	{
		if (TopTenPanelManage.rank.Count <= 0)
		{
			TopTenHandler.CG_TopTenListStart(1);
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("TopTenPanel", Loading.senceType);
			this.ClearScree();
		}
	}

	private void OpenItemBox(GameObject go)
	{
		if (HeroInfo.GetInstance().PlayerItemInfo.Count == 0)
		{
			ItemHandler.CG_ItemList(delegate
			{
				FuncUIManager.inst.OpenFuncUI("ItemPanel", SenceType.Island);
			});
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("ItemPanel", SenceType.Island);
		}
		this.ClearScree();
	}

	private void OpenBuildingStore(GameObject ga)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			return;
		}
		DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector2.zero, null);
		SenceManager.inst.DesGetWallLatelyEffect();
		FuncUIManager.inst.OpenFuncUI("BuildingStorePanel", SenceType.Island);
		if (BuildingStorePanelManage._instance != null)
		{
			BuildingStorePanelManage._instance.OnUpdateInfo();
		}
		this.OnMoveCameraShow();
	}

	public void OnMoveCameraShow()
	{
	}

	private void OpenPlayerInfo(GameObject go)
	{
		FuncUIManager.inst.OpenFuncUI("SetingPanel", SenceType.Island);
	}

	private void Start()
	{
		this.EnterHome();
		this.AddArmyShow();
		this.DisplayMessage(0, true);
		MessagePanel._Inst.applyList.Clear();
		this.SetDesignIcon();
		PVPHandler.CS_PVPMes();
		SenceManager.inst.ReturnHome();
	}

	public void AddArmyShow()
	{
	}

	public void isTask()
	{
	}

	public void AddMilitary(GameObject go)
	{
	}

	public void AddProde(GameObject go)
	{
	}

	private void Update()
	{
		if (!this.OnLine_Time_Notice)
		{
			this.OnLine_Time_Notice = this.OnLine_Time_Label.parent.transform.FindChild("notice").GetComponent<UISprite>();
		}
		else if (this.OnLine_Time_Notice)
		{
			this.OnLine_Time_Notice.gameObject.SetActive(false);
		}
		Color white = Color.white;
		Color black = Color.black;
		TimeSpan timeSpan = OnLineAward.laod.time - TimeTools.GetNowTimeSyncServerToDateTime();
		if (timeSpan.TotalSeconds > 1.0 && OnLineAward.laod.step != 0)
		{
			if (timeSpan.Hours > 0)
			{
				this.OnLine_Time = string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Minutes > 0)
			{
				this.OnLine_Time = string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Seconds > 1)
			{
				this.OnLine_Time = string.Format("{0}", timeSpan.Seconds);
			}
			else
			{
				this.OnLine_Time = "可领取";
				white = new Color(1f, 1f, 0.5f, 1f);
				black = new Color(0.5f, 0.5f, 0.5f, 1f);
				if (this.OnLine_Time_Notice)
				{
					this.OnLine_Time_Notice.gameObject.SetActive(true);
				}
			}
			this.OnLine_Time_Label.color = white;
			this.OnLine_Time_Label.effectColor = black;
			this.OnLine_Time_Label.text = this.OnLine_Time;
			this.OnLineButton_CanGet = false;
		}
		else if (OnLineAward.laod.step != 0)
		{
			this.OnLine_Time = "可领取";
			white = new Color(1f, 1f, 0.5f, 1f);
			black = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.OnLine_Time_Label.color = white;
			this.OnLine_Time_Label.effectColor = black;
			if (this.OnLine_Time_Notice)
			{
				this.OnLine_Time_Notice.gameObject.SetActive(true);
			}
			this.OnLine_Time_Label.text = this.OnLine_Time;
			this.OnLineButton_CanGet = true;
		}
		else
		{
			this.OnLine_Time = string.Empty;
			this.OnLine_Time_Label.text = this.OnLine_Time;
			this.OnLineButton_CanGet = false;
		}
	}

	private void DisplayMessage(int type, bool isFirst = false)
	{
		if (isFirst)
		{
			HeroInfo.GetInstance().chatMessage.ChatTempData = (from a in HeroInfo.GetInstance().chatMessage.ChatData
			orderby a.id
			select a).ToList<SCSendMessage>();
		}
		if (HeroInfo.GetInstance().chatMessage.ChatTempData.Count > 0)
		{
			HeroInfo.GetInstance().chatMessage.ChatTempData = (from a in HeroInfo.GetInstance().chatMessage.ChatTempData
			orderby a.id
			select a).ToList<SCSendMessage>();
			for (int i = 0; i < HeroInfo.GetInstance().chatMessage.ChatTempData.Count; i++)
			{
				GameObject gameObject = NGUITools.AddChild(MessagePanel._Inst.messageTable.gameObject, MessagePanel._Inst.messItem);
				gameObject.name = HeroInfo.GetInstance().chatMessage.ChatTempData[i].id.ToString();
				gameObject.GetComponent<messageItem>().Show(HeroInfo.GetInstance().chatMessage.ChatTempData[i]);
				if (HeroInfo.GetInstance().chatMessage.ChatTempData[i].msg.channel != type)
				{
					gameObject.SetActive(false);
				}
			}
		}
		HeroInfo.GetInstance().chatMessage.ChatTempData.Clear();
		MessagePanel._Inst.messageTable.onReposition = new UITable.OnReposition(this.SetValue);
		MessagePanel._Inst.messageTable.Reposition();
	}

	private void SetValue()
	{
		if (MessagePanel._Inst.messageTable && MessagePanel._Inst.messageTable.GetComponentInParent<UIScrollView>())
		{
			MessagePanel._Inst.messageTable.GetComponentInParent<UIScrollView>().ResetPosition();
		}
	}

	public void ShowApplyInfo(int type)
	{
		for (int i = 0; i < MessagePanel._Inst.applyList.Count; i++)
		{
			UnityEngine.Object.Destroy(MessagePanel._Inst.applyList[i].gameObject);
		}
		MessagePanel._Inst.messageTable.Reposition();
		MessagePanel._Inst.applyList.Clear();
		if (HeroInfo.GetInstance().legionApply.Count > 0)
		{
			foreach (KeyValuePair<long, LegionHelpApply> current in from a in HeroInfo.GetInstance().legionApply
			orderby a.Value.time
			select a)
			{
				GameObject gameObject = NGUITools.AddChild(MessagePanel._Inst.messageTable.gameObject, MessagePanel._Inst.applyItem);
				gameObject.name = current.Value.id.ToString();
				MessagePanel._Inst.applyList.Add(gameObject);
				gameObject.GetComponent<ApplyMessageItem>().btnApply.name = current.Value.id.ToString();
				gameObject.GetComponent<ApplyMessageItem>().endTime = current.Value.cdTime;
				gameObject.GetComponent<ApplyMessageItem>().id = current.Value.id;
				gameObject.GetComponent<ApplyMessageItem>().showInfo(current.Value);
				gameObject.GetComponent<ApplyMessageItem>().isCan = true;
				if (current.Value.userId == HeroInfo.GetInstance().userId)
				{
					gameObject.GetComponent<ApplyMessageItem>().btnApply.SetActive(false);
				}
				else
				{
					gameObject.GetComponent<ApplyMessageItem>().btnApply.SetActive(true);
				}
				if (gameObject.GetComponent<ApplyMessageItem>().state != type)
				{
					gameObject.SetActive(false);
				}
			}
		}
		HeroInfo.GetInstance().chatMessage.ChatTempData.Clear();
		MessagePanel._Inst.messageTable.onReposition = new UITable.OnReposition(this.SetValue);
		MessagePanel._Inst.messageTable.Reposition();
	}

	public void SetResourceText(bool isSetResNum = false)
	{
		if (isSetResNum)
		{
			this.SetResCoin();
			this.SetResOil();
			this.SetResSteel();
			this.SetResRareEarth();
		}
		this.SetResRMB();
		if (HeroInfo.GetInstance().playerRes.maxCoin > 0)
		{
			this.coinTransform.transform.localPosition = new Vector3(5f, -3f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxOil > 0)
		{
			this.coinTransform.transform.localPosition = new Vector3(-125f, -3f, 0f);
			this.oilTransform.transform.localPosition = new Vector3(5f, -3f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxSteel > 0)
		{
			this.coinTransform.transform.localPosition = new Vector3(-255f, -3f, 0f);
			this.oilTransform.transform.localPosition = new Vector3(-125f, -3f, 0f);
			this.steelTransform.transform.localPosition = new Vector3(5f, -3f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxRareEarth > 0)
		{
			this.coinTransform.transform.localPosition = new Vector3(-385f, -3f, 0f);
			this.oilTransform.transform.localPosition = new Vector3(-255f, -3f, 0f);
			this.steelTransform.transform.localPosition = new Vector3(-125f, -3f, 0f);
			this.rareEarthTransform.transform.localPosition = new Vector3(5f, -3f, 0f);
		}
		this.peopleTransform.localPosition = this.coinTransform.localPosition - new Vector3(130f, 0f, 0f);
	}

	private void SetResRMB()
	{
		this.rmbLabel.text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
	}

	private void SetResRareEarth()
	{
		this.resRareEarthLabel.text = HeroInfo.GetInstance().playerRes.resRareEarth.ToString();
		this.resRareEarthSprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resRareEarth / (float)HeroInfo.GetInstance().playerRes.maxRareEarth;
		if (HeroInfo.GetInstance().playerRes.resRareEarth >= HeroInfo.GetInstance().playerRes.maxRareEarth)
		{
			this.resRareEarthLabel.color = Color.red;
		}
		else
		{
			this.resRareEarthLabel.color = Color.white;
		}
		if ((float)HeroInfo.GetInstance().playerRes.resRareEarth <= (float)HeroInfo.GetInstance().playerRes.maxRareEarth * 0.05f)
		{
			if (this.EffectResRareEarthQueShi)
			{
				this.EffectResRareEarthQueShi.SetActive(true);
			}
			else
			{
				this.EffectResRareEarthQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", this.resRareEarthLabel.transform).ga;
			}
		}
		else if (this.EffectResRareEarthQueShi)
		{
			this.EffectResRareEarthQueShi.SetActive(false);
		}
	}

	private void SetResSteel()
	{
		this.resSteelLabel.text = HeroInfo.GetInstance().playerRes.resSteel.ToString();
		this.resSteelSprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resSteel / (float)HeroInfo.GetInstance().playerRes.maxSteel;
		if (HeroInfo.GetInstance().playerRes.resSteel >= HeroInfo.GetInstance().playerRes.maxSteel)
		{
			this.resSteelLabel.color = Color.red;
		}
		else
		{
			this.resSteelLabel.color = Color.white;
		}
		if ((float)HeroInfo.GetInstance().playerRes.resSteel <= (float)HeroInfo.GetInstance().playerRes.maxSteel * 0.05f)
		{
			if (this.EffectResSteelQueShi)
			{
				this.EffectResSteelQueShi.SetActive(true);
			}
			else
			{
				this.EffectResSteelQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", this.resSteelLabel.transform).ga;
			}
		}
		else if (this.EffectResSteelQueShi)
		{
			this.EffectResSteelQueShi.SetActive(false);
		}
	}

	private void SetResOil()
	{
		this.resOilLabel.text = HeroInfo.GetInstance().playerRes.resOil.ToString();
		this.resOilSprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resOil / (float)HeroInfo.GetInstance().playerRes.maxOil;
		if (HeroInfo.GetInstance().playerRes.resOil >= HeroInfo.GetInstance().playerRes.maxOil)
		{
			this.resOilLabel.color = Color.red;
		}
		else
		{
			this.resOilLabel.color = Color.white;
		}
		if ((float)HeroInfo.GetInstance().playerRes.resOil <= (float)HeroInfo.GetInstance().playerRes.maxOil * 0.05f)
		{
			if (this.EffectResOilQueShi)
			{
				this.EffectResOilQueShi.SetActive(true);
			}
			else
			{
				this.EffectResOilQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", this.resOilLabel.transform).ga;
			}
		}
		else if (this.EffectResOilQueShi)
		{
			this.EffectResOilQueShi.SetActive(false);
		}
	}

	private void SetResCoin()
	{
		this.resCoinLabel.text = HeroInfo.GetInstance().playerRes.resCoin.ToString();
		this.resCoinSprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resCoin / (float)HeroInfo.GetInstance().playerRes.maxCoin;
		if (HeroInfo.GetInstance().playerRes.resCoin >= HeroInfo.GetInstance().playerRes.maxCoin)
		{
			this.resCoinLabel.color = Color.red;
		}
		else
		{
			this.resCoinLabel.color = Color.white;
		}
		if ((float)HeroInfo.GetInstance().playerRes.resCoin <= (float)HeroInfo.GetInstance().playerRes.maxCoin * 0.05f)
		{
			if (this.EffectResCoinQueShi)
			{
				this.EffectResCoinQueShi.SetActive(true);
			}
			else
			{
				this.EffectResCoinQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", this.resCoinLabel.transform).ga;
			}
		}
		else if (this.EffectResCoinQueShi)
		{
			this.EffectResCoinQueShi.SetActive(false);
		}
	}

	private void GetResInfo(ResType resType)
	{
		this.playResInfoLabel();
		StringBuilder stringBuilder = new StringBuilder();
		switch (resType)
		{
		case ResType.金币:
			stringBuilder.Append(LanguageManage.GetTextByKey("金币", "others"));
			MainUIPanelManage.resInfo_All_ZhuJiDi_Ele(resType, stringBuilder);
			MainUIPanelManage.resInfo_Tech(resType, stringBuilder);
			MainUIPanelManage.resInfo_Vip(stringBuilder, VipConst.Enum_VipRight.金币产量);
			MainUIPanelManage.resInfo_Limit(resType, stringBuilder);
			this.resInfoLabel.text = stringBuilder.ToString();
			this.resInfoGameObject.transform.parent = this.coinTransform.transform;
			this.resInfoGameObject.transform.localPosition = Vector3.zero;
			break;
		case ResType.石油:
			stringBuilder.Append(LanguageManage.GetTextByKey("石油", "others"));
			MainUIPanelManage.resInfo_All_ZhuJiDi_Ele(resType, stringBuilder);
			MainUIPanelManage.resInfo_Tech(resType, stringBuilder);
			MainUIPanelManage.resInfo_Vip(stringBuilder, VipConst.Enum_VipRight.石油产量);
			MainUIPanelManage.resInfo_Limit(resType, stringBuilder);
			this.resInfoLabel.text = stringBuilder.ToString();
			this.resInfoGameObject.transform.parent = this.oilTransform.transform;
			this.resInfoGameObject.transform.localPosition = Vector3.zero;
			break;
		case ResType.钢铁:
			stringBuilder.Append(LanguageManage.GetTextByKey("钢铁", "others"));
			MainUIPanelManage.resInfo_All_ZhuJiDi_Ele(resType, stringBuilder);
			MainUIPanelManage.resInfo_Tech(resType, stringBuilder);
			MainUIPanelManage.resInfo_Vip(stringBuilder, VipConst.Enum_VipRight.钢铁产量);
			MainUIPanelManage.resInfo_Limit(resType, stringBuilder);
			this.resInfoLabel.text = stringBuilder.ToString();
			this.resInfoGameObject.transform.parent = this.steelTransform.transform;
			this.resInfoGameObject.transform.localPosition = Vector3.zero;
			break;
		case ResType.稀矿:
			stringBuilder.Append(LanguageManage.GetTextByKey("稀矿", "others"));
			MainUIPanelManage.resInfo_All_ZhuJiDi_Ele(resType, stringBuilder);
			MainUIPanelManage.resInfo_Tech(resType, stringBuilder);
			MainUIPanelManage.resInfo_Vip(stringBuilder, VipConst.Enum_VipRight.稀矿产量);
			MainUIPanelManage.resInfo_Limit(resType, stringBuilder);
			this.resInfoLabel.text = stringBuilder.ToString();
			this.resInfoGameObject.transform.parent = this.rareEarthTransform.transform;
			this.resInfoGameObject.transform.localPosition = Vector3.zero;
			break;
		}
	}

	private static void resInfo_Limit(ResType resType, StringBuilder resInfo)
	{
		resInfo.Append("\n\t");
		resInfo.Append(LanguageManage.GetTextByKey("最大储量", "others"));
		resInfo.Append(":");
		resInfo.Append("\n\t");
		resInfo.Append(HeroInfo.GetInstance().ResHomeLimit_ByTech(resType).ToString());
		resInfo.Append("\n\t");
	}

	private static void resInfo_Tech(ResType resType, StringBuilder resInfo)
	{
		string value = string.Empty;
		string value2 = string.Empty;
		string value3 = string.Empty;
		if (resType == ResType.石油)
		{
			value = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产量);
		}
		if (resType == ResType.钢铁)
		{
			value = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产量);
		}
		if (resType == ResType.稀矿)
		{
			value = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产量);
		}
		value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
		value3 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.资源保护);
		if (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(value2) || !string.IsNullOrEmpty(value3))
		{
			resInfo.Append("\n\t");
			resInfo.Append("[00FF00]");
			resInfo.Append(LanguageManage.GetTextByKey("从科技", "others"));
			resInfo.Append(":");
			resInfo.Append("\n\t");
			resInfo.Append("\n\t");
			if (!string.IsNullOrEmpty(value))
			{
				resInfo.Append("      ");
				resInfo.Append(LanguageManage.GetTextByKey("每小时的产量", "others"));
				resInfo.Append(":");
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(value);
				resInfo.Append("\n\t");
			}
			if (!string.IsNullOrEmpty(value2))
			{
				resInfo.Append("      ");
				resInfo.Append(LanguageManage.GetTextByKey("仓库存储上限", "others"));
				resInfo.Append(":");
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(value2);
				resInfo.Append("\n\t");
			}
			if (!string.IsNullOrEmpty(value3))
			{
				resInfo.Append("      ");
				resInfo.Append(LanguageManage.GetTextByKey("保险库保护", "others"));
				resInfo.Append(":");
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(value3);
				resInfo.Append("\n\t");
			}
			resInfo.Append("[-]");
			resInfo.Append("\n\t");
		}
	}

	private static void resInfo_Vip(StringBuilder resInfo, VipConst.Enum_VipRight type)
	{
		if (HeroInfo.GetInstance().ResProtectNumByVip() > 0 || VipConst.GetVipAddtion(100f, HeroInfo.GetInstance().vipData.VipLevel, type) > 0)
		{
			resInfo.Append("\n\t");
			resInfo.Append("[00FF00]");
			resInfo.Append(LanguageManage.GetTextByKey("从Vip特权", "others"));
			resInfo.Append(":");
			resInfo.Append("\n\t");
			if (VipConst.GetVipAddtion(100f, HeroInfo.GetInstance().vipData.VipLevel, type) > 0)
			{
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(LanguageManage.GetTextByKey("每小时的产量", "others"));
				resInfo.Append(":");
				resInfo.Append("\n\t");
				int i = 0;
				while (i < UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel].rights.Count)
				{
					if (UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel].rights[i].type == type)
					{
						if (UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel].rights[i].isPer)
						{
							resInfo.Append("      ");
							resInfo.Append(string.Format("+{0}%", UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel].rights[i].value));
							break;
						}
						resInfo.Append("      ");
						resInfo.Append(string.Format("+{0}", UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel].rights[i].value));
						break;
					}
					else
					{
						i++;
					}
				}
				resInfo.Append("\n\t");
			}
			if (HeroInfo.GetInstance().ResProtectNumByVip() > 0)
			{
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(LanguageManage.GetTextByKey("保险库保护", "others"));
				resInfo.Append(":");
				resInfo.Append("\n\t");
				resInfo.Append("      ");
				resInfo.Append(HeroInfo.GetInstance().ResProtectNumByVip().ToString());
				resInfo.Append("%");
				resInfo.Append("\n\t");
			}
			resInfo.Append("[-]");
		}
	}

	private static void resInfo_All_ZhuJiDi_Ele(ResType resType, StringBuilder resInfo)
	{
		resInfo.Append("                               ");
		resInfo.Append("\n\t");
		resInfo.Append("\n\t");
		resInfo.Append("      ");
		resInfo.Append(LanguageManage.GetTextByKey("每小时的产量", "others"));
		resInfo.Append(":");
		resInfo.Append("\n\t");
		resInfo.Append("      ");
		resInfo.Append(HeroInfo.GetInstance().ResHomeSpeedByStepEleVip(resType).ToString());
		resInfo.Append("\n\t");
		resInfo.Append("\n\t");
		resInfo.Append("      ");
		resInfo.Append(LanguageManage.GetTextByKey("从主基地", "others"));
		resInfo.Append(":");
		resInfo.Append("\n\t");
		resInfo.Append("      ");
		resInfo.Append(HeroInfo.GetInstance().ResHomeSpeed_NoEleVip(resType).ToString());
		resInfo.Append("\n\t");
		if (HUDTextTool.ResProduceSpeedByPower < 1f)
		{
			resInfo.Append("\n\t");
			resInfo.Append("[FF0000]");
			resInfo.Append(LanguageManage.GetTextByKey("电力减弱", "others"));
			resInfo.Append(":");
			resInfo.Append("\n\t");
			resInfo.Append("\n\t");
			resInfo.Append("      ");
			resInfo.Append(LanguageManage.GetTextByKey("每小时的产量", "others"));
			resInfo.Append(":");
			resInfo.Append("\n\t");
			resInfo.Append("      ");
			resInfo.Append("-");
			resInfo.Append(((1f - HUDTextTool.ResProduceSpeedByPower) * 100f).ToString());
			resInfo.Append("%");
			resInfo.Append("[-]");
			resInfo.Append("\n\t");
		}
	}

	private void playResInfoLabel()
	{
		if (!this.resInfoGameObject.activeSelf)
		{
			this.resInfoGameObject.SetActive(true);
		}
		TweenScale component = this.resInfoGameObject.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	private void ClearScree()
	{
		if (this.restips != null)
		{
			this.restips.gameObject.SetActive(false);
		}
		if (this.resInfoGameObject)
		{
			this.resInfoGameObject.SetActive(false);
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		this.RefreshBuildingStoreNotice();
		if (opcodeCMD == 10110)
		{
			this.ShowApplyInfo(MessagePanel.applyState);
		}
		if (opcodeCMD == 10039)
		{
			this.SetResourceText(true);
			return;
		}
		if (opcodeCMD == 10003)
		{
			this.SetResourceText(false);
			return;
		}
		if (opcodeCMD == 10002)
		{
			this.ShowTopTenInfo();
		}
		if (opcodeCMD == 30101 || opcodeCMD == 10064 || opcodeCMD == 10110)
		{
			if (MessagePanel.isOut)
			{
				this.unReadChatMessage += HeroInfo.GetInstance().chatMessage.ChatTempData.Count;
			}
			if (this.unReadChatMessage > messageItem.maxInt)
			{
				this.unReadChatMessage = messageItem.maxInt;
			}
			this.DisplayMessage(MessagePanel.messageState, false);
			if (MessagePanel.isOut && this.unReadChatMessage > 0)
			{
				this.messageRed.SetActive(true);
				this.messageRed.GetComponentInChildren<UILabel>().text = this.unReadChatMessage.ToString();
			}
			else
			{
				this.messageRed.SetActive(false);
			}
		}
	}

	public void RefreshBuildingStoreNotice()
	{
		if ((from a in UnitConst.GetInstance().buildingConst
		where a.storeType != 0
		select a).Any((NewBuildingInfo a) => HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(a.resIdx) && SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC b) => b.buildingIdx == a.resIdx) < HeroInfo.GetInstance().PlayerBuildingLimit[a.resIdx]))
		{
			this.buildingNotice.gameObject.SetActive(true);
			int num = 0;
			int i;
			for (i = 0; i < UnitConst.GetInstance().buildingConst.Length; i++)
			{
				int num2 = SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC a) => a.buildingIdx == UnitConst.GetInstance().buildingConst[i].resIdx);
				if (UnitConst.GetInstance().buildingConst[i].battleIdFieldId != 0)
				{
					if (UnitConst.GetInstance().BattleFieldConst[UnitConst.GetInstance().buildingConst[i].battleIdFieldId].fightRecord.isFight && HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(UnitConst.GetInstance().buildingConst[i].resIdx) && HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(UnitConst.GetInstance().buildingConst[i].resIdx) && num2 < HeroInfo.GetInstance().PlayerBuildingLimit[UnitConst.GetInstance().buildingConst[i].resIdx])
					{
						num++;
					}
				}
				else if (HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(UnitConst.GetInstance().buildingConst[i].resIdx) && HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(UnitConst.GetInstance().buildingConst[i].resIdx) && num2 < HeroInfo.GetInstance().PlayerBuildingLimit[UnitConst.GetInstance().buildingConst[i].resIdx])
				{
					num++;
				}
			}
			if (num > 0)
			{
				this.buildingNotice.gameObject.SetActive(true);
			}
			else
			{
				this.buildingNotice.gameObject.SetActive(false);
			}
		}
		else
		{
			this.buildingNotice.gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		T_Tower.ClickTowerSendMessage += new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage += new Action(this.ClearScree);
		ClientMgr.GetNet().NetDataHandler.CoinChange += new DataHandler.Data_Change(this.DataHandler_CoinChange);
		ClientMgr.GetNet().NetDataHandler.OilChange += new DataHandler.Data_Change(this.DataHandler_OilChange);
		ClientMgr.GetNet().NetDataHandler.SteelChange += new DataHandler.Data_Change(this.DataHandler_SteelChange);
		ClientMgr.GetNet().NetDataHandler.RareEarthChange += new DataHandler.Data_Change(this.DataHandler_RareEarthChange);
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		ClientMgr.GetNet().NetDataHandler.LevelChange += new DataHandler.Data_Change(this.DataHandler_LevelChange);
		SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		this.ShowTopTenInfo();
		HUDTextTool.inst.Powerhouse();
		this.ShowPlayerInfoLevel();
		this.SetResourceText(true);
		this.downLeft.SetActive(true);
		this.downRight.SetActive(true);
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.HidePanel();
		}
		this.DisplayMessage(MessagePanel.messageState, false);
	}

	public void PlayMedalCollect()
	{
	}

	private void DataHandler_RareEarthChange(int opcodeCMD)
	{
		this.resRareEarthLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resRareEarth, 2f, null);
	}

	private void DataHandler_SteelChange(int opcodeCMD)
	{
		this.resSteelLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resSteel, 2f, null);
	}

	private void DataHandler_OilChange(int opcodeCMD)
	{
		this.resOilLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resOil, 2f, null);
	}

	private void DataHandler_CoinChange(int opcodeCMD)
	{
		this.resCoinLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resCoin, 2f, null);
	}

	private void DataHandler_ExpChange(int opcodeCMD)
	{
	}

	[DebuggerHidden]
	public IEnumerator DisplayExpAdd(Vector3 startPosition, int opcodeCMD)
	{
		MainUIPanelManage.<DisplayExpAdd>c__Iterator88 <DisplayExpAdd>c__Iterator = new MainUIPanelManage.<DisplayExpAdd>c__Iterator88();
		<DisplayExpAdd>c__Iterator.startPosition = startPosition;
		<DisplayExpAdd>c__Iterator.opcodeCMD = opcodeCMD;
		<DisplayExpAdd>c__Iterator.<$>startPosition = startPosition;
		<DisplayExpAdd>c__Iterator.<$>opcodeCMD = opcodeCMD;
		<DisplayExpAdd>c__Iterator.<>f__this = this;
		return <DisplayExpAdd>c__Iterator;
	}

	private void DataHandler_LevelChange(int level)
	{
		this.ShowPlayerInfoLevel();
		this.playerRole.playerExp.width = 0;
		this.playerRole.playerLevelUp.gameObject.SetActive(true);
		this.playerRole.playerLevelUp.Reset();
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		this.RefreshBuildingStoreNotice();
		HUDTextTool.inst.Powerhouse();
	}

	public static void Powerconsumption(int pow, int electricUse, GameObject electricityPow)
	{
		float num = (electricUse <= 0) ? 0f : (100f * (float)pow / (float)electricUse);
		List<Electricity> list = (from a in UnitConst.GetInstance().ElectricityCont.Values
		orderby a.percent descending
		select a).ToList<Electricity>();
		if (electricityPow != null)
		{
			if (pow != 0)
			{
				electricityPow.transform.GetChild(0).gameObject.SetActive(false);
				electricityPow.gameObject.GetComponent<UISprite>().ShaderToNormal();
				electricityPow.GetComponent<UISprite>().gameObject.SetActive(false);
				for (int i = 0; i < list.Count; i++)
				{
					if (num >= (float)list[i].percent)
					{
						MainUIPanelManage.PowerShow(list[i].id, list[i].extraDamage, list[i].buffid, num, electricityPow);
						HUDTextTool.ResProduceSpeedByPower = list[i].actualReduce;
						break;
					}
				}
				electricityPow.SetActive(true && HeroInfo.GetInstance().PlayerElectricPowerPlantLV > 0);
			}
			else
			{
				if (MainUIPanelManage._instance)
				{
					electricityPow.gameObject.GetComponent<UISprite>().ShaderToGray();
					electricityPow.transform.GetChild(0).gameObject.SetActive(true);
				}
				if (HeroInfo.GetInstance().PlayerElectricPowerPlantLV > 0)
				{
					electricityPow.GetComponent<UISprite>().gameObject.SetActive(true);
					Electricity electricity = list[list.Count - 1];
					MainUIPanelManage.PowerShow(4, electricity.extraDamage, electricity.buffid, num, electricityPow);
				}
			}
		}
	}

	public static void PowerShow(int id, int extraDamage, int buffid, float vlou, GameObject electricityPow)
	{
		float num = vlou / 100f * 203f;
		if (num > 203f)
		{
			num = 203f;
		}
		switch (id)
		{
		case 1:
			MainUIPanelManage.oneLowPower = true;
			electricityPow.GetComponent<UISprite>().SetDimensions(18, (int)num);
			electricityPow.GetComponent<UISprite>().spriteName = "1";
			MainUIPanelManage.ElectricityDesLabelcolor = Color.green;
			if (MainUIPanelManage._instance)
			{
				MainUIPanelManage._instance.ElectricityDes = string.Format("{0}\r\n\r\n{1}  \r\n{2}\r\n\r\n{3} \r\n{4}\r\n                    ", new object[]
				{
					LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[id].description, "others"),
					LanguageManage.GetTextByKey("产电量", "others") + ":",
					MainUIPanelManage.power,
					LanguageManage.GetTextByKey("电量消耗", "others") + ":",
					MainUIPanelManage.electricUse
				});
			}
			MainUIPanelManage.extraAttack = (float)extraDamage;
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				SenceManager.inst.towers[i].OnDestyNoElectricityIcon();
			}
			SenceManager.inst.MapElectricity = SenceManager.ElectricityEnum.电力充沛;
			break;
		case 2:
			electricityPow.GetComponent<UISprite>().SetDimensions(18, (int)num);
			electricityPow.GetComponent<UISprite>().spriteName = "2";
			if (MainUIPanelManage._instance)
			{
				MainUIPanelManage._instance.ElectricityDes = string.Format("{0}\r\n\r\n{1}  \r\n{2}\r\n\r\n{3} \r\n{4}\r\n                    ", new object[]
				{
					LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[id].description, "others"),
					LanguageManage.GetTextByKey("产电量", "others") + ":",
					MainUIPanelManage.power,
					LanguageManage.GetTextByKey("电量消耗", "others") + ":",
					MainUIPanelManage.electricUse
				});
			}
			MainUIPanelManage.ElectricityDesLabelcolor = Color.yellow;
			MainUIPanelManage.extraAttack = (float)extraDamage;
			for (int j = 0; j < SenceManager.inst.towers.Count; j++)
			{
				SenceManager.inst.towers[j].OnT_Towermark(MainUIPanelManage.power, UnitConst.GetInstance().ElectricityCont[4].buffid);
			}
			SenceManager.inst.MapElectricity = SenceManager.ElectricityEnum.电力不足;
			if (MainUIPanelManage.oneLowPower)
			{
				MainUIPanelManage.oneLowPower = false;
				AudioManage.inst.audioPlay.Stop();
				AudioManage.inst.PlayAuido("LowPower", false);
			}
			break;
		case 3:
			electricityPow.GetComponent<UISprite>().SetDimensions(18, (int)num);
			electricityPow.GetComponent<UISprite>().spriteName = "3";
			if (MainUIPanelManage._instance)
			{
				MainUIPanelManage._instance.ElectricityDes = string.Format("{0}\r\n\r\n{1}  \r\n{2}\r\n\r\n{3} \r\n{4}\r\n                    ", new object[]
				{
					LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[id].description, "others"),
					LanguageManage.GetTextByKey("产电量", "others") + ":",
					MainUIPanelManage.power,
					LanguageManage.GetTextByKey("电量消耗", "others") + ":",
					MainUIPanelManage.electricUse
				});
			}
			MainUIPanelManage.ElectricityDesLabelcolor = Color.red;
			for (int k = 0; k < SenceManager.inst.towers.Count; k++)
			{
				SenceManager.inst.towers[k].OnT_Towermark(MainUIPanelManage.power, UnitConst.GetInstance().ElectricityCont[4].buffid);
			}
			MainUIPanelManage.extraAttack = (float)extraDamage;
			if (MainUIPanelManage.oneLowPower)
			{
				MainUIPanelManage.oneLowPower = false;
				AudioManage.inst.audioPlay.Stop();
				AudioManage.inst.PlayAuido("LowPower", false);
			}
			SenceManager.inst.MapElectricity = SenceManager.ElectricityEnum.严重不足;
			break;
		case 4:
			electricityPow.GetComponent<UISprite>().SetDimensions(18, (int)num);
			electricityPow.GetComponent<UISprite>().spriteName = "3";
			if (MainUIPanelManage._instance)
			{
				MainUIPanelManage._instance.ElectricityDes = string.Format("{0}\r\n\r\n{1}  \r\n{2}\r\n\r\n{3} \r\n{4}\r\n                    ", new object[]
				{
					LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[id].description, "others"),
					LanguageManage.GetTextByKey("产电量", "others") + ":",
					MainUIPanelManage.power,
					LanguageManage.GetTextByKey("电量消耗", "others") + ":",
					MainUIPanelManage.electricUse
				});
			}
			MainUIPanelManage.ElectricityDesLabelcolor = Color.red;
			MainUIPanelManage.extraAttack = (float)extraDamage;
			for (int l = 0; l < SenceManager.inst.towers.Count; l++)
			{
				SenceManager.inst.towers[l].OnT_Towermark(MainUIPanelManage.power, UnitConst.GetInstance().ElectricityCont[4].buffid);
			}
			if (MainUIPanelManage.oneLowPower)
			{
				MainUIPanelManage.oneLowPower = false;
				AudioManage.inst.audioPlay.Stop();
				AudioManage.inst.PlayAuido("LowPower", false);
			}
			SenceManager.inst.MapElectricity = SenceManager.ElectricityEnum.电力瘫痪;
			break;
		}
	}

	public void SetUpdateTittleInfo(int index)
	{
		if (this.IsFirst)
		{
			this.IsFirst = false;
		}
	}

	private void OnDisable()
	{
		this.resRareEarthLabel.GetComponent<ResLabel>().IsEnd = true;
		this.resSteelLabel.GetComponent<ResLabel>().IsEnd = true;
		this.resOilLabel.GetComponent<ResLabel>().IsEnd = true;
		this.resCoinLabel.GetComponent<ResLabel>().IsEnd = true;
		this.rmbLabel.GetComponent<ResLabel>().IsEnd = true;
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.HidePanel();
		}
		T_Tower.ClickTowerSendMessage -= new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage -= new Action(this.ClearScree);
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.LevelChange -= new DataHandler.Data_Change(this.DataHandler_LevelChange);
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
			ClientMgr.GetNet().NetDataHandler.CoinChange -= new DataHandler.Data_Change(this.DataHandler_CoinChange);
			ClientMgr.GetNet().NetDataHandler.OilChange -= new DataHandler.Data_Change(this.DataHandler_OilChange);
			ClientMgr.GetNet().NetDataHandler.SteelChange -= new DataHandler.Data_Change(this.DataHandler_SteelChange);
			ClientMgr.GetNet().NetDataHandler.RareEarthChange -= new DataHandler.Data_Change(this.DataHandler_RareEarthChange);
		}
	}

	private void SetDesignIcon()
	{
		if (UnitConst.GetInstance().DesighConfigDic[81].value == "0")
		{
			this.Btn_LiBao.SetActive(false);
		}
		else
		{
			this.Btn_LiBao.SetActive(true);
		}
		this.DesignIconGrid.Reposition();
	}
}
