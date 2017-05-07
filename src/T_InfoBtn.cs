using System;
using UnityEngine;

public class T_InfoBtn : MonoBehaviour
{
	public T_InfoBtnType type;

	public UISprite uis;

	private void OnPress(bool isPress)
	{
		if (!isPress)
		{
			FuncUIManager.inst.T_InfoPanelManage.EventBtn(this.type);
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
