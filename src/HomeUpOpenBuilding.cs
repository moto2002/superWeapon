using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class HomeUpOpenBuilding : FuncUIPanel
{
	public UIGrid itemGrid;

	public GameObject item;

	public Transform moxingParent;

	public UILabel homeLevel;

	public static HomeUpOpenBuilding inst;

	private bool _2DLock;

	private Transform tr;

	private bool isCanBack;

	private HomeUpdateOpenSetData _homeUpdateOpenSetData;

	private Body_Model model;

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
		if (NewarmyInfo.inst != null && NewarmyInfo.inst.gameObject.activeInHierarchy)
		{
			return;
		}
		if (TopTenPanelManage._ins != null && TopTenPanelManage._ins.gameObject.activeInHierarchy)
		{
			return;
		}
		HUDTextTool.inst.GUIGameObject.SetActive(true);
		NewbieGuidePanel.CallLuaByStart();
	}

	public override void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
		base.OnDisable();
	}

	public override void OnEnable()
	{
		AudioManage.inst.PlayAuidoBySelf_2D("silingbujiesuo", base.gameObject, false, 0uL);
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
		base.OnEnable();
		Camera_FingerManager.inst.DestroyDragCamera();
	}

	public override void Awake()
	{
		HomeUpOpenBuilding.inst = this;
		this.tr = base.transform;
		this.itemGrid.isRespositonOnStart = false;
		EventManager.Instance.AddEvent(EventManager.EventType.HomeUpOpenBuildingAndArmy_Close, new EventManager.VoidDelegate(this.CloseThis));
		this.isCanBack = false;
	}

	public void CloseThis(GameObject go)
	{
		if (!this.isCanBack)
		{
			return;
		}
		go.GetComponent<ButtonClick>().isSendLua = false;
		if (this._homeUpdateOpenSetData != null)
		{
			this._homeUpdateOpenSetData.buildingIds.Clear();
		}
		FuncUIManager.inst.DestoryFuncUI("HomeUPOpenBuildingPanel");
		HomeUpOpenBuilding.inst = null;
		FuncUIManager.inst.HomeUpOpenBuildingAndArmy();
	}

	public void SetInfo(HomeUpdateOpenSetData homeUpdateOpenSetData)
	{
		this._homeUpdateOpenSetData = homeUpdateOpenSetData;
		this.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[1].lvInfos[SenceManager.inst.MainBuilding.lv].bodyID, this.moxingParent);
		this.model.tr.localPosition = new Vector3(0f, 667f, -300f);
		this.model.tr.localRotation = Quaternion.Euler(new Vector3(5.5f, 118f, -8.5f));
		this.model.tr.localScale = Vector3.one * 32f;
		if (this.model.Red_DModel)
		{
			NGUITools.SetActive(this.model.Red_DModel.gameObject, false);
		}
		if (this.model.Blue_DModel)
		{
			NGUITools.SetActive(this.model.Blue_DModel.gameObject, false);
		}
		if (this.model.RedModel)
		{
			NGUITools.SetActive(this.model.RedModel.gameObject, false);
		}
		this.homeLevel.text = "LV." + SenceManager.inst.MainBuilding.lv;
		if (this.model.GetComponentsInChildren<ParticleSystem>() != null)
		{
			ParticleSystem[] componentsInChildren = this.model.GetComponentsInChildren<ParticleSystem>();
			if (UnitConst.GetInstance().buildingConst[1].particleInfo != null)
			{
				int num = (componentsInChildren.Length <= UnitConst.GetInstance().buildingConst[1].particleInfo.Length) ? componentsInChildren.Length : UnitConst.GetInstance().buildingConst[1].particleInfo.Length;
				for (int i = 0; i < num; i++)
				{
					componentsInChildren[i].startSize.GetType();
					componentsInChildren[i].startSize = UnitConst.GetInstance().buildingConst[1].particleInfo[i];
				}
			}
		}
		Transform[] componentsInChildren2 = this.model.GetComponentsInChildren<Transform>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			Transform transform = componentsInChildren2[j];
			transform.gameObject.layer = 8;
		}
		this.model.tr.DOLocalMoveY(50f, 0.3f, false).OnComplete(new TweenCallback(this.ModelDoMoveY));
	}

	private void ModelDoMoveY()
	{
		this.tr.DOShakePosition(0.3f, new Vector3(0f, -15f, 0f), 20, 90f, false);
		this.model.tr.DOLocalMoveY(67f, 0.1f, false).OnComplete(delegate
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect("silingbu_shengji", this.moxingParent.position, Quaternion.identity, this.moxingParent);
			dieBall.tr.localPosition = new Vector3(-7f, 91.73f, -300f);
			dieBall.tr.localScale = Vector3.one;
			Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 8;
			}
			CoroutineInstance.DoJob(this.Ani());
		});
	}

	[DebuggerHidden]
	private IEnumerator Ani()
	{
		HomeUpOpenBuilding.<Ani>c__Iterator83 <Ani>c__Iterator = new HomeUpOpenBuilding.<Ani>c__Iterator83();
		<Ani>c__Iterator.<>f__this = this;
		return <Ani>c__Iterator;
	}
}
