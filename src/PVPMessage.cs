using System;
using System.Collections.Generic;
using UnityEngine;

public class PVPMessage : FuncUIPanel
{
	public static PVPMessage inst;

	public GameObject AttButton;

	public GameObject DefButton;

	public UILabel AttButtonLabel;

	public UILabel DefButtonLabel;

	private Color color_open;

	private Color color_close;

	public UIScrollView PVP_ScrollView;

	public UIGrid PVP_Grid;

	public GameObject PVP_Item;

	public long PVPFightBackID;

	public GameObject AddRes_Prefab;

	public GameObject DesSoldier_Prefab;

	public List<ReportData> Att_PVPMessageList = new List<ReportData>();

	public List<ReportData> Def_PVPMessageList = new List<ReportData>();

	private List<ReportData> PVPMessageList;

	public void OnDestroy()
	{
		PVPMessage.inst = null;
	}

	public override void OnEnable()
	{
		if (HUDTextTool.ReportRedNotice)
		{
			HUDTextTool.ReportRedNotice = false;
			PVPHandler.CS_PVPMesRead();
		}
		base.OnEnable();
	}

	public override void Awake()
	{
		PVPMessage.inst = this;
		this.ReStart();
		this.InitEvent();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_FightBack, new EventManager.VoidDelegate(this.PVPMessage_FightBack));
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_FightWatch, new EventManager.VoidDelegate(this.PVPMessage_FightWatch));
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_AttType, new EventManager.VoidDelegate(this.PVPMessage_AttType));
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_DefType, new EventManager.VoidDelegate(this.PVPMessage_DefType));
		EventManager.Instance.AddEvent(EventManager.EventType.PVPMessage_Close, new EventManager.VoidDelegate(this.PVPMessage_Close));
	}

	private void PVPMessage_FightBack(GameObject go)
	{
		PVPMessageItem componentInParent = go.GetComponentInParent<PVPMessageItem>();
		PVPHandler.CS_PVPFightBack(componentInParent.M_fightback_id, componentInParent.M_id);
	}

	private void PVPMessage_FightWatch(GameObject go)
	{
		PVPMessageItem componentInParent = go.GetComponentInParent<PVPMessageItem>();
		PVPHandler.CS_PVPReport(componentInParent.M_id);
	}

	private void PVPMessage_AttType(GameObject go)
	{
		this.Set_PVPMessageList(PVPType.Att);
	}

	private void PVPMessage_DefType(GameObject go)
	{
		this.Set_PVPMessageList(PVPType.Def);
	}

	private void PVPMessage_Close(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("PVPMessage");
	}

	private void ReStart()
	{
		this.color_open = new Color(1f, 1f, 1f, 1f);
		this.color_close = new Color(0.671f, 0.671f, 0.671f, 1f);
		this.Att_PVPMessageList.Clear();
		this.Def_PVPMessageList.Clear();
		foreach (KeyValuePair<long, ReportData> current in HeroInfo.GetInstance().PVPReportDataList)
		{
			if (current.Value.fighterId == HeroInfo.GetInstance().userId)
			{
				this.Att_PVPMessageList.Add(current.Value);
			}
			else
			{
				this.Def_PVPMessageList.Add(current.Value);
			}
		}
		if (PVPMessage.inst)
		{
			PVPMessage.inst.SetListFormTime();
			PVPMessage.inst.Set_PVPMessageList(PVPType.Att);
		}
		UnityEngine.Object.Destroy(this.AttButton.GetComponent<UIButton>());
		UnityEngine.Object.Destroy(this.DefButton.GetComponent<UIButton>());
	}

	public void RedRot(bool set)
	{
	}

	public void Get_PVPMessageList()
	{
	}

	public void SetListFormTime()
	{
		List<ReportData> list = new List<ReportData>();
		long num = 0L;
		ReportData item = new ReportData();
		int count = this.Att_PVPMessageList.Count;
		for (int i = 0; i < count; i++)
		{
			foreach (ReportData current in this.Att_PVPMessageList)
			{
				if (current.id > num || num == 0L)
				{
					num = current.id;
					item = current;
				}
			}
			this.Att_PVPMessageList.Remove(item);
			num = 0L;
			list.Add(item);
		}
		this.Att_PVPMessageList = list;
		list = new List<ReportData>();
		num = 0L;
		item = new ReportData();
		count = this.Def_PVPMessageList.Count;
		for (int j = 0; j < count; j++)
		{
			foreach (ReportData current2 in this.Def_PVPMessageList)
			{
				if (current2.id > num || num == 0L)
				{
					num = current2.id;
					item = current2;
				}
			}
			this.Def_PVPMessageList.Remove(item);
			num = 0L;
			list.Add(item);
		}
		this.Def_PVPMessageList = list;
	}

	public void Set_PVPMessageList(PVPType pvptype)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.PVP_Grid.ClearChild();
		if (pvptype != PVPType.Att)
		{
			if (pvptype == PVPType.Def)
			{
				this.DefButtonLabel.color = this.color_open;
				this.DefButton.GetComponent<UISprite>().spriteName = "选中";
				this.AttButtonLabel.color = this.color_close;
				this.AttButton.GetComponent<UISprite>().spriteName = "未选中";
				this.PVPMessageList = this.Def_PVPMessageList;
			}
		}
		else
		{
			this.AttButtonLabel.color = this.color_open;
			this.AttButton.GetComponent<UISprite>().spriteName = "选中";
			this.DefButtonLabel.color = this.color_close;
			this.DefButton.GetComponent<UISprite>().spriteName = "未选中";
			this.PVPMessageList = this.Att_PVPMessageList;
		}
		for (int i = 0; i < this.PVPMessageList.Count; i++)
		{
			PVPMessageItem component = NGUITools.AddChild(this.PVP_Grid.gameObject, this.PVP_Item).GetComponent<PVPMessageItem>();
			DateTime dateTime = TimeTools.ConvertLongDateTime(this.PVPMessageList[i].time);
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			text = string.Empty + dateTime.Year;
			text2 = string.Empty + dateTime.Month;
			if (dateTime.Month < 10)
			{
				text2 = "0" + text2;
			}
			text3 = string.Empty + dateTime.Day;
			if (dateTime.Day < 10)
			{
				text3 = "0" + text3;
			}
			text4 = string.Empty + dateTime.Hour;
			if (dateTime.Hour < 10)
			{
				text4 = "0" + text4;
			}
			text5 = string.Empty + dateTime.Minute;
			if (dateTime.Minute < 10)
			{
				text5 = "0" + text5;
			}
			component.time = string.Concat(new string[]
			{
				string.Empty,
				text,
				"年",
				text2,
				"月",
				text3,
				"日"
			});
			component.fight_time = text4 + ":" + text5;
			component.fighter_left = this.PVPMessageList[i].fighterName;
			component.fighter_right = this.PVPMessageList[i].targetName;
			component.m_id = this.PVPMessageList[i].id;
			component.m_pvptype = pvptype;
			component.cup_Attack = this.PVPMessageList[i].fighterChangedMedal;
			component.cup_Def = this.PVPMessageList[i].targetChangedMedal;
			component.m_result = this.PVPMessageList[i].fighterWin;
			component.m_fightback = this.PVPMessageList[i].canRevenge;
			component.ThisPVPData = this.PVPMessageList[i];
			if (pvptype != PVPType.Att)
			{
				if (pvptype == PVPType.Def)
				{
					component.m_fightback_id = this.PVPMessageList[i].fighterId;
				}
			}
			else
			{
				component.m_fightback_id = 0L;
			}
			component.m_video_id = this.PVPMessageList[i].videoId;
			component.depth = 40;
			component.Init();
		}
		base.StartCoroutine(this.PVP_Grid.RepositionAfterFrame());
	}
}
