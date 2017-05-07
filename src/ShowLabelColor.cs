using System;
using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class ShowLabelColor : MonoBehaviour
{
	private UILabel countLabel;

	public ResType resType;

	public int resNum;

	public void OnEnable()
	{
		this.RefreshColor();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		this.RefreshColor();
	}

	public void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void Awake()
	{
		this.countLabel = base.GetComponent<UILabel>();
	}

	private void RefreshColor()
	{
		ResType resType = this.resType;
		switch (resType)
		{
		case ResType.金币:
			if (this.resNum > HeroInfo.GetInstance().playerRes.resCoin)
			{
				this.countLabel.color = Color.red;
			}
			else
			{
				this.countLabel.color = Color.white;
			}
			break;
		case ResType.石油:
			if (this.resNum > HeroInfo.GetInstance().playerRes.resOil)
			{
				this.countLabel.color = Color.red;
			}
			else
			{
				this.countLabel.color = Color.white;
			}
			break;
		case ResType.钢铁:
			if (this.resNum > HeroInfo.GetInstance().playerRes.resSteel)
			{
				this.countLabel.color = Color.red;
			}
			else
			{
				this.countLabel.color = Color.white;
			}
			break;
		case ResType.稀矿:
			if (this.resNum > HeroInfo.GetInstance().playerRes.resRareEarth)
			{
				this.countLabel.color = Color.red;
			}
			else
			{
				this.countLabel.color = Color.white;
			}
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
			break;
		case ResType.军令:
			break;
		case ResType.指挥官经验:
			break;
		case ResType.战绩积分:
			break;
		case ResType.探索令:
			break;
		case ResType.技能碎片:
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
}
