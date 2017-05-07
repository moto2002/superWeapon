using System;
using UnityEngine;

public class PlayerMedal : MonoBehaviour
{
	public UILabel uilabel;

	public ResType resourceType;

	private void Start()
	{
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		this.SetPlayerMedal();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10003)
		{
			this.SetPlayerMedal();
		}
	}

	private void SetPlayerMedal()
	{
		ResType resType = this.resourceType;
		switch (resType)
		{
		case ResType.金币:
			break;
		case ResType.石油:
			break;
		case ResType.钢铁:
			break;
		case ResType.稀矿:
			break;
		case ResType.技能点:
			break;
		case ResType.天赋点:
			break;
		case ResType.钻石:
			break;
		case ResType.兵种:
			break;
		case ResType.经验:
			break;
		case ResType.等级:
			break;
		case ResType.奖牌:
			this.uilabel.text = HeroInfo.GetInstance().playerRes.playermedal.ToString();
			break;
		case ResType.军令:
			this.uilabel.text = HeroInfo.GetInstance().playerRes.junLing.ToString();
			break;
		case ResType.指挥官经验:
			break;
		case ResType.战绩积分:
			break;
		case ResType.探索令:
			this.uilabel.text = HeroInfo.GetInstance().playerRes.tanSuoLing.ToString();
			break;
		case ResType.技能碎片:
			this.uilabel.text = HeroInfo.GetInstance().playerRes.skillDebris.ToString();
			break;
		case ResType.普通技能抽卡:
			break;
		case ResType.电力:
			break;
		case ResType.传奇技能抽卡:
			break;
		default:
			if (resType != ResType.弹药)
			{
			}
			break;
		}
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}
}
