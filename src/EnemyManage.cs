using System;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
	public static EnemyManage inst;

	public GameObject enemyPanle;

	public Transform Grid;

	public EnemyItemShow[] enemyList;

	public void OnDestroy()
	{
		EnemyManage.inst = null;
	}

	private void Awake()
	{
		EnemyManage.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_Close, new EventManager.VoidDelegate(this.CloseThisPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_CloseEnemyShow, new EventManager.VoidDelegate(this.CloseEnemyPanle));
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_DetecBtn, new EventManager.VoidDelegate(this.DetecBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.EnemyPanel_AttackBtn, new EventManager.VoidDelegate(this.AttackBtnClick));
	}

	public void DetecBtnClick(GameObject ga)
	{
		EnemyItemShow componentInParent = ga.GetComponentInParent<EnemyItemShow>();
		componentInParent.DetecBtnClick(null);
	}

	public void AttackBtnClick(GameObject ga)
	{
		EnemyItemShow componentInParent = ga.GetComponentInParent<EnemyItemShow>();
		componentInParent.AttackBtnClick(null);
	}

	private void Start()
	{
		this.ShowUI();
		if (UIManager.inst)
		{
			UIManager.inst.UIInUsed(true);
		}
	}

	public void ShowUI()
	{
		CameraControl.inst.isMouseScrollWheel = false;
		for (int i = 0; i < HeroInfo.GetInstance().enemyAttack.Count; i++)
		{
			this.enemyList[i].gameObject.SetActive(true);
			this.enemyList[i].ShowUI(HeroInfo.GetInstance().enemyAttack[i]);
		}
		for (int j = HeroInfo.GetInstance().enemyAttack.Count; j < this.enemyList.Length; j++)
		{
			this.enemyList[j].gameObject.SetActive(false);
		}
	}

	public void CloseThisPanel(GameObject ga)
	{
		if (UIManager.inst)
		{
			UIManager.inst.UIInUsed(false);
		}
		CameraControl.inst.isMouseScrollWheel = true;
		base.gameObject.SetActive(false);
	}

	public void CloseEnemyPanle(GameObject ga)
	{
		for (int i = 0; i < EnemyPanelManege.inst.tankShowList.Count; i++)
		{
			UnityEngine.Object.Destroy(EnemyPanelManege.inst.tankShowList[i]);
		}
		EnemyPanelManege.inst.tankShowList.Clear();
		this.enemyPanle.SetActive(false);
	}
}
