using System;
using System.Collections.Generic;

public class PlayerWMapData
{
	public long islandId;

	public int idx;

	public int ownerType;

	public int star;

	public string ownerId;

	public string ownerName;

	public string ownerIcon;

	public string ownerLv;

	public int commandlv;

	public long worldMapId;

	public Dictionary<ResType, int> reward = new Dictionary<ResType, int>();

	public bool replace;
}
