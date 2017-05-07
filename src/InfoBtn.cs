using System;
using UnityEngine;

public class InfoBtn : MonoBehaviour
{
	public InfoBtnType type;

	public UISprite uis;

	private void OnClick()
	{
		T_Info.inst.EventBtn(this.type);
	}
}
