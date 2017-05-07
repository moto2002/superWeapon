using System;
using UnityEngine;

public class AdjutantBtn : MonoBehaviour
{
	public AdjutantPanel.btnType btnType;

	private void Update()
	{
	}

	private void OnClick()
	{
		FuncUIManager.inst.AdjutantPanel.EventBtnType(base.gameObject, this.btnType);
	}
}
