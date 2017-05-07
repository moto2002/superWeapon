using System;
using System.Collections.Generic;
using UnityEngine;

public class VipContent_0 : MonoBehaviour
{
	public UITable vip0Table;

	public void Awake()
	{
		this.InitEvent();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.VIPPanel_ChongZhi, new EventManager.VoidDelegate(this.VIPPanel_ChongZhi));
	}

	private void VIPPanel_ChongZhi(GameObject go)
	{
		Vip_ShopItem component = go.GetComponent<Vip_ShopItem>();
		if (component != null)
		{
			ShopHandler.CS_ShopBuyRMB(int.Parse(go.name), 0, null);
		}
	}

	public void InitInfo()
	{
		foreach (KeyValuePair<int, ShopItem> current in UnitConst.GetInstance().shopItem)
		{
			if (current.Value.IsUIShow)
			{
				GameObject gameObject = this.vip0Table.CreateChildren(current.Key.ToString(), true);
				gameObject.GetComponent<Vip_ShopItem>().InitInfo(current.Value);
			}
		}
		this.vip0Table.Reposition();
	}
}
