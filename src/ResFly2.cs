using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ResFly2 : MonoBehaviour
{
	public delegate void voidDelegate(int resNum);

	public Camera startCam;

	public Camera endCam;

	public TweenScale TS;

	public float duration = 0.6f;

	public ResFly2.voidDelegate callBack;

	public Transform targ;

	private Transform curTrans;

	private int resNum;

	private bool startFly;

	public static bool isZhuanshiTrans;

	private void Start()
	{
		this.startCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
		this.curTrans = base.transform;
		this.TS = base.GetComponent<TweenScale>();
	}

	private void Update()
	{
		if (this.startFly)
		{
			try
			{
				Vector3 vector = this.endCam.WorldToScreenPoint(this.targ.position);
				Vector3 a = this.startCam.ScreenToWorldPoint(new Vector3(vector.x, vector.y, this.startCam.transform.position.z));
				float num = Vector3.Distance(a, this.curTrans.position);
				float d = num / this.duration;
				this.curTrans.position = (a - this.curTrans.position).normalized * d * Time.deltaTime + this.curTrans.position;
				this.duration -= Time.deltaTime;
				if (this.duration <= 0f)
				{
					this.startFly = false;
					if (this.callBack != null)
					{
						this.callBack(this.resNum);
					}
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			catch (Exception var_4_10F)
			{
				this.startFly = false;
				if (this.callBack != null)
				{
					this.callBack(this.resNum);
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	public void FlyTo(Transform UITrans, Vector3 targetPos, int resNum)
	{
		if (UITrans)
		{
			this.resNum = resNum;
			this.endCam = NGUITools.FindCameraForLayer(UITrans.gameObject.layer);
			this.targ = UITrans;
			base.transform.DOLocalMove(base.transform.localPosition + targetPos, 0.5f, false).OnComplete(new TweenCallback(this.onNormalSized));
		}
		else if (this.callBack != null)
		{
			this.callBack(resNum);
		}
	}

	private void onBiggerEnd()
	{
		this.TS.onFinished.Clear();
		this.TS.onFinished.Add(new EventDelegate(new EventDelegate.Callback(this.onNormalSized)));
		this.TS.PlayReverse();
	}

	private void onNormalSized()
	{
		this.startFly = true;
	}

	[DebuggerHidden]
	public static IEnumerator CreateRes(Transform tar, ResType res, int num, Action Callback = null, ResFly2.voidDelegate callBackByRes = null)
	{
		ResFly2.<CreateRes>c__Iterator76 <CreateRes>c__Iterator = new ResFly2.<CreateRes>c__Iterator76();
		<CreateRes>c__Iterator.tar = tar;
		<CreateRes>c__Iterator.num = num;
		<CreateRes>c__Iterator.res = res;
		<CreateRes>c__Iterator.callBackByRes = callBackByRes;
		<CreateRes>c__Iterator.Callback = Callback;
		<CreateRes>c__Iterator.<$>tar = tar;
		<CreateRes>c__Iterator.<$>num = num;
		<CreateRes>c__Iterator.<$>res = res;
		<CreateRes>c__Iterator.<$>callBackByRes = callBackByRes;
		<CreateRes>c__Iterator.<$>Callback = Callback;
		return <CreateRes>c__Iterator;
	}

	[DebuggerHidden]
	public static IEnumerator CreateRes2(Transform tar, ResType res, int num, Transform tr, Action Callback = null, ResFly2.voidDelegate callBackByRes = null)
	{
		ResFly2.<CreateRes2>c__Iterator77 <CreateRes2>c__Iterator = new ResFly2.<CreateRes2>c__Iterator77();
		<CreateRes2>c__Iterator.tar = tar;
		<CreateRes2>c__Iterator.num = num;
		<CreateRes2>c__Iterator.res = res;
		<CreateRes2>c__Iterator.callBackByRes = callBackByRes;
		<CreateRes2>c__Iterator.tr = tr;
		<CreateRes2>c__Iterator.Callback = Callback;
		<CreateRes2>c__Iterator.<$>tar = tar;
		<CreateRes2>c__Iterator.<$>num = num;
		<CreateRes2>c__Iterator.<$>res = res;
		<CreateRes2>c__Iterator.<$>callBackByRes = callBackByRes;
		<CreateRes2>c__Iterator.<$>tr = tr;
		<CreateRes2>c__Iterator.<$>Callback = Callback;
		return <CreateRes2>c__Iterator;
	}
}
