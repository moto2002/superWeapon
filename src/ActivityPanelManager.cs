using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivityPanelManager : FuncUIPanel
{
	public GameObject OtherBJ;

	public GameObject OtherTitle;

	public GameObject OtherLeft;

	public GameObject OtherRight;

	public GameObject OtherBG;

	public static ActivityPanelManager ins;

	public UILabel startTime;

	public UILabel awardType;

	public GameObject awardPrefab;

	public GameObject itemPrefab;

	public GameObject resPrefab;

	public GameObject skillPrefab;

	public GameObject leftPrefab;

	public UIScrollView leftScroll;

	public UIScrollView rightScroll;

	public UITable leftTabel;

	public UITable rightTabel;

	public static bool isCanGetOnLine;

	public UISprite titleName;

	public int activityType;

	public bool isRefreshOnLine;

	public Dictionary<int, ActivityLeftPrefab> activityState = new Dictionary<int, ActivityLeftPrefab>();

	public Dictionary<int, ShowTipTitle> tipTitle = new Dictionary<int, ShowTipTitle>();

	public GameObject[] tipGame = new GameObject[20];

	public UIGrid bottomGrid;

	public SevenDayPanel sevenPanel;

	public RightPanel rightPanel;

	public UICenterOnChild centerChild;

	public int selectIndex;

	public Body_Model effect;

	public int actType;

	public Body_Model borderEffect;

	public GameObject LeftButton;

	public GameObject RightButton;

	public bool IsOnline_Button;

	public GameObject RecieveActiveBtnBySevenDay;

	public List<GameObject> reIS = new List<GameObject>();

	private int k;

	private int sevenRewardNum;

	private bool IsEveryDayTask
	{
		get
		{
			return UnitConst.GetInstance().DailyTask.Values.Any((DailyTask a) => a.isUIShow && a.type == 1 && a.isCanRecieved && !a.isReceived);
		}
	}

	public void OnDestroy()
	{
		ActivityPanelManager.ins = null;
	}

	private new void Awake()
	{
		ActivityPanelManager.ins = this;
	}

	private new void OnEnable()
	{
		this.InitEvent();
		this.ShowLeftActivityType();
		if (HUDTextTool.isGetActivitiesAward)
		{
			this.ShowLeftState(1);
			this.OtherBG.gameObject.SetActive(true);
			this.OtherBJ.gameObject.SetActive(true);
			this.OtherLeft.gameObject.SetActive(true);
			this.OtherTitle.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.OtherRight.transform.localPosition = new Vector3(0f, 0f, 0f);
			if (MainUIPanelManage._instance.IsOnline_Button)
			{
				this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOnlineAward;
				this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
				this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
				this.ShowOnLine();
				this.OtherBG.gameObject.SetActive(false);
				this.OtherBJ.gameObject.SetActive(false);
				this.OtherLeft.gameObject.SetActive(false);
				this.OtherTitle.transform.localPosition = new Vector3(-100f, 0f, 0f);
				this.OtherRight.transform.localPosition = new Vector3(-100f, 0f, 0f);
			}
			else
			{
				this.ShowSevenDay();
			}
			this.leftTabel.Reposition();
		}
		else
		{
			this.ShowLeftState(this.activityType);
			this.ShowInfo(this.activityType);
		}
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		base.OnEnable();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
	}

	public void ShowInfo(int type)
	{
		switch (type)
		{
		case 1:
			this.ShowSevenDay();
			break;
		case 2:
			this.ShowOnLine();
			break;
		case 3:
			this.ShowEveryDayTask();
			break;
		case 4:
			this.ShowLevelAward();
			break;
		case 5:
			this.ShowHomeAward();
			break;
		case 6:
			this.ShowBattleAward();
			break;
		case 7:
			this.ShowTopAward();
			break;
		case 8:
			this.ShowOthersAward();
			break;
		case 9:
			this.ShowGetMoney();
			break;
		}
	}

	public void ShowLeftState(int type)
	{
		foreach (KeyValuePair<int, ActivityLeftPrefab> current in this.activityState)
		{
			if (current.Key == type)
			{
				current.Value.bgSprite.gameObject.SetActive(true);
				current.Value.activtiyName.color = new Color(1f, 1f, 1f);
			}
			else
			{
				current.Value.bgSprite.gameObject.SetActive(false);
				current.Value.activtiyName.color = new Color(0.996078432f, 8.568627f, 0.419607848f);
			}
		}
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Close, new EventManager.VoidDelegate(this.closeThis));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_EveryDay, new EventManager.VoidDelegate(this.OpenEveryDay));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Home, new EventManager.VoidDelegate(this.OpenHome));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Level, new EventManager.VoidDelegate(this.OpenLevel));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Online, new EventManager.VoidDelegate(this.OpenOnline));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Other, new EventManager.VoidDelegate(this.OpenOthers));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_SevenDay, new EventManager.VoidDelegate(this.OpenSeven));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Top, new EventManager.VoidDelegate(this.OpenTop));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetMonye, new EventManager.VoidDelegate(this.OpenLevelMoney));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Watch, new EventManager.VoidDelegate(this.OpenWathd));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_Battle, new EventManager.VoidDelegate(this.OpenBattle));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetAward, new EventManager.VoidDelegate(this.GetSevenAward));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetEveryDayTask, new EventManager.VoidDelegate(this.GetEveryDayTask));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetEveryDayTaskSkip, new EventManager.VoidDelegate(this.EveryDayTaskSkip));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetOnlineAward, new EventManager.VoidDelegate(this.GetOnlineAward));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetOtherAward, new EventManager.VoidDelegate(this.GetOtherAward));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Activity_Left, new EventManager.VoidDelegate(this.Activity_Left));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_Activity_Right, new EventManager.VoidDelegate(this.Activity_Right));
	}

	private void Activity_Left(GameObject ga)
	{
	}

	private void Activity_Right(GameObject ga)
	{
	}

	public void ShowLeftActivityType()
	{
		this.activityState.Clear();
		this.leftScroll.ResetPosition();
		this.leftTabel.DestoryChildren(true);
		foreach (KeyValuePair<int, List<ActivityClass>> current in HeroInfo.GetInstance().activityClass)
		{
			GameObject gameObject = NGUITools.AddChild(this.leftTabel.gameObject, this.leftPrefab);
			ActivityLeftPrefab component = gameObject.GetComponent<ActivityLeftPrefab>();
			component.actID = current.Key;
			gameObject.name = current.Key.ToString();
			for (int i = 0; i < current.Value.Count; i++)
			{
				if ((current.Value[i].startTimeStr < TimeTools.GetNowTimeSyncServerToDateTime() && current.Value[i].showEndTimeStr > TimeTools.GetNowTimeSyncServerToDateTime()) || current.Value[i].startTimeStr == DateTime.MinValue)
				{
					gameObject.SetActive(true);
					component.activtiyName.text = LanguageManage.GetTextByKey(current.Value[i].btnName, "Activities");
					switch (current.Key)
					{
					case 1:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_SevenDay;
						for (int j = 0; j < SevenDayMgr.state.Length; j++)
						{
							if (SevenDayMgr.state[j] == 0)
							{
								component.red.SetActive(true);
							}
						}
						break;
					case 2:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Online;
						foreach (KeyValuePair<int, LoadReward> current2 in UnitConst.GetInstance().loadReward)
						{
							if (OnLineAward.laod.step > 0)
							{
								if (UnitConst.GetInstance().loadReward[OnLineAward.laod.step].isCanGetOnLine)
								{
									component.red.SetActive(true);
								}
							}
							else
							{
								component.red.SetActive(false);
							}
						}
						break;
					case 3:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_EveryDay;
						component.red.SetActive(this.IsEveryDayTask);
						break;
					case 4:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Level;
						component.red.SetActive(this.isShowRed(4));
						break;
					case 5:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Home;
						component.red.SetActive(this.isShowRed(5));
						break;
					case 6:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Battle;
						component.red.SetActive(this.isShowRed(6));
						break;
					case 7:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Top;
						component.red.SetActive(this.isShowRed(7));
						break;
					case 8:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_Other;
						component.red.SetActive(this.isShowRed(8));
						break;
					case 9:
						component.btnClick.eventType = EventManager.EventType.ActivityPanelManager_GetMonye;
						component.red.SetActive(this.isShowRed(9));
						break;
					}
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
			this.activityState.Add(current.Key, component);
			this.leftTabel.Reposition();
		}
	}

	public bool isShowRed(int activityTtype)
	{
		foreach (KeyValuePair<int, List<ActivityClass>> current in HeroInfo.GetInstance().activityClass)
		{
			for (int i = 0; i < current.Value.Count; i++)
			{
				if (current.Value[i].activityType == activityTtype && current.Value[i].isCanGetAward)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void closeThis(GameObject ga)
	{
		HUDTextTool.isGetActivitiesAward = true;
		FuncUIManager.inst.HideFuncUI("ActivitiesPanel");
	}

	private void OpenSeven(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.ShowSevenDay();
	}

	private void OpenOnline(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		int actID = ga.GetComponent<ActivityLeftPrefab>().actID;
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOnlineAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowOnLine();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
	}

	private void OpenEveryDay(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetEveryDayTask;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowEveryDayTask();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
	}

	private void OpenLevel(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowLevelAward();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void OpenHome(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowHomeAward();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void OpenBattle(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowBattleAward();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void OpenOthers(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowOthersAward();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void OpenTop(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowTopAward();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void OpenLevelMoney(GameObject ga)
	{
		this.ShowLeftState(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
		this.rightPanel.centerChild.CenterOnBack = new Action<GameObject>(this.CallBackCenter);
		this.rightPanel.rightScrollView.onDragFinished = new UIScrollView.OnDragFinished(this.rightPanel.centerChild.OnDragFinished);
		this.ShowGetMoney();
		this.ShowTipState(this.selectIndex);
		this.RrefreshBtn(ga.GetComponent<ActivityLeftPrefab>().actID);
		this.actType = ga.GetComponent<ActivityLeftPrefab>().actID;
	}

	private void CallBackCenter(GameObject ga)
	{
		this.selectIndex = ga.GetComponent<ActivityPrefab>().ConstId;
		this.activityType = ga.GetComponent<ActivityPrefab>().activityType;
		this.rightPanel.getAwardBtn.name = this.selectIndex.ToString();
		this.rightPanel.skip.name = this.selectIndex.ToString();
		this.RrefreshBtn(this.activityType);
		this.ShowTipState(this.selectIndex);
	}

	private void ShowTipState(int id)
	{
		foreach (KeyValuePair<int, ShowTipTitle> current in this.tipTitle)
		{
			if (current.Key == id)
			{
				current.Value.whiteTip.SetActive(true);
			}
			else
			{
				current.Value.whiteTip.SetActive(false);
			}
		}
	}

	private void RrefreshBtn(int id)
	{
		switch (id)
		{
		case 2:
			for (int i = 1; i <= UnitConst.GetInstance().loadReward.Count; i++)
			{
				if (this.selectIndex == UnitConst.GetInstance().loadReward[i].id)
				{
					if ((OnLineAward.laod.step > 0 && OnLineAward.laod.step > UnitConst.GetInstance().loadReward[i].id) || OnLineAward.laod.step < 1)
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(false);
						this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOnlineAward;
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.haveGet.gameObject.SetActive(true);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
					else if (OnLineAward.laod.step > 0 && OnLineAward.laod.step == UnitConst.GetInstance().loadReward[i].id && UnitConst.GetInstance().loadReward[i].isCanGetOnLine)
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(true);
						this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOnlineAward;
						this.rightPanel.haveGet.gameObject.SetActive(false);
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
					else if (OnLineAward.laod.step > 0 && OnLineAward.laod.step == UnitConst.GetInstance().loadReward[i].id && !UnitConst.GetInstance().loadReward[i].isCanGetOnLine)
					{
						this.rightPanel.getAwardBtn.SetActive(false);
						this.rightPanel.OnlineTimeBtn.SetActive(true);
						this.rightPanel.haveGet.gameObject.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
					else
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(false);
						this.rightPanel.cantGet.SetActive(true);
						this.rightPanel.haveGet.gameObject.SetActive(false);
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
				}
			}
			break;
		case 3:
			foreach (DailyTask current in from a in UnitConst.GetInstance().DailyTask.Values
			where a.type == 1 && (a.isUIShow || a.isReceived)
			orderby a.isCanRecieved
			orderby !a.isCanRecieved
			orderby a.isReceived
			select a)
			{
				if (this.selectIndex == current.id)
				{
					if (current.isCanRecieved)
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(true);
						this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetEveryDayTask;
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.haveGet.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
					else if (current.isReceived)
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(false);
						this.rightPanel.haveGet.gameObject.SetActive(true);
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.skip.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
					else
					{
						this.rightPanel.getAwardBtn.gameObject.SetActive(false);
						this.rightPanel.skip.SetActive(true);
						this.rightPanel.haveGet.gameObject.SetActive(false);
						this.rightPanel.OnlineTimeBtn.SetActive(false);
						this.rightPanel.cantGet.SetActive(false);
						this.rightPanel.NoAward.gameObject.SetActive(false);
					}
				}
			}
			break;
		case 4:
			foreach (KeyValuePair<int, List<ActivityClass>> current2 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 4
			select a)
			{
				for (int j = 0; j < current2.Value.Count; j++)
				{
					if (current2.Value[j].activityId == this.selectIndex)
					{
						if (current2.Value[j].isCanGetAward)
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(true);
							this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.haveGet.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
						else if (current2.Value[j].isReceived)
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(true);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
						else
						{
							this.rightPanel.getAwardBtn.SetActive(false);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(true);
							this.rightPanel.haveGet.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
					}
				}
			}
			break;
		case 5:
			foreach (KeyValuePair<int, List<ActivityClass>> current3 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 5
			select a)
			{
				for (int k = 0; k < current3.Value.Count; k++)
				{
					if (this.selectIndex == current3.Value[k].activityId)
					{
						if (current3.Value[k].AwardCount > 0)
						{
							if (current3.Value[k].isCanGetAward)
							{
								this.rightPanel.getAwardBtn.SetActive(true);
								this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else if (current3.Value[k].isReceived)
							{
								this.rightPanel.getAwardBtn.gameObject.SetActive(false);
								this.rightPanel.haveGet.gameObject.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else
							{
								this.rightPanel.getAwardBtn.SetActive(false);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.cantGet.SetActive(true);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
						}
						else
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(true);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
						}
					}
				}
			}
			break;
		case 6:
			foreach (KeyValuePair<int, List<ActivityClass>> current4 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 6
			select a)
			{
				for (int l = 0; l < current4.Value.Count; l++)
				{
					if (this.selectIndex == current4.Value[l].activityId)
					{
						if (current4.Value[l].AwardCount > 0)
						{
							if (current4.Value[l].isCanGetAward)
							{
								this.rightPanel.getAwardBtn.SetActive(true);
								this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else if (current4.Value[l].isReceived)
							{
								this.rightPanel.getAwardBtn.gameObject.SetActive(false);
								this.rightPanel.haveGet.gameObject.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else
							{
								this.rightPanel.getAwardBtn.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(true);
								this.rightPanel.haveGet.SetActive(false);
							}
						}
						else
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(true);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(true);
						}
					}
				}
			}
			break;
		case 7:
			foreach (KeyValuePair<int, List<ActivityClass>> current5 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 7
			select a)
			{
				for (int m = 0; m < current5.Value.Count; m++)
				{
					if (this.selectIndex == current5.Value[m].activityId)
					{
						if (current5.Value[m].AwardCount > 0)
						{
							if (current5.Value[m].isCanGetAward)
							{
								this.rightPanel.getAwardBtn.SetActive(true);
								this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else if (current5.Value[m].isReceived)
							{
								this.rightPanel.getAwardBtn.gameObject.SetActive(false);
								this.rightPanel.haveGet.gameObject.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else
							{
								this.rightPanel.getAwardBtn.gameObject.SetActive(false);
								this.rightPanel.cantGet.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
						}
						else
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(false);
							this.rightPanel.NoAward.SetActive(true);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
						}
					}
				}
			}
			break;
		case 8:
			foreach (KeyValuePair<int, List<ActivityClass>> current6 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 8
			select a)
			{
				for (int n = 0; n < current6.Value.Count; n++)
				{
					if (this.selectIndex == current6.Value[n].activityId)
					{
						if (current6.Value[n].isCanGetAward)
						{
							this.rightPanel.btnDes.GetComponent<BoxCollider>().enabled = false;
							this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
							this.rightPanel.getAwardBtn.SetActive(true);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.haveGet.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
						else if (current6.Value[n].isReceived)
						{
							this.rightPanel.getAwardBtn.gameObject.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(true);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
						else
						{
							this.rightPanel.getAwardBtn.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.NoAward.gameObject.SetActive(false);
						}
					}
				}
			}
			break;
		case 9:
			foreach (KeyValuePair<int, List<ActivityClass>> current7 in from a in HeroInfo.GetInstance().activityClass
			where a.Key == 9
			select a)
			{
				for (int num = 0; num < current7.Value.Count; num++)
				{
					if (this.selectIndex == current7.Value[num].activityId)
					{
						if (current7.Value[num].AwardCount > 0)
						{
							if (current7.Value[num].isCanGetAward)
							{
								this.rightPanel.getAwardBtn.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.getAwardBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOtherAward;
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.haveGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else if (current7.Value[num].isReceived)
							{
								this.rightPanel.getAwardBtn.gameObject.SetActive(false);
								this.rightPanel.haveGet.gameObject.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
							else
							{
								this.rightPanel.cantGet.SetActive(true);
								this.rightPanel.skip.SetActive(false);
								this.rightPanel.OnlineTimeBtn.SetActive(false);
								this.rightPanel.cantGet.SetActive(false);
								this.rightPanel.NoAward.SetActive(false);
								this.rightPanel.NoAward.gameObject.SetActive(false);
							}
						}
						else
						{
							this.rightPanel.getAwardBtn.SetActive(false);
							this.rightPanel.haveGet.gameObject.SetActive(false);
							this.rightPanel.NoAward.SetActive(true);
							this.rightPanel.skip.SetActive(false);
							this.rightPanel.OnlineTimeBtn.SetActive(false);
							this.rightPanel.cantGet.SetActive(false);
						}
					}
				}
			}
			break;
		}
	}

	private void OpenWathd(GameObject ga)
	{
	}

	private void showPanel(int id)
	{
		if (id != 1)
		{
			if (id == 2)
			{
				this.sevenPanel.gameObject.SetActive(false);
				this.rightPanel.gameObject.SetActive(true);
				this.rightPanel.rightGrid.GetComponent<UICenterOnChild>().Recenter();
			}
		}
		else
		{
			this.sevenPanel.gameObject.SetActive(true);
			this.rightPanel.gameObject.SetActive(false);
		}
	}

	private void ClearSevenDayTabel()
	{
		GameObject gameObject = this.sevenPanel.sevenTabel.gameObject;
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

	private void SetTitleName()
	{
		this.titleName.transform.localPosition = new Vector3(120f, 130f, 0f);
		this.titleName.keepAspectRatio = UIWidget.AspectRatioSource.Free;
		this.titleName.SetDimensions(416, 122);
	}

	public void ShowSevenDay()
	{
		this.k = 0;
		this.showPanel(1);
		this.reIS.Clear();
		this.ClearSevenDayTabel();
		if (this.effect == null)
		{
			this.effect = PoolManage.Ins.GetEffectByName("chongzhidali", null);
			this.effect.ga.AddComponent<RenderQueueEdit>();
			this.effect.tr.parent = this.sevenPanel.texture;
			this.effect.tr.localPosition = new Vector3(-109.63f, 62.7f, 0f);
			this.effect.tr.localScale = Vector3.one;
		}
		this.titleName.spriteName = "name1";
		this.SetTitleName();
		this.showSeven();
		for (int i = 0; i < UnitConst.GetInstance().SevenDay.Count; i++)
		{
			if (i == 0)
			{
				this.RecieveActiveBtnBySevenDay = this.sevenPanel.btnAward;
			}
			if (UnitConst.GetInstance().SevenDay[i + 1].goldBox == 0 && i < 6)
			{
				foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().SevenDay[i + 1].res)
				{
					GameObject gameObject = NGUITools.AddChild(this.sevenPanel.sevenTabel.gameObject, this.resPrefab);
					gameObject.name = i.ToString();
					ShowStateLabel component = gameObject.GetComponent<ShowStateLabel>();
					component.numDay = i;
					ActivityRes component2 = gameObject.GetComponent<ActivityRes>();
					if (component2.resEffect == null)
					{
						component2.resEffect = PoolManage.Ins.GetEffectByName("tongyongpinzhi", null);
						component2.resEffect.transform.parent = component2.icon.transform;
						component2.resEffect.ga.AddComponent<RenderQueueEdit>();
						component2.resEffect.tr.localScale = Vector3.one;
						component2.resEffect.tr.localPosition = Vector3.zero;
					}
					this.reIS.Add(gameObject);
					UIEventListener.Get(component2.icon.gameObject).onClick = delegate(GameObject ga)
					{
						this.sevenPanel.btnAward.name = (int.Parse(ga.name) + 1).ToString();
						this.ShowRes(int.Parse(ga.name));
					};
					switch (current.Key)
					{
					case ResType.金币:
						AtlasManage.SetResSpriteName(component2.icon, current.Key);
						component2.count.text = current.Value.ToString();
						break;
					case ResType.石油:
						AtlasManage.SetResSpriteName(component2.icon, current.Key);
						component2.count.text = current.Value.ToString();
						break;
					case ResType.钢铁:
						AtlasManage.SetResSpriteName(component2.icon, current.Key);
						component2.count.text = current.Value.ToString();
						break;
					case ResType.稀矿:
						AtlasManage.SetResSpriteName(component2.icon, current.Key);
						component2.count.text = current.Value.ToString();
						break;
					}
				}
				this.sevenPanel.sevenTabel.Reposition();
				foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().SevenDay[i + 1].items)
				{
					GameObject gameObject2 = NGUITools.AddChild(this.sevenPanel.sevenTabel.gameObject, this.itemPrefab);
					gameObject2.name = i.ToString();
					ShowStateLabel component3 = gameObject2.GetComponent<ShowStateLabel>();
					component3.numDay = i;
					this.reIS.Add(gameObject2);
					ActivityItemPre component4 = gameObject2.GetComponent<ActivityItemPre>();
					if (component4.itemEffect == null)
					{
						component4.itemEffect = PoolManage.Ins.GetEffectByName("tongyongpinzhi", null);
						component4.itemEffect.transform.parent = component4.icon.transform;
						component4.itemEffect.ga.AddComponent<RenderQueueEdit>();
						component4.itemEffect.tr.localScale = Vector3.one;
						component4.itemEffect.tr.localPosition = Vector3.zero;
					}
					UIEventListener.Get(component4.icon.gameObject).onClick = delegate(GameObject ga)
					{
						this.sevenPanel.btnAward.name = (int.Parse(ga.name) + 1).ToString();
						this.ShowRes(int.Parse(ga.name));
					};
					AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
					component4.count.gameObject.SetActive(false);
					component4.name.text = current2.Value.ToString();
					AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
					ShowStateLabel component5 = component4.GetComponent<ShowStateLabel>();
					component5.index = current2.Key;
				}
				this.sevenPanel.sevenTabel.Reposition();
				foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().SevenDay[i + 1].skill)
				{
					GameObject gameObject3 = NGUITools.AddChild(this.sevenPanel.sevenTabel.gameObject, this.skillPrefab);
					gameObject3.name = i.ToString();
					ShowStateLabel component6 = gameObject3.GetComponent<ShowStateLabel>();
					component6.numDay = i;
					this.reIS.Add(gameObject3);
					ActivitySkillPrefab component7 = gameObject3.GetComponent<ActivitySkillPrefab>();
					if (component7.skillEffect == null)
					{
						component7.skillEffect = PoolManage.Ins.GetEffectByName("tongyongpinzhi", null);
						component7.skillEffect.transform.parent = component7.icon.transform;
						component7.skillEffect.ga.AddComponent<RenderQueueEdit>();
						component7.skillEffect.tr.localScale = Vector3.one;
						component7.skillEffect.tr.localPosition = Vector3.zero;
					}
					UIEventListener.Get(component7.icon.gameObject).onClick = delegate(GameObject ga)
					{
						this.sevenPanel.btnAward.name = (int.Parse(ga.name) + 1).ToString();
						this.ShowRes(int.Parse(ga.name));
					};
					AtlasManage.SetSkillSpritName(component7.icon, UnitConst.GetInstance().skillList[current3.Key].icon);
					AtlasManage.SetQuilitySpriteName(component7.bg, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
					component7.name.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillList[current3.Key].name, "skill");
				}
				this.sevenPanel.sevenTabel.Reposition();
				if (UnitConst.GetInstance().SevenDay[i + 1].money > 0)
				{
					GameObject gameObject4 = NGUITools.AddChild(this.sevenPanel.sevenTabel.gameObject, this.resPrefab);
					gameObject4.name = i.ToString();
					ShowStateLabel component8 = gameObject4.GetComponent<ShowStateLabel>();
					component8.numDay = i;
					this.reIS.Add(gameObject4);
					ActivityRes component9 = gameObject4.GetComponent<ActivityRes>();
					if (component9.resEffect == null)
					{
						component9.resEffect = PoolManage.Ins.GetEffectByName("tongyongpinzhi", null);
						component9.resEffect.transform.parent = component9.icon.transform;
						component9.resEffect.ga.AddComponent<RenderQueueEdit>();
						component9.resEffect.tr.localScale = Vector3.one;
						component9.resEffect.tr.localPosition = Vector3.zero;
					}
					UIEventListener.Get(component9.icon.gameObject).onClick = delegate(GameObject ga)
					{
						this.sevenPanel.btnAward.name = (int.Parse(ga.name) + 1).ToString();
						this.ShowRes(int.Parse(ga.name));
					};
					AtlasManage.SetResSpriteName(component9.icon, ResType.钻石);
					component9.count.text = UnitConst.GetInstance().SevenDay[i + 1].money.ToString();
				}
				this.sevenPanel.sevenTabel.Reposition();
			}
			if (UnitConst.GetInstance().SevenDay[i + 1].goldBox == 1)
			{
				GameObject gameObject5 = NGUITools.AddChild(this.sevenPanel.sevenTabel.gameObject, this.itemPrefab);
				gameObject5.name = i.ToString();
				ShowStateLabel component10 = gameObject5.GetComponent<ShowStateLabel>();
				component10.numDay = i;
				this.reIS.Add(gameObject5);
				ActivityItemPre component11 = gameObject5.GetComponent<ActivityItemPre>();
				if (component11.itemEffect == null)
				{
					component11.itemEffect = PoolManage.Ins.GetEffectByName("tongyongpinzhi", null);
					component11.itemEffect.transform.parent = component11.icon.transform;
					component11.itemEffect.ga.AddComponent<RenderQueueEdit>();
					component11.itemEffect.tr.localScale = Vector3.one;
					component11.itemEffect.tr.localPosition = Vector3.zero;
				}
				UIEventListener.Get(component11.icon.gameObject).onClick = delegate(GameObject ga)
				{
					this.sevenPanel.btnAward.name = (int.Parse(ga.name) + 1).ToString();
					this.ShowRes(int.Parse(ga.name));
				};
				AtlasManage.SetUiItemAtlas(component11.icon, UnitConst.GetInstance().ItemConst[107].IconId);
				AtlasManage.SetQuilitySpriteName(component11.quality, UnitConst.GetInstance().ItemConst[107].Quality);
				component11.name.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[107].Name, "item");
				component11.count.text = string.Empty;
			}
			this.sevenPanel.sevenTabel.Reposition();
		}
		this.showGetAwardState();
		this.ShowEffectToday();
	}

	private void showSevenDayState(int state)
	{
		if (state != 0)
		{
			if (state != 1)
			{
				this.sevenPanel.btnAward.gameObject.SetActive(false);
			}
			else
			{
				this.sevenPanel.btnAward.gameObject.SetActive(false);
			}
		}
		else
		{
			this.sevenPanel.btnAward.gameObject.SetActive(true);
		}
	}

	private void ShowRes(int id)
	{
		this.showSevenDayState(SevenDayMgr.state[id]);
		this.DestroySHoRes();
		for (int i = 0; i < 1; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.sevenPanel.showTabel.gameObject, this.reIS[id]);
			this.sevenRewardNum = int.Parse(this.reIS[id].name) + 1;
			ShowStateLabel component = gameObject.GetComponent<ShowStateLabel>();
			if (component.bg.childCount > 0)
			{
				UnityEngine.Object.Destroy(component.bg.GetChild(0).gameObject);
			}
			if (component.showSeventState)
			{
				component.showSeventState.gameObject.SetActive(false);
			}
			if (gameObject.GetComponent<ItemTipsShow2>())
			{
				gameObject.GetComponent<ItemTipsShow2>().Index = component.index;
				gameObject.GetComponent<ItemTipsShow2>().Type = 1;
			}
			this.sevenPanel.showTabel.Reposition();
		}
	}

	private void DestroySHoRes()
	{
		GameObject gameObject = this.sevenPanel.showTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.sevenPanel.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void ShowEffectToday()
	{
		if (this.borderEffect != null)
		{
			UnityEngine.Object.Destroy(this.borderEffect.gameObject);
		}
		for (int i = 0; i < SevenDayMgr.state.Length; i++)
		{
			if (SevenDayMgr.state[i] == 0 && i == this.k - 1)
			{
				this.borderEffect = PoolManage.Ins.GetEffectByName("qiandao", null);
				this.borderEffect.ga.AddComponent<RenderQueueEdit>();
				this.borderEffect.tr.parent = this.reIS[i].GetComponent<ShowStateLabel>().bg;
				this.borderEffect.tr.localPosition = Vector3.zero;
				this.borderEffect.tr.localScale = Vector3.one;
				this.ShowRes(i);
			}
		}
	}

	private void showGetAwardState()
	{
		for (int i = 0; i < SevenDayMgr.state.Length; i++)
		{
			if (SevenDayMgr.state[i] == 0)
			{
				this.k = i + 1;
			}
			if (SevenDayMgr.state[i] == 0 && this.reIS[i].GetComponent<ShowStateLabel>().numDay == i)
			{
				this.reIS[i].GetComponent<ShowStateLabel>().showSeventState.text = LanguageManage.GetTextByKey("可领取", "Activities");
				this.reIS[i].GetComponent<ShowStateLabel>().showSeventState.color = new Color(0.196078435f, 0.972549f, 0.117647059f);
			}
			else if (SevenDayMgr.state[i] == 1 && this.reIS[i].GetComponent<ShowStateLabel>().numDay == i)
			{
				this.reIS[i].GetComponent<ShowStateLabel>().showSeventState.text = LanguageManage.GetTextByKey("已领取", "Activities");
				this.reIS[i].GetComponent<ShowStateLabel>().showSeventState.color = new Color(1f, 0.1882353f, 0.101960786f);
			}
			else
			{
				this.reIS[i].GetComponent<ShowStateLabel>().showSeventState.text = string.Empty;
			}
		}
	}

	private void showSeven()
	{
		for (int i = 0; i < SevenDayMgr.state.Length; i++)
		{
			if (SevenDayMgr.state[i] == 0)
			{
				this.sevenPanel.btnAward.SetActive(true);
				this.sevenPanel.btnAward.name = (i + 1).ToString();
				return;
			}
			if (SevenDayMgr.state[i] == 1)
			{
				this.sevenPanel.btnAward.SetActive(false);
				return;
			}
			this.sevenPanel.btnAward.SetActive(false);
		}
	}

	private void ClearRightPanelGrid()
	{
		GameObject gameObject = this.rightPanel.rightGrid.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.rightPanel.rightScrollView.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void HideAllTip()
	{
		for (int i = 0; i < this.tipGame.Length; i++)
		{
			this.tipGame[i].SetActive(false);
		}
	}

	public void ShowOnLine()
	{
		this.showPanel(2);
		this.tipTitle.Clear();
		this.HideAllTip();
		this.ClearRightPanelGrid();
		this.isRefreshOnLine = false;
		this.rightPanel.rightScrollView.ResetPosition();
		this.rightPanel.rightGrid.Reposition();
		int num = 0;
		this.rightPanel.time.transform.localPosition = new Vector3(95.7f, -184.3f, 0f);
		this.rightPanel.actityTime.text = LanguageManage.GetTextByKey("永久", "Activities");
		this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("不限量", "Activities");
		this.titleName.spriteName = "name2";
		this.SetTitleName();
		GameObject gameObject = null;
		for (int i = 1; i <= UnitConst.GetInstance().loadReward.Count; i++)
		{
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().loadReward[i].des, "Activities");
			component.des.transform.localPosition = new Vector3(-65.5f, 55f, 0f);
			component.getLabelWord.gameObject.SetActive(true);
			component.ConstId = UnitConst.GetInstance().loadReward[i].id;
			component.activityType = 2;
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = UnitConst.GetInstance().loadReward[i].id.ToString();
			this.tipTitle.Add(UnitConst.GetInstance().loadReward[i].id, component2);
			num++;
			if (i == 1)
			{
				gameObject = gameObject2;
			}
			component.watch.SetActive(false);
			component.award.SetActive(false);
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().loadReward[i].res)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().loadReward[i].item)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
				component4.name.text = current2.Value.ToString();
				component4.count.gameObject.SetActive(false);
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current2.Key;
				component5.Type = 1;
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().loadReward[i].skill)
			{
				GameObject gameObject5 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.skillPrefab);
				ActivitySkillPrefab component6 = gameObject5.GetComponent<ActivitySkillPrefab>();
				AtlasManage.SetSkillSpritName(component6.icon, UnitConst.GetInstance().skillList[current3.Key].icon);
				AtlasManage.SetQuilitySpriteName(component6.bg, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
				component6.name.text = UnitConst.GetInstance().skillList[current3.Key].name;
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<ResType, int> current4 in UnitConst.GetInstance().loadReward[i].money)
			{
				GameObject gameObject6 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component7 = gameObject6.GetComponent<ActivityRes>();
				ResType key = current4.Key;
				if (key == ResType.钻石)
				{
					AtlasManage.SetResSpriteName(component7.icon, ResType.钻石);
					component7.count.text = current4.Value.ToString();
				}
			}
			component.showAwardTabel.Reposition();
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.bottomGrid.Reposition();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowEveryDayTask()
	{
		this.showPanel(2);
		this.tipTitle.Clear();
		this.HideAllTip();
		this.DestoryRightPanelGridChild();
		this.rightPanel.time.transform.localPosition = new Vector3(95.7f, -184.3f, 0f);
		this.rightPanel.actityTime.text = LanguageManage.GetTextByKey("永久", "Activities");
		this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("不限量", "Activities");
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		this.titleName.spriteName = "name3";
		this.SetTitleName();
		int num = 0;
		GameObject gameObject = null;
		foreach (DailyTask current in from a in UnitConst.GetInstance().DailyTask.Values
		where a.type == 1 && (a.isUIShow || a.isReceived)
		orderby a.isCanRecieved
		orderby !a.isCanRecieved
		orderby a.isReceived
		select a)
		{
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.ConstId = current.id;
			component.des.text = LanguageManage.GetTextByKey(current.description, "Task") + current.step + LanguageManage.GetTextByKey("次", "Task");
			component.activityType = 3;
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.id.ToString();
			this.tipTitle.Add(current.id, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<int, int> current2 in current.skillAward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.skillPrefab);
				ActivitySkillPrefab component3 = gameObject3.GetComponent<ActivitySkillPrefab>();
				AtlasManage.SetSkillSpritName(component3.icon, UnitConst.GetInstance().skillList[current2.Key].icon);
				AtlasManage.SetQuilitySpriteName(component3.bg, UnitConst.GetInstance().skillList[current2.Key].skillQuality);
				component3.name.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillList[current2.Key].name, "skill");
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.rewardItems)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
			component.showAwardTabel.Reposition();
			if (current.rewardNum > 0)
			{
				GameObject gameObject5 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component6 = gameObject5.GetComponent<ActivityRes>();
				AtlasManage.SetResSpriteName(component6.icon, ResType.钻石);
				component6.count.text = current.rewardNum.ToString();
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<ResType, int> current4 in current.rewardRes)
			{
				GameObject gameObject6 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component7 = gameObject6.GetComponent<ActivityRes>();
				switch (current4.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component7.icon, ResType.金币);
					component7.count.text = current4.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component7.icon, ResType.石油);
					component7.count.text = current4.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component7.icon, ResType.钢铁);
					component7.count.text = current4.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component7.icon, ResType.稀矿);
					component7.count.text = current4.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component7.icon, ResType.钻石);
					component7.count.text = current4.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowLevelAward()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		int num = 0;
		this.tipTitle.Clear();
		GameObject gameObject = null;
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[4])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name4";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.ConstId = current.activityId;
			component.award.SetActive(false);
			component.activityType = current.activityType;
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			this.tipTitle.Add(current.activityId, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
			component.showAwardTabel.Reposition();
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowHomeAward()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		int num = 0;
		GameObject gameObject = null;
		this.tipTitle.Clear();
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[5])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name5";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.getLabelWord.gameObject.SetActive(false);
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.award.gameObject.SetActive(true);
			component.activityType = current.activityType;
			component.ConstId = current.activityId;
			component.awardCount.GetComponent<UILabel>().text = current.AwardCount.ToString();
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			this.tipTitle.Add(current.activityId, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowBattleAward()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		int num = 0;
		this.tipTitle.Clear();
		GameObject gameObject = null;
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[6])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name6";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.ConstId = current.activityId;
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.award.gameObject.SetActive(true);
			component.activityType = current.activityType;
			component.awardCount.GetComponent<UILabel>().text = current.AwardCount.ToString();
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			this.tipTitle.Add(current.activityId, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
			component.showAwardTabel.Reposition();
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowTopAward()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		GameObject gameObject = null;
		int num = 0;
		this.tipTitle.Clear();
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[7])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name7";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.ConstId = current.activityId;
			component.award.gameObject.SetActive(true);
			component.activityType = current.activityType;
			component.awardCount.GetComponent<UILabel>().text = current.AwardCount.ToString();
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			this.tipTitle.Add(current.activityId, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.bottomGrid.Reposition();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowOthersAward()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		int num = 0;
		GameObject gameObject = null;
		this.tipTitle.Clear();
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[8])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name8";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.ConstId = current.activityId;
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.award.gameObject.SetActive(true);
			component.activityType = current.activityType;
			component.watch.GetComponent<UILabel>().text = current.userName;
			component.awardCount.GetComponent<UILabel>().text = current.AwardCount.ToString();
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			this.tipTitle.Add(current.activityId, component2);
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			if (!current.isCanGetAward)
			{
				if (!current.isReceived)
				{
					component.watch.gameObject.SetActive(true);
				}
			}
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.icon.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	public void ShowGetMoney()
	{
		this.showPanel(2);
		this.rightPanel.rightScrollView.ResetPosition();
		this.DestoryRightPanelGridChild();
		this.rightPanel.rightGrid.ClearChild();
		this.HideAllTip();
		this.rightPanel.rightGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
		int num = 0;
		GameObject gameObject = null;
		this.tipTitle.Clear();
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[9])
		{
			this.rightPanel.time.transform.localPosition = new Vector3(6.04f, -184.3f, 0f);
			this.rightPanel.actityTime.text = current.startTimeStr.ToString("yyyy/MM/dd") + "-" + current.endTimeStr.ToString("yyyy/MM/dd");
			this.rightPanel.actitAwardType.text = LanguageManage.GetTextByKey("有限", "Activities");
			this.titleName.spriteName = "name9";
			this.SetTitleName();
			GameObject gameObject2 = NGUITools.AddChild(this.rightPanel.rightGrid.gameObject, this.awardPrefab);
			ActivityPrefab component = gameObject2.GetComponent<ActivityPrefab>();
			component.des.text = LanguageManage.GetTextByKey(current.conditionName, "Activities");
			component.getLabelWord.gameObject.SetActive(false);
			component.des.transform.localPosition = new Vector3(0f, 55f, 0f);
			component.ConstId = current.activityId;
			component.award.gameObject.SetActive(true);
			component.activityType = current.activityType;
			component.awardCount.GetComponent<UILabel>().text = current.AwardCount.ToString();
			this.tipGame[num].gameObject.SetActive(true);
			ShowTipTitle component2 = this.tipGame[num].GetComponent<ShowTipTitle>();
			component2.bgName.name = current.activityId.ToString();
			if (!this.tipTitle.ContainsKey(current.activityId))
			{
				this.tipTitle.Add(current.activityId, component2);
			}
			else
			{
				this.tipTitle[current.activityId] = component2;
			}
			if (num == 0)
			{
				gameObject = gameObject2;
			}
			num++;
			component.showAwardTabel.ClearChild();
			foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
			{
				GameObject gameObject3 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.resPrefab);
				ActivityRes component3 = gameObject3.GetComponent<ActivityRes>();
				switch (current2.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component3.icon, ResType.金币);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component3.icon, ResType.石油);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钢铁);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component3.icon, ResType.稀矿);
					component3.count.text = current2.Value.ToString();
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component3.icon, ResType.钻石);
					component3.count.text = current2.Value.ToString();
					break;
				}
			}
			component.showAwardTabel.Reposition();
			foreach (KeyValuePair<int, int> current3 in current.curActivityItemReward)
			{
				GameObject gameObject4 = NGUITools.AddChild(component.showAwardTabel.gameObject, this.itemPrefab);
				ActivityItemPre component4 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component4.icon, UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component4.quality, UnitConst.GetInstance().ItemConst[current3.Key].Quality);
				component4.count.gameObject.SetActive(false);
				component4.name.text = current3.Value.ToString();
				ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
				component5.Index = current3.Key;
				component5.Type = 1;
			}
		}
		if (gameObject)
		{
			this.rightPanel.centerChild.CenterOn(gameObject.transform);
		}
		this.rightPanel.rightGrid.Reposition();
		this.bottomGrid.Reposition();
		this.rightPanel.rightScrollView.ResetPosition();
	}

	private void DestoryRightPanelGridChild()
	{
		GameObject gameObject = this.rightPanel.rightGrid.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.rightPanel.rightScrollView.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void GetSevenAward(GameObject ga)
	{
		this.activityType = 1;
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().SevenDay[this.sevenRewardNum].res)
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
		HUDTextTool.isGetActivitiesAward = true;
		SevenDayHandler.CS_SevenDay(this.sevenRewardNum, delegate(bool isError)
		{
			if (!isError)
			{
				this.ShowLeftActivityType();
				this.ShowOnLine();
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	private void GetOnlineAward(GameObject ga)
	{
		this.isRefreshOnLine = false;
		HUDTextTool.isGetActivitiesAward = false;
		this.activityType = 2;
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().loadReward[int.Parse(ga.name)].res)
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
		OnLinwAwardHandler.CG_CSOnLine(int.Parse(ga.name), delegate(bool isError)
		{
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	private void GetEveryDayTask(GameObject ga)
	{
		HUDTextTool.isGetActivitiesAward = false;
		this.activityType = 3;
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().DailyTask[int.Parse(ga.name)].rewardRes)
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
		TaskAndAchievementHandler.CG_CSCompleteTask(int.Parse(ga.name), delegate(bool isError)
		{
			ga.GetComponent<ButtonClick>().IsCanDoEvent = true;
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	private void EveryDayTaskSkip(GameObject ga)
	{
	}

	private void GetOtherAward(GameObject ga)
	{
		HUDTextTool.isGetActivitiesAward = false;
		this.activityType = int.Parse(ga.name);
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[this.actType])
		{
			if (current.activityId == int.Parse(ga.name))
			{
				foreach (KeyValuePair<ResType, int> current2 in current.curActivityResReward)
				{
					switch (current2.Key)
					{
					case ResType.金币:
						coin = current2.Value;
						break;
					case ResType.石油:
						oil = current2.Value;
						break;
					case ResType.钢铁:
						steel = current2.Value;
						break;
					case ResType.稀矿:
						earth = current2.Value;
						break;
					}
				}
			}
		}
		if (SenceManager.inst.NoResSpace(coin, oil, steel, earth, true))
		{
			return;
		}
		HomeActivityHandler.CG_CSgetActivityPrize(int.Parse(ga.name), delegate(bool isError)
		{
			if (!isError)
			{
				this.RefreshNum(ga);
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	private void RefreshNum(GameObject go)
	{
		ActivityPrefab[] componentsInChildren = this.centerChild.GetComponentsInChildren<ActivityPrefab>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ActivityPrefab activityPrefab = componentsInChildren[i];
			if (go.name == string.Empty + activityPrefab.ConstId)
			{
				try
				{
					if (activityPrefab.awardCount.transform.parent.gameObject.activeSelf)
					{
						activityPrefab.awardCount.GetComponent<UILabel>().text = string.Empty + (int.Parse(activityPrefab.awardCount.GetComponent<UILabel>().text) - 1);
						break;
					}
				}
				catch
				{
				}
			}
		}
	}

	public void Update()
	{
		if (this.isRefreshOnLine && TimeTools.GetNowTimeSyncServerToDateTime() >= OnLineAward.laod.time)
		{
			UnitConst.GetInstance().loadReward[OnLineAward.laod.step].isCanGetOnLine = true;
			if (MainUIPanelManage._instance.IsOnline_Button)
			{
				this.ShowOnLine();
			}
		}
	}

	public void FixedUpdate()
	{
		TimeSpan timeSpan = OnLineAward.laod.time - TimeTools.GetNowTimeSyncServerToDateTime();
		if (timeSpan.TotalSeconds > 1.0 && OnLineAward.laod.step != 0)
		{
			if (timeSpan.Hours > 0)
			{
				this.rightPanel.OnlineTimeLabel.text = string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Minutes > 0)
			{
				this.rightPanel.OnlineTimeLabel.text = string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Seconds > 1)
			{
				this.rightPanel.OnlineTimeLabel.text = string.Format("{0}", timeSpan.Seconds);
			}
			else
			{
				ActivityPanelManager.ins.isRefreshOnLine = true;
			}
		}
	}
}
