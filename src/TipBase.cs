using System;
using UnityEngine;

public class TipBase : MonoBehaviour
{
	public GameObject Offset;

	protected ReportData thisData;

	private UIFloat3DObj _uiFloat3DObj;

	private UIFloat3DObj uiFloat3DObj
	{
		get
		{
			if (this._uiFloat3DObj == null && this.Offset)
			{
				this._uiFloat3DObj = this.Offset.GetComponent<UIFloat3DObj>();
			}
			return this._uiFloat3DObj;
		}
	}

	public virtual void Open(ReportData thisData)
	{
		this.thisData = thisData;
		this.CameraFollow();
	}

	public virtual void RefreshUI()
	{
	}

	public virtual void Close()
	{
	}

	public virtual void CameraFollow()
	{
		T_Island t_IslandByIndex = T_WMap.inst.GetT_IslandByIndex((this.thisData.islandIndex != 0) ? this.thisData.islandIndex : 286);
		if (t_IslandByIndex != null)
		{
			Transform transform = t_IslandByIndex.transform;
			CameraSmoothMove.inst.MovePosition(new Vector3(transform.position.x, 0f, transform.position.z), delegate
			{
				if (this.Offset)
				{
					this.Offset.SetActive(true);
				}
			});
			if (this.uiFloat3DObj)
			{
				this.uiFloat3DObj.target = transform;
			}
		}
	}
}
