using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

public class LanguageManage
{
	public delegate void language();

	public static LanguageCategory selectedLanguage;

	public static LanguageCategory[] LanguageCategory;

	private static Dictionary<string, string> languageinfo_Client = new Dictionary<string, string>();

	public static event LanguageManage.language LangeChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			LanguageManage.LangeChange = (LanguageManage.language)Delegate.Combine(LanguageManage.LangeChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			LanguageManage.LangeChange = (LanguageManage.language)Delegate.Remove(LanguageManage.LangeChange, value);
		}
	}

	public static void LoadLanguageCategory()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("language"), XmlNodeType.Document, null))
		{
			List<LanguageCategory> list = new List<LanguageCategory>();
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					LanguageCategory item = default(LanguageCategory);
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("value");
					string attribute3 = xmlTextReader.GetAttribute("Uiname");
					item.Id = attribute;
					item.XmlName = attribute2;
					item.UiName = attribute3;
					list.Add(item);
				}
			}
			LanguageManage.LanguageCategory = list.ToArray();
		}
	}

	public static void LoadLanguageContent(LanguageCategory lanuage_Sel)
	{
		LanguageManage.selectedLanguage = lanuage_Sel;
		for (int i = 0; i < LanguageManage.LanguageCategory.Length; i++)
		{
			if (LanguageManage.LanguageCategory[i].Id == lanuage_Sel.Id)
			{
				User.SetLanguegeSetting(i);
			}
		}
		UnitConst.GetInstance().languageinfo.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName(LanguageManage.selectedLanguage.XmlName), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("value");
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					try
					{
						using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName(attribute), XmlNodeType.Document, null))
						{
							while (xmlTextReader2.Read())
							{
								if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
								{
									string attribute2 = xmlTextReader2.GetAttribute("id");
									string attribute3 = xmlTextReader2.GetAttribute("value");
									if (!string.IsNullOrEmpty(attribute2))
									{
										if (!dictionary.ContainsKey(attribute2.Trim()))
										{
											dictionary.Add(attribute2.Trim(), attribute3.Trim());
										}
										else
										{
											LogManage.LogError(string.Format("多语言有重复: 表{0} 数据是{1}", attribute, attribute2));
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						dictionary.Clear();
						LogManage.LogError(string.Format("多语言有表错误: 表{0} 错误是{1}  在 重新加载中·· ·", attribute, ex.ToString()));
						XMLParser xMLParser = new XMLParser();
						XMLNode xMLNode = xMLParser.Parse(UnitXML.GetInstance().GetXMLTextByName(attribute));
						XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
						if (nodeList == null)
						{
							Debug.LogError("not contain XML" + attribute);
							continue;
						}
						for (int j = 0; j < nodeList.Count; j++)
						{
							string value = xMLNode.GetValue("configures>0>configure>" + j + ">@id");
							string value2 = xMLNode.GetValue("configures>0>configure>" + j + ">@value");
							if (!string.IsNullOrEmpty(value))
							{
								if (!dictionary.ContainsKey(value.Trim()))
								{
									dictionary.Add(value.Trim(), value2.Trim());
								}
								else
								{
									dictionary[value.Trim()] = value2.Trim();
									LogManage.LogError(string.Format("多语言有重复: 表{0} 数据是{1}", attribute, value));
								}
							}
						}
					}
					UnitConst.GetInstance().languageinfo.Add(attribute.Trim(), dictionary);
				}
			}
		}
		if (LanguageManage.LangeChange != null)
		{
			LanguageManage.LangeChange();
		}
	}

	private static void LoadLanguage_Resource()
	{
		TextAsset[] array = Resources.LoadAll<TextAsset>("Language");
		TextAsset[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			TextAsset textAsset = array2[i];
			XMLParser xMLParser = new XMLParser();
			XMLNode xMLNode = xMLParser.Parse(textAsset.text);
			XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
			for (int j = 0; j < nodeList.Count; j++)
			{
				string value = xMLNode.GetValue("configures>0>configure>" + j + ">@id");
				string value2 = xMLNode.GetValue("configures>0>configure>" + j + ">@value");
				if (!string.IsNullOrEmpty(value))
				{
					LanguageManage.languageinfo_Client.Add(value.Trim(), value2.Trim());
				}
			}
		}
	}

	public static string GetTextByKey(string textKey, string pickName)
	{
		if (string.IsNullOrEmpty(textKey))
		{
			return string.Empty;
		}
		string key = LanguageManage.selectedLanguage.Id + "_" + pickName;
		if (UnitConst.GetInstance().languageinfo.ContainsKey(key))
		{
			if (UnitConst.GetInstance().languageinfo[key].ContainsKey(textKey.Trim()))
			{
				return UnitConst.GetInstance().languageinfo[key][textKey.Trim()];
			}
		}
		else
		{
			if (LanguageManage.languageinfo_Client.Count == 0)
			{
				LanguageManage.LoadLanguage_Resource();
			}
			if (LanguageManage.languageinfo_Client.ContainsKey(textKey))
			{
				return LanguageManage.languageinfo_Client[textKey];
			}
		}
		return pickName + "!==! 表不包含" + textKey;
	}

	public static string GetTextByKey(string textKey, string pickName, ref bool isTransLator)
	{
		isTransLator = false;
		if (string.IsNullOrEmpty(textKey))
		{
			return string.Empty;
		}
		string key = LanguageManage.selectedLanguage.Id + "_" + pickName;
		if (UnitConst.GetInstance().languageinfo.ContainsKey(key))
		{
			if (UnitConst.GetInstance().languageinfo[key].ContainsKey(textKey.Trim()))
			{
				string result = UnitConst.GetInstance().languageinfo[key][textKey.Trim()];
				isTransLator = true;
				return result;
			}
		}
		else
		{
			if (LanguageManage.languageinfo_Client.Count == 0)
			{
				LanguageManage.LoadLanguage_Resource();
			}
			if (LanguageManage.languageinfo_Client.ContainsKey(textKey))
			{
				isTransLator = true;
				return LanguageManage.languageinfo_Client[textKey];
			}
		}
		return pickName + "!==! 表不包含" + textKey;
	}
}
