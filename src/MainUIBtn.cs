using System;
using UnityEngine;

public class MainUIBtn : MonoBehaviour
{
	public static MainUIBtn inst;

	private float last = -1000f;

	private void Awake()
	{
		MainUIBtn.inst = this;
	}

	private void Start()
	{
	}

	private void OnPress(bool isPress)
	{
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

	public void OnClick()
	{
		if (Time.time > this.last + 0.3f)
		{
			this.last = Time.time;
		}
	}
}
