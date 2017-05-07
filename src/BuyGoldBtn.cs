using System;
using UnityEngine;

public class BuyGoldBtn : MonoBehaviour
{
	public BuyGoldBtnType type = BuyGoldBtnType.Close;

	private void OnPress(bool isPress)
	{
		if (!isPress)
		{
			BuyGoldPanelManager.ins.Click(this.type);
		}
	}
}
