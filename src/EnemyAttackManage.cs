using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttackManage : MonoBehaviour
{
	public static EnemyAttackManage inst;

	public Dictionary<int, NpcAttark> npc;

	public int waveid = 1;

	public Vector3 endTank;

	private int tankNum;

	private int wave;

	private EnemyNpcAttack m_npc;

	public bool isFist;

	public EnemyScore enemyScore;

	public bool isAttack = true;

	public bool isExistTank;

	public bool isAwardBox;

	public GameObject awardBox;

	public float awardBoxTime;

	public void OnDestroy()
	{
		EnemyAttackManage.inst = null;
	}

	private void Awake()
	{
		EnemyAttackManage.inst = this;
	}

	public void ShowBoxItem()
	{
		string name = UnitConst.GetInstance().ItemConst[(int)this.enemyScore.wavePrize[this.waveid - 2].key].Name;
		string str = this.enemyScore.wavePrize[this.waveid - 2].value.ToString();
		HUDTextTool.inst.SetText("恭喜你获得" + name + "X" + str, HUDTextTool.TextUITypeEnum.Num1);
	}

	public void StartAttack(EnemyNpcAttack Npc)
	{
		GameObject gameObject = FuncUIManager.inst.OpenFuncUI("FightPanel", SenceType.Island);
		CameraControl.inst.isMouseScrollWheel = true;
		UIManager.inst.ResetSenceState(SenceState.Attacking);
		this.m_npc = Npc;
		this.wave = Npc.waveID[this.waveid - 1];
		this.npc = UnitConst.GetInstance().npcAttartList;
		NpcAttark npcAttark = this.npc[this.wave];
		SenceManager.inst.fightType = FightingType.Guard;
		UIManager.curState = SenceState.Spy;
		if (this.isFist)
		{
			SenceManager.inst.RefreshPath();
			TimePanel.inst.num = 0;
		}
		FuncUIManager.inst.gameObject.SetActive(false);
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.gameObject.SetActive(true);
			FightPanelManager.inst.HidePanelList();
		}
		this.isFist = false;
		foreach (int current in npcAttark.armyNum.Keys)
		{
			this.tankNum = npcAttark.armyNum[current];
		}
		this.waveid++;
	}

	private void Update()
	{
		if (this.tankNum != 0 && SenceManager.inst.Tanks_Attack.Count > 0)
		{
			if (SenceManager.inst.Tanks_Attack.Count >= this.tankNum)
			{
				this.isAttack = true;
			}
			if (this.isAttack)
			{
				this.TankAttack();
				this.tankNum = 0;
			}
		}
		if (this.isExistTank)
		{
			if (SenceManager.inst.Tanks_Attack.Count > 1)
			{
				this.endTank = SenceManager.inst.Tanks_Attack[0].tr.position;
			}
			if (SenceManager.inst.Tanks_Attack.Count == 0)
			{
				this.CreateAwardBox();
			}
		}
		if (this.isAwardBox)
		{
			this.awardBoxTime += Time.deltaTime;
			if (this.awardBoxTime > 10f)
			{
				TimePanel.inst.num++;
				this.isAwardBox = false;
				this.tankNum = 0;
				UnityEngine.Object.Destroy(this.awardBox);
				CoroutineInstance.DoJob(ResFly2.CreateRes(this.awardBox.transform, ResType.奖牌, 1, null, null));
				this.ShowBoxItem();
				if (this.waveid <= this.m_npc.waveID.Count)
				{
					this.StartAttack(this.m_npc);
				}
				else
				{
					FightHundler.CG_FinishFight();
				}
			}
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.gameObject.tag == "AwardBox")
				{
					TimePanel.inst.num++;
					this.isAwardBox = false;
					this.tankNum = 0;
					UnityEngine.Object.Destroy(this.awardBox);
					CoroutineInstance.DoJob(ResFly2.CreateRes(this.awardBox.transform, ResType.奖牌, 1, null, null));
					this.ShowBoxItem();
					if (this.waveid <= this.m_npc.waveID.Count)
					{
						this.StartAttack(this.m_npc);
					}
					else
					{
						FightHundler.CG_FinishFight();
					}
				}
			}
		}
	}

	public void CreateAwardBox()
	{
		this.isExistTank = false;
		this.isAwardBox = true;
		this.awardBox = (UnityEngine.Object.Instantiate(Resources.Load(ResManager.FuncUI_Path + "AwardBox"), this.endTank, Quaternion.identity) as GameObject);
		this.awardBoxTime = 0f;
		CSFightingEvent cSFightingEvent = new CSFightingEvent();
		FightEventData fightEventData = new FightEventData();
		fightEventData.deadType = 5;
		fightEventData.deadId = (long)this.wave;
		cSFightingEvent.data.Add(fightEventData);
		ClientMgr.GetNet().SendHttp(5004, cSFightingEvent, null, null);
	}

	public void TankAttack()
	{
		this.isExistTank = true;
		List<T_Tower> towers = SenceManager.inst.towers;
		T_Tower t_Tower = (from a in SenceManager.inst.towers
		where UnitConst.GetInstance().buildingConst[a.index].resType != 3 && UnitConst.GetInstance().buildingConst[a.index].resType != 5
		orderby Vector3.Distance(a.tr.position, SenceManager.inst.Tanks_Attack[0].tr.position)
		select a).FirstOrDefault<T_Tower>();
		LogManage.Log("目标点的位置" + t_Tower);
		if (UnitConst.GetInstance().buildingConst[t_Tower.GetComponent<T_Tower>().index].secType == 1)
		{
			t_Tower.GetComponent<T_Tower>().DieCallBack.Add(new Action(this.TankAttack));
		}
		else
		{
			t_Tower.GetComponent<T_Tower>().DieCallBack.Add(new Action(this.TankAttack));
		}
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			Vector3 pos = t_Tower.tr.position + SenceManager.inst.GetVPos(SenceManager.inst.Tanks_Attack[i], SenceManager.inst.Tanks_Attack[i].CharacterBaseFightInfo.ShootMaxRadius, t_Tower.tr.position);
			SenceManager.inst.Tanks_Attack[i].ForceAttack(t_Tower.tr, pos);
		}
	}
}
