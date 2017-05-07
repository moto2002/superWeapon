using System;

public abstract class ChargeActityItem : IMonoBehaviour
{
	public ActivityClass curActity;

	public SevenDay curDay;

	public void SetInfo(ActivityClass acty)
	{
		this.curActity = acty;
		this.InitData();
	}

	public void SetInfoBySevenDay(SevenDay data)
	{
		this.curDay = data;
		this.InitDataBySevenDay();
	}

	public abstract void InitData();

	public abstract void InitDataBySevenDay();
}
