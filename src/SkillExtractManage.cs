using DG.Tweening;
using msg;
using System;
using System.Linq;
using UnityEngine;

public class SkillExtractManage : FuncUIPanel
{
	public static SkillExtractManage inst;

	public GameObject zhanshi;

	public GameObject zhaobu;

	public GameObject PrimaryGa;

	public GameObject IntermediateGa;

	public GameObject SeniorGa;

	public GameObject PrimaryCDGa;

	public GameObject IntermediateCDGa;

	public GameObject SeniorCDGa;

	public GameObject PrimaryBtn;

	public GameObject IntermediateBtn;

	public GameObject SeniorBtn;

	public UILabel PrimaryItem;

	public UILabel PrimaryCoin;

	public UILabel PrimaryCD;

	public UILabel IntermediateItem;

	public UILabel IntermediateCoin;

	public UILabel IntermediateCD;

	public UILabel SeniorItem;

	public UILabel SeniorCoin;

	public UILabel SeniorCD;

	public UISprite PrimarySprite;

	public UISprite IntermediateSprite;

	public UISprite SeniorSprite;

	private int level;

	private int item1;

	private int num1;

	private int item2;

	private int num2;

	private int item3;

	private int num3;

	private ButtonClick BuySkillCard;

	public void FixedUpdate()
	{
	}

