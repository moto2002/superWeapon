using DicForUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NBattleManage : MonoBehaviour
{
	public static NBattleManage inst;

	public List<GameObject> NBattleItemList = new List<GameObject>();

	public Transform parent;

	public List<Battle> AllBattle = new List<Battle>();

	public GameObject closeBtn;

	public void OnDestroy()
	{
		NBattleManage.inst = null;
	}

	private void Awake()
	{
		NBattleManage.inst = this;
		base.gameObject.SetActive(false);
		this.Initialize();
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_battleItem, new EventManager.VoidDelegate(this.BattleItemClick));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_ClosePanle, new EventManager.VoidDelegate(this.ClosePanle));
	}

	private void Initialize()
	{
		this.parent = base.transform.FindChild("BattleList");
		this.closeBtn = base.transform.FindChild("Sprite").gameObject;
	}

	private void ClosePanle(GameObject ga)
	{
		SenceInfo.CurBattle = null;
		FuncUIManager.inst.MainUIPanelManage.gameObject.SetActive(true);
		base.transform.parent.gameObject.SetActive(false);
	}

	public void BattleItemClick(GameObject ga)
	{
	}

	public void BattleItem(Battle item)
	{
	}

	public void CreateBattleItem()
	{
		for (int i = 0; i < this.NBattleItemList.Count; i++)
		{
			UnityEngine.Object.Destroy(this.NBattleItemList[i]);
		}
		this.NBattleItemList.Clear();
		DicForU.GetValues<int, Battle>(UnitConst.GetInstance().BattleConst, this.AllBattle);
		for (int j = 0; j < this.AllBattle.Count; j++)
		{
			Battle battle = this.AllBattle[j];
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(ResManager.FuncUI_Path + "NBattleItem") as GameObject) as GameObject;
			gameObject.GetComponent<NBattleItem>().item = battle;
			gameObject.transform.parent = this.parent;
			gameObject.transform.localPosition = new Vector3(battle.coord[0], battle.coord[1], battle.coord[2]);
			gameObject.transform.localScale = Vector3.one;
			if (battle.preId != 0 && battle.nextId != 0)
			{
				if (!battle.FightRecord.isFight && UnitConst.GetInstance().BattleConst[battle.preId].FightRecord.isFight && !UnitConst.GetInstance().BattleConst[battle.nextId].FightRecord.isFight)
				{
					gameObject.GetComponent<NBattleItem>().isNowBattle = true;
				}
			}
			else if (battle.preId == 0 && !UnitConst.GetInstance().BattleConst[battle.nextId].FightRecord.isFight && !battle.FightRecord.isFight)
			{
				gameObject.GetComponent<NBattleItem>().isNowBattle = true;
			}
			else if (battle.nextId == 0 && UnitConst.GetInstance().BattleConst[battle.preId].FightRecord.isFight)
			{
				gameObject.GetComponent<NBattleItem>().isNowBattle = true;
			}
			gameObject.GetComponent<NBattleItem>().ShowBattleItem();
			this.NBattleItemList.Add(gameObject);
		}
	}
}
