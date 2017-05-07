using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillKaCaoItem : IMonoBehaviour
{
	public int Index;

	public SkillCarteItem skillItem;

	public GameObject suo;

	public UILabel tiaojian;

	public bool isSkill;

	public List<int> ZhanYongIndexs = new List<int>();

	public override void Awake()
	{
		base.Awake();
		this.skillItem = base.transform.FindChild("SkillCarteItem").GetComponent<SkillCarteItem>();
		this.suo = base.transform.FindChild("Suo").gameObject;
		this.tiaojian = base.transform.FindChild("Suo/Label").GetComponent<UILabel>();
	}
}
