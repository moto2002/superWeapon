using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class T_Info : MonoBehaviour
{
	public static T_Info inst;

	public Transform tar;

	public bool show;

	public int ownType;

	private InfoType infoTy;

	public GameObject Tank;

	public GameObject Tower;

	public UILabel towerLV;

	public UILabel towerName;

	public UILabel lvLab;

	public InfoPrograme programe;

	public InfoLife life;

	public UISprite res;

	public UISprite lifeSp;

	public UISprite lifeBg;

	private Camera cam;

	private Transform tr;

	public float lifeRatio;

	private Body_Model model;

	private void Awake()
	{
		this.tr = base.transform;
		T_Info.inst = this;
	}

	public void OnEnable()
	{
	}

	private void Start()
	{
		this.cam = UIManager.inst.uiCamera;
	}

	private void Update()
	{
		if (this.tar == null || !this.tar.gameObject.activeSelf)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + 1.5f, this.tar.position.z);
			if (Camera.main == null)
			{
				return;
			}
			Vector3 position2 = Camera.main.WorldToScreenPoint(position);
			Vector3 position3 = this.cam.ScreenToWorldPoint(position2);
			if (position3.z < 0f)
			{
			}
			position3 = new Vector3(position3.x, position3.y + 0.1f, 0f);
			this.tr.position = position3;
		}
	}

	[DebuggerHidden]
	public IEnumerator AreadyMaxLife()
	{
		T_Info.<AreadyMaxLife>c__Iterator84 <AreadyMaxLife>c__Iterator = new T_Info.<AreadyMaxLife>c__Iterator84();
		<AreadyMaxLife>c__Iterator.<>f__this = this;
		return <AreadyMaxLife>c__Iterator;
	}

	public void ShowTankLife(T_TankAbstract tank, T_Tower tower)
	{
		base.enabled = true;
		this.infoTy = InfoType.life;
		if (tank != null)
		{
			this.Tank.SetActive(true);
			this.lvLab.gameObject.SetActive(false);
			this.programe.Show(false);
			this.life.Show(true);
			if (this.res)
			{
				this.res.gameObject.SetActive(false);
			}
			if (SenceManager.inst.fightType == FightingType.Guard || !NewbieGuidePanel.isEnemyAttck)
			{
				if (tank.charaType == Enum_CharaType.defender)
				{
					this.lifeSp.spriteName = "dilv";
				}
				else
				{
					this.lifeSp.spriteName = "dilv";
				}
			}
			else if (tank.charaType == Enum_CharaType.defender)
			{
				this.lifeSp.spriteName = "dilv";
			}
			else
			{
				this.lifeSp.spriteName = "dilv";
			}
			this.life.lvtext.text = tank.lv.ToString();
			if (tank.CurLife < (float)tank.MaxLife)
			{
				HUDTextTool.inst.SetColorChange(this.lifeSp.gameObject);
			}
			this.lifeRatio = tank.CurLife / (float)tank.MaxLife;
			float num = tank.CurLife / (float)tank.MaxLife * 100f;
			if (num < 50f)
			{
				this.lifeSp.spriteName = "dihong";
				this.lifeBg.spriteName = "di";
				this.life.headBg.spriteName = "hong";
			}
			else
			{
				this.lifeSp.spriteName = "dilv";
				this.lifeBg.spriteName = "di";
				this.life.headBg.spriteName = "lv";
			}
			this.life.uis.value = this.lifeRatio;
			this.life.uisp.SetDimensions(78, 12);
			this.tar = tank.tr;
		}
		else
		{
			this.Tower.SetActive(true);
			this.towerLV.text = string.Format("LV.{0}", tower.lv);
			this.towerName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build");
			this.tar = tower.tr;
		}
	}

	public void ShowShieldLife(T_TankAbstract tank, T_Tower tower)
	{
		base.enabled = true;
		this.infoTy = InfoType.life;
		if (tank.CharacterBaseFightInfo.Shield > 0)
		{
			HUDTextTool.inst.SetColorChange(this.lifeSp.gameObject);
		}
		this.lvLab.gameObject.SetActive(false);
		if (this.programe)
		{
			this.programe.Show(false);
		}
		if (this.life)
		{
			this.life.Show(true);
		}
		if (this.res)
		{
			this.res.gameObject.SetActive(false);
		}
		if (this.life.headBg)
		{
			this.life.headBg.gameObject.SetActive(false);
		}
		if (tank != null)
		{
			if (SenceManager.inst.fightType == FightingType.Guard || !NewbieGuidePanel.isEnemyAttck)
			{
				if (tank.charaType == Enum_CharaType.defender)
				{
					this.lifeSp.spriteName = "dilv";
				}
				else
				{
					this.lifeSp.spriteName = "dilv";
				}
			}
			else if (tank.charaType == Enum_CharaType.defender)
			{
				this.lifeSp.spriteName = "dilv";
			}
			else
			{
				this.lifeSp.spriteName = "dilv";
			}
			this.lifeSp.spriteName = "dicheng";
			this.lifeBg.spriteName = "di";
			this.life.headBg.spriteName = "lv";
			this.life.transform.localPosition = new Vector3(0f, 8f, 0f);
			this.life.transform.localScale = new Vector3(1f, 0.5f, 1f);
			this.life.uis.value = this.lifeRatio;
			this.life.uisp.SetDimensions(78, 12);
			this.tar = tank.tr;
		}
		else
		{
			if (SenceManager.inst.fightType == FightingType.Guard || !NewbieGuidePanel.isEnemyAttck)
			{
				this.lifeSp.spriteName = "dilv";
			}
			else
			{
				this.lifeSp.spriteName = "dilv";
			}
			this.life.uis.value = tower.CurLife / (float)tower.MaxLife;
			this.life.uisp.SetDimensions(100, 12);
			this.tar = tower.tr;
		}
	}

	public void Remove()
	{
		this.tr.position = SenceManager.inst.hidePos;
		base.enabled = false;
		if (UIManager.curState == SenceState.Spy || UIManager.curState == SenceState.Visit)
		{
			CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(40f, 0f, 0f), 0.6f, RotateMode.Fast);
			CameraControl.inst.cameraMain.DOLocalMoveZ(CameraInitMove.inst.data.moveHeight, 0.6f, false);
		}
		if (this.tar != null && this.tar.GetComponent<T_Tower>() != null && this.tar.gameObject.activeInHierarchy)
		{
			this.tar.GetComponent<T_Tower>().T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
		}
	}

	public void EventBtn(InfoBtnType type)
	{
	}
}
