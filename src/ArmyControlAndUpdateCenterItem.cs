using System;
using UnityEngine;

public class ArmyControlAndUpdateCenterItem : UIDragScrollView
{
	private bool isDragToJiJieDian;

	public int itemID;

	public Body_Model model;

	public Body_Model taizi;

	private bool isFirstDrag;

	[HideInInspector]
	public bool isClock;

	public Transform leftPos;

	public Transform rightPos;

	private Body_Model TuoZhuaiModel;

	private UICenterOnChild parent;

	public int preItemid;

	public int nextItemId;

	private float pressTime;

	private bool isPress;

	private float input_x_0;

	private float input_time;

	private DieBall UpdateEndEffect;

	public ArmyShow thisArmyShow;

	private void LateUpdate()
	{
		if (!UnitConst.IsHaveInstance())
		{
			return;
		}
		if (ArmyControlAndUpdatePanel.Inst.isAddDataEnd)
		{
			if (this.leftPos.position.x >= ArmyControlAndUpdatePanel.Inst.leftPos.position.x && this.rightPos.position.x <= ArmyControlAndUpdatePanel.Inst.rightPos.position.x)
			{
				if (this.model)
				{
					this.model.tr.localScale = Vector3.one * (UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateScale_InCenter.x / UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziScale_InCenter.x);
				}
				if (this.taizi)
				{
					this.taizi.tr.localScale = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziScale_InCenter;
				}
				if (this.model && this.thisArmyShow && !this.thisArmyShow.NowChoose)
				{
					this.thisArmyShow.SetNowChoose(true);
				}
				if (this.TuoZhuaiModel)
				{
					this.TuoZhuaiModel.tr.eulerAngles = new Vector3(5f, 150f, 0f);
				}
				if (Input.GetMouseButton(0))
				{
					float num = Input.mousePosition.x / (float)Screen.width;
					this.input_time += Time.deltaTime;
					if (this.input_time > 0.05f)
					{
						this.input_time = 0f;
						if (this.input_x_0 != 0f)
						{
							float num2 = num - this.input_x_0;
							if (this.model && this.thisArmyShow)
							{
								this.thisArmyShow.SetCarRotate(-1f * num2);
							}
						}
						this.input_x_0 = num;
					}
				}
				else
				{
					this.input_x_0 = 0f;
				}
			}
			else
			{
				if (this.model)
				{
					this.model.tr.localScale = Vector3.one * (UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateScale.x / UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziScale.x);
				}
				if (this.taizi)
				{
					this.taizi.tr.localScale = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziScale;
				}
				if (this.model && this.thisArmyShow)
				{
					this.thisArmyShow.SetNowChoose(false);
				}
			}
		}
	}

	public void PlayUpdateEffect()
	{
		if (this.UpdateEndEffect == null)
		{
			this.UpdateEndEffect = PoolManage.Ins.CreatEffect("car", base.transform.position, Quaternion.identity, base.transform);
			this.UpdateEndEffect.LifeTime = 3f;
			this.UpdateEndEffect.tr.localPosition = new Vector3(-60f, 100f, -815f);
			this.UpdateEndEffect.tr.localScale = Vector3.one;
			Transform[] componentsInChildren = this.UpdateEndEffect.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 18;
			}
		}
	}

	public void SetInfo(int _itemID, bool isLock)
	{
		this.itemID = _itemID;
		this.isClock = !isLock;
		Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[this.itemID].bodyId, base.transform);
		if (modelByBundleByName)
		{
			modelByBundleByName.tr.localScale = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateScale;
			modelByBundleByName.tr.localPosition = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdatePosition;
			modelByBundleByName.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateRotation);
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(true);
			}
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
			Transform[] componentsInChildren = modelByBundleByName.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 5;
			}
		}
		this.model = modelByBundleByName;
		Body_Model modelByBundleByName2 = PoolManage.Ins.GetModelByBundleByName("Platform", base.transform);
		if (!this.isClock && modelByBundleByName2)
		{
			Body_Model effectByName = PoolManage.Ins.GetEffectByName("peibingtai", modelByBundleByName2.tr);
			if (effectByName)
			{
				effectByName.tr.localScale = Vector3.one * 12f;
				effectByName.tr.localPosition = Vector3.zero;
				effectByName.tr.localRotation = Quaternion.identity;
			}
		}
		if (modelByBundleByName2)
		{
			modelByBundleByName2.tr.localScale = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziScale;
			modelByBundleByName2.tr.localPosition = UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziPosition;
			modelByBundleByName2.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().soldierConst[this.itemID].army_UpdateTaziRotation);
			Transform[] componentsInChildren2 = modelByBundleByName2.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = 18;
			}
			this.taizi = modelByBundleByName2;
			Transform[] componentsInChildren3 = this.taizi.GetComponentsInChildren<Transform>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				Transform transform3 = componentsInChildren3[k];
				if (transform3.name == "taizi2")
				{
					transform3.transform.gameObject.SetActive(false);
				}
			}
		}
		base.gameObject.AddComponent<ArmyShow>();
		this.thisArmyShow = base.GetComponent<ArmyShow>();
		this.thisArmyShow.Index = this.itemID;
		this.thisArmyShow.effect_model = this.model;
		if (this.taizi)
		{
			this.thisArmyShow.TaiZi_model = this.taizi.tr;
		}
		this.thisArmyShow.Init();
	}
}
