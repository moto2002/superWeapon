using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameAnnouncementManager : FuncUIPanel
{
	public static GameAnnouncementManager ins;

	public UITable tabel;

	public UIScrollView scorll;

	public GameObject gameItem;

	public void OnDestroy()
	{
		GameAnnouncementManager.ins = null;
	}

	public override void Awake()
	{
		GameAnnouncementManager.ins = this;
	}

	public override void OnEnable()
	{
		this.init();
		this.ShowAnnounceInfo();
		base.OnEnable();
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.GameAnnouncement_ClosePanel, new EventManager.VoidDelegate(this.ClosePanel));
	}

	private void ClosePanel(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("GameAnnouncement");
	}

	public void ShowAnnounceInfo()
	{
		this.tabel.DestoryChildren(true);
		foreach (KeyValuePair<long, NoticeData> current in HeroInfo.GetInstance().gameAnnouncementData.showText)
		{
			if (current.Value.noticeType == 0)
			{
				GameObject gameObject = NGUITools.AddChild(this.tabel.gameObject, this.gameItem);
				GmaeAnnounceItem component = gameObject.GetComponent<GmaeAnnounceItem>();
				component.showinfo.text = current.Value.content.ToString();
			}
		}
		this.tabel.Reposition();
		this.scorll.ResetPosition();
		this.tabel.transform.localPosition = new Vector3(-448.7f, 180f, 0f);
	}

	public void ResfreshGmaeTips()
	{
		CSReadNotice cSReadNotice = new CSReadNotice();
		cSReadNotice.type = 2;
		ClientMgr.GetNet().SendHttp(9010, cSReadNotice, null, null);
		HeroInfo.GetInstance().gameAnnouncementData.isHaveNewAnounce = false;
	}
}
