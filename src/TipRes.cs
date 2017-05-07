using System;
using UnityEngine;

public class TipRes : MonoBehaviour
{
	public UILabel resText;

	private T_Island island;

	private void Start()
	{
	}

	private void OnEnable()
	{
		this.island = base.GetComponent<TipsRoot>().tar;
		if (this.island == null)
		{
			return;
		}
		switch (this.island.MapType)
		{
		case IslandType.oil:
			this.resText.text = string.Format(LanguageManage.GetTextByKey("每小时生产石油", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.island.commandLV][ResType.石油].speendValue);
			break;
		case IslandType.steel:
			this.resText.text = string.Format(LanguageManage.GetTextByKey("每小时生产钢铁", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.island.commandLV][ResType.钢铁].speendValue);
			break;
		case IslandType.rareEarth:
			this.resText.text = string.Format(LanguageManage.GetTextByKey("每小时生产稀矿", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.island.commandLV][ResType.稀矿].speendValue);
			break;
		}
	}
}
