using System;
using System.Collections.Generic;
using UnityEngine;

public class ActivityDailyTip : MonoBehaviour
{
	[Serializable]
	public class BuyPart
	{
		public GameObject obj;

		public UILabel costLbl;

		public UILabel vipInfo;
	}

	public UISprite[] diffSprites;

	public UISprite[] itemIcons;

	public UILabel[] itemNums;

	public UIButton spyBtn;

	public UIButton attBtn;

	public UIButton buyBtn;

	public UILabel attCost;

	public UILabel leftTimes;

	public ActivityDailyTip.BuyPart buyPart;

	private Vector3 bigger = new Vector3(1.2f, 1.2f, 1.2f);

	private Vector3 normal = Vector3.one;

	public static ActivityDailyTip ins;

	public void OnDestroy()
	{
		ActivityDailyTip.ins = null;
	}

	private void Awake()
	{
		ActivityDailyTip.ins = this;
	}

	private void OnEnable()
	{
		if (TipsManager.inst == null)
		{
			return;
		}
		T_Island curIsland = TipsManager.inst.curIsland;
		ActivityManager.GetIns().CurActIsland = null;
		if (curIsland != null && curIsland.uiType == WMapTipsType.dailyAct)
		{
			ActivityManager.GetIns().CurActIsland = curIsland;
			if (ActivityManager.GetIns().dailyNpcList.Count <= 0)
			{
				int key = int.Parse(curIsland.ownerId);
				Npc npc = UnitConst.GetInstance().AllNpc[key];
				Npc npc2 = UnitConst.GetInstance().AllNpc[npc.nextId];
				Npc item = UnitConst.GetInstance().AllNpc[npc2.nextId];
				ActivityManager.GetIns().dailyNpcList.Add(npc);
				ActivityManager.GetIns().dailyNpcList.Add(npc2);
				ActivityManager.GetIns().dailyNpcList.Add(item);
			}
			ActivityManager.GetIns().curActItem = ActivityManager.GetActByNpcId(int.Parse(curIsland.ownerId));
			ActivityManager.GetIns().selectedLevel = 1;
			this.RefreshUI();
			this.RefreshOpen();
			this.buyPart.obj.SetActive(false);
		}
	}

	public void RefreshUI()
	{
		if (ActivityManager.GetIns().CurActIsland != null)
		{
			ActivityItem curActItem = ActivityManager.GetIns().curActItem;
			if (ActivityManager.GetIns().selectedLevel == 1)
			{
				Npc curNpc = ActivityManager.GetIns().dailyNpcList[0];
				ActivityManager.GetIns().curNpc = curNpc;
				this.RefreshInfo(curNpc, curActItem);
			}
			else if (ActivityManager.GetIns().selectedLevel == 2)
			{
				Npc curNpc2 = ActivityManager.GetIns().dailyNpcList[1];
				ActivityManager.GetIns().curNpc = curNpc2;
				this.RefreshInfo(curNpc2, curActItem);
			}
			else if (ActivityManager.GetIns().selectedLevel == 3)
			{
				Npc curNpc3 = ActivityManager.GetIns().dailyNpcList[2];
				ActivityManager.GetIns().curNpc = curNpc3;
				this.RefreshInfo(curNpc3, curActItem);
			}
			for (int i = 0; i < this.diffSprites.Length; i++)
			{
				if (i == ActivityManager.GetIns().selectedLevel - 1)
				{
					this.diffSprites[i].transform.localScale = this.bigger;
				}
				else
				{
					this.diffSprites[i].transform.localScale = this.normal;
				}
				NGUITools.UpdateWidgetCollider(this.diffSprites[i].gameObject);
			}
		}
	}

	private void RefreshOpen()
	{
		if (ActivityManager.GetIns().CurActIsland != null)
		{
			Npc npc = ActivityManager.GetIns().dailyNpcList[0];
			Npc npc2 = ActivityManager.GetIns().dailyNpcList[1];
			Npc npc3 = ActivityManager.GetIns().dailyNpcList[2];
			if (npc != null && ActivityManager.GetIns().curActData.dailyNpcIds.Contains(npc.id))
			{
				this.ChangeButton(this.diffSprites[0], true);
			}
			else
			{
				this.ChangeButton(this.diffSprites[0], false);
			}
			if (npc2 != null && ActivityManager.GetIns().curActData.dailyNpcIds.Contains(npc2.id))
			{
				this.ChangeButton(this.diffSprites[1], true);
			}
			else
			{
				this.ChangeButton(this.diffSprites[1], false);
			}
			if (npc3 != null && ActivityManager.GetIns().curActData.dailyNpcIds.Contains(npc3.id))
			{
				this.ChangeButton(this.diffSprites[2], true);
			}
			else
			{
				this.ChangeButton(this.diffSprites[2], false);
			}
		}
	}

