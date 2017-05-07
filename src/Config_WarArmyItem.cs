using System;
using UnityEngine;

public class Config_WarArmyItem : IMonoBehaviour
{
	public UILabel armyNum;

	public UISprite armyIcom;

	public GameObject addSolider;

	public GameObject armyUnLock;

	public GameObject armyLock;

	public GameObject armyFuncingGa;

	[HideInInspector]
	public int infoKey;

	public void FixedUpdate()
	{
	}

	public void DoSendADC()
	{
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
	}
}
