using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TaskNoticePanel : MonoBehaviour
{
	public static TaskNoticePanel _instance;

	public TweenPosition tweenPos;

	public GameObject taskRemindBtn;

	public GameObject btnClick;

	public UILabel taskDes;

	public bool ispass;

	public DailyTask daily;

	public TaskGetawardNew task;

	private Queue<DailyTask> TaskedQueue = new Queue<DailyTask>();

	private bool isShowMainLineTask;

	private void Awake()
	{
		TaskNoticePanel._instance = this;
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MainTaskTittle, new EventManager.VoidDelegate(this.ClickTittle));
		this.taskRemindBtn = base.transform.FindChild("Container/Sprite/Btn").gameObject;
	}

	private void ClickTittle(GameObject go)
	{
	}

	public void FixedUpdate()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void FirstDisplayMainlineTask()
	{
	}

	private void AddTaskInQueue(DailyTask task)
	{
		this.ShowTask(task);
	}

	private void ShowTask(DailyTask task)
	{
		if (task != null)
		{
			this.tweenPos.PlayForward();
			this.isShowMainLineTask = true;
			if (task.isCanRecieved)
			{
				this.taskDes.text = string.Format("{0}:{1}/{2} （已完成）", task.description, task.StepRecord, task.step);
			}
			else
			{
				this.btnClick.transform.localEulerAngles = Vector3.zero;
				this.taskDes.text = string.Format("{0} {1}/{2}  （进行中）", task.description, task.StepRecord, task.step);
			}
		}
		else
		{
			this.tweenPos.PlayForward();
			this.isShowMainLineTask = true;
			FuncUIManager.inst.TaskGetaward.OnTaskPanelFas();
			this.taskDes.text = string.Format("主线任务已完成 ", new object[0]);
		}
	}

	public void OnTask(DailyTask task)
	{
		if (!task.isCanRecieved)
		{
			FuncUIManager.inst.TaskGetaward.OnTaskPanelFas();
		}
	}

	[DebuggerHidden]
	private IEnumerator DisplayTasked()
	{
		TaskNoticePanel.<DisplayTasked>c__Iterator8E <DisplayTasked>c__Iterator8E = new TaskNoticePanel.<DisplayTasked>c__Iterator8E();
		<DisplayTasked>c__Iterator8E.<>f__this = this;
		return <DisplayTasked>c__Iterator8E;
	}
}
