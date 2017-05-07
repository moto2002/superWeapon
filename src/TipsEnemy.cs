using System;
using System.Collections.Generic;
using UnityEngine;

public class TipsEnemy : MonoBehaviour
{
	public GameObject Reward;

	public GameObject NoReward;

	public UISimpleItem[] rewards;

	public UILabel tanSuoLingLable;

	public UILabel ResSpeed;

	public UISprite back;

	public UITable rewardtable;

	public void RefreshUI()
	{
	}

	private void OnEnable()
	{
		NGUITools.SetActive(this.Reward, false);
		NGUITools.SetActive(this.NoReward, true);
		if (this.tanSuoLingLable != null)
		{
			this.tanSuoLingLable.text = string.Concat(new object[]
			{
				(1 <= HeroInfo.GetInstance().playerRes.tanSuoLing) ? string.Empty : "[ff0000]",
				HeroInfo.GetInstance().playerRes.tanSuoLing,
				"/",
				1
			});
		}
		if (this.ResSpeed != null)
		{
			this.ResSpeed.text = string.Empty;
			if (TipsManager.inst != null && TipsManager.inst.curIsland != null && TipsManager.inst.curIsland.commandLV > 0)
			{
				switch (T_WMap.IdxGetMapType(TipsManager.inst.curIsland.mapIdx))
				{
				case IslandType.oil:
					this.ResSpeed.text = string.Format("资源生长速度(以时为单位) ： {0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[TipsManager.inst.curIsland.commandLV][ResType.石油].speendValue);
					break;
				case IslandType.steel:
					this.ResSpeed.text = string.Format("资源生长速度(以时为单位) ： {0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[TipsManager.inst.curIsland.commandLV][ResType.钢铁].speendValue);
					break;
				case IslandType.rareEarth:
					this.ResSpeed.text = string.Format("资源生长速度(以时为单位) ： {0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[TipsManager.inst.curIsland.commandLV][ResType.稀矿].speendValue);
					break;
				}
			}
		}
		if (TipsManager.inst != null && TipsManager.inst.curIsland != null && TipsManager.inst.curIsland.iconType != IconType.enemyBase)
		{
			if (TipsManager.inst.curIsland.reward != null && TipsManager.inst.curIsland.reward.Count > 0)
			{
				NGUITools.SetActive(this.Reward, true);
				NGUITools.SetActive(this.NoReward, false);
				foreach (KeyValuePair<ResType, int> current in TipsManager.inst.curIsland.reward)
				{
					switch (current.Key)
					{
					case ResType.金币:
						this.rewards[0].gameObject.SetActive(true);
						this.rewards[0].num.text = current.Value.ToString();
						break;
					case ResType.石油:
						this.rewards[2].gameObject.SetActive(true);
						this.rewards[2].num.text = current.Value.ToString();
						break;
					case ResType.钢铁:
						this.rewards[1].gameObject.SetActive(true);
						this.rewards[1].num.text = current.Value.ToString();
						break;
					case ResType.稀矿:
						this.rewards[3].gameObject.SetActive(true);
						this.rewards[3].num.text = current.Value.ToString();
						break;
					}
				}
				this.rewardtable.Reposition();
			}
		}
	}
}
