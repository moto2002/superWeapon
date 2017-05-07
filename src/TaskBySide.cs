using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskBySide : MonoBehaviour
{
	public static TaskBySide _inst;

	public GameObject This_Ga;

	public UISprite This_Button;

	public UILabel[] TaskBySide_Title;

	public UILabel[] TaskBySide_State;

	private UISprite TaskBySide_MainTitle;

	private int state;

	private List<DailyTask> LineTask;

	public void OnDestroy()
	{
		TaskBySide._inst = null;
	}

	private void Awake()
	{
		TaskBySide._inst = this;
	}

	public void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		this.RefreshTaskBySide();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		this.RefreshTaskBySide();
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.TaskBySide, new EventManager.VoidDelegate(this.TaskBtn_Back));
		EventManager.Instance.AddEvent(EventManager.EventType.TaskBySide_Get, new EventManager.VoidDelegate(this.TaskGetBtn_Back));
		this.TaskBySide_MainTitle = base.transform.FindChild("Title").GetComponent<UISprite>();
		this.This_Ga.gameObject.SetActive(false);
		this.RefreshTaskBySide();
	}

	private void TaskBtn_Back(GameObject ga)
	{
		if (!base.transform.GetComponent<UISprite>().enabled)
		{
			return;
		}
		this.This_Ga.gameObject.SetActive(!this.This_Ga.gameObject.activeSelf);
	}

	private void TaskGetBtn_Back(GameObject ga)
	{
		int num = int.Parse(ga.name);
		if (this.LineTask.Count > num)
		{
			if (this.LineTask[num].isCanRecieved)
			{
				int ReceieveTaskId = this.LineTask[num].id;
				int coin = 0;
				int oil = 0;
				int steel = 0;
				int earth = 0;
				foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().DailyTask[ReceieveTaskId].rewardRes)
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
				ButtonClick btnClick = ga.GetComponent<ButtonClick>();
				btnClick.IsCanDoEvent = false;
				TaskAndAchievementHandler.CG_CSCompleteTask(ReceieveTaskId, delegate(bool isError)
				{
					btnClick.IsCanDoEvent = true;
					if (!isError)
					{
						ShowAwardPanelManger.showAwardList();
						foreach (DailyTask current2 in UnitConst.GetInstance().ALLMainlineTask())
						{
							if (current2.id == ReceieveTaskId)
							{
								current2.isReceived = true;
							}
						}
						this.RefreshTaskBySide();
					}
				});
			}
			else
			{
				MainUIPanelManage._instance.TaskPanel_Skip(this.LineTask[num].id);
			}
		}
	}

	private void RefreshTaskBySide()
	{
		if (HeroInfo.GetInstance().PlayerCommondLv < 3)
		{
			base.transform.GetComponent<UISprite>().enabled = false;
			if (this.TaskBySide_MainTitle)
			{
				this.TaskBySide_MainTitle.enabled = false;
			}
			base.transform.GetComponent<BoxCollider>().enabled = false;
			return;
		}
		base.transform.GetComponent<UISprite>().enabled = false;
		if (this.TaskBySide_MainTitle)
		{
			this.TaskBySide_MainTitle.enabled = false;
		}
		base.transform.GetComponent<BoxCollider>().enabled = false;
		if (SenceInfo.curMap.IsMyHome && SenceManager.inst.MainBuilding && SenceManager.inst.MainBuilding.lv >= 2)
		{
			base.transform.GetComponent<UISprite>().enabled = true;
			if (this.TaskBySide_MainTitle)
			{
				this.TaskBySide_MainTitle.enabled = true;
			}
			base.transform.GetComponent<BoxCollider>().enabled = true;
		}
		this.LineTask = (from a in UnitConst.GetInstance().ALLlineTask()
		where a.type == 2
		orderby a.isCanRecieved descending
		select a).ToList<DailyTask>();
		if (this.LineTask.Count > 0)
		{
			for (int i = 0; i < 3; i++)
			{
				if (i < this.LineTask.Count)
				{
					this.TaskBySide_State[i].name = string.Empty + i;
					this.TaskBySide_Title[i].text = LanguageManage.GetTextByKey(this.LineTask[i].description, "Task");
					if (this.LineTask[i].isCanRecieved)
					{
						this.TaskBySide_State[i].text = LanguageManage.GetTextByKey("领取奖励", "Task");
						if (base.transform.GetComponent<UISprite>().enabled)
						{
							this.This_Ga.gameObject.SetActive(true);
						}
					}
					else
					{
						this.TaskBySide_State[i].text = LanguageManage.GetTextByKey("跳转", "Task");
					}
				}
				else
				{
					this.TaskBySide_State[i].name = string.Empty + i;
					this.TaskBySide_Title[i].text = "-";
					this.TaskBySide_State[i].text = "-";
				}
			}
		}
	}

	private void Update()
	{
	}
}
