using System;
using UnityEngine;

public class T_MineTower : T_Tower
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Tank"))
		{
			Character component = other.GetComponent<Character>();
			if (base.IsCanShootByCharType(component))
			{
				for (int i = 0; i < this.Targetes.Count; i++)
				{
					if (this.Targetes[i].charaType != this.charaType && this.Targetes[i].Tank)
					{
						this.Targetes[i].Tank.DoHurt(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.breakArmor);
					}
				}
				this.DoHurt((int)base.CurLife + 50, -10L, true);
			}
		}
	}

	public void SkillHurt()
	{
		foreach (Character current in this.Targetes)
		{
			for (int i = 0; i < this.Targetes.Count; i++)
			{
				if (this.Targetes[i].charaType != this.charaType && this.Targetes[i].Tank)
				{
					this.Targetes[i].Tank.DoHurt(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.breakArmor);
				}
			}
		}
		this.DoHurt((int)base.CurLife + 50, -10L, true);
	}

	public override void DoHurt(int damage, long containerID, bool isExtraAttack = true)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		base.CreateTowerDeadEffect();
		UnityEngine.Object.Destroy(this.tr.gameObject);
	}
}
