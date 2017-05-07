using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Mining_Car : IMonoBehaviour
{
	private T_Tower Home;

	private Body_Model Body;

	private Animation Ani;

	private Transform TrueBody;

	private Transform Kuang;

	private Vector3 Minings;

	private float speed;

	private Tweener tween;

	public void SetInfo(T_Tower _home)
	{
		this.Minings = new Vector3(-10f, 0f, -23f);
		this.Home = _home;
		this.tr.position = this.Home.tr.position;
		this.tr.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		this.Body = base.GetComponent<Body_Model>();
		if (this.Body.BlueModel)
		{
			this.Body.BlueModel.gameObject.SetActive(this.Home.modelClore == Enum_ModelColor.Blue);
		}
		if (this.Body.RedModel)
		{
			this.Body.RedModel.gameObject.SetActive(this.Home.modelClore == Enum_ModelColor.Red);
		}
		if (this.Home.modelClore == Enum_ModelColor.Blue)
		{
			this.TrueBody = this.Body.BlueModel;
		}
		else
		{
			this.TrueBody = this.Body.RedModel;
		}
		if (this.TrueBody)
		{
			this.Ani = this.TrueBody.GetComponent<Animation>();
			this.Kuang = this.TrueBody.FindChild("kuang");
		}
		this.speed = 3f;
		this.DoJob();
		FightHundler.OnFighting += new Action(this.PlayerHandle_OnFighting);
	}

	private void PlayerHandle_OnFighting()
	{
		PoolManage.Ins.CreatEffect("chubing", this.tr.position, Quaternion.identity, null);
		if (this.tween != null)
		{
			this.tween.Kill(false);
		}
		base.StopCoroutine(this.GoToMining());
		GameTools.DelayMethod(0.3f, delegate
		{
			if (this.Home)
			{
				this.tr.localPosition = new Vector3(this.Home.tr.localPosition.x + 4f, 0f, this.Home.tr.localPosition.z);
				this.tr.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
				PoolManage.Ins.CreatEffect("chubing", this.tr.position, Quaternion.identity, null);
				this.tr.DOLocalMoveX(this.Home.tr.localPosition.x, 1.6f, false).OnComplete(delegate
				{
					this.Body.DesInsInPool();
					this.Home.mining_Car = null;
				});
			}
			else if (this.Body)
			{
				this.Body.DesInsInPool();
			}
		});
	}

	private void OnDisable()
	{
		FightHundler.OnFighting -= new Action(this.PlayerHandle_OnFighting);
	}

	private void DoJob()
	{
		base.StartCoroutine(this.GoToMining());
	}

	[DebuggerHidden]
	private IEnumerator GoToMining()
	{
		Mining_Car.<GoToMining>c__Iterator2C <GoToMining>c__Iterator2C = new Mining_Car.<GoToMining>c__Iterator2C();
		<GoToMining>c__Iterator2C.<>f__this = this;
		return <GoToMining>c__Iterator2C;
	}
}
