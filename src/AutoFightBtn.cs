using System;
using UnityEngine;

public class AutoFightBtn : MonoBehaviour
{
	public static AutoFightBtn _inst;

	public UILabel lab;

	public void OnDestroy()
	{
		AutoFightBtn._inst = null;
	}

	private void Awake()
	{
		AutoFightBtn._inst = this;
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_AutoFight, new EventManager.VoidDelegate(this.AutoFight));
		if (HeroInfo.GetInstance().PlayerCommondLv >= 3)
		{
			base.gameObject.SetActive(true);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void AutoFight(GameObject ga)
	{
		if (FightPanelManager.IsRetreat)
		{
			return;
		}
		this.SetAutoFight();
	}

	public void SetAutoFight()
	{
		bool autoFight = GameSetting.autoFight;
		if (autoFight)
		{
			if (autoFight)
			{
				GameSetting.autoFight = false;
				this.lab.text = LanguageManage.GetTextByKey("自动战斗", "others");
				EventNoteMgr.NoticeOpenAutoFight(false);
				base.GetComponent<UISprite>().spriteName = "自动战斗";
			}
		}
		else
		{
			GameSetting.autoFight = true;
			this.lab.text = LanguageManage.GetTextByKey("手动操作", "others");
			base.GetComponent<UISprite>().spriteName = "自动战斗选中";
			EventNoteMgr.NoticeOpenAutoFight(true);
			SenceManager.inst.TankSearching();
		}
	}
}
