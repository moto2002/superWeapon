using System;
using UnityEngine;

public class ShowUpGradeInfo : MonoBehaviour
{
	public UILabel UpGradeName;

	public UILabel changCount;

	public Body_Model effect;

	public void Awake()
	{
		if (this.effect == null)
		{
			this.effect = PoolManage.Ins.GetModelByBundleByName("shuxingtisheng", base.transform);
			this.effect.ga.AddComponent<RenderQueueEdit>();
			this.effect.tr.localPosition = new Vector3(80f, 0f, 0f);
			this.effect.tr.localScale = Vector3.one;
			this.effect.SetActive(false);
		}
	}

	public void PlayEffect()
	{
		if (this.effect)
		{
			this.effect.SetActive(false);
			this.effect.SetActive(true);
		}
	}
}
