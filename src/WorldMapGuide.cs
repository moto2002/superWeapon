using System;
using UnityEngine;

public class WorldMapGuide : IMonoBehaviour
{
	private Transform mParent;

	private bool isDrag;

	private void Start()
	{
		this.mParent = this.tr.parent;
	}

	public void LateUpdate()
	{
		if (WMap_DragManager.inst && !this.isDrag)
		{
			this.mParent.localPosition = Vector3.zero;
			this.tr.localPosition = new Vector3((WMap_DragManager.inst.tr.localPosition.x + 12f) * 280f / 24f - 140f, (WMap_DragManager.inst.tr.localPosition.z + 12f) * 280f / 24f - 140f);
		}
	}

	private void OnDrag(Vector2 vc2)
	{
		if (T_WMap.newWapIndex.Count != 0)
		{
			return;
		}
		this.mParent.localPosition += new Vector3(vc2.x, vc2.y, 0f);
		Vector3 vector = WMap_DragManager.inst.tr.localPosition;
		vector += new Vector3(vc2.y, 0f, -vc2.x) * 8f / 90f;
		if (vector.x > 8f)
		{
			vector.x = 8f;
		}
		else if (vector.x < -8f)
		{
			vector.x = -8f;
		}
		if (vector.z > 8f)
		{
			vector.z = 8f;
		}
		else if (vector.z < -8f)
		{
			vector.z = -8f;
		}
		WMap_DragManager.inst.tr.localPosition = vector;
	}

	private void OnDragStart()
	{
		if (T_WMap.newWapIndex.Count != 0)
		{
			return;
		}
		this.isDrag = true;
	}

	private void OnDragEnd()
	{
		if (T_WMap.newWapIndex.Count != 0)
		{
			return;
		}
		this.isDrag = false;
	}
}
