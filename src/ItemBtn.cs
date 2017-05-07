using System;
using UnityEngine;

public class ItemBtn : MonoBehaviour
{
	public ItemBtnType type;

	private void OnClick()
	{
		if (!DragMgr.inst.isInScrollViewDrag && ItemPanelManage.ins)
		{
			ItemPanelManage.ins.ButtonClick(this.type, base.gameObject);
		}
	}
}
