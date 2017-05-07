using DG.Tweening;
using System;
using UnityEngine;

public class ResUIMonoBehaviour : MonoBehaviour
{
	public GameObject Coin;

	public GameObject Oil;

	public GameObject Steel;

	public GameObject RareEarth;

	public GameObject RMB;

	public ResLabel CoinResLabel;

	public ResLabel OilResLabel;

	public ResLabel SteelResLabel;

	public ResLabel RareEarthResLabel;

	public ResLabel RMBResLabel;

	public GameObject CoinCollect;

	public GameObject OilCollect;

	public GameObject SteelCollect;

	public GameObject RareEarthCollect;

	public GameObject RMBCollect;

	public UIGrid ResGrid;

	public float height;

	[HideInInspector]
	public GameObject EffectResCoinQueShi;

	[HideInInspector]
	public GameObject EffectResOilQueShi;

	[HideInInspector]
	public GameObject EffectResSteelQueShi;

	[HideInInspector]
	public GameObject EffectResRareEarthQueShi;

	private void Start()
	{
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		ClientMgr.GetNet().NetDataHandler.CoinChange += new DataHandler.Data_Change(this.DataHandler_CoinChange);
		ClientMgr.GetNet().NetDataHandler.OilChange += new DataHandler.Data_Change(this.DataHandler_OilChange);
		ClientMgr.GetNet().NetDataHandler.SteelChange += new DataHandler.Data_Change(this.DataHandler_SteelChange);
		ClientMgr.GetNet().NetDataHandler.RareEarthChange += new DataHandler.Data_Change(this.DataHandler_RareEarthChange);
		this.DisplayRes();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10039)
		{
			this.DisplayRes();
			return;
		}
		if (opcodeCMD == 10003)
		{
			this.DisplayRes();
			return;
		}
	}

	private void DataHandler_RareEarthChange(int opcodeCMD)
	{
		this.RareEarthResLabel.ChangNumText(HeroInfo.GetInstance().playerRes.resRareEarth, 2f, null);
	}

	private void DataHandler_SteelChange(int opcodeCMD)
	{
		this.SteelResLabel.ChangNumText(HeroInfo.GetInstance().playerRes.resSteel, 2f, null);
	}

	private void DataHandler_OilChange(int opcodeCMD)
	{
		this.OilResLabel.ChangNumText(HeroInfo.GetInstance().playerRes.resOil, 2f, null);
	}

	private void DataHandler_CoinChange(int opcodeCMD)
	{
		this.CoinResLabel.ChangNumText(HeroInfo.GetInstance().playerRes.resCoin, 2f, null);
	}

	private void DisplayRes()
	{
		if (HeroInfo.GetInstance().playerRes.maxCoin > 0)
		{
			this.SetResCoin();
		}
		else
		{
			this.Coin.SetActive(false);
		}
		if (HeroInfo.GetInstance().playerRes.maxOil > 0)
		{
			this.SetResOil();
		}
		else
		{
			this.Oil.SetActive(false);
		}
		if (HeroInfo.GetInstance().playerRes.maxSteel > 0)
		{
			this.SetResSteel();
		}
		else
		{
			this.Steel.SetActive(false);
		}
		if (HeroInfo.GetInstance().playerRes.maxRareEarth > 0)
		{
			this.SetResRareEarth();
		}
		else
		{
			this.RareEarth.SetActive(false);
		}
		this.SetResRMB();
		this.ResGrid.Reposition();
		this.ResGrid.transform.DOLocalMoveY(this.height, 0.8f, false);
	}

	public void OnSetPostion()
	{
		base.gameObject.transform.localPosition = Vector3.zero;
		if (!this.Oil.activeSelf)
		{
			base.gameObject.transform.localPosition = new Vector3(-62801f, this.height, 0f);
			return;
		}
		if (!this.Steel.activeSelf)
		{
			base.gameObject.transform.localPosition = new Vector3(-62688f, this.height, 0f);
			return;
		}
		if (!this.RareEarth.activeSelf)
		{
			base.gameObject.transform.localPosition = new Vector3(-62631f, this.height, 0f);
			return;
		}
		base.gameObject.transform.localPosition = new Vector3(-62593f, this.height, 0f);
	}

	private void SetResRMB()
	{
		this.RMBResLabel.textLable.text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
	}

	private void SetResRareEarth()
	{
		this.RareEarthResLabel.textLable.text = HeroInfo.GetInstance().playerRes.resRareEarth.ToString();
		this.RareEarthResLabel.textUISprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resRareEarth / (float)HeroInfo.GetInstance().playerRes.maxRareEarth;
		if (HeroInfo.GetInstance().playerRes.resRareEarth >= HeroInfo.GetInstance().playerRes.maxRareEarth)
		{
			this.RareEarthResLabel.textLable.color = Color.red;
		}
		else
		{
			this.RareEarthResLabel.textLable.color = Color.white;
		}
	}

	private void SetResSteel()
	{
		this.SteelResLabel.textLable.text = HeroInfo.GetInstance().playerRes.resSteel.ToString();
		this.SteelResLabel.textUISprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resSteel / (float)HeroInfo.GetInstance().playerRes.maxSteel;
		if (HeroInfo.GetInstance().playerRes.resSteel >= HeroInfo.GetInstance().playerRes.maxSteel)
		{
			this.SteelResLabel.textLable.color = Color.red;
		}
		else
		{
			this.SteelResLabel.textLable.color = Color.white;
		}
	}

	private void SetResOil()
	{
		this.OilResLabel.textLable.text = HeroInfo.GetInstance().playerRes.resOil.ToString();
		this.OilResLabel.textUISprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resOil / (float)HeroInfo.GetInstance().playerRes.maxOil;
		if (HeroInfo.GetInstance().playerRes.resOil >= HeroInfo.GetInstance().playerRes.maxOil)
		{
			this.OilResLabel.textLable.color = Color.red;
		}
		else
		{
			this.OilResLabel.textLable.color = Color.white;
		}
	}

	private void SetResCoin()
	{
		this.CoinResLabel.textLable.text = HeroInfo.GetInstance().playerRes.resCoin.ToString();
		this.CoinResLabel.textUISprite.fillAmount = (float)HeroInfo.GetInstance().playerRes.resCoin / (float)HeroInfo.GetInstance().playerRes.maxCoin;
		if (HeroInfo.GetInstance().playerRes.resCoin >= HeroInfo.GetInstance().playerRes.maxCoin)
		{
			this.CoinResLabel.textLable.color = Color.red;
		}
		else
		{
			this.CoinResLabel.textLable.color = Color.white;
		}
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
			ClientMgr.GetNet().NetDataHandler.CoinChange -= new DataHandler.Data_Change(this.DataHandler_CoinChange);
			ClientMgr.GetNet().NetDataHandler.OilChange -= new DataHandler.Data_Change(this.DataHandler_OilChange);
			ClientMgr.GetNet().NetDataHandler.SteelChange -= new DataHandler.Data_Change(this.DataHandler_SteelChange);
			ClientMgr.GetNet().NetDataHandler.RareEarthChange -= new DataHandler.Data_Change(this.DataHandler_RareEarthChange);
		}
	}

	private void Update()
	{
	}
}
