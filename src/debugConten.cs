using DG.Tweening;
using msg;
using System;
using UnityEngine;

public class debugConten : MonoBehaviour
{
	public GameObject ga1;

	public GameObject ga2;

	public GameObject ga3;

	public GameObject ga4;

	public GameObject ga6;

	public GameObject drag;

	private void Start()
	{
		UIEventListener.Get(this.ga1).onClick = delegate(GameObject ga)
		{
			CSDebug cSDebug = new CSDebug();
			cSDebug.id.Add(1);
			ClientMgr.GetNet().SendHttp(9002, cSDebug, null, null);
		};
		UIEventListener.Get(this.ga2).onClick = delegate(GameObject ga)
		{
			CSDebug cSDebug = new CSDebug();
			cSDebug.id.Add(2);
			cSDebug.id.Add(5);
			ClientMgr.GetNet().SendHttp(9002, cSDebug, null, null);
		};
		UIEventListener.Get(this.ga3).onClick = delegate(GameObject ga)
		{
			CSDebug cSDebug = new CSDebug();
			cSDebug.id.Add(3);
			ClientMgr.GetNet().SendHttp(9002, cSDebug, null, null);
		};
		UIEventListener.Get(this.ga4).onClick = delegate(GameObject ga)
		{
			CSDebug cSDebug = new CSDebug();
			cSDebug.id.Add(4);
			ClientMgr.GetNet().SendHttp(9002, cSDebug, null, null);
		};
		UIEventListener.Get(this.ga6).onClick = delegate(GameObject ga)
		{
			CSDebug cSDebug = new CSDebug();
			cSDebug.id.Add(6);
			ClientMgr.GetNet().SendHttp(9002, cSDebug, null, null);
		};
	}

	public void OnClickCloseBtn()
	{
		base.transform.DOScale(Vector3.zero, 1f).OnComplete(delegate
		{
			this.drag.SetActive(true);
			if (UIManager.inst != null)
			{
				UIManager.inst.UIInUsed(false);
			}
		});
	}
}
