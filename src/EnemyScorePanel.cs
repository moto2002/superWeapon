using System;
using UnityEngine;

public class EnemyScorePanel : MonoBehaviour
{
	public UILabel victoryName;

	public UILabel vivtoryAwardName;

	public EnemyScoreItem[] boxAward;

	public EnemyScoreItem[] victoryAward;

	private void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_CloseScorePanel, new EventManager.VoidDelegate(this.CloseThisPanel));
	}

	private void Start()
	{
		this.ShowUI();
	}

	public void ShowUI()
	{
		EnemyScore enemyScore = EnemyAttackManage.inst.enemyScore;
		if (enemyScore.wavePrize.Count > 0 && enemyScore.wavePrize.Count <= this.boxAward.Length)
		{
			for (int i = 0; i < enemyScore.wavePrize.Count; i++)
			{
				this.boxAward[i].gameObject.SetActive(true);
				this.boxAward[i].icon.spriteName = UnitConst.GetInstance().ItemConst[(int)enemyScore.wavePrize[i].key].IconId;
				this.boxAward[i].num.text = enemyScore.wavePrize[i].value.ToString();
				this.boxAward[i]._name = UnitConst.GetInstance().ItemConst[(int)enemyScore.wavePrize[i].key].Name;
			}
			for (int j = enemyScore.wavePrize.Count; j < this.boxAward.Length; j++)
			{
				this.boxAward[j].gameObject.SetActive(false);
			}
		}
		else
		{
			for (int k = 0; k < this.boxAward.Length; k++)
			{
				this.boxAward[k].gameObject.SetActive(false);
			}
		}
		if (enemyScore.item.Count + enemyScore.res.Count > 0 && enemyScore.item.Count + enemyScore.res.Count <= this.victoryAward.Length)
		{
			this.victoryName.text = "战斗胜利";
			this.vivtoryAwardName.text = "胜利奖励";
			for (int l = 0; l < enemyScore.item.Count; l++)
			{
				this.victoryAward[l].gameObject.SetActive(true);
				this.victoryAward[l].icon.spriteName = UnitConst.GetInstance().ItemConst[(int)enemyScore.item[l].key].IconId;
				this.victoryAward[l].num.text = enemyScore.item[l].value.ToString();
				this.victoryAward[l]._name = UnitConst.GetInstance().ItemConst[(int)enemyScore.item[l].key].Name;
			}
			for (int m = enemyScore.item.Count; m < enemyScore.item.Count + enemyScore.res.Count; m++)
			{
				this.victoryAward[m].gameObject.SetActive(true);
				this.victoryAward[m].icon.spriteName = UnitConst.GetInstance().ItemConst[(int)enemyScore.res[m - enemyScore.item.Count].key].IconId;
				this.victoryAward[m].num.text = enemyScore.res[m - enemyScore.item.Count].value.ToString();
				this.victoryAward[m]._name = UnitConst.GetInstance().ItemConst[(int)enemyScore.res[m - enemyScore.item.Count].key].Name;
			}
			for (int n = enemyScore.item.Count + enemyScore.res.Count; n < this.victoryAward.Length; n++)
			{
				this.victoryAward[n].gameObject.SetActive(false);
			}
		}
		else
		{
			for (int num = 0; num < this.victoryAward.Length; num++)
			{
				this.victoryAward[num].gameObject.SetActive(false);
			}
			this.victoryName.text = "战斗失败";
			this.vivtoryAwardName.text = "胜利奖励  你没有获得任何奖励";
		}
	}

	public void CloseThisPanel(GameObject ga)
	{
		FuncUIManager.inst.gameObject.SetActive(true);
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.ShowPanelList();
		}
		UIManager.inst.ResetSenceState(SenceState.Home);
		EnemyAttackManage.inst.isExistTank = false;
		UIManager.curState = SenceState.Home;
		CoroutineInstance.DoJob(SenceManager.inst.CreateMap());
		SenceManager.inst.fightType = FightingType.Attack;
		base.gameObject.SetActive(false);
	}
}
