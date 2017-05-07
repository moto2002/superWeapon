using DicForUnity;
using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcePanelManage : MonoBehaviour
{
	public List<ResouceTittleBtn> resPool;

	public List<Tittle_3D> _3DUIPool;

	public List<DescripUIInfo> DescripUIInfoPool;

	public static float getResNum;

	public GameObject[] resIconPrefab;

	public List<Body_Model> effModelList;

	public GameObject BuildingDesPrefab;

	public GameObject timePrefab;

	public GameObject describtionUIInfo;

	public GameObject NoelectricityPow;

	public GameObject armyTitlePrefab;

	public static long tarId;

	public static ResourcePanelManage inst;

	public Transform Camera_PlayerModel;

	private GameObject go;

	private Transform tr;

	public static Transform GetRMBTran;

	public Body_Model Qua1battleBox;

	public Body_Model Qua2battleBox;

	public Body_Model Qua3battleBox;

	public DescripUIInfo battleBoxQua1DesciptInfo;

	public DescripUIInfo battleBoxQua2DesciptInfo;

	public DescripUIInfo battleBoxQua3DesciptInfo;

	private TimeTittleBtn timeBtn_TechUpdateTime;

	private TimeTittleBtn skillCDTime;

	private float outProNum;

	private ResouceTittleBtn tittleBtn;

	private BuildingNPC item;

	private bool isOpenCollectResourceHeartBeart;

	private Dictionary<int, int> Soliders = new Dictionary<int, int>();

	private List<T_Tower> Army_Towes = new List<T_Tower>();

	private List<KVStruct> Army_LandCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_LandDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	private List<KVStruct> Army_AirCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_AirDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	public void OnDestroy()
	{
		ResourcePanelManage.inst = null;
	}

	public void FixedUpdate()
	{
		if (!UnitConst.IsHaveInstance())
		{
			return;
		}
	}

	private void Awake()
	{
		ResourcePanelManage.inst = this;
		this.tr = base.transform;
		this.go = base.gameObject;
		this.resPool = new List<ResouceTittleBtn>();
		this._3DUIPool = new List<Tittle_3D>();
		this.DescripUIInfoPool = new List<DescripUIInfo>();
	}

	public void ClearChildren()
	{
		this.isOpenCollectResourceHeartBeart = false;
		for (int i = 0; i < this.tr.childCount; i++)
		{
			Transform child = this.tr.GetChild(i);
			if (!child.Equals(this.Camera_PlayerModel))
			{
				NGUITools.Destroy(child.gameObject);
			}
		}
	}

	private void Start()
	{
		base.InvokeRepeating("CollectResourceHeartBeart", 1f, 120f);
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.RMBChange += new DataHandler.Data_Change(this.DataHandler_RMBChange);
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		this.SenceManager_OnCreateMapDataEnd();
		HUDTextTool.inst.Powerhouse();
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		if (UIManager.curState == SenceState.Home)
		{
			this.isOpenCollectResourceHeartBeart = true;
			this.CollectResourceHeartBeart();
			this.GetMonthlyCardRewards();
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				SenceManager.inst.towers[i].DisplayLogo();
			}
			this.RefreshArmyUpdateTime();
			this.RefreshTechUpdateTime();
			this.OnSetBuildCD();
			this.ShowBattleBoxData();
			this.RefreshArmyFuncTime();
			this.Army_Towes.Clear();
			this.SetTankModel(true);
		}
		else if (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.WatchVideo)
		{
			this.Army_Towes.Clear();
			this.SetTankModel(false);
		}
	}

	private void RefreshArmyFuncTime()
	{
		T_Tower[] array = (from a in SenceManager.inst.towers
		where UnitConst.GetInstance().buildingConst[a.index].secType == 15 || UnitConst.GetInstance().buildingConst[a.index].secType == 6 || UnitConst.GetInstance().buildingConst[a.index].secType == 21
		select a).ToArray<T_Tower>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].FreshArmyFuncUITimeBehaviour();
		}
	}

	private void ShowBattleBoxData()
	{
		if (BattleFieldBox.battleFieldBoxes.ContainsKey(1) && BattleFieldBox.battleFieldBoxes[1].Count > 0)
		{
			if (this.Qua1battleBox == null)
			{
				this.Qua1battleBox = PoolManage.Ins.GetModelByBundleByName("case", null);
				if (this.Qua1battleBox)
				{
					BoxCollider compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<BoxCollider>(this.Qua1battleBox.ga);
					BattleFieldBoxItem compentIfNoAddOne2 = GameTools.GetCompentIfNoAddOne<BattleFieldBoxItem>(this.Qua1battleBox.ga);
					compentIfNoAddOne2.Quilaity = 1;
					PoolManage.Ins.GetEffectByName("xiangzi_guang", this.Qua1battleBox.tr);
					compentIfNoAddOne.size = Vector3.one * 2f;
					compentIfNoAddOne.center = Vector3.zero;
					this.Qua1battleBox.tr.localPosition = new Vector3(51.27f, 0.1f, 17.59f);
					this.ResfreshBattleBox(this.Qua1battleBox, 1);
				}
			}
			if (this.Qua1battleBox)
			{
				this.battleBoxQua1DesciptInfo = this.AddChildByDescriptInfo(this.Qua1battleBox.tr, string.Empty).GetComponent<DescripUIInfo>();
				this.battleBoxQua1DesciptInfo.tipSprite.enabled = false;
				this.battleBoxQua1DesciptInfo.textDes.text = string.Format("{0}", LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[1].Sum((KVStruct a) => a.value));
			}
		}
		if (BattleFieldBox.battleFieldBoxes.ContainsKey(2) && BattleFieldBox.battleFieldBoxes[2].Count > 0)
		{
			if (this.Qua2battleBox == null)
			{
				this.Qua2battleBox = PoolManage.Ins.GetModelByBundleByName("case", null);
				if (this.Qua2battleBox)
				{
					BoxCollider compentIfNoAddOne3 = GameTools.GetCompentIfNoAddOne<BoxCollider>(this.Qua2battleBox.ga);
					BattleFieldBoxItem compentIfNoAddOne4 = GameTools.GetCompentIfNoAddOne<BattleFieldBoxItem>(this.Qua2battleBox.ga);
					compentIfNoAddOne4.Quilaity = 2;
					PoolManage.Ins.GetEffectByName("xiangzi_guang", this.Qua2battleBox.tr);
					compentIfNoAddOne3.size = Vector3.one * 2f;
					compentIfNoAddOne3.center = Vector3.zero;
					this.Qua2battleBox.tr.localPosition = new Vector3(51.27f, 0.1f, 22.88f);
					this.ResfreshBattleBox(this.Qua2battleBox, 2);
				}
			}
			if (this.Qua2battleBox)
			{
				this.battleBoxQua2DesciptInfo = this.AddChildByDescriptInfo(this.Qua2battleBox.tr, string.Empty).GetComponent<DescripUIInfo>();
				this.battleBoxQua2DesciptInfo.tipSprite.enabled = false;
				this.battleBoxQua2DesciptInfo.textDes.text = string.Format("{0}", LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[2].Sum((KVStruct a) => a.value));
			}
		}
		if (BattleFieldBox.battleFieldBoxes.ContainsKey(3) && BattleFieldBox.battleFieldBoxes[3].Count > 0)
		{
			if (this.Qua3battleBox == null)
			{
				this.Qua3battleBox = PoolManage.Ins.GetModelByBundleByName("case", null);
				if (this.Qua3battleBox)
				{
					BoxCollider compentIfNoAddOne5 = GameTools.GetCompentIfNoAddOne<BoxCollider>(this.Qua3battleBox.ga);
					BattleFieldBoxItem compentIfNoAddOne6 = GameTools.GetCompentIfNoAddOne<BattleFieldBoxItem>(this.Qua3battleBox.ga);
					compentIfNoAddOne6.Quilaity = 3;
					PoolManage.Ins.GetEffectByName("xiangzi_guang", this.Qua3battleBox.tr);
					compentIfNoAddOne5.size = Vector3.one * 2f;
					compentIfNoAddOne5.center = Vector3.zero;
					this.Qua3battleBox.tr.localPosition = new Vector3(51.2f, 0.1f, 28.5f);
					this.ResfreshBattleBox(this.Qua3battleBox, 3);
				}
			}
			if (this.Qua3battleBox)
			{
				this.battleBoxQua3DesciptInfo = this.AddChildByDescriptInfo(this.Qua3battleBox.tr, string.Empty).GetComponent<DescripUIInfo>();
				this.battleBoxQua3DesciptInfo.tipSprite.enabled = false;
				this.battleBoxQua3DesciptInfo.textDes.text = string.Format("{0}", LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[3].Sum((KVStruct a) => a.value));
			}
		}
	}

	public void ResfreshBattleBox(Body_Model Box, int quality)
	{
		Transform transform = Box.tr.FindChild("case_w");
		Transform transform2 = Box.tr.FindChild("case_y");
		Transform transform3 = Box.tr.FindChild("case_g");
		transform3.gameObject.SetActive(quality == 1);
		transform2.gameObject.SetActive(quality == 3);
		transform.gameObject.SetActive(quality == 2);
	}

	public void OnSetBuildCD()
	{
		if (UIManager.curState == SenceState.Home && NewbieGuidePanel.isEnemyAttck)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (HeroInfo.GetInstance().armyBuildingCDTime > 0L && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 6)
				{
					if (UnitConst.GetInstance().GetArmyId(SenceManager.inst.towers[i].index, SenceManager.inst.towers[i].trueLv) > 0)
					{
						if (SenceManager.inst.towers[i].ArmyTitleNew == null)
						{
							SenceManager.inst.towers[i].ArmyTitleNew = FuncUIManager.inst.ResourcePanelManage.AddArmyTitleByTime(SenceManager.inst.towers[i].tr).GetComponent<ArmyTitleShow>();
							SenceManager.inst.towers[i].ArmyTitleNew.tar = SenceManager.inst.towers[i].tr;
							SenceManager.inst.towers[i].ArmyTitleNew.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime);
							SenceManager.inst.towers[i].ArmyTitleNew.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime).AddSeconds((double)(UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].BuildTime * -1));
							SenceManager.inst.towers[i].ArmyTitleNew.SetUpdatingEum(SenceManager.inst.towers[i].index, SenceManager.inst.towers[i].trueLv);
						}
					}
					else if (SenceManager.inst.towers[i].BuilindingCDInfo == null)
					{
						SenceManager.inst.towers[i].BuilindingCDInfo = FuncUIManager.inst.ResourcePanelManage.AddChildByTime(SenceManager.inst.towers[i].tr).GetComponent<TimeTittleBtn>();
						SenceManager.inst.towers[i].BuilindingCDInfo.tar = SenceManager.inst.towers[i].tr;
						SenceManager.inst.towers[i].BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime);
						SenceManager.inst.towers[i].BuilindingCDInfo.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime).AddSeconds((double)(UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].BuildTime * -1));
						SenceManager.inst.towers[i].BuilindingCDInfo.SetUpdatingEum(1);
					}
				}
				else if (HeroInfo.GetInstance().airBuildingCDTime > 0L && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 21)
				{
					if (UnitConst.GetInstance().GetArmyId(SenceManager.inst.towers[i].index, SenceManager.inst.towers[i].trueLv) > 0)
					{
						if (SenceManager.inst.towers[i].ArmyTitleNew == null)
						{
							SenceManager.inst.towers[i].ArmyTitleNew = FuncUIManager.inst.ResourcePanelManage.AddArmyTitleByTime(SenceManager.inst.towers[i].tr).GetComponent<ArmyTitleShow>();
							SenceManager.inst.towers[i].ArmyTitleNew.tar = SenceManager.inst.towers[i].tr;
							SenceManager.inst.towers[i].ArmyTitleNew.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime);
							SenceManager.inst.towers[i].ArmyTitleNew.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime).AddSeconds((double)(UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].BuildTime * -1));
							SenceManager.inst.towers[i].ArmyTitleNew.SetUpdatingEum(SenceManager.inst.towers[i].index, SenceManager.inst.towers[i].trueLv);
						}
					}
					else if (SenceManager.inst.towers[i].BuilindingCDInfo == null)
					{
						SenceManager.inst.towers[i].BuilindingCDInfo = FuncUIManager.inst.ResourcePanelManage.AddChildByTime(SenceManager.inst.towers[i].tr).GetComponent<TimeTittleBtn>();
						SenceManager.inst.towers[i].BuilindingCDInfo.tar = SenceManager.inst.towers[i].tr;
						SenceManager.inst.towers[i].BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime);
						SenceManager.inst.towers[i].BuilindingCDInfo.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime).AddSeconds((double)(UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].BuildTime * -1));
						SenceManager.inst.towers[i].BuilindingCDInfo.SetUpdatingEum(1);
					}
				}
				else if (HeroInfo.GetInstance().BuildCD.Contains(SenceManager.inst.towers[i].id) && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType != 20)
				{
					if (SenceManager.inst.towers[i].BuilindingCDInfo == null)
					{
						SenceManager.inst.towers[i].BuilindingCDInfo = FuncUIManager.inst.ResourcePanelManage.AddChildByTime(SenceManager.inst.towers[i].tr).GetComponent<TimeTittleBtn>();
						SenceManager.inst.towers[i].BuilindingCDInfo.tar = SenceManager.inst.towers[i].tr;
						SenceManager.inst.towers[i].BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[SenceManager.inst.towers[i].id]);
						SenceManager.inst.towers[i].BuilindingCDInfo.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[SenceManager.inst.towers[i].id]).AddSeconds((double)(UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].BuildTime * -1));
						SenceManager.inst.towers[i].BuilindingCDInfo.SetUpdatingEum(1);
					}
				}
				else
				{
					GameObject gameObject = FuncUIManager.inst.ResourcePanelManage.AddBuildingDes(SenceManager.inst.towers[i].tr);
				}
			}
			foreach (KeyValuePair<int, T_Res> current in SenceManager.inst.reses)
			{
				if (HeroInfo.GetInstance().BuildCD.Contains((long)current.Key) && current.Value.BuilindingCDInfo == null)
				{
					current.Value.BuilindingCDInfo = FuncUIManager.inst.ResourcePanelManage.AddChildByTime(current.Value.tr).GetComponent<TimeTittleBtn>();
					current.Value.BuilindingCDInfo.tar = current.Value.tr;
					current.Value.BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[(long)current.Key]);
					current.Value.BuilindingCDInfo.beginTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[(long)current.Key]).AddSeconds((double)(UnitConst.GetInstance().buildingConst[current.Value.index].lvInfos[0].BuildTime * -1));
					current.Value.BuilindingCDInfo.SetUpdatingEum(0);
					current.Value.BuilindingCDInfo.CallBack = new Action(current.Value.SendResRemoveComplete);
				}
			}
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10091)
		{
			this.RefreshArmyUpdateTime();
			return;
		}
		if (opcodeCMD == 10021 || opcodeCMD == 10075)
		{
			this.RefreshTechUpdateTime();
			return;
		}
		if (opcodeCMD == 10007)
		{
			this.OnSetBuildCD();
			this.RefreshArmyFuncTime();
			return;
		}
		if (opcodeCMD == 10054)
		{
			this.GetMonthlyCardRewards();
		}
		if (opcodeCMD == 10006)
		{
			this.CollectResourceHeartBeart();
		}
		if ((opcodeCMD == 10116 || opcodeCMD == 10090) && SenceInfo.curMap.IsMyHome)
		{
			this.SetTankModel(true);
		}
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
			ClientMgr.GetNet().NetDataHandler.RMBChange -= new DataHandler.Data_Change(this.DataHandler_RMBChange);
		}
	}

	private void DataHandler_RMBChange(int opcodeCMD)
	{
		ResourcePanelManage.ChangeResDiamond();
	}

	public void showTipNum()
	{
	}

	public void RefreshArmyUpdateTime()
	{
		if (TimeManage.inst)
		{
			TimeManage.inst.ClearArmyUpdateTimeEvent();
		}
		if (UIManager.curState == SenceState.Home && NewbieGuidePanel.isEnemyAttck)
		{
			if (HeroInfo.GetInstance().PlayerArmy_LandDataCDTime.Count > 0)
			{
				for (int i = 0; i < HeroInfo.GetInstance().PlayerArmy_LandDataCDTime.Count; i++)
				{
					TimeManage.inst.AddArmyUpdateTimeEvent((int)HeroInfo.GetInstance().PlayerArmy_LandDataCDTime[i].key, TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerArmy_LandDataCDTime[i].value), new Action<int>(ResourcePanelManage.CSArmyUpdateEnd));
				}
			}
			if (HeroInfo.GetInstance().PlayerArmy_AirDataCDTime.Count > 0)
			{
				for (int j = 0; j < HeroInfo.GetInstance().PlayerArmy_AirDataCDTime.Count; j++)
				{
					TimeManage.inst.AddArmyUpdateTimeEvent((int)HeroInfo.GetInstance().PlayerArmy_AirDataCDTime[j].key, TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerArmy_AirDataCDTime[j].value), new Action<int>(ResourcePanelManage.CSArmyUpdateEnd));
				}
			}
		}
	}

	private static void CSArmyUpdateEnd(int updateingItem)
	{
		LogManage.LogError("CSArmyUpdateEnd" + updateingItem);
		ArmyHandler.CG_CSArmyLevelUpEnd(0, delegate(int[] ii)
		{
			AudioManage.inst.PlayAuido("upgrade", false);
			if (ArmyControlAndUpdatePanel.Inst)
			{
				ArmyControlAndUpdatePanel.Inst.ShowArmyUpdateInfo();
				if (ArmyControlAndUpdatePanel.Inst.AllSolider.ContainsKey(ii[0]) && ArmyControlAndUpdatePanel.Inst.AllSolider[ii[0]])
				{
					ArmyControlAndUpdatePanel.Inst.AllSolider[ii[0]].PlayUpdateEffect();
				}
			}
		}, new int[]
		{
			updateingItem
		});
	}

	public void RefreshTechUpdateTime()
	{
		if (UIManager.curState == SenceState.Home && NewbieGuidePanel.isEnemyAttck)
		{
			T_Tower t_Tower = SenceManager.inst.towers.FirstOrDefault((T_Tower a) => a.index == 11);
			bool flag = false;
			if (HeroInfo.GetInstance().PlayerTechnologyUpdatingTime > 0L)
			{
				flag = true;
				if (t_Tower != null)
				{
					t_Tower.HideLogo();
					if (this.timeBtn_TechUpdateTime == null)
					{
						this.timeBtn_TechUpdateTime = FuncUIManager.inst.ResourcePanelManage.AddChildByTime(t_Tower.tr).GetComponent<TimeTittleBtn>();
					}
					this.timeBtn_TechUpdateTime.tar = t_Tower.transform;
					this.timeBtn_TechUpdateTime.SetUpdatingEum(3);
					this.timeBtn_TechUpdateTime.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerTechnologyUpdatingTime);
					this.timeBtn_TechUpdateTime.CallBack = delegate
					{
						TechHandler.CG_CSTechUpEnd(HeroInfo.GetInstance().PlayerTechnologyUpdatingItemID, 0, null);
					};
				}
			}
			if (!flag && t_Tower != null)
			{
				t_Tower.DisplayLogo();
				if (this.timeBtn_TechUpdateTime != null && this.timeBtn_TechUpdateTime.tar == t_Tower.tr)
				{
					this.timeBtn_TechUpdateTime.CallBack = null;
					this.timeBtn_TechUpdateTime.tar = null;
				}
				if (this.skillCDTime != null && this.skillCDTime.tar == t_Tower.tr)
				{
					this.skillCDTime.CallBack = null;
					this.skillCDTime.tar = null;
				}
			}
		}
	}

	private void GetMonthlyCardRewards()
	{
		if (HeroInfo.GetInstance().PlayerTechnologyInfo.ContainsKey(10) && HeroInfo.GetInstance().PlayerTechnologyInfo[10] > 0 && (HeroInfo.GetInstance().TechGetRMB_Time == 0L || TimeTools.IsSmallByDay(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().TechGetRMB_Time), TimeTools.GetNowTimeSyncServerToDateTime())))
		{
			SenceManager.inst.MainBuilding.ShowYueKaModel();
		}
	}

	public void CollectResourceHeartBeart()
	{
		if (UIManager.curState != SenceState.Home)
		{
			return;
		}
		if (!this.go.activeInHierarchy)
		{
			return;
		}
		if (!this.isOpenCollectResourceHeartBeart)
		{
			return;
		}
		int count = SenceManager.inst.towers.Count;
		for (int i = 0; i < count; i++)
		{
			if (SenceManager.inst.towers[i])
			{
				SenceManager.inst.towers[i].CalcResShowOrNo_Heart();
			}
		}
	}

	public GameObject OnNoelectricityPow(Transform tar, int power)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.NoelectricityPow);
		int num = 1;
		for (int i = 0; i < this._3DUIPool.Count; i++)
		{
			if (this._3DUIPool[i] && this._3DUIPool[i].tar && this._3DUIPool[i].tar.Equals(tar))
			{
				this._3DUIPool[i].YoofectUp((float)num);
				num++;
			}
		}
		NoelectricityPowManager component = gameObject.GetComponent<NoelectricityPowManager>();
		component.YoofectUp((float)num);
		this._3DUIPool.Add(component);
		component.tar = tar;
		if (tar.GetComponent<T_Tower>())
		{
			tar.GetComponent<T_Tower>().nolectricityPow = component;
		}
		return gameObject;
	}

	public Body_Model AddChildByRes(GameObject tar, ResType resType)
	{
		Body_Model body_Model = null;
		switch (resType)
		{
		case ResType.金币:
		{
			body_Model = PoolManage.Ins.GetModelByBundleByName("Jb", tar.transform);
			body_Model.tr.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			body_Model.tr.localPosition = new Vector3(0f, 2f, 0f);
			body_Model.ga.AddComponent<TransRotate>();
			TransRotate component = body_Model.ga.GetComponent<TransRotate>();
			component.Bg = body_Model.tr;
			component.FuDu = 120f;
			component.xyz = TransRotate.RotateXYZ.Y;
			break;
		}
		case ResType.石油:
		{
			body_Model = PoolManage.Ins.GetModelByBundleByName("Sy", tar.transform);
			body_Model.tr.localPosition = new Vector3(0f, 2f, 0f);
			body_Model.tr.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			body_Model.ga.AddComponent<TransRotate>();
			TransRotate component2 = body_Model.ga.GetComponent<TransRotate>();
			component2.Bg = body_Model.tr;
			component2.FuDu = 120f;
			component2.xyz = TransRotate.RotateXYZ.Y;
			break;
		}
		case ResType.钢铁:
		{
			body_Model = PoolManage.Ins.GetModelByBundleByName("Gt", tar.transform);
			body_Model.tr.localPosition = new Vector3(0f, 2f, 0f);
			body_Model.tr.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			body_Model.ga.AddComponent<TransRotate>();
			TransRotate component3 = body_Model.ga.GetComponent<TransRotate>();
			component3.Bg = body_Model.tr;
			component3.FuDu = 120f;
			component3.xyz = TransRotate.RotateXYZ.Y;
			break;
		}
		case ResType.稀矿:
		{
			body_Model = PoolManage.Ins.GetModelByBundleByName("Xk", tar.transform);
			body_Model.tr.localPosition = new Vector3(0f, 2f, 0f);
			body_Model.tr.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			body_Model.ga.AddComponent<TransRotate>();
			TransRotate component4 = body_Model.ga.GetComponent<TransRotate>();
			component4.Bg = body_Model.tr;
			component4.FuDu = 120f;
			component4.xyz = TransRotate.RotateXYZ.Y;
			break;
		}
		case ResType.钻石:
		{
			body_Model = PoolManage.Ins.GetModelByBundleByName("Zs", tar.transform);
			body_Model.tr.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			body_Model.ga.AddComponent<TransRotate>();
			TransRotate component5 = body_Model.ga.GetComponent<TransRotate>();
			component5.Bg = body_Model.tr;
			component5.FuDu = 120f;
			component5.xyz = TransRotate.RotateXYZ.Y;
			break;
		}
		}
		return body_Model;
	}

	public GameObject AddBuildingDes(Transform tar)
	{
		int index = tar.GetComponent<T_Tower>().index;
		string des;
		switch (index)
		{
		case 91:
			des = "战斗机";
			goto IL_6D;
		case 92:
		case 93:
			IL_25:
			switch (index)
			{
			case 11:
				des = "科技与技能";
				goto IL_6D;
			case 13:
				des = "坦克";
				goto IL_6D;
			}
			return null;
		case 94:
			des = "特种兵";
			goto IL_6D;
		}
		goto IL_25;
		IL_6D:
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.BuildingDesPrefab);
		if (tar.GetComponent<T_Tower>())
		{
			if (tar.GetComponent<T_Tower>()._BuildingDes)
			{
				UnityEngine.Object.Destroy(tar.GetComponent<T_Tower>()._BuildingDes.gameObject);
			}
			tar.GetComponent<T_Tower>()._BuildingDes = gameObject.transform.GetComponent<BuildingDes>();
		}
		gameObject.transform.GetComponent<BuildingDes>().tar = tar;
		gameObject.transform.GetComponent<BuildingDes>().Des = des;
		return gameObject;
	}

	public GameObject AddExtraArmyDes(Transform tar, string des)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.BuildingDesPrefab);
		gameObject.transform.GetComponent<BuildingDes>().tar = tar;
		gameObject.transform.GetComponent<BuildingDes>().Des = des;
		return gameObject;
	}

	public GameObject AddChildByTime(Transform tar)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.timePrefab);
		int num = 1;
		for (int i = 0; i < this._3DUIPool.Count; i++)
		{
			if (this._3DUIPool[i] && this._3DUIPool[i].tar && this._3DUIPool[i].tar.Equals(tar))
			{
				this._3DUIPool[i].YoofectUp((float)num);
				num++;
			}
		}
		TimeTittleBtn component = gameObject.GetComponent<TimeTittleBtn>();
		component.YoofectUp((float)num);
		this._3DUIPool.Add(component);
		return gameObject;
	}

	public GameObject AddArmyTitleByTime(Transform tar)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.armyTitlePrefab);
		int num = 1;
		for (int i = 0; i < this._3DUIPool.Count; i++)
		{
			if (this._3DUIPool[i] && this._3DUIPool[i].tar && this._3DUIPool[i].tar.Equals(tar))
			{
				this._3DUIPool[i].YoofectUp((float)num);
				num++;
			}
		}
		ArmyTitleShow component = gameObject.GetComponent<ArmyTitleShow>();
		component.YoofectUp((float)num);
		this._3DUIPool.Add(component);
		return gameObject;
	}

	public GameObject AddChildByDescriptInfo(Transform tar, string helpInfo)
	{
		DescripUIInfo descripUIInfo = null;
		for (int i = 0; i < this.DescripUIInfoPool.Count; i++)
		{
			if (this.DescripUIInfoPool[i] && this.DescripUIInfoPool[i].tar && this.DescripUIInfoPool[i].tar.Equals(tar))
			{
				descripUIInfo = this.DescripUIInfoPool[i];
				break;
			}
		}
		if (descripUIInfo == null)
		{
			descripUIInfo = NGUITools.AddChild(base.gameObject, this.describtionUIInfo).GetComponent<DescripUIInfo>();
			this.DescripUIInfoPool.Add(descripUIInfo);
		}
		if (tar != null)
		{
			descripUIInfo.tar = tar.transform;
		}
		descripUIInfo.tipSprite.spriteName = helpInfo;
		return descripUIInfo.ga;
	}

	public void CollectResource(T_Tower tar, Action func)
	{
		if (func != null)
		{
			func();
		}
		if (UnitConst.GetInstance().buildingConst[tar.index].secType == 1)
		{
			TechHandler.CG_CSTechDiamondPrize();
			this.OnGetdiamond();
			return;
		}
		CollectResourcesHandler.TakeResource.buildingId = tar.id;
		CollectResourcesHandler.TakeResource.resId = (int)UnitConst.GetInstance().buildingConst[tar.index].outputType;
		CollectResourcesHandler.TakeResource.takeTime = TimeTools.GetNowTimeSyncServerToLong();
		CollectResourcesHandler.TakeResource.takeNum = Mathf.RoundToInt((float)((double)tar.ResSpeed_ByStep_Ele_Tech_Vip * (TimeTools.GetNowTimeSyncServerToDateTime() - SenceInfo.curMap.ResourceBuildingList[tar.id].takeTime).TotalHours)) + SenceInfo.curMap.ResourceBuildingList[tar.id].protductNum;
		if (UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos.Count == 0)
		{
			if ((float)CollectResourcesHandler.TakeResource.takeNum >= tar.ResMaxLimit_ProdByTech)
			{
				CollectResourcesHandler.TakeResource.takeNum = (int)tar.ResMaxLimit_ProdByTech;
			}
		}
		else if ((float)CollectResourcesHandler.TakeResource.takeNum >= tar.ResMaxLimit_ProdByTech)
		{
			CollectResourcesHandler.TakeResource.takeNum = (int)tar.ResMaxLimit_ProdByTech;
		}
		switch (CollectResourcesHandler.TakeResource.resId)
		{
		case 1:
			if (HeroInfo.GetInstance().playerRes.resCoin >= HeroInfo.GetInstance().playerRes.maxCoin)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金库已满，请先升级金库建筑等级", "ResIsland") + "！", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		case 2:
			if (HeroInfo.GetInstance().playerRes.resOil >= HeroInfo.GetInstance().playerRes.maxOil)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("储油罐已满，请先升级储油罐建筑等级", "ResIsland") + "！", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		case 3:
			if (HeroInfo.GetInstance().playerRes.resSteel >= HeroInfo.GetInstance().playerRes.maxSteel)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("钢铁仓库已满，请先升级钢铁仓库等级", "ResIsland") + "！", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		case 4:
			if (HeroInfo.GetInstance().playerRes.resRareEarth >= HeroInfo.GetInstance().playerRes.maxRareEarth)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("稀矿仓库已满，请先升级稀矿仓库建筑等级", "ResIsland") + "！", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			break;
		}
		int num = 0;
		switch (CollectResourcesHandler.TakeResource.resId)
		{
		case 1:
			num = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resCoin <= HeroInfo.GetInstance().playerRes.maxCoin) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxCoin - HeroInfo.GetInstance().playerRes.resCoin));
			CollectResourcesHandler.TakeResource.takeNum = num;
			CoroutineInstance.DoJob(ResFly2.CreateRes(tar.tr, ResType.金币, num, new Action(CollectResourcesHandler.CG_CollectResources), null));
			break;
		case 2:
			num = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resOil <= HeroInfo.GetInstance().playerRes.maxOil) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxOil - HeroInfo.GetInstance().playerRes.resOil));
			CollectResourcesHandler.TakeResource.takeNum = num;
			CoroutineInstance.DoJob(ResFly2.CreateRes(tar.tr, ResType.石油, num, new Action(CollectResourcesHandler.CG_CollectResources), null));
			break;
		case 3:
			num = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resSteel <= HeroInfo.GetInstance().playerRes.maxSteel) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxSteel - HeroInfo.GetInstance().playerRes.resSteel));
			CollectResourcesHandler.TakeResource.takeNum = num;
			CoroutineInstance.DoJob(ResFly2.CreateRes(tar.tr, ResType.钢铁, num, new Action(CollectResourcesHandler.CG_CollectResources), null));
			break;
		case 4:
			num = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resRareEarth <= HeroInfo.GetInstance().playerRes.maxRareEarth) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxRareEarth - HeroInfo.GetInstance().playerRes.resRareEarth));
			CollectResourcesHandler.TakeResource.takeNum = num;
			CoroutineInstance.DoJob(ResFly2.CreateRes(tar.tr, ResType.稀矿, num, new Action(CollectResourcesHandler.CG_CollectResources), null));
			break;
		}
		LogManage.LogError(string.Format("建筑收集资源时间:{0}  当前时间{1}  生产速率{2}  收取数目{3}  时间差{4} 缓存{5} ", new object[]
		{
			TimeTools.GetTimeLongByDateTime(SenceInfo.curMap.ResourceBuildingList[tar.id].takeTime),
			CollectResourcesHandler.TakeResource.takeTime,
			tar.ResSpeed_ByStep_Ele_Tech_Vip,
			num,
			(long)(TimeTools.ConvertLongDateTime(CollectResourcesHandler.TakeResource.takeTime) - SenceInfo.curMap.ResourceBuildingList[tar.id].takeTime).TotalMilliseconds,
			SenceInfo.curMap.ResourceBuildingList[tar.id].protductNum
		}));
		SenceInfo.curMap.ResourceBuildingList[tar.id].takeTime = TimeTools.GetNowTimeSyncServerToDateTime();
		if (this.resPool.SingleOrDefault((ResouceTittleBtn i) => i != null && i.gameObject.activeSelf && i.bulidTar == tar) != null)
		{
			this.resPool.SingleOrDefault((ResouceTittleBtn i) => i != null && i.gameObject.activeSelf && i.bulidTar == tar).GetComponent<ResouceTittleBtn>().tar = null;
		}
	}

	public void OnGetdiamond()
	{
		CoroutineInstance.DoJob(ResFly2.CreateRes(SenceManager.inst.MainBuilding.tr, ResType.钻石, 10, new Action(ResourcePanelManage.ChangeResDiamond), null));
	}

	public static void ChangeResDiamond()
	{
		MainUIPanelManage._instance.rmbLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.RMBCoin, 2f, null);
	}

	public static void ChangeResSteel()
	{
		FuncUIManager.inst.MainUIPanelManage.resSteelLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resSteel, 2f, new Action(CollectResourcesHandler.CG_CollectResources));
	}

	public static void ChaneResRareEarth()
	{
		FuncUIManager.inst.MainUIPanelManage.resRareEarthLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resRareEarth, 2f, new Action(CollectResourcesHandler.CG_CollectResources));
	}

	public static void ChangeResOil()
	{
		FuncUIManager.inst.MainUIPanelManage.resOilLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resOil, 2f, new Action(CollectResourcesHandler.CG_CollectResources));
	}

	public static void ChangeResCoin()
	{
		FuncUIManager.inst.MainUIPanelManage.resCoinLabel.GetComponent<ResLabel>().ChangNumText(HeroInfo.GetInstance().playerRes.resCoin, 2f, new Action(CollectResourcesHandler.CG_CollectResources));
	}

	public void SetTankModel(bool IsMyHome)
	{
		this.Soliders.Clear();
		if (IsMyHome && UIManager.curState != SenceState.WatchVideo)
		{
			foreach (KeyValuePair<long, armyInfoInBuilding> current in HeroInfo.GetInstance().AllArmyInfo)
			{
				for (int i = 0; i < current.Value.armyFunced.Count; i++)
				{
					if (!UnitConst.GetInstance().soldierConst[(int)current.Value.armyFunced[i].key].isCanFly)
					{
						if (this.Soliders.ContainsKey((int)current.Value.armyFunced[i].key))
						{
							Dictionary<int, int> soliders;
							Dictionary<int, int> expr_9B = soliders = this.Soliders;
							int num;
							int expr_B6 = num = (int)current.Value.armyFunced[i].key;
							num = soliders[num];
							expr_9B[expr_B6] = num + (int)current.Value.armyFunced[i].value;
						}
						else
						{
							this.Soliders.Add((int)current.Value.armyFunced[i].key, (int)current.Value.armyFunced[i].value);
						}
					}
				}
			}
		}
		else
		{
			foreach (KeyValuePair<long, armyInfoInBuilding> current2 in SenceInfo.curMap.mapSoldierInfo)
			{
				for (int j = 0; j < current2.Value.armyFunced.Count; j++)
				{
					if (SenceInfo.curMap.GetArmysDef().Contains((int)current2.Value.armyFunced[j].key))
					{
						if (this.Soliders.ContainsKey((int)current2.Value.armyFunced[j].key))
						{
							Dictionary<int, int> soliders2;
							Dictionary<int, int> expr_1E3 = soliders2 = this.Soliders;
							int num;
							int expr_1FF = num = (int)current2.Value.armyFunced[j].key;
							num = soliders2[num];
							expr_1E3[expr_1FF] = num + (int)current2.Value.armyFunced[j].value;
						}
						else
						{
							this.Soliders.Add((int)current2.Value.armyFunced[j].key, (int)current2.Value.armyFunced[j].value);
						}
					}
				}
			}
		}
		List<T_Tower> list = (from a in SenceManager.inst.towers
		orderby a.id
		select a).ToList<T_Tower>();
		for (int k = 0; k < list.Count; k++)
		{
			if (UnitConst.GetInstance().buildingConst[list[k].index].secType == 6 && !this.Army_Towes.Contains(list[k]))
			{
				this.Army_Towes.Add(list[k]);
			}
		}
		List<int> list2 = new List<int>();
		DicForU.GetKeys<int, int>(this.Soliders, list2);
		for (int l = 0; l < this.Army_Towes.Count; l++)
		{
			for (int m = 0; m < this.Army_Towes[l].AllParadeTanks_Pos.Length; m++)
			{
				if (this.Army_Towes[l].AllParadeTanks_Pos[m] == null)
				{
					int n = 0;
					while (n < list2.Count)
					{
						if (this.Soliders[list2[n]] > 0)
						{
							if (IsMyHome && UIManager.curState != SenceState.WatchVideo)
							{
								Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[list2[n]].bodyId, null);
								if (modelByBundleByName)
								{
									ParadeTank compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<ParadeTank>(modelByBundleByName.ga);
									compentIfNoAddOne.tr.localScale = Vector3.one;
									compentIfNoAddOne.SetInfo(this.Army_Towes[l], m + 1);
									compentIfNoAddOne.ga.name = list2[n].ToString();
									Dictionary<int, int> soliders3;
									Dictionary<int, int> expr_450 = soliders3 = this.Soliders;
									int num2;
									int expr_45C = num2 = list2[n];
									num2 = soliders3[num2];
									expr_450[expr_45C] = num2 - 1;
									this.Army_Towes[l].AllParadeTanks_Pos[m] = compentIfNoAddOne.ga;
								}
								break;
							}
							GameObject gameObject = T_TowerTankManager.inst.CreateTowerTank_NoIE(this.GetParadeTankPos(this.Army_Towes[l], m + 1), list2[n], SenceInfo.curMap.mapArmyData[list2[n]].level, 0f, T_TowerTank.TowerTankAttType.StandBy, 0);
							this.Army_Towes[l].AllParadeTanks_Pos[m] = gameObject;
							gameObject.name = list2[n].ToString();
							Dictionary<int, int> soliders4;
							Dictionary<int, int> expr_51A = soliders4 = this.Soliders;
							int num3;
							int expr_526 = num3 = list2[n];
							num3 = soliders4[num3];
							expr_51A[expr_526] = num3 - 1;
							break;
						}
						else
						{
							n++;
						}
					}
				}
				else
				{
					int num4 = int.Parse(this.Army_Towes[l].AllParadeTanks_Pos[m].name);
					if (!this.Soliders.ContainsKey(num4) || this.Soliders[num4] <= 0)
					{
						UnityEngine.Object.Destroy(this.Army_Towes[l].AllParadeTanks_Pos[m]);
					}
					else
					{
						Dictionary<int, int> soliders5;
						Dictionary<int, int> expr_5C6 = soliders5 = this.Soliders;
						int num3;
						int expr_5CB = num3 = num4;
						num3 = soliders5[num3];
						expr_5C6[expr_5CB] = num3 - 1;
					}
				}
			}
		}
	}

	private Vector3 GetParadeTankPos(T_Tower Home, int weizhi)
	{
		Vector3 result = Home.tr.position;
		switch (weizhi)
		{
		case 1:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 2:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 3:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f);
			break;
		case 4:
			result = Home.tr.position + new Vector3(0f, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 5:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, 0f);
			break;
		case 6:
			result = Home.tr.position + new Vector3(-(UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f), 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 7:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, -(UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f));
			break;
		case 8:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 9:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 10:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f);
			break;
		case 11:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 12:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, 0f);
			break;
		case 13:
			result = Home.tr.position + new Vector3(0f, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 14:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f);
			break;
		case 15:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize * 0.5f, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		case 16:
			result = Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		default:
			result = Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[Home.index].bodySize);
			break;
		}
		return result;
	}
}
