using System;
using UnityEngine;

public class GuideDialogSkip : MonoBehaviour
{
	public TypewriterEffect textWrite;

	private bool isClick;

	private void Start()
	{
	}

	private void OnDisable()
	{
		this.isClick = false;
	}

	private void OnClick()
	{
		if (this.textWrite.isEnd && !this.isClick)
		{
			this.isClick = true;
			if (NewbieGuidePanel._instance && NewbieGuidePanel._instance.gameObject.activeSelf)
			{
				NewbieGuidePanel._instance.HideSelf();
			}
			if (HUDTextTool.inst)
			{
				HUDTextTool.inst.NextLuaCall("引导 对话后，点击调用Lua", new object[]
				{
					base.gameObject
				});
			}
		}
		else
		{
			this.textWrite.ToEnd();
		}
	}
}
