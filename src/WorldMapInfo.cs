using System;
using System.Collections.Generic;

public sealed class WorldMapInfo
{
	public Dictionary<int, PlayerWMapData> playerWMap = new Dictionary<int, PlayerWMapData>();

	public Dictionary<int, WMapConst> MapConst = new Dictionary<int, WMapConst>();

	public List<W_MapTerrain> terrainList = new List<W_MapTerrain>();

	public List<W_MapData> coinMapList = new List<W_MapData>();

	public List<W_MapData> OilMapList = new List<W_MapData>();

	public List<W_MapData> rareEarthMapList = new List<W_MapData>();

	public List<W_MapData> steelMapList = new List<W_MapData>();
}
