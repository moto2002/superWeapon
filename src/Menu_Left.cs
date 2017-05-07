using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Menu_Left : IMonoBehaviour
{
	private bool isFirstMenu = true;

	private Vector3 menuExtendOnPos;

	private Vector3 menuExtendOffPos;

	public Transform menuAni_1;

	public Transform menuAni_2;

	public Transform menuAni_3;

	public GameObject GNotice;

	public GameObject EmailNotice;

	public GameObject AchieveNotice;

	public GameObject taskNotice;

	public GameObject reportNotice;

	private bool isOpen;

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		T_Tower.ClickTowerSendMessage -= new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage -= new Action(this.ClearScree);
	}

	public void OnEnable()
	{
		this.SetReportNotice();
		this.SetEmailNotice();
		this.SetTaskNotice();
		this.SetAchieveNotice();
		this.SetGNotice();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		T_Tower.ClickTowerSendMessage += new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage += new Action(this.ClearScree);
	}

	private void ClearScree()
	{
		this.Clear();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10044)
		{
			this.SetAchieveNotice();
			this.SetGNotice();
		}
		if (opcodeCMD == 10037)
		{
			this.SetTaskNotice();
			this.SetGNotice();
		}
		if (opcodeCMD == 10055)
		{
			this.SetEmailNotice();
			this.SetGNotice();
		}
		if (opcodeCMD == 10018)
		{
			this.SetReportNotice();
			this.SetGNotice();
		}
	}

	private void SetReportNotice()
	{
		this.reportNotice.SetActive(HUDTextTool.ReportRedNotice);
	}

	private void SetTaskNotice()
	{
		this.taskNotice.SetActive(UnitConst.GetInstance().DailyTask.Any((KeyValuePair<int, DailyTask> a) => a.Value.isUIShow && a.Value.isCanRecieved));
	}

	private void SetAchieveNotice()
	{
		this.AchieveNotice.SetActive(UnitConst.GetInstance().AllAchievementConst.Any((KeyValuePair<int, Achievement> a) => a.Value.isCanRecieved));
	}

	private void SetEmailNotice()
	{
		this.EmailNotice.SetActive(EmailManager.GetIns().HaveNotice());
	}

	private void SetGNotice()
	{
		this.GNotice.SetActive(this.EmailNotice.activeSelf || this.AchieveNotice.activeSelf || this.taskNotice.activeSelf || this.reportNotice.activeSelf);
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MenuBtn, new EventManager.VoidDelegate(this.DisplayMenuBtn));
	}

	private void DisplayMenuBtn(GameObject go)
	{
		AudioManage.inst.PlayAuido("openUI", false);
		if (this.isFirstMenu)
		{
			this.menuExtendOffPos = this.tr.localPosition;
			this.menuExtendOnPos = this.menuExtendOffPos + new Vector3(240f, 0f, 0f);
			this.tr.GetChild(1).gameObject.transform.localRotation = new Quaternion(0f, 0f, -180f, 0f);
			this.isFirstMenu = false;
		}
		if (Vector3.Distance(this.tr.localPosition, this.menuExtendOnPos) > Vector3.Distance(this.tr.localPosition, this.menuExtendOffPos))
		{
			TweenPosition.Begin(go, 0.2f, this.menuExtendOnPos).SetOnFinished(new EventDelegate(delegate
			{
				this.tr.DOShakePosition(0.3f, new Vector3(15f, 0f, 0f), 20, 90f, false);
			}));
			base.StartCoroutine(this.PlayMenuAni());
			this.tr.GetChild(1).gameObject.transform.localRotation = new Quaternion(0f, 0f, -180f, 0f);
			this.isOpen = true;
		}
		else
		{
			this.isOpen = false;
			this.tr.GetChild(1).gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			TweenPosition.Begin(go, 0.2f, this.menuExtendOffPos).SetOnFinished(new EventDelegate(delegate
			{
				this.tr.DOShakePosition(0.3f, new Vector3(-15f, 0f, 0f), 20, 90f, false);
			}));
		}
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.gameObject.SetActive(false);
			MainUIPanelManage._instance.OpenPanelMian();
		}
	}

	[DebuggerHidden]
	public IEnumerator PlayMenuAni()
	{
		Menu_Left.<PlayMenuAni>c__Iterator89 <PlayMenuAni>c__Iterator = new Menu_Left.<PlayMenuAni>c__Iterator89();
		<PlayMenuAni>c__Iterator.<>f__this = this;
		return <PlayMenuAni>c__Iterator;
	}

	private void Clear()
	{
		if (!this.isFirstMenu && this.isOpen)
		{
			this.isOpen = false;
			TweenPosition.Begin(this.ga, 0.5f, this.menuExtendOffPos);
		}
	}
}
