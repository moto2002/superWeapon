using DG.Tweening;
using System;
using UnityEngine;

public class T_ParkingSpace : MonoBehaviour
{
	public int id;

	public long towerID;

	public bool isFist = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Tank"))
		{
			T_Tank component = other.GetComponent<T_Tank>();
			if (component.parkingId == this.id && component.towerID == this.towerID && this.isFist)
			{
				T_Tower tower = null;
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if (SenceManager.inst.towers[i].id == this.towerID)
					{
						tower = SenceManager.inst.towers[i];
					}
				}
				other.transform.parent = base.transform;
				other.transform.DOLocalMove(Vector3.zero, 3f, false).OnComplete(delegate
				{
					tower.tanksNum++;
					if (tower.tanksNum >= tower.tankMaxNum)
					{
						tower.biankuang.DOLocalMoveY(0.12f, 1f, false);
					}
				});
				this.isFist = false;
			}
		}
	}
}
