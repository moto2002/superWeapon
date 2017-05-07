using System;
using UnityEngine;

public class topTenPrizeScript : MonoBehaviour
{
	public UILabel title;

	public UILabel count;

	public GameObject itemPre;

	public GameObject resprefab;

	public UITable itemTable;

	public UITable resTable;

	public UILabel award;

	public GameObject getBtn;

	private void Start()
	{
		this.title.text = LanguageManage.GetTextByKey("我的荣誉排行", "others") + ":";
		this.award.text = LanguageManage.GetTextByKey("奖励", "others") + ":";
	}

	private void Update()
	{
	}
}
