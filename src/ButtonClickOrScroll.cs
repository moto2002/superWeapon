using System;

public class ButtonClickOrScroll : ButtonClick
{
	public override void OnClick()
	{
		if (!DragMgr.inst.isInScrollViewDrag)
		{
			EventManager.Instance.DispatchEvent(this.eventType, base.gameObject);
		}
	}
}
