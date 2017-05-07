using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManage : MonoBehaviour
{
	public static TimeManage inst;

	private List<TimeEndCallBackStruct> alleventList_ArmyUpdating = new List<TimeEndCallBackStruct>();

	private List<TimeEndCallBackStruct> alleventList_SpecialSoliderList = new List<TimeEndCallBackStruct>();

	public void OnDestroy()
	{
		TimeManage.inst = null;
	}

	private void Awake()
	{
		if (TimeManage.inst)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		TimeManage.inst = this;
		GameTools.DontDestroyOnLoad(base.gameObject);
		base.InvokeRepeating("TimeDo", 1f, 1f);
	}

	private void TimeDo()
	{
		for (int i = this.alleventList_ArmyUpdating.Count - 1; i >= 0; i--)
		{
			if (TimeTools.GetNowTimeSyncServerToDateTime() >= this.alleventList_ArmyUpdating[i].TimeEnd)
			{
				TimeEndCallBackStruct timeEndCallBackStruct = this.alleventList_ArmyUpdating[i];
				this.alleventList_ArmyUpdating.Remove(timeEndCallBackStruct);
				if (timeEndCallBackStruct.CallBack != null)
				{
					timeEndCallBackStruct.CallBack(timeEndCallBackStruct.ItemID);
				}
			}
		}
		for (int j = this.alleventList_SpecialSoliderList.Count - 1; j >= 0; j--)
		{
			if (TimeTools.GetNowTimeSyncServerToDateTime() >= this.alleventList_SpecialSoliderList[j].TimeEnd)
			{
				TimeEndCallBackStruct timeEndCallBackStruct2 = this.alleventList_SpecialSoliderList[j];
				this.alleventList_SpecialSoliderList.Remove(timeEndCallBackStruct2);
				if (timeEndCallBackStruct2.CallBack != null)
				{
					timeEndCallBackStruct2.CallBack(timeEndCallBackStruct2.ItemID);
				}
			}
		}
	}

	public void AddArmyUpdateTimeEvent(int itemID, DateTime _timeEnd, Action<int> _CallBack)
	{
		TimeEndCallBackStruct item = new TimeEndCallBackStruct(itemID, _timeEnd, _CallBack);
		this.alleventList_ArmyUpdating.Add(item);
	}

	public void AddSpecialSoliderTimeRelieve(int id, DateTime relieveTime, Action<int> _SpecialCallBack)
	{
		TimeEndCallBackStruct item = new TimeEndCallBackStruct(id, relieveTime, _SpecialCallBack);
		this.alleventList_SpecialSoliderList.Add(item);
	}

	public void ClearArmyUpdateTimeEvent()
	{
		this.alleventList_ArmyUpdating.Clear();
	}

	public void ClearSepcialRelieveTimeEvent()
	{
		this.alleventList_SpecialSoliderList.Clear();
	}
}
