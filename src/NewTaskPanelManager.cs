using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewTaskPanelManager : FuncUIPanel
{
	public static NewTaskPanelManager ins;

	public UISprite mainLineTaskBtn;

	public UISprite dailyTaskBtn;

	public UIGrid TaskGrid;

	public UIScrollView scrollView;

	public List<NewDailyTask> TaskUI_List = new List<NewDailyTask>();

	private List<ResType> restypeGetKey = new List<ResType>();

	public GameObject resPrefab;

	public GameObject itemPrefab;

	public GameObject skillPrefab;

	public GameObject taskItemPrefab;

	public GameObject zhuxianTaskNotice;

	public GameObject DailyTaskNotice;

	private bool isOpenLineTask = true;

	private List<DailyTask> LineTask;

	public int mainTaskType;

	public void OnDestroy()
	{
		NewTaskPanelManager.ins = null;
	}

	public override void Awake()
	{
		NewTaskPanelManager.ins = this;
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_DailyTask, new EventManager.VoidDelegate(this.DailyBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_MainTask, new EventManager.VoidDelegate(this.MainBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_GetDailyTaskBtn, new EventManager.VoidDelegate(this.GetMainBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_GetMainTaskBtn, new EventManager.VoidDelegate(this.GetMainBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_ClosePanel, new EventManager.VoidDelegate(this.ClostPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.NewTaskPanel_Skip, new EventManager.VoidDelegate(this.TaskPanel_Skip));
	}

	public override void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
		if (this.isOpenLineTask)
		{
			this.MainBtnClick(null);
		}
		else
		{
			this.DailyBtnClick(null);
		}
	}

	private void Start()
	{
		this.TaskGrid.isRespositonOnStart = false;
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10037)
		{
			this.DisPlay();
		}
	}

	private void SetUI()
	{
		this.mainLineTaskBtn.spriteName = "页签2";
		this.mainLineTaskBtn.GetComponent<BoxCollider>().enabled = false;
		this.mainLineTaskBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.none;
		this.dailyTaskBtn.spriteName = "页签1";
		this.dailyTaskBtn.GetComponent<BoxCollider>().enabled = true;
		this.dailyTaskBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.TaskPane_principal_DayClick;
	}

	private void DailyBtnClick(GameObject go)
	{
		this.ChangeDaiyBtnState(false);
		GameTools.RemoveChilderns(this.TaskGrid.transform);
		this.scrollView.ResetPosition();
		this.TaskGrid.Reposition();
		this.TaskUI_List.Clear();
		this.LineTask = (from a in UnitConst.GetInstance().ALLDailyTask()
		orderby a.isCanRecieved descending
		select a).ToList<DailyTask>();
		if (this.LineTask.Count > 0)
		{
			for (int i = 0; i < this.LineTask.Count; i++)
			{
				GameObject gameObject = NGUITools.AddChild(this.TaskGrid.gameObject, this.taskItemPrefab);
				NewDailyTask component = gameObject.GetComponent<NewDailyTask>();
				component.taskPlanning = this.LineTask[i];
				component.InitData();
				this.TaskUI_List.Add(component);
				gameObject.transform.localPosition = new Vector3(-1200f, this.TaskGrid.cellHeight * (float)(-(float)i), 0f);
				gameObject.transform.DOLocalMoveX(0f, 0.2f, false).SetDelay((float)i * 0.1f);
			}
		}
	}

	private void RefreshData()
	{
		for (int i = 0; i < this.TaskUI_List.Count; i++)
		{
			this.TaskUI_List[i].RefreshData();
		}
		this.TaskUI_List = (from a in this.TaskUI_List
		orderby a.taskPlanning.isCanRecieved descending
		select a).ToList<NewDailyTask>();
		for (int j = 0; j < this.TaskUI_List.Count; j++)
		{
			this.TaskUI_List[j].transform.localPosition = new Vector3(-1200f, this.TaskGrid.cellHeight * (float)(-(float)j), 0f);
			this.TaskUI_List[j].transform.DOLocalMoveX(0f, 0.2f, false).SetDelay((float)j * 0.1f);
		}
	}

	private void MainBtnClick(GameObject go)
	{
		GameTools.RemoveChilderns(this.TaskGrid.transform);
		this.ChangeDaiyBtnState(true);
		this.scrollView.ResetPosition();
		this.TaskGrid.Reposition();
		this.TaskUI_List.Clear();
		this.LineTask = (from a in UnitConst.GetInstance().ALLlineTask()
		orderby a.type == 0 descending
		orderby a.isCanRecieved descending
		select a).ToList<DailyTask>();
		if (this.LineTask.Count > 0)
		{
			for (int i = 0; i < this.LineTask.Count; i++)
			{
				GameObject gameObject = NGUITools.AddChild(this.TaskGrid.gameObject, this.taskItemPrefab);
				NewDailyTask component = gameObject.GetComponent<NewDailyTask>();
				component.taskPlanning = this.LineTask[i];
				component.InitData();
				this.TaskUI_List.Add(component);
				gameObject.transform.localPosition = new Vector3(-1200f, this.TaskGrid.cellHeight * (float)(-(float)i), 0f);
				gameObject.transform.DOLocalMoveX(0f, 0.2f, false).SetDelay((float)i * 0.1f);
			}
		}
	}

	private void GetMainBtnClick(GameObject ga)
	{
		int num = int.Parse(ga.name);
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().DailyTask[num].rewardRes)
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
		TaskAndAchievementHandler.CG_CSCompleteTask(num, delegate(bool isError)
		{
			btnClick.IsCanDoEvent = true;
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	private void TaskPanel_Skip(GameObject ga)
	{
		int task_id = int.Parse(ga.name);
		MainUIPanelManage._instance.TaskPanel_Skip(task_id);
	}

	private void ChangeDaiyBtnState(bool TaskType)
	{
		this.isOpenLineTask = TaskType;
		if (TaskType)
		{
			this.mainLineTaskBtn.spriteName = "页签1";
			this.mainLineTaskBtn.GetComponent<BoxCollider>().enabled = false;
			this.dailyTaskBtn.spriteName = "页签2";
			this.dailyTaskBtn.GetComponent<BoxCollider>().enabled = true;
		}
		else
		{
			this.mainLineTaskBtn.spriteName = "页签2";
			this.mainLineTaskBtn.GetComponent<BoxCollider>().enabled = true;
			this.dailyTaskBtn.spriteName = "页签1";
			this.dailyTaskBtn.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void ShowTaskNotice()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		bool active = UnitConst.GetInstance().DailyTask.Values.Any((DailyTask a) => a.isUIShow && (a.type == 0 || a.type == 2) && a.isCanRecieved && !a.isReceived);
		this.zhuxianTaskNotice.SetActive(active);
		bool active2 = UnitConst.GetInstance().DailyTask.Values.Any((DailyTask a) => a.isUIShow && a.type == 1 && a.isCanRecieved && !a.isReceived);
		this.DailyTaskNotice.SetActive(active2);
	}

	private void ClostPanel(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("NewTaskPanel");
	}

	public void DisPlay()
	{
		if (base.gameObject.activeSelf)
		{
			this.ShowTaskNotice();
			this.RefreshData();
		}
	}
}
