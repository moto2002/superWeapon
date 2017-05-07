using System;
using System.Xml;
using UnityEngine;

public class DataMgr
{
	public static XmlDocument node;

	public static XMLData GetXMLFromLocal(string name)
	{
		XMLData xMLData = new XMLData();
		TextAsset textAsset = (TextAsset)Resources.Load("ClientData/" + name);
		if (textAsset != null)
		{
			xMLData.node.LoadXml(textAsset.text);
			xMLData.list = xMLData.node.SelectNodes("/configures/configure");
		}
		else
		{
			LogManage.Log("没有找到xml");
		}
		return xMLData;
	}
}
