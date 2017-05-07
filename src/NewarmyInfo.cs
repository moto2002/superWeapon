using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class NewarmyInfo : FuncUIPanel
{
	public static NewarmyInfo inst;

	public UISprite armyIcon;

	public GameObject xinbingzhongClose;

	public UILabel armyname;

	public UITable itemInfo;

	public UILabel itemintro;

	public GameObject[] SpeedSp;

	public GameObject[] defenseSp;

	public GameObject[] AttakeSp;

	public GameObject[] lifeSp;

	public GameObject[] SpeedAttakeSp;

	public TweenScale tweenScale;

	public GameObject ArmyPrefab;

	public int armyID;

	public Body_Model armyInstance;

	public UILabel speed;

	public UILabel defend;

	public UILabel attack;

	public UILabel life;

	public UILabel speedattack;

	private bool IsSoldier;

	private bool isCanBack;

	private bool _2DLock;

	public override void Awake()
	{
		NewarmyInfo.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.HomeUpOpenBuildingAndArmy_Close, new EventManager.VoidDelegate(this.CloseThis));
		this.isCanBack = false;
	}

	private void CreateStart()
	{
		this.armyInstance = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[this.armyID].bodyId, this.ArmyPrefab.transform);
		if (this.armyInstance)
		{
			this.IsSoldier = false;
			if (this.armyID == 23 || this.armyID == 24)
			{
				this.IsSoldier = true;
			}
			this.armyInstance.tr.localScale = UnitConst.GetInstance().soldierConst[this.armyID].uisize * Vector3.one;
			if (this.IsSoldier)
			{
				this.armyInstance.tr.localScale *= 3f;
				if (this.armyInstance)
				{
					Animation[] componentsInChildren = this.armyInstance.GetComponentsInChildren<Animation>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].GetClip("Idle"))
						{
							componentsInChildren[i].Play("Idle");
						}
					}
				}
			}
			this.armyInstance.tr.localRotation = Quaternion.Euler(new Vector3(0f, 160f, 0f));
			this.armyInstance.tr.localPosition = new Vector3(0f, 0.2f, 0f);
			if (this.armyInstance.RedModel)
			{
				NGUITools.SetActiveSelf(this.armyInstance.RedModel.gameObject, false);
			}
			Transform[] componentsInChildren2 = base.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].gameObject.layer = 5;
			}
		}
	}

	public void CloseThis(GameObject go)
	{
		if (!this.isCanBack)
		{
			return;
		}
		go.GetComponent<ButtonClick>().isSendLua = false;
		FuncUIManager.inst.DestoryFuncUI("ArmyOpenPanel");
		NewarmyInfo.inst = null;
		FuncUIManager.inst.HomeUpOpenBuildingAndArmy();
	}

	private void OnDestroy()
	{
		if (ShowPlayerLevelManager.ins != null && ShowPlayerLevelManager.ins.gameObject.activeInHierarchy)
		{
			return;
		}
		if (ShowAwardPanelManger._ins != null && ShowAwardPanelManger._ins.gameObject.activeInHierarchy)
		{
			return;
		}
		if (HomeUpOpenBuilding.inst != null && HomeUpOpenBuilding.inst.gameObject.activeInHierarchy)
		{
			return;
		}
		if (TopTenPanelManage._ins != null && TopTenPanelManage._ins.gameObject.activeInHierarchy)
		{
			return;
		}
		HUDTextTool.inst.GUIGameObject.SetActive(true);
		NewarmyInfo.inst = null;
		NewbieGuidePanel.CallLuaByStart();
	}

	public override void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
		base.OnDisable();
	}

	public override void OnEnable()
	{
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
		base.OnEnable();
		Camera_FingerManager.inst.DestroyDragCamera();
		AudioManage.inst.PlayAuido("opennewarmy", false);
	}

	private void ArmyInsRota()
	{
		if (this.armyInstance)
		{
			this.armyInstance.transform.DOLocalRotate(new Vector3(0f, 520f, 0f), 2f, RotateMode.FastBeyond360).OnComplete(new TweenCallback(this.AddSpinMouse));
		}
		base.StartCoroutine(this.Stars());
	}

	[DebuggerHidden]
	private IEnumerator Stars()
	{
		NewarmyInfo.<Stars>c__Iterator67 <Stars>c__Iterator = new NewarmyInfo.<Stars>c__Iterator67();
		<Stars>c__Iterator.<>f__this = this;
		return <Stars>c__Iterator;
	}

	private void AddSpinMouse()
	{
		if (this.armyInstance)
		{
			Animation[] componentsInChildren = this.armyInstance.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].GetClip("Attack1"))
				{
					componentsInChildren[i].Play("Attack1");
				}
				if (componentsInChildren[i].GetClip("Attack2"))
				{
					componentsInChildren[i].PlayQueued("Attack2");
				}
			}
		}
	}
}
