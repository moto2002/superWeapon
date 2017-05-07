using msg;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public sealed class BattleFieldBox
{
	public class BattleFieldBoxConst
	{
		public int id;

		public int level;

		public int quility;

		public string name;

		public Dictionary<ResType, int> res_View = new Dictionary<ResType, int>();

		public Dictionary<ResType, int> res_ViewQuility = new Dictionary<ResType, int>();

		public Dictionary<int, int> items_View = new Dictionary<int, int>();

		public Dictionary<int, int> skills_View = new Dictionary<int, int>();

		public int keyId;

		public string icon;

		public string description;
	}

	public class BattleBoxConst
	{
		public int id;

		public int quility;

		public string name;

		public Dictionary<ResType, int> res_View = new Dictionary<ResType, int>();

		public Dictionary<ResType, int> res_ViewQuility = new Dictionary<ResType, int>();

		public Dictionary<int, int> items_View = new Dictionary<int, int>();

		public Dictionary<int, int> skills_View = new Dictionary<int, int>();

		public string description;
	}

	public static Dictionary<int, BattleFieldBox.BattleFieldBoxConst> BattleFieldBox_PlannerData = new Dictionary<int, BattleFieldBox.BattleFieldBoxConst>();

	public static Dictionary<int, BattleFieldBox.BattleBoxConst> BattleBox_PlannerData = new Dictionary<int, BattleFieldBox.BattleBoxConst>();

	public static Dictionary<int, List<KVStruct>> battleFieldBoxes = new Dictionary<int, List<KVStruct>>();

	public static void LoadClientData()
	{
		BattleFieldBox.BattleFieldBox_PlannerData.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BattleBox"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					int level = int.Parse(xmlTextReader.GetAttribute("level"));
					int quility = int.Parse(xmlTextReader.GetAttribute("quility"));
					int keyId = int.Parse(xmlTextReader.GetAttribute("keyId"));
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("viewRes");
					string attribute3 = xmlTextReader.GetAttribute("resQuility");
					string attribute4 = xmlTextReader.GetAttribute("viewItems");
					string attribute5 = xmlTextReader.GetAttribute("viewSkills");
					string attribute6 = xmlTextReader.GetAttribute("icon");
					string attribute7 = xmlTextReader.GetAttribute("description");
					BattleFieldBox.BattleFieldBoxConst battleFieldBoxConst = new BattleFieldBox.BattleFieldBoxConst();
					battleFieldBoxConst.id = id;
					battleFieldBoxConst.level = level;
					battleFieldBoxConst.quility = quility;
					battleFieldBoxConst.keyId = keyId;
					battleFieldBoxConst.icon = attribute6;
					battleFieldBoxConst.description = attribute7;
					if (!string.IsNullOrEmpty(attribute2))
					{
						string[] array = attribute2.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (text.Contains(":"))
							{
								battleFieldBoxConst.res_View.Add((ResType)int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute))
					{
						battleFieldBoxConst.name = attribute;
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						string[] array2 = attribute3.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(":"))
							{
								battleFieldBoxConst.res_ViewQuility.Add((ResType)int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						string[] array3 = attribute4.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							if (text3.Contains(":"))
							{
								if (battleFieldBoxConst.items_View.ContainsKey(int.Parse(text3.Split(new char[]
								{
									':'
								})[0])))
								{
									Debug.LogError(string.Format("ID:{0} items_View有相同的Key", battleFieldBoxConst.id));
								}
								battleFieldBoxConst.items_View.Add(int.Parse(text3.Split(new char[]
								{
									':'
								})[0]), int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array4 = attribute5.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							string text4 = array4[l];
							if (text4.Contains(":"))
							{
								battleFieldBoxConst.skills_View.Add(int.Parse(text4.Split(new char[]
								{
									':'
								})[0]), int.Parse(text4.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					BattleFieldBox.BattleFieldBox_PlannerData.Add(battleFieldBoxConst.id, battleFieldBoxConst);
				}
			}
		}
		BattleFieldBox.BattleBox_PlannerData.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BattleAllPassBox"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					int id2 = int.Parse(xmlTextReader2.GetAttribute("id"));
					int quility2 = int.Parse(xmlTextReader2.GetAttribute("quality"));
					string attribute8 = xmlTextReader2.GetAttribute("name");
					string attribute9 = xmlTextReader2.GetAttribute("res");
					string attribute10 = xmlTextReader2.GetAttribute("items");
					string attribute11 = xmlTextReader2.GetAttribute("skills");
					string attribute12 = xmlTextReader2.GetAttribute("description");
					BattleFieldBox.BattleBoxConst battleBoxConst = new BattleFieldBox.BattleBoxConst();
					battleBoxConst.id = id2;
					battleBoxConst.quility = quility2;
					battleBoxConst.description = attribute12;
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array5 = attribute9.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array5.Length; m++)
						{
							string text5 = array5[m];
							if (text5.Contains(":"))
							{
								battleBoxConst.res_View.Add((ResType)int.Parse(text5.Split(new char[]
								{
									':'
								})[0]), int.Parse(text5.Split(new char[]
								{
									':'
								})[1]));
								battleBoxConst.res_ViewQuility.Add((ResType)int.Parse(text5.Split(new char[]
								{
									':'
								})[0]), 1);
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						battleBoxConst.name = attribute8;
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						string[] array6 = attribute10.Split(new char[]
						{
							','
						});
						for (int n = 0; n < array6.Length; n++)
						{
							string text6 = array6[n];
							if (text6.Contains(":"))
							{
								battleBoxConst.items_View.Add(int.Parse(text6.Split(new char[]
								{
									':'
								})[0]), int.Parse(text6.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						string[] array7 = attribute11.Split(new char[]
						{
							','
						});
						for (int num = 0; num < array7.Length; num++)
						{
							string text7 = array7[num];
							if (text7.Contains(":"))
							{
								battleBoxConst.skills_View.Add(int.Parse(text7.Split(new char[]
								{
									':'
								})[0]), int.Parse(text7.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					BattleFieldBox.BattleBox_PlannerData.Add(battleBoxConst.id, battleBoxConst);
				}
			}
		}
	}
}
