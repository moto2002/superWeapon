using System;
using UnityEngine;

public class EventTest : MonoBehaviour
{
	private float last = -1000f;

	private void Start()
	{
	}

	private void OnMouseDown()
	{
		if (EventNoteMgr.EvenData == null)
		{
			return;
		}
		if (Time.time > this.last + 2.5f)
		{
			if (WMap_DragManager.inst.btnInUse)
			{
				return;
			}
			SenceHandler.GC_WatchNote2();
			this.last = Time.time;
		}
	}
}
