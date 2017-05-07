using System;
using UnityEngine;

public class ItemTipsShow2 : MonoBehaviour
{
	public int Index;

	public int Type;

	public int JianTouPostion;

	private void Start()
	{
	}

	public void OnPress(bool down)
	{
		if (down)
		{
			Camera camera = NGUITools.FindCameraForLayer(base.gameObject.layer);
			Vector3 vector = camera.WorldToScreenPoint(base.transform.position);
			LogManage.LogError(string.Format("当前点击的物体屏幕位置:{0} Screen的宽是{1}  高{2}", vector, Screen.width, Screen.height));
			if (vector.x > (float)Screen.width * 0.25f && vector.x < (float)Screen.width * 0.75f)
			{
				if (vector.y > (float)Screen.height * 0.5f)
				{
					this.JianTouPostion = 1;
				}
				else
				{
					this.JianTouPostion = 2;
				}
			}
			else if (vector.x > (float)Screen.width * 0.5f)
			{
				this.JianTouPostion = 4;
			}
			else
			{
				this.JianTouPostion = 3;
			}
			HUDTextTool.inst.itemTips.OnDown(base.gameObject, this.Index, this.Type, this.JianTouPostion);
		}
		else
		{
			HUDTextTool.inst.itemTips.OnUp();
		}
	}
}
