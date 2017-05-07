using System;
using UnityEngine;

public class SkillTipshow : MonoBehaviour
{
	public int Index;

	public int JianTouPostion;

	public int type;

	private void Start()
	{
	}

	public void OnPress(bool down)
	{
		if (down)
		{
			HUDTextTool.inst.TextTips.OnDown(base.gameObject, UnitConst.GetInstance().skillList[this.Index].Description);
		}
		else
		{
			HUDTextTool.inst.TextTips.OnUp();
		}
	}

	public void LateUpdate()
	{
		if (Input.touchCount > 1)
		{
			HUDTextTool.inst.TextTips.OnUp();
		}
	}
}
