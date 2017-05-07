using System;
using UnityEngine;

public class ItemPrefabScript : MonoBehaviour
{
	public UISprite itemIcon;

	public UISprite itemBG;

	public UILabel itemLabel;

	private void Awake()
	{
		this.itemIcon = base.transform.FindChild("icon").GetComponent<UISprite>();
		this.itemIcon.gameObject.AddComponent<ItemTipsShow2>();
		if (!this.itemIcon.gameObject.GetComponent<ButtonClick>())
		{
			this.itemIcon.gameObject.AddComponent<ButtonClick>();
		}
		ButtonClick component = this.itemIcon.gameObject.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.none;
		this.itemBG = base.transform.FindChild("bg").GetComponent<UISprite>();
		this.itemLabel = base.transform.FindChild("num").GetComponent<UILabel>();
	}
}
public class itemPrefabScript : MonoBehaviour
{
	public UISprite itemName;

	public UISprite itemQuality;

	public UILabel itemCount;

	public UILabel des;
}
