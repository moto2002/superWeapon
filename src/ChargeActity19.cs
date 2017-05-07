using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeActity19 : ChargeRightPanel
{
	public Transform zhuan;

	public Transform[] prizes;

	public UILabel oneLabel;

	public UILabel tenLabel;

	private ActivityClass curActitvty;

	public GameObject zhuanPanBac;

	public GameObject onePrizeUiSprite;

	private int prizeLength;

	private bool isZhuan;

	private LotteryData lotteryData;

	public void OnDisable()
	{
		this.isZhuan = false;
		this.zhuanPanBac.SetActive(false);
		for (int i = 0; i < this.prizes.Length; i++)
		{
			this.prizes[i].localScale = Vector3.one * 0.799872f;
		}
	}

	public void Awake()
	{
		this.prizeLength = this.prizes.Length;
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get19_One, new EventManager.VoidDelegate(this.Get19_One));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get19_Ten, new EventManager.VoidDelegate(this.Get19_Ten));
		this.curActitvty = ChargeActityPanel.GetRegCharges[19][0];
		this.lotteryData = HeroInfo.GetInstance().LotteryData;
		this.InitPrizes();
	}

	private void Get19_One(GameObject ga)
	{
		if (HeroInfo.GetInstance().LotteryDataFreeTimes <= 0 && this.lotteryData.onePrice > HeroInfo.GetInstance().playerRes.RMBCoin)
		{
			ShopPanelManage.ShowHelp_NoRMB(null, null);
			return;
		}
		ButtonClick component = ga.GetComponent<ButtonClick>();
		component.IsCanDoEvent = false;
		CSTurntableDraw cSTurntableDraw = new CSTurntableDraw();
		cSTurntableDraw.type = 1;
		ClientMgr.GetNet().SendHttp(2216, cSTurntableDraw, new DataHandler.OpcodeHandler(this.ZhuanPaneOneCallBack), null);
	}

	public void Update()
	{
	}

	private void ZhuanPaneOneCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			this.zhuanPanBac.SetActive(true);
			List<SCLotteryPrize> list = opcode.Get<SCLotteryPrize>(10118);
			if (list.Count > 0)
			{
				int num = list[0].prizeList[0];
				float min = ((float)num - 1f) * -360f / (float)this.prizeLength;
				float max = (float)num * -360f / (float)this.prizeLength;
				float z = (float)(UnityEngine.Random.Range(5, 10) * -360) + UnityEngine.Random.Range(min, max);
				this.isZhuan = true;
				this.zhuan.DOLocalRotate(new Vector3(0f, 0f, z), 3.6f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).OnComplete(delegate
				{
					ShowAwardPanelManger.showAwardList();
				});
			}
		}
	}

	private void ZhuanPaneTenCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			this.zhuanPanBac.SetActive(true);
			List<SCLotteryPrize> list = opcode.Get<SCLotteryPrize>(10118);
			if (list.Count > 0)
			{
				int num = list[0].prizeList[0];
				float min = ((float)num - 1f) * -360f / (float)this.prizeLength;
				float max = (float)num * -360f / (float)this.prizeLength;
				float z = (float)(UnityEngine.Random.Range(5, 8) * -360) + UnityEngine.Random.Range(min, max);
				this.isZhuan = true;
				this.zhuan.DOLocalRotate(new Vector3(0f, 0f, z), 1.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(delegate
				{
					ShowAwardPanelManger.showAwardList();
				});
			}
		}
	}

	private void Get19_Ten(GameObject ga)
	{
		if (this.lotteryData.tenPrice > HeroInfo.GetInstance().playerRes.RMBCoin)
		{
			ShopPanelManage.ShowHelp_NoRMB(null, null);
			return;
		}
		ButtonClick component = ga.GetComponent<ButtonClick>();
		component.IsCanDoEvent = false;
		CSTurntableDraw cSTurntableDraw = new CSTurntableDraw();
		cSTurntableDraw.type = 2;
		ClientMgr.GetNet().SendHttp(2216, cSTurntableDraw, new DataHandler.OpcodeHandler(this.ZhuanPaneTenCallBack), null);
	}

	public override void OnEnable()
	{
		if (HeroInfo.GetInstance().LotteryDataFreeTimes > 0)
		{
			this.onePrizeUiSprite.SetActive(false);
			this.oneLabel.text = string.Format("{0} : {1}", LanguageManage.GetTextByKey("剩余次数", "Activities"), HeroInfo.GetInstance().LotteryDataFreeTimes);
		}
		else if (this.lotteryData.onePrice == 0)
		{
			this.onePrizeUiSprite.SetActive(false);
			this.oneLabel.text = LanguageManage.GetTextByKey("免费一次", "Activities");
		}
		else
		{
			this.onePrizeUiSprite.SetActive(true);
			this.oneLabel.text = this.lotteryData.onePrice.ToString();
		}
		if (this.lotteryData.tenPrice == 0)
		{
			this.tenLabel.text = LanguageManage.GetTextByKey("免费十次", "Activities");
		}
		else
		{
			this.tenLabel.text = this.lotteryData.tenPrice.ToString();
		}
	}

	private void InitPrizes()
	{
		for (int i = 0; i < this.lotteryData.option.Count; i++)
		{
			RewardOption rewardOption = this.lotteryData.option[i];
			for (int j = 0; j < rewardOption.resReward.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(ChargeActityPanel.ins.resPrefab) as GameObject;
				if (this.prizes.Length > i)
				{
					gameObject.transform.parent = this.prizes[i];
				}
				else
				{
					gameObject.transform.parent = this.prizes[0];
				}
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				ActivityRes component = gameObject.GetComponent<ActivityRes>();
				AtlasManage.SetResSpriteName(component.icon, (ResType)rewardOption.resReward[j].key);
				component.count.text = rewardOption.resReward[j].value.ToString();
				ItemTipsShow2 component2 = gameObject.GetComponent<ItemTipsShow2>();
				component2.Index = (int)rewardOption.resReward[j].key;
				component2.Type = 2;
			}
			for (int k = 0; k < rewardOption.itemReward.Count; k++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(ChargeActityPanel.ins.itemPrefab) as GameObject;
				if (this.prizes.Length > i)
				{
					gameObject2.transform.parent = this.prizes[i];
				}
				else
				{
					gameObject2.transform.parent = this.prizes[0];
				}
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localScale = Vector3.one;
				ActivityItemPre component3 = gameObject2.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component3.icon, UnitConst.GetInstance().ItemConst[(int)rewardOption.itemReward[k].key].IconId);
				AtlasManage.SetQuilitySpriteName(component3.quality, UnitConst.GetInstance().ItemConst[(int)rewardOption.itemReward[k].key].Quality);
				component3.count.text = string.Format("X{0}", rewardOption.itemReward[k].value);
				ItemTipsShow2 component4 = component3.GetComponent<ItemTipsShow2>();
				component4.Index = (int)rewardOption.itemReward[k].key;
				component4.Type = 1;
			}
			for (int l = 0; l < rewardOption.moneyReward.Count; l++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(ChargeActityPanel.ins.resPrefab) as GameObject;
				if (this.prizes.Length > i)
				{
					gameObject3.transform.parent = this.prizes[i];
				}
				else
				{
					gameObject3.transform.parent = this.prizes[0];
				}
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localScale = Vector3.one;
				ActivityRes component5 = gameObject3.GetComponent<ActivityRes>();
				AtlasManage.SetResSpriteName(component5.icon, (ResType)rewardOption.moneyReward[l].key);
				component5.count.text = rewardOption.moneyReward[l].value.ToString();
				ItemTipsShow2 component6 = gameObject3.GetComponent<ItemTipsShow2>();
				component6.Index = (int)rewardOption.moneyReward[l].key;
				component6.Type = 2;
			}
			for (int m = 0; m < rewardOption.skillReward.Count; m++)
			{
				GameObject gameObject4 = UnityEngine.Object.Instantiate(ChargeActityPanel.ins.skillPrefab) as GameObject;
				if (this.prizes.Length > i)
				{
					gameObject4.transform.parent = this.prizes[i];
				}
				else
				{
					gameObject4.transform.parent = this.prizes[0];
				}
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.transform.localScale = Vector3.one;
				ActivitySkillPrefab component7 = gameObject4.GetComponent<ActivitySkillPrefab>();
				AtlasManage.SetSkillSpritName(component7.icon, UnitConst.GetInstance().skillList[(int)rewardOption.skillReward[m].key].icon);
				AtlasManage.SetQuilitySpriteName(component7.bg, UnitConst.GetInstance().skillList[(int)rewardOption.skillReward[m].key].skillQuality);
				component7.name.text = UnitConst.GetInstance().skillList[(int)rewardOption.skillReward[m].key].name;
				switch (UnitConst.GetInstance().skillList[(int)rewardOption.skillReward[m].key].skillQuality)
				{
				case Quility_ResAndItemAndSkill.白:
					component7.name.color = Color.white;
					break;
				case Quility_ResAndItemAndSkill.绿:
					component7.name.color = new Color(0.243137255f, 0.8862745f, 0.117647059f);
					break;
				case Quility_ResAndItemAndSkill.蓝:
					component7.name.color = new Color(0.007843138f, 0.8039216f, 1f);
					break;
				case Quility_ResAndItemAndSkill.紫:
					component7.name.color = new Color(0.7372549f, 0.007843138f, 0.870588243f);
					break;
				case Quility_ResAndItemAndSkill.红:
					component7.name.color = new Color(1f, 0.007843138f, 0.09019608f);
					break;
				}
				ItemTipsShow2 component8 = component7.GetComponent<ItemTipsShow2>();
				component8.Index = (int)rewardOption.skillReward[m].key;
				component8.Type = 3;
			}
		}
	}
}
