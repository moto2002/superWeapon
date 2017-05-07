using DG.Tweening;
using System;
using UnityEngine;

public class VIPPanel : FuncUIPanel
{
	public UILabel label_CurVipLv;

	public UILabel label_NextVipLv;

	public UILabel label_NextLvNeedCoin;

	public UISprite yellowSprite;

	public UISprite bacSprite;

	public GameObject leftGa;

	public GameObject rightGa;

	public GameObject label_help;

	public UIGrid vipContentGrid;

	public GameObject vipContentPrefab;

	public GameObject vip0Prefab;

	public GameObject lingquBtn;

	public GameObject lingqued;

	private Tweener tween;

	public int curPages;

	public int maxPages;

	public override void Awake()
	{
		this.InitEvent();
		this.vipContentGrid.isRespositonOnStart = false;
	}

	public override void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		base.OnEnable();
		this.Initinfo();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10054 && this.curPages != HeroInfo.GetInstance().vipData.VipLevel)
		{
			this.tween = this.vipContentGrid.transform.DOLocalMoveX(this.vipContentGrid.transform.localPosition.x + this.vipContentGrid.cellWidth * (float)(this.curPages - HeroInfo.GetInstance().vipData.VipLevel), 0.6f, false);
			this.tween.SetEase(Ease.OutQuad);
			this.tween.OnComplete(delegate
			{
				this.CenterOnBack(HeroInfo.GetInstance().vipData.VipLevel);
				this.tween = null;
			});
		}
	}

	public override void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		base.OnDisable();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_ChongZhiTiaoZhuan, new EventManager.VoidDelegate(this.ChongZhiTiaoZhuan));
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_LingQuJiangLi, new EventManager.VoidDelegate(this.LingQuJiangLi));
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_Next, new EventManager.VoidDelegate(this.VIPPanel_Next));
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_Pre, new EventManager.VoidDelegate(this.VIPPanel_Pre));
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_ClosePanel, new EventManager.VoidDelegate(this.VIPPanel_ClosePanel));
	}

	private void ChongZhiTiaoZhuan(GameObject ga)
	{
		FuncUIManager.inst.OpenFuncUI("ShopPanel", SenceType.Island);
	}

	private void VIPPanel_ClosePanel(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("VIPPanel");
	}

	private void LingQuJiangLi(GameObject ga)
	{
		VipHandler.ReceieveVIP();
		this.lingquBtn.SetActive(false);
		this.lingqued.SetActive(false);
	}

	private void VIPPanel_Next(GameObject ga)
	{
		if (this.tween != null)
		{
			return;
		}
		if (this.curPages == this.maxPages)
		{
			return;
		}
		this.lingquBtn.SetActive(false);
		this.lingqued.SetActive(false);
		this.tween = this.vipContentGrid.transform.DOLocalMoveX(this.vipContentGrid.transform.localPosition.x - this.vipContentGrid.cellWidth, 0.6f, false);
		this.tween.SetEase(Ease.OutQuad);
		this.tween.OnComplete(delegate
		{
			this.CenterOnBack(this.curPages + 1);
			this.tween = null;
		});
	}

	private void VIPPanel_Pre(GameObject ga)
	{
		if (this.tween != null)
		{
			return;
		}
		if (HeroInfo.GetInstance().vipData.VipLevel > 0 && this.curPages == 1)
		{
			return;
		}
		if (HeroInfo.GetInstance().vipData.VipLevel == 0 && this.curPages == 0)
		{
			return;
		}
		this.lingquBtn.SetActive(false);
		this.lingqued.SetActive(false);
		this.tween = this.vipContentGrid.transform.DOLocalMoveX(this.vipContentGrid.transform.localPosition.x + this.vipContentGrid.cellWidth, 0.6f, false);
		this.tween.SetEase(Ease.OutQuad);
		this.tween.OnComplete(delegate
		{
			this.CenterOnBack(this.curPages - 1);
			this.tween = null;
		});
	}

	private void CenterOnBack(int level)
	{
		this.curPages = level;
		if (this.curPages < this.maxPages)
		{
			this.rightGa.SetActive(true);
		}
		else
		{
			this.rightGa.SetActive(false);
		}
		if (level == 1 && HeroInfo.GetInstance().vipData.VipLevel > 0)
		{
			this.leftGa.SetActive(false);
		}
		else if (level == 0 && HeroInfo.GetInstance().vipData.VipLevel == 0)
		{
			this.leftGa.SetActive(false);
		}
		else
		{
			this.leftGa.SetActive(true);
		}
		if (level == HeroInfo.GetInstance().vipData.VipLevel && HeroInfo.GetInstance().vipData.VipLevel > 0)
		{
			if (TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.vipPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
			{
				this.lingquBtn.SetActive(true);
			}
			else
			{
				this.lingqued.SetActive(true);
			}
		}
	}

	private void Start()
	{
	}

	private void Initinfo()
	{
		this.label_CurVipLv.text = HeroInfo.GetInstance().vipData.VipLevel.ToString();
		this.vipContentGrid.ClearChild();
		this.vipContentGrid.transform.localPosition = new Vector3(16f, -90f, 0f);
		this.vipContentGrid.GetComponentInParent<UIScrollView>().ResetPosition();
		if (HeroInfo.GetInstance().vipData.NextVipNeedMoney > 0)
		{
			this.label_NextLvNeedCoin.text = string.Format("{0}{1}", HeroInfo.GetInstance().vipData.NextVipNeedMoney, LanguageManage.GetTextByKey("钻石", "ResIsland"));
			this.label_NextVipLv.text = string.Format("VIP{0}", HeroInfo.GetInstance().vipData.VipLevel + 1);
			this.bacSprite.width = (int)(420f * ((float)HeroInfo.GetInstance().vipData.money_Buyed * 1f / (float)UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel + 1].money_buyed));
			this.yellowSprite.width = (int)(420f * ((float)HeroInfo.GetInstance().vipData.money_Buyed * 1f / (float)UnitConst.GetInstance().VipConstData[HeroInfo.GetInstance().vipData.VipLevel + 1].money_buyed));
			if (HeroInfo.GetInstance().vipData.money_Buyed == 0)
			{
				this.yellowSprite.enabled = false;
				this.bacSprite.enabled = false;
			}
			else
			{
				this.yellowSprite.enabled = true;
				this.bacSprite.enabled = true;
			}
		}
		else
		{
			this.label_help.SetActive(false);
			this.yellowSprite.enabled = false;
			this.bacSprite.enabled = false;
		}
		this.leftGa.SetActive(false);
		if (HeroInfo.GetInstance().vipData.VipLevel > 0)
		{
			this.curPages = HeroInfo.GetInstance().vipData.VipLevel;
			this.InitVIP();
			if (this.curPages > 1)
			{
				this.leftGa.SetActive(true);
			}
			if (TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.vipPrizeTime), TimeTools.GetNowTimeSyncServerToDateTime()))
			{
				this.lingquBtn.SetActive(true);
			}
			else
			{
				this.lingqued.SetActive(true);
			}
		}
		else
		{
			this.curPages = 0;
			this.InitVip0();
			this.InitVIP();
		}
	}

	private void InitVip0()
	{
		GameObject gameObject = NGUITools.AddChild(this.vipContentGrid.gameObject, this.vip0Prefab);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.GetComponent<VipContent_0>().InitInfo();
	}

	private void InitVIP()
	{
		for (int i = 1; i < 100; i++)
		{
			if (!UnitConst.GetInstance().VipConstData.ContainsKey(i))
			{
				break;
			}
			GameObject gameObject = NGUITools.AddChild(this.vipContentGrid.gameObject, this.vipContentPrefab);
			gameObject.transform.localPosition = new Vector3(this.vipContentGrid.cellWidth * (float)(i - this.curPages), 0f, 0f);
			VipContent component = gameObject.GetComponent<VipContent>();
			component.InitInfo(i);
			this.maxPages = i;
		}
	}
}
