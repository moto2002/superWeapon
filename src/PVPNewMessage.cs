using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PVPNewMessage : MonoBehaviour
{
	public ReportData ThisReportData;

	public GameObject ThisGa;

	public UISprite ThisBG;

	public static PVPNewMessage _inst;

	public UILabel Title;

	public UILabel Des1;

	public UILabel Des2;

	public GameObject LossResPrefab;

	public UIGrid Grid1;

	public Dictionary<int, int> LossResList = new Dictionary<int, int>();

	private bool _2DLock;

	public void OnEnable()
	{
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
	}

	public void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
	}

	private void OnDestroy()
	{
		HUDTextTool.inst.GUIGameObject.SetActive(true);
		NewbieGuidePanel.CallLuaByStart();
		PVPNewMessage._inst = null;
	}

	private void Awake()
	{
		PVPNewMessage._inst = this;
	}

	public void SetInfo()
	{
		this.Title.text = "基地防守消息";
		this.Des1.text = string.Format("您被[FF00FF]{0}[-]攻击了", this.ThisReportData.fighterName);
		this.Des2.text = "您损失了以下资源：";
		Debug.Log("您损失了以下资源：" + this.ThisReportData.lossRes.Count);
		int num = 0;
		foreach (KVStruct current in this.ThisReportData.lossRes)
		{
			num++;
			GameObject gameObject = UnityEngine.Object.Instantiate(this.LossResPrefab) as GameObject;
			gameObject.transform.parent = this.Grid1.transform;
			gameObject.transform.localScale = Vector3.one;
			AtlasManage.SetResSpriteName(gameObject.transform.FindChild("Icon").GetComponent<UISprite>(), (ResType)current.key);
			gameObject.transform.Find("Label").GetComponent<UILabel>().text = "-" + current.value;
		}
		this.Grid1.Reposition();
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_PVPNewMessageClose, new EventManager.VoidDelegate(this.MainPanel_PVPNewMessageClose));
		this.ThisGa.transform.localScale = Vector3.zero;
		TweenScale.Begin(this.ThisGa, 0.2f, Vector3.one);
	}

	private void MainPanel_PVPNewMessageClose(GameObject ga)
	{
		CSBattleReport cSBattleReport = new CSBattleReport();
		cSBattleReport.reportId = this.ThisReportData.id;
		cSBattleReport.type = 6;
		cSBattleReport.video = false;
		ClientMgr.GetNet().SendHttp(5008, cSBattleReport, null, null);
		FuncUIManager.inst.DestoryFuncUI("PVPNewMessage");
		MainUIPanelManage._instance.CheckPVPNewMessage();
	}

	private void Update()
	{
	}
}
