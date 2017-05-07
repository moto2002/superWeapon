using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemMixPanelManage : FuncUIPanel
{
	public enum ItemMixBtnType
	{
		DisplayItemMix,
		ClickItemMix,
		MixSend
	}

	[SerializeField]
	private GameObject itemInfo;

	[SerializeField]
	private GameObject itemMix;

	[SerializeField]
	private UILabel btnLabel;

	[SerializeField]
	private UILabel itemName;

	[SerializeField]
	private UILabel mixBtnLabel;

	[SerializeField]
	private UITable itemTabGrid;

	[SerializeField]
	private UITable itemMixGrid;

	[SerializeField]
	private GameObject itemPrefab;

	private Item selectItem;

	private int needNum;

	private bool isMoved;

	private List<Transform> allItemTabItem;

	private void Start()
	{
	}

	public override void Awake()
	{
		this.allItemTabItem = new List<Transform>();
	}

	private void Update()
	{
	}

	public override void OnEnable()
	{
		this.isMoved = false;
		this.itemInfo.transform.localPosition = Vector3.zero;
		this.itemMix.transform.localPosition = new Vector3(1000f, 0f, 0f);
		if (this.selectItem != null)
		{
			this.itemName.text = this.selectItem.Name;
			GameObject gameObject = this.itemTabGrid.CreateChildren(this.selectItem.Id + ":" + this.needNum, true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.GetComponentInChildren<UILabel>().text = string.Concat(new object[]
			{
				this.selectItem.Name,
				":",
				this.needNum,
				"当前拥有量为:",
				(!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.selectItem.Id)) ? 0 : HeroInfo.GetInstance().PlayerItemInfo[this.selectItem.Id]
			});
			this.allItemTabItem.Add(gameObject.transform);
			ItemMixSet itemMixSet = null;
			if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.selectItem.Id))
			{
				itemMixSet = UnitConst.GetInstance().ItemMixSetConst[this.selectItem.Id];
			}
			if (itemMixSet != null)
			{
				int num = 0;
				foreach (KeyValuePair<Item, int> current in itemMixSet.NeedItems)
				{
					GameObject gameObject2 = this.itemMixGrid.CreateChildren(current.Key.Id + ":" + current.Value, true);
					gameObject2.transform.localPosition = new Vector3((float)(num * 100), 0f, 0f);
					gameObject2.GetComponentInChildren<UILabel>().text = string.Concat(new object[]
					{
						current.Key.Name,
						":",
						current.Value,
						"当前拥有量为:",
						(!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current.Key.Id)) ? 0 : HeroInfo.GetInstance().PlayerItemInfo[current.Key.Id]
					});
					num++;
				}
			}
			if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.selectItem.Id))
			{
				this.btnLabel.text = "合成公式";
			}
			else
			{
				this.btnLabel.text = "获取方式";
			}
			if (this.selectItem.IsCanMix)
			{
				this.mixBtnLabel.text = "合成";
			}
			else
			{
				this.mixBtnLabel.text = "不可合成";
			}
		}
		this.itemMixGrid.Reposition();
		this.itemTabGrid.Reposition();
		base.OnEnable();
	}

	private void Clear()
	{
		this.itemMixGrid.HideAllChildren();
		this.itemTabGrid.HideAllChildren();
		this.allItemTabItem.Clear();
	}

	private void ClickItemMix(GameObject ga)
	{
		int key = int.Parse(ga.name.Split(new char[]
		{
			':'
		})[0]);
		int num = int.Parse(ga.name.Split(new char[]
		{
			':'
		})[1]);
		if (UnitConst.GetInstance().ItemConst.ContainsKey(key) && UnitConst.GetInstance().ItemMixSetConst.ContainsKey(key))
		{
			this.selectItem = UnitConst.GetInstance().ItemConst[key];
			this.needNum = num;
			this.itemMixGrid.HideAllChildren();
			if (this.allItemTabItem.Any((Transform a) => a.name == ga.name))
			{
				int num2 = this.allItemTabItem.FindIndex((Transform a) => a.name == ga.name);
				if (this.allItemTabItem.Count > num2 + 1)
				{
					this.allItemTabItem.RemoveAt(num2 + 1);
				}
				foreach (Transform current in this.itemTabGrid.GetChildren(true))
				{
					if (!this.allItemTabItem.Contains(current))
					{
						UnityEngine.Object.Destroy(current.gameObject);
					}
				}
			}
			else
			{
				GameObject gameObject = this.itemTabGrid.CreateChildren(this.selectItem.Id + ":" + this.needNum, true);
				gameObject.transform.localPosition = this.allItemTabItem[this.allItemTabItem.Count - 1].transform.localPosition + new Vector3(100f, 0f, 0f);
				gameObject.GetComponentInChildren<UILabel>().text = string.Concat(new object[]
				{
					this.selectItem.Name,
					":",
					this.needNum,
					"当前拥有量为:",
					(!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.selectItem.Id)) ? 0 : HeroInfo.GetInstance().PlayerItemInfo[this.selectItem.Id]
				});
				this.allItemTabItem.Add(gameObject.transform);
			}
			ItemMixSet itemMixSet = null;
			if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.selectItem.Id))
			{
				itemMixSet = UnitConst.GetInstance().ItemMixSetConst[this.selectItem.Id];
			}
			if (itemMixSet != null)
			{
				int num3 = 0;
				foreach (KeyValuePair<Item, int> current2 in itemMixSet.NeedItems)
				{
					GameObject gameObject2 = this.itemMixGrid.CreateChildren(current2.Key.Id + ":" + current2.Value, true);
					gameObject2.transform.localPosition = new Vector3((float)(num3 * 100), 0f, 0f);
					gameObject2.GetComponentInChildren<UILabel>().text = string.Concat(new object[]
					{
						current2.Key.Name,
						"::",
						current2.Value,
						"当前拥有量为:",
						(!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current2.Key.Id)) ? 0 : HeroInfo.GetInstance().PlayerItemInfo[current2.Key.Id]
					});
					num3++;
				}
			}
			if (this.selectItem.IsCanMix)
			{
				this.mixBtnLabel.text = "合成";
			}
			else
			{
				this.mixBtnLabel.text = "不可合成";
			}
			return;
		}
		LogManage.Log("道具表不包含此道具 或者 合成表不包含此");
	}

	public void Show(int selItemID, int _needNum)
	{
		this.selectItem = UnitConst.GetInstance().ItemConst[selItemID];
		this.needNum = _needNum;
		this.Clear();
		this.OnEnable();
	}

	public void EventHandler(GameObject ga, ItemMixPanelManage.ItemMixBtnType btnType)
	{
		switch (btnType)
		{
		case ItemMixPanelManage.ItemMixBtnType.DisplayItemMix:
			if (!this.isMoved)
			{
				TweenPosition.Begin(this.itemInfo, 1f, new Vector3(-280f, 0f, 0f));
				TweenPosition.Begin(this.itemMix, 1f, new Vector3(220f, 0f, 0f));
				this.isMoved = true;
			}
			break;
		case ItemMixPanelManage.ItemMixBtnType.ClickItemMix:
			this.ClickItemMix(ga);
			break;
		case ItemMixPanelManage.ItemMixBtnType.MixSend:
			LogManage.Log(this.selectItem.Name + "~~~~~~~`" + this.needNum);
			break;
		}
	}
}
