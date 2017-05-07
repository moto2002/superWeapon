using System;
using UnityEngine;

public class SkillDuiHuanItem : MonoBehaviour
{
	public SkillCarteItem item;

	public UILabel Num;

	private void Awake()
	{
		this.item = base.transform.FindChild("SkillCarteItem").GetComponent<SkillCarteItem>();
		this.Num = base.transform.FindChild("Label").GetComponent<UILabel>();
	}

	public void ShowItem(Skill sk)
	{
		this.Num.text = sk.needChips.ToString();
		this.item.ShowItem(sk.id);
	}
}
