using System;
using UnityEngine;

public class ParadeTank : IMonoBehaviour
{
	private T_Tower Home;

	private Body_Model Body;

	private Animation Ani;

	private Transform TrueBody;

	public void SetInfo(T_Tower _home, int weizhi)
	{
		this.Home = _home;
		this.tr.parent = this.Home.tr;
		switch (weizhi)
		{
		case 1:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 2:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 3:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f);
			break;
		case 4:
			this.tr.position = this.Home.tr.position + new Vector3(0f, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 5:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, 0f);
			break;
		case 6:
			this.tr.position = this.Home.tr.position + new Vector3(-(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f), 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 7:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, -(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f));
			break;
		case 8:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 9:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 10:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f);
			break;
		case 11:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 12:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, 0f);
			break;
		case 13:
			this.tr.position = this.Home.tr.position + new Vector3(0f, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 14:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f);
			break;
		case 15:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize * 0.5f, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		case 16:
			this.tr.position = this.Home.tr.position + new Vector3(-UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, -UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		default:
			this.tr.position = this.Home.tr.position + new Vector3(UnitConst.GetInstance().buildingConst[this.Home.index].bodySize, 0f, UnitConst.GetInstance().buildingConst[this.Home.index].bodySize);
			break;
		}
		PoolManage.Ins.CreatEffect("chubing", this.tr.position, Quaternion.identity, null);
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
		Ani_CharacterControler compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
		compentIfNoAddOne.AnimPlay("Idle");
		FightHundler.OnFighting += new Action(this.PlayerHandle_OnFighting);
	}

	private void PlayerHandle_OnFighting()
	{
	}

	private void OnDisable()
	{
		FightHundler.OnFighting -= new Action(this.PlayerHandle_OnFighting);
	}
}
