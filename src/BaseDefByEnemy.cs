using System;
using UnityEngine;

public class BaseDefByEnemy : TipBase
{
	public UILabel lblName;

	public UILabel lblLevel;

	public SendSolider[] enemySolider;

	public SendSolider[] destorySolider;

	public GameObject videoLostObj;

	public GameObject fuChouObj;

	public UILabel medalNum;

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

	public void OnClosePanel()
	{
		base.gameObject.SetActive(false);
	}
}
