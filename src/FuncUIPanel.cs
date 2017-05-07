using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuncUIPanel : MonoBehaviour
{
	public Action ShowPanelCallback;

	public Action HidePanelCallback;

	public static bool IsPingBiButtonClick = false;

	public static List<FuncUIPanel> AllUIPanel = new List<FuncUIPanel>();

	public virtual void OnEnable()
	{
		FuncUIPanel.IsPingBiButtonClick = true;
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		if (this.ShowPanelCallback != null)
		{
			this.ShowPanelCallback();
		}
		if (UIManager.inst != null)
		{
			UIManager.inst.UIInUsed(true);
		}
		if (!FuncUIPanel.AllUIPanel.Contains(this))
		{
			FuncUIPanel.AllUIPanel.Add(this);
		}
		this.PlayOpenPanelUIAudio();
	}

	public virtual void PlayOpenPanelUIAudio()
	{
		LogManage.LogError("播放OpenUI~~~~~~~~~~~~~~~~~~" + base.gameObject.name);
		AudioManage.inst.PlayAuidoBySelf_2D("openUI", base.gameObject, false, 0uL);
	}

	public virtual void Awake()
	{
	}

	public virtual void OnDisable()
	{
		if (this.HidePanelCallback != null)
		{
			this.HidePanelCallback();
		}
		FuncUIPanel.AllUIPanel.Remove(this);
		for (int i = FuncUIPanel.AllUIPanel.Count - 1; i >= 0; i--)
		{
			if (!FuncUIPanel.AllUIPanel[i].gameObject.activeInHierarchy)
			{
				FuncUIPanel.AllUIPanel.RemoveAt(i);
			}
		}
		if (FuncUIPanel.AllUIPanel.Count <= 0)
		{
			if (CameraControl.inst)
			{
				CameraControl.inst.blur.enabled = false;
			}
			if (UIManager.inst != null)
			{
				UIManager.inst.UIInUsed(false);
			}
			if (HUDTextTool.inst)
			{
				HUDTextTool.inst.ClearTextQueue();
			}
		}
	}
}
