using BattleEvent;
using DG.Tweening;
using SimpleFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class NewbieGuidePanel : MonoBehaviour
{
	public struct NewEnemyAttackArmyStruct
	{
		public int tankID;

		public int tankLv;

		public int tankNum;

		public Vector3 tankPos;
	}

	public struct NewEnemySolider
	{
		public int soliderID;

		public int soliderLv;

		public int soliderStar;

		public int soliderSkillLv;

		public Vector3 tankPos;
	}

	public static NewbieGuidePanel _instance;

	public int firstID;

	public int twoID;

	public UISprite m_HandIcon;

	public GameObject m_ArrowsIcon;

	public UILabel arrowDes_left;

	public UILabel arrowDes_right;

	public UILabel arrowDes_up;

	public UILabel arrowDes_down;

	public Transform pianyi;

	public GameObject left;

	public GameObject right;

	public GameObject up;

	public GameObject down;

	public UISprite m_TapZoneIcon;

	public UILabel m_LeftDes;

	public UILabel m_RightDes;

	public GameObject m_btnLeft;

	public GameObject m_btnRight;

	public UILabel m_btnDes;

	public UILabel m_btnDesRight;

	public UITexture personIconLift;

	public UITexture persoIconRight;

	public GameObject personLift;

	public GameObject personRight;

	public GameObject personObj;

	public UILabel Person_Name;

	public Dictionary<int, NewbieGuide> newbieList;

	public Dictionary<int, NewbieGuidePerson> halfTalkList;

	public GameObject picture;

	public GameObject desLabels;

	public GameObject picDes;

	public GameObject desPic;

	public UISprite towerPic;

	public UISprite soldierPic;

	public UILabel desNoPic;

	public UILabel picToLabel;

	public UILabel labelToPic;

	public UILabel towerName;

	public UILabel soliderName;

	public static int curGuideIndex = -1;

	public static int guideIdByServer;

	public static int TaskGuideID;

	public GameObject BG;

	public bool isNewbie = true;

	public static bool isEnemyAttck = true;

	public static bool towerDieCallLua_InStarFire;

	public Camera cam;

	public bool isStrikeBox;

	public bool isEnable;

	public bool isClickGround;

	public static bool isZhanyi;

	public Transform camerOldPos;

	public float camerTime;

	public bool backCamer;

	public bool isZhanbao;

	public GameObject btnPanel;

	public GameObject buildingInfo;

	public UILabel buildingName;

	public UILabel buildingDes;

	public Transform moxingParent;

	public GameObject ga_Self;

	public bool IsUIMove = true;

	public GameObject box;

	private Vector3 boxPos;

	private bool isbox = true;

	public T_Tower AttackTower;

	private List<NewbieGuidePanel.NewEnemyAttackArmyStruct> allTankAttack = new List<NewbieGuidePanel.NewEnemyAttackArmyStruct>();

	private List<NewbieGuidePanel.NewEnemySolider> allCommandoAttack = new List<NewbieGuidePanel.NewEnemySolider>();

	public static bool isNewGuidBattle;

	private GameObject gameobj;

	private int type;

	private int angle;

	private Vector3 pos;

	private GameObject forceObj;

	private int showForceGuideType;

	public Transform oldBtnParent;

	public int oldBtnLayer;

	public GameObject oldBtn;

	public bool isShowShield;

	public GameObject tx;

	private string personSound = string.Empty;

	private float lastPersonSoundTime = -10f;

	private Body_Model model_Building;

	public static void CallLuaByStart()
	{
		NewbieGuideWrap.nextNewBi = string.Empty;
		NewbieGuideWrap.CurNewBiFuncName = string.Empty;
		LogManage.LogError("CallLuaByStart");
		if (GameManager.Instance && GameManager.Instance.GetLuaManage() != null && NewbieGuidePanel.isEnemyAttck)
		{
			GameManager.Instance.GetLuaManage().CallLuaFunction("NewBi.Open", new object[]
			{
				NewbieGuidePanel.guideIdByServer
			});
		}
	}

	public static void CallLuaByStartFire()
	{
		NewbieGuideWrap.nextNewBi = string.Empty;
		NewbieGuideWrap.CurNewBiFuncName = string.Empty;
		LogManage.LogError("CallLuaByStartFire");
		if (GameManager.Instance && GameManager.Instance.GetLuaManage() != null && !NewbieGuidePanel.isEnemyAttck)
		{
			GameManager.Instance.GetLuaManage().CallLuaFunction("StartFireLua.DoJob", new object[0]);
		}
	}

	public void ClearDes()
	{
		this.left.SetActive(false);
		this.right.SetActive(false);
		this.up.SetActive(false);
		this.down.SetActive(false);
	}

	public void Awake()
	{
		this.ga_Self = base.gameObject;
		NewbieGuidePanel._instance = this;
	}

	private void Start()
	{
		this.newbieList = UnitConst.GetInstance().newbieGuide;
		this.halfTalkList = UnitConst.GetInstance().newbieGuidePerson;
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (this.box == null)
		{
			if (this.gameobj == null)
			{
				this.ShowButtonAllUI(this.type, this.angle, this.pos, this.gameobj);
			}
			else if (this.gameobj.GetComponent<ButtonClick>() != null)
			{
				this.ShowButtonAllUI(this.type, this.angle, this.pos, this.gameobj);
			}
			else if (Camera.main && this.cam)
			{
				if (this.m_ArrowsIcon.activeSelf)
				{
					Vector3 position = Camera.main.WorldToScreenPoint(this.gameobj.transform.position);
					Vector3 vector = this.cam.ScreenToWorldPoint(position);
					this.m_ArrowsIcon.transform.position = new Vector3(vector.x + this.pos.x, vector.y + this.pos.y, 0f);
				}
				else
				{
					Vector3 position2 = Camera.main.WorldToScreenPoint(this.gameobj.transform.position);
					Vector3 vector2 = this.cam.ScreenToWorldPoint(position2);
					this.m_HandIcon.transform.position = new Vector3(vector2.x + this.pos.x, vector2.y + this.pos.y, 0f);
				}
			}
			this.ShowForceGuide(this.forceObj, this.showForceGuideType);
		}
		if (this.box != null)
		{
			this.m_HandIcon.gameObject.SetActive(true);
			Vector3 position3 = Camera.main.WorldToScreenPoint(this.box.transform.position);
			Vector3 vector3 = this.cam.ScreenToWorldPoint(position3);
			this.m_HandIcon.transform.position = new Vector3(vector3.x + this.boxPos.x, vector3.y + this.boxPos.y, 0f);
		}
	}

	public void StrikeBox(int id, Vector3 pos, Vector3 weizhi)
	{
		this.isbox = false;
		this.ShowSelf();
		this.box = (UnityEngine.Object.Instantiate(TimePanel.inst.ranEventPrefab, weizhi, Quaternion.identity) as GameObject);
		this.box.transform.parent = SenceManager.inst.transform;
		RandomEventMonoBehaviour randomEventMonoBehaviour = this.box.AddComponent<RandomEventMonoBehaviour>();
		randomEventMonoBehaviour.boxKey = 1;
		randomEventMonoBehaviour.eventKey = id;
		Vector3 cameraMoveEndPos = HUDTextTool.inst.GetCameraMoveEndPos(this.box.transform.position, CameraControl.inst.Tr.position, 50f);
		NewbieGuideManage._instance.cc = CameraControl.inst.Tr.DOMove(cameraMoveEndPos, 3f, false);
		CameraControl.inst.openDragCameraAndInertia = false;
		NewbieGuideManage._instance.cc.OnComplete(delegate
		{
			HUDTextTool.inst.GetCenterPos();
			this.m_HandIcon.gameObject.SetActive(true);
			if (this.box && Camera.main)
			{
				Vector3 position = Camera.main.WorldToScreenPoint(this.box.transform.position);
				Vector3 vector = this.cam.ScreenToWorldPoint(position);
				this.m_HandIcon.transform.position = new Vector3(vector.x + pos.x, vector.y + pos.y, 0f);
				this.BG.SetActive(false);
				this.isStrikeBox = true;
				this.isbox = true;
			}
		});
	}

	public void EnemyAttack(int _tankID, int _tankLv, int _tankNum, Vector3 pos1)
	{
		NewbieGuidePanel.NewEnemyAttackArmyStruct item = default(NewbieGuidePanel.NewEnemyAttackArmyStruct);
		item.tankID = _tankID;
		item.tankLv = _tankLv;
		item.tankNum = _tankNum;
		item.tankPos = pos1;
		this.allTankAttack.Add(item);
	}

	public void SoliderAttack(int _tankID, int _tankLv, int _tankStar, int _tankSkillLv, Vector3 pos1)
	{
		NewbieGuidePanel.NewEnemySolider item = default(NewbieGuidePanel.NewEnemySolider);
		item.soliderID = _tankID;
		item.soliderLv = _tankLv;
		item.soliderStar = _tankStar;
		item.soliderSkillLv = _tankSkillLv;
		item.tankPos = pos1;
		this.allCommandoAttack.Add(item);
	}

	public void EnemyAttackStart()
	{
		UIManager.curState = SenceState.Attacking;
		SenceManager.inst.OnGoHome += new UnityAction(this.SenceManager_OnGoHome);
		Loading.IsRefreshSence = true;
		PlayerHandle.EnterSence("island");
	}

	private void SenceManager_OnGoHome()
	{
		NewbieGuidePanel.isEnemyAttck = false;
		this.BG.SetActive(true);
		this.m_TapZoneIcon.gameObject.SetActive(false);
		UIManager.curState = SenceState.Attacking;
		UIManager.inst._3DUICamera.gameObject.SetActive(false);
		AudioManage.inst.PlayAudioBackground("battle", true);
		FightHundler.FightEnd = false;
		CameraControl.inst.blur.enabled = false;
		CameraControl.inst.cameraMain.DOLocalMoveZ(10f, 0.6f, false);
		FightHundler.isSendFightEnd = true;
		for (int i = 0; i < this.allTankAttack.Count; i++)
		{
			for (int j = 0; j < this.allTankAttack[i].tankNum; j++)
			{
				Container.CreateContainer(this.allTankAttack[i].tankPos, 0L, SenceManager.inst.SoldierId, this.allTankAttack[i].tankID, this.allTankAttack[i].tankLv, true, false, CommanderType.None);
			}
		}
		for (int k = 0; k < this.allCommandoAttack.Count; k++)
		{
			Container.CreateCommander(this.allCommandoAttack[k].tankPos, 0L, 1L, this.allCommandoAttack[k].soliderID, this.allCommandoAttack[k].soliderLv, this.allCommandoAttack[k].soliderStar, this.allCommandoAttack[k].soliderSkillLv, true);
		}
		SenceManager.inst.Tank_AttackAllDie += new UnityAction(this.SenceManager_TankAllDie);
	}

	private void SenceManager_TankAllDie()
	{
	}

	[DebuggerHidden]
	private IEnumerator CloseBattle()
	{
		NewbieGuidePanel.<CloseBattle>c__Iterator92 <CloseBattle>c__Iterator = new NewbieGuidePanel.<CloseBattle>c__Iterator92();
		<CloseBattle>c__Iterator.<>f__this = this;
		return <CloseBattle>c__Iterator;
	}

	public void GoHome()
	{
		if (NewbieGuidePanel.curGuideIndex == -1)
		{
			NewbieGuidePanel.guideIdByServer = 1;
			NewbieGuidePanel.curGuideIndex = 1;
			NewbieGuideManage._instance.CS_NewGuide(1);
		}
		MovePoint.ChangeTaget(null);
		UIManager.curState = SenceState.Home;
		Loading.IsRefreshSence = true;
		FightHundler.FightEnd = false;
		SenceManager.inst.OnGoHome -= new UnityAction(this.SenceManager_OnGoHome);
		SenceManager.inst.Tank_AttackAllDie -= new UnityAction(this.SenceManager_TankAllDie);
		NewbieGuidePanel.isEnemyAttck = true;
		SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
	}

	public void ShowDianJiDiMian(int type, int angle, Vector3 pos)
	{
		Camera_FingerManager.YinDaoDianji = true;
		this.ShowButtonAllUI(type, angle, pos, null);
	}

	public void ShowButtonAllUI(int type, int angle, Vector3 pos, GameObject buttton = null)
	{
		this.gameobj = buttton;
		this.type = type;
		this.angle = angle;
		this.pos = pos;
		this.ShowSelf();
		switch (type)
		{
		case 0:
			this.m_HandIcon.gameObject.SetActive(false);
			this.m_ArrowsIcon.gameObject.SetActive(false);
			this.m_TapZoneIcon.gameObject.SetActive(false);
			break;
		case 1:
			this.m_HandIcon.gameObject.SetActive(true);
			this.m_ArrowsIcon.gameObject.SetActive(false);
			this.m_HandIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
			this.ShowHand(pos, buttton);
			break;
		case 2:
			this.m_HandIcon.gameObject.SetActive(false);
			this.m_ArrowsIcon.gameObject.SetActive(true);
			this.m_ArrowsIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
			this.ShowArrows(pos, buttton);
			break;
		}
	}

	public void ShowTowerAllUI(int type, int angle, Vector3 pos, GameObject tower = null)
	{
		this.gameobj = tower;
		this.type = type;
		this.angle = angle;
		this.pos = pos;
		this.ShowSelf();
		if (tower != null)
		{
			NewbieGuideManage._instance.CameraMoveTo(tower.transform);
			NewbieGuideManage._instance.cc.OnComplete(delegate
			{
				UIManager.inst.gameObject.SetActive(true);
				this.BG.SetActive(false);
				switch (type)
				{
				case 0:
					this.m_HandIcon.gameObject.SetActive(false);
					this.m_ArrowsIcon.gameObject.SetActive(false);
					this.m_TapZoneIcon.gameObject.SetActive(false);
					break;
				case 1:
					this.m_HandIcon.gameObject.SetActive(true);
					this.m_ArrowsIcon.gameObject.SetActive(false);
					this.m_HandIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
					this.ShowHand(pos, tower);
					break;
				case 2:
					this.m_HandIcon.gameObject.SetActive(false);
					this.m_ArrowsIcon.gameObject.SetActive(true);
					this.m_ArrowsIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
					this.ShowArrows(pos, tower);
					break;
				}
			});
		}
		else
		{
			this.BG.SetActive(false);
			switch (type)
			{
			case 0:
				this.m_HandIcon.gameObject.SetActive(false);
				this.m_ArrowsIcon.gameObject.SetActive(false);
				break;
			case 1:
				this.m_HandIcon.gameObject.SetActive(true);
				this.m_ArrowsIcon.gameObject.SetActive(false);
				this.m_HandIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
				this.ShowHand(pos, tower);
				break;
			case 2:
				this.m_HandIcon.gameObject.SetActive(false);
				this.m_ArrowsIcon.gameObject.SetActive(true);
				this.m_ArrowsIcon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (float)angle));
				this.ShowArrows(pos, tower);
				break;
			}
		}
	}

	public void ShowForceGuide(GameObject ga, int type)
	{
		this.ShowSelf();
		this.showForceGuideType = type;
		this.forceObj = ga;
		if (ga == null)
		{
			this.BG.SetActive(false);
			this.m_TapZoneIcon.gameObject.SetActive(false);
			return;
		}
		this.BG.SetActive(true);
		this.m_TapZoneIcon.gameObject.SetActive(true);
		if (ClickBtn.inst)
		{
			ClickBtn.inst.dele = null;
			if (ga.GetComponent<ButtonClick>() != null)
			{
				UIWidget component = ga.GetComponent<UIWidget>();
				if (component)
				{
					if (component.GetComponent<EarthStar>())
					{
						this.m_TapZoneIcon.SetDimensions(150, 150);
					}
					else
					{
						this.m_TapZoneIcon.SetDimensions(component.width + 20, component.height + 20);
					}
				}
				else
				{
					this.m_TapZoneIcon.SetDimensions(36, 36);
				}
				if (ga.GetComponent<EarthStar>() != null)
				{
					Camera earth_Camera = LegionMapManager._inst.Earth_Camera;
					if (earth_Camera)
					{
						Vector3 position = earth_Camera.WorldToScreenPoint(ga.transform.position);
						Vector3 vector = this.cam.ScreenToWorldPoint(position);
						this.m_TapZoneIcon.transform.position = new Vector3(vector.x, vector.y, 0f);
					}
					ClickBtn.inst.dele = new Action(ga.GetComponent<ButtonClick>().DoClick);
					ClickBtn.inst.deleGa = ga;
				}
				else
				{
					Camera camera = NGUITools.FindCameraForLayer(ga.layer);
					if (camera)
					{
						Vector3 position2 = camera.WorldToScreenPoint(ga.transform.position);
						Vector3 vector2 = this.cam.ScreenToWorldPoint(position2);
						this.m_TapZoneIcon.transform.position = new Vector3(vector2.x, vector2.y, 0f);
					}
					ClickBtn.inst.dele = new Action(ga.GetComponent<ButtonClick>().DoClick);
					ClickBtn.inst.deleGa = ga;
				}
			}
			else if (ga.GetComponent<T_Tower>() != null)
			{
				if (Camera.main)
				{
					Vector3 position3 = Camera.main.WorldToScreenPoint(ga.transform.position);
					Vector3 vector3 = this.cam.ScreenToWorldPoint(position3);
					this.m_TapZoneIcon.transform.position = new Vector3(vector3.x, vector3.y, 0f);
					this.m_TapZoneIcon.SetDimensions(150, 150);
					ClickBtn.inst.dele = new Action(ga.GetComponent<T_Tower>().MouseUp);
					ClickBtn.inst.deleGa = ga;
				}
			}
			else if (ga.GetComponent<T_Island>() != null)
			{
				Camera camera2 = NGUITools.FindCameraForLayer(ga.layer);
				if (camera2)
				{
					Vector3 position4 = camera2.WorldToScreenPoint(ga.transform.position);
					Vector3 vector4 = this.cam.ScreenToWorldPoint(position4);
					this.m_TapZoneIcon.transform.position = new Vector3(vector4.x, vector4.y, 0f);
					this.m_TapZoneIcon.SetDimensions(150, 150);
				}
				ClickBtn.inst.dele = new Action(ga.GetComponent<T_Island>().MouseUp);
				ClickBtn.inst.deleGa = ga;
			}
			else if (ga.GetComponent<LandArmy_Guid>() != null && Camera.main && this.cam)
			{
				Vector3 position5 = Camera.main.WorldToScreenPoint(ga.transform.position);
				Vector3 vector5 = this.cam.ScreenToWorldPoint(position5);
				this.m_TapZoneIcon.transform.position = new Vector3(vector5.x, vector5.y, 0f);
				this.m_TapZoneIcon.SetDimensions(300, 300);
				ClickBtn.inst.dele = new Action(ga.GetComponent<LandArmy_Guid>().OnMouseUp);
				ClickBtn.inst.deleGa = ga;
			}
		}
	}

	public void ShowShield(GameObject ga)
	{
		this.isShowShield = true;
		this.btnPanel.SetActive(true);
		this.oldBtn = ga;
		this.oldBtnParent = ga.transform.parent;
		this.oldBtnLayer = ga.layer;
		ga.transform.parent = this.btnPanel.transform;
		ga.layer = 8;
		Transform[] componentsInChildren = ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 8;
		}
	}

	public void HideShiele()
	{
		this.isShowShield = false;
		if (this.oldBtnParent != null)
		{
			this.oldBtn.transform.parent = this.oldBtnParent;
			this.oldBtn.layer = this.oldBtnLayer;
			Transform[] componentsInChildren = this.oldBtn.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = this.oldBtnLayer;
			}
			this.oldBtn.SetActive(false);
			this.oldBtn.SetActive(true);
			this.oldBtnLayer = -1;
			this.oldBtnParent = null;
			this.oldBtn = null;
		}
		else
		{
			UnityEngine.Object.Destroy(this.oldBtn);
		}
		this.btnPanel.SetActive(false);
	}

	public void ShowArrows(Vector3 pos, GameObject ga = null)
	{
		if (ga == null)
		{
			this.m_ArrowsIcon.transform.localPosition = pos;
		}
		else if (ga.GetComponent<T_Tower>() != null)
		{
			Vector3 position = Camera.main.WorldToScreenPoint(ga.transform.position);
			Vector3 vector = this.cam.ScreenToWorldPoint(position);
			this.m_ArrowsIcon.transform.position = new Vector3(vector.x + pos.x, vector.y + pos.y, 0f);
		}
		else if (ga.GetComponent<EarthStar>() != null)
		{
			Vector3 position2 = LegionMapManager._inst.Earth_Camera.WorldToScreenPoint(ga.transform.position);
			Vector3 vector2 = this.cam.ScreenToWorldPoint(position2);
			this.m_ArrowsIcon.transform.position = new Vector3(vector2.x + pos.x, vector2.y + pos.y, 0f);
		}
		else
		{
			Camera camera = NGUITools.FindCameraForLayer(ga.layer);
			if (camera)
			{
				Vector3 position3 = camera.WorldToScreenPoint(ga.transform.position);
				Vector3 vector3 = this.cam.ScreenToWorldPoint(position3);
				this.m_ArrowsIcon.transform.position = new Vector3(vector3.x + pos.x, vector3.y + pos.y, 0f);
			}
		}
	}

	public void ShowHand(Vector3 pos, GameObject ga = null)
	{
		if (ga == null)
		{
			this.m_HandIcon.transform.position = pos;
		}
		else
		{
			this.m_HandIcon.transform.position = new Vector3(ga.transform.position.x + pos.x, ga.transform.position.y + pos.y, ga.transform.position.z + pos.z);
		}
	}

	public void ShowPerson(int personId, int buildingIndex)
	{
		this.ShowSelf();
		this.type = 0;
		if (this.halfTalkList == null)
		{
			this.halfTalkList = UnitConst.GetInstance().newbieGuidePerson;
		}
		NewbieGuidePerson newbieGuidePerson = this.halfTalkList[personId];
		int bodyPlance = newbieGuidePerson.bodyPlance;
		if (bodyPlance != 1)
		{
			if (bodyPlance == 2)
			{
				this.personRight.SetActive(true);
				this.m_RightDes.text = LanguageManage.GetTextByKey(newbieGuidePerson.content, "Halftalk");
				this.m_btnDesRight.text = LanguageManage.GetTextByKey(newbieGuidePerson.buttonContent, "Halftalk");
				HUDTextTool.inst.OnSetTextureIcon(this.persoIconRight, newbieGuidePerson.bodyId, "GuideDialogue/");
				this.persoIconRight.GetComponent<TweenPosition>().enabled = true;
			}
		}
		else
		{
			this.personLift.SetActive(true);
			this.m_LeftDes.text = LanguageManage.GetTextByKey(newbieGuidePerson.content, "Halftalk");
			this.m_btnDes.text = LanguageManage.GetTextByKey(newbieGuidePerson.buttonContent, "Halftalk");
			HUDTextTool.inst.OnSetTextureIcon(this.personIconLift, newbieGuidePerson.bodyId, "GuideDialogue/");
			this.personIconLift.GetComponent<TweenPosition>().enabled = true;
			if (this.Person_Name)
			{
				this.Person_Name.text = LanguageManage.GetTextByKey(newbieGuidePerson.name, "Halftalk") + ":";
			}
		}
		AudioManage.inst.audioPlay.Stop();
		if (newbieGuidePerson.sound != null)
		{
			string text = newbieGuidePerson.sound[UnityEngine.Random.Range(0, newbieGuidePerson.sound.Length)];
			if (!this.personSound.Equals(text) || Time.time - this.lastPersonSoundTime > 2f)
			{
				AudioManage.inst.PlayAuido(text, false);
				this.lastPersonSoundTime = Time.time;
				this.personSound = text;
			}
		}
		if (buildingIndex != 0)
		{
			this.buildingInfo.gameObject.SetActive(true);
			this.picture.SetActive(false);
			this.desLabels.SetActive(false);
			this.picDes.SetActive(false);
			this.desPic.SetActive(false);
			this.buildingName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[buildingIndex].name, "build");
			this.buildingDes.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[buildingIndex].description, "build");
			foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().buildingConst[buildingIndex].NewbiArant)
			{
				switch (current.Key)
				{
				case 1:
					this.picture.SetActive(true);
					this.towerPic.spriteName = UnitConst.GetInstance().buildingConst[buildingIndex].bigIcon;
					this.towerName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[buildingIndex].name, "build");
					this.soldierPic.spriteName = UnitConst.GetInstance().soldierConst[current.Value].icon;
					this.soliderName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[current.Value].name, "Army");
					break;
				case 2:
					this.desLabels.SetActive(true);
					break;
				case 3:
					this.picDes.SetActive(true);
					break;
				case 4:
					this.desPic.SetActive(true);
					break;
				}
			}
			if (this.model_Building)
			{
				UnityEngine.Object.Destroy(this.model_Building.ga);
			}
			this.model_Building = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[buildingIndex].lvInfos[1].bodyID, this.moxingParent);
			float num = 0f;
			if (this.model_Building)
			{
				num = UnitConst.GetInstance().buildingConst[buildingIndex].guid_display[2].x / this.model_Building.tr.localScale.x;
				UnityEngine.Debug.Log("bl:" + num);
				this.model_Building.tr.localScale = UnitConst.GetInstance().buildingConst[buildingIndex].guid_display[2];
				this.model_Building.tr.localPosition = UnitConst.GetInstance().buildingConst[buildingIndex].guid_display[0];
				this.model_Building.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[buildingIndex].guid_display[1]);
				if (this.model_Building.RedModel)
				{
					this.model_Building.RedModel.gameObject.SetActive(false);
				}
				if (this.model_Building.Red_DModel)
				{
					this.model_Building.Red_DModel.gameObject.SetActive(false);
				}
				if (this.model_Building.Blue_DModel)
				{
					this.model_Building.Blue_DModel.gameObject.SetActive(false);
				}
			}
			this.buildingInfo.SetActive(true);
			ParticleSystem[] componentsInChildren = this.model_Building.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem particleSystem = componentsInChildren[i];
				particleSystem.startSize *= num;
				particleSystem.gameObject.SetActive(true);
			}
			Transform[] componentsInChildren2 = this.model_Building.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform = componentsInChildren2[j];
				transform.gameObject.layer = 8;
			}
		}
		else
		{
			this.buildingInfo.gameObject.SetActive(false);
		}
	}

	public void ShowSelf()
	{
		base.gameObject.SetActive(true);
	}

	public void HideSelf()
	{
		this.gameobj = null;
		this.m_TapZoneIcon.gameObject.SetActive(false);
		this.m_HandIcon.gameObject.SetActive(false);
		this.m_ArrowsIcon.gameObject.SetActive(false);
		this.BG.SetActive(false);
		this.forceObj = null;
		if (this.personLift.activeSelf)
		{
			this.personLift.SetActive(false);
		}
		else
		{
			this.personRight.SetActive(false);
		}
		this.buildingInfo.SetActive(false);
		if (this.model_Building)
		{
			this.model_Building.DesInsInPool();
		}
		base.gameObject.SetActive(false);
	}

	public void NewEnemyAttack(int tankID, int tankNum, int tankLv, Vector3 pos0)
	{
		base.gameObject.SetActive(true);
		this.type = 0;
		this.gameobj = null;
		this.EnemyAttack(tankID, tankLv, tankNum, pos0);
	}
}
