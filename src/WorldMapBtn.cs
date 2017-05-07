using System;
using UnityEngine;

public class WorldMapBtn : MonoBehaviour
{
	public WorldMapBtnType type;

	private float last = -1000f;

	private void Start()
	{
	}

	private void OnPress(bool isPress)
	{
		if (!isPress && Time.time > this.last + 0.3f)
		{
			if (WorldMapManager.inst)
			{
				WorldMapManager.inst.BtnEvent(this.type);
			}
			this.last = Time.time;
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
				}
			}
		}
		else
		{
			if (DragMgr.inst)
			{
				DragMgr.inst.BtnInUse = isPress;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}
}
