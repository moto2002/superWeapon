using System;
using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class ResLabel : IMonoBehaviour
{
	private int tarNum;

	public ResType resType;

	[SerializeField]
	private float times = -2f;

	[SerializeField]
	private bool isEnd = true;

	private Action CallBack;

	public UILabel textLable;

	public UISprite textUISprite;

	public GameObject CoinCollectTX;

	public GameObject OilCollectTX;

	public GameObject SteelCollectTX;

	public GameObject RareEarthCollectTX;

	public GameObject RMBCollectTX;

	public GameObject CoinLostEffect;

	public GameObject OilLostEffect;

	public GameObject SteelLostEffect;

	public GameObject RarearthLostEffect;

	public static Transform CoinTr;

	public static Transform OilTr;

	public static Transform SteelTr;

	public static Transform RareEarthTr;

	public static Transform RMBTr;

	public bool FlyTarget;

	public bool IsEnd
	{
		get
		{
			return this.isEnd;
		}
		set
		{
			this.isEnd = value;
		}
	}

	private new void Awake()
	{
		base.Awake();
		this.textLable = base.GetComponent<UILabel>();
	}

	public void OnBecameVisible()
	{
		this.SetIns();
	}

	public void OnEnable()
	{
		this.SetIns();
	}

	private void SetIns()
	{
		if (this.CoinCollectTX != null)
		{
			this.tarNum = HeroInfo.GetInstance().playerRes.resCoin;
		}
		else if (this.OilCollectTX != null)
		{
			this.tarNum = HeroInfo.GetInstance().playerRes.resOil;
		}
		else if (this.SteelCollectTX != null)
		{
			this.tarNum = HeroInfo.GetInstance().playerRes.resSteel;
		}
		else if (this.RareEarthCollectTX != null)
		{
			this.tarNum = HeroInfo.GetInstance().playerRes.resRareEarth;
		}
		ResType resType = this.resType;
		switch (resType)
		{
		case ResType.金币:
			if (this.FlyTarget)
			{
				ResLabel.CoinTr = this.tr;
			}
			return;
		case ResType.石油:
			if (this.FlyTarget)
			{
				ResLabel.OilTr = this.tr;
			}
			return;
		case ResType.钢铁:
			if (this.FlyTarget)
			{
				ResLabel.SteelTr = this.tr;
			}
			return;
		case ResType.稀矿:
			if (this.FlyTarget)
			{
				ResLabel.RareEarthTr = this.tr;
			}
			return;
		case ResType.技能点:
			return;
		case ResType.天赋点:
			return;
		case ResType.钻石:
			if (this.FlyTarget)
			{
				ResLabel.RMBTr = this.tr;
			}
			return;
		case ResType.兵种:
			return;
		case ResType.经验:
			return;
		case ResType.等级:
			return;
		case ResType.奖牌:
			return;
		case ResType.军令:
			return;
		case ResType.指挥官经验:
			return;
		case ResType.战绩积分:
			return;
		case ResType.探索令:
			return;
		case ResType.技能碎片:
		case ResType.普通技能抽卡:
			IL_FE:
			if (resType != ResType.弹药)
			{
				return;
			}
			return;
		case ResType.电力:
			return;
		}
		goto IL_FE;
	}

	private void Update()
	{
		if (!this.IsEnd && this.times > 0f)
		{
			int num = this.tarNum - int.Parse(this.textLable.text);
			float num2 = (float)num / this.times;
			float num3 = Mathf.Clamp(Time.deltaTime, 0f, 0.1f);
			this.times -= num3;
			float num4 = num2 * num3 + (float)int.Parse(this.textLable.text);
			if (num2 == 0f)
			{
				num4 = (float)this.tarNum;
			}
			else if (num2 > 0f)
			{
				num4 = ((num4 <= (float)this.tarNum) ? num4 : ((float)this.tarNum));
			}
			else
			{
				num4 = ((num4 >= (float)this.tarNum) ? num4 : ((float)this.tarNum));
			}
			switch (this.resType)
			{
			case ResType.金币:
				if (this.CoinCollectTX != null && num2 > 0f && !this.CoinCollectTX.activeSelf)
				{
					this.CoinCollectTX.SetActive(true);
				}
				this.textLable.text = ((int)num4).ToString();
				this.textUISprite.fillAmount = num4 / (float)HeroInfo.GetInstance().playerRes.maxCoin;
				goto IL_2FC;
			case ResType.石油:
				if (this.OilCollectTX != null && num2 > 0f && !this.OilCollectTX.activeSelf)
				{
					this.OilCollectTX.SetActive(true);
				}
				this.textLable.text = ((int)num4).ToString();
				this.textUISprite.fillAmount = num4 / (float)HeroInfo.GetInstance().playerRes.maxOil;
				goto IL_2FC;
			case ResType.钢铁:
				if (this.SteelCollectTX != null && num2 > 0f && !this.SteelCollectTX.activeSelf)
				{
					this.SteelCollectTX.SetActive(true);
				}
				this.textLable.text = ((int)num4).ToString();
				this.textUISprite.fillAmount = num4 / (float)HeroInfo.GetInstance().playerRes.maxSteel;
				goto IL_2FC;
			case ResType.稀矿:
				if (this.RareEarthCollectTX != null && num2 > 0f && !this.RareEarthCollectTX.activeSelf)
				{
					this.RareEarthCollectTX.SetActive(true);
				}
				this.textLable.text = ((int)num4).ToString();
				this.textUISprite.fillAmount = num4 / (float)HeroInfo.GetInstance().playerRes.maxRareEarth;
				goto IL_2FC;
			case ResType.钻石:
				this.textLable.text = ((int)num4).ToString();
				goto IL_2FC;
			}
			this.textLable.text = ((int)num4).ToString();
			IL_2FC:
			if (this.times <= 0f)
			{
				if (this.CallBack != null)
				{
					this.CallBack();
					this.CallBack = null;
				}
				if (!(this.CoinCollectTX != null) || this.CoinCollectTX.activeSelf)
				{
				}
				if (!(this.OilCollectTX != null) || this.OilCollectTX.activeSelf)
				{
				}
				if (!(this.SteelCollectTX != null) || this.SteelCollectTX.activeSelf)
				{
				}
				if (!(this.RareEarthCollectTX != null) || this.RareEarthCollectTX.activeSelf)
				{
				}
				if (this.RMBCollectTX != null && !this.RMBCollectTX.activeSelf)
				{
					this.RMBCollectTX.GetComponent<UISpriteAnimation>().RebuildSpriteList();
					this.RMBCollectTX.SetActive(false);
				}
				this.IsEnd = true;
				switch (this.resType)
				{
				case ResType.金币:
					this.textLable.text = this.tarNum.ToString();
					this.textUISprite.fillAmount = (float)this.tarNum * 1f / (float)HeroInfo.GetInstance().playerRes.maxCoin;
					if (this.tarNum >= HeroInfo.GetInstance().playerRes.maxCoin)
					{
						MainUIPanelManage._instance.resCoinLabel.color = Color.red;
					}
					else
					{
						MainUIPanelManage._instance.resCoinLabel.color = Color.white;
					}
					if ((float)HeroInfo.GetInstance().playerRes.resCoin <= (float)HeroInfo.GetInstance().playerRes.maxCoin * 0.05f)
					{
						if (MainUIPanelManage._instance.EffectResCoinQueShi)
						{
							MainUIPanelManage._instance.EffectResCoinQueShi.SetActive(true);
						}
						else
						{
							MainUIPanelManage._instance.EffectResCoinQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", MainUIPanelManage._instance.resCoinLabel.transform).ga;
						}
						if (this.CoinLostEffect)
						{
							this.CoinLostEffect.SetActive(true);
						}
					}
					else
					{
						if (MainUIPanelManage._instance.EffectResCoinQueShi)
						{
							MainUIPanelManage._instance.EffectResCoinQueShi.SetActive(false);
						}
						if (this.CoinLostEffect)
						{
							this.CoinLostEffect.SetActive(false);
						}
					}
					goto IL_A0D;
				case ResType.石油:
					this.textLable.text = this.tarNum.ToString();
					this.textUISprite.fillAmount = (float)this.tarNum * 1f / (float)HeroInfo.GetInstance().playerRes.maxOil;
					if (this.tarNum >= HeroInfo.GetInstance().playerRes.maxOil)
					{
						MainUIPanelManage._instance.resOilLabel.color = Color.red;
					}
					else
					{
						MainUIPanelManage._instance.resOilLabel.color = Color.white;
					}
					if ((float)HeroInfo.GetInstance().playerRes.resOil <= (float)HeroInfo.GetInstance().playerRes.maxOil * 0.05f)
					{
						if (MainUIPanelManage._instance.EffectResOilQueShi)
						{
							MainUIPanelManage._instance.EffectResOilQueShi.SetActive(true);
						}
						else
						{
							MainUIPanelManage._instance.EffectResOilQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", MainUIPanelManage._instance.resOilLabel.transform).ga;
						}
						if (this.OilLostEffect)
						{
							this.OilLostEffect.SetActive(true);
						}
					}
					else
					{
						if (MainUIPanelManage._instance.EffectResOilQueShi)
						{
							MainUIPanelManage._instance.EffectResOilQueShi.SetActive(false);
						}
						if (this.OilLostEffect)
						{
							this.OilLostEffect.SetActive(false);
						}
					}
					goto IL_A0D;
				case ResType.钢铁:
					this.textLable.text = this.tarNum.ToString();
					this.textUISprite.fillAmount = (float)this.tarNum * 1f / (float)HeroInfo.GetInstance().playerRes.maxSteel;
					if (this.tarNum >= HeroInfo.GetInstance().playerRes.maxSteel)
					{
						MainUIPanelManage._instance.resSteelLabel.color = Color.red;
					}
					else
					{
						MainUIPanelManage._instance.resSteelLabel.color = Color.white;
					}
					if ((float)HeroInfo.GetInstance().playerRes.resSteel <= (float)HeroInfo.GetInstance().playerRes.maxSteel * 0.05f)
					{
						if (MainUIPanelManage._instance.EffectResSteelQueShi)
						{
							MainUIPanelManage._instance.EffectResSteelQueShi.SetActive(true);
						}
						else
						{
							MainUIPanelManage._instance.EffectResSteelQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", MainUIPanelManage._instance.resSteelLabel.transform).ga;
						}
						if (this.SteelLostEffect)
						{
							this.SteelLostEffect.SetActive(true);
						}
					}
					else
					{
						if (MainUIPanelManage._instance.EffectResSteelQueShi)
						{
							MainUIPanelManage._instance.EffectResSteelQueShi.SetActive(false);
						}
						if (this.SteelLostEffect)
						{
							this.SteelLostEffect.SetActive(false);
						}
					}
					goto IL_A0D;
				case ResType.稀矿:
					this.textLable.text = this.tarNum.ToString();
					this.textUISprite.fillAmount = (float)this.tarNum * 1f / (float)HeroInfo.GetInstance().playerRes.maxRareEarth;
					if (this.tarNum >= HeroInfo.GetInstance().playerRes.maxRareEarth)
					{
						MainUIPanelManage._instance.resRareEarthLabel.color = Color.red;
					}
					else
					{
						MainUIPanelManage._instance.resRareEarthLabel.color = Color.white;
					}
					if ((float)HeroInfo.GetInstance().playerRes.resRareEarth <= (float)HeroInfo.GetInstance().playerRes.maxRareEarth * 0.05f)
					{
						if (MainUIPanelManage._instance.EffectResRareEarthQueShi)
						{
							MainUIPanelManage._instance.EffectResRareEarthQueShi.SetActive(true);
						}
						else
						{
							MainUIPanelManage._instance.EffectResRareEarthQueShi = PoolManage.Ins.GetEffectByName("ziyuanqueshi", MainUIPanelManage._instance.resRareEarthLabel.transform).ga;
						}
						if (this.RarearthLostEffect)
						{
							this.RarearthLostEffect.SetActive(true);
						}
					}
					else
					{
						if (MainUIPanelManage._instance.EffectResRareEarthQueShi)
						{
							MainUIPanelManage._instance.EffectResRareEarthQueShi.SetActive(false);
						}
						if (this.RarearthLostEffect)
						{
							this.RarearthLostEffect.SetActive(false);
						}
					}
					goto IL_A0D;
				case ResType.钻石:
					this.textLable.text = this.tarNum.ToString();
					goto IL_A0D;
				}
				this.textLable.text = this.tarNum.ToString();
			}
			IL_A0D:;
		}
		else if (this.times < 0f)
		{
			if (this.CoinCollectTX != null && this.CoinCollectTX.activeSelf)
			{
				this.CoinCollectTX.SetActive(false);
			}
			if (this.OilCollectTX != null && this.OilCollectTX.activeSelf)
			{
				this.OilCollectTX.SetActive(false);
			}
			if (this.SteelCollectTX != null && this.SteelCollectTX.activeSelf)
			{
				this.SteelCollectTX.SetActive(false);
			}
			if (this.RareEarthCollectTX != null && this.RareEarthCollectTX.activeSelf)
			{
				this.RareEarthCollectTX.SetActive(false);
			}
		}
	}

	public void ChangNumText(int _tarNum, float _times, Action _CallBack)
	{
		this.tarNum = _tarNum;
		this.times = _times;
		this.IsEnd = false;
		this.CallBack = _CallBack;
	}

	public void ChangeTarNum(int _tarNum, float _times)
	{
		this.tarNum = _tarNum;
		this.times = _times;
		this.IsEnd = false;
	}

	public void OnDisable()
	{
		if (this.CoinCollectTX != null && this.CoinCollectTX.activeSelf)
		{
			this.CoinCollectTX.SetActive(false);
		}
		if (this.OilCollectTX != null && this.OilCollectTX.activeSelf)
		{
			this.OilCollectTX.SetActive(false);
		}
		if (this.SteelCollectTX != null && this.SteelCollectTX.activeSelf)
		{
			this.SteelCollectTX.SetActive(false);
		}
		if (this.RareEarthCollectTX != null && this.RareEarthCollectTX.activeSelf)
		{
			this.RareEarthCollectTX.SetActive(false);
		}
	}
}
