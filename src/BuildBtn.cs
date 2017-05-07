using System;
using UnityEngine;

public class BuildBtn : MonoBehaviour
{
	public BuildBtnType btnType = BuildBtnType.goBattle;

	private void Start()
	{
	}

	private void OnClick()
	{
		BuildPanelManager.inst.PressButton(this.btnType);
	}
}
