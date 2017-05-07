using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
	public enum CharacterSelectStates
	{
		Idle = 1,
		Selected
	}

	public Enum_RoleType roleType;

	public Enum_ModelColor modelClore;

	public BoxCollider bodyForAttack;

	public int index = 1;

	public int lv = 1;

	public int trueLv = 1;

	public int size;

	public float height;

	public int star;

	public int strengthLevel;

	public long id;

	private int maxLife = 1000;

	public bool IsDie;

	[SerializeField]
	private float curLife = 1000f;

	public Ani_CharacterControler AnimationControler;

	public List<Character> Targetes = new List<Character>();

	public BaseFightInfo CharacterBaseFightInfo;

	public Enum_CharaType charaType;

	public List<Transform> shootPList = new List<Transform>();

	public Transform curShootP;

	public Transform tr;

	public GameObject ga;

	public Transform head;

	public Transform muzzle;

	public Transform defaultPos;

	public List<Action> DieCallBack = new List<Action>();

	private SenceManager.ElectricityEnum mapElectricity;

	public Body_Model ModelBody;

	private string bodyName = string.Empty;

	protected float tmpGetNearestChar;

	public Character.CharacterSelectStates characterSelectStates = Character.CharacterSelectStates.Idle;

	public int MaxLife
	{
		get
		{
			return this.maxLife;
		}
		protected set
		{
			this.maxLife = value;
		}
	}

	public float CurLife
	{
		get
		{
			return this.curLife;
		}
		protected set
		{
			if (value >= (float)this.MaxLife)
			{
				this.curLife = (float)this.MaxLife;
			}
			else
			{
				this.curLife = value;
			}
		}
	}

	public Character Target
	{
		get
		{
			return this.GetTarget();
		}
	}

	public T_Tower Tower
	{
		get
		{
			if (this.roleType == Enum_RoleType.tower)
			{
				return this as T_Tower;
			}
			return null;
		}
	}

	public T_TankAbstract Tank
	{
		get
		{
			if (this.roleType == Enum_RoleType.tank)
			{
				return this as T_TankAbstract;
			}
			return null;
		}
	}

	public SenceManager.ElectricityEnum MapElectricity
	{
		get
		{
			return this.mapElectricity;
		}
		set
		{
			if (value != SenceManager.ElectricityEnum.严重不足 && value != SenceManager.ElectricityEnum.电力瘫痪)
			{
				if (this.AnimationControler != null)
				{
					for (int i = 0; i < this.AnimationControler.AllAnimation.Count; i++)
					{
						if (this.AnimationControler.AllAnimation[i])
						{
							this.AnimationControler.AllAnimation[i].enabled = true;
						}
					}
					for (int j = 0; j < this.AnimationControler.AllUVAnimation.Length; j++)
					{
						if (this.AnimationControler.AllUVAnimation[j])
						{
							this.AnimationControler.AllUVAnimation[j].enabled = true;
						}
					}
					for (int k = 0; k < this.AnimationControler.AllParticleSystem.Length; k++)
					{
						if (this.AnimationControler.AllParticleSystem[k])
						{
							this.AnimationControler.AllParticleSystem[k].Play();
						}
					}
				}
			}
			else if (HeroInfo.GetInstance().PlayerElectricPowerPlantLV > 0 && this.AnimationControler != null)
			{
				for (int l = 0; l < this.AnimationControler.AllAnimation.Count; l++)
				{
					if (this.AnimationControler.AllAnimation[l])
					{
						this.AnimationControler.AllAnimation[l].enabled = false;
					}
				}
				for (int m = 0; m < this.AnimationControler.AllUVAnimation.Length; m++)
				{
					if (this.AnimationControler.AllUVAnimation[m])
					{
						this.AnimationControler.AllUVAnimation[m].enabled = false;
					}
				}
				for (int n = 0; n < this.AnimationControler.AllParticleSystem.Length; n++)
				{
					if (this.AnimationControler.AllParticleSystem[n])
					{
						this.AnimationControler.AllParticleSystem[n].Pause();
					}
				}
			}
			this.mapElectricity = value;
		}
	}

	public void MaxLifeChange(int change)
	{
		float num = this.CurLife / (float)this.MaxLife;
		this.MaxLife = change;
		this.CurLife = (float)((int)((float)this.MaxLife * num));
	}

	public abstract Vector3 GetVPos(T_TankAbstract tank);

	public bool IsCanShootByCharType(Character t_Ch)
	{
		if (FightHundler.FightEnd && UIManager.curState != SenceState.WatchVideo)
		{
			return false;
		}
		Enum_CharaType enum_CharaType = this.charaType;
		if (enum_CharaType != Enum_CharaType.attacker)
		{
			if (enum_CharaType != Enum_CharaType.defender)
			{
				return false;
			}
			if (t_Ch.charaType == Enum_CharaType.attacker)
			{
				return true;
			}
		}
		else if (t_Ch.charaType == Enum_CharaType.defender)
		{
			return true;
		}
		return false;
	}

	public bool IsCanShootByCharFlak(Character t_Ch)
	{
		if (this.Tower)
		{
			if (UnitConst.GetInstance().buildingConst[this.index].flak == 1)
			{
				if (t_Ch.Tank && !UnitConst.GetInstance().soldierConst[t_Ch.Tank.index].isCanFly)
				{
					return true;
				}
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].flak == 3)
			{
				if (t_Ch.Tank && UnitConst.GetInstance().soldierConst[t_Ch.Tank.index].isCanFly)
				{
					return true;
				}
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].flak == 2 && t_Ch.Tank)
			{
				return true;
			}
		}
		if (this.Tank)
		{
			if (t_Ch.Tank)
			{
				return !UnitConst.GetInstance().soldierConst[t_Ch.Tank.index].isCanFly;
			}
			if (t_Ch.Tower)
			{
				return true;
			}
		}
		return false;
	}

	public void Destory()
	{
		if (this.ModelBody)
		{
			this.ModelBody.DesInsInPool();
		}
		for (int i = 0; i < this.DieCallBack.Count; i++)
		{
			if (this.DieCallBack[i] != null)
			{
				this.DieCallBack[i]();
			}
		}
		if (this.Tower)
		{
			this.Tower.UpdateGraphs(false);
			if (UnitConst.GetInstance().buildingConst[this.Tower.index].resType < 3)
			{
				HUDTextTool.inst.Powerhouse();
			}
		}
		if (UIManager.curState == SenceState.WatchVideo)
		{
			UnityEngine.Object.Destroy(this.ga);
			return;
		}
		if (this.Tank)
		{
			if (NewbieGuidePanel.isEnemyAttck && this.Tank.charaType == Enum_CharaType.attacker)
			{
				SenceManager.inst.Tanks_Attack.Remove(this.Tank);
				if (FightPanelManager.inst != null && !FightPanelManager.inst.IsCanBattle() && SenceManager.inst.Tanks_Attack.Count == 0)
				{
					SenceManager.inst.settType = SettlementType.failure;
					FightHundler.CG_FinishFight();
				}
			}
			if (FightPanelManager.inst)
			{
				FightPanelManager.inst.RreshTankUI(this.Tank);
			}
		}
		UnityEngine.Object.Destroy(this.ga);
	}

	public void DesInPool()
	{
		this.Destory();
	}

	public virtual void Awake()
	{
		this.tr = base.transform;
		this.ga = base.gameObject;
	}

	public void GetHeadOrMuzzle()
	{
		if (this.ModelBody != null)
		{
			if (this.modelClore == Enum_ModelColor.Blue)
			{
				if (this.ModelBody.BlueModel)
				{
					HeadTr componentInChildren = this.ModelBody.BlueModel.GetComponentInChildren<HeadTr>();
					if (componentInChildren)
					{
						this.head = componentInChildren.transform;
					}
					else
					{
						this.head = null;
					}
					Muzzle componentInChildren2 = this.ModelBody.BlueModel.GetComponentInChildren<Muzzle>();
					if (componentInChildren2)
					{
						this.muzzle = componentInChildren2.transform;
					}
					else
					{
						this.muzzle = null;
					}
				}
				else
				{
					this.head = null;
					this.muzzle = null;
				}
			}
			else if (this.modelClore == Enum_ModelColor.Red)
			{
				if (this.ModelBody.RedModel)
				{
					HeadTr componentInChildren3 = this.ModelBody.RedModel.GetComponentInChildren<HeadTr>();
					if (componentInChildren3)
					{
						this.head = componentInChildren3.transform;
					}
					else
					{
						this.head = null;
					}
					Muzzle componentInChildren4 = this.ModelBody.RedModel.GetComponentInChildren<Muzzle>();
					if (componentInChildren4)
					{
						this.muzzle = componentInChildren4.transform;
					}
					else
					{
						this.muzzle = null;
					}
				}
				else
				{
					this.head = null;
					this.muzzle = null;
				}
			}
			this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
			if (this.AnimationControler != null)
			{
				this.AnimationControler.AllAnimation.Clear();
				this.AnimationControler.AllAnimation.AddRange(this.ModelBody.ga.GetComponentsInChildren<Animation>());
			}
		}
	}

	public void CreateBody(string _name)
	{
		if (this.bodyName.Equals(_name))
		{
			return;
		}
		this.bodyName = _name;
		if (this.ModelBody != null)
		{
			UnityEngine.Object.Destroy(this.ModelBody.ga);
		}
		if (this.roleType == Enum_RoleType.tower)
		{
			T_Tower t_Tower = this as T_Tower;
			if (t_Tower.index != 12)
			{
				this.ModelBody = PoolManage.Ins.GetModelByBundleByName(_name, this.tr);
			}
			if (this.ModelBody)
			{
				this.ModelBody.tr.localScale = Vector3.one;
			}
			if (t_Tower.index == 90)
			{
				SenceManager.inst.CreateMuraille(t_Tower);
			}
			if (SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.电力不足 || SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.电力瘫痪 || SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.严重不足)
			{
				t_Tower.SetColorBlack(true);
			}
		}
		else
		{
			this.ModelBody = PoolManage.Ins.GetModelByBundleByName(_name, this.tr);
			if (this.ModelBody)
			{
				this.ModelBody.tr.localScale = Vector3.one;
			}
		}
		this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
	}

	public Vector3 GetShootPos(Character tar)
	{
		if (!(tar != null))
		{
			return Vector3.zero;
		}
		if (this.roleType == Enum_RoleType.tank && tar != null)
		{
			return new Vector3(tar.tr.position.x, tar.tr.position.y, tar.tr.position.z);
		}
		if (this.roleType != Enum_RoleType.tower || !(this.Target != null))
		{
			return tar.tr.position;
		}
		switch (UnitConst.GetInstance().buildingConst[(this as T_Tower).index].attackPoint)
		{
		case Enum_AttackPointType.head:
			if (tar.roleType == Enum_RoleType.tank)
			{
				return tar.tr.position + new Vector3(0f, UnitConst.GetInstance().soldierConst[tar.index].hight, 0f);
			}
			return tar.tr.position;
		case Enum_AttackPointType.body:
			if (tar.roleType == Enum_RoleType.tank)
			{
				return tar.tr.position + new Vector3(0f, UnitConst.GetInstance().soldierConst[tar.index].hight / 2f, 0f);
			}
			return tar.tr.position;
		case Enum_AttackPointType.foot:
			return tar.tr.position;
		default:
			return tar.tr.position;
		}
	}

	private Character GetNearest()
	{
		Character result = null;
		this.tmpGetNearestChar = 3.40282347E+38f;
		for (int i = 0; i < this.Targetes.Count; i++)
		{
			if (this.Targetes[i] != null && this.Targetes[i].IsCanBedShootByBuff() && Vector3.Distance(this.tr.position, this.Targetes[i].tr.position) < this.tmpGetNearestChar)
			{
				this.tmpGetNearestChar = Vector3.Distance(this.tr.position, this.Targetes[i].tr.position);
				result = this.Targetes[i];
			}
		}
		return result;
	}

	private Character GetBloodest()
	{
		Character result = null;
		this.tmpGetNearestChar = 0f;
		for (int i = 0; i < this.Targetes.Count; i++)
		{
			if (this.Targetes[i] != null && this.Targetes[i].IsCanBedShootByBuff() && (this.tmpGetNearestChar == 0f || this.Targetes[i].curLife < this.tmpGetNearestChar))
			{
				this.tmpGetNearestChar = this.Targetes[i].curLife;
				result = this.Targetes[i];
			}
		}
		return result;
	}

	public Character GetTarget()
	{
		try
		{
			if (this.Tank != null)
			{
				for (int i = this.Targetes.Count - 1; i >= 0; i--)
				{
					if (this.Targetes[i] == null)
					{
						this.Targetes.Remove(this.Targetes[i]);
					}
				}
				Character result;
				switch (UnitConst.GetInstance().soldierConst[this.Tank.index].GetTarType)
				{
				case Enum_GetTargetType.enemyNearest:
					result = this.GetNearest();
					return result;
				case Enum_GetTargetType.oneByOne:
					for (int j = 0; j < this.Targetes.Count; j++)
					{
						if (this.Targetes[j] != null && this.Targetes[j].IsCanBedShootByBuff())
						{
							result = this.Targetes[j];
							return result;
						}
					}
					result = null;
					return result;
				case Enum_GetTargetType.ownerBloodLeast:
					result = this.GetBloodest();
					return result;
				}
				if (this.Targetes.Count > 0)
				{
					for (int k = 0; k < this.Targetes.Count; k++)
					{
						if (this.Targetes[k] != null && this.Targetes[k].IsCanBedShootByBuff() && this.Targetes[k].roleType == Enum_RoleType.FlyGa)
						{
							result = this.Targetes[k];
							return result;
						}
					}
					result = this.Targetes[0];
					return result;
				}
				result = null;
				return result;
			}
			else if (this.Tower != null)
			{
				for (int l = this.Targetes.Count - 1; l >= 0; l--)
				{
					if (this.Targetes[l] == null)
					{
						this.Targetes.Remove(this.Targetes[l]);
					}
				}
				Character result;
				switch (UnitConst.GetInstance().buildingConst[this.Tower.index].GetTarType)
				{
				case Enum_GetTargetType.enemyNearest:
					result = this.GetNearest();
					return result;
				case Enum_GetTargetType.oneByOne:
					for (int m = 0; m < this.Targetes.Count; m++)
					{
						if (this.Targetes[m] != null && this.Targetes[m].IsCanBedShootByBuff())
						{
							result = this.Targetes[m];
							return result;
						}
					}
					result = null;
					return result;
				case Enum_GetTargetType.ownerBloodLeast:
					result = this.GetBloodest();
					return result;
				}
				if (this.Targetes.Count > 0)
				{
					for (int n = 0; n < this.Targetes.Count; n++)
					{
						if (this.Targetes[n] != null && this.Targetes[n].IsCanBedShootByBuff() && this.Targetes[n].roleType == Enum_RoleType.FlyGa)
						{
							result = this.Targetes[n];
							return result;
						}
					}
					result = this.Targetes[0];
					return result;
				}
				result = null;
				return result;
			}
		}
		catch (Exception var_6_388)
		{
		}
		return null;
	}

	public void HurtNew(BaseFightInfo attackFightInfo, long containerID)
	{
		if (this.roleType == Enum_RoleType.tower)
		{
			this.Tower.New_HurtByBaseFightInfo(attackFightInfo, containerID, 1f);
		}
		else if (this.roleType == Enum_RoleType.tank)
		{
			this.Tank.HurtByBaseFightInfo(attackFightInfo);
		}
	}

	public void AddBuffIndex(Character buffSender, params int[] buffIndex)
	{
		if (this.roleType == Enum_RoleType.tower)
		{
			this.Tower.MyBuffRuntime.AddBuffIndex(0, buffSender, buffIndex);
		}
		else if (this.roleType == Enum_RoleType.tank)
		{
			this.Tank.MyBuffRuntime.AddBuffIndex(0, buffSender, buffIndex);
		}
	}

	public bool IsCanShootByBuff()
	{
		if (FightHundler.FightEnd && UIManager.curState != SenceState.WatchVideo)
		{
			return false;
		}
		if (this.roleType == Enum_RoleType.tower)
		{
			return this.Tower.MyBuffRuntime.IsCanShoot();
		}
		return this.roleType == Enum_RoleType.tank && this.Tank.MyBuffRuntime.IsCanShoot();
	}

	public bool IsCanBedShootByBuff()
	{
		if (FightHundler.FightEnd && UIManager.curState != SenceState.WatchVideo)
		{
			return false;
		}
		if (this.roleType == Enum_RoleType.tower)
		{
			return this.Tower.MyBuffRuntime.IsCanBedShoot();
		}
		if (this.roleType == Enum_RoleType.tank)
		{
			return this.Tank.MyBuffRuntime.IsCanBedShoot();
		}
		return this.roleType == Enum_RoleType.FlyGa;
	}

	public void ChangeSelectState(Character.CharacterSelectStates state)
	{
		if (this.Tank && !this.IsDie)
		{
			if (this.Tank.T_TankSelectState)
			{
				this.Tank.T_TankSelectState.ChangeState(state);
			}
		}
		else if (this.Tower && !this.IsDie && this.Tower.T_TowerSelectState)
		{
			this.Tower.T_TowerSelectState.ChangeState(state);
		}
	}

	public virtual void MouseDown()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.sameTower = false;
		}
		DragMgr.inst.NewMouseDown();
	}

	public virtual void MouseDrag()
	{
		if (DragMgr.inst.BtnInUse)
		{
			return;
		}
		if (Input.touchCount > 1)
		{
			DragMgr.inst.ZoomDoing();
			return;
		}
		if (Input.touchCount == 1)
		{
			DragMgr.inst.dragX = -Input.GetTouch(0).deltaPosition.x;
			DragMgr.inst.dragY = -Input.GetTouch(0).deltaPosition.y;
		}
		else
		{
			DragMgr.inst.dragX = -Input.GetAxis("Mouse X");
			DragMgr.inst.dragY = -Input.GetAxis("Mouse Y");
		}
		DragMgr.inst.ConversionFormulas(DragMgr.inst.speed);
	}

	public virtual void MouseUp()
	{
		DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector3.zero, null);
	}
}
