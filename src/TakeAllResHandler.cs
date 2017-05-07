using msg;
using System;
using UnityEngine;

public class TakeAllResHandler : MonoBehaviour
{
	private static Action TakeAllRes;

	public static void CG_CSTakeAllRes(int resid, Action _Fun)
	{
		CSTakeAllResource cSTakeAllResource = new CSTakeAllResource();
		cSTakeAllResource.id = resid;
		TakeAllResHandler.TakeAllRes = _Fun;
		ClientMgr.GetNet().SendHttp(2012, cSTakeAllResource, new DataHandler.OpcodeHandler(TakeAllResHandler.OnHttpShow), null);
	}

	public static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (TakeAllResHandler.TakeAllRes != null)
		{
			TakeAllResHandler.TakeAllRes();
		}
	}
}
