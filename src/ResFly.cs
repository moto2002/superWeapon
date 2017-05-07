using DG.Tweening;
using System;
using UnityEngine;

public class ResFly : MonoBehaviour
{
	public delegate void voidDelegate(int resNum);

	private bool FlyByCommonCamera;

	public Camera startCam;

	public Camera endCam;

	public TweenScale TS;

	public float duration = 1f;

	public ResFly.voidDelegate callBack;

	public TextMesh text;

	public Transform targ;

	private Transform curTrans;

	private int resNum;

	private bool startFly;

	private void Start()
	{
		try
		{
			this.startCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			this.curTrans = base.transform;
		}
		catch (Exception ex)
		{
			Debug.LogError("resfly Start error:: " + ex.ToString());
		}
	}

	private void Update()
	{
		try
		{
			if (this.startFly)
			{
				if (this.endCam == null || this.targ == null)
				{
					if (this.callBack != null)
					{
						this.callBack(this.resNum);
					}
					UnityEngine.Object.Destroy(base.gameObject);
					this.startFly = false;
					return;
				}
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
			if (this.FlyByCommonCamera)
			{
				float num2 = Vector3.Distance(this.targ.position, this.curTrans.position);
				float d2 = num2 / this.duration;
				this.curTrans.position = (this.targ.position - this.curTrans.position).normalized * d2 * Time.deltaTime + this.curTrans.position;
				if (this.curTrans.position.y < 0f)
				{
					this.curTrans.position = new Vector3(this.curTrans.position.x, 0.1f, this.curTrans.position.z);
				}
				this.duration -= Time.deltaTime;
				if (this.duration <= 0f)
				{
					this.FlyByCommonCamera = false;
					if (this.callBack != null)
					{
						this.callBack(this.resNum);
					}
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("ResFly Update " + ex.ToString());
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void FlyTo(Transform UITrans, int resNum)
	{
		this.resNum = resNum;
		LogManage.Log("收取资源数目是：" + resNum);
		this.text.text = resNum.ToString();
		this.endCam = NGUITools.FindCameraForLayer(UITrans.gameObject.layer);
		this.targ = UITrans;
		this.TS.from = new Vector3(0.4f, 0.4f, 0.4f);
		this.TS.to = new Vector3(0.8f, 0.8f, 0.8f);
		this.TS.duration = 0.5f;
		this.TS.onFinished.Add(new EventDelegate(new EventDelegate.Callback(this.onBiggerEnd)));
		this.TS.PlayForward();
	}

	public void FlyTo2(Transform UITrans, Vector3 targetPos, int resNum)
	{
		try
		{
			this.resNum = resNum;
			this.text.text = resNum.ToString();
			this.endCam = NGUITools.FindCameraForLayer(UITrans.gameObject.layer);
			this.targ = UITrans;
			base.transform.DOMove(base.transform.position + targetPos, 0.5f, false).OnComplete(new TweenCallback(this.onNormalSized));
		}
		catch (Exception ex)
		{
			Debug.LogError("resfly fly2 error:: " + ex.ToString());
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
		try
		{
			if (this.startCam.Equals(this.endCam))
			{
				this.FlyByCommonCamera = true;
				this.startFly = false;
			}
			else
			{
				this.FlyByCommonCamera = false;
				this.startFly = true;
			}
			ResRotate component = base.GetComponent<ResRotate>();
			if (component)
			{
				component.enabled = true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("resfly onNormalSized error:: " + ex.ToString());
		}
	}
}
