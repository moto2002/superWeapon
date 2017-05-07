using System;
using UnityEngine;

public class BaseDefByMe : TipBase
{
	public UILabel lblName;

	public SendSolider[] enemySolider;

	public SendSolider[] destorySolider;

	public GameObject videoLostObj;

	public GameObject fuChouObj;

	public UILabel Level;

	public UIButton btnWatchFire;

	public UIButton btnGetMoney;

	public UIButton btnFuChou;

	public override void Open(ReportData thisData)
	{
		base.Open(thisData);
		this.RefreshUI();
	}

	public override void RefreshUI()
	{
	}
}
