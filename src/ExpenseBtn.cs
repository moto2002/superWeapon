using System;
using UnityEngine;

public class ExpenseBtn : MonoBehaviour
{
	public ExpenseBtnType type;

	public UISprite uis;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnPress(bool isPress)
	{
		if (!isPress)
		{
			ExpensePanelManage.inst.EventBtn(this.type);
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
