using System;
using System.Collections.Generic;
using UnityEngine;

public class PointingBox : IMonoBehaviour
{
	public static PointingBox ins;

	private Ray ray;

	private RaycastHit hit;

	private Character SelTar;

	public void OnDestroy()
	{
		PointingBox.ins = null;
	}

	public override void Awake()
	{
		PointingBox.ins = this;
		base.Awake();
	}

	private void OnMouseDown()
	{
		this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(this.ray, out this.hit) && this.hit.collider.name.Equals("pointPlane"))
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (Vector3.Distance(this.hit.point, SenceManager.inst.towers[i].tr.position) <= (float)SenceManager.inst.towers[i].size * 0.5f)
				{
					this.SelTar = SenceManager.inst.towers[i];
					break;
				}
			}
			if (this.SelTar == null)
			{
				foreach (KeyValuePair<int, T_Res> current in SenceManager.inst.reses)
				{
					if (Vector3.Distance(this.hit.point, current.Value.tr.position) <= (float)current.Value.size * 0.5f)
					{
						this.SelTar = current.Value;
						break;
					}
				}
			}
		}
		if (this.SelTar)
		{
			this.SelTar.MouseDown();
			return;
		}
		if (DragMgr.inst.BtnInUse)
		{
			return;
		}
		SenceManager.inst.sameTower = false;
		DragMgr.inst.NewMouseDown();
	}

	public virtual void OnMouseUp()
	{
		if (this.SelTar)
		{
			this.SelTar.MouseUp();
			this.SelTar = null;
			return;
		}
		if (DragMgr.inst.BtnInUse)
		{
			return;
		}
		if (FightPanelManager.inst == null)
		{
			DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector3.zero, null);
			return;
		}
		if (FightPanelManager.supperSkillReady || FightPanelManager.inst.isSpColor)
		{
			DragMgr.inst.MouseUp(MouseCommonType.supperSkill, this.tr.position, null);
		}
		else
		{
			DragMgr.inst.MouseUp(MouseCommonType.forceMove, this.tr.position, null);
		}
	}

	private void OnMouseDrag()
	{
		if (this.SelTar)
		{
			this.SelTar.MouseDrag();
			return;
		}
		if (DragMgr.inst.BtnInUse)
		{
			return;
		}
		if (Input.touchCount > 1)
		{
			DragMgr.inst.ZoomDoing();
			return;
		}
		DragMgr.inst.towTouch = false;
		DragMgr.inst.dragX = -Input.GetAxis("Mouse X");
		DragMgr.inst.dragY = -Input.GetAxis("Mouse Y");
		DragMgr.inst.ConversionFormulas(DragMgr.inst.speed);
	}
}
