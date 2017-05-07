using System;

public class T_Island_NoOpen : T_Island
{
	public override bool IsOpen
	{
		get
		{
			return false;
		}
	}

	public void SetInfo(int idx, T_WMap worldMap)
	{
		this.mapIdx = idx;
		base.MapType = T_WMap.IdxGetMapType(this.mapIdx);
		base.CreatBody();
	}

	public override void MouseUp()
	{
		if (HeroInfo.GetInstance().PlayerRadarLv < UnitConst.GetInstance().mapEntityList[this.mapIdx].radarLevel)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("雷达等级不足,不能够探索更大的区域", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			LogManage.LogError("按理说这个应该不存在  岛屿没有推送");
		}
	}
}
