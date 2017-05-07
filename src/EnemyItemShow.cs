using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemShow : MonoBehaviour
{
	public static EnemyItemShow inst;

	public UILabel nameUILabel;

	public UILabel lv;

	[HideInInspector]
	public EnemyNpcAttack npc;

	public TimePanel timePanel;

	public GameObject timePanelShow;

	public void OnDestroy()
	{
		EnemyItemShow.inst = null;
	}

	private void Awake()
	{
		EnemyItemShow.inst = this;
	}

	public void ShowUI(EnemyNpcAttack _npc)
	{
		this.npc = _npc;
	}

	public void DetecBtnClick(GameObject ga)
	{
		EnemyManage.inst.enemyPanle.SetActive(true);
		EnemyPanelManege.inst.ShowUI(this.npc);
	}

	public void AttackBtnClick(GameObject ga)
	{
		CSBattleStart cSBattleStart = new CSBattleStart();
		cSBattleStart.id = (long)this.npc.id;
		cSBattleStart.from = 5;
		ClientMgr.GetNet().SendHttp(5000, cSBattleStart, new DataHandler.OpcodeHandler(this.StartAttack), null);
	}

	public void StartAttack(bool isFalse, Opcode opcode)
	{
		if (!isFalse)
		{
			List<SCBattleStart> list = opcode.Get<SCBattleStart>(10067);
			if (list.Count > 0 && list[0].start)
			{
				EnemyAttackManage.inst.waveid = 1;
				EnemyAttackManage.inst.isFist = true;
				UIManager.inst.enemyPanel.gameObject.SetActive(false);
				EnemyAttackManage.inst.enemyScore = new EnemyScore();
				GameSetting.autoFight = false;
				EnemyAttackManage.inst.StartAttack(this.npc);
			}
		}
	}
}
