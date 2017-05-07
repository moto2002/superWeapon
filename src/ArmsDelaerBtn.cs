using System;
using UnityEngine;

public class ArmsDelaerBtn : MonoBehaviour
{
	public ArmsDealerPanel.ArmsDealerBtnType btnType;

	private void Start()
	{
	}

	private void OnClick()
	{
		ArmsDealerPanel.ins.EventBtn(this.btnType, base.gameObject);
	}
}
