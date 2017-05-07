using System;
using System.Collections.Generic;

public class ArmsDealerPanelData
{
	public static List<ArmsDealer> ArmsDealers = new List<ArmsDealer>();

	public static List<ArmsDealer> ArmsDealers_Net = new List<ArmsDealer>();

	public static List<ArmsDealer> ArmsDealers_Mall = new List<ArmsDealer>();

	public static int useMoneyRefreshTimes;

	public static int lastRefreshTimeWithMoney;

	public static DateTime nextRefreshTime;
}
