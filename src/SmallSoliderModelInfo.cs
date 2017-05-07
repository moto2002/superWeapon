using System;
using UnityEngine;

public class SmallSoliderModelInfo : MonoBehaviour
{
	public Body_Model smodel;

	public static SmallSoliderModelInfo ins;

	public GameObject effect;

	[HideInInspector]
	public int HaveGotFightKey;

	public void Awake()
	{
		SmallSoliderModelInfo.ins = this;
	}

	public void ShowGotoFightSolider(int key)
	{
		if (this.HaveGotFightKey == key)
		{
			return;
		}
		this.HaveGotFightKey = key;
		SepcialSoliderPanel.ins.funcArmy.SetActive(false);
		if (this.smodel == null)
		{
			this.smodel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[key].bodyId, base.transform);
			this.smodel.tr.localScale = new Vector3(200f, 200f, 200f);
			this.smodel.tr.localPosition = new Vector3(0f, 38f, -348f);
			this.smodel.tr.localRotation = Quaternion.Euler(3.4f, 148f, 0f);
			Transform[] componentsInChildren = this.smodel.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 5;
			}
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.smodel.ga);
			compentIfNoAddOne.SetAnimaInfo();
			compentIfNoAddOne.AnimPlay("Idle");
			if (this.smodel.BlueModel)
			{
				this.smodel.BlueModel.gameObject.SetActive(true);
			}
			if (this.smodel.RedModel)
			{
				this.smodel.RedModel.gameObject.SetActive(false);
			}
		}
		else
		{
			this.smodel.DesInsInPool();
			this.smodel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[key].bodyId, base.transform);
			this.smodel.tr.localScale = new Vector3(200f, 200f, 200f);
			this.smodel.tr.localPosition = new Vector3(0f, 38f, -348f);
			this.smodel.tr.localRotation = Quaternion.Euler(3.4f, 178.6f, 0f);
			Transform[] componentsInChildren2 = this.smodel.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = 5;
			}
			Ani_CharacterControler compentIfNoAddOne2 = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.smodel.ga);
			compentIfNoAddOne2.SetAnimaInfo();
			compentIfNoAddOne2.AnimPlay("Idle");
			if (this.smodel.BlueModel)
			{
				this.smodel.BlueModel.gameObject.SetActive(true);
			}
			if (this.smodel.RedModel)
			{
				this.smodel.RedModel.gameObject.SetActive(false);
			}
		}
	}
}
