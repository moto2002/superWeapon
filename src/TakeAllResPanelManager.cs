using System;
using System.Collections.Generic;
using UnityEngine;

public class TakeAllResPanelManager : MonoBehaviour
{
	public UILabel coinCount;

	public UILabel oilCount;

	public UILabel steelCount;

	public UILabel rareCount;

	public GameObject sureBtn;

	public GameObject cancleBtn;

	public static TakeAllResPanelManager ins;

	public List<T_Tower> resTower = new List<T_Tower>();

	public int[] resCount = new int[4];

	public UIGrid grid;

	public float outProNum;

	private float coinMax;

	private float oilMax;

	private float steelMax;

	private float rareMax;

	private bool isCoin;

	private bool isOil;

	private bool isSteel;

	private bool isRare;

	public void OnDestroy()
	{
		TakeAllResPanelManager.ins = null;
	}

	private void Start()
	{
	}

	private void Awake()
	{
		TakeAllResPanelManager.ins = this;
		EventManager.Instance.AddEvent(EventManager.EventType.TakeAllResPanel_SureBtn, new EventManager.VoidDelegate(this.SureBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.TakeAllResPanel_CancleBtn, new EventManager.VoidDelegate(this.CancleBtnClick));
	}

	private void OnEnable()
	{
		this.GetResCount();
	}

	public void GetResCount()
	{
	}

	private void SureBtnClick(GameObject ga)
	{
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			T_Tower item = SenceManager.inst.towers[i];
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 3)
			{
				this.resTower.Add(item);
			}
		}
		for (int j = 0; j < this.resTower.Count; j++)
		{
			switch (UnitConst.GetInstance().buildingConst[this.resTower[j].index].outputType)
			{
			case ResType.金币:
				if (this.isCoin)
				{
					CoroutineInstance.DoJob(ResFly2.CreateRes(this.resTower[j].tr, ResType.金币, (int)this.coinMax, null, null));
					this.resTower[j].HideResModel();
				}
				break;
			case ResType.石油:
				if (this.isOil)
				{
					CoroutineInstance.DoJob(ResFly2.CreateRes(this.resTower[j].tr, ResType.石油, (int)this.oilMax, null, null));
					this.resTower[j].HideResModel();
				}
				break;
			case ResType.钢铁:
				if (this.isSteel)
				{
					CoroutineInstance.DoJob(ResFly2.CreateRes(this.resTower[j].tr, ResType.钢铁, (int)this.steelMax, null, null));
					this.resTower[j].HideResModel();
				}
				break;
			case ResType.稀矿:
				if (this.isRare)
				{
					CoroutineInstance.DoJob(ResFly2.CreateRes(this.resTower[j].tr, ResType.稀矿, (int)this.rareMax, null, null));
					this.resTower[j].HideResModel();
				}
				break;
			}
		}
		base.gameObject.SetActive(false);
	}

	private void CancleBtnClick(GameObject ga)
	{
		base.gameObject.SetActive(false);
	}
}
