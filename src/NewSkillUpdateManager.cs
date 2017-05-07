using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class NewSkillUpdateManager : FuncUIPanel
{
	public static NewSkillUpdateManager ins;

	public UIScrollView scroll;

	public UITable skillTabel;

	public GameObject skillItem;

	public UIGrid showGrid;

	public Transform btnUpdate;

	public ShowSkillInfo skillInfos;

	public UILabel btnLabel;

	public GameObject buildingLv;

	public GameObject needSkill;

	public Transform kuang;

	public GameObject btn1;

	public GameObject btn2;

	public GameObject btn3;

	public GameObject bestLevel;

	private Dictionary<int, SkillCreateInfo> allSkillUpsetUI = new Dictionary<int, SkillCreateInfo>();

	public List<long> skillID = new List<long>();

	public int itemID;

	public void OnDestroy()
	{
		NewSkillUpdateManager.ins = null;
	}

	private void Start()
	{
	}

	public override void Awake()
	{
		NewSkillUpdateManager.ins = this;
		this.Init();
	}

	private void HideBtns()
	{
		this.btn1.SetActive(false);
		this.btn2.SetActive(false);
		this.btn3.SetActive(false);
	}

	public override void OnEnable()
	{
		this.RefershSkill();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10084)
		{
			this.RefreshSkillInfo();
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.NewSkillUpdatePanel_Close, new EventManager.VoidDelegate(this.CloseThis));
		EventManager.Instance.AddEvent(EventManager.EventType.NewSkillUpdatePanel_SkillClick, new EventManager.VoidDelegate(this.SkillItemClick));
		EventManager.Instance.AddEvent(EventManager.EventType.NewSkillUpdatePanel_Update, new EventManager.VoidDelegate(this.BtnUpdate));
	}

	private void CloseThis(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("NewSkillUpdatePanel");
	}

	private void RefershSkill()
	{
		int num = 0;
		foreach (SkillUpdate item in from a in UnitConst.GetInstance().skillUpdateConst.Values
		where a.skillVoloum != 0
		select a)
		{
			if (this.allSkillUpsetUI.ContainsKey(item.itemId))
			{
				if (num == 0)
				{
					this.skillInfos.ShowMain(ShowSkillInfo.ins.curSelSKillInfo);
				}
			}
			else
			{
				GameObject gameObject = NGUITools.AddChild(this.skillTabel.gameObject, this.skillItem);
				SkillCreateInfo component = gameObject.GetComponent<SkillCreateInfo>();
				component.name_Client.text = LanguageManage.GetTextByKey(item.name, "skill") + "lv." + HeroInfo.GetInstance().SkillInfo[item.itemId];
				AtlasManage.SetBigSkillAtlas(component.icon, UnitConst.GetInstance().skillList.Values.First((Skill b) => b.skillType == item.itemId).Ficon);
				component.id_Server = HeroInfo.GetInstance().SkillInfo[item.itemId];
				component.itemId = item.itemId;
				component.id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == item.itemId && a.level == HeroInfo.GetInstance().SkillInfo[item.itemId]).id;
				component.des.text = UnitConst.GetInstance().skillList.Values.First((Skill a) => a.skillType == item.itemId).Description;
				component.DesTip.text = LanguageManage.GetTextByKey("占用卡槽", "skill") + ":" + UnitConst.GetInstance().skillList.Values.First((Skill a) => a.skillType == item.itemId).skillVolume;
				if (!component.tip.GetComponent<SkillTipshow>())
				{
					component.tip.AddComponent<SkillTipshow>();
				}
				SkillTipshow component2 = component.tip.GetComponent<SkillTipshow>();
				component2.JianTouPostion = 2;
				component2.Index = component.id;
				component2.type = 2;
				this.allSkillUpsetUI.Add(item.itemId, component);
				if (num == 0)
				{
					this.skillInfos.ShowMain(component);
					this.btnUpdate.name = component.itemId.ToString();
				}
				num++;
			}
		}
		this.showGrid.Reposition();
	}

	private void SkillItemClick(GameObject ga)
	{
		this.HideBtns();
		SkillCreateInfo component = ga.GetComponent<SkillCreateInfo>();
		this.btnUpdate.name = component.itemId.ToString();
		this.skillInfos.ShowMain(component);
		this.showGrid.Reposition();
	}

	private void RefreshSkillInfo()
	{
		ShowSkillInfo.ins.ShowMain(ShowSkillInfo.ins.curSelSKillInfo);
	}

	private void BtnUpdate(GameObject ga)
	{
		this.itemID = int.Parse(ga.name);
		int id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == int.Parse(ga.name) && a.level == HeroInfo.GetInstance().SkillInfo[int.Parse(ga.name)]).id;
		CSSkillUp cSSkillUp = new CSSkillUp();
		LogManage.LogError(UnitConst.GetInstance().skillUpdateConst[int.Parse(ga.name)].itemId);
		cSSkillUp.skillType = UnitConst.GetInstance().skillUpdateConst[id].itemId;
		foreach (KeyValuePair<int, int> item in UnitConst.GetInstance().skillUpdateConst[id].coistItems)
		{
			int num = 0;
			foreach (SkillCarteData current in from a in HeroInfo.GetInstance().skillCarteList
			where a.itemID == item.Key
			select a)
			{
				cSSkillUp.skills.Add(current.id);
				num++;
				if (num == item.Value)
				{
					break;
				}
			}
		}
		ClientMgr.GetNet().SendHttp(2308, cSSkillUp, new DataHandler.OpcodeHandler(this.GC_SkillUpdate), null);
		this.RefreshSkillInfo();
	}

	public void ShowTipId(int ids)
	{
		foreach (KeyValuePair<int, SkillCreateInfo> current in this.allSkillUpsetUI)
		{
			if (current.Value.itemId == ids)
			{
				current.Value.id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == ids && a.level == HeroInfo.GetInstance().SkillInfo[ids]).id;
				if (!current.Value.tip.GetComponent<SkillTipshow>())
				{
					current.Value.tip.AddComponent<SkillTipshow>();
				}
				SkillTipshow component = current.Value.tip.GetComponent<SkillTipshow>();
				component.JianTouPostion = 2;
				component.Index = current.Value.id;
				component.type = 2;
			}
		}
	}

	public void GC_SkillUpdate(bool isError, Opcode opcode)
	{
		this.kuang.gameObject.SetActive(true);
		TweenAlpha.Begin(this.kuang.gameObject, 0.2f, 1f);
		base.StartCoroutine(this.ShowKuang(0.2f));
		this.ShowTipId(this.itemID);
	}

	[DebuggerHidden]
	public IEnumerator ShowKuang(float seconds)
	{
		NewSkillUpdateManager.<ShowKuang>c__Iterator74 <ShowKuang>c__Iterator = new NewSkillUpdateManager.<ShowKuang>c__Iterator74();
		<ShowKuang>c__Iterator.seconds = seconds;
		<ShowKuang>c__Iterator.<$>seconds = seconds;
		<ShowKuang>c__Iterator.<>f__this = this;
		return <ShowKuang>c__Iterator;
	}
}