	private void ChangeButton(UISprite sp, bool isOpen)
	{
		if (sp == null)
		{
			return;
		}
		sp.GetComponent<BoxCollider>().enabled = isOpen;
		if (isOpen)
		{
			sp.ShaderToNormal();
		}
		else
		{
			sp.ShaderToGray();
		}
	}

	private void RefreshInfo(Npc curNpc, ActivityItem curItem)
	{
		if (ActivityManager.GetIns().curActData.attackDailyActivityTimes < curItem.freeTimes + ActivityManager.GetIns().curActData.refreshDailyActivityTimes)
		{
			this.attCost.text = curNpc.cost.ToString();
			this.attBtn.gameObject.SetActive(false);
			this.buyBtn.gameObject.SetActive(false);
		}
		else if (ActivityManager.GetIns().curActData.refreshDailyActivityTimes < curItem.times)
		{
			this.leftTimes.text = ActivityManager.GetIns().curActData.refreshDailyActivityTimes + "/" + curItem.times;
			this.attBtn.gameObject.SetActive(false);
			this.buyBtn.gameObject.SetActive(true);
			this.buyBtn.isEnabled = true;
		}
		else
		{
			this.leftTimes.text = ActivityManager.GetIns().curActData.attackDailyActivityTimes - curItem.freeTimes + "/" + curItem.times;
			this.attBtn.gameObject.SetActive(false);
			this.buyBtn.gameObject.SetActive(true);
			this.buyBtn.isEnabled = false;
		}
		DropList dropList = UnitConst.GetInstance().AllDropList[curNpc.dropListId];
		List<ResType> list = new List<ResType>();
		foreach (ResType current in dropList.res.Keys)
		{
			list.Add(current);
		}
		for (int i = 0; i < this.itemIcons.Length; i++)
		{
			if (i < list.Count)
			{
				this.itemIcons[i].gameObject.SetActive(true);
				this.itemNums[i].gameObject.SetActive(true);
				this.itemNums[i].text = dropList.res[list[i]].ToString();
			}
			else
			{
				this.itemIcons[i].gameObject.SetActive(false);
				this.itemNums[i].gameObject.SetActive(false);
			}
		}
	}

	public void ButtonClick(ActivityBtnType type, GameObject go)
	{
		if (type == ActivityBtnType.commonLv)
		{
			ActivityManager.GetIns().selectedLevel = 1;
			this.RefreshUI();
		}
		else if (type == ActivityBtnType.hardLv)
		{
			ActivityManager.GetIns().selectedLevel = 2;
			this.RefreshUI();
		}
		else if (type == ActivityBtnType.godLv)
		{
			ActivityManager.GetIns().selectedLevel = 3;
			this.RefreshUI();
		}
		else if (type == ActivityBtnType.spy)
		{
			ActivityManager.GetIns().IsFromAct = true;
			if (ActivityManager.GetIns().curActItem.soliders.Count > 0)
			{
				ActivityManager.GetIns().ToActivitySlotInfo();
			}
			ActivityManager.GetIns().IsDailyAct = true;
			TipsManager.inst.BtnEvent(TipsBtnType.spy, ActivityManager.GetIns().curNpc.id);
		}
		else if (type == ActivityBtnType.attrack)
		{
			ActivityManager.GetIns().IsFromAct = true;
			if (ActivityManager.GetIns().curActItem.soliders.Count > 0)
			{
				ActivityManager.GetIns().ToActivitySlotInfo();
			}
			ActivityManager.GetIns().IsDailyAct = true;
			TipsManager.inst.BtnEvent(TipsBtnType.warring, ActivityManager.GetIns().curNpc.id);
		}
		else if (type == ActivityBtnType.buytimes)
		{
			if (ActivityManager.GetIns().curActData.refreshDailyActivityTimes < ActivityManager.GetIns().curActItem.times)
			{
				float f = (float)ActivityManager.GetIns().curActItem.needMoney * Mathf.Pow((float)ActivityManager.GetIns().curActItem.nextNeedMoneyMultiplier / 10f, (float)(ActivityManager.GetIns().curActData.refreshDailyActivityTimes + 1));
				int num = Mathf.FloorToInt(f);
				this.buyPart.costLbl.text = num.ToString();
				this.buyPart.obj.SetActive(true);
			}
		}
		else if (type == ActivityBtnType.closeBuy)
		{
			this.buyPart.obj.SetActive(false);
		}
		else if (type == ActivityBtnType.buy)
		{
			ActivityHandler.CSRefreshActivity(1);
			this.buyPart.obj.SetActive(false);
		}
	}
}
