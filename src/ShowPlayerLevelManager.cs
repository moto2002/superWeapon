using System;
using UnityEngine;

public class ShowPlayerLevelManager : FuncUIPanel
{
	public UILabel level;

	public static ShowPlayerLevelManager ins;

	public Transform effectParent;

	private bool _2DLock;

	private void OnDestroy()
	{
		if (TopTenPanelManage._ins != null && TopTenPanelManage._ins.gameObject.activeInHierarchy)
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
		if (NewarmyInfo.inst != null && NewarmyInfo.inst.gameObject.activeInHierarchy)
		{
			return;
		}
		HUDTextTool.inst.GUIGameObject.SetActive(true);
		NewbieGuidePanel.CallLuaByStart();
	}

	public override void Awake()
	{
		ShowPlayerLevelManager.ins = this;
	}

	public override void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
		base.OnDisable();
	}

	public override void OnEnable()
	{
		AudioManage.inst.PlayAuidoBySelf_2D("levleup", base.gameObject, false, 0uL);
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
		EventManager.Instance.AddEvent(EventManager.EventType.ShowPlayerLevelPanel_CloseThis, new EventManager.VoidDelegate(this.CloseShowPlayer));
		base.OnEnable();
		Body_Model effectByName = PoolManage.Ins.GetEffectByName("shengli_effect", null);
		effectByName.tr.parent = this.effectParent;
		effectByName.ga.AddComponent<RenderQueueEdit>();
		effectByName.tr.localPosition = new Vector3(0f, 55f, 0f);
		Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("shengli_effect_star", null);
		effectByName2.tr.parent = base.transform;
		effectByName2.ga.AddComponent<RenderQueueEdit>();
		effectByName2.tr.localPosition = Vector3.zero;
		Camera_FingerManager.inst.DestroyDragCamera();
	}

	public void CloseShowPlayer(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		FuncUIManager.inst.DestoryFuncUI("ShowPlayerLevelPanel");
	}
}
