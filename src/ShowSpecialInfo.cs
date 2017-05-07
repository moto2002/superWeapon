using System;
using UnityEngine;

public class ShowSpecialInfo : UIDragScrollView
{
	public ShowDetailInfo infos;

	public Transform leftPos;

	public Transform rightPos;

	public Body_Model taizi;

	public Body_Model soliderModel;

	public int preItemid;

	public int nextItemID;

	public int itemID;

	public static bool isDeadSolider;

	[HideInInspector]
	public bool isGotoFight;

	[HideInInspector]
	public bool isCanGotoFight;

	private bool isFirstDrg = true;

	private UICenterOnChild parent;

	public bool isClock;

	private Body_Model smallModel;

	public bool isStars;

	private void Awake()
	{
	}

	public void setSpecialSoliderInfo(int _itemid, bool isHave)
	{
		this.isClock = isHave;
		this.itemID = _itemid;
		if (this.taizi == null)
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("Platform", base.transform);
			modelByBundleByName.tr.localPosition = new Vector3(-8.6f, -166f, -233.2f);
			modelByBundleByName.tr.localRotation = Quaternion.Euler(-20f, 0f, 0f);
			modelByBundleByName.tr.localScale = new Vector3(80f, 80f, 80f);
			Transform[] componentsInChildren = modelByBundleByName.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 5;
			}
			this.taizi = modelByBundleByName;
		}
		if (this.isClock)
		{
			Body_Model effectByName = PoolManage.Ins.GetEffectByName("peibingtai", this.taizi.tr);
			effectByName.tr.localScale = Vector3.one * 12f;
			effectByName.tr.localPosition = Vector3.zero;
			effectByName.tr.localRotation = Quaternion.identity;
			Transform[] componentsInChildren2 = effectByName.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = 5;
			}
		}
		if (this.soliderModel == null)
		{
			Body_Model modelByBundleByName2 = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[_itemid].bodyId, base.transform);
			modelByBundleByName2.tr.localPosition = new Vector3(0f, -148.7f, -326f);
			modelByBundleByName2.tr.localRotation = Quaternion.Euler(0f, 148f, 0f);
			if (this.itemID == 1)
			{
				modelByBundleByName2.tr.localPosition = new Vector3(-31.2f, -154.07f, -326f);
			}
			else if (this.itemID == 2)
			{
				modelByBundleByName2.tr.localPosition = new Vector3(-41.2f, -175.39f, -326f);
			}
			Transform[] componentsInChildren3 = modelByBundleByName2.GetComponentsInChildren<Transform>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				Transform transform3 = componentsInChildren3[k];
				transform3.gameObject.layer = 5;
			}
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(modelByBundleByName2.ga);
			compentIfNoAddOne.SetAnimaInfo();
			compentIfNoAddOne.AnimPlay("Idle");
			if (modelByBundleByName2.BlueModel)
			{
				modelByBundleByName2.BlueModel.gameObject.SetActive(true);
			}
			if (modelByBundleByName2.RedModel)
			{
				modelByBundleByName2.RedModel.gameObject.SetActive(false);
			}
			this.soliderModel = modelByBundleByName2;
		}
		if (this.taizi.tr.FindChild("taizi2").transform)
		{
			this.taizi.tr.FindChild("taizi2").transform.gameObject.SetActive(false);
		}
	}

	public void BuySoliderByMoney()
	{
		if (this.soliderModel)
		{
			Body_Model effectByName = PoolManage.Ins.GetEffectByName("peibingtai", this.taizi.tr);
			effectByName.tr.localScale = Vector3.one * 12f;
			effectByName.tr.localPosition = Vector3.zero;
			effectByName.tr.localRotation = Quaternion.identity;
			Transform[] componentsInChildren = effectByName.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 5;
			}
		}
	}

	public void UpStar()
	{
		if (this.soliderModel)
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect("speSolider", this.soliderModel.transform.position, Quaternion.identity, this.soliderModel.transform.parent);
			dieBall.tr.localPosition = new Vector3(0f, -15f, -326f);
			dieBall.tr.localScale = Vector3.one;
			Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 8;
			}
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.soliderModel.ga);
			compentIfNoAddOne.AnimPlay("Upgrade");
			compentIfNoAddOne.AnimPlayCrocessQuened("Idle");
		}
	}

	public void Idel()
	{
		if (this.soliderModel)
		{
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.soliderModel.ga);
			compentIfNoAddOne.SetAnimaInfo();
			compentIfNoAddOne.AnimPlay("Idle");
		}
	}

	public void ReLive()
	{
		if (this.soliderModel)
		{
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.soliderModel.ga);
			compentIfNoAddOne.SetAnimaInfo();
			compentIfNoAddOne.AnimPlay("Res");
			compentIfNoAddOne.AnimPlayCrocessQuened("Idle");
		}
	}

	public void Dead()
	{
		if (this.soliderModel)
		{
			Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.soliderModel.ga);
			compentIfNoAddOne.SetAnimaInfo();
			compentIfNoAddOne.AnimPlay("Idle2");
		}
	}

	public void LateUpdate()
	{
		if (SepcialSoliderPanel.ins.isDragInfo)
		{
			if (this.leftPos.position.x >= SepcialSoliderPanel.ins.left_Border.position.x && this.rightPos.position.x <= SepcialSoliderPanel.ins.rightBorder.position.x)
			{
				if (this.soliderModel)
				{
					this.soliderModel.tr.localScale = new Vector3(384f * UnitConst.GetInstance().soldierList[this.itemID].modelScale, 384f * UnitConst.GetInstance().soldierList[this.itemID].modelScale, 384f * UnitConst.GetInstance().soldierList[this.itemID].modelScale);
				}
				if (this.taizi)
				{
					this.taizi.tr.localScale = new Vector3(80f, 80f, 80f);
				}
			}
			else
			{
				if (this.soliderModel)
				{
					this.soliderModel.tr.localScale = new Vector3(250f * UnitConst.GetInstance().soldierList[this.itemID].modelScale, 250f * UnitConst.GetInstance().soldierList[this.itemID].modelScale, 250f * UnitConst.GetInstance().soldierList[this.itemID].modelScale);
				}
				if (this.taizi)
				{
					this.taizi.tr.localScale = new Vector3(40f, 40f, 40f);
				}
			}
		}
	}
}
