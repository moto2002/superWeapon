using System;

public class GameConst
{
	public static int fightBegain = 30;

	public static int fightTime = 180;

	public static float resolutionTimes = 1f;

	public static bool IsFirstLogin;

	public static int buyJiNengCoin = 1000;

	public static int bugJinengRMB = 20;

	public static long ArmyUpdateCDTime
	{
		get
		{
			string text = UnitConst.GetInstance().DesighConfigDic[70].value.Replace("commandLevel", HeroInfo.GetInstance().PlayerCommondLv.ToString());
			int num = int.Parse(text.Split(new char[]
			{
				'*'
			})[0]);
			int num2 = int.Parse(text.Split(new char[]
			{
				'*'
			})[1].Split(new char[]
			{
				'-'
			})[0]);
			int num3 = int.Parse(text.Split(new char[]
			{
				'*'
			})[1].Split(new char[]
			{
				'-'
			})[1]);
			long num4 = (long)(num * num2);
			return num4 - (long)num3;
		}
	}
}
