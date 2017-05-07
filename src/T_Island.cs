using System;
using System.Collections.Generic;
using UnityEngine;

public class T_Island : IMonoBehaviour
{
	public T_WMap wmap;

	public int mapIdx;

	private IslandType mapType;

	[SerializeField]
	private OwnerType ownerType;

	public WMapTipsType uiType;

	public IconType iconType;

	public string ownerId;

	public string ownerName;

	public string ownerIcon;

	public string ownerLv = "9";

	public Dictionary<ResType, int> reward;

	public bool replace;

	public long islandId;

	public Battle battleItem;

	public int EliteBattleStar;

	public int commandLV;

	public long worldMapID;

	private GameObject icon;

	public GameObject IslandIcon;

	private Body_Model Body;

	private Body_Model Box;

	private TitleIsland title;

	public virtual bool IsOpen
	{
		get
		{
			return true;
		}
	}

	public IslandType MapType
	{
		get
		{
			return this.mapType;
		}
		protected set
		{
			this.mapType = value;
		}
	}

	public OwnerType OwnerType
	{
		get
		{
			return this.ownerType;
		}
		protected set
		{
			this.ownerType = value;
		}
	}

	public void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10078)
		{
			this.ResetInfo();
		}
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	public void OnDestroy()
	{
		if (this.IslandIcon)
		{
			UnityEngine.Object.Destroy(this.IslandIcon);
		}
	}

	private void Start()
	{
	}

	public void SetInfo(PlayerWMapData data, T_WMap worldMap)
	{
		this.mapIdx = data.idx;
		this.commandLV = data.commandlv;
		this.worldMapID = data.worldMapId;
		this.ownerType = (OwnerType)data.ownerType;
		if (this.ownerType == OwnerType.user)
		{
			if (T_WMap.IdxGetMapType(this.mapIdx) == IslandType.npc || T_WMap.IdxGetMapType(this.mapIdx) == IslandType.Activity || T_WMap.IdxGetMapType(this.mapIdx) == IslandType.otherPlayer)
			{
				this.mapType = IslandType.Coin;
			}
			else if (this.mapIdx == 286)
			{
				this.mapType = IslandType.myHome;
			}
			else
			{
				this.mapType = T_WMap.IdxGetMapType(this.mapIdx);
			}
		}
		else
		{
			this.mapType = T_WMap.IdxGetMapType(this.mapIdx);
		}
		this.ownerId = data.ownerId;
		this.ownerName = data.ownerName;
		this.ownerIcon = data.ownerIcon;
		this.reward = data.reward;
		this.replace = data.replace;
		this.islandId = data.islandId;
		this.ownerLv = data.ownerLv;
		this.wmap = worldMap;
		if (!string.IsNullOrEmpty(this.ownerId) && UnitConst.GetInstance().AllNpc.ContainsKey(int.Parse(this.ownerId)))
		{
			this.EliteBattleStar = UnitConst.GetInstance().AllNpc[int.Parse(this.ownerId)].Star;
			SenceManager.inst.ElliteBallteBoxMax = Mathf.Max(SenceManager.inst.ElliteBallteBoxMax, this.EliteBattleStar);
		}
		this.CreatBody();
		this.CreatIcon();
		this.IslandIcon = WorldMapIslandsToUI.instance.CreateIsland(this);
	}

	public void SetInfo(Battle battle, T_WMap worldMap)
	{
		this.mapType = IslandType.battle;
		this.battleItem = battle;
		this.wmap = worldMap;
		this.CreatBody();
		this.CreatIcon();
		this.IslandIcon = WorldMapIslandsToUI.instance.CreateIsland(this);
	}

	public void ResetInfo()
	{
		IslandType islandType = this.mapType;
		if (islandType == IslandType.battle)
		{
			if (this.battleItem.battleBox > 0)
			{
				if (this.Body)
				{
					this.Body.ga.SetActive(false);
				}
				if (this.Box)
				{
					this.Box.ga.SetActive(true);
				}
				else
				{
					this.Box = PoolManage.Ins.GetModelByBundleByName("case", this.tr);
					this.Box.tr.localScale = Vector3.one * 0.25f;
					Transform transform = this.Box.tr.FindChild("case_w");
					Transform transform2 = this.Box.tr.FindChild("case_y");
					Transform transform3 = this.Box.tr.FindChild("case_g");
					transform3.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 1);
					transform2.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 3);
					transform.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 2);
				}
				this.IslandIcon.GetComponent<UISprite>().spriteName = "战役通关";
			}
			else
			{
				this.IslandIcon.GetComponent<UISprite>().spriteName = "攻占";
				if (this.Body)
				{
					this.Body.ga.SetActive(true);
				}
				if (this.Box)
				{
					this.Box.DesInsInPool();
				}
			}
			this.uiType = WMapTipsType.battle;
			this.iconType = IconType.battle;
			if (this.title)
			{
				this.title.ResetInfo();
			}
		}
	}

	protected void CreatBody()
	{
		if (this.EliteBattleStar != 0 || (this.ownerType == OwnerType.otherPlayer && (this.mapType == IslandType.otherPlayer || this.mapType == IslandType.npc)))
		{
			this.Body = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().IslandTypeToModel[2], this.tr);
			this.Body.tr.localScale = UnitConst.GetInstance().IslandTypeToModelScale[2];
		}
		else
		{
			this.Body = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().IslandTypeToModel[(int)this.mapType], this.tr);
			this.Body.tr.localScale = UnitConst.GetInstance().IslandTypeToModelScale[(int)this.mapType];
		}
		if (this.Body.BlueModel)
		{
			this.Body.BlueModel.gameObject.SetActive(this.ownerType == OwnerType.user);
		}
		if (this.Body.RedModel)
		{
			this.Body.RedModel.gameObject.SetActive(this.ownerType != OwnerType.user);
		}
		if (this.Body.Red_DModel)
		{
			this.Body.Red_DModel.gameObject.SetActive(false);
		}
		if (this.Body.Blue_DModel)
		{
			this.Body.Blue_DModel.gameObject.SetActive(false);
		}
		if (this.mapType != IslandType.battle)
		{
			ParticleSystem[] componentsInChildren = this.Body.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem particleSystem = componentsInChildren[i];
				particleSystem.startSize = this.Body.tr.localScale.x;
			}
		}
		this.Body.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().IslandTypeToModelAngel[(int)this.mapType]);
		switch (this.mapType)
		{
		case IslandType.myHome:
			this.uiType = WMapTipsType.myHome;
			this.iconType = IconType.myBase;
			break;
		case IslandType.otherPlayer:
		case IslandType.npc:
		case IslandType.Coin:
			if (this.ownerType == OwnerType.user)
			{
				this.uiType = WMapTipsType.myCoin;
			}
			else
			{
				this.uiType = WMapTipsType.enemy;
				if (this.ownerType == OwnerType.otherPlayer)
				{
					this.iconType = IconType.enemyBase;
				}
				else
				{
					this.iconType = IconType.npcBase;
				}
			}
			if (this.ownerType == OwnerType.npc)
			{
				this.iconType = IconType.npcJiqir;
			}
			break;
		case IslandType.oil:
			if (this.ownerType == OwnerType.user)
			{
				this.uiType = WMapTipsType.myRes;
				this.iconType = IconType.myOil;
			}
			else if (this.ownerType == OwnerType.otherPlayer)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.enemyOil;
			}
			else if (this.ownerType == OwnerType.npc)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.npcOil;
			}
			break;
		case IslandType.steel:
			if (this.ownerType == OwnerType.user)
			{
				this.uiType = WMapTipsType.myRes;
				this.iconType = IconType.mySteel;
			}
			else if (this.ownerType == OwnerType.otherPlayer)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.enemySteel;
			}
			else if (this.ownerType == OwnerType.npc)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.npcSteel;
			}
			break;
		case IslandType.rareEarth:
			if (this.ownerType == OwnerType.user)
			{
				this.uiType = WMapTipsType.myRes;
				this.iconType = IconType.myRareEarth;
			}
			else if (this.ownerType == OwnerType.otherPlayer)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.enemyRareEarth;
			}
			else if (this.ownerType == OwnerType.npc)
			{
				this.uiType = WMapTipsType.enemyRes;
				this.iconType = IconType.npcRareEarth;
			}
			break;
		case IslandType.battle:
			if (this.battleItem.battleBox > 0)
			{
				if (this.Body)
				{
					this.Body.ga.SetActive(false);
				}
				if (this.Box)
				{
					this.Box.ga.SetActive(true);
				}
				else
				{
					this.Box = PoolManage.Ins.GetModelByBundleByName("case", this.tr);
					this.Box.tr.localScale = Vector3.one * 0.25f;
					Transform transform = this.Box.tr.FindChild("case_w");
					Transform transform2 = this.Box.tr.FindChild("case_y");
					Transform transform3 = this.Box.tr.FindChild("case_g");
					transform3.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 1);
					transform2.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 3);
					transform.gameObject.SetActive(BattleFieldBox.BattleBox_PlannerData[this.battleItem.battleBox].quility == 2);
				}
			}
			else
			{
				if (this.Body)
				{
					this.Body.ga.SetActive(true);
				}
				if (this.Box)
				{
					this.Box.DesInsInPool();
				}
			}
			this.uiType = WMapTipsType.battle;
			this.iconType = IconType.battle;
			break;
		}
	}

	private void CreatIcon()
	{
		if (this.uiType != WMapTipsType.myCoin && this.uiType != WMapTipsType.battle && this.uiType != WMapTipsType.myRes)
		{
			TipsManager.inst.CreatChild(this);
		}
		if (this.uiType == WMapTipsType.battle)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(TipsManager.inst.islandIcon, Vector3.zero, Quaternion.identity) as GameObject;
			gameObject.transform.parent = TipsManager.inst.tipsChild;
			this.title = gameObject.GetComponent<TitleIsland>();
			this.title.island = base.GetComponent<T_Island>();
			this.title.ResetInfo();
		}
	}

	public virtual void MouseUp()
	{
		if (this.mapType == IslandType.battle)
		{
			T_WMap.inst.curIsland_Sel = this;
			if (this.battleItem.battleBox > 0)
			{
				BattleBoxPanel.ShowBattleBox(this.battleItem.battleBox, this);
			}
			else
			{
				if (this.battleItem.preId != 0 && UnitConst.GetInstance().BattleConst[this.battleItem.preId].battleBox <= 0 && !UnitConst.GetInstance().BattleConst[this.battleItem.preId].isCanSweep)
				{
					HUDTextTool.inst.SetText(string.Format("{0}{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().BattleConst[this.battleItem.preId].name, "Battle"), LanguageManage.GetTextByKey("前置战役未通过", "Battle")), HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				T_WMap.inst.curIsland_Sel = this;
				TipsManager.inst.CloseAllTips();
				TipsManager.inst.OpenTips(this);
			}
			CameraZhedang.inst.uiInUseBox.SetActive(true);
			HUDTextTool.inst.NextLuaCall("战役点击", new object[0]);
			return;
		}
		T_WMap.inst.curIsland_Sel = this;
		TipsManager.inst.CloseAllTips();
		TipsManager.inst.OpenTips(this);
		HUDTextTool.inst.NextLuaCall("岛屿点击", new object[0]);
	}
}
