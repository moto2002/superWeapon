using System;
using UnityEngine;

public class ItemUnit : MonoBehaviour
{
	public UILabel num;

	public UILabel name_Client;

	public UISprite itemSprite;

	public UISprite itemQuSprite;

	public int Officertype;

	public GameObject selecting;

	public UISprite oprforSp;

	public int index;

	[HideInInspector]
	public Item item;

	[HideInInspector]
	public int itemNum;

	public int id;

	public long ID;

	private void Awake()
	{
		if (base.transform.FindChild("name") != null)
		{
			this.name_Client = base.transform.FindChild("name").GetComponent<UILabel>();
		}
		if (base.transform.FindChild("Label") != null)
		{
			this.num = base.transform.FindChild("Label").GetComponent<UILabel>();
		}
		else
		{
			this.num = base.transform.FindChild("num").GetComponent<UILabel>();
		}
		if (base.transform.FindChild("Sprite") != null)
		{
			this.itemSprite = base.transform.FindChild("Sprite").GetComponent<UISprite>();
		}
		else
		{
			this.itemSprite = base.gameObject.GetComponent<UISprite>();
		}
		if (base.transform.FindChild("qua") != null)
		{
			this.itemQuSprite = base.transform.FindChild("qua").GetComponent<UISprite>();
		}
		else
		{
			this.itemQuSprite = base.transform.GetComponent<UISprite>();
		}
		if (base.transform.FindChild("selecting") != null)
		{
			this.selecting = base.transform.FindChild("selecting").gameObject;
		}
		if (base.transform.FindChild("ClickBg") != null)
		{
			this.oprforSp = base.transform.FindChild("ClickBg").GetComponent<UISprite>();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
