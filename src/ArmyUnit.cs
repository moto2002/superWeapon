using System;
using System.Xml;
using UnityEngine;

public class ArmyUnit : MonoBehaviour
{
	public static void ReadArmyIconXML()
	{
		UnitConst.GetInstance().armyIcon.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("LegionFlag"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					int name = int.Parse(xmlTextReader.GetAttribute("flag"));
					ArmyIconClass armyIconClass = new ArmyIconClass();
					armyIconClass.id = id;
					armyIconClass.name = name;
					UnitConst.GetInstance().armyIcon.Add(armyIconClass.id, armyIconClass);
				}
			}
		}
	}

	public static void ReadArmyRightXML()
	{
		UnitConst.GetInstance().armyRight.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("LegionsRight"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("num");
					string attribute3 = xmlTextReader.GetAttribute("right");
					ArmyRight armyRight = new ArmyRight();
					armyRight.id = id;
					armyRight.name = attribute;
					if (!string.IsNullOrEmpty(attribute2))
					{
						armyRight.num = int.Parse(attribute2);
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						for (int i = 0; i < attribute3.Split(new char[]
						{
							','
						}).Length; i++)
						{
							armyRight.right.Add(int.Parse(attribute3.Split(new char[]
							{
								','
							})[i]), int.Parse(attribute3.Split(new char[]
							{
								','
							})[i]));
						}
					}
					UnitConst.GetInstance().armyRight.Add(armyRight.id, armyRight);
				}
			}
		}
	}
}
