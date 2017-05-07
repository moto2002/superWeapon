using System;
using UnityEngine;

public class T_TowerR : MonoBehaviour
{
	public Transform maxR;

	public Transform minR;

	private Transform tr;

	private void Awake()
	{
		this.tr = base.transform;
	}

	public void ShowTowerR(T_Tower tower)
	{
		this.maxR.localScale = Vector3.one * UnitConst.GetInstance().buildingConst[tower.index].maxRadius * 2f;
		if (UnitConst.GetInstance().buildingConst[tower.index].minRadius == 0f)
		{
			this.minR.gameObject.SetActive(false);
		}
		else
		{
			this.minR.gameObject.SetActive(true);
			this.minR.localScale = Vector3.one * UnitConst.GetInstance().buildingConst[tower.index].minRadius * 2f;
		}
		this.tr.position = tower.tr.position;
	}

	public void Hiden()
	{
		this.tr.position = SenceManager.inst.hidePos;
	}
}
