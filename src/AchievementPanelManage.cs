using DG.Tweening;
using DicForUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementPanelManage : FuncUIPanel
{
	public static AchievementPanelManage inst;

	public UIGrid itemGrid;

	public UIScrollView ScrollView;

	public GameObject itemPrefab;

	private List<Achievement> Achievement = new List<Achievement>();

	public void OnDestroy()
	{
		AchievementPanelManage.inst = null;
	}

	public override void Awake()
	{
		AchievementPanelManage.inst = this;
		this.Init();
		this.itemGrid.isRespositonOnStart = false;
	}

	public void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.TaskPane_BackClick, new EventManager.VoidDelegate(this.OnGoBack));
		EventManager.Instance.AddEvent(EventManager.EventType.TaskPane_achievementGetClick, new EventManager.VoidDelegate(this.OnGet_principal));
	}

	public void OnGet_principal(GameObject ga)
	{
		AchievementItem component = ga.transform.parent.parent.GetComponent<AchievementItem>();
		ButtonClick btnClick = ga.GetComponent<ButtonClick>();
		btnClick.IsCanDoEvent = false;
		TaskAndAchievementHandler.CG_CSCompleteAchievement(component.id, delegate(bool isError)
		{
			btnClick.IsCanDoEvent = true;
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	public void OnGoBack(GameObject o)
	{
		FuncUIManager.inst.DestoryFuncUI("AchievementPanel");
	}

	private new void OnEnable()
	{
		this.itemGrid.ClearChild();
		this.itemGrid.Reposition();
		this.ScrollView.ResetPosition();
		DicForU.GetValues<int, Achievement>(UnitConst.GetInstance().AllAchievementConst, this.Achievement);
		this.Achievement = (from a in UnitConst.GetInstance().AllAchievementConst.Values
		orderby a.isCanRecieved descending
		select a).ToList<Achievement>();
		for (int i = 0; i < this.Achievement.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.itemGrid.gameObject, this.itemPrefab);
			AchievementItem component = gameObject.GetComponent<AchievementItem>();
			component.id = this.Achievement[i].id;
			component.InitData();
			gameObject.transform.localPosition = new Vector3(-1200f, this.itemGrid.cellHeight * (float)(-(float)i), 0f);
			gameObject.transform.DOLocalMoveX(0f, 0.2f, false).SetDelay((float)i * 0.1f);
		}
		base.OnEnable();
	}
}
