using BattleEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class UniteRD
{
	private static UniteRD instance;

	private UniteRD()
	{
	}

	public static UniteRD GetInstance()
	{
		if (UniteRD.instance == null)
		{
			UniteRD.instance = new UniteRD();
		}
		return UniteRD.instance;
	}

	public void ReadGameStartXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("gameStart"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int num = int.Parse(xmlTextReader.GetAttribute("ID"));
					string attribute = xmlTextReader.GetAttribute("type");
					switch (num)
					{
					case 0:
					{
						string[] array = attribute.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							GameStart_AttackNPC.armyData_NpcAttack item = default(GameStart_AttackNPC.armyData_NpcAttack);
							item.armyIndex = int.Parse(text.Split(new char[]
							{
								':'
							})[0]);
							item.armyNum = int.Parse(text.Split(new char[]
							{
								':'
							})[1]);
							item.armyLV = int.Parse(text.Split(new char[]
							{
								':'
							})[2]);
							HeroInfo.GetInstance().gameStart.armys.Add(item);
						}
						break;
					}
					case 1:
					{
						string[] array2 = attribute.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							HeroInfo.GetInstance().gameStart.soliderIndex = int.Parse(text2.Split(new char[]
							{
								':'
							})[0]);
							HeroInfo.GetInstance().gameStart.soliderlV = int.Parse(text2.Split(new char[]
							{
								':'
							})[2]);
						}
						break;
					}
					case 2:
					{
						string[] array3 = attribute.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string s = array3[k];
							HeroInfo.GetInstance().gameStart.skills.Add(int.Parse(s));
						}
						break;
					}
					}
				}
			}
		}
	}

	public void ReadMilitaryRankXML()
	{
		UnitConst.GetInstance().MilitaryRankDataList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("MilitaryRank"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("description");
					int commondLevel = int.Parse(xmlTextReader.GetAttribute("commondLevel"));
					int medal = int.Parse(xmlTextReader.GetAttribute("medal"));
					string attribute3 = xmlTextReader.GetAttribute("resAward");
					string attribute4 = xmlTextReader.GetAttribute("itemAward");
					string attribute5 = xmlTextReader.GetAttribute("diamondAward");
					string attribute6 = xmlTextReader.GetAttribute("skillAward");
					string attribute7 = xmlTextReader.GetAttribute("icon");
					MilitaryRankData militaryRankData = new MilitaryRankData();
					militaryRankData.id = id;
					militaryRankData.name = attribute;
					if (!string.IsNullOrEmpty(attribute2))
					{
						militaryRankData.description = attribute2;
					}
					militaryRankData.icon = attribute7;
					militaryRankData.commondLevel = commondLevel;
					militaryRankData.medal = medal;
					if (!string.IsNullOrEmpty(attribute3))
					{
						string[] array = attribute3.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (text.Contains(':'))
							{
								switch (int.Parse(text.Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									militaryRankData.res.Add(ResType.金币, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									militaryRankData.res.Add(ResType.石油, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									militaryRankData.res.Add(ResType.钢铁, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									militaryRankData.res.Add(ResType.稀矿, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 7:
									militaryRankData.res.Add(ResType.钻石, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						string[] array2 = attribute4.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(':'))
							{
								militaryRankData.items.Add(int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array3 = attribute6.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							if (text3.Contains(':'))
							{
								militaryRankData.skill.Add(int.Parse(text3.Split(new char[]
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
						militaryRankData.money = int.Parse(attribute5);
					}
					UnitConst.GetInstance().MilitaryRankDataList.Add(militaryRankData.id, militaryRankData);
				}
			}
		}
	}

	public void ReadSevenDayXML()
	{
		UnitConst.GetInstance().SevenDay.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DailySign"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("description");
					string attribute3 = xmlTextReader.GetAttribute("money");
					string attribute4 = xmlTextReader.GetAttribute("items");
					string attribute5 = xmlTextReader.GetAttribute("skill");
					string attribute6 = xmlTextReader.GetAttribute("res");
					int goldBox = int.Parse(xmlTextReader.GetAttribute("goldBox"));
					string attribute7 = xmlTextReader.GetAttribute("type");
					string attribute8 = xmlTextReader.GetAttribute("doubleNeedVipLevel");
					SevenDay sevenDay = new SevenDay();
					sevenDay.id = id;
					sevenDay.name = attribute;
					sevenDay.goldBox = goldBox;
					if (!string.IsNullOrEmpty(attribute8))
					{
						sevenDay.doubule = int.Parse(attribute8);
					}
					if (!string.IsNullOrEmpty(attribute7.ToString()))
					{
						sevenDay.type = int.Parse(attribute7);
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						sevenDay.des = attribute2;
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						sevenDay.money = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute4) && attribute4.Contains(':'))
					{
						sevenDay.items.Add(int.Parse(attribute4.Split(new char[]
						{
							':'
						})[0]), int.Parse(attribute4.Split(new char[]
						{
							':'
						})[1]));
					}
					if (!string.IsNullOrEmpty(attribute5) && attribute5.Contains(':'))
					{
						sevenDay.skill.Add(int.Parse(attribute5.Split(new char[]
						{
							':'
						})[0]), int.Parse(attribute5.Split(new char[]
						{
							':'
						})[1]));
					}
					if (!string.IsNullOrEmpty(attribute6) && attribute6.Contains(':'))
					{
						switch (int.Parse(attribute6.Split(new char[]
						{
							':'
						})[0]))
						{
						case 1:
							sevenDay.res.Add(ResType.金币, int.Parse(attribute6.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 2:
							sevenDay.res.Add(ResType.石油, int.Parse(attribute6.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 3:
							sevenDay.res.Add(ResType.钢铁, int.Parse(attribute6.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 4:
							sevenDay.res.Add(ResType.稀矿, int.Parse(attribute6.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 7:
							sevenDay.res.Add(ResType.钻石, int.Parse(attribute6.Split(new char[]
							{
								':'
							})[1]));
							break;
						}
					}
					UnitConst.GetInstance().SevenDay.Add(sevenDay.id, sevenDay);
				}
			}
		}
	}

	public void ReadLoadRewadXML()
	{
		UnitConst.GetInstance().loadReward.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("OnlinePrize"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					string attribute = xmlTextReader.GetAttribute("resPrize");
					string attribute2 = xmlTextReader.GetAttribute("itemPrize");
					string attribute3 = xmlTextReader.GetAttribute("skillPrize");
					string attribute4 = xmlTextReader.GetAttribute("desprtion");
					string attribute5 = xmlTextReader.GetAttribute("moneyPrize");
					LoadReward loadReward = new LoadReward();
					loadReward.id = id;
					if (!string.IsNullOrEmpty(attribute4))
					{
						loadReward.des = attribute4;
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
							if (text.Contains(':'))
							{
								switch (int.Parse(text.Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									loadReward.money.Add(ResType.金币, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									loadReward.money.Add(ResType.石油, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									loadReward.money.Add(ResType.钢铁, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									loadReward.money.Add(ResType.稀矿, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 7:
									loadReward.money.Add(ResType.钻石, int.Parse(text.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute))
					{
						string[] array2 = attribute.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(':'))
							{
								switch (int.Parse(text2.Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									loadReward.res.Add(ResType.金币, int.Parse(text2.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									loadReward.res.Add(ResType.石油, int.Parse(text2.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									loadReward.res.Add(ResType.钢铁, int.Parse(text2.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									loadReward.res.Add(ResType.稀矿, int.Parse(text2.Split(new char[]
									{
										':'
									})[1]));
									break;
								case 7:
									loadReward.res.Add(ResType.钻石, int.Parse(text2.Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						string[] array3 = attribute2.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							if (text3.Contains(':'))
							{
								loadReward.item.Add(int.Parse(text3.Split(new char[]
								{
									':'
								})[0]), int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						string[] array4 = attribute3.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							string text4 = array4[l];
							if (attribute3.Contains(':'))
							{
								loadReward.skill.Add(int.Parse(text4.Split(new char[]
								{
									':'
								})[0]), int.Parse(text4.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().loadReward.Add(loadReward.id, loadReward);
				}
			}
		}
	}

	public void NewReadArmyConstXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Army"), XmlNodeType.Document, null))
		{
			List<NewUnInfo> list = new List<NewUnInfo>();
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("type");
					string attribute2 = xmlTextReader.GetAttribute("id");
					string attribute3 = xmlTextReader.GetAttribute("name");
					string attribute4 = xmlTextReader.GetAttribute("icon");
					string attribute5 = xmlTextReader.GetAttribute("bodyID");
					string attribute6 = xmlTextReader.GetAttribute("description");
					string attribute7 = xmlTextReader.GetAttribute("amySize");
					string attribute8 = xmlTextReader.GetAttribute("hight");
					string attribute9 = xmlTextReader.GetAttribute("speed");
					string attribute10 = xmlTextReader.GetAttribute("roatSpeed");
					string attribute11 = xmlTextReader.GetAttribute("headSpeed");
					string attribute12 = xmlTextReader.GetAttribute("peopleNum");
					string attribute13 = xmlTextReader.GetAttribute("maxRadius");
					string attribute14 = xmlTextReader.GetAttribute("minRadius");
					string attribute15 = xmlTextReader.GetAttribute("dirtyBack");
					string attribute16 = xmlTextReader.GetAttribute("bornType");
					string attribute17 = xmlTextReader.GetAttribute("presence");
					string attribute18 = xmlTextReader.GetAttribute("accelerationSpeed");
					string attribute19 = xmlTextReader.GetAttribute("count");
					string attribute20 = xmlTextReader.GetAttribute("floowRadius");
					string attribute21 = xmlTextReader.GetAttribute("eyeRadius");
					string attribute22 = xmlTextReader.GetAttribute("show");
					string attribute23 = xmlTextReader.GetAttribute("uiSize");
					string attribute24 = xmlTextReader.GetAttribute("isTrack");
					string attribute25 = xmlTextReader.GetAttribute("attackPoint");
					string attribute26 = xmlTextReader.GetAttribute("GetTarType");
					string attribute27 = xmlTextReader.GetAttribute("bulletType");
					string attribute28 = xmlTextReader.GetAttribute("fightEffect");
					string attribute29 = xmlTextReader.GetAttribute("BodyEffect");
					string attribute30 = xmlTextReader.GetAttribute("DamageEffect");
					string attribute31 = xmlTextReader.GetAttribute("fightSound");
					string attribute32 = xmlTextReader.GetAttribute("DamageSound");
					string attribute33 = xmlTextReader.GetAttribute("frequency");
					string attribute34 = xmlTextReader.GetAttribute("frequency1");
					string attribute35 = xmlTextReader.GetAttribute("angle");
					string attribute36 = xmlTextReader.GetAttribute("renju");
					string attribute37 = xmlTextReader.GetAttribute("renjuCD");
					string attribute38 = xmlTextReader.GetAttribute("isEndLianji");
					string attribute39 = xmlTextReader.GetAttribute("isShootSearchTarget");
					string attribute40 = xmlTextReader.GetAttribute("bulletSpeed");
					string attribute41 = xmlTextReader.GetAttribute("hurtRadius");
					string attribute42 = xmlTextReader.GetAttribute("BuffIdx");
					string attribute43 = xmlTextReader.GetAttribute("BulletInAngle");
					string attribute44 = xmlTextReader.GetAttribute("IsMultipleAttack");
					string attribute45 = xmlTextReader.GetAttribute("star");
					string attribute46 = xmlTextReader.GetAttribute("restrain");
					string attribute47 = xmlTextReader.GetAttribute("hitSound");
					string attribute48 = xmlTextReader.GetAttribute("isByPhysical");
					string attribute49 = xmlTextReader.GetAttribute("canFly");
					string attribute50 = xmlTextReader.GetAttribute("army_UpdateScale");
					string attribute51 = xmlTextReader.GetAttribute("army_UpdatePosition");
					string attribute52 = xmlTextReader.GetAttribute("army_UpdateRotation");
					string attribute53 = xmlTextReader.GetAttribute("army_UpdateTaziScale");
					string attribute54 = xmlTextReader.GetAttribute("army_UpdateTaziPosition");
					string attribute55 = xmlTextReader.GetAttribute("army_UpdateTaziRotation");
					string attribute56 = xmlTextReader.GetAttribute("army_TuoZhuaiScale");
					string attribute57 = xmlTextReader.GetAttribute("army_UpdateTaziScale_InCenter");
					string attribute58 = xmlTextReader.GetAttribute("army_UpdateScale_InCenter");
					string attribute59 = xmlTextReader.GetAttribute("army_JiJieDianScale");
					string attribute60 = xmlTextReader.GetAttribute("army_JiJieDianPosition");
					string attribute61 = xmlTextReader.GetAttribute("army_JiJieDianRotation");
					string attribute62 = xmlTextReader.GetAttribute("buildTime");
					string attribute63 = xmlTextReader.GetAttribute("modelclear");
					NewUnInfo newUnInfo = new NewUnInfo();
					list.Add(newUnInfo);
					if (!string.IsNullOrEmpty(attribute50))
					{
						newUnInfo.army_UpdateScale = new Vector3(float.Parse(attribute50.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute50.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute50.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute51))
					{
						newUnInfo.army_UpdatePosition = new Vector3(float.Parse(attribute51.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute51.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute51.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute52))
					{
						newUnInfo.army_UpdateRotation = new Vector3(float.Parse(attribute52.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute52.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute52.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute53))
					{
						newUnInfo.army_UpdateTaziScale = new Vector3(float.Parse(attribute53.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute53.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute53.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute54))
					{
						newUnInfo.army_UpdateTaziPosition = new Vector3(float.Parse(attribute54.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute54.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute54.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute55))
					{
						newUnInfo.army_UpdateTaziRotation = new Vector3(float.Parse(attribute55.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute55.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute55.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute56))
					{
						newUnInfo.army_TuoZhuaiScale = new Vector3(float.Parse(attribute56.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute56.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute56.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute57))
					{
						newUnInfo.army_UpdateTaziScale_InCenter = new Vector3(float.Parse(attribute57.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute57.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute57.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute58))
					{
						newUnInfo.army_UpdateScale_InCenter = new Vector3(float.Parse(attribute58.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute58.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute58.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute59))
					{
						newUnInfo.army_JiJieDianScale = new Vector3(float.Parse(attribute59.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute59.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute59.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute60))
					{
						newUnInfo.army_JiJieDianPosition = new Vector3(float.Parse(attribute60.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute60.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute60.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute61))
					{
						newUnInfo.army_JiJieDianRotation = new Vector3(float.Parse(attribute61.Split(new char[]
						{
							','
						})[0]), float.Parse(attribute61.Split(new char[]
						{
							','
						})[1]), float.Parse(attribute61.Split(new char[]
						{
							','
						})[2]));
					}
					if (!string.IsNullOrEmpty(attribute63))
					{
						newUnInfo.modelclearPos_TInfo = new Vector3(float.Parse(attribute63.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						})[0]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						})[1]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						})[2]));
						newUnInfo.modelclearRotation_TInfo = new Vector3(float.Parse(attribute63.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						})[0]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						})[1]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						})[2]));
						newUnInfo.modelclearScale_TInfo = new Vector3(float.Parse(attribute63.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						})[0]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						})[1]), float.Parse(attribute63.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						})[2]));
					}
					newUnInfo.unitType = int.Parse(attribute);
					newUnInfo.unitId = int.Parse(attribute2);
					newUnInfo.name = attribute3;
					newUnInfo.icon = attribute4;
					newUnInfo.bodyId = attribute5;
					newUnInfo.description = attribute6;
					if (!string.IsNullOrEmpty(attribute46))
					{
						newUnInfo.restraint = int.Parse(attribute46);
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						newUnInfo.counts = int.Parse(attribute19);
					}
					if (!string.IsNullOrEmpty(attribute23))
					{
						newUnInfo.uisize = float.Parse(attribute23);
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						newUnInfo.size = float.Parse(attribute7);
					}
					newUnInfo.isByPhysic = (string.IsNullOrEmpty(attribute48) || attribute48 == "1");
					newUnInfo.hight = ((!string.IsNullOrEmpty(attribute8)) ? float.Parse(attribute8) : 0f);
					newUnInfo.IsMultipleAttack = (!string.IsNullOrEmpty(attribute44) && attribute44.Trim().Equals("1"));
					if (!string.IsNullOrEmpty(attribute9))
					{
						newUnInfo.speed = float.Parse(attribute9);
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						newUnInfo.roatSpeed = float.Parse(attribute10);
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						newUnInfo.headSpeed = float.Parse(attribute11);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						newUnInfo.peopleNum = int.Parse(attribute12);
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						newUnInfo.maxRadius = float.Parse(attribute13);
					}
					if (!string.IsNullOrEmpty(attribute14))
					{
						newUnInfo.minRadius = float.Parse(attribute14);
					}
					if (!string.IsNullOrEmpty(attribute20))
					{
						newUnInfo.floowRadius = float.Parse(attribute20);
					}
					if (!string.IsNullOrEmpty(attribute21))
					{
						newUnInfo.eyeRadius = float.Parse(attribute21);
					}
					else
					{
						newUnInfo.eyeRadius = newUnInfo.maxRadius * 1.5f;
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						newUnInfo.presence = int.Parse(attribute17);
					}
					if (attribute17 == "2" || attribute17 == "3")
					{
						newUnInfo.patrol = true;
					}
					if (attribute17 == "4" || attribute17 == "3")
					{
						newUnInfo.inFeiji = true;
					}
					newUnInfo.isCanFly = (int.Parse(attribute49) == 1);
					newUnInfo.dirtyBack = attribute15;
					newUnInfo.funcTime = ((!string.IsNullOrEmpty(attribute62)) ? float.Parse(attribute62) : 1f);
					newUnInfo.bornType = ((!string.IsNullOrEmpty(attribute16)) ? int.Parse(attribute16) : 0);
					newUnInfo.accelerationSpeed = ((!string.IsNullOrEmpty(attribute18)) ? float.Parse(attribute18) : 0f);
					newUnInfo.hitSound = attribute47;
					if (!string.IsNullOrEmpty(attribute22))
					{
						string[] array = attribute22.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string s = array[i];
							newUnInfo.uiShows.Add(int.Parse(s));
						}
					}
					if (!string.IsNullOrEmpty(attribute45))
					{
						string[] array2 = attribute45.Split(new char[]
						{
							','
						});
						newUnInfo.lifeStar = int.Parse(array2[0]);
						newUnInfo.attackStar = int.Parse(array2[1]);
						newUnInfo.defendStar = int.Parse(array2[2]);
						newUnInfo.speedStar = int.Parse(array2[3]);
						newUnInfo.shootFarStar = int.Parse(array2[4]);
					}
					newUnInfo.isTrack = (!string.IsNullOrEmpty(attribute24) && attribute24 == "1");
					newUnInfo.attackPoint = (Enum_AttackPointType)((!string.IsNullOrEmpty(attribute25)) ? int.Parse(attribute25) : 0);
					newUnInfo.GetTarType = (Enum_GetTargetType)((!string.IsNullOrEmpty(attribute26)) ? int.Parse(attribute26) : 0);
					newUnInfo.bulletType = ((!string.IsNullOrEmpty(attribute27)) ? int.Parse(attribute27) : 0);
					newUnInfo.fightEffect = attribute28;
					newUnInfo.BodyEffect = attribute29;
					newUnInfo.DamageEffect = attribute30;
					newUnInfo.fightSound = attribute31;
					newUnInfo.DamageSound = attribute32;
					newUnInfo.frequency = ((!string.IsNullOrEmpty(attribute33)) ? float.Parse(attribute33) : 0f);
					newUnInfo.frequency1 = ((!string.IsNullOrEmpty(attribute34)) ? float.Parse(attribute34) : 0f);
					newUnInfo.angle = ((!string.IsNullOrEmpty(attribute35)) ? int.Parse(attribute35) : 0);
					newUnInfo.renju = ((!string.IsNullOrEmpty(attribute36)) ? int.Parse(attribute36) : 0);
					newUnInfo.renjuCD = ((!string.IsNullOrEmpty(attribute37)) ? float.Parse(attribute37) : 0f);
					newUnInfo.isEndLianji = (!string.IsNullOrEmpty(attribute38) && attribute38 == "1");
					newUnInfo.isShootSearchTarget = (!string.IsNullOrEmpty(attribute39) && attribute39 == "1");
					newUnInfo.bulletSpeed = ((!string.IsNullOrEmpty(attribute40)) ? int.Parse(attribute40) : 0);
					newUnInfo.hurtRadius = ((!string.IsNullOrEmpty(attribute41)) ? float.Parse(attribute41) : 0f);
					if (!string.IsNullOrEmpty(attribute42))
					{
						string[] array3 = attribute42.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array3.Length; j++)
						{
							string s2 = array3[j];
							newUnInfo.BuffIdx.Add(int.Parse(s2));
						}
					}
					newUnInfo.BulletInAngle = ((!string.IsNullOrEmpty(attribute43)) ? int.Parse(attribute43) : 0);
				}
			}
			UnitConst.GetInstance().soldierConst = list.ToArray();
		}
	}

	public void NewReadArmyLvXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ArmyUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("itemId");
					string attribute2 = xmlTextReader.GetAttribute("level");
					string attribute3 = xmlTextReader.GetAttribute("life");
					string attribute4 = xmlTextReader.GetAttribute("cost");
					string attribute5 = xmlTextReader.GetAttribute("buyCost");
					string attribute6 = xmlTextReader.GetAttribute("costTime");
					string attribute7 = xmlTextReader.GetAttribute("costTime");
					string attribute8 = xmlTextReader.GetAttribute("playerExp");
					string attribute9 = xmlTextReader.GetAttribute("reduceCommondLifePer");
					string attribute10 = xmlTextReader.GetAttribute("breakArmor");
					string attribute11 = xmlTextReader.GetAttribute("defBreak");
					string attribute12 = xmlTextReader.GetAttribute("hitArmor");
					string attribute13 = xmlTextReader.GetAttribute("defHit");
					string attribute14 = xmlTextReader.GetAttribute("critPer");
					string attribute15 = xmlTextReader.GetAttribute("resistPer");
					string attribute16 = xmlTextReader.GetAttribute("critHR");
					string attribute17 = xmlTextReader.GetAttribute("trueDamage");
					string attribute18 = xmlTextReader.GetAttribute("reDamage");
					string attribute19 = xmlTextReader.GetAttribute("reChancePer");
					string attribute20 = xmlTextReader.GetAttribute("avoidDef");
					string attribute21 = xmlTextReader.GetAttribute("skills");
					string attribute22 = xmlTextReader.GetAttribute("skill");
					NewUnLvInfo newUnLvInfo = new NewUnLvInfo();
					newUnLvInfo.lv = int.Parse(attribute2);
					string[] array = attribute4.Split(new char[]
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
								newUnLvInfo.resCost.Add(ResType.金币, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 2:
								newUnLvInfo.resCost.Add(ResType.石油, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 3:
								newUnLvInfo.resCost.Add(ResType.钢铁, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 4:
								newUnLvInfo.resCost.Add(ResType.稀矿, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 7:
								newUnLvInfo.resCost.Add(ResType.钻石, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 9:
								newUnLvInfo.resCost.Add(ResType.经验, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 11:
								newUnLvInfo.resCost.Add(ResType.奖牌, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 12:
								newUnLvInfo.resCost.Add(ResType.军令, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						newUnLvInfo.BuyCost = int.Parse(attribute5);
					}
					newUnLvInfo.playerExp = int.Parse(attribute8);
					newUnLvInfo.skillIds = attribute21;
					newUnLvInfo.skillId = attribute22;
					if (!string.IsNullOrEmpty(attribute6))
					{
						newUnLvInfo.cdTime = int.Parse(attribute6);
					}
					newUnLvInfo.fightInfo = default(BaseFightInfo);
					if (!int.TryParse(attribute10, out newUnLvInfo.fightInfo.breakArmor))
					{
						LogManage.Log(UnitConst.GetInstance().soldierConst[int.Parse(attribute)].name + "::::" + newUnLvInfo.lv);
					}
					newUnLvInfo.fightInfo.life = int.Parse(attribute3);
					newUnLvInfo.fightInfo.defBreak = int.Parse(attribute11);
					newUnLvInfo.fightInfo.hitArmor = int.Parse(attribute12);
					newUnLvInfo.fightInfo.defHit = int.Parse(attribute13);
					newUnLvInfo.fightInfo.crit = int.Parse(attribute14);
					newUnLvInfo.fightInfo.resist = int.Parse(attribute15);
					newUnLvInfo.fightInfo.critHR = int.Parse(attribute16);
					newUnLvInfo.fightInfo.trueDamage = int.Parse(attribute17);
					newUnLvInfo.fightInfo.reDamage = int.Parse(attribute18);
					newUnLvInfo.fightInfo.reChancePer = int.Parse(attribute19);
					newUnLvInfo.fightInfo.avoiddef = int.Parse(attribute20);
					if (newUnLvInfo.lv == 1 && UnitConst.GetInstance().soldierConst[int.Parse(attribute)].lvInfos.Count == 0)
					{
						UnitConst.GetInstance().soldierConst[int.Parse(attribute)].lvInfos.Add(newUnLvInfo);
					}
					UnitConst.GetInstance().soldierConst[int.Parse(attribute)].lvInfos.Add(newUnLvInfo);
				}
			}
		}
	}

	public void NewReadUNArmyStarXML()
	{
		UnitConst.GetInstance().armyStarConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ArmyStar"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("level");
					string attribute3 = xmlTextReader.GetAttribute("num");
					string attribute4 = xmlTextReader.GetAttribute("needExp");
					ArmyStar armyStar = new ArmyStar();
					armyStar.id = int.Parse(attribute);
					armyStar.level = int.Parse(attribute2);
					armyStar.needExp = int.Parse(attribute4);
					armyStar.num = int.Parse(attribute3);
					UnitConst.GetInstance().armyStarConst.Add(armyStar.level, armyStar);
				}
			}
		}
	}

	public void HintXML()
	{
		UnitConst.GetInstance().hintPanel.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("HalfTalk"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("type");
					string attribute3 = xmlTextReader.GetAttribute("bodyId");
					string attribute4 = xmlTextReader.GetAttribute("bodyPlace");
					string attribute5 = xmlTextReader.GetAttribute("behavior");
					string attribute6 = xmlTextReader.GetAttribute("behaviorNum");
					string attribute7 = xmlTextReader.GetAttribute("rnd");
					string attribute8 = xmlTextReader.GetAttribute("content");
					string attribute9 = xmlTextReader.GetAttribute("sound");
					HintPanelInfo hintPanelInfo = new HintPanelInfo();
					hintPanelInfo.id = int.Parse(attribute);
					if (!string.IsNullOrEmpty(attribute2))
					{
						hintPanelInfo.type = int.Parse(attribute2);
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						hintPanelInfo.bodyPlace = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						hintPanelInfo.behavior = int.Parse(attribute5);
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						hintPanelInfo.behaviorNum = int.Parse(attribute6);
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						hintPanelInfo.rnd = attribute7;
					}
					hintPanelInfo.hintinfo = attribute8;
					hintPanelInfo.Sound = attribute9;
					UnitConst.GetInstance().hintPanel.Add(hintPanelInfo.id, hintPanelInfo);
				}
			}
		}
	}

	public void NewReadBuildingConstXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Building"), XmlNodeType.Document, null))
		{
			List<NewBuildingInfo> list = new List<NewBuildingInfo>();
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("type");
					string attribute2 = xmlTextReader.GetAttribute("secondType");
					string attribute3 = xmlTextReader.GetAttribute("storeType");
					string attribute4 = xmlTextReader.GetAttribute("id");
					string attribute5 = xmlTextReader.GetAttribute("name");
					string attribute6 = xmlTextReader.GetAttribute("bodyID");
					string attribute7 = xmlTextReader.GetAttribute("icon");
					string attribute8 = xmlTextReader.GetAttribute("bigicon");
					string attribute9 = xmlTextReader.GetAttribute("description");
					string attribute10 = xmlTextReader.GetAttribute("size");
					string attribute11 = xmlTextReader.GetAttribute("bodySize");
					string attribute12 = xmlTextReader.GetAttribute("uiSize");
					string attribute13 = xmlTextReader.GetAttribute("uiAngle");
					string attribute14 = xmlTextReader.GetAttribute("hight");
					string attribute15 = xmlTextReader.GetAttribute("hightForUI");
					string attribute16 = xmlTextReader.GetAttribute("particle");
					string attribute17 = xmlTextReader.GetAttribute("information");
					string attribute18 = xmlTextReader.GetAttribute("modelRotation");
					string attribute19 = xmlTextReader.GetAttribute("modelPositon");
					string attribute20 = xmlTextReader.GetAttribute("lighticon");
					string attribute21 = xmlTextReader.GetAttribute("outputType");
					string attribute22 = xmlTextReader.GetAttribute("maxRadius");
					string attribute23 = xmlTextReader.GetAttribute("minRadius");
					string attribute24 = xmlTextReader.GetAttribute("show");
					string attribute25 = xmlTextReader.GetAttribute("isTrack");
					string attribute26 = xmlTextReader.GetAttribute("attackPoint");
					string attribute27 = xmlTextReader.GetAttribute("GetTarType");
					string attribute28 = xmlTextReader.GetAttribute("bulletType");
					string attribute29 = xmlTextReader.GetAttribute("fightEffect");
					string attribute30 = xmlTextReader.GetAttribute("flak");
					string attribute31 = xmlTextReader.GetAttribute("BodyEffect");
					string attribute32 = xmlTextReader.GetAttribute("DamageEffect");
					string attribute33 = xmlTextReader.GetAttribute("fightSound");
					string attribute34 = xmlTextReader.GetAttribute("DamageSound");
					string attribute35 = xmlTextReader.GetAttribute("frequency");
					string attribute36 = xmlTextReader.GetAttribute("angle");
					string attribute37 = xmlTextReader.GetAttribute("renju");
					string attribute38 = xmlTextReader.GetAttribute("renjuCD");
					string attribute39 = xmlTextReader.GetAttribute("isEndLianji");
					string attribute40 = xmlTextReader.GetAttribute("isShootSearchTarget");
					string attribute41 = xmlTextReader.GetAttribute("bulletSpeed");
					string attribute42 = xmlTextReader.GetAttribute("hurtRadius");
					string attribute43 = xmlTextReader.GetAttribute("BuffIdx");
					string attribute44 = xmlTextReader.GetAttribute("BulletInAngle");
					string attribute45 = xmlTextReader.GetAttribute("headRotationSpeed");
					string attribute46 = xmlTextReader.GetAttribute("battleFieldId");
					string attribute47 = xmlTextReader.GetAttribute("MaxNum");
					string attribute48 = xmlTextReader.GetAttribute("hitSound");
					string attribute49 = xmlTextReader.GetAttribute("xuanzhong_home");
					string attribute50 = xmlTextReader.GetAttribute("xuanzhong_attack");
					string attribute51 = xmlTextReader.GetAttribute("guid_display");
					string attribute52 = xmlTextReader.GetAttribute("IsMultipleAttack");
					string attribute53 = xmlTextReader.GetAttribute("xuetiao_home");
					string attribute54 = xmlTextReader.GetAttribute("isByPhysical");
					string attribute55 = xmlTextReader.GetAttribute("description1");
					string attribute56 = xmlTextReader.GetAttribute("updateTittle");
					string attribute57 = xmlTextReader.GetAttribute("description1");
					string attribute58 = xmlTextReader.GetAttribute("armyrestrain");
					string attribute59 = xmlTextReader.GetAttribute("modelclear");
					NewBuildingInfo newBuildingInfo = new NewBuildingInfo();
					string attribute60 = xmlTextReader.GetAttribute("newbieGroup");
					if (!string.IsNullOrEmpty(attribute60))
					{
						newBuildingInfo.NewbieGroup = attribute60;
					}
					else
					{
						newBuildingInfo.NewbieGroup = string.Empty;
					}
					list.Add(newBuildingInfo);
					newBuildingInfo.resType = int.Parse(attribute);
					newBuildingInfo.secType = int.Parse(attribute2);
					if (!string.IsNullOrEmpty(attribute20))
					{
						newBuildingInfo.electricityShow = int.Parse(attribute20);
					}
					if (!string.IsNullOrEmpty(attribute3))
					{
						newBuildingInfo.storeType = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute57))
					{
						newBuildingInfo.desForNewBie = attribute57;
					}
					if (!string.IsNullOrEmpty(attribute58))
					{
						string[] array = attribute58.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (attribute58.Contains(":"))
							{
								newBuildingInfo.NewbiArant.Add(int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					newBuildingInfo.resIdx = int.Parse(attribute4);
					newBuildingInfo.name = attribute5;
					if (!string.IsNullOrEmpty(attribute12))
					{
						newBuildingInfo.TowerSize = float.Parse(attribute12);
					}
					newBuildingInfo.bodyID = attribute6;
					newBuildingInfo.iconId = attribute7;
					newBuildingInfo.IsMultipleAttack = (!string.IsNullOrEmpty(attribute52) && attribute52.Trim().Equals("1"));
					newBuildingInfo.bigIcon = attribute8;
					newBuildingInfo.description = attribute9;
					if (!string.IsNullOrEmpty(attribute46))
					{
						newBuildingInfo.battleIdFieldId = int.Parse(attribute46);
					}
					newBuildingInfo.infTips = attribute55;
					if (!string.IsNullOrEmpty(attribute30))
					{
						newBuildingInfo.flak = int.Parse(attribute30);
					}
					newBuildingInfo.size = int.Parse(attribute10);
					newBuildingInfo.bodySize = float.Parse(attribute11);
					newBuildingInfo.hight = float.Parse(attribute14);
					if (!string.IsNullOrEmpty(attribute15))
					{
						newBuildingInfo.hightForShanDian = float.Parse(attribute15);
					}
					if (!string.IsNullOrEmpty(attribute56))
					{
						string[] array2 = attribute56.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array3 = attribute56.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						newBuildingInfo.updateTittlePos = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
						newBuildingInfo.updateTittleRotion = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
					}
					if (!string.IsNullOrEmpty(attribute49))
					{
						newBuildingInfo.selectBlue_Home = new Vector3[2];
						string[] array4 = attribute49.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array5 = attribute49.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						newBuildingInfo.selectBlue_Home[0] = new Vector3(float.Parse(array4[0]), float.Parse(array4[1]), float.Parse(array4[2]));
						newBuildingInfo.selectBlue_Home[1] = new Vector3(float.Parse(array5[0]), float.Parse(array5[1]), float.Parse(array5[2]));
					}
					else
					{
						newBuildingInfo.selectBlue_Home = new Vector3[2];
						newBuildingInfo.selectBlue_Home[0] = Vector3.zero;
						newBuildingInfo.selectBlue_Home[1] = Vector3.zero;
					}
					if (!string.IsNullOrEmpty(attribute59))
					{
						string[] array6 = attribute59.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array7 = attribute59.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						newBuildingInfo.modelclearPos = new Vector3(float.Parse(array6[0]), float.Parse(array6[1]), float.Parse(array6[2]));
						newBuildingInfo.modelclearScale = new Vector3(float.Parse(array7[0]), float.Parse(array7[1]), float.Parse(array7[2]));
					}
					if (!string.IsNullOrEmpty(attribute50))
					{
						newBuildingInfo.selectRed_Attack = new Vector3[2];
						string[] array8 = attribute50.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array9 = attribute50.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						if (array9.Length < 3)
						{
							LogManage.LogError(attribute4);
						}
						newBuildingInfo.selectRed_Attack[0] = new Vector3(float.Parse(array8[0]), float.Parse(array8[1]), float.Parse(array8[2]));
						newBuildingInfo.selectRed_Attack[1] = new Vector3(float.Parse(array9[0]), float.Parse(array9[1]), float.Parse(array9[2]));
					}
					else
					{
						newBuildingInfo.selectRed_Attack = new Vector3[2];
						newBuildingInfo.selectRed_Attack[0] = Vector3.zero;
						newBuildingInfo.selectRed_Attack[1] = Vector3.zero;
					}
					if (!string.IsNullOrEmpty(attribute51))
					{
						newBuildingInfo.guid_display = new Vector3[3];
						string[] array10 = attribute51.Split(new char[]
						{
							';'
						})[0].Split(new char[]
						{
							','
						});
						string[] array11 = attribute51.Split(new char[]
						{
							';'
						})[1].Split(new char[]
						{
							','
						});
						string[] array12 = attribute51.Split(new char[]
						{
							';'
						})[2].Split(new char[]
						{
							','
						});
						newBuildingInfo.guid_display[0] = new Vector3(float.Parse(array10[0]), float.Parse(array10[1]), float.Parse(array10[2]));
						newBuildingInfo.guid_display[1] = new Vector3(float.Parse(array11[0]), float.Parse(array11[1]), float.Parse(array11[2]));
						newBuildingInfo.guid_display[2] = new Vector3(float.Parse(array12[0]), float.Parse(array12[1]), float.Parse(array12[2]));
					}
					else
					{
						newBuildingInfo.guid_display = new Vector3[3];
						newBuildingInfo.guid_display[0] = Vector3.one;
						newBuildingInfo.guid_display[1] = Vector3.one;
						newBuildingInfo.guid_display[2] = Vector3.one;
					}
					newBuildingInfo.outputType = (ResType)int.Parse(attribute21);
					if (newBuildingInfo.resType == 2 || newBuildingInfo.resType == 3)
					{
						newBuildingInfo.buildUIType = BuildUIType.defense;
					}
					if ((newBuildingInfo.resType == 1 && newBuildingInfo.secType == 3) || (newBuildingInfo.resType == 1 && newBuildingInfo.secType == 2))
					{
						newBuildingInfo.buildUIType = BuildUIType.economy;
					}
					if ((newBuildingInfo.resType == 1 && newBuildingInfo.secType == 4) || (newBuildingInfo.resType == 1 && newBuildingInfo.secType == 5) || (newBuildingInfo.resType == 1 && newBuildingInfo.secType == 6))
					{
						newBuildingInfo.buildUIType = BuildUIType.Support;
					}
					string[] array13 = attribute21.Split(new char[]
					{
						','
					});
					if (!string.IsNullOrEmpty(attribute18))
					{
						string[] array14 = attribute18.Split(new char[]
						{
							','
						});
						newBuildingInfo.modelRostion = new float[array14.Length];
						for (int j = 0; j < array14.Length; j++)
						{
							newBuildingInfo.modelRostion[j] = float.Parse(array14[j]);
						}
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						string[] array15 = attribute19.Split(new char[]
						{
							','
						});
						newBuildingInfo.modlePosition = new float[attribute19.Length];
						for (int k = 0; k < array15.Length; k++)
						{
							newBuildingInfo.modlePosition[k] = float.Parse(array15[k]);
						}
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						string[] array16 = attribute17.Split(new char[]
						{
							','
						});
						newBuildingInfo.particleInfo = new float[array16.Length];
						for (int l = 0; l < array16.Length; l++)
						{
							newBuildingInfo.particleInfo[l] = float.Parse(array16[l]);
						}
					}
					if (!string.IsNullOrEmpty(attribute16))
					{
						string[] array17 = attribute16.Split(new char[]
						{
							','
						});
						newBuildingInfo.particleSizeArr = new float[array17.Length];
						for (int m = 0; m < array17.Length; m++)
						{
							newBuildingInfo.particleSizeArr[m] = float.Parse(array17[m]);
						}
					}
					if (!string.IsNullOrEmpty(attribute58))
					{
					}
					string[] array18 = attribute13.Split(new char[]
					{
						','
					});
					newBuildingInfo.TowerQuaternion = new int[array18.Length];
					for (int n = 0; n < array18.Length; n++)
					{
						if (array18.Length > 2)
						{
							newBuildingInfo.TowerQuaternion[n] = int.Parse(array18[n]);
						}
					}
					newBuildingInfo.dropType = new int[array13.Length];
					for (int num = 0; num < array13.Length; num++)
					{
						newBuildingInfo.dropType[num] = int.Parse(array13[num]);
					}
					if (!string.IsNullOrEmpty(attribute24))
					{
						string[] array19 = attribute24.Split(new char[]
						{
							','
						});
						for (int num2 = 0; num2 < array19.Length; num2++)
						{
							string s = array19[num2];
							newBuildingInfo.uiShows.Add(int.Parse(s));
						}
					}
					newBuildingInfo.isByPhysic = (string.IsNullOrEmpty(attribute54) || attribute54 == "1");
					newBuildingInfo.headRotationSpeed = ((!string.IsNullOrEmpty(attribute45.Trim())) ? int.Parse(attribute45) : 0);
					newBuildingInfo.hitSound = attribute48;
					newBuildingInfo.MaxNum = ((!string.IsNullOrEmpty(attribute47)) ? int.Parse(attribute47) : 0);
					newBuildingInfo.isTrack = (!string.IsNullOrEmpty(attribute25) && attribute25 == "1");
					newBuildingInfo.attackPoint = (Enum_AttackPointType)((!string.IsNullOrEmpty(attribute26)) ? int.Parse(attribute26) : 0);
					newBuildingInfo.GetTarType = (Enum_GetTargetType)((!string.IsNullOrEmpty(attribute27)) ? int.Parse(attribute27) : 0);
					newBuildingInfo.bulletType = ((!string.IsNullOrEmpty(attribute28)) ? int.Parse(attribute28) : 0);
					newBuildingInfo.fightEffect = attribute29;
					newBuildingInfo.BodyEffect = attribute31;
					newBuildingInfo.DamageEffect = attribute32;
					newBuildingInfo.fightSound = attribute33;
					newBuildingInfo.DamageSound = attribute34;
					newBuildingInfo.frequency = ((!string.IsNullOrEmpty(attribute35)) ? float.Parse(attribute35) : 0f);
					newBuildingInfo.angle = ((!string.IsNullOrEmpty(attribute36)) ? int.Parse(attribute36) : 0);
					newBuildingInfo.renju = ((!string.IsNullOrEmpty(attribute37)) ? int.Parse(attribute37) : 0);
					newBuildingInfo.renjuCD = ((!string.IsNullOrEmpty(attribute38)) ? float.Parse(attribute38) : 0f);
					newBuildingInfo.isEndLianji = (!string.IsNullOrEmpty(attribute39) && attribute39 == "1");
					newBuildingInfo.isShootSearchTarget = (!string.IsNullOrEmpty(attribute40) && attribute40 == "1");
					newBuildingInfo.bulletSpeed = ((!string.IsNullOrEmpty(attribute41)) ? int.Parse(attribute41) : 0);
					newBuildingInfo.hurtRadius = ((!string.IsNullOrEmpty(attribute42)) ? float.Parse(attribute42) : 0f);
					if (!string.IsNullOrEmpty(attribute43))
					{
						string[] array20 = attribute43.Split(new char[]
						{
							','
						});
						for (int num3 = 0; num3 < array20.Length; num3++)
						{
							string s2 = array20[num3];
							newBuildingInfo.BuffIdx.Add(int.Parse(s2));
						}
					}
					newBuildingInfo.BulletInAngle = ((!string.IsNullOrEmpty(attribute44)) ? int.Parse(attribute44) : 0);
					newBuildingInfo.maxRadius = float.Parse(attribute22);
					newBuildingInfo.minRadius = float.Parse(attribute23);
					newBuildingInfo.towerGrids = new TowerGrid[int.Parse(attribute10) * int.Parse(attribute10)];
					int num4 = 0;
					int num5 = 0;
					for (int num6 = 0; num6 < newBuildingInfo.towerGrids.Length; num6++)
					{
						if (num5 == int.Parse(attribute10))
						{
							num4++;
							num5 = 0;
						}
						newBuildingInfo.towerGrids[num6] = new TowerGrid();
						newBuildingInfo.towerGrids[num6].numX = num4 - Mathf.CeilToInt((float)(int.Parse(attribute10) / 2));
						newBuildingInfo.towerGrids[num6].numZ = num5 - Mathf.CeilToInt((float)(int.Parse(attribute10) / 2));
						num5++;
					}
					newBuildingInfo.xuetiao_home = attribute53;
				}
			}
			UnitConst.GetInstance().buildingConst = list.ToArray();
		}
	}

	public void NewReadBuildingLvXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BuildingUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("itemId");
					string attribute2 = xmlTextReader.GetAttribute("level");
					string attribute3 = xmlTextReader.GetAttribute("life");
					string attribute4 = xmlTextReader.GetAttribute("cost");
					string attribute5 = xmlTextReader.GetAttribute("itemCost");
					string attribute6 = xmlTextReader.GetAttribute("outputNum");
					string attribute7 = xmlTextReader.GetAttribute("outputLimit");
					string attribute8 = xmlTextReader.GetAttribute("costTime");
					string attribute9 = xmlTextReader.GetAttribute("needCommandLevel");
					string attribute10 = xmlTextReader.GetAttribute("playerExp");
					string attribute11 = xmlTextReader.GetAttribute("reduceCommondLifePer");
					string attribute12 = xmlTextReader.GetAttribute("bodyID");
					string attribute13 = xmlTextReader.GetAttribute("breakArmor");
					string attribute14 = xmlTextReader.GetAttribute("defBreak");
					string attribute15 = xmlTextReader.GetAttribute("hitArmor");
					string attribute16 = xmlTextReader.GetAttribute("defHit");
					string attribute17 = xmlTextReader.GetAttribute("critPer");
					string attribute18 = xmlTextReader.GetAttribute("resistPer");
					string attribute19 = xmlTextReader.GetAttribute("critHR");
					string attribute20 = xmlTextReader.GetAttribute("trueDamage");
					string attribute21 = xmlTextReader.GetAttribute("reDamage");
					string attribute22 = xmlTextReader.GetAttribute("reChancePer");
					string attribute23 = xmlTextReader.GetAttribute("avoidDef");
					string attribute24 = xmlTextReader.GetAttribute("skillIds");
					string attribute25 = xmlTextReader.GetAttribute("skill");
					string attribute26 = xmlTextReader.GetAttribute("energypoints");
					string attribute27 = xmlTextReader.GetAttribute("haveenergypoints");
					string attribute28 = xmlTextReader.GetAttribute("electricUse");
					string attribute29 = xmlTextReader.GetAttribute("armyExp");
					NewTowerLvInfo newTowerLvInfo = new NewTowerLvInfo();
					newTowerLvInfo.lv = int.Parse(attribute2);
					string[] array = attribute4.Split(new char[]
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
								newTowerLvInfo.resCost.Add(ResType.金币, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 2:
								newTowerLvInfo.resCost.Add(ResType.石油, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 3:
								newTowerLvInfo.resCost.Add(ResType.钢铁, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							case 4:
								newTowerLvInfo.resCost.Add(ResType.稀矿, int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
								break;
							}
						}
					}
					string[] array2 = attribute5.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j];
						if (text2.Contains(":"))
						{
							newTowerLvInfo.itemCost.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					newTowerLvInfo.outputNum = int.Parse(attribute6);
					string[] array3 = attribute7.Split(new char[]
					{
						','
					});
					for (int k = 0; k < array3.Length; k++)
					{
						string text3 = array3[k];
						if (text3.Contains(":"))
						{
							ResType resType = (ResType)int.Parse(text3.Split(new char[]
							{
								':'
							})[0]);
							switch (resType)
							{
							case ResType.金币:
								newTowerLvInfo.outputLimit.Add(ResType.金币, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							case ResType.石油:
								newTowerLvInfo.outputLimit.Add(ResType.石油, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							case ResType.钢铁:
								newTowerLvInfo.outputLimit.Add(ResType.钢铁, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							case ResType.稀矿:
								newTowerLvInfo.outputLimit.Add(ResType.稀矿, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							case ResType.技能点:
							case ResType.天赋点:
							case ResType.钻石:
								IL_400:
								if (resType != ResType.电力)
								{
									goto IL_516;
								}
								newTowerLvInfo.outputLimit.Add(ResType.电力, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							case ResType.兵种:
								newTowerLvInfo.outputLimit.Add(ResType.兵种, int.Parse(text3.Split(new char[]
								{
									':'
								})[1]));
								goto IL_516;
							}
							goto IL_400;
						}
						IL_516:;
					}
					newTowerLvInfo.armyExp = ((!string.IsNullOrEmpty(attribute29.Trim())) ? int.Parse(attribute29) : 0);
					if (!string.IsNullOrEmpty(attribute28))
					{
						newTowerLvInfo.electricUse = int.Parse(attribute28);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						newTowerLvInfo.BuildTime = int.Parse(attribute8);
					}
					newTowerLvInfo.needCommandLevel = int.Parse(attribute9);
					newTowerLvInfo.playerExp = int.Parse(attribute10);
					newTowerLvInfo.reduceCommondLifePer = int.Parse(attribute11);
					newTowerLvInfo.skillIds = attribute24;
					newTowerLvInfo.skillId = attribute25;
					newTowerLvInfo.energypoints = int.Parse(attribute26);
					newTowerLvInfo.haveenergypoints = ((!string.IsNullOrEmpty(attribute27)) ? int.Parse(attribute27) : 0);
					newTowerLvInfo.bodyID = attribute12;
					newTowerLvInfo.fightInfo = default(BaseFightInfo);
					newTowerLvInfo.fightInfo.life = int.Parse(attribute3);
					newTowerLvInfo.fightInfo.breakArmor = int.Parse(attribute13);
					newTowerLvInfo.fightInfo.defBreak = int.Parse(attribute14);
					newTowerLvInfo.fightInfo.hitArmor = int.Parse(attribute15);
					newTowerLvInfo.fightInfo.defHit = int.Parse(attribute16);
					newTowerLvInfo.fightInfo.crit = int.Parse(attribute17);
					newTowerLvInfo.fightInfo.resist = int.Parse(attribute18);
					newTowerLvInfo.fightInfo.critHR = int.Parse(attribute19);
					newTowerLvInfo.fightInfo.trueDamage = int.Parse(attribute20);
					newTowerLvInfo.fightInfo.reDamage = int.Parse(attribute21);
					newTowerLvInfo.fightInfo.reChancePer = int.Parse(attribute22);
					newTowerLvInfo.fightInfo.avoiddef = int.Parse(attribute23);
					UnitConst.GetInstance().buildingConst[int.Parse(attribute)].lvInfos.Add(newTowerLvInfo);
				}
			}
		}
	}

	public void ReadElectricityXML()
	{
		UnitConst.GetInstance().ElectricityCont.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Electricity"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("ID");
					string attribute2 = xmlTextReader.GetAttribute("description");
					string attribute3 = xmlTextReader.GetAttribute("percent");
					string attribute4 = xmlTextReader.GetAttribute("extraDamage");
					string attribute5 = xmlTextReader.GetAttribute("buildings");
					string attribute6 = xmlTextReader.GetAttribute("buffId");
					string attribute7 = xmlTextReader.GetAttribute("resReduce");
					string attribute8 = xmlTextReader.GetAttribute("actualReduce");
					Electricity electricity = new Electricity();
					electricity.id = int.Parse(attribute);
					electricity.actualReduce = float.Parse(attribute8);
					electricity.description = attribute2;
					electricity.percent = int.Parse(attribute3);
					if (!string.IsNullOrEmpty(attribute7))
					{
						electricity.resdamaged = attribute7;
					}
					electricity.extraDamage = int.Parse(attribute4);
					if (!string.IsNullOrEmpty(attribute6))
					{
						electricity.buffid = int.Parse(attribute6);
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
							if (!string.IsNullOrEmpty(text))
							{
								electricity.budingids.Add(int.Parse(text));
							}
						}
					}
					UnitConst.GetInstance().ElectricityCont.Add(electricity.id, electricity);
				}
			}
		}
	}

	public void ReadMilitaryOpenSetXML()
	{
		UnitConst.GetInstance().HomeUpdateOpenSetDataConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("MilitaryOpenSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("buildingIds");
					string attribute3 = xmlTextReader.GetAttribute("armyIds");
					string attribute4 = xmlTextReader.GetAttribute("officerIds");
					string attribute5 = xmlTextReader.GetAttribute("sendAide");
					string attribute6 = xmlTextReader.GetAttribute("skillNum");
					string attribute7 = xmlTextReader.GetAttribute("skillBox");
					string attribute8 = xmlTextReader.GetAttribute("openBuildingIds");
					HomeUpdateOpenSetData homeUpdateOpenSetData = new HomeUpdateOpenSetData();
					homeUpdateOpenSetData.homeLevel = int.Parse(attribute);
					if (!string.IsNullOrEmpty(attribute8))
					{
						homeUpdateOpenSetData.rallypoint = Mathf.Abs(int.Parse(attribute8.Split(new char[]
						{
							','
						})[0].Split(new char[]
						{
							':'
						})[1]));
					}
					string[] array = attribute2.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						homeUpdateOpenSetData.buildingIds.Add(int.Parse(text.Split(new char[]
						{
							':'
						})[0]), int.Parse(text.Split(new char[]
						{
							':'
						})[1]));
					}
					string[] array2 = attribute3.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j];
						if (!string.IsNullOrEmpty(text2))
						{
							homeUpdateOpenSetData.armsIds.Add(int.Parse(text2));
						}
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						homeUpdateOpenSetData.officerNum = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						homeUpdateOpenSetData.sendAide = int.Parse(attribute5);
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						homeUpdateOpenSetData.skillNum = int.Parse(attribute6);
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						homeUpdateOpenSetData.skillBox = int.Parse(attribute7);
					}
					UnitConst.GetInstance().HomeUpdateOpenSetDataConst.Add(homeUpdateOpenSetData.homeLevel, homeUpdateOpenSetData);
				}
			}
		}
		UnitConst.GetInstance().OtherBuildingOpenSetDataConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ArmyFactory"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					int num = int.Parse(xmlTextReader2.GetAttribute("itemId"));
					int num2 = int.Parse(xmlTextReader2.GetAttribute("level"));
					string attribute9 = xmlTextReader2.GetAttribute("openArmys");
					int armyMaxLevel = int.Parse(xmlTextReader2.GetAttribute("armyLevelMax"));
					BuildingUpOpenSet buildingUpOpenSet = new BuildingUpOpenSet();
					buildingUpOpenSet.armyMaxLevel = armyMaxLevel;
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array3 = attribute9.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string s = array3[k];
							buildingUpOpenSet.armsIds.Add(int.Parse(s));
						}
					}
					UnitConst.GetInstance().OtherBuildingOpenSetDataConst.Add(new Vector2((float)num, (float)num2), buildingUpOpenSet);
				}
			}
		}
	}

	public void ReadTechnologyAndTechnologyUpsetXML()
	{
		UnitConst.GetInstance().TechnologyDataConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("TechnologyTree"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("itemId");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("type");
					string attribute5 = xmlTextReader.GetAttribute("level");
					string attribute6 = xmlTextReader.GetAttribute("iconId");
					string attribute7 = xmlTextReader.GetAttribute("buildingLevel");
					string attribute8 = xmlTextReader.GetAttribute("costTime");
					string attribute9 = xmlTextReader.GetAttribute("props");
					string attribute10 = xmlTextReader.GetAttribute("per");
					string attribute11 = xmlTextReader.GetAttribute("resCost");
					string attribute12 = xmlTextReader.GetAttribute("itemCost");
					string attribute13 = xmlTextReader.GetAttribute("needTech");
					string attribute14 = xmlTextReader.GetAttribute("nextTech");
					Technology technology = new Technology();
					technology.name = attribute2;
					technology.des = attribute3;
					technology.iconId = attribute6;
					if (!string.IsNullOrEmpty(attribute))
					{
						technology.itemid = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						technology.type = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						technology.level = int.Parse(attribute5);
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						technology.buildingLevel = int.Parse(attribute7);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						technology.costTime = int.Parse(attribute8);
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array = attribute9.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							technology.Props.Add((Technology.Enum_TechnologyProps)int.Parse(array[i].Split(new char[]
							{
								':'
							})[0]), int.Parse(array[i].Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						string[] array2 = attribute10.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							technology.Props_Presence.Add((Technology.Enum_TechnologyProps)int.Parse(array2[j].Split(new char[]
							{
								':'
							})[0]), array2[j].Split(new char[]
							{
								':'
							})[1].Equals("1"));
						}
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						string[] array3 = attribute11.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							technology.resCost.Add((ResType)int.Parse(array3[k].Split(new char[]
							{
								':'
							})[0]), int.Parse(array3[k].Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						string[] array4 = attribute12.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							technology.itemCost.Add(int.Parse(array4[l].Split(new char[]
							{
								':'
							})[0]), int.Parse(array4[l].Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						string[] array5 = attribute13.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array5.Length; m++)
						{
							technology.needTech.Add(int.Parse(array5[m].Split(new char[]
							{
								':'
							})[0]), int.Parse(array5[m].Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute14))
					{
						string[] array6 = attribute14.Split(new char[]
						{
							','
						});
						for (int n = 0; n < array6.Length; n++)
						{
							technology.nextTech.Add(int.Parse(array6[n]));
						}
					}
					UnitConst.GetInstance().TechnologyDataConst.Add(new Vector2((float)technology.itemid, (float)technology.level), technology);
				}
			}
		}
	}

	public void ReadItemAndItemMixSetXML()
	{
		UnitConst.GetInstance().ItemConst.Clear();
		UnitConst.GetInstance().ItemMixSetConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Item"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("quality");
					string attribute5 = xmlTextReader.GetAttribute("type");
					string attribute6 = xmlTextReader.GetAttribute("sell");
					string attribute7 = xmlTextReader.GetAttribute("convertMoney");
					string attribute8 = xmlTextReader.GetAttribute("giveItems");
					string attribute9 = xmlTextReader.GetAttribute("giveRes");
					string attribute10 = xmlTextReader.GetAttribute("costRes");
					string attribute11 = xmlTextReader.GetAttribute("iconId");
					Item item = new Item();
					item.Id = int.Parse(attribute);
					item.Name = attribute2;
					item.Description = attribute3;
					item.Quality = (Quility_ResAndItemAndSkill)int.Parse(attribute4);
					item.Type = (TypeItem)int.Parse(attribute5);
					item.Price = int.Parse(attribute6);
					item.ConvertMoney = int.Parse(attribute7);
					item.IconId = attribute11;
					string[] array = attribute8.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						if (text.Contains(":"))
						{
							item.GiveItems.Add(int.Parse(text.Split(new char[]
							{
								':'
							})[0]), int.Parse(text.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					string[] array2 = attribute9.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j];
						if (text2.Contains(":"))
						{
							item.GiveRes.Add((ResType)int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					string[] array3 = attribute10.Split(new char[]
					{
						','
					});
					for (int k = 0; k < array3.Length; k++)
					{
						string text3 = array3[k];
						if (text3.Contains(":"))
						{
							item.CostRes.Add((ResType)int.Parse(text3.Split(new char[]
							{
								':'
							})[0]), int.Parse(text3.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					UnitConst.GetInstance().ItemConst.Add(item.Id, item);
				}
			}
		}
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ItemMixSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute12 = xmlTextReader2.GetAttribute("name");
					string attribute13 = xmlTextReader2.GetAttribute("description");
					string attribute14 = xmlTextReader2.GetAttribute("id");
					string attribute15 = xmlTextReader2.GetAttribute("needItems");
					string attribute16 = xmlTextReader2.GetAttribute("gold");
					ItemMixSet itemMixSet = new ItemMixSet();
					itemMixSet.Name = attribute12;
					itemMixSet.Description = attribute13;
					if (!string.IsNullOrEmpty(attribute14))
					{
						itemMixSet.MixedId = int.Parse(attribute14);
					}
					if (!string.IsNullOrEmpty(attribute16))
					{
						itemMixSet.Gold = int.Parse(attribute16);
					}
					string[] array4 = attribute15.Split(new char[]
					{
						','
					});
					for (int l = 0; l < array4.Length; l++)
					{
						string text4 = array4[l];
						if (text4.Contains(":"))
						{
							itemMixSet.NeedItems.Add(UnitConst.GetInstance().ItemConst[int.Parse(text4.Split(new char[]
							{
								':'
							})[0])], int.Parse(text4.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (itemMixSet.MixedId != 0)
					{
						UnitConst.GetInstance().ItemMixSetConst.Add(itemMixSet.MixedId, itemMixSet);
					}
				}
			}
		}
	}

	public void ReadPlayerUpSetXML()
	{
		UnitConst.GetInstance().PlayerExpConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("PlayerUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("level");
					string attribute2 = xmlTextReader.GetAttribute("exp");
					UnitConst.GetInstance().PlayerExpConst.Add(int.Parse(attribute), int.Parse(attribute2));
				}
			}
		}
	}

	public void ReadBattleConstXML()
	{
		UnitConst.GetInstance().AllWarZone.Clear();
		List<StarCondition> list = new List<StarCondition>();
		List<BattleField> list2 = new List<BattleField>();
		UnitConst.GetInstance().BattleFieldConst.Clear();
		List<Battle> list3 = new List<Battle>();
		UnitConst.GetInstance().BattleConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("StarCondition"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("conditionId");
					string attribute3 = xmlTextReader.GetAttribute("contitions");
					StarCondition starCondition = new StarCondition();
					if (!string.IsNullOrEmpty(attribute))
					{
						starCondition.id = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						starCondition.conditionId = int.Parse(attribute2);
					}
					if (starCondition.conditionId != 0 && !string.IsNullOrEmpty(attribute3) && attribute3.Contains(":"))
					{
						string[] array = attribute3.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							starCondition.contitions.Add(int.Parse(text.Split(new char[]
							{
								':'
							})[0]), int.Parse(text.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					list.Add(starCondition);
				}
			}
		}
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BattleField"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute4 = xmlTextReader2.GetAttribute("id");
					string attribute5 = xmlTextReader2.GetAttribute("commandLevel");
					string attribute6 = xmlTextReader2.GetAttribute("name");
					string attribute7 = xmlTextReader2.GetAttribute("description");
					string attribute8 = xmlTextReader2.GetAttribute("battleId");
					string attribute9 = xmlTextReader2.GetAttribute("cost");
					string attribute10 = xmlTextReader2.GetAttribute("preId");
					string attribute11 = xmlTextReader2.GetAttribute("nextId");
					string attribute12 = xmlTextReader2.GetAttribute("needBattleStar");
					string attribute13 = xmlTextReader2.GetAttribute("star1Condition");
					string attribute14 = xmlTextReader2.GetAttribute("star2Condition");
					string attribute15 = xmlTextReader2.GetAttribute("star3Condition");
					string attribute16 = xmlTextReader2.GetAttribute("npcId");
					string attribute17 = xmlTextReader2.GetAttribute("battleType");
					string attribute18 = xmlTextReader2.GetAttribute("buildingId");
					string attribute19 = xmlTextReader2.GetAttribute("coord");
					string attribute20 = xmlTextReader2.GetAttribute("target1Description");
					string attribute21 = xmlTextReader2.GetAttribute("target2Description");
					string attribute22 = xmlTextReader2.GetAttribute("target3Description");
					BattleField battleField = new BattleField();
					battleField.name = attribute6;
					battleField.description = attribute7;
					battleField.id = int.Parse(attribute4);
					if (!string.IsNullOrEmpty(attribute5))
					{
						battleField.commondLevel = int.Parse(attribute5);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						battleField.battleId = int.Parse(attribute8);
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						battleField.cost = int.Parse(attribute9);
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						battleField.preId = int.Parse(attribute10);
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						battleField.nextId = int.Parse(attribute11);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						battleField.needBattleStar = int.Parse(attribute12);
					}
					if (!string.IsNullOrEmpty(attribute16))
					{
						battleField.npcId = int.Parse(attribute16);
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						battleField.battleType = int.Parse(attribute17);
					}
					battleField.target1Description = attribute20;
					battleField.target2Description = attribute21;
					battleField.target3Description = attribute22;
					if (!string.IsNullOrEmpty(attribute13))
					{
						string[] array2 = attribute13.Split(new char[]
						{
							','
						});
						string item;
						for (int j = 0; j < array2.Length; j++)
						{
							item = array2[j];
							battleField.star1Condition.Add(list.SingleOrDefault((StarCondition a) => a.id == int.Parse(item)));
						}
					}
					if (!string.IsNullOrEmpty(attribute14))
					{
						string[] array3 = attribute14.Split(new char[]
						{
							','
						});
						string item;
						for (int k = 0; k < array3.Length; k++)
						{
							item = array3[k];
							battleField.star2Condition.Add(list.SingleOrDefault((StarCondition a) => a.id == int.Parse(item)));
						}
					}
					if (!string.IsNullOrEmpty(attribute15))
					{
						string[] array4 = attribute15.Split(new char[]
						{
							','
						});
						string item;
						for (int l = 0; l < array4.Length; l++)
						{
							item = array4[l];
							battleField.star3Condition.Add(list.SingleOrDefault((StarCondition a) => a.id == int.Parse(item)));
						}
					}
					if (!string.IsNullOrEmpty(attribute18))
					{
						for (int m = 0; m < attribute18.Split(new char[]
						{
							','
						}).Length; m++)
						{
							battleField.buildingId.Add(int.Parse(attribute18.Split(new char[]
							{
								','
							})[m]));
						}
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						for (int n = 0; n < attribute19.Split(new char[]
						{
							','
						}).Length; n++)
						{
							battleField.coord.Add(float.Parse(attribute19.Split(new char[]
							{
								','
							})[n]));
						}
					}
					list2.Add(battleField);
					if (battleField.id != 0)
					{
						UnitConst.GetInstance().BattleFieldConst.Add(battleField.id, battleField);
					}
				}
			}
		}
		using (XmlTextReader xmlTextReader3 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Battle"), XmlNodeType.Document, null))
		{
			while (xmlTextReader3.Read())
			{
				if (xmlTextReader3.NodeType == XmlNodeType.Element && xmlTextReader3.LocalName.Equals("configure"))
				{
					string attribute23 = xmlTextReader3.GetAttribute("id");
					string attribute24 = xmlTextReader3.GetAttribute("name");
					string attribute25 = xmlTextReader3.GetAttribute("type");
					string attribute26 = xmlTextReader3.GetAttribute("description");
					string attribute27 = xmlTextReader3.GetAttribute("zoneId");
					string attribute28 = xmlTextReader3.GetAttribute("needZoneStar");
					string attribute29 = xmlTextReader3.GetAttribute("preId");
					string attribute30 = xmlTextReader3.GetAttribute("nextId");
					string attribute31 = xmlTextReader3.GetAttribute("firstBattleFieldId");
					string attribute32 = xmlTextReader3.GetAttribute("starNum1");
					string attribute33 = xmlTextReader3.GetAttribute("itemPrize1");
					string attribute34 = xmlTextReader3.GetAttribute("starNum2");
					string attribute35 = xmlTextReader3.GetAttribute("itemPrize2");
					string attribute36 = xmlTextReader3.GetAttribute("starNum3");
					string attribute37 = xmlTextReader3.GetAttribute("itemPrize3");
					string attribute38 = xmlTextReader3.GetAttribute("mapId");
					string attribute39 = xmlTextReader3.GetAttribute("stagePrize");
					string attribute40 = xmlTextReader3.GetAttribute("coord");
					string attribute41 = xmlTextReader3.GetAttribute("radarLevel");
					Battle battle = new Battle();
					battle.name = attribute24;
					battle.type = int.Parse(attribute25);
					battle.description = attribute26;
					if (!string.IsNullOrEmpty(attribute23))
					{
						battle.id = int.Parse(attribute23);
					}
					if (!string.IsNullOrEmpty(attribute27))
					{
						battle.zoneId = int.Parse(attribute27);
					}
					if (!string.IsNullOrEmpty(attribute28))
					{
						battle.needZoneStar = int.Parse(attribute28);
					}
					if (!string.IsNullOrEmpty(attribute29))
					{
						battle.preId = int.Parse(attribute29);
					}
					if (!string.IsNullOrEmpty(attribute30))
					{
						battle.nextId = int.Parse(attribute30);
					}
					else
					{
						battle.nextId = 0;
					}
					if (!string.IsNullOrEmpty(attribute31))
					{
						battle.firstBattleFieldId = int.Parse(attribute31);
					}
					if (!string.IsNullOrEmpty(attribute32))
					{
						battle.starNum1 = int.Parse(attribute32);
					}
					if (!string.IsNullOrEmpty(attribute33))
					{
						string[] array5 = attribute33.Split(new char[]
						{
							','
						});
						for (int num = 0; num < array5.Length; num++)
						{
							string text2 = array5[num];
							battle.itemPrize1.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute34))
					{
						battle.starNum2 = int.Parse(attribute34);
					}
					if (!string.IsNullOrEmpty(attribute35))
					{
						string[] array6 = attribute35.Split(new char[]
						{
							','
						});
						for (int num2 = 0; num2 < array6.Length; num2++)
						{
							string text3 = array6[num2];
							battle.itemPrize2.Add(int.Parse(text3.Split(new char[]
							{
								':'
							})[0]), int.Parse(text3.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute36))
					{
						battle.starNum3 = int.Parse(attribute36);
					}
					if (!string.IsNullOrEmpty(attribute37))
					{
						string[] array7 = attribute37.Split(new char[]
						{
							','
						});
						for (int num3 = 0; num3 < array7.Length; num3++)
						{
							string text4 = array7[num3];
							battle.itemPrize3.Add(int.Parse(text4.Split(new char[]
							{
								':'
							})[0]), int.Parse(text4.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute38))
					{
						battle.mapId = int.Parse(attribute38);
					}
					if (!string.IsNullOrEmpty(attribute41))
					{
						battle.radarLevel = int.Parse(attribute41);
					}
					if (!string.IsNullOrEmpty(attribute39))
					{
						string[] array8 = attribute39.Split(new char[]
						{
							','
						});
						for (int num4 = 0; num4 < array8.Length; num4++)
						{
							string text5 = array8[num4];
							battle.stagePrize.Add(int.Parse(text5.Split(new char[]
							{
								':'
							})[0]), int.Parse(text5.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute40))
					{
						for (int num5 = 0; num5 < attribute40.Split(new char[]
						{
							','
						}).Length; num5++)
						{
							battle.coord.Add(float.Parse(attribute40.Split(new char[]
							{
								','
							})[num5]));
						}
					}
					else
					{
						for (int num6 = 0; num6 < 3; num6++)
						{
							battle.coord.Add(0f);
						}
					}
					for (int num7 = 0; num7 < list2.Count; num7++)
					{
						if (list2[num7].battleId == battle.id)
						{
							battle.allBattleField.Add(list2[num7].id, list2[num7]);
						}
					}
					list3.Add(battle);
					if (battle.id != 0)
					{
						UnitConst.GetInstance().BattleConst.Add(battle.id, battle);
					}
				}
			}
		}
		using (XmlTextReader xmlTextReader4 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("WarZone"), XmlNodeType.Document, null))
		{
			while (xmlTextReader4.Read())
			{
				if (xmlTextReader4.NodeType == XmlNodeType.Element && xmlTextReader4.LocalName.Equals("configure"))
				{
					string attribute42 = xmlTextReader4.GetAttribute("id");
					string attribute43 = xmlTextReader4.GetAttribute("name");
					string attribute44 = xmlTextReader4.GetAttribute("description");
					string attribute45 = xmlTextReader4.GetAttribute("lastBattleId");
					string attribute46 = xmlTextReader4.GetAttribute("firstBattleId");
					string attribute47 = xmlTextReader4.GetAttribute("commondLevel");
					string attribute48 = xmlTextReader4.GetAttribute("preId");
					string attribute49 = xmlTextReader4.GetAttribute("nextId");
					string attribute50 = xmlTextReader4.GetAttribute("zoneMapId");
					WarZone warZone = new WarZone();
					warZone.name = attribute43;
					warZone.description = attribute44;
					if (!string.IsNullOrEmpty(attribute42))
					{
						warZone.id = int.Parse(attribute42);
					}
					if (!string.IsNullOrEmpty(attribute46))
					{
						warZone.firstBattleId = int.Parse(attribute46);
					}
					if (!string.IsNullOrEmpty(attribute45))
					{
						warZone.lastBattleId = int.Parse(attribute45);
					}
					if (!string.IsNullOrEmpty(attribute47))
					{
						warZone.commondLevel = int.Parse(attribute47);
					}
					if (!string.IsNullOrEmpty(attribute48))
					{
						warZone.preId = int.Parse(attribute48);
					}
					if (!string.IsNullOrEmpty(attribute49))
					{
						warZone.nextId = int.Parse(attribute49);
					}
					if (!string.IsNullOrEmpty(attribute50))
					{
						warZone.zoneMapId = int.Parse(attribute50);
					}
					foreach (Battle current in from a in list3
					where a.zoneId == warZone.id
					select a)
					{
						warZone.allBattle.Add(current.id, current);
					}
					if (warZone.id == 2)
					{
						int key = warZone.firstBattleId;
						int num8 = 1;
						while (UnitConst.GetInstance().BattleConst.ContainsKey(key) && UnitConst.GetInstance().BattleConst[key].nextId != 0)
						{
							UnitConst.GetInstance().BattleConst[key].number = num8;
							num8++;
							key = UnitConst.GetInstance().BattleConst[key].nextId;
						}
					}
					UnitConst.GetInstance().AllWarZone.Add(warZone.id, warZone);
				}
			}
		}
	}

	public void ReadNpc_Box_DropListXML()
	{
		UnitConst.GetInstance().AllBox.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Box"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("items");
					string attribute5 = xmlTextReader.GetAttribute("rate");
					string attribute6 = xmlTextReader.GetAttribute("equips");
					string attribute7 = xmlTextReader.GetAttribute("equipsRate");
					Box box = new Box();
					if (!string.IsNullOrEmpty(attribute))
					{
						box.id = int.Parse(attribute);
					}
					box.name = attribute2;
					box.description = attribute3;
					if (!string.IsNullOrEmpty(attribute4))
					{
						string[] array = attribute4.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							box.items.Add(int.Parse(text.Split(new char[]
							{
								':'
							})[0]), int.Parse(text.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array2 = attribute5.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							box.rate.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array3 = attribute6.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							box.equips.Add(int.Parse(text3.Split(new char[]
							{
								':'
							})[0]), int.Parse(text3.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute7))
					{
						string[] array4 = attribute7.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							string text4 = array4[l];
							box.equipRate.Add(int.Parse(text4.Split(new char[]
							{
								':'
							})[0]), int.Parse(text4.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (UnitConst.GetInstance().AllBox.ContainsKey(box.id))
					{
						LogManage.LogError("已包含 BoxID" + box.id);
					}
					UnitConst.GetInstance().AllBox.Add(box.id, box);
				}
			}
		}
		UnitConst.GetInstance().AllDropList.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DropList"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute8 = xmlTextReader2.GetAttribute("id");
					string attribute9 = xmlTextReader2.GetAttribute("name");
					string attribute10 = xmlTextReader2.GetAttribute("description");
					string attribute11 = xmlTextReader2.GetAttribute("boxRate");
					string attribute12 = xmlTextReader2.GetAttribute("res");
					DropList dropList = new DropList();
					if (!string.IsNullOrEmpty(attribute8))
					{
						dropList.id = int.Parse(attribute8);
					}
					dropList.name = attribute9;
					dropList.description = attribute10;
					if (!string.IsNullOrEmpty(attribute11))
					{
						string[] array5 = attribute11.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array5.Length; m++)
						{
							string text5 = array5[m];
							dropList.boxRate.Add(int.Parse(text5.Split(new char[]
							{
								':'
							})[0]), int.Parse(text5.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						string[] array6 = attribute12.Split(new char[]
						{
							','
						});
						for (int n = 0; n < array6.Length; n++)
						{
							string text6 = array6[n];
							dropList.res.Add((ResType)int.Parse(text6.Split(new char[]
							{
								':'
							})[0]), int.Parse(text6.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (UnitConst.GetInstance().AllDropList.ContainsKey(dropList.id))
					{
						LogManage.Log("已包含 dropListID" + dropList.id);
					}
					UnitConst.GetInstance().AllDropList.Add(dropList.id, dropList);
				}
			}
		}
		UnitConst.GetInstance().AllNpc.Clear();
		using (XmlTextReader xmlTextReader3 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("NPC"), XmlNodeType.Document, null))
		{
			while (xmlTextReader3.Read())
			{
				if (xmlTextReader3.NodeType == XmlNodeType.Element && xmlTextReader3.LocalName.Equals("configure"))
				{
					string attribute13 = xmlTextReader3.GetAttribute("id");
					string attribute14 = xmlTextReader3.GetAttribute("name");
					string attribute15 = xmlTextReader3.GetAttribute("description");
					string attribute16 = xmlTextReader3.GetAttribute("type");
					string attribute17 = xmlTextReader3.GetAttribute("islandId");
					string attribute18 = xmlTextReader3.GetAttribute("level");
					string attribute19 = xmlTextReader3.GetAttribute("dropListId");
					string attribute20 = xmlTextReader3.GetAttribute("cost");
					string attribute21 = xmlTextReader3.GetAttribute("nextId");
					string attribute22 = xmlTextReader3.GetAttribute("star");
					Npc npc = new Npc();
					npc.name = attribute14;
					npc.description = attribute15;
					if (!string.IsNullOrEmpty(attribute22))
					{
						npc.Star = int.Parse(attribute22);
					}
					else
					{
						npc.Star = 0;
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						npc.id = int.Parse(attribute13);
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						npc.type = (NpcType)int.Parse(attribute16);
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						npc.islandId = int.Parse(attribute17);
					}
					if (!string.IsNullOrEmpty(attribute18))
					{
						npc.level = int.Parse(attribute18);
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						npc.dropListId = int.Parse(attribute19);
					}
					if (!string.IsNullOrEmpty(attribute20))
					{
						npc.cost = int.Parse(attribute20);
					}
					if (!string.IsNullOrEmpty(attribute21))
					{
						npc.nextId = int.Parse(attribute21);
					}
					if (UnitConst.GetInstance().AllNpc.ContainsKey(npc.id))
					{
						LogManage.LogError("已包含 npcID" + npc.id);
					}
					UnitConst.GetInstance().AllNpc.Add(npc.id, npc);
				}
			}
		}
	}

	public void ReadTaskAndAchievementXML()
	{
		UnitConst.GetInstance().DailyTask.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Task"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("description");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("conditionId");
					string attribute5 = xmlTextReader.GetAttribute("secondConditionId");
					string attribute6 = xmlTextReader.GetAttribute("step");
					string attribute7 = xmlTextReader.GetAttribute("mainLine");
					string attribute8 = xmlTextReader.GetAttribute("items");
					string attribute9 = xmlTextReader.GetAttribute("equips");
					string attribute10 = xmlTextReader.GetAttribute("res");
					string attribute11 = xmlTextReader.GetAttribute("type");
					string attribute12 = xmlTextReader.GetAttribute("uitaskId");
					string attribute13 = xmlTextReader.GetAttribute("money");
					string attribute14 = xmlTextReader.GetAttribute("skills");
					string attribute15 = xmlTextReader.GetAttribute("playerExp");
					string attribute16 = xmlTextReader.GetAttribute("skip");
					string attribute17 = xmlTextReader.GetAttribute("skipid");
					string attribute18 = xmlTextReader.GetAttribute("newbieGroup");
					DailyTask dailyTask = new DailyTask();
					if (!string.IsNullOrEmpty(attribute18))
					{
						dailyTask.NewBieGroup = attribute18;
					}
					else
					{
						dailyTask.NewBieGroup = string.Empty;
					}
					if (!string.IsNullOrEmpty(attribute))
					{
						dailyTask.id = int.Parse(attribute);
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						dailyTask.step = int.Parse(attribute6);
					}
					if (!string.IsNullOrEmpty(attribute13))
					{
						dailyTask.rewardNum = int.Parse(attribute13);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						dailyTask.PanelType = int.Parse(attribute12);
					}
					if (!string.IsNullOrEmpty(attribute16))
					{
						dailyTask.skipType = (DailyTask.taskSkilType)int.Parse(attribute16);
					}
					if (!string.IsNullOrEmpty(attribute17))
					{
						dailyTask.skipValue = int.Parse(attribute17);
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						dailyTask.conditionId = int.Parse(attribute4);
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						dailyTask.secondConditionId = int.Parse(attribute5);
						dailyTask.PanelId = int.Parse(attribute5);
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						dailyTask.type = int.Parse(attribute11);
					}
					dailyTask.isTips = true;
					if (!string.IsNullOrEmpty(attribute7))
					{
						dailyTask.mainTaskType = int.Parse(attribute7);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						string[] array = attribute8.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							dailyTask.rewardItems.Add(int.Parse(text.Split(new char[]
							{
								':'
							})[0]), int.Parse(text.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute14))
					{
						string[] array2 = attribute14.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							dailyTask.skillAward.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array3 = attribute9.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							dailyTask.rewardEquips.Add(int.Parse(text3.Split(new char[]
							{
								':'
							})[0]), int.Parse(text3.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						string[] array4 = attribute10.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							string text4 = array4[l];
							dailyTask.rewardRes.Add((ResType)int.Parse(text4.Split(new char[]
							{
								':'
							})[0]), int.Parse(text4.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute15))
					{
						dailyTask.rewardRes.Add(ResType.经验, int.Parse(attribute15));
					}
					dailyTask.description = attribute3;
					dailyTask.name = attribute2;
					if (dailyTask.id != 0)
					{
						UnitConst.GetInstance().DailyTask.Add(dailyTask.id, dailyTask);
					}
				}
			}
		}
		UnitConst.GetInstance().AllAchievementConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Achievement"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute19 = xmlTextReader2.GetAttribute("id");
					string attribute20 = xmlTextReader2.GetAttribute("name");
					string attribute21 = xmlTextReader2.GetAttribute("description");
					string attribute22 = xmlTextReader2.GetAttribute("values");
					string attribute23 = xmlTextReader2.GetAttribute("prizes");
					string attribute24 = xmlTextReader2.GetAttribute("conditionId");
					string attribute25 = xmlTextReader2.GetAttribute("secondConditionId");
					string attribute26 = xmlTextReader2.GetAttribute("icon");
					Achievement achievement = new Achievement();
					achievement.iconName = attribute26;
					if (!string.IsNullOrEmpty(attribute24))
					{
						achievement.conditionId = int.Parse(attribute24);
					}
					if (!string.IsNullOrEmpty(attribute25))
					{
						achievement.secondConditionId = int.Parse(attribute25);
					}
					if (!string.IsNullOrEmpty(attribute19))
					{
						achievement.id = int.Parse(attribute19);
					}
					achievement.name = attribute20;
					achievement.description = attribute21;
					if (!string.IsNullOrEmpty(attribute22))
					{
						string[] array5 = attribute22.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array5.Length; m++)
						{
							string s = array5[m];
							achievement.step.Add(int.Parse(s));
						}
					}
					if (!string.IsNullOrEmpty(attribute23))
					{
						string[] array6 = attribute23.Split(new char[]
						{
							','
						});
						for (int n = 0; n < array6.Length; n++)
						{
							string s2 = array6[n];
							achievement.prizes.Add(int.Parse(s2));
						}
					}
					if (achievement.id != 0)
					{
						UnitConst.GetInstance().AllAchievementConst.Add(achievement.id, achievement);
					}
				}
			}
		}
	}

	public void ReadResConvertXML()
	{
		ResourceMgr.CoinPool.Clear();
		ResourceMgr.OilPool.Clear();
		ResourceMgr.SteelPool.Clear();
		ResourceMgr.RareEarthPool.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ResConvert"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int num = int.Parse(xmlTextReader.GetAttribute("resType"));
					int key = int.Parse(xmlTextReader.GetAttribute("resource"));
					int value = int.Parse(xmlTextReader.GetAttribute("money"));
					switch (num)
					{
					case 1:
						ResourceMgr.CoinPool.Add(key, value);
						break;
					case 2:
						ResourceMgr.OilPool.Add(key, value);
						break;
					case 3:
						ResourceMgr.SteelPool.Add(key, value);
						break;
					case 4:
						ResourceMgr.RareEarthPool.Add(key, value);
						break;
					}
				}
			}
		}
	}

	public void ReadResIslandOutputXML()
	{
		HeroInfo.GetInstance().islandResData.ResIslandOutputConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ResIslandOutput"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int key = int.Parse(xmlTextReader.GetAttribute("commondLevel"));
					ResType resType = (ResType)int.Parse(xmlTextReader.GetAttribute("resId"));
					int speendValue = int.Parse(xmlTextReader.GetAttribute("value"));
					int limit = int.Parse(xmlTextReader.GetAttribute("limit"));
					if (!HeroInfo.GetInstance().islandResData.ResIslandOutputConst.ContainsKey(key))
					{
						HeroInfo.GetInstance().islandResData.ResIslandOutputConst.Add(key, new Dictionary<ResType, IslandResData.ResIslandOut>());
					}
					if (!HeroInfo.GetInstance().islandResData.ResIslandOutputConst[key].ContainsKey(resType))
					{
						HeroInfo.GetInstance().islandResData.ResIslandOutputConst[key].Add(resType, new IslandResData.ResIslandOut());
					}
					HeroInfo.GetInstance().islandResData.ResIslandOutputConst[key][resType].resType = resType;
					HeroInfo.GetInstance().islandResData.ResIslandOutputConst[key][resType].speendValue = speendValue;
					HeroInfo.GetInstance().islandResData.ResIslandOutputConst[key][resType].limit = limit;
				}
			}
		}
	}

	public void ReadArmsDealerXML()
	{
		UnitConst.GetInstance().ArmsDealerConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Mall"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader.GetAttribute("id"));
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("description");
					int type = int.Parse(xmlTextReader.GetAttribute("type"));
					string attribute3 = xmlTextReader.GetAttribute("price");
					int num = int.Parse(xmlTextReader.GetAttribute("priceType"));
					int show = int.Parse(xmlTextReader.GetAttribute("show"));
					string attribute4 = xmlTextReader.GetAttribute("sort");
					string attribute5 = xmlTextReader.GetAttribute("items");
					string attribute6 = xmlTextReader.GetAttribute("skills");
					int sales = int.Parse(xmlTextReader.GetAttribute("sales"));
					ArmsDealer armsDealer = new ArmsDealer();
					armsDealer.id = id;
					armsDealer.name = attribute;
					armsDealer.description = attribute2;
					armsDealer.type = type;
					armsDealer.priceType = num;
					armsDealer.show = show;
					if (!string.IsNullOrEmpty(attribute4))
					{
						armsDealer.sort = int.Parse(attribute4);
					}
					else
					{
						armsDealer.sort = 0;
					}
					armsDealer.sales = sales;
					if (!string.IsNullOrEmpty(attribute3))
					{
						string[] array = attribute3.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (num == 1)
							{
								armsDealer.ResBuyer.Add((ResType)int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
							else if (num == 2)
							{
								armsDealer.ItemBuyer.Add(int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array2 = attribute5.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							armsDealer.ItemSeller.Add(int.Parse(text2.Split(new char[]
							{
								':'
							})[0]), int.Parse(text2.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array3 = attribute6.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							armsDealer.SkillSeller.Add(int.Parse(text3.Split(new char[]
							{
								':'
							})[0]), int.Parse(text3.Split(new char[]
							{
								':'
							})[1]));
						}
					}
					UnitConst.GetInstance().ArmsDealerConst.Add(armsDealer.id, armsDealer);
				}
			}
		}
	}

	public void ReadResDesXML()
	{
		UnitConst.GetInstance().ResDes.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("ResDes"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					UnitConst.resDes value = default(UnitConst.resDes);
					value.resID = int.Parse(attribute);
					value.resName = attribute2;
					value.resDesciption = attribute3;
					UnitConst.GetInstance().ResDes.Add(value.resID, value);
				}
			}
		}
	}

	public void ReadAideXML()
	{
		UnitConst.GetInstance().AideConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Aide"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("type");
					string attribute5 = xmlTextReader.GetAttribute("level");
					string attribute6 = xmlTextReader.GetAttribute("needItemId");
					string attribute7 = xmlTextReader.GetAttribute("needItemNumber");
					string attribute8 = xmlTextReader.GetAttribute("itemOfDecomposed");
					string attribute9 = xmlTextReader.GetAttribute("durationTime");
					string attribute10 = xmlTextReader.GetAttribute("mixNeedTime");
					string attribute11 = xmlTextReader.GetAttribute("icon");
					string attribute12 = xmlTextReader.GetAttribute("UiTime");
					Aide aide = new Aide();
					aide.id = int.Parse(attribute);
					aide.name = attribute2;
					aide.description = attribute3;
					aide.type = int.Parse(attribute4);
					aide.level = int.Parse(attribute5);
					aide.needItemId = int.Parse(attribute6);
					aide.needItemNum = int.Parse(attribute7);
					aide.itemOfDecomposed = int.Parse(attribute8);
					aide.durationTime = int.Parse(attribute9);
					aide.mixNeedTime = int.Parse(attribute10);
					aide.bigIcon = attribute11;
					aide.Uitime = attribute12;
					UnitConst.GetInstance().AideConst.Add(aide.id, aide);
				}
			}
		}
		UnitConst.GetInstance().AideAbilityConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("AideAbility"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute13 = xmlTextReader2.GetAttribute("id");
					string attribute14 = xmlTextReader2.GetAttribute("name");
					string attribute15 = xmlTextReader2.GetAttribute("description");
					string attribute16 = xmlTextReader2.GetAttribute("aideType");
					string attribute17 = xmlTextReader2.GetAttribute("aideLevel");
					string attribute18 = xmlTextReader2.GetAttribute("abilityType");
					string attribute19 = xmlTextReader2.GetAttribute("value");
					AideAbility aideAbility = new AideAbility();
					aideAbility.id = int.Parse(attribute13);
					aideAbility.name = attribute14;
					aideAbility.description = attribute15;
					aideAbility.aideType = int.Parse(attribute16);
					aideAbility.aideLevel = int.Parse(attribute17);
					aideAbility.abilitity = int.Parse(attribute18);
					aideAbility.value = int.Parse(attribute19);
					UnitConst.GetInstance().AideAbilityConst.Add(aideAbility.id, aideAbility);
				}
			}
		}
	}

	public void ReadShopXML()
	{
		UnitConst.GetInstance().shopItem.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Shop"), XmlNodeType.Document, null))
		{
			try
			{
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
					{
						string attribute = xmlTextReader.GetAttribute("id");
						string attribute2 = xmlTextReader.GetAttribute("appleProductId");
						string attribute3 = xmlTextReader.GetAttribute("name");
						string attribute4 = xmlTextReader.GetAttribute("type");
						string attribute5 = xmlTextReader.GetAttribute("price");
						string attribute6 = xmlTextReader.GetAttribute("diamonds");
						string attribute7 = xmlTextReader.GetAttribute("extDiamonds");
						string attribute8 = xmlTextReader.GetAttribute("res");
						string attribute9 = xmlTextReader.GetAttribute("desciption");
						string attribute10 = xmlTextReader.GetAttribute("icon");
						string attribute11 = xmlTextReader.GetAttribute("titleIcon");
						string attribute12 = xmlTextReader.GetAttribute("priceType");
						string attribute13 = xmlTextReader.GetAttribute("isShow");
						ShopItem shopItem = new ShopItem();
						shopItem.id = int.Parse(attribute);
						shopItem.appleProductId = attribute2;
						shopItem.name = attribute3;
						if (!string.IsNullOrEmpty(attribute4))
						{
							shopItem.type = int.Parse(attribute4);
						}
						if (!string.IsNullOrEmpty(attribute5))
						{
							shopItem.price = int.Parse(attribute5);
						}
						if (!string.IsNullOrEmpty(attribute6))
						{
							shopItem.diamonds = attribute6;
						}
						if (!string.IsNullOrEmpty(attribute12))
						{
							shopItem.priceType = int.Parse(attribute12);
						}
						if (!string.IsNullOrEmpty(attribute13))
						{
							shopItem.IsUIShow = attribute13.Equals("1");
						}
						if (!string.IsNullOrEmpty(attribute7))
						{
							shopItem.extDiamonds = attribute7;
						}
						else
						{
							shopItem.extDiamonds = "0";
						}
						shopItem.desciption = attribute9;
						shopItem.icon = attribute10;
						shopItem.titleIcon = attribute11;
						if (!UnitConst.GetInstance().shopItem.ContainsKey(shopItem.id))
						{
							UnitConst.GetInstance().shopItem.Add(shopItem.id, shopItem);
						}
						else
						{
							Debug.LogError("Shop包含相同的Key" + shopItem.id);
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

	public void ReadBtnUpsetXML()
	{
		UnitConst.GetInstance().btnUpSet.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BtnUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("btnId");
					string attribute2 = xmlTextReader.GetAttribute("type");
					string attribute3 = xmlTextReader.GetAttribute("effect");
					string text = xmlTextReader.GetAttribute("newbieGroup");
					if (string.IsNullOrEmpty(text))
					{
						text = string.Empty;
					}
					UnitConst.GetInstance().btnUpSet.Add(int.Parse(attribute), new string[]
					{
						attribute2,
						attribute3,
						text
					});
				}
			}
		}
	}

	public void ReadMoneyToToken()
	{
		UnitConst.GetInstance().moneyToToken.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("MoneyToToken"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("desciption");
					string attribute4 = xmlTextReader.GetAttribute("type");
					string attribute5 = xmlTextReader.GetAttribute("times");
					string attribute6 = xmlTextReader.GetAttribute("money");
					MoneyToToken moneyToToken = new MoneyToToken();
					moneyToToken.id = int.Parse(attribute);
					moneyToToken.name = attribute2;
					moneyToToken.description = attribute3;
					moneyToToken.type = int.Parse(attribute4);
					moneyToToken.times = int.Parse(attribute5);
					moneyToToken.money = int.Parse(attribute6);
					UnitConst.GetInstance().moneyToToken.Add(moneyToToken);
				}
			}
		}
	}

	public void ReadNewGuildXml()
	{
		UnitConst.GetInstance().newbieGuide.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("NewGuild"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("commandLevel");
					string attribute3 = xmlTextReader.GetAttribute("playerLevel");
					string attribute4 = xmlTextReader.GetAttribute("battleFieldId");
					string attribute5 = xmlTextReader.GetAttribute("talkTime");
					string attribute6 = xmlTextReader.GetAttribute("type");
					string attribute7 = xmlTextReader.GetAttribute("teamId");
					string attribute8 = xmlTextReader.GetAttribute("preGuild");
					string attribute9 = xmlTextReader.GetAttribute("guildType");
					string attribute10 = xmlTextReader.GetAttribute("targetType");
					string attribute11 = xmlTextReader.GetAttribute("buttonId");
					string attribute12 = xmlTextReader.GetAttribute("arrowType");
					string attribute13 = xmlTextReader.GetAttribute("targetPos");
					string attribute14 = xmlTextReader.GetAttribute("arrowAngle");
					string attribute15 = xmlTextReader.GetAttribute("tapZonePos");
					string attribute16 = xmlTextReader.GetAttribute("tapZoneArea");
					string attribute17 = xmlTextReader.GetAttribute("cameraTargetPos");
					string attribute18 = xmlTextReader.GetAttribute("words");
					string attribute19 = xmlTextReader.GetAttribute("talkId");
					string attribute20 = xmlTextReader.GetAttribute("xml");
					string attribute21 = xmlTextReader.GetAttribute("skipNpc");
					string attribute22 = xmlTextReader.GetAttribute("npcId");
					string attribute23 = xmlTextReader.GetAttribute("desciption");
					string attribute24 = xmlTextReader.GetAttribute("toNextTeam");
					string attribute25 = xmlTextReader.GetAttribute("battleField");
					string attribute26 = xmlTextReader.GetAttribute("waitService");
					NewbieGuide newbieGuide = new NewbieGuide();
					newbieGuide.id = int.Parse(attribute);
					newbieGuide.commandLevel = int.Parse(attribute2);
					newbieGuide.playerLevel = int.Parse(attribute3);
					newbieGuide.battleFieldId = attribute4;
					newbieGuide.talkTime = attribute5;
					newbieGuide.type = attribute6;
					newbieGuide.teamId = int.Parse(attribute7);
					newbieGuide.preGuild = attribute8;
					if (!string.IsNullOrEmpty(attribute9))
					{
						newbieGuide.guildType = int.Parse(attribute9);
					}
					newbieGuide.targetType = attribute10;
					newbieGuide.buttonId = attribute11;
					newbieGuide.arrowType = int.Parse(attribute12);
					newbieGuide.targetPos = attribute13;
					newbieGuide.arrowAngle = attribute14;
					newbieGuide.tapZonePos = attribute15;
					newbieGuide.tapZoneArea = attribute16;
					newbieGuide.cameraTargetPos = attribute17;
					newbieGuide.words = attribute18;
					if (!string.IsNullOrEmpty(attribute19))
					{
						newbieGuide.talkId = int.Parse(attribute19);
					}
					newbieGuide.xml = attribute20;
					newbieGuide.skipNpc = attribute21;
					newbieGuide.npcId = attribute22;
					newbieGuide.desciption = attribute23;
					newbieGuide.toNextTeam = int.Parse(attribute24);
					newbieGuide.waitService = int.Parse(attribute26);
					if (!string.IsNullOrEmpty(attribute25))
					{
						newbieGuide.battleField = int.Parse(attribute25);
					}
					UnitConst.GetInstance().newbieGuide.Add(newbieGuide.id, newbieGuide);
				}
			}
		}
	}

	public void ReadNewGuildPersonXml()
	{
		UnitConst.GetInstance().newbieGuidePerson.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("HalfTalk"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("bodyId");
					string attribute3 = xmlTextReader.GetAttribute("bodyPlace");
					string attribute4 = xmlTextReader.GetAttribute("content");
					string attribute5 = xmlTextReader.GetAttribute("buttonContent");
					string attribute6 = xmlTextReader.GetAttribute("sound");
					string attribute7 = xmlTextReader.GetAttribute("name");
					NewbieGuidePerson newbieGuidePerson = new NewbieGuidePerson();
					newbieGuidePerson.id = int.Parse(attribute);
					newbieGuidePerson.bodyId = attribute2;
					newbieGuidePerson.bodyPlance = int.Parse(attribute3);
					newbieGuidePerson.content = attribute4;
					newbieGuidePerson.buttonContent = attribute5;
					if (!string.IsNullOrEmpty(attribute7))
					{
						newbieGuidePerson.name = attribute7;
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						newbieGuidePerson.sound = attribute6.Split(new char[]
						{
							','
						});
					}
					UnitConst.GetInstance().newbieGuidePerson.Add(newbieGuidePerson.id, newbieGuidePerson);
				}
			}
		}
	}

	public void ReadRandomEventXml()
	{
		RandomEventManage.RandomEventConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Events"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("eventId");
					string attribute3 = xmlTextReader.GetAttribute("num1");
					string attribute4 = xmlTextReader.GetAttribute("num2");
					string attribute5 = xmlTextReader.GetAttribute("desciption");
					RandomEvent randomEvent = new RandomEvent();
					randomEvent.id = int.Parse(attribute);
					randomEvent.eventID = int.Parse(attribute2);
					randomEvent.num1 = attribute3;
					randomEvent.num2 = ((!string.IsNullOrEmpty(attribute4.Trim())) ? int.Parse(attribute4) : 0);
					randomEvent.desciption = attribute5;
					RandomEventManage.RandomEventConst.Add(randomEvent.id, randomEvent);
				}
			}
		}
		RandomEventManage.RandomBoxConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("RndBox"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute6 = xmlTextReader2.GetAttribute("id");
					string attribute7 = xmlTextReader2.GetAttribute("bodyId");
					string attribute8 = xmlTextReader2.GetAttribute("radius");
					string attribute9 = xmlTextReader2.GetAttribute("desciption");
					RandomBox randomBox = new RandomBox();
					randomBox.id = int.Parse(attribute6);
					randomBox.bodyId = attribute7;
					randomBox.radius = ((!string.IsNullOrEmpty(attribute8.Trim())) ? float.Parse(attribute8) : 0f);
					randomBox.description = attribute9;
					RandomEventManage.RandomBoxConst.Add(randomBox.id, randomBox);
				}
			}
		}
	}

	public void ReadNpcAttarkXml()
	{
		UnitConst.GetInstance().npcAttartList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("NPCAttackWave"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("level");
					string attribute3 = xmlTextReader.GetAttribute("army");
					NpcAttark npcAttark = new NpcAttark();
					npcAttark.id = int.Parse(attribute);
					npcAttark.level = int.Parse(attribute2);
					string[] array = attribute3.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = array[i].Split(new char[]
						{
							':'
						});
						npcAttark.armyNum.Add(int.Parse(array2[0]), int.Parse(array2[1]));
					}
					UnitConst.GetInstance().npcAttartList.Add(npcAttark.id, npcAttark);
				}
			}
		}
	}

	public void ReadEquipXml()
	{
		UnitConst.GetInstance().equipList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("equip"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("quality");
					string attribute4 = xmlTextReader.GetAttribute("equipType");
					string attribute5 = xmlTextReader.GetAttribute("Props");
					string attribute6 = xmlTextReader.GetAttribute("armyProps");
					string attribute7 = xmlTextReader.GetAttribute("towerProps");
					string attribute8 = xmlTextReader.GetAttribute("officerType");
					string attribute9 = xmlTextReader.GetAttribute("levelLimit");
					string attribute10 = xmlTextReader.GetAttribute("icon");
					Equip equip = new Equip();
					equip.id = int.Parse(attribute);
					equip.name = attribute2;
					equip.equipQuality = (Quility_ResAndItemAndSkill)int.Parse(attribute3);
					equip.type = int.Parse(attribute4);
					equip.props = attribute5;
					equip.aemyProps = attribute6;
					equip.towerProps = attribute7;
					equip.commanderType = int.Parse(attribute8);
					equip.level = int.Parse(attribute9);
					equip.icon = attribute10;
					UnitConst.GetInstance().equipList.Add(equip.id, equip);
				}
			}
		}
	}

	public void ReadSkillXml()
	{
		UnitConst.GetInstance().skillList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Skill"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("skillType");
					string attribute4 = xmlTextReader.GetAttribute("sceneType");
					string attribute5 = xmlTextReader.GetAttribute("targetType");
					string attribute6 = xmlTextReader.GetAttribute("level");
					string attribute7 = xmlTextReader.GetAttribute("castTime");
					string attribute8 = xmlTextReader.GetAttribute("coldDown");
					string attribute9 = xmlTextReader.GetAttribute("basecost");
					string attribute10 = xmlTextReader.GetAttribute("xishu");
					string attribute11 = xmlTextReader.GetAttribute("basePower");
					string attribute12 = xmlTextReader.GetAttribute("renjuType");
					string attribute13 = xmlTextReader.GetAttribute("renju");
					string attribute14 = xmlTextReader.GetAttribute("renjuCd");
					string attribute15 = xmlTextReader.GetAttribute("attarkRadius");
					string attribute16 = xmlTextReader.GetAttribute("hurtRadius");
					string attribute17 = xmlTextReader.GetAttribute("hitCount");
					string attribute18 = xmlTextReader.GetAttribute("buffId");
					string attribute19 = xmlTextReader.GetAttribute("icon");
					string attribute20 = xmlTextReader.GetAttribute("squareIcon");
					string attribute21 = xmlTextReader.GetAttribute("skillQuality");
					string attribute22 = xmlTextReader.GetAttribute("effect");
					string attribute23 = xmlTextReader.GetAttribute("fightEffect");
					string attribute24 = xmlTextReader.GetAttribute("bodyEffect");
					string attribute25 = xmlTextReader.GetAttribute("damageEffect");
					string attribute26 = xmlTextReader.GetAttribute("circleEffect");
					string attribute27 = xmlTextReader.GetAttribute("description");
					string attribute28 = xmlTextReader.GetAttribute("sellPrice");
					string attribute29 = xmlTextReader.GetAttribute("needChips");
					string attribute30 = xmlTextReader.GetAttribute("skillVolume");
					string attribute31 = xmlTextReader.GetAttribute("fighticon");
					Skill skill = new Skill();
					skill.id = int.Parse(attribute);
					skill.name = attribute2;
					skill.skillType = int.Parse(attribute3);
					skill.sceneType = int.Parse(attribute4);
					skill.targetType = int.Parse(attribute5);
					skill.level = int.Parse(attribute6);
					if (!string.IsNullOrEmpty(attribute7))
					{
						skill.castTime = int.Parse(attribute7);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						skill.coldDown = int.Parse(attribute8);
					}
					skill.basecost = int.Parse(attribute9);
					skill.xishu = int.Parse(attribute10);
					skill.basePower = int.Parse(attribute11);
					skill.renjuType = int.Parse(attribute12);
					skill.renju = int.Parse(attribute13);
					skill.renjuCd = attribute14;
					skill.attarkRadius = int.Parse(attribute15);
					skill.hurtRadius = int.Parse(attribute16);
					skill.hitCount = int.Parse(attribute17);
					if (!string.IsNullOrEmpty(attribute18))
					{
						string[] array = attribute18.Split(new char[]
						{
							','
						});
						skill.buffId = new int[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							skill.buffId[i] = int.Parse(array[i]);
						}
					}
					if (!string.IsNullOrEmpty(attribute29))
					{
						skill.needChips = int.Parse(attribute29);
					}
					skill.icon = attribute19;
					skill.skillQuality = (Quility_ResAndItemAndSkill)int.Parse(attribute21);
					skill.effect = attribute22;
					skill.fightEffect = attribute23;
					skill.circleEffect = attribute26;
					if (string.IsNullOrEmpty(attribute30))
					{
						skill.skillVolume = 0;
					}
					else
					{
						skill.skillVolume = int.Parse(attribute30);
					}
					skill.Ficon = attribute20;
					skill.fighticon = attribute31;
					skill.bodyEffect = attribute24;
					skill.damageEffect = attribute25;
					skill.Description = attribute27;
					if (!string.IsNullOrEmpty(attribute28))
					{
						skill.sellPrice = int.Parse(attribute28);
					}
					if (!string.IsNullOrEmpty(attribute29))
					{
						skill.needChips = int.Parse(attribute29);
					}
					UnitConst.GetInstance().skillList.Add(skill.id, skill);
				}
			}
		}
		UnitConst.GetInstance().skillUpdateConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("SkillUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader2.GetAttribute("id"));
					string attribute32 = xmlTextReader2.GetAttribute("name");
					string attribute33 = xmlTextReader2.GetAttribute("description");
					int level = int.Parse(xmlTextReader2.GetAttribute("level"));
					string attribute34 = xmlTextReader2.GetAttribute("needSkillCard");
					int itemId = int.Parse(xmlTextReader2.GetAttribute("itemId"));
					int commandLevel = int.Parse(xmlTextReader2.GetAttribute("needBuildingLevel"));
					int skillVoloum = int.Parse(xmlTextReader2.GetAttribute("skillVolume"));
					string attribute35 = xmlTextReader2.GetAttribute("needRes");
					string attribute36 = xmlTextReader2.GetAttribute("needSkillPoint");
					SkillUpdate skillUpdate = new SkillUpdate();
					skillUpdate.id = id;
					skillUpdate.itemId = itemId;
					if (!string.IsNullOrEmpty(attribute36))
					{
						skillUpdate.needSkillPoint = int.Parse(attribute36);
					}
					skillUpdate.commandLevel = commandLevel;
					skillUpdate.skillVoloum = skillVoloum;
					if (!string.IsNullOrEmpty(attribute32))
					{
						skillUpdate.name = attribute32;
					}
					if (!string.IsNullOrEmpty(attribute33))
					{
						skillUpdate.des = attribute33;
					}
					if (!string.IsNullOrEmpty(level.ToString()))
					{
						skillUpdate.level = level;
					}
					if (!string.IsNullOrEmpty(attribute35) && attribute35.Contains(':'))
					{
						switch (int.Parse(attribute35.Split(new char[]
						{
							':'
						})[0]))
						{
						case 1:
							skillUpdate.resCost.Add(ResType.金币, int.Parse(attribute35.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 2:
							skillUpdate.resCost.Add(ResType.石油, int.Parse(attribute35.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 3:
							skillUpdate.resCost.Add(ResType.钢铁, int.Parse(attribute35.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 4:
							skillUpdate.resCost.Add(ResType.稀矿, int.Parse(attribute35.Split(new char[]
							{
								':'
							})[1]));
							break;
						case 7:
							skillUpdate.resCost.Add(ResType.钻石, int.Parse(attribute35.Split(new char[]
							{
								':'
							})[1]));
							break;
						}
					}
					if (!string.IsNullOrEmpty(attribute34))
					{
						string[] array2 = attribute34.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text = array2[j];
							if (text.Contains(":"))
							{
								skillUpdate.coistItems.Add(int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().skillUpdateConst.Add(skillUpdate.id, skillUpdate);
				}
			}
		}
		UnitConst.GetInstance().SkillMixConstData.Clear();
		using (XmlTextReader xmlTextReader3 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("skillMix"), XmlNodeType.Document, null))
		{
			while (xmlTextReader3.Read())
			{
				if (xmlTextReader3.NodeType == XmlNodeType.Element && xmlTextReader3.LocalName.Equals("configure"))
				{
					int type = int.Parse(xmlTextReader3.GetAttribute("level"));
					string attribute37 = xmlTextReader3.GetAttribute("costItem");
					string attribute38 = xmlTextReader3.GetAttribute("costGold");
					SkillMix skillMix = new SkillMix();
					skillMix.type = type;
					if (!string.IsNullOrEmpty(attribute38))
					{
						skillMix.costGold = int.Parse(attribute38);
					}
					if (!string.IsNullOrEmpty(attribute37))
					{
						string[] array3 = attribute37.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text2 = array3[k];
							if (text2.Contains(":"))
							{
								skillMix.costItem.Add(int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().SkillMixConstData.Add(skillMix.type, skillMix);
				}
			}
		}
	}

	public void ReadSoldierXml()
	{
		UnitConst.GetInstance().soldierList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("soldier"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("isUI");
					string attribute4 = xmlTextReader.GetAttribute("bodyId");
					string attribute5 = xmlTextReader.GetAttribute("maxRadius");
					string attribute6 = xmlTextReader.GetAttribute("minRadius");
					string attribute7 = xmlTextReader.GetAttribute("speed");
					string attribute8 = xmlTextReader.GetAttribute("star");
					string attribute9 = xmlTextReader.GetAttribute("skillType");
					string attribute10 = xmlTextReader.GetAttribute("renju");
					string attribute11 = xmlTextReader.GetAttribute("renjuCd");
					string attribute12 = xmlTextReader.GetAttribute("Cd");
					string attribute13 = xmlTextReader.GetAttribute("unlockType");
					string attribute14 = xmlTextReader.GetAttribute("unLock");
					string attribute15 = xmlTextReader.GetAttribute("fightEffect");
					string attribute16 = xmlTextReader.GetAttribute("BodyEffect");
					string attribute17 = xmlTextReader.GetAttribute("DamageEffect");
					string attribute18 = xmlTextReader.GetAttribute("fightSound");
					string attribute19 = xmlTextReader.GetAttribute("DamageSound");
					string attribute20 = xmlTextReader.GetAttribute("bulletSpeed");
					string attribute21 = xmlTextReader.GetAttribute("hurtRadius");
					string attribute22 = xmlTextReader.GetAttribute("description");
					string attribute23 = xmlTextReader.GetAttribute("skillId");
					string attribute24 = xmlTextReader.GetAttribute("icon");
					string attribute25 = xmlTextReader.GetAttribute("modelSize");
					Soldier soldier = new Soldier();
					soldier.id = int.Parse(attribute);
					soldier.name = attribute2;
					soldier.isUI = (!string.IsNullOrEmpty(attribute3) && attribute3 == "1");
					soldier.bodyId = attribute4;
					soldier.maxRadius = int.Parse(attribute5);
					soldier.minRadius = int.Parse(attribute6);
					soldier.speed = float.Parse(attribute7);
					soldier.star = int.Parse(attribute8);
					soldier.skillType = attribute9;
					soldier.renju = int.Parse(attribute10);
					soldier.shoot_index = int.Parse(attribute10);
					soldier.icon = attribute24;
					if (!string.IsNullOrEmpty(attribute11.ToString()))
					{
						soldier.renjuCd = float.Parse(attribute11);
						soldier.shoot_reload_time = float.Parse(attribute11);
					}
					if (!string.IsNullOrEmpty(attribute25))
					{
						soldier.modelScale = float.Parse(attribute25.ToString());
					}
					if (!string.IsNullOrEmpty(attribute23))
					{
						soldier.skillID = int.Parse(attribute23);
					}
					soldier.Cd = float.Parse(attribute12);
					soldier.shoot_cd_time = float.Parse(attribute12);
					soldier.unlockType = int.Parse(attribute13);
					soldier.unLock = int.Parse(attribute14);
					soldier.fightEffect = attribute15;
					soldier.BodyEffect = attribute16;
					soldier.DamageEffect = attribute17;
					soldier.fightSound = attribute18;
					soldier.DamageSound = attribute19;
					soldier.bulletSpeed = int.Parse(attribute20);
					soldier.hurtRadius = int.Parse(attribute21);
					soldier.description = attribute22;
					UnitConst.GetInstance().soldierList.Add(soldier.id, soldier);
				}
			}
		}
		UnitConst.GetInstance().soldierUpSetConst.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("soldierUpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					int id = int.Parse(xmlTextReader2.GetAttribute("id"));
					int itemId = int.Parse(xmlTextReader2.GetAttribute("itemId"));
					string attribute26 = xmlTextReader2.GetAttribute("name");
					int level = int.Parse(xmlTextReader2.GetAttribute("level"));
					string attribute27 = xmlTextReader2.GetAttribute("starUpItem");
					string attribute28 = xmlTextReader2.GetAttribute("starUpRes");
					string attribute29 = xmlTextReader2.GetAttribute("price");
					int armyId = int.Parse(xmlTextReader2.GetAttribute("armyId"));
					string attribute30 = xmlTextReader2.GetAttribute("armyAffixes");
					string attribute31 = xmlTextReader2.GetAttribute("description");
					SoldierUpSet soldierUpSet = new SoldierUpSet();
					soldierUpSet.id = id;
					soldierUpSet.itemId = itemId;
					soldierUpSet.armyId = armyId;
					if (!string.IsNullOrEmpty(attribute31))
					{
						soldierUpSet.des = attribute31;
					}
					if (!string.IsNullOrEmpty(attribute26))
					{
						soldierUpSet.name = attribute26;
					}
					if (!string.IsNullOrEmpty(level.ToString()))
					{
						soldierUpSet.level = level;
					}
					if (!string.IsNullOrEmpty(attribute27))
					{
						string[] array = attribute27.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (text.Contains(":"))
							{
								soldierUpSet.starUpItem.Add(int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute28))
					{
						string[] array2 = attribute28.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(":"))
							{
								soldierUpSet.starUpRes.Add(int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute29))
					{
						string[] array3 = attribute29.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length; k++)
						{
							string text3 = array3[k];
							if (text3.Contains(":") && int.Parse(text3.Split(new char[]
							{
								':'
							})[0]) == 1)
							{
								soldierUpSet.FuncMoney = int.Parse(text3.Split(new char[]
								{
									':'
								})[1]);
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute30))
					{
						string[] array4 = attribute30.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array4.Length; l++)
						{
							string text4 = array4[l];
							if (text4.Contains(":"))
							{
								soldierUpSet.armyAffixes.Add((SpecialPro)int.Parse(text4.Split(new char[]
								{
									':'
								})[0]), int.Parse(text4.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().soldierUpSetConst.Add(new Vector2((float)soldierUpSet.itemId, (float)soldierUpSet.level), soldierUpSet);
				}
			}
		}
		UnitConst.GetInstance().soldierExpSetConst.Clear();
		using (XmlTextReader xmlTextReader3 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("soldierExpSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader3.Read())
			{
				if (xmlTextReader3.NodeType == XmlNodeType.Element && xmlTextReader3.LocalName.Equals("configure"))
				{
					int id2 = int.Parse(xmlTextReader3.GetAttribute("id"));
					string attribute32 = xmlTextReader3.GetAttribute("exp");
					string attribute33 = xmlTextReader3.GetAttribute("reliveCd");
					SoldierExpSet soldierExpSet = new SoldierExpSet();
					soldierExpSet.id = id2;
					if (!string.IsNullOrEmpty(attribute32))
					{
						string[] array5 = attribute32.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array5.Length; m++)
						{
							string text5 = array5[m];
							if (text5.Contains(":"))
							{
								soldierExpSet.exp.Add(int.Parse(text5.Split(new char[]
								{
									':'
								})[0]), int.Parse(text5.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute33))
					{
						string[] array6 = attribute33.Split(new char[]
						{
							','
						});
						for (int n = 0; n < array6.Length; n++)
						{
							string text6 = array6[n];
							if (text6.Contains(":"))
							{
								soldierExpSet.reliveTime.Add(int.Parse(text6.Split(new char[]
								{
									':'
								})[0]), int.Parse(text6.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().soldierExpSetConst.Add(soldierExpSet.id, soldierExpSet);
				}
			}
		}
		UnitConst.GetInstance().soldierLevelSetConst.Clear();
		using (XmlTextReader xmlTextReader4 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("soldierLevelSet"), XmlNodeType.Document, null))
		{
			while (xmlTextReader4.Read())
			{
				if (xmlTextReader4.NodeType == XmlNodeType.Element && xmlTextReader4.LocalName.Equals("configure"))
				{
					int id3 = int.Parse(xmlTextReader4.GetAttribute("id"));
					string attribute34 = xmlTextReader4.GetAttribute("name");
					int itemId2 = int.Parse(xmlTextReader4.GetAttribute("itemId"));
					int level2 = int.Parse(xmlTextReader4.GetAttribute("level"));
					int life = int.Parse(xmlTextReader4.GetAttribute("life"));
					int breakArmor = int.Parse(xmlTextReader4.GetAttribute("breakArmor"));
					int hitArmor = int.Parse(xmlTextReader4.GetAttribute("hitArmor"));
					int defBreak = int.Parse(xmlTextReader4.GetAttribute("defBreak"));
					SoldierLevelSet soldierLevelSet = new SoldierLevelSet();
					soldierLevelSet.id = id3;
					soldierLevelSet.name = attribute34;
					soldierLevelSet.itemId = itemId2;
					soldierLevelSet.level = level2;
					soldierLevelSet.life = life;
					soldierLevelSet.breakArmor = breakArmor;
					soldierLevelSet.hitArmor = hitArmor;
					soldierLevelSet.defBreak = defBreak;
					UnitConst.GetInstance().soldierLevelSetConst.Add(new Vector2((float)soldierLevelSet.itemId, (float)soldierLevelSet.level), soldierLevelSet);
				}
			}
		}
		UnitConst.GetInstance().CommanderTalkList.Clear();
		using (XmlTextReader xmlTextReader5 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("sodierTalk"), XmlNodeType.Document, null))
		{
			while (xmlTextReader5.Read())
			{
				if (xmlTextReader5.NodeType == XmlNodeType.Element && xmlTextReader5.LocalName.Equals("configure"))
				{
					int id4 = int.Parse(xmlTextReader5.GetAttribute("id"));
					string attribute35 = xmlTextReader5.GetAttribute("description");
					CommanderTalk commanderTalk = new CommanderTalk();
					commanderTalk.id = id4;
					commanderTalk.description = attribute35;
					UnitConst.GetInstance().CommanderTalkList.Add(commanderTalk.id, commanderTalk);
				}
			}
		}
	}

	public void ReadTowerTankOrderListXml()
	{
		UnitConst.GetInstance().TowerTankOrderList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DeathToArmy"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("index");
					string attribute2 = xmlTextReader.GetAttribute("ID");
					string attribute3 = xmlTextReader.GetAttribute("buidingLevel");
					string attribute4 = xmlTextReader.GetAttribute("conditions");
					string attribute5 = xmlTextReader.GetAttribute("armyType");
					string attribute6 = xmlTextReader.GetAttribute("armyNum");
					TowerTankOrder towerTankOrder = new TowerTankOrder();
					towerTankOrder.id = int.Parse(attribute);
					towerTankOrder.buildindex = int.Parse(attribute2);
					towerTankOrder.buildlevel = int.Parse(attribute3);
					if (!string.IsNullOrEmpty(attribute4))
					{
						string[] array = attribute4.Split(new char[]
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
								case 0:
									towerTankOrder.damagetype = DamageType.Null;
									break;
								case 1:
									towerTankOrder.damagetype = DamageType.NormalBuild;
									break;
								case 2:
									towerTankOrder.damagetype = DamageType.AllBuild;
									break;
								case 3:
									towerTankOrder.damagetype = DamageType.CommandCenter;
									break;
								}
								towerTankOrder.life_crisis = (float)int.Parse(text.Split(new char[]
								{
									':'
								})[1]);
							}
						}
					}
					towerTankOrder.tank_index = int.Parse(attribute5);
					towerTankOrder.tank_num = int.Parse(attribute6);
					towerTankOrder.use = true;
					UnitConst.GetInstance().TowerTankOrderList.Add(towerTankOrder.id, towerTankOrder);
				}
			}
		}
	}

	public void ReadDeathToArmyXml()
	{
		UnitConst.GetInstance().BuildingDeathToArmy.Clear();
		UnitConst.GetInstance().ArmyDeathToArmy.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DeathToArmy"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("index");
					string attribute2 = xmlTextReader.GetAttribute("ID");
					string attribute3 = xmlTextReader.GetAttribute("type");
					string attribute4 = xmlTextReader.GetAttribute("buidingName");
					string attribute5 = xmlTextReader.GetAttribute("armyType");
					string attribute6 = xmlTextReader.GetAttribute("armyProbabilaty");
					string attribute7 = xmlTextReader.GetAttribute("region");
					DeathToArmy deathToArmy = new DeathToArmy();
					deathToArmy.id = int.Parse(attribute);
					deathToArmy.armyOrBuildingId = int.Parse(attribute2);
					deathToArmy.type = int.Parse(attribute3);
					deathToArmy.armyType = attribute5;
					deathToArmy.armyProbabilaty = attribute6;
					deathToArmy.region = int.Parse(attribute7);
					if (deathToArmy.type == 1)
					{
						UnitConst.GetInstance().BuildingDeathToArmy.Add(deathToArmy.armyOrBuildingId, deathToArmy);
					}
					else if (deathToArmy.type == 2)
					{
						UnitConst.GetInstance().ArmyDeathToArmy.Add(deathToArmy.armyOrBuildingId, deathToArmy);
					}
				}
			}
		}
	}

	public void ReadCameraXml()
	{
		UnitConst.GetInstance().cameraDataList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Camera"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("type");
					string attribute3 = xmlTextReader.GetAttribute("farest");
					string attribute4 = xmlTextReader.GetAttribute("nearest");
					string attribute5 = xmlTextReader.GetAttribute("biggestAngle");
					string attribute6 = xmlTextReader.GetAttribute("smallestAngel");
					string attribute7 = xmlTextReader.GetAttribute("normalAngle");
					string attribute8 = xmlTextReader.GetAttribute("moveAngle");
					string attribute9 = xmlTextReader.GetAttribute("placeTime");
					string attribute10 = xmlTextReader.GetAttribute("normalHeight");
					string attribute11 = xmlTextReader.GetAttribute("moveHeight");
					string attribute12 = xmlTextReader.GetAttribute("heightTime");
					CameraData cameraData = new CameraData();
					cameraData.id = int.Parse(attribute);
					cameraData.type = int.Parse(attribute2);
					cameraData.farest = float.Parse(attribute3);
					cameraData.nearest = float.Parse(attribute4);
					cameraData.biggestAngle = float.Parse(attribute5);
					cameraData.smallestAngle = float.Parse(attribute6);
					cameraData.normalAagle = float.Parse(attribute7);
					cameraData.moveAngle = float.Parse(attribute8);
					cameraData.placeTime = float.Parse(attribute9);
					cameraData.normalHeight = float.Parse(attribute10);
					cameraData.moveHeight = float.Parse(attribute11);
					cameraData.heightTime = float.Parse(attribute12);
					UnitConst.GetInstance().cameraDataList.Add(cameraData);
				}
			}
		}
	}

	public void ReadLoadingXml()
	{
		UnitConst.GetInstance().LoadList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("loading"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("officerId");
					string attribute3 = xmlTextReader.GetAttribute("description");
					LoadDes loadDes = new LoadDes();
					loadDes.id = int.Parse(attribute);
					loadDes.officerId = int.Parse(attribute2);
					loadDes.description = attribute3;
					UnitConst.GetInstance().LoadList.Add(loadDes);
				}
			}
		}
	}

	public void ReadEliteNpcBoxXML()
	{
		UnitConst.GetInstance().ElliteBattleBoxList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("EliteNpcBox"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					int num = int.Parse(xmlTextReader.GetAttribute("id"));
					int level = int.Parse(xmlTextReader.GetAttribute("level"));
					int star = int.Parse(xmlTextReader.GetAttribute("star"));
					string attribute = xmlTextReader.GetAttribute("item");
					string attribute2 = xmlTextReader.GetAttribute("res");
					string attribute3 = xmlTextReader.GetAttribute("diamond");
					ElliteBattleBox elliteBattleBox = new ElliteBattleBox();
					elliteBattleBox.id = (long)num;
					elliteBattleBox.level = level;
					elliteBattleBox.star = star;
					if (!string.IsNullOrEmpty(attribute3))
					{
						elliteBattleBox.diamond = int.Parse(attribute3);
					}
					if (!string.IsNullOrEmpty(attribute))
					{
						string[] array = attribute.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i];
							if (text.Contains(":"))
							{
								elliteBattleBox.item.Add(int.Parse(text.Split(new char[]
								{
									':'
								})[0]), int.Parse(text.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					if (!string.IsNullOrEmpty(attribute2))
					{
						string[] array2 = attribute2.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							string text2 = array2[j];
							if (text2.Contains(":"))
							{
								elliteBattleBox.res.Add((ResType)int.Parse(text2.Split(new char[]
								{
									':'
								})[0]), int.Parse(text2.Split(new char[]
								{
									':'
								})[1]));
							}
						}
					}
					UnitConst.GetInstance().ElliteBattleBoxList.Add((int)elliteBattleBox.id, elliteBattleBox);
				}
			}
		}
	}

	public void ReadBuffXML()
	{
		UnitConst.GetInstance().BuffConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Buff"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("des");
					string attribute4 = xmlTextReader.GetAttribute("buffType");
					string attribute5 = xmlTextReader.GetAttribute("buffLevel");
					string attribute6 = xmlTextReader.GetAttribute("target");
					string attribute7 = xmlTextReader.GetAttribute("powerType");
					string attribute8 = xmlTextReader.GetAttribute("power");
					string attribute9 = xmlTextReader.GetAttribute("lifetime");
					string attribute10 = xmlTextReader.GetAttribute("effect");
					Buff buff = new Buff();
					buff.id = int.Parse(attribute);
					buff.name = attribute2;
					buff.desc = attribute3;
					buff.buffType = (Buff.BuffType)int.Parse(attribute4);
					buff.buffLevel = int.Parse(attribute5);
					buff.target = (Buff.TargetType)int.Parse(attribute6);
					buff.powerType = (Buff.PowerType)int.Parse(attribute7);
					buff.power = int.Parse(attribute8);
					buff.lifeTime = float.Parse(attribute9);
					buff.effect = attribute10;
					UnitConst.GetInstance().BuffConst.Add(buff.id, buff);
				}
			}
		}
	}

	public void ReadDesignConfigXML()
	{
		UnitConst.GetInstance().DesighConfigDic.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DesignConfig"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("value");
					DesignConfig designConfig = new DesignConfig();
					designConfig.id = int.Parse(attribute);
					designConfig.name = attribute2;
					designConfig.desc = attribute3;
					designConfig.value = attribute4;
					UnitConst.GetInstance().DesighConfigDic.Add(designConfig.id, designConfig);
				}
			}
		}
	}

	public void ReadGoldPurchaseXML()
	{
		UnitConst.GetInstance().GoldPurchaseDic.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("GoldPurchase"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("number");
					GoldPurchase goldPurchase = new GoldPurchase();
					goldPurchase.id = int.Parse(attribute);
					goldPurchase.name = attribute2;
					goldPurchase.desc = attribute3;
					string[] array = attribute4.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						goldPurchase.number.Add((ResType)int.Parse(text.Split(new char[]
						{
							':'
						})[0]), int.Parse(text.Split(new char[]
						{
							':'
						})[1]));
					}
					UnitConst.GetInstance().GoldPurchaseDic.Add(goldPurchase.id, goldPurchase);
				}
			}
		}
	}

	public void ReadVipUpSetXML()
	{
		Dictionary<int, VipRight> dictionary = new Dictionary<int, VipRight>();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("VipRight"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("type");
					string attribute3 = xmlTextReader.GetAttribute("value");
					string attribute4 = xmlTextReader.GetAttribute("per");
					string attribute5 = xmlTextReader.GetAttribute("description");
					VipRight value = default(VipRight);
					value.id = int.Parse(attribute);
					value.type = (VipConst.Enum_VipRight)int.Parse(attribute2);
					value.value = int.Parse(attribute3);
					value.isPer = (!string.IsNullOrEmpty(attribute4) && int.Parse(attribute4) == 1);
					value.description = attribute5;
					dictionary.Add(value.id, value);
				}
			}
		}
		UnitConst.GetInstance().VipConstData.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Vip"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute6 = xmlTextReader2.GetAttribute("id");
					string attribute7 = xmlTextReader2.GetAttribute("num");
					string attribute8 = xmlTextReader2.GetAttribute("resReward");
					string attribute9 = xmlTextReader2.GetAttribute("itemReward");
					string attribute10 = xmlTextReader2.GetAttribute("skillReward");
					string attribute11 = xmlTextReader2.GetAttribute("vipRight");
					VipConst vipConst = new VipConst();
					vipConst.level = int.Parse(attribute6);
					vipConst.money_buyed = int.Parse(attribute7);
					if (!string.IsNullOrEmpty(attribute8))
					{
						string[] array = attribute8.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string[] array2 = array[i].Split(new char[]
							{
								':'
							});
							vipConst.resReward.Add((ResType)int.Parse(array2[0]), int.Parse(array2[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array3 = attribute9.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array3.Length; j++)
						{
							string[] array4 = array3[j].Split(new char[]
							{
								':'
							});
							vipConst.itemReward.Add(int.Parse(array4[0]), int.Parse(array4[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						string[] array5 = attribute10.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array5.Length; k++)
						{
							string[] array6 = array5[k].Split(new char[]
							{
								':'
							});
							vipConst.skillReward.Add(int.Parse(array6[0]), int.Parse(array6[1]));
						}
					}
					if (!string.IsNullOrEmpty(attribute11))
					{
						string[] array7 = attribute11.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array7.Length; l++)
						{
							vipConst.rights.Add(dictionary[int.Parse(array7[l])]);
						}
					}
					UnitConst.GetInstance().VipConstData.Add(vipConst.level, vipConst);
				}
			}
		}
	}

	public void ReadTalkItemXML()
	{
		UnitConst.GetInstance().TalkItemConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("talk"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("content");
					string attribute3 = xmlTextReader.GetAttribute("nextid");
					string attribute4 = xmlTextReader.GetAttribute("name");
					string attribute5 = xmlTextReader.GetAttribute("icon");
					TalkItem talkItem = new TalkItem();
					talkItem.id = int.Parse(attribute);
					talkItem.content = attribute2;
					talkItem.nextId = int.Parse(attribute3);
					talkItem.talkerName = attribute4;
					talkItem.talkerIcon = attribute5;
					UnitConst.GetInstance().TalkItemConst.Add(talkItem.id, talkItem);
				}
			}
		}
	}

	public void ReadActivityXML()
	{
		UnitConst.GetInstance().ActivityItemConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Activity"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("type");
					string attribute5 = xmlTextReader.GetAttribute("npcId");
					string attribute6 = xmlTextReader.GetAttribute("beginDayOfWeek");
					string attribute7 = xmlTextReader.GetAttribute("beginTime");
					string attribute8 = xmlTextReader.GetAttribute("durationTime");
					string attribute9 = xmlTextReader.GetAttribute("minLevel");
					string attribute10 = xmlTextReader.GetAttribute("maxLevel");
					string attribute11 = xmlTextReader.GetAttribute("freeTimes");
					string attribute12 = xmlTextReader.GetAttribute("times");
					string attribute13 = xmlTextReader.GetAttribute("needMoney");
					string attribute14 = xmlTextReader.GetAttribute("nextNeedMoneyMultiplier");
					string attribute15 = xmlTextReader.GetAttribute("armysInfo");
					ActivityItem activityItem = new ActivityItem();
					activityItem.id = int.Parse(attribute);
					activityItem.name = attribute2;
					activityItem.desc = attribute3;
					activityItem.actType = (ActType)int.Parse(attribute4);
					activityItem.npcId = int.Parse(attribute5);
					if (!string.IsNullOrEmpty(attribute6))
					{
						string[] array = attribute6.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							activityItem.beginDayOfWeek.Add(int.Parse(array[i]));
						}
					}
					activityItem.beginTime = int.Parse(attribute7);
					activityItem.durationTime = int.Parse(attribute8);
					activityItem.minLv = int.Parse(attribute9);
					activityItem.maxLv = int.Parse(attribute10);
					activityItem.freeTimes = int.Parse(attribute11);
					activityItem.times = int.Parse(attribute12);
					activityItem.needMoney = int.Parse(attribute13);
					activityItem.nextNeedMoneyMultiplier = int.Parse(attribute14);
					if (!string.IsNullOrEmpty(attribute15))
					{
						string[] array2 = attribute15.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array2.Length; j++)
						{
							ActivityItem.Solider solider = new ActivityItem.Solider();
							solider.index = int.Parse(array2[j].Split(new char[]
							{
								':'
							})[0]);
							solider.id = int.Parse(array2[j].Split(new char[]
							{
								':'
							})[1]);
							solider.lv = int.Parse(array2[j].Split(new char[]
							{
								':'
							})[2]);
							solider.num = int.Parse(array2[j].Split(new char[]
							{
								':'
							})[3]);
							activityItem.soliders.Add(solider);
						}
					}
					UnitConst.GetInstance().ActivityItemConst.Add(activityItem.id, activityItem);
				}
			}
		}
		ActivityManager.InitNpcActMap();
	}

	public void ReadSignXML()
	{
		UnitConst.GetInstance().signConst.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("DailySign"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("name");
					string attribute3 = xmlTextReader.GetAttribute("description");
					string attribute4 = xmlTextReader.GetAttribute("money");
					string attribute5 = xmlTextReader.GetAttribute("items");
					string attribute6 = xmlTextReader.GetAttribute("doubleNeedVipLevel");
					Sign sign = new Sign();
					sign.id = int.Parse(attribute);
					sign.name = attribute2;
					sign.desc = attribute3;
					sign.money = ((!string.IsNullOrEmpty(attribute4)) ? int.Parse(attribute4) : 0);
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array = attribute5.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							sign.reward.Add(int.Parse(array[i].Split(new char[]
							{
								':'
							})[0]), int.Parse(array[i].Split(new char[]
							{
								':'
							})[1]));
						}
					}
					if (!UnitConst.GetInstance().signConst.ContainsKey(sign.id))
					{
						UnitConst.GetInstance().signConst.Add(sign.id, sign);
					}
					else
					{
						Debug.LogError("Sign相同的Key" + sign.id);
					}
				}
			}
		}
	}

	public void ReadMapEntity()
	{
		UnitConst.GetInstance().mapEntityList.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("MapEntity_1"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("index");
					string attribute3 = xmlTextReader.GetAttribute("entityType");
					string attribute4 = xmlTextReader.GetAttribute("assignNPC");
					string attribute5 = xmlTextReader.GetAttribute("offset");
					string attribute6 = xmlTextReader.GetAttribute("mapRes");
					string attribute7 = xmlTextReader.GetAttribute("radarLevel");
					string attribute8 = xmlTextReader.GetAttribute("nextIndexs");
					string attribute9 = xmlTextReader.GetAttribute("coord");
					MapEntity mapEntity = new MapEntity();
					mapEntity.id = int.Parse(attribute);
					mapEntity.index = int.Parse(attribute2);
					mapEntity.entityType = int.Parse(attribute3);
					mapEntity.assignNPC = attribute4;
					mapEntity.offset = attribute5;
					mapEntity.mapRes = attribute6;
					if (!string.IsNullOrEmpty(attribute9))
					{
						mapEntity.radarLevel = int.Parse(attribute7);
					}
					mapEntity.nextIndexs = attribute8;
					if (string.IsNullOrEmpty(attribute9))
					{
						mapEntity.coord = new Vector3(0f, 0f, 0f);
					}
					else
					{
						string[] array = attribute9.Split(new char[]
						{
							','
						});
						mapEntity.coord = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
					UnitConst.GetInstance().mapEntityList.Add(mapEntity.index, mapEntity);
				}
			}
		}
		UnitConst.GetInstance().IslandTypeToModel.Clear();
		UnitConst.GetInstance().IslandTypeToModelScale.Clear();
		UnitConst.GetInstance().IslandTypeToModelAngel.Clear();
		using (XmlTextReader xmlTextReader2 = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("IslandTypeToModel"), XmlNodeType.Document, null))
		{
			while (xmlTextReader2.Read())
			{
				if (xmlTextReader2.NodeType == XmlNodeType.Element && xmlTextReader2.LocalName.Equals("configure"))
				{
					string attribute10 = xmlTextReader2.GetAttribute("id");
					string attribute11 = xmlTextReader2.GetAttribute("islandType");
					string attribute12 = xmlTextReader2.GetAttribute("bodyId");
					string attribute13 = xmlTextReader2.GetAttribute("scale");
					string attribute14 = xmlTextReader2.GetAttribute("angle");
					if (!UnitConst.GetInstance().IslandTypeToModel.ContainsKey(int.Parse(attribute11)))
					{
						UnitConst.GetInstance().IslandTypeToModel.Add(int.Parse(attribute11), attribute12);
						if (!string.IsNullOrEmpty(attribute13))
						{
							string[] array2 = attribute13.Split(new char[]
							{
								','
							});
							UnitConst.GetInstance().IslandTypeToModelScale.Add(int.Parse(attribute11), new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2])));
						}
						else
						{
							UnitConst.GetInstance().IslandTypeToModelScale.Add(int.Parse(attribute11), Vector3.zero);
						}
						if (!string.IsNullOrEmpty(attribute14))
						{
							string[] array3 = attribute14.Split(new char[]
							{
								','
							});
							UnitConst.GetInstance().IslandTypeToModelAngel.Add(int.Parse(attribute11), new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2])));
						}
						else
						{
							UnitConst.GetInstance().IslandTypeToModelAngel.Add(int.Parse(attribute11), Vector3.zero);
						}
					}
					else
					{
						LogManage.Log("IslandTypeToModel 的 islandType 有重复");
					}
				}
			}
		}
	}

	public void ReadRandomname()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("Randomname"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("name");
					string attribute2 = xmlTextReader.GetAttribute("name1");
					list.Add(attribute);
					list2.Add(attribute2);
				}
			}
		}
		UnitConst.GetInstance().ReName_Name = list.ToArray();
		UnitConst.GetInstance().ReName_Name1 = list2.ToArray();
	}

	public void ReadNpcAttackBattle()
	{
		UnitConst.GetInstance().npcAttackBattle.Clear();
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("NpcAttackBattle"), XmlNodeType.Document, null))
		{
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					string attribute = xmlTextReader.GetAttribute("id");
					string attribute2 = xmlTextReader.GetAttribute("level");
					string attribute3 = xmlTextReader.GetAttribute("AmyType1");
					string attribute4 = xmlTextReader.GetAttribute("AppearPoint1");
					string attribute5 = xmlTextReader.GetAttribute("AmyType2");
					string attribute6 = xmlTextReader.GetAttribute("AppearPoint2");
					string attribute7 = xmlTextReader.GetAttribute("AmyType3");
					string attribute8 = xmlTextReader.GetAttribute("AppearPoint3");
					string attribute9 = xmlTextReader.GetAttribute("AmyType4");
					string attribute10 = xmlTextReader.GetAttribute("AppearPoint4");
					string attribute11 = xmlTextReader.GetAttribute("AmyType5");
					string attribute12 = xmlTextReader.GetAttribute("AppearPoint5");
					NpcAttackBattle npcAttackBattle = new NpcAttackBattle();
					npcAttackBattle.id = int.Parse(attribute);
					npcAttackBattle.level = int.Parse(attribute2);
					List<NpcAttackBattleItem> list = new List<NpcAttackBattleItem>();
					if (!string.IsNullOrEmpty(attribute3))
					{
						string[] array = attribute3.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string[] array2 = array[i].Split(new char[]
							{
								':'
							});
							list.Add(new NpcAttackBattleItem
							{
								type = int.Parse(array2[0]),
								num = int.Parse(array2[1])
							});
						}
					}
					List<NpcAttackBattleItem> list2 = new List<NpcAttackBattleItem>();
					if (!string.IsNullOrEmpty(attribute5))
					{
						string[] array3 = attribute5.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array3.Length; j++)
						{
							string[] array4 = array3[j].Split(new char[]
							{
								':'
							});
							list2.Add(new NpcAttackBattleItem
							{
								type = int.Parse(array4[0]),
								num = int.Parse(array4[1])
							});
						}
					}
					List<NpcAttackBattleItem> list3 = new List<NpcAttackBattleItem>();
					if (!string.IsNullOrEmpty(attribute7))
					{
						string[] array5 = attribute7.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array5.Length; k++)
						{
							string[] array6 = array5[k].Split(new char[]
							{
								':'
							});
							list3.Add(new NpcAttackBattleItem
							{
								type = int.Parse(array6[0]),
								num = int.Parse(array6[1])
							});
						}
					}
					List<NpcAttackBattleItem> list4 = new List<NpcAttackBattleItem>();
					if (!string.IsNullOrEmpty(attribute9))
					{
						string[] array7 = attribute9.Split(new char[]
						{
							','
						});
						for (int l = 0; l < array7.Length; l++)
						{
							string[] array8 = array7[l].Split(new char[]
							{
								':'
							});
							list4.Add(new NpcAttackBattleItem
							{
								type = int.Parse(array8[0]),
								num = int.Parse(array8[1])
							});
						}
					}
					List<NpcAttackBattleItem> list5 = new List<NpcAttackBattleItem>();
					if (!string.IsNullOrEmpty(attribute11))
					{
						string[] array9 = attribute11.Split(new char[]
						{
							','
						});
						for (int m = 0; m < array9.Length; m++)
						{
							string[] array10 = array9[m].Split(new char[]
							{
								':'
							});
							list5.Add(new NpcAttackBattleItem
							{
								type = int.Parse(array10[0]),
								num = int.Parse(array10[1])
							});
						}
					}
					if (!string.IsNullOrEmpty(attribute4))
					{
						npcAttackBattle.item.Add(int.Parse(attribute4), list);
					}
					if (!string.IsNullOrEmpty(attribute6))
					{
						npcAttackBattle.item.Add(int.Parse(attribute6), list2);
					}
					if (!string.IsNullOrEmpty(attribute8))
					{
						npcAttackBattle.item.Add(int.Parse(attribute8), list3);
					}
					if (!string.IsNullOrEmpty(attribute10))
					{
						npcAttackBattle.item.Add(int.Parse(attribute10), list4);
					}
					if (!string.IsNullOrEmpty(attribute12))
					{
						npcAttackBattle.item.Add(int.Parse(attribute12), list5);
					}
					UnitConst.GetInstance().npcAttackBattle.Add(npcAttackBattle.id, npcAttackBattle);
				}
			}
		}
	}
}
