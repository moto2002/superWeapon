using System;
using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class PVP_CoinNeedUIlabel : MonoBehaviour
{
	private UILabel moneyUIlabel;

	public void Awake()
	{
		this.moneyUIlabel = base.GetComponent<UILabel>();
	}

	public void OnEnable()
	{
		int num = int.Parse(UnitConst.GetInstance().DesighConfigDic[65].value) * HeroInfo.GetInstance().PlayerCommondLv;
		this.moneyUIlabel.text = num.ToString();
		if (HeroInfo.GetInstance().playerRes.resCoin < num)
		{
			this.moneyUIlabel.color = Color.red;
		}
		else
		{
			this.moneyUIlabel.color = Color.green;
		}
	}
}
