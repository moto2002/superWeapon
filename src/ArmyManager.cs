using System;
using UnityEngine;

public class ArmyManager : FuncUIPanel
{
	public static ArmyManager ins;

	public ArmyGroupManager armyGropManger;

	public ArmyPeopleManager armyPeopleManger;

	public GameObject ChangeLegionIconPanel;

	public GameObject LegionGiftPanel;

	public void OnDestroy()
	{
		ArmyManager.ins = null;
	}

	public void OpenChangeLegionIconPanel()
	{
		this.ChangeLegionIconPanel.gameObject.SetActive(true);
	}

	public void OpenLegionGiftPanel(bool set)
	{
		this.LegionGiftPanel.gameObject.SetActive(set);
	}

	public new void Awake()
	{
		ArmyManager.ins = this;
	}

	public new void OnEnable()
	{
		this.Init();
		this.ChangePanel();
		base.OnEnable();
	}

	public new void OnDisable()
	{
		base.OnDisable();
	}

	public void ChangePanel()
	{
		if (HeroInfo.GetInstance().playerGroupId == 0L)
		{
			this.armyGropManger.gameObject.SetActive(true);
			this.armyPeopleManger.gameObject.SetActive(false);
		}
		else
		{
			this.armyGropManger.gameObject.SetActive(false);
			this.armyPeopleManger.gameObject.SetActive(true);
		}
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_ClosePanel, new EventManager.VoidDelegate(this.CloseThis));
	}

	public void CloseThis(GameObject ga)
	{
		if (this.LegionGiftPanel.gameObject.activeSelf)
		{
			this.OpenLegionGiftPanel(false);
			return;
		}
		this.ChangeLegionIconPanel.gameObject.SetActive(false);
		FuncUIManager.inst.HideFuncUI("ArmyPeoplePanlManager");
	}

	public void Refresh()
	{
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.OpenArmyGroup(null);
		}
	}
}
