using msg;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SenceInfo
{
	public enum BattleResource
	{
		None,
		WorldMap,
		NormalBattleFight,
		LegionBattleFight,
		PVPFightBack_Home,
		PVPFightBack_WorldMap,
		HOME,
		ReplayVideo_Home,
		ReplayVideo_WorldMap
	}

	public const string island = "island";

	public const string init = "Login";

	public const string worldMap = "WorldMap";

	public static MapData curMap = new MapData();

	public static SCSpyIsland SpyPlayerInfo;

	public static SenceInfo.BattleResource battleResource = SenceInfo.BattleResource.None;

	public static List<TerrainType> terrainTypeList = new List<TerrainType>();

	public static Battle CurBattle = null;

	public static BattleField CurBattleField = null;

	public static PlayerWMapData CurSelectIslandData = null;

	public static ReportData CurReportData = null;

	public static void BuildTerrainList()
	{
		SenceInfo.terrainTypeList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("TerrainDatas"), XmlNodeType.Document, null))
		{
			TerrainType terrainType = null;
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element)
				{
					if (xmlTextReader.LocalName.Equals("configure"))
					{
						terrainType = new TerrainType();
						if (xmlTextReader.AttributeCount > 0)
						{
							terrainType.typeId = xmlTextReader.GetAttribute("typeId");
							terrainType.terrName = xmlTextReader.GetAttribute("terrName");
							terrainType.sizeX = int.Parse(xmlTextReader.GetAttribute("sizeX"));
							terrainType.sizeZ = int.Parse(xmlTextReader.GetAttribute("sizeZ"));
							terrainType.terrainRes = xmlTextReader.GetAttribute("terrainRes");
						}
						SenceInfo.terrainTypeList.Add(terrainType);
					}
					else if (xmlTextReader.LocalName.Equals("res"))
					{
						ResInfo resInfo = new ResInfo();
						resInfo.typeId = int.Parse(xmlTextReader.GetAttribute("resType"));
						resInfo.info = xmlTextReader.GetAttribute("resInfo");
						string attribute = xmlTextReader.GetAttribute("landingInfo");
						string attribute2 = xmlTextReader.GetAttribute("noMovingInfo");
						string attribute3 = xmlTextReader.GetAttribute("randomBox");
						string attribute4 = xmlTextReader.GetAttribute("movingArea");
						string attribute5 = xmlTextReader.GetAttribute("LandingArmyArea");
						resInfo.towers = SenceInfo.StrToTowerList(resInfo.info);
						if (!string.IsNullOrEmpty(attribute))
						{
							string[] array = attribute.Split(new char[]
							{
								'|'
							});
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								string text = array2[i];
								Vector3 item = new Vector3(float.Parse(text.Split(new char[]
								{
									','
								})[0]), -0.2f, float.Parse(text.Split(new char[]
								{
									','
								})[1]));
								resInfo.landingInfo.Add(item);
							}
						}
						if (!string.IsNullOrEmpty(attribute2))
						{
							string[] array3 = attribute2.Split(new char[]
							{
								'|'
							});
							string[] array4 = array3;
							for (int j = 0; j < array4.Length; j++)
							{
								string text2 = array4[j];
								Vector3 item2 = new Vector3(float.Parse(text2.Split(new char[]
								{
									','
								})[0]), 0.5f, float.Parse(text2.Split(new char[]
								{
									','
								})[1]));
								resInfo.noMovingInfo.Add(item2);
							}
						}
						if (!string.IsNullOrEmpty(attribute3))
						{
							string[] array5 = attribute3.Split(new char[]
							{
								'|'
							});
							resInfo.randomBox = new Vector3[array5.Length];
							for (int k = 0; k < array5.Length; k++)
							{
								resInfo.randomBox[k] = new Vector3(float.Parse(array5[k].Split(new char[]
								{
									','
								})[0]), 0f, float.Parse(array5[k].Split(new char[]
								{
									','
								})[1]));
							}
						}
						if (!string.IsNullOrEmpty(attribute5))
						{
							string[] array6 = attribute5.Split(new char[]
							{
								'|'
							});
							resInfo.landingArmyArea = new Vector3[array6.Length];
							for (int l = 0; l < array6.Length; l++)
							{
								resInfo.landingArmyArea[l] = new Vector3(float.Parse(array6[l].Split(new char[]
								{
									','
								})[0]), 0.2f, float.Parse(array6[l].Split(new char[]
								{
									','
								})[1]));
							}
						}
						if (!string.IsNullOrEmpty(attribute4))
						{
							resInfo.MoveArea.Set(float.Parse(attribute4.Split(new char[]
							{
								','
							})[0]), float.Parse(attribute4.Split(new char[]
							{
								','
							})[1]), float.Parse(attribute4.Split(new char[]
							{
								','
							})[2]), float.Parse(attribute4.Split(new char[]
							{
								','
							})[3]));
						}
						else
						{
							resInfo.MoveArea.Set(0f, 70f, 0f, 70f);
						}
						terrainType.resInfo.Add(resInfo.typeId, resInfo);
					}
				}
			}
		}
	}

	public static TowerList[] StrToTowerList(string towerList)
	{
		TowerList[] array = null;
		if (towerList != "-1" && towerList != "0")
		{
			string[] array2 = towerList.Split(new char[]
			{
				'|'
			});
			array = new TowerList[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					','
				});
				if (array3.Length > 2)
				{
					array[i] = new TowerList();
					array[i].towerIdx = int.Parse(array3[0]);
					array[i].lv = int.Parse(array3[1]);
					array[i].posIdx = int.Parse(array3[2]);
				}
			}
		}
		return array;
	}
}
