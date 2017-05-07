using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Body_Model : ModelByBundle
{
	public Transform BlueModel;

	public Transform RedModel;

	public Transform Blue_DModel;

	public Transform Red_DModel;

	public List<DieBall> Effects = new List<DieBall>();

	public override void Awake()
	{
		base.Awake();
		this.BlueModel = GameTools.GetTranformChildByName(this.tr, "BLUE");
		this.Blue_DModel = GameTools.GetTranformChildByName(this.tr, "B_D");
		this.RedModel = GameTools.GetTranformChildByName(this.tr, "RED");
		this.Red_DModel = GameTools.GetTranformChildByName(this.tr, "R_D");
		this.HideTrail();
	}

	public void HideTrail()
	{
	}

	public void DisplayTrail()
	{
	}

	public override void SetActive(bool isActiveSelf)
	{
		if (!isActiveSelf)
		{
			if (this.BlueModel)
			{
				this.BlueModel.gameObject.SetActive(true);
			}
			if (this.RedModel)
			{
				this.RedModel.gameObject.SetActive(false);
			}
			this.HideTrail();
		}
		this.ga.SetActive(isActiveSelf);
		base.IsActive = isActiveSelf;
	}

	public override void DesInsInPool()
	{
		for (int i = 0; i < this.Effects.Count; i++)
		{
			if (this.Effects[0])
			{
				this.Effects[0].DesInPool();
			}
		}
		this.Effects.Clear();
		PoolManage.Ins.DesSpawn_bundleModelPool(this.tr);
	}

	public void DesInsInPool(float second)
	{
		if (second > 0f)
		{
			base.StartCoroutine(this.WaitSec(second));
		}
		else
		{
			this.DesInsInPool();
		}
	}

	[DebuggerHidden]
	private IEnumerator WaitSec(float sec)
	{
		Body_Model.<WaitSec>c__Iterator28 <WaitSec>c__Iterator = new Body_Model.<WaitSec>c__Iterator28();
		<WaitSec>c__Iterator.sec = sec;
		<WaitSec>c__Iterator.<$>sec = sec;
		<WaitSec>c__Iterator.<>f__this = this;
		return <WaitSec>c__Iterator;
	}
}
