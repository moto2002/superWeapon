using System;
using UnityEngine;

public class CollectRes : MonoBehaviour
{
	public IslandResCollect island;

	public Transform tar;

	private void Awake()
	{
		this.tar = base.transform;
		this.island = base.GetComponentInParent<IslandResCollect>();
	}

	public void OnMouseUp()
	{
		AudioManage.inst.PlayAuido("xuanqujianzhu", false);
		this.island.CollectIslandRes(delegate
		{
		});
	}
}
