using msg;
using System;

public class AideHandler
{
	public static void CG_AideList()
	{
		CSAideList cSAideList = new CSAideList();
		cSAideList.id = 1;
		ClientMgr.GetNet().SendHttp(8000, cSAideList, null, null);
	}

	public static void CG_AideMix(int id)
	{
		CSAideMix cSAideMix = new CSAideMix();
		cSAideMix.aideId = id;
		ClientMgr.GetNet().SendHttp(8002, cSAideMix, null, null);
	}

	public static void CG_AideSend()
	{
		CSAideSend cSAideSend = new CSAideSend();
		cSAideSend.aideId = 1;
		ClientMgr.GetNet().SendHttp(8004, cSAideSend, null, null);
	}

	public static void CG_AideRecycle(int id)
	{
		CSAideRecycle cSAideRecycle = new CSAideRecycle();
		cSAideRecycle.aideId = id;
		ClientMgr.GetNet().SendHttp(8006, cSAideRecycle, null, null);
	}

	public static void CG_AideIntensify(int id)
	{
		CSAideIntensify cSAideIntensify = new CSAideIntensify();
		cSAideIntensify.aideID = id;
		ClientMgr.GetNet().SendHttp(8008, cSAideIntensify, null, null);
	}
}
