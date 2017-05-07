using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ShowAwardPanelManger : FuncUIPanel
{
	public Action CloseCallBack;

	public static ShowAwardPanelManger _ins;

	public GameObject awardTabel;

	public GameObject resPreb;

	public GameObject itemPre;

	public GameObject skillPreb;

	public Transform jiangli;

	private bool _2DLock;

	private bool isCanBack;

	private bool isSecond;

	public new void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
		base.OnDisable();
	}

	public void OnDestroy()
	{
		if (ShowPlayerLevelManager.ins != null && ShowPlayerLevelManager.ins.gameObject.activeInHierarchy)
		{
			return;
		}
		if (HomeUpOpenBuilding.inst != null && HomeUpOpenBuilding.inst.gameObject.activeInHierarchy)
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
		ShowAwardPanelManger._ins = null;
		if (!NewbieGuideManage._instance.MainNewbieStay)
		{
			NewbieGuidePanel.CallLuaByStart();
		}
	}

	public override void Awake()
	{
		ShowAwardPanelManger._ins = this;
		this.init();
		this.jiangli.localPosition = new Vector3(0f, 1000f, 0f);
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ShowAwardPanelManager_SureClick, new EventManager.VoidDelegate(this.CloseThis));
	}

	public override void OnEnable()
	{
		this.isCanBack = false;
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
		base.OnEnable();
	}

	private void Start()
	{
		DieBall dieBall = PoolManage.Ins.CreatEffect("jiangli_UI", this.jiangli.position, Quaternion.identity, this.jiangli);
		dieBall.tr.localPosition = new Vector3(2f, -17f, 0f);
		dieBall.tr.localScale = Vector3.one;
		Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 8;
		}
	}

	public void CloseThis(GameObject go)
	{
		go.GetComponent<ButtonClick>().isSendLua = false;
		if (!this.isCanBack && !this.isSecond)
		{
			return;
		}
		FuncUIManager.inst.DestoryFuncUI("ShowAwardPanel");
		if (this.CloseCallBack != null)
		{
			this.CloseCallBack();
		}
	}

	public static void showAwardList()
	{
		FuncUIManager.inst.OpenFuncUI("ShowAwardPanel", SenceType.Other);
		AudioManage.inst.PlayAuido("draw", false);
		ShowAwardPanelManger._ins.StartCoroutine(ShowAwardPanelManger._ins.JiangLiItem());
	}

	[DebuggerHidden]
	public IEnumerator JiangLiItem()
	{
		ShowAwardPanelManger.<JiangLiItem>c__Iterator81 <JiangLiItem>c__Iterator = new ShowAwardPanelManger.<JiangLiItem>c__Iterator81();
		<JiangLiItem>c__Iterator.<>f__this = this;
		return <JiangLiItem>c__Iterator;
	}
}
