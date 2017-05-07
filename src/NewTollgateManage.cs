using DicForUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NewTollgateManage : MonoBehaviour
{
	public static NewTollgateManage inst;

	public Battle item;

	public List<GameObject> tollgateItemList = new List<GameObject>();

	public GameObject BattleFightUIInfo;

	public Transform parent;

	public List<Transform> dianList = new List<Transform>();

	public UILabel battleName;

	public GameObject closeZhanyi;

	private float time;

	private bool tishi = true;

	private List<BattleField> AllBattleField = new List<BattleField>();

	public void OnDestroy()
	{
		NewTollgateManage.inst = null;
	}

	private void Awake()
	{
		NewTollgateManage.inst = this;
		this.Initialize();
		base.gameObject.SetActive(false);
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_GoBattleField, new EventManager.VoidDelegate(this.GoBattleField));
		EventManager.Instance.AddEvent(EventManager.EventType.battlePanel_CloseGotoFight, new EventManager.VoidDelegate(this.CloseGotoFight));
	}

	private void Initialize()
	{
		this.BattleFightUIInfo = base.transform.FindChild("battle").gameObject;
		this.BattleFightUIInfo.AddComponent<BattleFightUIInfo>();
		this.parent = base.transform.FindChild("TollgateItemList");
		this.battleName = base.transform.FindChild("battle/Layout name/Label").GetComponent<UILabel>();
	}

	private void Update()
	{
		if (!this.tishi)
		{
			this.time += Time.deltaTime;
			if (this.time > 3f)
			{
				this.time = 0f;
				this.tishi = true;
			}
		}
	}

	private void CloseGotoFight(GameObject ga)
	{
		ga.transform.parent.gameObject.SetActive(false);
	}

	private void GoBattleField(GameObject ga)
	{
		BattleField curBattleField = ga.GetComponent<NewTollgateItem>().item;
		this.OnBattleFieldPanelShow(curBattleField);
	}

	public void OnBattleFieldPanelShow(BattleField curBattleField)
	{
		if (curBattleField.preId != 0 && !UnitConst.GetInstance().BattleFieldConst[curBattleField.preId].fightRecord.isFight)
		{
			if (this.tishi)
			{
				this.tishi = false;
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("前置关卡未通过", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
		else if (curBattleField.preId == 0 || curBattleField.needBattleStar <= UnitConst.GetInstance().BattleConst[curBattleField.battleId].FightRecord.star)
		{
			this.BattleFightUIInfo.SetActive(true);
			this.BattleFightUIInfo.GetComponent<BattleFightUIInfo>().ShowBattelFightInFo(curBattleField);
		}
		else
		{
			MessageBox.GetMessagePanel().ShowBtn("星数不足", string.Format("战役星数未达到{0}", curBattleField.needBattleStar), "确定", null);
		}
	}

	public void ShowTollgateItem()
	{
		this.battleName.text = LanguageManage.GetTextByKey(this.item.name, "Battle");
		for (int i = 0; i < this.tollgateItemList.Count; i++)
		{
			UnityEngine.Object.Destroy(this.tollgateItemList[i]);
		}
		this.tollgateItemList.Clear();
		DicForU.GetValues<int, BattleField>(this.item.allBattleField, this.AllBattleField);
		for (int j = 0; j < this.AllBattleField.Count; j++)
		{
			BattleField battleField = this.AllBattleField[j];
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(ResManager.FuncUI_Path + "NewTollgateItem") as GameObject) as GameObject;
			NewTollgateItem component = gameObject.GetComponent<NewTollgateItem>();
			component.item = battleField;
			if (battleField.preId != 0 && battleField.nextId != 0)
			{
				if (!battleField.fightRecord.isFight && this.item.allBattleField[battleField.preId].fightRecord.isFight && !this.item.allBattleField[battleField.nextId].fightRecord.isFight)
				{
					HUDTextTool.inst.isEndTollgate = null;
					component.isCurrentTollgate = true;
				}
			}
			else if (battleField.nextId == 0 && this.item.allBattleField[battleField.preId].fightRecord.isFight)
			{
				HUDTextTool.inst.isEndTollgate = battleField;
				component.isCurrentTollgate = true;
			}
			else if (battleField.preId == 0 && !this.item.allBattleField[battleField.nextId].fightRecord.isFight)
			{
				HUDTextTool.inst.isEndTollgate = null;
				component.isCurrentTollgate = true;
			}
			component.ShowTollgate();
			gameObject.transform.parent = this.parent;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = new Vector3(battleField.coord[0], battleField.coord[1], battleField.coord[2]);
			this.tollgateItemList.Add(gameObject);
		}
	}
}
