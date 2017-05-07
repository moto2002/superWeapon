using msg;
using System;

public class SignHandler
{
	public static void CSSignIn(int id)
	{
		CSSignIn cSSignIn = new CSSignIn();
		cSSignIn.id = id;
		ClientMgr.GetNet().SendHttp(2104, cSSignIn, null, null);
	}

	public static void CSRetroactiveSignIn(int day)
	{
		CSRetroactiveSignIn cSRetroactiveSignIn = new CSRetroactiveSignIn();
		cSRetroactiveSignIn.day = day;
		ClientMgr.GetNet().SendHttp(2106, cSRetroactiveSignIn, null, null);
	}

	public static void CSRetroactiveAllSignIn()
	{
		CSRetroactiveAllSignIn cSRetroactiveAllSignIn = new CSRetroactiveAllSignIn();
		cSRetroactiveAllSignIn.id = 1;
		ClientMgr.GetNet().SendHttp(2108, cSRetroactiveAllSignIn, null, null);
	}

	public static void CSReceiveAccumulativeSignRewards(int id)
	{
		CSReceiveAccumulativeSignRewards cSReceiveAccumulativeSignRewards = new CSReceiveAccumulativeSignRewards();
		cSReceiveAccumulativeSignRewards.id = id;
		ClientMgr.GetNet().SendHttp(2110, cSReceiveAccumulativeSignRewards, null, null);
	}
}
