using System;
using System.Xml;
using UnityEngine;

public class WMapRD
{
	public static void RDNewWMapXml(int id)
	{
		HeroInfo.GetInstance().worldMapInfo.MapConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("MapEntity_" + id), XmlNodeType.Document, null))
		{
			try
			{
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
					{
						int idx = int.Parse(xmlTextReader.GetAttribute("index"));
						string attribute = xmlTextReader.GetAttribute("entityType");
						string attribute2 = xmlTextReader.GetAttribute("offset");
						string attribute3 = xmlTextReader.GetAttribute("mapRes");
						int num = int.Parse(attribute);
						if (num != -1)
						{
							WMapConst wMapConst = new WMapConst();
							wMapConst.idx = idx;
							wMapConst.mapType = int.Parse(attribute);
							wMapConst.offset = attribute2;
							wMapConst.mapRes = attribute3;
							if (!HeroInfo.GetInstance().worldMapInfo.MapConst.ContainsKey(wMapConst.idx))
							{
								HeroInfo.GetInstance().worldMapInfo.MapConst.Add(wMapConst.idx, wMapConst);
							}
							else
							{
								Debug.LogError("WMap包含相同的Key " + wMapConst.idx);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}
	}
}
