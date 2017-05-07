using System;
using UnityEngine;

public class IslandAttrackFaild : TipBase
{
	public GetItemAward[] itemGet;

	public GetResourceAward[] resGet;

	public SendSolider[] enemySolider;

	public SendSolider[] destorySolider;

	public UILabel name_Client;

	public UILabel level;

	public GameObject ZhenChaname;

	public GameObject JingongName;

	public override void Open(ReportData thisData)
	{
		base.Open(thisData);
		this.RefreshUI();
	}

	public override void RefreshUI()
	{
	}
}
