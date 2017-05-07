using System;
using System.Collections.Generic;
using UnityEngine;

public class TankTeamOperation : MonoBehaviour
{
	public static TankTeamOperation inst;

	public UISprite Button_BMP;

	public UISprite Button_Light;

	public UILabel Button_Des;

	public int TankTeamOperation_Item;

	public bool TankTeamOperationOpen;

	private float buttonCD;

	public float jd;

	public List<T_TankAbstract> ChooseTankTeam;

	public int TankTeamIndex;

	public void OnDestroy()
	{
		TankTeamOperation.inst = null;
	}

	private void Awake()
	{
		TankTeamOperation.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanle_IsControlSolider, new EventManager.VoidDelegate(this.Click));
	}

	private void Start()
	{
		this.TankTeamOperationOpen = false;
		this.Button_Light.enabled = false;
	}

	public void Button_Show(bool show)
	{
		base.gameObject.SetActive(show);
	}

	private void Click(GameObject ga)
	{
		if (this.buttonCD <= 0f)
		{
			if (!this.TankTeamOperationOpen)
			{
				if (FightPanelManager.inst.CurSelectUIItem != null)
				{
					FightPanelManager.inst.CurSelectUIItem = null;
				}
				this.TankTeamOperationOpen = true;
				this.Button_Light.enabled = true;
				this.jd = 0f;
				foreach (KeyValuePair<int, SoldierUIITem> current in FightPanelManager.inst.solider_UIDIC)
				{
					if (current.Value)
					{
						this.CardToTank(current.Value);
						break;
					}
				}
			}
			else
			{
				EventNoteMgr.NoticeSelControlArmyID(0, 0);
				foreach (KeyValuePair<int, SoldierUIITem> current2 in FightPanelManager.inst.solider_UIDIC)
				{
					if (current2.Value)
					{
						current2.Value.light1.gameObject.SetActive(false);
					}
				}
				this.ButtonClose();
			}
			this.buttonCD = 0.5f;
		}
	}

	private void Update()
	{
		if (this.buttonCD >= 0f)
		{
			this.buttonCD -= Time.deltaTime;
		}
		if (this.TankTeamOperationOpen)
		{
			this.jd = Mathf.Min(360f, this.jd + 500f * Time.deltaTime);
		}
	}

	public void ButtonClose()
	{
		this.TankTeamOperationOpen = false;
		this.Button_Light.enabled = false;
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			if (SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam != null)
			{
				UnityEngine.Object.Destroy(SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam.ga);
			}
		}
	}

	public void CardToTank(SoldierUIITem solideruiitem)
	{
		if (solideruiitem.soliderIndex > 1000)
		{
			EventNoteMgr.NoticeSelControlArmyID(2, solideruiitem.soliderIndex - 1000);
		}
		else
		{
			EventNoteMgr.NoticeSelControlArmyID(1, solideruiitem.soliderIndex);
		}
		foreach (KeyValuePair<int, SoldierUIITem> current in FightPanelManager.inst.solider_UIDIC)
		{
			if (current.Value)
			{
				if (current.Value == solideruiitem)
				{
					this.TankTeamOperation_Item = solideruiitem.soliderIndex;
					current.Value.light1.gameObject.SetActive(true);
				}
				else
				{
					current.Value.light1.gameObject.SetActive(false);
				}
			}
		}
		solideruiitem.light1.gameObject.SetActive(true);
		this.ChooseTankTeam.Clear();
		this.TankTeamIndex = 0;
		string audioName = string.Empty;
		int soliderIndex = solideruiitem.soliderIndex;
		switch (soliderIndex)
		{
		case 1:
			this.TankTeamIndex = 1;
			audioName = "awaitorder";
			break;
		case 2:
			this.TankTeamIndex = 2;
			audioName = "huixiong";
			break;
		case 3:
			this.TankTeamIndex = 3;
			audioName = "jiguang";
			break;
		case 4:
			this.TankTeamIndex = 4;
			audioName = "huojian";
			break;
		case 5:
			this.TankTeamIndex = 5;
			audioName = "jiguang";
			break;
		case 6:
			this.TankTeamIndex = 6;
			audioName = "awaitorder";
			break;
		default:
			if (soliderIndex != 1001)
			{
				if (soliderIndex != 1002)
				{
					this.TankTeamIndex = solideruiitem.soliderIndex;
				}
				else
				{
					this.TankTeamIndex = 102;
					audioName = "jiguang";
				}
			}
			else
			{
				this.TankTeamIndex = 101;
				audioName = "awaitorder";
			}
			break;
		}
		AudioManage.inst.PlayAuido(audioName, false);
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			if (SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam != null)
			{
				UnityEngine.Object.Destroy(SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam.ga);
			}
			if (SenceManager.inst.Tanks_Attack[i].index == this.TankTeamIndex && SenceManager.inst.Tanks_Attack[i].tankType != T_TankAbstract.TankType.特种兵)
			{
				this.ChooseTankTeam.Add(SenceManager.inst.Tanks_Attack[i]);
				SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam = PoolManage.Ins.GetEffectByName("heidongzhuangjia", SenceManager.inst.Tanks_Attack[i].tr);
			}
			else if ((this.TankTeamIndex == 101 || this.TankTeamIndex == 102) && SenceManager.inst.Tanks_Attack[i].tankType == T_TankAbstract.TankType.特种兵)
			{
				this.ChooseTankTeam.Add(SenceManager.inst.Tanks_Attack[i]);
				SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.Effect_TankTeam = PoolManage.Ins.GetEffectByName("heidongzhuangjia", SenceManager.inst.Tanks_Attack[i].tr);
			}
		}
	}
}
