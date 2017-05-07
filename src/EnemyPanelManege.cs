using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanelManege : MonoBehaviour
{
	public static EnemyPanelManege inst;

	public UILabel nameUIlabel;

	public UILabel lv;

	public Transform Grid;

	public GameObject enemyTank;

	public List<GameObject> tankShowList = new List<GameObject>();

	public void OnDestroy()
	{
		EnemyPanelManege.inst = null;
	}

	private void Awake()
	{
		EnemyPanelManege.inst = this;
	}

	public void ShowUI(EnemyNpcAttack npc)
	{
		this.nameUIlabel.text = npc.name;
		this.lv.text = "LV." + HeroInfo.GetInstance().playerlevel.ToString();
		for (int i = 0; i < npc.waveID.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(ResManager.FuncUI_Path + "EnemyTankItem"), base.transform.position, base.transform.rotation) as GameObject;
			gameObject.transform.parent = this.Grid;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			EnemyTankShow component = gameObject.GetComponent<EnemyTankShow>();
			component.nameUIlabel.text = "第" + (i + 1) + "波";
			component.ShowUI(npc.waveID[i]);
			this.tankShowList.Add(gameObject);
		}
		this.Grid.GetComponent<UIGrid>().Reposition();
	}
}
