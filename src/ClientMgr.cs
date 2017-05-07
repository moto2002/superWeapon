using System;

public class ClientMgr
{
	public static NetMgr GetNet()
	{
		return NetMgr.inst;
	}
}
