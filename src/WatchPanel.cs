using System;
using UnityEngine;

public class WatchPanel : MonoBehaviour
{
	public UILabel lv;

	public UILabel name_Client;

	public UISprite ExpSp;

	public UILabel VIPLevel;

	public UILabel JiangPai;

	public GameObject powerGame;

	private void Start()
	{
		this.lv.text = SenceInfo.SpyPlayerInfo.ownerLevel.ToString();
		this.name_Client.text = SenceInfo.SpyPlayerInfo.ownerName;
		this.JiangPai.text = SenceInfo.SpyPlayerInfo.medal.ToString();
		this.VIPLevel.text = string.Format("VIP {0}", SenceInfo.SpyPlayerInfo.vip);
		this.ExpSp.fillAmount = (float)HeroInfo.GetInstance().playerRes.playerExp / (float)UnitConst.GetInstance().PlayerExpConst[HeroInfo.GetInstance().playerlevel];
		MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, this.powerGame);
	}
}
