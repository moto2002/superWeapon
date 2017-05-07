using System;
using UnityEngine;

public class ResouceTittleBtn : IMonoBehaviour
{
	private Camera cam;

	public Transform tar;

	public T_Tower bulidTar;

	public IslandResCollect islandTar;

	public UILabel maxLabel;

	public bool isVIP;

	private void Start()
	{
		if (this.maxLabel != null)
		{
			this.maxLabel.gameObject.SetActive(false);
		}
		this.tr = base.transform;
		this.cam = UIManager.inst.uiCamera;
	}

	private void Update()
	{
		if (this.tar == null || !this.tar.gameObject.activeSelf)
		{
			this.SetUISpriteColor(Color.white);
			this.bulidTar = null;
			this.islandTar = null;
			NGUITools.Destroy(this.ga);
		}
		else
		{
			Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + 3.5f, this.tar.position.z);
			if (Camera.main == null)
			{
				return;
			}
			this.tr.position = position;
		}
	}

	public void SetUISpriteColor(Color color)
	{
		if (this.maxLabel != null)
		{
			if (color == Color.red)
			{
				this.maxLabel.gameObject.SetActive(true);
			}
			else
			{
				this.maxLabel.gameObject.SetActive(false);
			}
		}
	}

	private void OnPress(bool isPress)
	{
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
					DragMgr.inst.isInScrollViewDrag = false;
				}
			}
		}
		else
		{
			if (DragMgr.inst)
			{
				DragMgr.inst.BtnInUse = isPress;
				DragMgr.inst.isInScrollViewDrag = isPress;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}

	private void OnClick()
	{
		FuncUIManager.inst.ResourcePanelManage.CollectResource(this.bulidTar, delegate
		{
			this.tar = null;
			this.bulidTar = null;
		});
	}
}
