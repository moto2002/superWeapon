using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmsDelaerItem : MonoBehaviour
{
	public UILabel tittleName;

	public UILabel ItemNum;

	public UILabel costNum;

	public UISprite itemQuSprite;

	public UISprite ItemIcon2;

	public GameObject NoBuy;

	public GameObject buyBtn;

	public GameObject backLayer;

	public GameObject backSprite;

	public UISprite ResTypeIcon;

	[HideInInspector]
	public ArmsDealer curArmsDealer;

	public static ArmsDelaerItem _inst;

	public int CostResType;

	public int CostItemType;

	public int CostNum;

	public void OnDestroy()
	{
		ArmsDelaerItem._inst = null;
	}

	private void Start()
	{
		ArmsDelaerItem._inst = this;
	}

	private void Awake()
	{
		this.tittleName = base.transform.FindChild("icon/name").GetComponent<UILabel>();
		this.ItemNum = base.transform.FindChild("icon/num").GetComponent<UILabel>();
		this.costNum = base.transform.FindChild("BuyBtn/costIcon/Label").GetComponent<UILabel>();
		this.itemQuSprite = base.transform.FindChild("icon/Sprite").GetComponent<UISprite>();
		this.ItemIcon2 = base.transform.FindChild("icon").GetComponent<UISprite>();
		this.NoBuy = base.transform.FindChild("NoBuy").gameObject;
		this.buyBtn = base.transform.FindChild("BuyBtn").gameObject;
		if (!this.buyBtn.GetComponent<ArmsDelaerBtn>())
		{
			this.buyBtn.AddComponent<ArmsDelaerBtn>();
		}
		ArmsDelaerBtn component = this.buyBtn.GetComponent<ArmsDelaerBtn>();
		component.btnType = ArmsDealerPanel.ArmsDealerBtnType.Buy;
		this.backLayer = base.transform.FindChild("BlackLayer").gameObject;
		this.backSprite = base.transform.FindChild("icon/Sprite").gameObject;
	}

	public void Display()
	{
		if (this.curArmsDealer != null)
		{
			int num = 0;
			int num2 = 0;
			if (this.curArmsDealer.ItemSeller.Count != 0)
			{
				using (Dictionary<int, int>.Enumerator enumerator = this.curArmsDealer.ItemSeller.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						KeyValuePair<int, int> current = enumerator.Current;
						num = current.Value;
					}
				}
			}
			else if (this.curArmsDealer.SkillSeller.Count != 0)
			{
				using (Dictionary<int, int>.Enumerator enumerator2 = this.curArmsDealer.SkillSeller.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						KeyValuePair<int, int> current2 = enumerator2.Current;
						num = current2.Value;
					}
				}
			}
			this.ItemNum.text = "X" + num;
			if (this.curArmsDealer.priceType == 1)
			{
				using (Dictionary<ResType, int>.Enumerator enumerator3 = this.curArmsDealer.ResBuyer.GetEnumerator())
				{
					if (enumerator3.MoveNext())
					{
						KeyValuePair<ResType, int> current3 = enumerator3.Current;
						num2 = current3.Value;
						this.CostResType = (int)current3.Key;
						this.CostNum = current3.Value;
						AtlasManage.SetResSpriteName(this.ResTypeIcon, current3.Key);
					}
				}
			}
			else if (this.curArmsDealer.priceType == 2)
			{
				using (Dictionary<int, int>.Enumerator enumerator4 = this.curArmsDealer.ItemBuyer.GetEnumerator())
				{
					if (enumerator4.MoveNext())
					{
						KeyValuePair<int, int> current4 = enumerator4.Current;
						num2 = current4.Value;
						this.CostItemType = current4.Key;
						this.CostNum = current4.Value;
						AtlasManage.SetUiItemAtlas(this.ResTypeIcon, UnitConst.GetInstance().ItemConst[current4.Key].IconId);
					}
				}
			}
			this.costNum.text = num2.ToString();
			this.NoBuy.SetActive(this.curArmsDealer.isSelled);
			if (this.NoBuy.activeSelf)
			{
				this.ItemIcon2.ShaderToGray();
				this.itemQuSprite.ShaderToGray();
			}
			else
			{
				this.ItemIcon2.ShaderToNormal();
				this.itemQuSprite.ShaderToNormal();
			}
			this.backLayer.SetActive(this.curArmsDealer.isSelled);
			this.buyBtn.SetActive(!this.curArmsDealer.isSelled);
		}
	}

	private void Update()
	{
		if (this.CostResType > 0)
		{
			switch (this.CostResType)
			{
			case 1:
				this.costNum.color = ((HeroInfo.GetInstance().playerRes.resCoin >= this.CostNum) ? Color.white : Color.red);
				break;
			case 2:
				this.costNum.color = ((HeroInfo.GetInstance().playerRes.resOil >= this.CostNum) ? Color.white : Color.red);
				break;
			case 3:
				this.costNum.color = ((HeroInfo.GetInstance().playerRes.resSteel >= this.CostNum) ? Color.white : Color.red);
				break;
			case 4:
				this.costNum.color = ((HeroInfo.GetInstance().playerRes.resRareEarth >= this.CostNum) ? Color.white : Color.red);
				break;
			case 7:
				this.costNum.color = ((HeroInfo.GetInstance().playerRes.RMBCoin >= this.CostNum) ? Color.white : Color.red);
				break;
			}
		}
		else if (this.CostItemType > 0)
		{
			if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.CostItemType))
			{
				this.costNum.color = ((HeroInfo.GetInstance().PlayerItemInfo[this.CostItemType] >= this.CostNum) ? Color.white : Color.red);
			}
			else
			{
				this.costNum.color = Color.red;
			}
		}
	}
}
