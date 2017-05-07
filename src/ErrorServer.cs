using System;
using System.Collections.Generic;
using System.Xml;

public class ErrorServer
{
	public static Dictionary<string, string> ErrorServerString = new Dictionary<string, string>();

	public static void ReadErrorServerXML()
	{
		ErrorServer.ErrorServerString.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ErrorMassage"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					ErrorServer.ErrorServerString.Add(xmlTextReader.GetAttribute("id").Trim(), xmlTextReader.GetAttribute("description"));
				}
			}
		}
	}
}