	public override void Awake()
	{
		SkillExtractManage.inst = this;
		this.Init();
		this.InitData();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_Close, new EventManager.VoidDelegate(this.CloseThisPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_Primary, new EventManager.VoidDelegate(this.SkillExtractPanel_Primary));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_PrimaryEndCDTime, new EventManager.VoidDelegate(this.SkillExtractPanel_PrimaryEndCDTime));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_Intermediate, new EventManager.VoidDelegate(this.SkillExtractPanel_Intermediate));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_IntermediateEndCDTime, new EventManager.VoidDelegate(this.SkillExtractPanel_IntermediateEndCDTime));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_Senior, new EventManager.VoidDelegate(this.SkillExtractPanel_Senior));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_SeniorEndCDTime, new EventManager.VoidDelegate(this.SkillExtractPanel_SeniorEndCDTime));
	}

	private void InitData()
	{
		this.level = HeroInfo.GetInstance().PlayerTechBuildingLv;
		this.item1 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Keys.First<int>();
		this.num1 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Values.First<int>();
		this.item2 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Keys.First<int>();
		this.num2 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Values.First<int>();
		this.item3 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Keys.First<int>();
		this.num3 = UnitConst.GetInstance().SkillMixConstData[this.level].costItem.Values.First<int>();
		this.PrimaryItem.text = string.Format("{0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[this.item1].Name, "item"), this.num1);
		this.IntermediateItem.text = string.Format("{0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[this.item2].Name, "item"), this.num2);
		this.SeniorItem.text = string.Format("{0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[this.item3].Name, "item"), this.num3);
		this.PrimaryCoin.text = UnitConst.GetInstance().SkillMixConstData[this.level].costGold.ToString();
		this.IntermediateCoin.text = UnitConst.GetInstance().SkillMixConstData[this.level].costGold.ToString();
		this.SeniorCoin.text = UnitConst.GetInstance().SkillMixConstData[this.level].costGold.ToString();
	}

	private void DisplayData()
	{
		this.RreshData();
		this.PrimaryGa.transform.localPosition = new Vector3(this.PrimaryGa.transform.localPosition.x, (float)Screen.height, 0f);
		this.IntermediateGa.transform.localPosition = new Vector3(this.IntermediateGa.transform.localPosition.x, (float)Screen.height, 0f);
		this.SeniorGa.transform.localPosition = new Vector3(this.SeniorGa.transform.localPosition.x, (float)Screen.height, 0f);
		this.PrimaryGa.transform.DOLocalMoveY(-100f, 0.4f, false).OnComplete(delegate
		{
			this.PrimaryGa.transform.DOLocalMoveY(0f, 0.1f, false);
		});
		this.IntermediateGa.transform.DOLocalMoveY(-100f, 0.4f, false).SetDelay(0.16f).OnComplete(delegate
		{
			this.IntermediateGa.transform.DOLocalMoveY(0f, 0.1f, false);
		});
		this.SeniorGa.transform.DOLocalMoveY(-100f, 0.4f, false).SetDelay(0.32f).OnComplete(delegate
		{
			this.SeniorGa.transform.DOLocalMoveY(0f, 0.1f, false);
		});
	}

	private void RreshData()
	{
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item1) && HeroInfo.GetInstance().PlayerItemInfo[this.item1] >= this.num1)
		{
			this.PrimaryItem.color = Color.white;
		}
		else
		{
			this.PrimaryItem.color = Color.red;
		}
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item2) && HeroInfo.GetInstance().PlayerItemInfo[this.item2] >= this.num2)
		{
			this.IntermediateItem.color = Color.white;
		}
		else
		{
			this.IntermediateItem.color = Color.red;
		}
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item3) && HeroInfo.GetInstance().PlayerItemInfo[this.item3] >= this.num3)
		{
			this.SeniorItem.color = Color.white;
		}
		else
		{
			this.SeniorItem.color = Color.red;
		}
		this.PrimaryCoin.color = ((UnitConst.GetInstance().SkillMixConstData[this.level].costGold <= HeroInfo.GetInstance().playerRes.resCoin) ? Color.white : Color.red);
		this.IntermediateCoin.color = ((UnitConst.GetInstance().SkillMixConstData[this.level].costGold <= HeroInfo.GetInstance().playerRes.resCoin) ? Color.white : Color.red);
		this.SeniorCoin.color = ((UnitConst.GetInstance().SkillMixConstData[this.level].costGold <= HeroInfo.GetInstance().playerRes.resCoin) ? Color.white : Color.red);
	}

	private void SkillExtractPanel_Primary(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMix(1);
	}

	private void CSSkillMix(int type)
	{
		switch (type)
		{
		case 1:
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item1) || HeroInfo.GetInstance().PlayerItemInfo[this.item1] < this.num1)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "item"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		case 2:
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item2) || HeroInfo.GetInstance().PlayerItemInfo[this.item2] < this.num2)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "item"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		case 3:
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.item3) || HeroInfo.GetInstance().PlayerItemInfo[this.item3] < this.num3)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "item"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		}
		CalcMoneyHandler.CSCalcMoney(13, 0, 0, 0L, type, 0, delegate(bool isBuy, int Money)
		{
			if (isBuy)
			{
				if (HeroInfo.GetInstance().playerRes.RMBCoin < Money)
				{
					ShopPanelManage.ShowHelp_NoRMB(null, null);
					return;
				}
				CSSkillMix cSSkillMix = new CSSkillMix();
				cSSkillMix.type = type;
				ClientMgr.GetNet().SendHttp(2310, cSSkillMix, new DataHandler.OpcodeHandler(SkillExtractManage.CSSkillMixCallBack), null);
			}
		});
	}

	public static void CSSkillMixCallBack(bool Error, Opcode func)
	{
		if (!Error)
		{
			HUDTextTool.inst.NextLuaCall("制卡回调· ·", new object[]
			{
				true
			});
		}
	}

	private void CSSkillMixEndCD(int type)
	{
		CalcMoneyHandler.CSCalcMoney(1, 10, 0, 0L, type, 0, delegate(bool isBuy, int Money)
		{
			if (isBuy)
			{
				if (HeroInfo.GetInstance().playerRes.RMBCoin < Money)
				{
					ShopPanelManage.ShowHelp_NoRMB(null, null);
					return;
				}
				CSSkillMixEnd cSSkillMixEnd = new CSSkillMixEnd();
				cSSkillMixEnd.skillId = type;
				ClientMgr.GetNet().SendHttp(2312, cSSkillMixEnd, new DataHandler.OpcodeHandler(SkillExtractManage.CSSkillMixEndCallBack), null);
			}
		});
	}

	public static void CSSkillMixEndCallBack(bool Error, Opcode func)
	{
		if (!Error)
		{
			HUDTextTool.inst.NextLuaCall("制卡CD结束回调· ·", new object[]
			{
				true
			});
		}
	}

	private void SkillExtractPanel_PrimaryEndCDTime(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMixEndCD(1);
	}

	private void SkillExtractPanel_Intermediate(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMix(2);
	}

	private void SkillExtractPanel_IntermediateEndCDTime(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMixEndCD(2);
	}

	private void SkillExtractPanel_Senior(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMix(3);
	}

	private void SkillExtractPanel_SeniorEndCDTime(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.CSSkillMixEndCD(3);
	}

	public override void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
		this.DisplayData();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10075)
		{
			if (HeroInfo.GetInstance().addSkill.Count > 0)
			{
				this.ShowSkill();
			}
			this.RreshData();
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void CloseThisPanel(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("SkillExtractPanel");
	}

	public void ShowSkill()
	{
		this.zhanshi.SetActive(true);
		this.zhanshi.GetComponent<SkillShowManage>().ShowSkill();
		this.zhaobu.SetActive(false);
		if (this.BuySkillCard)
		{
			this.BuySkillCard.IsCanDoEvent = true;
		}
	}
}
