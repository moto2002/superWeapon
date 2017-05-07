using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatArmyPanel : MonoBehaviour
{
	public static CreatArmyPanel ins;

	public UIInput inputArmyName;

	public UIInput inputArmyDes;

	public UIInput inputNeedMedalCount;

	public UILabel armyOpenType;

	public GameObject armyOpenTypeBtn;

	public int Armytype;

	public UISprite armyIcon;

	public UILabel needCoinLabel;

	public GameObject armyIconSeLect;

	public UIScrollView iconView;

	public UITable iconTable;

	public GameObject iconPrefab;

	public void OnDestroy()
	{
		CreatArmyPanel.ins = null;
	}

	private void Awake()
	{
		CreatArmyPanel.ins = this;
	}

	public void OnEnable()
	{
		this.init();
	}

	private void init()
	{
		this.armyOpenTypeBtn.GetComponent<UIPopupList>().items.Clear();
		this.armyOpenTypeBtn.GetComponent<UIPopupList>().items.Add(LanguageManage.GetTextByKey("向所有人开放", "Battle"));
		this.armyOpenTypeBtn.GetComponent<UIPopupList>().items.Add(LanguageManage.GetTextByKey("不向所有人开放", "Battle"));
		EventDelegate.Add(this.armyOpenTypeBtn.GetComponent<UIPopupList>().onChange, new EventDelegate.Callback(this.ChangeType));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_SetArmyIcon, new EventManager.VoidDelegate(this.SetArmyIcon));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_SureCreatArmy, new EventManager.VoidDelegate(this.SureCreatArmy));
		using (Dictionary<int, ArmyIconClass>.Enumerator enumerator = UnitConst.GetInstance().armyIcon.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<int, ArmyIconClass> current = enumerator.Current;
				this.armyIcon.spriteName = current.Value.name.ToString();
				this.armyIcon.name = current.Key.ToString();
			}
		}
	}

	private void ChangeType()
	{
		this.armyOpenType.text = this.armyOpenTypeBtn.GetComponent<UIPopupList>().value;
		for (int i = 0; i < this.armyOpenTypeBtn.GetComponent<UIPopupList>().items.Count; i++)
		{
			if (this.armyOpenType.text.Equals(this.armyOpenTypeBtn.GetComponent<UIPopupList>().items[i]))
			{
				this.Armytype = i + 1;
			}
		}
	}

	private void SetArmyIcon(GameObject ga)
	{
		ArmyManager.ins.OpenChangeLegionIconPanel();
	}

	private void SureCreatArmy(GameObject ga)
	{
		if (GameTools.CheckStringlength(this.inputArmyName.value) > 12)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团名称长度不能超过12个字符", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (string.IsNullOrEmpty(this.inputArmyName.value))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团名称长度不能为0", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (GameTools.CheckStringlength(this.inputArmyDes.value) > 140)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团公告长度不能超过140个字符", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (string.IsNullOrEmpty(this.inputNeedMedalCount.value))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("请输入奖牌数目", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (int.Parse(this.inputNeedMedalCount.value) > 1000)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("加入军团的奖牌数目上限不能超过", "Battle") + "1000", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		ArmyGroupHandler.CG_CSLegionCreate(this.inputArmyName.value, int.Parse(this.armyIcon.name), int.Parse(this.inputNeedMedalCount.value), this.Armytype, this.inputArmyDes.value, delegate(bool isError)
		{
			if (!isError)
			{
				ArmyGroupManager.isSearch = false;
				ArmyManager.ins.ChangePanel();
			}
		});
	}
}
