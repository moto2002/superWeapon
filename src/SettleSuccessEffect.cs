using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SettleSuccessEffect : MonoBehaviour
{
	public Transform effectParent;

	public GameObject waiGuang;

	public Transform Sheng;

	public Transform Li;

	public Transform Bac;

	public Transform EffectParent;

	public Transform top;

	public SettleSuccessAnimaton2 successStar;

	private void Start()
	{
	}

	private void OnEnable()
	{
		this.AniMation();
	}

	private void AniMation()
	{
		this.Bac.localPosition = new Vector3(0f, 1000f, 0f);
		this.Sheng.localPosition = new Vector3(-1000f, 0f, 0f);
		this.Bac.DOLocalMoveY(220f, 0.3f, false).OnComplete(delegate
		{
			this.Bac.DOShakePosition(0.5f, new Vector3(0f, 20f, 0f), 20, 90f, false);
			base.transform.DOShakePosition(0.5f, new Vector3(0f, -10f, 0f), 20, 90f, false);
			this.Bac.DOLocalMoveY(420f, 0.2f, false).SetDelay(1f).OnComplete(delegate
			{
				Body_Model effectByName = PoolManage.Ins.GetEffectByName("shengli_effect", null);
				effectByName.tr.parent = this.EffectParent;
				effectByName.ga.AddComponent<RenderQueueEdit>();
				effectByName.tr.localPosition = Vector3.zero;
				Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("shengli_effect_star", null);
				effectByName2.tr.parent = this.Bac;
				effectByName2.ga.AddComponent<RenderQueueEdit>();
				effectByName2.tr.localPosition = Vector3.zero;
				this.Bac.DOShakePosition(0.5f, new Vector3(0f, 35f, 0f), 20, 90f, false);
			});
			this.Sheng.DOLocalMoveX(-30f, 0.2f, false).OnComplete(delegate
			{
				this.Sheng.DOLocalMoveX(-70f, 0.16f, false);
			});
			this.TanChuSettleSuccessUI();
		});
	}

	private void OnGUI()
	{
	}

	public void Shake()
	{
		base.transform.DOShakePosition(0.2f, 10f, 20, 90f, false);
	}

	public void TanChuSettleSuccessUI()
	{
		CoroutineInstance.DoJob(this.IEDisplayUI());
	}

	[DebuggerHidden]
	private IEnumerator IEDisplayUI()
	{
		SettleSuccessEffect.<IEDisplayUI>c__Iterator7A <IEDisplayUI>c__Iterator7A = new SettleSuccessEffect.<IEDisplayUI>c__Iterator7A();
		<IEDisplayUI>c__Iterator7A.<>f__this = this;
		return <IEDisplayUI>c__Iterator7A;
	}

	public void DisplayWaiGuangUI()
	{
		this.waiGuang.transform.DOLocalMoveX(1f, 0.2f, false);
	}
}
