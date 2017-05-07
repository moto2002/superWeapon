using DG.Tweening;
using System;
using UnityEngine;

public class SpyInfo : MonoBehaviour
{
	public Transform tar;

	public UILabel lifeLab;

	public UILabel damageLab;

	public UILabel defendLab;

	public UILabel Des;

	public UILabel nameLab;

	public UILabel lvLab;

	private Body_Model model;

	private InfoType infoTy;

	private void Start()
	{
	}

	private void OnEnable()
	{
		DragMgr.ClickTerrSendMessage += new Action(this.DragMgr_ClickTerrSendMessage);
	}

	private void DragMgr_ClickTerrSendMessage()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnDisable()
	{
		DragMgr.ClickTerrSendMessage -= new Action(this.DragMgr_ClickTerrSendMessage);
	}

	private void Update()
	{
	}

	public void Remove()
	{
		if (UIManager.curState == SenceState.Spy || UIManager.curState == SenceState.Visit)
		{
			CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(40f, 0f, 0f), 0.6f, RotateMode.Fast);
			CameraControl.inst.cameraMain.DOLocalMoveZ(CameraInitMove.inst.data.moveHeight, 0.6f, false);
		}
		this.Des.gameObject.GetComponent<TypewriterEffect>().enabled = false;
		this.defendLab.gameObject.GetComponent<TypewriterEffect>().enabled = false;
		this.damageLab.gameObject.GetComponent<TypewriterEffect>().enabled = false;
		this.lifeLab.gameObject.GetComponent<TypewriterEffect>().enabled = false;
		if (this.tar != null && this.tar.GetComponent<T_Tower>() != null && this.tar.gameObject.activeInHierarchy)
		{
			this.tar.GetComponent<T_Tower>().T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
		}
	}

	public void ShowInfo(T_Tower tower, SenceState state, InfoType info)
	{
		if (UIManager.curState == SenceState.Spy || UIManager.curState == SenceState.Visit)
		{
			CameraControl.inst.cameraMain.DOLocalMoveZ(40f, 0.6f, false);
			CameraControl.inst.Tr.DOMove(HUDTextTool.inst.GetCameraMoveEndPos(tower.tr.position, CameraControl.inst.Tr.position, 40f), 0.6f, false);
			CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(40f, 0f, 0f), 0.6f, RotateMode.Fast);
			CameraControl.inst.openDragCameraAndInertia = false;
		}
		this.lifeLab.text = string.Empty;
		this.damageLab.text = string.Empty;
		this.defendLab.text = string.Empty;
		this.infoTy = info;
		this.tar = tower.tr;
		if (state != SenceState.Home && state != SenceState.WatchResIsland)
		{
			this.nameLab.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + "Lv." + tower.lv.ToString();
			this.Des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].description, "build");
			this.Des.gameObject.GetComponent<TypewriterEffect>().enabled = true;
		}
		else
		{
			this.nameLab.text = string.Empty;
		}
		switch (state)
		{
		case SenceState.Home:
		case SenceState.WatchResIsland:
			this.lifeLab.gameObject.SetActive(false);
			this.damageLab.gameObject.SetActive(false);
			this.defendLab.gameObject.SetActive(false);
			return;
		case SenceState.Spy:
			goto IL_1B8;
		case SenceState.Attacking:
			IL_1AA:
			if (state != SenceState.Visit)
			{
				return;
			}
			goto IL_1B8;
		case SenceState.InBuild:
			this.lifeLab.gameObject.SetActive(false);
			this.damageLab.gameObject.SetActive(false);
			this.defendLab.gameObject.SetActive(false);
			return;
		}
		goto IL_1AA;
		IL_1B8:
		this.lifeLab.gameObject.SetActive(true);
		if (this.nameLab.transform.childCount <= 0)
		{
			this.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tower.index].bodyID, this.nameLab.transform);
			this.model.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tower.index].modelRostion[0], UnitConst.GetInstance().buildingConst[tower.index].modelRostion[1], UnitConst.GetInstance().buildingConst[tower.index].modelRostion[2]);
			this.model.tr.localScale = new Vector3(UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f);
			this.model.tr.localPosition = new Vector3(0.98f, -161.3f, -100f);
			if (tower.index == 23)
			{
				this.model.tr.localPosition = new Vector3(0.98f, -190f, -100f);
			}
			if (tower.index == 1)
			{
				this.model.tr.localPosition = new Vector3(0.98f, -131.12f, -100f);
			}
			if (this.model && this.model.RedModel)
			{
				NGUITools.SetActiveSelf(this.model.RedModel.gameObject, true);
			}
			if (this.model && this.model.Blue_DModel)
			{
				NGUITools.SetActiveSelf(this.model.Blue_DModel.gameObject, false);
			}
			if (this.model && this.model.BlueModel)
			{
				NGUITools.SetActiveSelf(this.model.BlueModel.gameObject, false);
			}
			if (this.model && this.model.Red_DModel)
			{
				NGUITools.SetActiveSelf(this.model.Red_DModel.gameObject, false);
			}
			Transform[] componentsInChildren = this.model.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
			if (this.model.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren2 = this.model.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tower.index].particleInfo != null)
				{
					int num = (componentsInChildren2.Length <= UnitConst.GetInstance().buildingConst[tower.index].particleInfo.Length) ? componentsInChildren2.Length : UnitConst.GetInstance().buildingConst[tower.index].particleInfo.Length;
					for (int j = 0; j < num; j++)
					{
						componentsInChildren2[j].startSize.GetType();
						componentsInChildren2[j].startSize = UnitConst.GetInstance().buildingConst[tower.index].particleInfo[j];
					}
				}
			}
		}
		else
		{
			UnityEngine.Object.Destroy(this.nameLab.transform.GetChild(0).gameObject);
			this.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tower.index].bodyID, this.nameLab.transform);
			this.model.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tower.index].modelRostion[0], UnitConst.GetInstance().buildingConst[tower.index].modelRostion[1], UnitConst.GetInstance().buildingConst[tower.index].modelRostion[2]);
			this.model.tr.localScale = new Vector3(UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[tower.index].TowerSize * 100f);
			this.model.tr.localPosition = new Vector3(0.98f, -161.3f, -100f);
			if (tower.index == 23)
			{
				this.model.tr.localPosition = new Vector3(0.98f, -190f, -100f);
			}
			if (tower.index == 1)
			{
				this.model.tr.localPosition = new Vector3(0.98f, -131.12f, -100f);
			}
			if (this.model && this.model.RedModel)
			{
				NGUITools.SetActiveSelf(this.model.RedModel.gameObject, true);
			}
			if (this.model && this.model.Blue_DModel)
			{
				NGUITools.SetActiveSelf(this.model.Blue_DModel.gameObject, false);
			}
			if (this.model && this.model.BlueModel)
			{
				NGUITools.SetActiveSelf(this.model.BlueModel.gameObject, false);
			}
			if (this.model && this.model.Red_DModel)
			{
				NGUITools.SetActiveSelf(this.model.Red_DModel.gameObject, false);
			}
			Transform[] componentsInChildren3 = this.model.GetComponentsInChildren<Transform>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				componentsInChildren3[k].gameObject.layer = 5;
			}
			if (this.model.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren4 = this.model.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tower.index].particleInfo != null)
				{
					int num2 = (componentsInChildren4.Length <= UnitConst.GetInstance().buildingConst[tower.index].particleInfo.Length) ? componentsInChildren4.Length : UnitConst.GetInstance().buildingConst[tower.index].particleInfo.Length;
					for (int l = 0; l < num2; l++)
					{
						componentsInChildren4[l].startSize.GetType();
						componentsInChildren4[l].startSize = UnitConst.GetInstance().buildingConst[tower.index].particleInfo[l];
					}
				}
			}
		}
		this.damageLab.gameObject.SetActive(true);
		this.defendLab.gameObject.SetActive(true);
		this.lifeLab.text = string.Format("{0}", tower.CharacterBaseFightInfo.life);
		this.lifeLab.gameObject.GetComponent<TypewriterEffect>().enabled = true;
		if (tower.type == 2 || tower.type == 3)
		{
			this.damageLab.text = tower.CharacterBaseFightInfo.breakArmor.ToString();
			this.damageLab.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			this.damageLab.gameObject.GetComponent<TypewriterEffect>().enabled = true;
			this.defendLab.text = string.Format("{0}", tower.CharacterBaseFightInfo.defBreak);
			if (this.defendLab.text == string.Empty)
			{
				this.defendLab.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			}
			else
			{
				this.defendLab.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			}
			this.defendLab.gameObject.GetComponent<TypewriterEffect>().enabled = true;
		}
		else if (tower.type == 99)
		{
			this.damageLab.text = string.Empty;
			this.damageLab.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			this.defendLab.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
		else
		{
			this.damageLab.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			this.damageLab.text = string.Empty;
			this.defendLab.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
