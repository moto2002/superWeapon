using System;
using UnityEngine;

public class MainLineTaskItem : MonoBehaviour
{
	public UILabel nameUILable;

	public UILabel tittleUILable;

	public GameObject LingQuButton;

	public UITable resTable;

	public UITable itemTable;

	public UISprite TaskTypeIcon;

	private void Awake()
	{
		this.nameUILable = base.transform.FindChild("name").GetComponent<UILabel>();
		this.tittleUILable = base.transform.FindChild("tittle").GetComponent<UILabel>();
		this.LingQuButton = base.transform.FindChild("lingquBtn").gameObject;
		this.resTable = base.transform.FindChild("Restable").GetComponent<UITable>();
		this.itemTable = base.transform.FindChild("ItemTable").GetComponent<UITable>();
		this.TaskTypeIcon = base.transform.FindChild("ItemBg/Sprite/TaskTypeIcon").GetComponent<UISprite>();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
