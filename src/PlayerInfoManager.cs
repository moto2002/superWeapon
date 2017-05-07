using System;
using UnityEngine;

public class PlayerInfoManager : FuncUIPanel
{
	public UILabel playerNameLabe;

	public UILabel accountLabel;

	public UILabel playerLevelLabel;

	public UILabel playerExpLabel;

	public UILabel serverLabel;

	public UILabel armyGroupLabel;

	public GameObject leftOnOff;

	public GameObject rightOnOff;

	public UILabel leftOnOffLabel;

	public UILabel rightOnOffLabel;

	public static PlayerInfoManager ins;

	public static int left = 1;

	public static int right = 1;

	public GameObject LanguageChange_Button;

	public UILabel LanguageChange_ButtonLabel;

	public override void Awake()
	{
		PlayerInfoManager.ins = this;
	}

	public override void OnEnable()
	{
		this.Init();
		this.ShowRolerInfo();
		this.RefreshMusic();
		this.RefreshSoundEffect();
		base.OnEnable();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_Close, new EventManager.VoidDelegate(this.CloseThis));
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_CloseMusic, new EventManager.VoidDelegate(this.OpenMusic));
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_CloseMusicEffect, new EventManager.VoidDelegate(this.OpenSoundEffect));
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_Exit, new EventManager.VoidDelegate(this.ExitGame));
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_LoginAgain, new EventManager.VoidDelegate(this.LoginAgain));
		EventManager.Instance.AddEvent(EventManager.EventType.PlayerInfoPanel_LanguageChange, new EventManager.VoidDelegate(this.LanguageChange));
		EventDelegate.Add(this.LanguageChange_Button.GetComponent<UIPopupList>().onChange, new EventDelegate.Callback(this.LanguageChoose));
		this.LanguageChange_Button.GetComponent<UIPopupList>().items.Clear();
		for (int i = 0; i < LanguageManage.LanguageCategory.Length; i++)
		{
			if (LanguageManage.LanguageCategory[i].UiName == "English")
			{
			}
			this.LanguageChange_Button.GetComponent<UIPopupList>().items.Add(LanguageManage.LanguageCategory[i].UiName);
		}
		this.LanguageChange_Button.GetComponent<UIPopupList>().value = LanguageManage.LanguageCategory[User.GetLanguageSetting()].UiName;
		this.LanguageChange_ButtonLabel.text = LanguageManage.LanguageCategory[User.GetLanguageSetting()].UiName;
	}

	public void ShowRolerInfo()
	{
		this.playerNameLabe.text = HeroInfo.GetInstance().userName;
		this.accountLabel.text = HeroInfo.GetInstance().userId.ToString();
		this.playerLevelLabel.text = HeroInfo.GetInstance().playerlevel.ToString();
		if (HeroInfo.GetInstance().playerlevel < int.Parse(UnitConst.GetInstance().DesighConfigDic[71].value))
		{
			this.playerExpLabel.text = HeroInfo.GetInstance().playerRes.playerExp + "/" + UnitConst.GetInstance().PlayerExpConst[HeroInfo.GetInstance().playerlevel];
		}
		else
		{
			this.playerExpLabel.text = LanguageManage.GetTextByKey("已满级", "others");
		}
		if (HeroInfo.GetInstance().playerGroupId == 0L)
		{
			this.armyGroupLabel.text = LanguageManage.GetTextByKey("未加入军团", "others");
		}
		else
		{
			this.armyGroupLabel.text = HeroInfo.GetInstance().playerGroup;
		}
		this.serverLabel.text = HUDTextTool.serverName;
	}

	private void LanguageChange(GameObject ga)
	{
	}

	private void LanguageChoose()
	{
		if (!(this.LanguageChange_ButtonLabel.text == this.LanguageChange_Button.GetComponent<UIPopupList>().value))
		{
			this.LanguageChange_ButtonLabel.text = this.LanguageChange_Button.GetComponent<UIPopupList>().value;
			LanguageCategory[] languageCategory = LanguageManage.LanguageCategory;
			for (int i = 0; i < languageCategory.Length; i++)
			{
				LanguageCategory lanuage_Sel = languageCategory[i];
				if (lanuage_Sel.UiName == this.LanguageChange_Button.GetComponent<UIPopupList>().value)
				{
					LanguageManage.LoadLanguageContent(lanuage_Sel);
				}
			}
			this.RefreshMusic();
			this.RefreshSoundEffect();
			this.ShowRolerInfo();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F4))
		{
			this.LanguageChange_Button.gameObject.SetActive(!this.LanguageChange_Button.gameObject.activeSelf);
		}
	}

	public void CloseThis(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("PlayerInfoPanel");
	}

	public void RefreshMusic()
	{
		if (User.GetMusicState() == 1)
		{
			this.leftOnOff.transform.localPosition = new Vector3(49.2f, 0f, 0f);
			this.leftOnOffLabel.transform.localPosition = new Vector3(-9f, -2.3f, 0f);
			this.leftOnOffLabel.text = LanguageManage.GetTextByKey("开", "others");
			AudioManage.inst.IsOpenMusic = true;
			PlayerInfoManager.left = 1;
			return;
		}
		if (User.GetMusicState() == 2)
		{
			this.leftOnOff.transform.localPosition = new Vector3(-49.2f, 0f, 0f);
			this.leftOnOffLabel.transform.localPosition = new Vector3(6f, -2.3f, 0f);
			this.leftOnOffLabel.text = LanguageManage.GetTextByKey("关", "others");
			AudioManage.inst.IsOpenMusic = false;
			PlayerInfoManager.right = 2;
			return;
		}
	}

	public void RefreshSoundEffect()
	{
		if (User.GetSoundEffectState() == 1)
		{
			this.rightOnOff.transform.localPosition = new Vector3(49.2f, 0f, 0f);
			this.rightOnOffLabel.transform.localPosition = new Vector3(-9f, -2.3f, 0f);
			this.rightOnOffLabel.text = LanguageManage.GetTextByKey("开", "others");
			AudioManage.inst.IsOpenPlayAudioBackGround = true;
			PlayerInfoManager.right = 1;
			return;
		}
		if (User.GetSoundEffectState() == 2)
		{
			this.rightOnOff.transform.localPosition = new Vector3(-49.2f, 0f, 0f);
			this.rightOnOffLabel.transform.localPosition = new Vector3(6f, -2.3f, 0f);
			this.rightOnOffLabel.text = LanguageManage.GetTextByKey("关", "others");
			AudioManage.inst.IsOpenPlayAudioBackGround = false;
			PlayerInfoManager.right = 2;
			return;
		}
	}

	public void OpenMusic(GameObject ga)
	{
		if (PlayerInfoManager.left == 1)
		{
			this.leftOnOff.transform.localPosition = new Vector3(-49.2f, 0f, 0f);
			this.leftOnOffLabel.transform.localPosition = new Vector3(6f, -2.3f, 0f);
			this.leftOnOffLabel.text = LanguageManage.GetTextByKey("关", "others");
			PlayerInfoManager.left = 2;
			AudioManage.inst.IsOpenMusic = false;
			PlayerPrefs.SetInt("Music", 2);
			return;
		}
		if (PlayerInfoManager.left == 2)
		{
			this.leftOnOff.transform.localPosition = new Vector3(49.2f, 0f, 0f);
			this.leftOnOffLabel.transform.localPosition = new Vector3(-9f, -2.3f, 0f);
			this.leftOnOffLabel.text = LanguageManage.GetTextByKey("开", "others");
			PlayerInfoManager.left = 1;
			PlayerPrefs.SetInt("Music", 1);
			AudioManage.inst.IsOpenMusic = true;
			return;
		}
	}

	public void OpenSoundEffect(GameObject ga)
	{
		if (PlayerInfoManager.right == 1)
		{
			this.rightOnOff.transform.localPosition = new Vector3(-49.2f, 0f, 0f);
			this.rightOnOffLabel.transform.localPosition = new Vector3(6f, -2.3f, 0f);
			this.rightOnOffLabel.text = LanguageManage.GetTextByKey("关", "others");
			PlayerInfoManager.right = 2;
			PlayerPrefs.SetInt("Sound", 2);
			AudioManage.inst.IsOpenPlayAudioBackGround = false;
			return;
		}
		if (PlayerInfoManager.right == 2)
		{
			this.rightOnOff.transform.localPosition = new Vector3(49.2f, 0f, 0f);
			this.rightOnOffLabel.transform.localPosition = new Vector3(-9f, -2.3f, 0f);
			this.rightOnOffLabel.text = LanguageManage.GetTextByKey("开", "others");
			PlayerInfoManager.right = 1;
			AudioManage.inst.IsOpenPlayAudioBackGround = true;
			PlayerPrefs.SetInt("Sound", 1);
			return;
		}
	}

	public void LoginAgain(GameObject ga)
	{
		HttpMgr.ReStartGame();
	}

	public void ExitGame(GameObject ga)
	{
		HDSDKInit.ExitGame();
	}
}
