using System;
using System.Collections.Generic;
using UnityEngine;

public class ActivityWeekTip : MonoBehaviour
{
	[Serializable]
	public class ResetPart
	{
		public GameObject obj;

		public UILabel title;

		public UISprite icon;

		public UILabel num;
	}

	[Serializable]
	public class PrivilagePart
	{
		public GameObject obj;

		public UILabel title;
	}

	public UISprite[] icons;

	public UILabel[] nums;

	public UISprite costIcon;

	public UILabel costNum;

	public UILabel leftResetTimes;

	public UIButton attObj;

	public ActivityWeekTip.ResetPart resetPart;

	public ActivityWeekTip.PrivilagePart privilagePart;

	public static ActivityWeekTip ins;

	public void OnDestroy()
	{
		ActivityWeekTip.ins = null;
	}

	private void Awake()
	{
		ActivityWeekTip.ins = this;
	}

	private void OnEnable()
	{
		if (TipsManager.inst == null)
		{
			return;
		}
		T_Island curIsland = TipsManager.inst.curIsland;
		ActivityManager.GetIns().CurActIsland = null;
		if (curIsland != null && curIsland.uiType == WMapTipsType.weekAct)
		{
			ActivityManager.GetIns().CurActIsland = curIsland;
			ActivityManager.GetIns().curActItem = ActivityManager.GetActByNpcId(int.Parse(curIsland.ownerId));
			if (!UnitConst.GetInstance().AllNpc.ContainsKey(ActivityManager.GetIns().curActData.weekNpcId))
			{
				base.gameObject.SetActive(false);
				return;
			}
			ActivityManager.GetIns().curNpc = UnitConst.GetInstance().AllNpc[ActivityManager.GetIns().curActData.weekNpcId];
			this.resetPart.obj.SetActive(false);
			this.privilagePart.obj.SetActive(false);
			this.RefreshUI();
		}
	}

	public void RefreshUI()
	{
		ActivityManager.GetIns().curNpc = UnitConst.GetInstance().AllNpc[ActivityManager.GetIns().curActData.weekNpcId];
		Npc curNpc = ActivityManager.GetIns().curNpc;
		DropList dropList = UnitConst.GetInstance().AllDropList[curNpc.dropListId];
		List<ResType> list = new List<ResType>();
		foreach (ResType current in dropList.res.Keys)
		{
			list.Add(current);
		}
		for (int i = 0; i < this.icons.Length; i++)
		{
			if (i < list.Count)
			{
				this.icons[i].gameObject.SetActive(true);
				this.nums[i].gameObject.SetActive(true);
				this.nums[i].text = dropList.res[list[i]].ToString();
			}
			else
			{
				this.icons[i].gameObject.SetActive(false);
				this.nums[i].gameObject.SetActive(false);
			}
		}
		this.attObj.isEnabled = (ActivityManager.GetIns().GetWeekStage() != -1);
		this.costNum.text = curNpc.cost.ToString();
		this.leftResetTimes.text = string.Format("剩余次数：{0}", (long)(ActivityManager.GetIns().curActItem.times + ActivityManager.GetIns().curActItem.freeTimes) - ActivityManager.GetIns().curActData.resetTimes);
	}

	public void ButtonClick(WeekBtnType type, GameObject go)
	{
		if (type == WeekBtnType.spy)
		{
			ActivityManager.GetIns().IsFromAct = true;
			ActivityManager.GetIns().IsWeekAct = true;
			if (ActivityManager.GetIns().curActItem.soliders.Count > 0)
			{
				ActivityManager.GetIns().ToActivitySlotInfo();
			}
			TipsManager.inst.BtnEvent(TipsBtnType.spy, 0);
		}
		else if (type == WeekBtnType.att)
		{
			ActivityManager.GetIns().IsFromAct = true;
			ActivityManager.GetIns().IsWeekAct = true;
			if (ActivityManager.GetIns().curActItem.soliders.Count > 0)
			{
				ActivityManager.GetIns().ToActivitySlotInfo();
			}
			TipsManager.inst.BtnEvent(TipsBtnType.warring, 0);
		}
		else if (type == WeekBtnType.toReset)
		{
			if (ActivityManager.GetIns().curActData.resetTimes >= (long)(ActivityManager.GetIns().curActItem.freeTimes + ActivityManager.GetIns().curActItem.times))
			{
				this.privilagePart.obj.SetActive(true);
			}
			else
			{
				this.resetPart.obj.SetActive(true);
				this.resetPart.title.text = string.Format("重置{0}关卡进度", ActivityManager.GetIns().curNpc.name);
				if (ActivityManager.GetIns().curActData.resetTimes < (long)ActivityManager.GetIns().curActItem.freeTimes)
				{
					this.resetPart.num.text = "0";
				}
				else
				{
					float f = (float)ActivityManager.GetIns().curActItem.needMoney * Mathf.Pow((float)ActivityManager.GetIns().curActItem.nextNeedMoneyMultiplier / 10f, (float)(ActivityManager.GetIns().curActData.resetTimes - (long)ActivityManager.GetIns().curActItem.freeTimes));
					this.resetPart.num.text = Mathf.FloorToInt(f).ToString();
				}
			}
		}
		else if (type == WeekBtnType.reset)
		{
			ActivityHandler.CSRefreshActivity(2);
			this.resetPart.obj.SetActive(false);
		}
		else if (type == WeekBtnType.cancle)
		{
			this.privilagePart.obj.SetActive(false);
		}
		else if (type == WeekBtnType.openPrivilage)
		{
			MessageBox.GetMessagePanel().ShowBtn("特权", "特权", "查看特权", null);
		}
		else if (type == WeekBtnType.closeReset)
		{
			this.resetPart.obj.SetActive(false);
		}
		else if (type == WeekBtnType.closePrivilage)
		{
			this.privilagePart.obj.SetActive(false);
		}
	}
}
