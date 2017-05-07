using System;
using System.Collections.Generic;

public class TerrainType
{
	public string typeId;

	public string terrName;

	public int terrStyle;

	public int sizeX;

	public int sizeZ;

	public string terrainRes;

	public string outPos;

	public Dictionary<int, ResInfo> resInfo = new Dictionary<int, ResInfo>();
}
