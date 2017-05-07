using System;
using UnityEngine;

public class Cheat : MonoBehaviour
{
	private bool BuildHit;

	private bool BuildBoom;

	private float time1;

	private void Start()
	{
	}

	private void Update()
	{
		if (UIManager.curState == SenceState.Attacking)
		{
			if (this.BuildHit && SenceManager.inst && SenceManager.inst.CurSelectTower)
			{
				this.time1 += Time.deltaTime;
				if (this.time1 > 0.1f)
				{
					this.time1 = 0f;
					SenceManager.inst.CurSelectTower.DoHurt((int)((float)SenceManager.inst.CurSelectTower.MaxLife * 0.1f), -10L, true);
				}
			}
			if (this.BuildBoom && SenceManager.inst && SenceManager.inst.CurSelectTower)
			{
				SenceManager.inst.CurSelectTower.DoHurt(SenceManager.inst.CurSelectTower.MaxLife + 50, -10L, true);
			}
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(600f, 0f, 180f, 60f), "所选建筑持续掉血：" + this.BuildHit))
		{
			this.BuildHit = !this.BuildHit;
		}
		if (GUI.Button(new Rect(400f, 0f, 180f, 60f), "所选建筑爆炸：" + this.BuildBoom))
		{
			this.BuildBoom = !this.BuildBoom;
		}
		if (GUI.Button(new Rect(800f, 0f, 180f, 60f), "场上坦克血量无限大："))
		{
			for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
			{
				SenceManager.inst.Tanks_Attack[i].MaxLifeChange(9999999);
			}
		}
		if (GUI.Button(new Rect(1000f, 0f, 180f, 60f), "场上坦克血量归1："))
		{
			for (int j = 0; j < SenceManager.inst.Tanks_Attack.Count; j++)
			{
				SenceManager.inst.Tanks_Attack[j].MaxLifeChange(1);
			}
		}
	}
}
