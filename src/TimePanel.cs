using System;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
	public static TimePanel inst;

	public float timer;

	private float finiTime;

	public UILabel time;

	public GameObject timeGa;

	public GameObject ranEventPrefab;

	public bool isEnd;

	public UILabel PlayerName;

	public UILabel PlayerLevel;

	public UILabel EnemyName;

	public UILabel EnemyLevel;

	public GameObject box;

	public UILabel boxNum;

	public int num;

	public string enemyName;

	public UILabel countdownUIlabel;

	public GameObject countdownGa;

	private bool isStartTime;

	private DateTime dateTime_StartFire;

	private int startFireTimeSecond = 10;

	private float timeRemaining;

	private void Awake()
	{
		TimePanel.inst = this;
		SenceManager.inst.settType = SettlementType.failure;
	}

	private void Start()
	{
	}

	public void StartTime()
	{
		if (this.isStartTime)
		{
			return;
		}
		if (!NewbieGuidePanel.isEnemyAttck)
		{
			return;
		}
		this.timeGa.SetActive(true);
		this.isStartTime = true;
		this.dateTime_StartFire = DateTime.Now;
		this.finiTime = Time.time + (float)int.Parse(UnitConst.GetInstance().DesighConfigDic[86].value);
	}

	private void OnEnable()
	{
		this.isEnd = false;
		this.PlayerName.text = HeroInfo.GetInstance().userName;
		this.PlayerLevel.text = HeroInfo.GetInstance().playerlevel.ToString();
		EventNoteMgr.BegainNewWar();
	}

	public void OnEnemyInfo(string Name, string Leve)
	{
		this.EnemyName.text = Name;
		this.EnemyLevel.text = Leve.ToString();
	}

	private void Update()
	{
		if (!FightHundler.FightEnd && !this.isEnd && this.isStartTime)
		{
			this.timer += Time.deltaTime;
			this.timeRemaining = this.finiTime - Time.time;
			this.time.text = string.Format("{0}分{1}秒", (int)(this.timeRemaining / 60f), (int)(this.timeRemaining % 60f));
			if (this.timeRemaining > 0f && (int)(this.timeRemaining / 60f) == 0 && (int)(this.timeRemaining % 60f) <= 5)
			{
				this.countdownGa.SetActive(true);
				this.countdownUIlabel.text = ((int)(this.timeRemaining % 60f)).ToString();
			}
			else
			{
				this.countdownGa.SetActive(false);
			}
			if (Time.time > this.finiTime)
			{
				SenceManager.inst.UnitOver(3);
			}
			else if ((DateTime.Now - this.dateTime_StartFire).TotalSeconds > (double)this.startFireTimeSecond)
			{
				this.startFireTimeSecond += 10;
				FightHundler.CG_UniteDead();
			}
		}
		if (SenceManager.inst.fightType == FightingType.Guard)
		{
			this.box.SetActive(true);
			this.boxNum.text = this.num.ToString();
		}
		else
		{
			this.box.SetActive(false);
		}
	}
}
