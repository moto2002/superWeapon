using System;
using UnityEngine;

public class NewbieEffect : MonoBehaviour
{
	public UISprite Finger;

	[SerializeField]
	private DieBall yindao;

	private void Start()
	{
		DieBall dieBall = PoolManage.Ins.CreatEffect("xinshou_zhiyin", base.transform.position, Quaternion.identity, null);
		this.Finger.transform.localPosition = dieBall.tr.localPosition;
		dieBall.IsAutoDes = false;
		dieBall.ga.AddComponent<RenderQueueEdit>();
		dieBall.name = "mumu";
		dieBall.gameObject.layer = base.gameObject.layer;
		dieBall.transform.parent = base.transform;
		dieBall.transform.localPosition = Vector3.zero;
		this.yindao = dieBall;
		this.Finger.transform.localPosition = dieBall.tr.localPosition;
		this.Finger.GetComponent<TweenPosition>().from = new Vector3(dieBall.tr.localPosition.x - 30f, dieBall.tr.localPosition.y - 70f, 0f);
		this.Finger.GetComponent<TweenPosition>().to = new Vector3(dieBall.tr.localPosition.x - 50f, dieBall.tr.localPosition.y - 90f, 0f);
	}
}
