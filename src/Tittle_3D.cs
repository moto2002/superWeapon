using System;
using UnityEngine;

public class Tittle_3D : IMonoBehaviour
{
	public Transform tar;

	public float yOffect;

	public void OnDestroy()
	{
		if (ResourcePanelManage.inst)
		{
			ResourcePanelManage.inst._3DUIPool.Remove(this);
		}
	}

	public virtual void YoofectUp(float i)
	{
		this.yOffect = 2f + i * 3f;
	}
}
