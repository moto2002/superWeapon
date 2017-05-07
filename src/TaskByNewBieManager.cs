using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskByNewBieManager : MonoBehaviour
{
	public static TaskByNewBieManager _inst;

	public GameObject Task_Left;

	public GameObject Task_Right;

	public TaskByNewBie[] Task_Left_Btn;

	public TaskByNewBie[] Task_Right_Btn;

	private List<DailyTask> mainLineTask;

	private DailyTask NowMainTask;

	public int TaskByNewBiePanel_No;

	public void OnDestroy()
	{
		TaskByNewBieManager._inst = null;
	}

	public void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd += new Action(this.NetDataHandler_DataChange);
		}
		if (NewbieGuidePanel.TaskGuideID > 1000)
		{
			this.TaskByNewBiePanel_No = 1;
			this.Task_Right_Btn[0].gameObject.SetActive(false);
			this.Task_Right_Btn[1].gameObject.SetActive(true);
		}
		else
		{
			this.TaskByNewBiePanel_No = 0;
			this.Task_Right_Btn[1].gameObject.SetActive(false);
			this.Task_Right_Btn[0].gameObject.SetActive(true);
		}
		this.RefeshTaskData();
		this.RefeshTaskBtnMessage();
	}

	private void NetDataHandler_DataChange()
	{
		this.RefeshTaskBtnMessage();
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd -= new Action(this.NetDataHandler_DataChange);
		}
	}

	private void Awake()
	{
		TaskByNewBieManager._inst = this;
	}

	private void Start()
	{
		this.Task_Left.gameObject.SetActive(true);
		this.Task_Right.gameObject.SetActive(true);
		EventManager.Instance.AddEvent(EventManager.EventType.TaskByNewBie, new EventManager.VoidDelegate(this.TaskBtn_Back));
		EventManager.Instance.AddEvent(EventManager.EventType.TaskByNewBie_Get, new EventManager.VoidDelegate(this.TaskGetBtn_Back));
	}

	private void TaskBtn_Back(GameObject ga)
	{
		if (NewbieGuideManage._instance.LockTaskByNewBie)
		{
			return;
		}
		TaskByNewBie component = ga.GetComponent<TaskByNewBie>();
		if (component.NewBtnMessage_0)
		{
			component.Message_Close();
		}
		else
		{
			component.Message_Open();
		}
	}

	private void TaskGetBtn_Back(GameObject ga)
	{
		if (NewbieGuideManage._instance.LockTaskByNewBie)
		{
			return;
		}
		DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector3.zero, null);
		if (ga.transform.parent.GetComponent<TaskByNewBie>().Finish)
		{
			int ReceieveTaskId = ga.transform.parent.GetComponent<TaskByNewBie>().Task_id;
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
				}
			});
		}
		else if (!ga.transform.parent.GetComponent<TaskByNewBie>().CannotFinish)
		{
			this.SetNewbieGroup(ga.transform.parent.GetComponent<TaskByNewBie>().NewbieGroup);
		}
	}

	private void TaskPanel_Skip(int task_id)
	{
	}

	public void RefeshTaskBtnMessage()
	{
		if (!this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].gameObject.activeSelf)
		{
			return;
		}
		if (this.NowMainTask == null)
		{
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Title_label = "-";
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].NewbieGroup = "-";
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Finish = false;
			return;
		}
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Title_label = LanguageManage.GetTextByKey(this.NowMainTask.name, "Task");
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].NewbieGroup = this.NowMainTask.NewBieGroup;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Task_Build_index = this.NowMainTask.conditionId;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Task_Build_Second_index = this.NowMainTask.secondConditionId;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Task_Build_Step = this.NowMainTask.step;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].step_all = (float)this.NowMainTask.step;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].step_now = (float)this.NowMainTask.StepRecord;
		this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Task_id = this.NowMainTask.id;
		if (this.NowMainTask.isCanRecieved)
		{
			NewbieGuideManage._instance.LockTaskByNewBie = false;
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Finish = true;
			HUDTextTool.inst.NextLuaCall("任务完成->可领取奖励状态>>>通知Lua", new object[]
			{
				true
			});
		}
		else
		{
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Finish = false;
		}
		if (!this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].NewBtnMessage_0)
		{
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Message_Close();
		}
		else
		{
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Message_Open();
		}
	}

	public void RefeshTaskData()
	{
		this.mainLineTask = UnitConst.GetInstance().ALLMainlineTask();
		if (this.mainLineTask != null && this.mainLineTask.Count > 0)
		{
			for (int i = 0; i < this.mainLineTask.Count; i++)
			{
				if (this.mainLineTask[i].type == 0)
				{
					this.NowMainTask = this.mainLineTask[i];
				}
			}
		}
		this.RefeshTaskBtnMessage();
	}

	public void SomeBuildingEnd(T_Tower tower)
	{
		if (tower.index == 94 && tower.lv == 1)
		{
			FuncUIManager.inst.ArmyFuncHandler_ArmyInfoNew(23);
		}
		if (UnitConst.GetInstance().buildingConst[tower.index].NewbieGroup != string.Empty)
		{
			this.SetNewbieGroup(UnitConst.GetInstance().buildingConst[tower.index].NewbieGroup);
		}
	}

	public void SetNewbieGroup(string newbieGroup)
	{
		Debug.Log("调用引导：" + newbieGroup);
		NewbieGuideWrap.nextNewBi = newbieGroup;
		if (string.IsNullOrEmpty(newbieGroup))
		{
			MainUIPanelManage._instance.TaskPanel_Skip(this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Task_id);
			return;
		}
		string s = newbieGroup.Substring(4, 1);
		string text = newbieGroup.Substring(5, 1);
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
		Debug.Log("GroundID:" + num);
		HUDTextTool.inst.NextLuaCall("任务 调用· ·", new object[]
		{
			true
		});
	}

	private void Update()
	{
		if (HeroInfo.GetInstance().PlayerCommondLv < 2)
		{
			this.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].gameObject.SetActive(false);
			return;
		}
		if (NewbieGuidePanel.TaskGuideID > 1000)
		{
			this.TaskByNewBiePanel_No = 1;
			this.Task_Right_Btn[0].gameObject.SetActive(false);
			this.Task_Right_Btn[1].gameObject.SetActive(true);
		}
		else
		{
			this.TaskByNewBiePanel_No = 0;
			this.Task_Right_Btn[1].gameObject.SetActive(false);
			this.Task_Right_Btn[0].gameObject.SetActive(true);
		}
	}
}
