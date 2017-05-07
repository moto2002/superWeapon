using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TopTenPanelManage : FuncUIPanel
{
	public static int topTenRefrshTime = -1;

	public static List<RankingData> rank = new List<RankingData>();

	public UIGrid topTenItemsUITable;

	public UIScrollView scrollView;

	public GameObject topTenitemPrefab;

	public static TopTenPanelManage _ins;

	private bool _2DLock;

	private List<topTenItem> allTopItem = new List<topTenItem>();

	public void OnDestroy()
	{
		if (ShowPlayerLevelManager.ins != null && ShowPlayerLevelManager.ins.gameObject.activeInHierarchy)
		{
			return;
		}
		if (ShowAwardPanelManger._ins != null && ShowAwardPanelManger._ins.gameObject.activeInHierarchy)
		{
			return;
		}
		if (NewarmyInfo.inst != null && NewarmyInfo.inst.gameObject.activeInHierarchy)
		{
			return;
		}
		if (HomeUpOpenBuilding.inst != null && HomeUpOpenBuilding.inst.gameObject.activeInHierarchy)
		{
			return;
		}
		if (HUDTextTool.inst != null)
		{
			HUDTextTool.inst.GUIGameObject.SetActive(true);
		}
		TopTenPanelManage._ins = null;
		NewbieGuidePanel.CallLuaByStart();
	}

	public override void Awake()
	{
		TopTenPanelManage._ins = this;
		this.topTenItemsUITable.isRespositonOnStart = false;
		EventManager.Instance.AddEvent(EventManager.EventType.ToptenPanel_CloseTop, new EventManager.VoidDelegate(this.CloseTopTen));
		EventManager.Instance.AddEvent(EventManager.EventType.ToptenPanle_VistiteSomeOne, new EventManager.VoidDelegate(this.VisitSomeBody));
	}

	public override void OnEnable()
	{
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		this.scrollView.ResetPosition();
		this.topTenItemsUITable.Reposition();
		this.InitRankingData(0);
		base.OnEnable();
	}

	public override void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		ButtonClick.newbiLock = this._2DLock;
		base.OnDisable();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10033)
		{
			this.RefreshData();
		}
	}

	public void CloseTopTen(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("TopTenPanel");
	}

	public void InitRankingData(int dataIndex)
	{
		this.scrollView.isCaneMove_Client = (this.allTopItem.Count > 5);
		if (this.allTopItem.Count <= dataIndex && TopTenPanelManage.rank.Count > dataIndex)
		{
			GameObject gameObject = NGUITools.AddChild(this.topTenItemsUITable.gameObject, this.topTenitemPrefab);
			topTenItem bb = gameObject.GetComponent<topTenItem>();
			bb.transform.localPosition = new Vector3(-1200f, this.topTenItemsUITable.cellHeight * (float)(-(float)dataIndex), 0f);
			bb.InitData(TopTenPanelManage.rank[dataIndex]);
			bb.transform.DOLocalMoveX(120f, 0.16f, false).OnComplete(delegate
			{
				bb.transform.DOLocalMoveX(0f, 0.1f, false);
				bb.NextRankData();
			});
			this.allTopItem.Add(bb);
		}
	}

	private void RefreshData()
	{
		for (int i = 0; i < this.allTopItem.Count; i++)
		{
			this.allTopItem[i].InitData(TopTenPanelManage.rank[i]);
		}
	}

	private void VisitSomeBody(GameObject ga)
	{
		UIManager.curState = SenceState.Visit;
		SenceInfo.CurBattle = null;
		SenceHandler.CG_GetMapData(int.Parse(ga.name), 3, 0, null);
	}
}
