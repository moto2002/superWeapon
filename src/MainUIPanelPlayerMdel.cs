using System;
using UnityEngine;

public class MainUIPanelPlayerMdel : MonoBehaviour
{
	public Transform model_tr;

	private Body_Model solider;

	public void OnEnable()
	{
		if (this.solider)
		{
			Animation[] componentsInChildren = this.solider.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Animation animation = componentsInChildren[i];
				if (animation.GetClip("Idle") != null)
				{
					animation.Play("Idle");
				}
			}
		}
	}

	private void Start()
	{
		this.solider = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[1].bodyId, null);
		if (this.solider)
		{
			this.solider.tr.parent = base.transform;
			this.solider.tr.localPosition = this.model_tr.localPosition;
			this.solider.tr.localRotation = this.model_tr.localRotation;
			this.solider.tr.localScale = this.model_tr.localScale;
			Transform[] componentsInChildren = this.solider.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 31;
			}
			if (this.solider.BlueModel)
			{
				this.solider.BlueModel.gameObject.SetActive(true);
			}
			if (this.solider.RedModel)
			{
				this.solider.RedModel.gameObject.SetActive(false);
			}
			if (this.solider.Red_DModel)
			{
				this.solider.Red_DModel.gameObject.SetActive(false);
			}
			if (this.solider.Blue_DModel)
			{
				this.solider.Blue_DModel.gameObject.SetActive(false);
			}
			Animation[] componentsInChildren2 = this.solider.GetComponentsInChildren<Animation>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Animation animation = componentsInChildren2[j];
				if (animation.GetClip("Idle") != null)
				{
					animation.Play("Idle");
				}
			}
		}
	}
}
