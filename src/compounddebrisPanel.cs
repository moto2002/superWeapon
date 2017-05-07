using System;
using System.Collections.Generic;
using UnityEngine;

public class compounddebrisPanel : MonoBehaviour
{
	public UISprite CompoundSprite;

	public UISprite debrisSp;

	public UISprite debrisLogion;

	public UISprite CompoundSp;

	public UILabel nameLabel;

	public UILabel debrisNum;

	public UILabel debrisName;

	public GameObject CompoundPanel;

	public static compounddebrisPanel instance;

	private Item item;

	private ItemMixSet MixSet;

	public void OnDestroy()
	{
		compounddebrisPanel.instance = null;
	}

	private void Start()
	{
		compounddebrisPanel.instance = this;
	}

	public void OnCompounddebrisInfo(int ItemSuiId, string debrisSpritename, string ComName, int Num)
	{
		this.item = UnitConst.GetInstance().ItemConst[ItemSuiId];
		if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.item.Id))
		{
			this.MixSet = UnitConst.GetInstance().ItemMixSetConst[this.item.Id];
		}
		foreach (KeyValuePair<Item, int> current in this.MixSet.NeedItems)
		{
			this.debrisSp.spriteName = current.Key.IconId;
			this.debrisLogion.spriteName = current.Key.IconId;
			this.debrisName.text = current.Key.Name;
		}
		base.gameObject.SetActive(true);
		this.CompoundPanel.SetActive(false);
		this.CompoundSprite.spriteName = debrisSpritename;
		this.CompoundSp.spriteName = debrisSpritename;
		this.nameLabel.text = ComName;
		this.debrisNum.text = Num.ToString();
	}

	public void BackClick()
	{
		base.gameObject.SetActive(false);
		this.CompoundPanel.SetActive(true);
	}
}
