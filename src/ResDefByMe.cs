using System;
using UnityEngine;

public class ResDefByMe : TipBase
{
	public UILabel lblName;

	public SendSolider[] enemySolider;

	public SendSolider[] destorySolider;

	public GameObject videoLostObj;

	public GameObject fuChouObj;

	public UIButton btnWatchFire;

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
