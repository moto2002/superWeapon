using DG.Tweening;
using System;
using UnityEngine;

public class PlayerRole : MonoBehaviour
{
	public static PlayerRole _Inst;

	public UILabel playerName;

	public UILabel playerLV;

	public UILabel playerMedal;

	public GameObject PlayermedalTips;

	public UISpriteAnimation playerLevelUp;

	public DieBall levelUPEffect;

	public UISprite playerExp;

	public Transform electricityPow;

	public UILabel VIPLevel_Label;

	public GameObject vip_Putong;

	public GameObject vip_ZhiZun;

	private float expMubiao;

	private Sequence vipPutpng;

	private Sequence vipZhiZun;

	private void Awake()
	{
		PlayerRole._Inst = this;
	}

	public void SetPlayerInfo(bool isLevelUP = false)
	{
		this.playerName.text = HeroInfo.GetInstance().userName;
		if (HeroInfo.GetInstance().playerlevel == 0)
		{
			HeroInfo.GetInstance().playerlevel = 1;
		}
		this.playerLV.text = string.Format("LV.{0}", HeroInfo.GetInstance().playerlevel.ToString());
		this.playerMedal.text = HeroInfo.GetInstance().playerRes.playermedal.ToString();
		this.expMubiao = (float)HeroInfo.GetInstance().playerRes.playerExp / (float)UnitConst.GetInstance().PlayerExpConst[HeroInfo.GetInstance().playerlevel];
		ResourcePanelManage.GetRMBTran = MainUIPanelManage._instance.diamondTransform;
		if (isLevelUP)
		{
			TweenWidth.Begin(this.playerExp, 0.3f, (int)(this.expMubiao * 170f)).delay = 0.2f;
		}
		else
		{
			this.playerExp.width = (int)(this.expMubiao * 170f);
		}
		if (HeroInfo.GetInstance().playerlevel >= int.Parse(UnitConst.GetInstance().DesighConfigDic[71].value))
		{
			this.playerExp.gameObject.SetActive(false);
		}
		if (this.VIPLevel_Label)
		{
			this.VIPLevel_Label.text = string.Format("VIP{0}", HeroInfo.GetInstance().vipData.VipLevel);
		}
	}

	private void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_JiangPaiTips, new EventManager.VoidDelegate(this.JiangPaiTips));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_ElectricityInfo, new EventManager.VoidDelegate(this.OnElectricityInfo));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenVip, new EventManager.VoidDelegate(this.OpenVip));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenVip_PuTong, new EventManager.VoidDelegate(this.OpenVip_PuTong));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenVip_ZhiZun, new EventManager.VoidDelegate(this.OpenVip_ZhiZun));
		this.SetPlayerInfo(true);
		this.DisplayVIPData();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void OpenVip_PuTong(GameObject ga)
	{
		if (HeroInfo.GetInstance().vipData.cardEndTime > 0L && TimeTools.IsSmallOrEquByDay(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardEndTime)) && TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
		{
			VipHandler.ReceieveMonthlyCard(SenceManager.inst.MainBuilding.id);
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("MonthlyCardPanel", SenceType.Island);
		}
	}

	private void OpenVip_ZhiZun(GameObject ga)
	{
		if (HeroInfo.GetInstance().vipData.superCard == 1 && TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.scardPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
		{
			VipHandler.ReceieveMonthlyCard(SenceManager.inst.MainBuilding.id);
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("MonthlyCardPanel", SenceType.Island);
		}
	}

	private void OpenVip(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("VIPPanel", SenceType.Island);
	}

	private void OnElectricityInfo(GameObject go)
	{
		if (MainUIPanelManage._instance.ElectricityDes != string.Empty)
		{
			HUDTextTool.inst.restip.PlayTextTip(this.electricityPow, MainUIPanelManage._instance.ElectricityDes, -0.15f);
		}
	}

	private void JiangPaiTips(GameObject o)
	{
		HUDTextTool.inst.restip.PlayTextTip(this.PlayermedalTips.transform, string.Concat(new string[]
		{
			LanguageManage.GetTextByKey("勋章会影响排名，勋章数", "others"),
			"\n",
			LanguageManage.GetTextByKey("量越多排行越高。作战图", "others"),
			"\n",
			LanguageManage.GetTextByKey("每胜利一场会获得1至2枚", "others"),
			"\n",
			LanguageManage.GetTextByKey("勋章，如果基地被敌人摧", "others"),
			"\n",
			LanguageManage.GetTextByKey("毁，勋章会丢失。", "others")
		}));
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
		if (this.levelUPEffect)
		{
			this.levelUPEffect.DesInPool();
		}
		if (this.vipPutpng != null)
		{
			this.vipPutpng.Kill(false);
		}
		this.vip_Putong.transform.localScale = Vector3.one;
		if (this.vipZhiZun != null)
		{
			this.vipZhiZun.Kill(false);
		}
		this.vip_ZhiZun.transform.localScale = Vector3.one;
	}

	private void DisplayVIPData()
	{
		if (HeroInfo.GetInstance().vipData.IsVIP)
		{
			this.vip_Putong.GetComponent<UISprite>().ShaderToNormal();
		}
		else
		{
			this.vip_Putong.GetComponent<UISprite>().ShaderToGray();
		}
		this.vip_ZhiZun.SetActive(HeroInfo.GetInstance().vipData.superCard == 1);
		if (HeroInfo.GetInstance().vipData.cardEndTime > 0L && TimeTools.IsSmallOrEquByDay(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardEndTime)) && TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
		{
			this.vipPutpng = DOTween.Sequence();
			this.vipPutpng.Append(this.vip_Putong.transform.DOScale(Vector3.one * 1.2f, 0.2f));
			this.vipPutpng.Append(this.vip_Putong.transform.DOScale(Vector3.one, 0.2f));
			this.vipPutpng.PrependInterval(1f);
			this.vipPutpng.SetLoops(-1);
		}
		else
		{
			if (this.vipPutpng != null)
			{
				this.vipPutpng.Kill(false);
			}
			this.vip_Putong.transform.localScale = Vector3.one;
		}
		if (HeroInfo.GetInstance().vipData.superCard == 1 && TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.scardPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
		{
			this.vipZhiZun = DOTween.Sequence();
			this.vipZhiZun.Append(this.vip_ZhiZun.transform.DOScale(Vector3.one * 1.2f, 0.2f));
			this.vipZhiZun.Append(this.vip_ZhiZun.transform.DOScale(Vector3.one, 0.2f));
			this.vipZhiZun.PrependInterval(1f);
			this.vipZhiZun.SetLoops(-1);
		}
		else
		{
			if (this.vipZhiZun != null)
			{
				this.vipZhiZun.Kill(false);
			}
			this.vip_ZhiZun.transform.localScale = Vector3.one;
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10002)
		{
			this.SetPlayerInfo(true);
			return;
		}
		if (opcodeCMD == 10054)
		{
			this.DisplayVIPData();
			return;
		}
		if (opcodeCMD == 10003)
		{
			this.SetPlayerInfo(true);
			return;
		}
	}
}
