using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventNoteManager : MonoBehaviour
{
	public static EventNoteManager inst;

	public float timer;

	public float leftTime;

	private DateTime finiTime;

	public GameObject rewatchBtn;

	public GameObject speedBtn;

	public UILabel describe;

	public UILabel time;

	public UILabel timeBtn;

	private int timeSpeed = 1;

	public GameObject randomEventBoxPrefab;

	private bool isEnd;

	private int eventIndex;

	private int tankPosIndex;

	public static int armyType_Controled;

	public static int armyIndex_Controled;

	public void OnDestroy()
	{
		EventNoteManager.inst = null;
	}

	public void OnDisable()
	{
		Time.timeScale = 1f;
	}

	private void Awake()
	{
		EventNoteManager.inst = this;
	}

	private void Start()
	{
		this.Init();
		GameSetting.RandomSeed = EventNoteMgr.EvenData.randomSeed;
		EventNoteManager.armyType_Controled = 0;
		EventNoteMgr.TankText.Remove(0, EventNoteMgr.TankText.Length);
	}

	private void Update()
	{
		if (!this.isEnd)
		{
			if (this.timer > EventNoteMgr.EvenData.endTime + 2f)
			{
				this.FinishEventNote();
			}
			else
			{
				this.timer += Time.deltaTime;
				this.time.text = TimeTools.ConvertFloatToTimeBySecond(this.leftTime - this.timer);
				if (this.eventIndex >= EventNoteMgr.EvenData.operData.Count)
				{
					return;
				}
				if (this.timer > (float)EventNoteMgr.EvenData.operData[this.eventIndex].time * 0.1f)
				{
					this.DoEvent(EventNoteMgr.EvenData.operData[this.eventIndex]);
					this.eventIndex++;
				}
				if (this.tankPosIndex >= EventNoteMgr.EvenData.tankPoses.Count)
				{
					return;
				}
				if (this.timer > (float)EventNoteMgr.EvenData.tankPoses[this.tankPosIndex].time)
				{
					this.tankPosIndex++;
				}
			}
		}
	}

	private void DoEvent(OperationEventData eventData)
	{
		switch (eventData.OperationType)
		{
		case 1:
			LogManage.Log("播放send");
			SenceManager.inst.NoteSend(eventData.send);
			break;
		case 2:
			LogManage.Log("播放move");
			this.SynchronizationTanksPos(eventData.move.tanksPos);
			SenceManager.inst.ForceMove(EventNoteMgr.MsgPosToVector(eventData.move.pos));
			break;
		case 3:
		{
			LogManage.Log("播放技能");
			Vector3 pos = EventNoteMgr.MsgPosToVector(eventData.superSkill.pos);
			this.SynchronizationTanksPos(eventData.superSkill.tanksPos);
			SkillManage.inst.AcquittalSkill(eventData.superSkill.skillId, pos, -1);
			break;
		}
		case 4:
			LogManage.Log("播放die");
			this.SynchronizationTanksPos(eventData.die.tanksPos);
			SenceManager.inst.NoteUnitOver(eventData.die.type, eventData.die.id);
			break;
		case 5:
			LogManage.Log("播放foceAtt");
			this.SynchronizationTanksPos(eventData.foceAtt.tanksPos);
			SenceManager.inst.ForceFindAttack(eventData.foceAtt.id);
			break;
		case 6:
			LogManage.Log("播放ChangeAutoAttack");
			if (eventData.IsAutoFight)
			{
				SenceManager.inst.TankSearching();
			}
			GameSetting.autoFight = eventData.IsAutoFight;
			break;
		case 7:
			LogManage.Log("播放OpenBox");
			break;
		case 8:
			LogManage.Log("播放兵种行动选择 SoliderSelected");
			EventNoteManager.armyType_Controled = eventData.armyControl.type;
			EventNoteManager.armyIndex_Controled = eventData.armyControl.index;
			break;
		default:
			LogManage.Log("播放？？===" + eventData.OperationType);
			break;
		}
	}

	private void SynchronizationTanksPos(OperationTankPos data)
	{
	}

	private void SynchronizationTanksPos(List<OperationTanksPos> data)
	{
	}

	private void FinishEventNote()
	{
		if (!this.isEnd)
		{
			this.isEnd = true;
			this.speedBtn.SetActive(false);
			this.rewatchBtn.SetActive(true);
			FightHundler.FightEnd = true;
		}
	}

	public void ButtonEvent(EventNoteBtnType btnType)
	{
		switch (btnType)
		{
		case EventNoteBtnType.speed:
			if (this.timeSpeed == 1)
			{
				this.timeSpeed = 2;
			}
			else if (this.timeSpeed == 2)
			{
				this.timeSpeed = 4;
			}
			else if (this.timeSpeed == 4)
			{
				this.timeSpeed = 1;
			}
			Time.timeScale = (float)this.timeSpeed;
			this.timeBtn.text = this.timeSpeed.ToString();
			LogManage.Log("点击watchspeed啦！");
			break;
		case EventNoteBtnType.rewatch:
			Time.timeScale = 1f;
			LogManage.Log("点击rewatch啦！");
			SenceHandler.GC_WatchNote2();
			break;
		case EventNoteBtnType.back:
			Time.timeScale = 1f;
			if (SenceInfo.battleResource == SenceInfo.BattleResource.ReplayVideo_Home)
			{
				LogManage.Log("点击back Home啦！");
				Loading.IsRefreshSence = true;
				SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
			}
			else
			{
				LogManage.Log("点击back WorldMap啦！");
				UIManager.curState = SenceState.None;
				PlayerHandle.GOTO_WorldMap();
			}
			break;
		}
	}

	public void Init()
	{
		Time.timeScale = 1f;
		this.eventIndex = 0;
		this.tankPosIndex = 0;
		this.timer = 0f;
		this.leftTime = EventNoteMgr.EvenData.endTime;
		this.finiTime = TimeTools.ConvertSecondDateTime(Mathf.CeilToInt(this.leftTime));
		this.rewatchBtn.SetActive(false);
	}
}
