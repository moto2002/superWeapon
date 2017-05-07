using DG.Tweening;
using System;
using UnityEngine;

public class WinAniEvent : MonoBehaviour
{
	public GameObject[] SpAnimator;

	public Transform EffectTr;

	public GameObject DiJunLaiXi;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPlayAni()
	{
		this.SpAnimator[0].gameObject.SetActive(true);
		this.SpAnimator[1].gameObject.SetActive(true);
	}

	private void WinAniEnd()
	{
		Body_Model effectByName = PoolManage.Ins.GetEffectByName("huaxing", base.transform);
		effectByName.ga.layer = 8;
		effectByName.tr.localPosition = new Vector3(0f, 186f, -10f);
	}

	private void StarPlay()
	{
		Body_Model effectByName = PoolManage.Ins.GetEffectByName("shengli_starglow", base.transform);
		effectByName.ga.layer = 8;
		SettlementManager.inst.transform.DOShakePosition(0.3f, new Vector3(0f, -20f, 0f), 10, 90f, false);
	}

	private void DiJunLaiXiEffect()
	{
		this.DiJunLaiXi.SetActive(true);
	}
}
