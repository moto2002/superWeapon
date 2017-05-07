using System;
using UnityEngine;

public class ResDefByEnemy : TipBase
{
	public UILabel lblName;

	public UILabel lbLeve;

	public SendSolider[] enemySolider;

	public SendSolider[] destorySolider;

	public GameObject videoLostObj;

	public GameObject fuChouObj;

	public UIButton btnWatchFire;

	public UIButton btnShare;

	public override void Open(ReportData thisData)
	{
		base.Open(thisData);
		this.RefreshUI();
	}

	public override void RefreshUI()
	{
	}
}
