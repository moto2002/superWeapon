using System;
using System.Xml;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
	public static void NewReadTowerUpdateXml()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("TowerUpGrade"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("itemId");
					string attribute3 = xmlTextReader.GetAttribute("level");
					string attribute4 = xmlTextReader.GetAttribute("needLevel");
					string attribute5 = xmlTextReader.GetAttribute("resCost");
					string attribute6 = xmlTextReader.GetAttribute("itemCost");
					string attribute7 = xmlTextReader.GetAttribute("bodyId");
					string attribute8 = xmlTextReader.GetAttribute("description");
					string attribute9 = xmlTextReader.GetAttribute("frequency");
					string attribute10 = xmlTextReader.GetAttribute("renju");
					string attribute11 = xmlTextReader.GetAttribute("renjuCD");
					string attribute12 = xmlTextReader.GetAttribute("headRotationSpeed");
					string attribute13 = xmlTextReader.GetAttribute("hurtRadius");
					string attribute14 = xmlTextReader.GetAttribute("BuffIdx");
					string attribute15 = xmlTextReader.GetAttribute("isTrack");
					string attribute16 = xmlTextReader.GetAttribute("attackPoint");
					string attribute17 = xmlTextReader.GetAttribute("GetTarType");
					string attribute18 = xmlTextReader.GetAttribute("bulletType");
					string attribute19 = xmlTextReader.GetAttribute("fightEffect");
					string attribute20 = xmlTextReader.GetAttribute("BodyEffect");
					string attribute21 = xmlTextReader.GetAttribute("DamageEffect");
					string attribute22 = xmlTextReader.GetAttribute("specialshow");
					string attribute23 = xmlTextReader.GetAttribute("critPer");
					string attribute24 = xmlTextReader.GetAttribute("resistPer");
					string attribute25 = xmlTextReader.GetAttribute("avoidDef");
					string attribute26 = xmlTextReader.GetAttribute("makeshow");
					TowerUpdate towerUpdate = new TowerUpdate();
					if (!string.IsNullOrEmpty(attribute))
					{
						towerUpdate.id = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						towerUpdate.itemid = int.Parse(attribute2);
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						towerUpdate.level = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						towerUpdate.needlevel = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute26))
					{
						towerUpdate.makeShow = new Vector3[3];
						string[] array = attribute26.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array2 = attribute26.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						string[] array3 = attribute26.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						});
						towerUpdate.makeShow[0] = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
						towerUpdate.makeShow[1] = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
						towerUpdate.makeShow[2] = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
					}
					else
					{
						towerUpdate.makeShow = new Vector3[3];
						towerUpdate.makeShow[0] = Vector3.one;
						towerUpdate.makeShow[1] = Vector3.one;
						towerUpdate.makeShow[2] = Vector3.one;
					}
					string[] array4 = attribute5.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array4.Length; i++)
					{
						string text = array4[i];
						if (text.Contains(":"))
						{
							switch (int.Parse(text.Split(new char[]
							{
								':'
							})[0]))
							{
							case 1:
								towerUpdate.resCost.Add(ResType.金币, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 2:
								towerUpdate.resCost.Add(ResType.石油, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 3:
								towerUpdate.resCost.Add(ResType.钢铁, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 4:
								towerUpdate.resCost.Add(ResType.稀矿, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							}
						}
					}
					string[] array5 = attribute6.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array5.Length; j++)
					{
						string text2 = array5[j];
						if (text2.Contains(":"))
						{
							towerUpdate.itemCost.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						towerUpdate.bodyName = attribute7;
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						towerUpdate.des = attribute8;
					}
					if (!string.IsNullOrEmpty(attribute22))
					{
						string[] array6 = attribute22.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array6.Length; k++)
						{
							string s = array6[k];
							towerUpdate.specialshow.Add(int.Parse(s));
						}
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						towerUpdate.frequency = float.Parse(attribute9);
					}
					if (!string.IsNullOrEmpty(attribute23))
					{
						towerUpdate.critPer = float.Parse(attribute23);
					}
					if (!string.IsNullOrEmpty(attribute24))
					{
						towerUpdate.resistPer = int.Parse(attribute24);
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						towerUpdate.renju = int.Parse(attribute10);
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						towerUpdate.renjuCD = float.Parse(attribute11);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						towerUpdate.headRotationSpeed = (float)int.Parse(attribute12);
					}
					if (!string.IsNullOrEmpty(attribute25))
					{
						towerUpdate.avoidDef = int.Parse(attribute25);
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						towerUpdate.hurtRadius = (float)int.Parse(attribute13);
					}
					if (!string.IsNullOrEmpty(attribute14))
					{
						string[] array7 = attribute14.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array7.Length; l++)
						{
							string s2 = array7[l];
							towerUpdate.buffid.Add(int.Parse(s2));
						}
					}
					if (!string.IsNullOrEmpty(attribute15))
					{
						towerUpdate.isTrack = int.Parse(attribute15);
					}
					if (!string.IsNullOrEmpty(attribute16))
					{
						towerUpdate.attackPoint = int.Parse(attribute16);
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						towerUpdate.GetTarType = int.Parse(attribute17);
					}
					if (!string.IsNullOrEmpty(attribute18))
					{
						towerUpdate.bulletType = int.Parse(attribute18);
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						towerUpdate.fightEffect = attribute19;
					}
					if (!string.IsNullOrEmpty(attribute20))
					{
						towerUpdate.BodyEffect = attribute20;
					}
					if (!string.IsNullOrEmpty(attribute21))
					{
						towerUpdate.DamageEffect = attribute21;
					}
					UnitConst.GetInstance().buildingConst[towerUpdate.itemid].UpdateStarInfos.Add(towerUpdate);
				}
			}
		}
	}

	public static void NewReadTowerStrengthXml()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("TowerStrength"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("itemId");
					string attribute3 = xmlTextReader.GetAttribute("level");
					string attribute4 = xmlTextReader.GetAttribute("needLevel");
					string attribute5 = xmlTextReader.GetAttribute("resCost");
					string attribute6 = xmlTextReader.GetAttribute("itemCost");
					string attribute7 = xmlTextReader.GetAttribute("attribute");
					string attribute8 = xmlTextReader.GetAttribute("description");
					string attribute9 = xmlTextReader.GetAttribute("makeshow");
					TowerStrong towerStrong = new TowerStrong();
					if (!string.IsNullOrEmpty(attribute))
					{
						towerStrong.id = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						towerStrong.towerId = int.Parse(attribute2);
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						towerStrong.level = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						towerStrong.needLevel = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array = attribute5.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (text.Contains(":"))
							{
								switch (int.Parse(text.Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									towerStrong.rescost.Add(ResType.金币, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									towerStrong.rescost.Add(ResType.石油, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									towerStrong.rescost.Add(ResType.钢铁, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									towerStrong.rescost.Add(ResType.稀矿, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array2 = attribute6.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(":"))
							{
								towerStrong.itemCost.Add(int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						string[] array3 = attribute7.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							if (text3.Contains(":"))
							{
								string text4 = text3.Split(new char[]
								{
									':'
								})[0];
								switch (text4)
								{
								case "1":
									towerStrong.attribute.Add(int.Parse(text3.Split(new char[]
									{
										':'
									})[0]), int.Parse(text3.Split(new char[]
									{
										':'
									})[1]));
									break;
								case "2":
									towerStrong.attribute.Add(int.Parse(text3.Split(new char[]
									{
										':'
									})[0]), int.Parse(text3.Split(new char[]
									{
										':'
									})[1]));
									break;
								case "3":
									towerStrong.attribute.Add(int.Parse(text3.Split(new char[]
									{
										':'
									})[0]), int.Parse(text3.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						towerStrong.des = attribute8;
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						towerStrong.makeShow = new Vector3[3];
						string[] array4 = attribute9.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array5 = attribute9.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						string[] array6 = attribute9.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						});
						towerStrong.makeShow[0] = new Vector3(float.Parse(array4[0]), float.Parse(array4[1]), float.Parse(array4[2]));
						towerStrong.makeShow[1] = new Vector3(float.Parse(array5[0]), float.Parse(array5[1]), float.Parse(array5[2]));
						towerStrong.makeShow[2] = new Vector3(float.Parse(array6[0]), float.Parse(array6[1]), float.Parse(array6[2]));
					}
					else
					{
						towerStrong.makeShow = new Vector3[3];
						towerStrong.makeShow[0] = Vector3.one;
						towerStrong.makeShow[1] = Vector3.one;
						towerStrong.makeShow[2] = Vector3.one;
					}
					UnitConst.GetInstance().buildingConst[towerStrong.towerId].StrongInfos.Add(towerStrong);
				}
			}
		}
	}

	public static void NewReadBuildingUpGradeXml()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BuildingUpGrade"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("itemId");
					string attribute3 = xmlTextReader.GetAttribute("level");
					string attribute4 = xmlTextReader.GetAttribute("needLevel");
					string attribute5 = xmlTextReader.GetAttribute("resCost");
					string attribute6 = xmlTextReader.GetAttribute("itemCost");
					string attribute7 = xmlTextReader.GetAttribute("specialshow");
					string attribute8 = xmlTextReader.GetAttribute("description");
					string attribute9 = xmlTextReader.GetAttribute("bodyId");
					string attribute10 = xmlTextReader.GetAttribute("outPut");
					string attribute11 = xmlTextReader.GetAttribute("outPutLimit");
					string attribute12 = xmlTextReader.GetAttribute("makeshow");
					BuildingGrade buildingGrade = new BuildingGrade();
					if (!string.IsNullOrEmpty(attribute))
					{
						buildingGrade.id = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						buildingGrade.itemid = int.Parse(attribute2);
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						buildingGrade.level = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						buildingGrade.bodyName = attribute9;
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						buildingGrade.needLevel = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						buildingGrade.makeShow = new Vector3[3];
						string[] array = attribute12.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array2 = attribute12.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						string[] array3 = attribute12.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						});
						buildingGrade.makeShow[0] = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
						buildingGrade.makeShow[1] = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
						buildingGrade.makeShow[2] = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
					}
					else
					{
						buildingGrade.makeShow = new Vector3[3];
						buildingGrade.makeShow[0] = Vector3.one;
						buildingGrade.makeShow[1] = Vector3.one;
						buildingGrade.makeShow[2] = Vector3.one;
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array4 = attribute5.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array4.Length; i++)
						{
							string text = array4[i];
							if (text.Contains(":"))
							{
								switch (int.Parse(text.Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									buildingGrade.resCost.Add(ResType.金币, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									buildingGrade.resCost.Add(ResType.石油, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									buildingGrade.resCost.Add(ResType.钢铁, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									buildingGrade.resCost.Add(ResType.稀矿, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						buildingGrade.des = attribute8;
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array5 = attribute6.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array5.Length; j++)
						{
							string text2 = array5[j];
							if (text2.Contains(":"))
							{
								buildingGrade.itemCost.Add(int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						buildingGrade.output = int.Parse(attribute10);
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						buildingGrade.outlimit = int.Parse(attribute11);
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						string[] array6 = attribute7.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array6.Length; k++)
						{
							string s = array6[k];
							buildingGrade.buildUpGradeShow.Add(int.Parse(s));
						}
					}
					UnitConst.GetInstance().buildingConst[buildingGrade.itemid].buildGradeInfos.Add(buildingGrade);
				}
			}
		}
	}
}
