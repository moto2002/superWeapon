using System;
using UnityEngine;

public class EmailBtn : MonoBehaviour
{
	public EmailBtnType type;

	private void OnClick()
	{
		if (!DragMgr.inst.isInScrollViewDrag)
		{
			EmailPanel.ins.ButtonClick(this.type, base.gameObject);
		}
	}
}
