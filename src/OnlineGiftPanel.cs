using System;
using System.Collections.Generic;
using UnityEngine;

public class OnlineGiftPanel : MonoBehaviour
{
	public GameObject leftPrefab;

	public GameObject itemPrefab;

	public GameObject skillPrefab;

	public GameObject resPrefab;

	public UIScrollView leftScrollView;

	public UIGrid leftTabel;

	public UIGrid RightGrid;

	public UISprite GetBtn;

	public UILabel GetBtnLabel;

	public UILabel GetBtnTimeLabel;

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPanel_LeftBtnClick, new EventManager.VoidDelegate(this.LeftBtnClick));
		this.ShowOnLine();
	}

	private void LeftBtnClick(GameObject ga)
	{
		this.ChangeOnLineGift(int.Parse(ga.name));
	}

	private void RightBtnClick(GameObject ga)
	{
	}

	private void CloseBtnClick(GameObject ga)
	{
	}

	private void ChangeOnLineGift(int i)
	{
		this.RightGrid.ClearChild();
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().loadReward[i].res)
		{
			GameObject gameObject = NGUITools.AddChild(this.RightGrid.gameObject, this.resPrefab);
			ActivityRes component = gameObject.GetComponent<ActivityRes>();
			switch (current.Key)
			{
			case ResType.金币:
				AtlasManage.SetResSpriteName(component.icon, ResType.金币);
				component.count.text = current.Value.ToString();
				break;
			case ResType.石油:
				AtlasManage.SetResSpriteName(component.icon, ResType.石油);
				component.count.text = current.Value.ToString();
				break;
			case ResType.钢铁:
				AtlasManage.SetResSpriteName(component.icon, ResType.钢铁);
				component.count.text = current.Value.ToString();
				break;
			case ResType.稀矿:
				AtlasManage.SetResSpriteName(component.icon, ResType.稀矿);
				component.count.text = current.Value.ToString();
				break;
			case ResType.钻石:
				AtlasManage.SetResSpriteName(component.icon, ResType.钻石);
				component.count.text = current.Value.ToString();
				break;
			}
		}
		this.RightGrid.Reposition();
		foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().loadReward[i].item)
		{
			GameObject gameObject2 = NGUITools.AddChild(this.RightGrid.gameObject, this.itemPrefab);
			ActivityItemPre component2 = gameObject2.GetComponent<ActivityItemPre>();
			AtlasManage.SetUiItemAtlas(component2.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component2.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			component2.name.text = current2.Value.ToString();
			component2.count.gameObject.SetActive(false);
			ItemTipsShow2 component3 = component2.GetComponent<ItemTipsShow2>();
			component3.Index = current2.Key;
			component3.Type = 1;
		}
		this.RightGrid.Reposition();
		foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().loadReward[i].skill)
		{
			GameObject gameObject3 = NGUITools.AddChild(this.RightGrid.gameObject, this.skillPrefab);
			ActivitySkillPrefab component4 = gameObject3.GetComponent<ActivitySkillPrefab>();
			AtlasManage.SetSkillSpritName(component4.icon, UnitConst.GetInstance().skillList[current3.Key].icon);
			AtlasManage.SetQuilitySpriteName(component4.bg, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
			component4.name.text = UnitConst.GetInstance().skillList[current3.Key].name;
		}
		this.RightGrid.Reposition();
		foreach (KeyValuePair<ResType, int> current4 in UnitConst.GetInstance().loadReward[i].money)
		{
			GameObject gameObject4 = NGUITools.AddChild(this.RightGrid.gameObject, this.resPrefab);
			ActivityRes component5 = gameObject4.GetComponent<ActivityRes>();
			ResType key = current4.Key;
			if (key == ResType.钻石)
			{
				AtlasManage.SetResSpriteName(component5.icon, ResType.钻石);
				component5.count.text = current4.Value.ToString();
			}
		}
		this.RightGrid.Reposition();
		base.StartCoroutine(this.RightGrid.RepositionAfterFrame());
		if ((OnLineAward.laod.step > 0 && OnLineAward.laod.step > UnitConst.GetInstance().loadReward[i].id) || OnLineAward.laod.step < 1)
		{
			this.GetBtn.spriteName = "hui";
			this.GetBtnLabel.text = "已领取";
			this.GetBtnTimeLabel.gameObject.SetActive(false);
			this.GetBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.none;
		}
		else if (OnLineAward.laod.step > 0 && OnLineAward.laod.step == UnitConst.GetInstance().loadReward[i].id && UnitConst.GetInstance().loadReward[i].isCanGetOnLine)
		{
			this.GetBtn.spriteName = "yellow";
			this.GetBtnLabel.text = "可领取";
			this.GetBtnTimeLabel.gameObject.SetActive(false);
			this.GetBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.ActivityPanelManager_GetOnlineAward;
		}
		else if (OnLineAward.laod.step > 0 && OnLineAward.laod.step == UnitConst.GetInstance().loadReward[i].id && !UnitConst.GetInstance().loadReward[i].isCanGetOnLine)
		{
			this.GetBtn.spriteName = "hui";
			this.GetBtnLabel.text = "进行中";
			this.GetBtnTimeLabel.gameObject.SetActive(true);
			this.GetBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.none;
		}
		else
		{
			this.GetBtn.spriteName = "hui";
			this.GetBtnLabel.text = "未进行";
			this.GetBtnTimeLabel.gameObject.SetActive(false);
			this.GetBtn.GetComponent<ButtonClick>().eventType = EventManager.EventType.none;
		}
	}

	private void Update()
	{
	}

	public void ShowOnLine()
	{
		this.leftTabel.ClearChild();
		bool flag = true;
		foreach (KeyValuePair<int, LoadReward> current in UnitConst.GetInstance().loadReward)
		{
			GameObject gameObject = NGUITools.AddChild(this.leftTabel.gameObject, this.leftPrefab);
			gameObject.name = current.Key.ToString();
			ChargeLeftPrefab component = gameObject.GetComponent<ChargeLeftPrefab>();
			component.activtiyName.text = LanguageManage.GetTextByKey(current.Value.des, "Activities");
			if (flag)
			{
				flag = false;
				this.ChangeOnLineGift(current.Key);
			}
		}
		this.leftTabel.Reposition();
	}
}
