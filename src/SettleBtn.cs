using System;
using UnityEngine;

public class SettleBtn : MonoBehaviour
{
	public SettleBtnType btnType;

	private float last = -1000f;

	private void Start()
	{
	}

	private void OnPress(bool isPress)
	{
		if (!isPress)
		{
			if (Time.time > this.last + 5f)
			{
				SettlementManager.inst.ButtnEvent(this.btnType);
				this.last = Time.time;
			}
		}
		SenceType senceType = Loading.senceType;
		if (senceType != SenceType.Island)
		{
			if (senceType == SenceType.WorldMap)
			{
				if (WMap_DragManager.inst)
				{
					WMap_DragManager.inst.btnInUse = isPress;
				}
				if (DragMgr.inst)
				{
					DragMgr.inst.BtnInUse = false;
					DragMgr.inst.isInScrollViewDrag = false;
				}
			}
		}
		else
		{
			if (DragMgr.inst)
			{
				DragMgr.inst.BtnInUse = isPress;
				DragMgr.inst.isInScrollViewDrag = isPress;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}
}
