using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingTimeShow : MonoBehaviour
{
	public UISprite TimeShowBtn;

	public UIGrid grid;

	public GameObject showGame;

	public GameObject LesiureShow;

	public UILabel Notice;

	private bool TimeShow;

	private List<KVStruct> allArmyCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_LandDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	private List<KVStruct> allAirCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_AirDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd -= new Action(this.NetDataHandler_DataChange);
		}
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_BuildingTimeShow, new EventManager.VoidDelegate(this.MainPanel_BuildingTimeShow_CallBack));
	}

	private void SetBtnShow(bool show)
	{
		show = true;
		this.TimeShowBtn.enabled = show;
		this.TimeShowBtn.GetComponent<BoxCollider>().enabled = show;
	}

	private void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd += new Action(this.NetDataHandler_DataChange);
		}
		this.NetDataHandler_DataChange();
	}

	private int TimeShowNum()
	{
		int num = 0;
		if (HeroInfo.GetInstance().BuildCD.Count > 0)
		{
			num += HeroInfo.GetInstance().BuildCD.Count;
		}
		if (this.allArmyCDTime.Count > 0)
		{
			num += this.allArmyCDTime.Count;
		}
		if (this.allAirCDTime.Count > 0)
		{
			num += this.allAirCDTime.Count;
		}
		return num;
	}

	private void NetDataHandler_DataChange()
	{
		int num = this.TimeShowNum();
		if (num > 0)
		{
			this.SetBtnShow(true);
			this.TimeShow = true;
		}
		else
		{
			this.SetBtnShow(false);
			this.TimeShow = false;
			this.grid.ClearChild();
			if (HeroInfo.GetInstance().BuildCD.Count < BuildingQueue.MaxBuildingQueue)
			{
				this.Notice.gameObject.SetActive(true);
			}
		}
		this.Notice.gameObject.SetActive(false);
		if (this.TimeShow)
		{
			this.showBuildingTime();
		}
		else if (HeroInfo.GetInstance().BuildCD.Count < BuildingQueue.MaxBuildingQueue)
		{
			this.Notice.gameObject.SetActive(true);
		}
	}

	private void MainPanel_BuildingTimeShow_CallBack(GameObject ga)
	{
		this.grid.cellHeight = 40f;
		if (!this.TimeShow)
		{
			this.TimeShow = true;
			this.showBuildingTime();
			this.Notice.gameObject.SetActive(false);
		}
		else
		{
			this.TimeShow = false;
			this.grid.ClearChild();
			if (HeroInfo.GetInstance().BuildCD.Count < BuildingQueue.MaxBuildingQueue)
			{
				this.Notice.gameObject.SetActive(true);
			}
		}
	}

	public void showBuildingTime()
	{
		float num = 40f;
		int num2 = 0;
		this.grid.ClearChild();
		for (int i = 0; i < HeroInfo.GetInstance().BuildCD.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.showGame);
			gameObject.transform.localPosition = new Vector3(0f, -num * (float)num2, 0f);
			num2++;
			ShowTimeTip component = gameObject.GetComponent<ShowTimeTip>();
			for (int j = 0; j < SenceManager.inst.towers.Count; j++)
			{
				if (HeroInfo.GetInstance().BuildCD.Contains(SenceManager.inst.towers[j].id))
				{
					component.tar = SenceManager.inst.towers[j];
					gameObject.name = component.tar.id.ToString();
					component.cdType = 1;
					component.btnType = 1;
					component.id = component.tar.id;
					component.posIndex = component.tar.posIdx;
					component.itemid = component.tar.index;
				}
			}
			foreach (KeyValuePair<int, T_Res> current in SenceManager.inst.reses)
			{
				if (SenceInfo.curMap.ResRemoveCDTime.ContainsKey((long)current.Key))
				{
					component.res = current.Value;
					component.cdType = 4;
					component.btnType = 2;
					component.id = (long)component.res.posIndex;
					component.posIndex = component.res.posIndex;
					component.itemid = component.res.index;
				}
			}
			component.SetUpdatingEum(1);
			component.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[HeroInfo.GetInstance().BuildCD[i]]);
		}
		if (HeroInfo.GetInstance().BuildCD.Count < BuildingQueue.MaxBuildingQueue)
		{
			for (int k = 0; k < BuildingQueue.MaxBuildingQueue - HeroInfo.GetInstance().BuildCD.Count; k++)
			{
				GameObject gameObject2 = NGUITools.AddChild(this.grid.gameObject, this.LesiureShow);
				gameObject2.transform.localPosition = new Vector3(0f, -num * (float)num2, 0f);
				num2++;
			}
		}
		if (this.allArmyCDTime.Count > 0)
		{
			for (int l = 0; l < 1; l++)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.grid.gameObject, this.showGame);
				gameObject3.transform.localPosition = new Vector3(0f, -num * (float)num2, 0f);
				num2++;
				ShowTimeTip component2 = gameObject3.GetComponent<ShowTimeTip>();
				component2.cdType = 2;
				component2.id = this.allArmyCDTime[l].key;
				component2.posIndex = int.Parse(this.allArmyCDTime[l].key.ToString());
				component2.itemid = int.Parse(this.allArmyCDTime[l].key.ToString());
				component2.btnType = 3;
				component2.SetUpdatingEum(2);
				component2.endTime = TimeTools.ConvertLongDateTime(this.allArmyCDTime[this.allArmyCDTime.Count - 1].value);
			}
		}
		if (this.allAirCDTime.Count > 0)
		{
			for (int m = 0; m < 1; m++)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.grid.gameObject, this.showGame);
				gameObject4.transform.localPosition = new Vector3(0f, -num * (float)num2, 0f);
				num2++;
				ShowTimeTip component3 = gameObject4.GetComponent<ShowTimeTip>();
				component3.cdType = 2;
				component3.btnType = 4;
				component3.id = this.allAirCDTime[m].key;
				component3.posIndex = int.Parse(this.allAirCDTime[m].key.ToString());
				component3.itemid = int.Parse(this.allAirCDTime[m].key.ToString());
				component3.SetUpdatingEum(2);
				component3.endTime = TimeTools.ConvertLongDateTime(this.allAirCDTime[this.allAirCDTime.Count - 1].value);
			}
		}
	}

	private void Update()
	{
	}
}
