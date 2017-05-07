using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskGetawardNew : FuncUIPanel
{
	public static TaskGetawardNew init;

	public GameObject itemPrf;

	public GameObject ResItem;

	public List<Transform> trs;

	public UIGrid grid;

	public GameObject btn;

	private AchievementPanelManage taskManage;

	public static bool isPanelShow;

	public TweenScale tween;

	public UILabel Info;

	public void OnDestroy()
	{
		TaskGetawardNew.init = null;
	}

	private void Start()
	{
		this.OnItemInfo();
	}

	private void inits()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.TaskPane_Principal_LingQu, new EventManager.VoidDelegate(this.OnGetClick));
	}

	public override void Awake()
	{
		TaskGetawardNew.init = this;
		this.inits();
	}

	public void OnGetClick(GameObject go)
	{
		if (UnitConst.GetInstance().CanRecieveMainlineTask == null)
		{
			return;
		}
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().CanRecieveMainlineTask.rewardRes)
		{
			switch (current.Key)
			{
			case ResType.金币:
				HUDTextTool.inst.SetText(string.Format("获得金币+{0}", current.Value), HUDTextTool.TextUITypeEnum.Num1);
				break;
			case ResType.石油:
				CoroutineInstance.DoJob(ResFly2.CreateRes(go.transform, ResType.石油, current.Value, null, null));
				break;
			case ResType.钢铁:
				HUDTextTool.inst.SetText(string.Format("获得钢铁+{0}", current.Value), HUDTextTool.TextUITypeEnum.Num1);
				break;
			case ResType.稀矿:
				HUDTextTool.inst.SetText(string.Format("获得稀矿+{0}", current.Value), HUDTextTool.TextUITypeEnum.Num1);
				break;
			}
		}
		TaskAndAchievementHandler.CG_CSCompleteTask(UnitConst.GetInstance().CanRecieveMainlineTask.id, null);
		UnitConst.GetInstance().CanRecieveMainlineTask.isReceived = true;
		this.OnDestyPanel();
	}

	public void TaskInfo(string TaskinfoShow)
	{
		if (TaskinfoShow == "New Label")
		{
			this.Info.text = string.Empty;
		}
		else
		{
			this.Info.text = TaskinfoShow;
		}
	}

	public void OnTaskPanelFas()
	{
		this.tween.PlayReverse();
		this.tween.onFinished.Clear();
		this.tween.AddOnFinished(new EventDelegate.Callback(this.OnDestyPanel));
	}

	public void OnDestyPanel()
	{
		this.Info.text = string.Empty;
		base.gameObject.SetActive(false);
		FuncUIManager.inst.HideFuncUI("TaskJiangliPanel");
	}

	public void OnTaskPanelShow()
	{
		this.tween.PlayForward();
		this.tween.onFinished.Clear();
		this.tween.AddOnFinished(new EventDelegate.Callback(this.OnShowPanel));
		base.gameObject.SetActive(true);
		base.gameObject.transform.parent = FuncUIManager.inst.transform;
		base.gameObject.transform.localScale = Vector3.one;
	}

	public void OnShowPanel()
	{
		base.gameObject.SetActive(true);
	}

	public void OnItemInfo()
	{
		this.grid.ClearChild();
		this.trs = new List<Transform>();
		if (UnitConst.GetInstance().CanRecieveMainlineTask == null)
		{
			return;
		}
		foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().CanRecieveMainlineTask.rewardItems)
		{
			TaskItemNew component = NGUITools.AddChild(this.grid.gameObject, this.itemPrf).GetComponent<TaskItemNew>();
			component.gameObject.SetActive(true);
			component.icon.spriteName = UnitConst.GetInstance().ItemConst[current.Key].IconId;
			component.Name.text = UnitConst.GetInstance().ItemConst[current.Key].Name;
			this.trs.Add(component.gameObject.transform);
		}
		foreach (KeyValuePair<ResType, int> current2 in UnitConst.GetInstance().CanRecieveMainlineTask.rewardRes)
		{
			GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.ResItem);
			gameObject.gameObject.SetActive(true);
			AtlasManage.SetResSpriteName(gameObject.GetComponent<UISprite>(), current2.Key);
			gameObject.GetComponent<TaskItemNew>().Name.text = current2.Value.ToString();
			this.trs.Add(gameObject.transform);
		}
		switch (this.trs.Count)
		{
		case 1:
			this.grid.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			break;
		case 2:
			this.grid.gameObject.transform.localPosition = new Vector3(-38f, 0f, 0f);
			break;
		case 3:
			this.grid.gameObject.transform.localPosition = new Vector3(-93f, 0f, 0f);
			break;
		case 4:
			this.grid.gameObject.transform.localPosition = new Vector3(-142f, 0f, 0f);
			break;
		case 5:
			this.grid.gameObject.transform.localPosition = new Vector3(-201f, 0f, 0f);
			break;
		}
		this.grid.Reposition();
	}
}
