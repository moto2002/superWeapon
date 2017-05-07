using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class GameStart_C : MonoBehaviour
{
	public Transform cam;

	public void Awake()
	{
		this.cam = base.transform;
	}

	private void Start()
	{
		this.cam.localPosition = new Vector3(187f, 14f, 107f);
		this.cam.localRotation = Quaternion.Euler(new Vector3(26f, -48f, 0f));
		this.cam.localScale = Vector3.one;
		base.StartCoroutine(this.DoCameraJob());
		base.StartCoroutine(this.DoTankJob());
	}

	[DebuggerHidden]
	private IEnumerator DoCameraJob()
	{
		GameStart_C.<DoCameraJob>c__Iterator45 <DoCameraJob>c__Iterator = new GameStart_C.<DoCameraJob>c__Iterator45();
		<DoCameraJob>c__Iterator.<>f__this = this;
		return <DoCameraJob>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator DoTankJob()
	{
		GameStart_C.<DoTankJob>c__Iterator46 <DoTankJob>c__Iterator = new GameStart_C.<DoTankJob>c__Iterator46();
		<DoTankJob>c__Iterator.<>f__this = this;
		return <DoTankJob>c__Iterator;
	}

	private void DoMove_DesSpw(Body_Model tank, Vector3 pos, int sec)
	{
		if (tank)
		{
			tank.tr.DOLocalMove(pos, (float)sec, false).OnComplete(delegate
			{
				if (tank)
				{
					tank.DesInsInPool();
				}
			});
		}
	}

	public Body_Model BuildTank(Vector3 pos, Vector3 rota, Vector3 scale, int index, int num)
	{
		if (!UnitConst.IsHaveInstance())
		{
			return null;
		}
		if (UnitConst.GetInstance().soldierConst.Length == 0)
		{
			LogManage.LogError(" UnitConst.GetInstance().soldierConst.Length == 0   ");
			return null;
		}
		Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[index].bodyId, null);
		if (modelByBundleByName == null)
		{
			LogManage.LogError(" UnitConst.GetInstance().soldierConst[index].bodyId -----------   " + UnitConst.GetInstance().soldierConst[index].bodyId);
			return null;
		}
		modelByBundleByName.tr.localPosition = pos;
		modelByBundleByName.tr.localRotation = Quaternion.Euler(rota);
		modelByBundleByName.tr.localScale = scale;
		if (index == 7)
		{
			Animation[] componentsInChildren = modelByBundleByName.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Animation animation = componentsInChildren[i];
				if (animation.GetClip("Run") != null)
				{
					animation.Play("Run");
				}
			}
		}
		DieBall dieBall = null;
		switch (index)
		{
		case 1:
			dieBall = PoolManage.Ins.CreatEffect("huichen_S", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 2:
			dieBall = PoolManage.Ins.CreatEffect("huichen_S", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 3:
		case 5:
		case 6:
			dieBall = PoolManage.Ins.CreatEffect("huichen_M", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 4:
			dieBall = PoolManage.Ins.CreatEffect("huichen_L", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		}
		if (dieBall)
		{
			modelByBundleByName.Effects.Add(dieBall);
		}
		if (num == 0)
		{
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
		}
		else if (num == 1)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
		}
		else if (num == 3)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
		}
		else if (num == 4)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
		}
		return modelByBundleByName;
	}
}
