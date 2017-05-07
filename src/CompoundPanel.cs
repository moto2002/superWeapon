using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CompoundPanel : FuncUIPanel
{
	public GameObject NewCompound;

	public static CompoundPanel ins;

	public UILabel NameLabel;

	public UILabel NumberLabel;

	public UISprite CompoundLogin;

	public UISprite debrislogion;

	public UISprite debrisSprite;

	public UISprite CompoundSprite;

	public UILabel differenceLabel;

	public UILabel debrisName;

	public UILabel moneyLabel;

	private ItemMixSet MixSet;

	private Item itemId;

	private int PlayerItemNum;

	private int CompoundItemNum;

	private int ItemID;

	public void OnDestroy()
	{
		CompoundPanel.ins = null;
	}

	public override void Awake()
	{
		CompoundPanel.ins = this;
	}

	public void OnDestyPanel()
	{
		base.gameObject.SetActive(false);
	}

	public void OnPanelShow()
	{
		base.gameObject.SetActive(true);
	}

	public void OnCompound(int id, int Number)
	{
		this.CompoundLogin.spriteName = UnitConst.GetInstance().ItemConst[id].IconId;
		Item item = UnitConst.GetInstance().ItemConst[id];
		this.CompoundSprite.spriteName = this.CompoundLogin.spriteName;
		this.NameLabel.text = UnitConst.GetInstance().ItemConst[id].Name;
		if (UnitConst.GetInstance().ItemConst.ContainsKey(id) && UnitConst.GetInstance().ItemMixSetConst.ContainsKey(id))
		{
			this.itemId = UnitConst.GetInstance().ItemConst[id];
		}
		if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.itemId.Id))
		{
			this.MixSet = UnitConst.GetInstance().ItemMixSetConst[this.itemId.Id];
		}
		this.moneyLabel.text = this.MixSet.Gold.ToString();
		if (this.MixSet != null)
		{
			foreach (KeyValuePair<Item, int> current in this.MixSet.NeedItems)
			{
				this.ItemID = current.Key.Id;
				this.debrisName.text = current.Key.Name;
				this.debrislogion.spriteName = current.Key.IconId;
				this.debrisSprite.spriteName = current.Key.IconId;
				this.debrislogion.GetComponent<UIButton>().normalSprite = current.Key.IconId;
				this.CompoundItemNum = current.Value;
			}
		}
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(id) && HeroInfo.GetInstance().PlayerItemInfo[id] > 0)
		{
			this.PlayerItemNum = HeroInfo.GetInstance().PlayerItemInfo[id];
		}
		else
		{
			this.PlayerItemNum = 0;
		}
		this.differenceLabel.text = this.PlayerItemNum + "/" + this.CompoundItemNum.ToString();
	}

	public void OnComoundClick()
	{
		if (this.PlayerItemNum > this.CompoundItemNum)
		{
			this.OnComoundOk();
		}
		else
		{
			this.OnComounddefeated();
		}
	}

	public void OnComoundOk()
	{
		CSItemMix cSItemMix = new CSItemMix();
		cSItemMix.itemId = this.ItemID;
		cSItemMix.itemNum = this.PlayerItemNum;
		ClientMgr.GetNet().SendHttp(1102, cSItemMix, null, null);
	}

	public void OnComounddefeated()
	{
		compounddebrisPanel component = this.NewCompound.GetComponent<compounddebrisPanel>();
		component.OnCompounddebrisInfo(this.ItemID, this.debrisSprite.spriteName.ToString(), this.debrisName.text, this.PlayerItemNum);
		base.Invoke("OnPanelCut", 0.5f);
	}

	public void OnPanelCut()
	{
		base.gameObject.SetActive(false);
		this.NewCompound.gameObject.SetActive(true);
	}
}
