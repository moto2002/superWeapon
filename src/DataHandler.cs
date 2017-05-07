using BattleEvent;
using msg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public sealed class DataHandler
{
	public delegate void Data_Change(int opcodeCMD);

	public delegate void DicData_Change(Dictionary<int, int> key_Value);

	public delegate void KVData_Change(List<KVStruct_Client> key_Value);

	public delegate void OpcodeHandler(bool isError, Opcode opcode);

	public Action<DailyTask> TaskNotice;

	public Action AchieveNotice;

	private bool isInited;

	public Dictionary<int, DataHandler.OpcodeHandler> NetMsgDic = new Dictionary<int, DataHandler.OpcodeHandler>();

	public Dictionary<int, ButtonClick> NetMsgButtonDic = new Dictionary<int, ButtonClick>();

	public event DataHandler.Data_Change DataChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.DataChange = (DataHandler.Data_Change)Delegate.Combine(this.DataChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.DataChange = (DataHandler.Data_Change)Delegate.Remove(this.DataChange, value);
		}
	}

	public event Action DataReadEnd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.DataReadEnd = (Action)Delegate.Combine(this.DataReadEnd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.DataReadEnd = (Action)Delegate.Remove(this.DataReadEnd, value);
		}
	}

	public event DataHandler.KVData_Change ItemAddChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.ItemAddChange = (DataHandler.KVData_Change)Delegate.Combine(this.ItemAddChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.ItemAddChange = (DataHandler.KVData_Change)Delegate.Remove(this.ItemAddChange, value);
		}
	}

	public event DataHandler.KVData_Change ItemSubChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.ItemSubChange = (DataHandler.KVData_Change)Delegate.Combine(this.ItemSubChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.ItemSubChange = (DataHandler.KVData_Change)Delegate.Remove(this.ItemSubChange, value);
		}
	}

	public event DataHandler.Data_Change LevelChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.LevelChange = (DataHandler.Data_Change)Delegate.Combine(this.LevelChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.LevelChange = (DataHandler.Data_Change)Delegate.Remove(this.LevelChange, value);
		}
	}

	public event DataHandler.Data_Change CoinChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.CoinChange = (DataHandler.Data_Change)Delegate.Combine(this.CoinChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.CoinChange = (DataHandler.Data_Change)Delegate.Remove(this.CoinChange, value);
		}
	}

	public event DataHandler.Data_Change CoinAdd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.CoinAdd = (DataHandler.Data_Change)Delegate.Combine(this.CoinAdd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.CoinAdd = (DataHandler.Data_Change)Delegate.Remove(this.CoinAdd, value);
		}
	}

	public event DataHandler.Data_Change OilChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OilChange = (DataHandler.Data_Change)Delegate.Combine(this.OilChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OilChange = (DataHandler.Data_Change)Delegate.Remove(this.OilChange, value);
		}
	}

	public event DataHandler.Data_Change OilAdd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OilAdd = (DataHandler.Data_Change)Delegate.Combine(this.OilAdd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OilAdd = (DataHandler.Data_Change)Delegate.Remove(this.OilAdd, value);
		}
	}

	public event DataHandler.Data_Change SteelChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.SteelChange = (DataHandler.Data_Change)Delegate.Combine(this.SteelChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.SteelChange = (DataHandler.Data_Change)Delegate.Remove(this.SteelChange, value);
		}
	}

	public event DataHandler.Data_Change SteelAdd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.SteelAdd = (DataHandler.Data_Change)Delegate.Combine(this.SteelAdd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.SteelAdd = (DataHandler.Data_Change)Delegate.Remove(this.SteelAdd, value);
		}
	}

	public event DataHandler.Data_Change RareEarthChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.RareEarthChange = (DataHandler.Data_Change)Delegate.Combine(this.RareEarthChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.RareEarthChange = (DataHandler.Data_Change)Delegate.Remove(this.RareEarthChange, value);
		}
	}

	public event DataHandler.Data_Change RareEarthAdd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.RareEarthAdd = (DataHandler.Data_Change)Delegate.Combine(this.RareEarthAdd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.RareEarthAdd = (DataHandler.Data_Change)Delegate.Remove(this.RareEarthAdd, value);
		}
	}

	public event DataHandler.Data_Change RMBChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.RMBChange = (DataHandler.Data_Change)Delegate.Combine(this.RMBChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.RMBChange = (DataHandler.Data_Change)Delegate.Remove(this.RMBChange, value);
		}
	}

	public event DataHandler.Data_Change ExpChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.ExpChange = (DataHandler.Data_Change)Delegate.Combine(this.ExpChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.ExpChange = (DataHandler.Data_Change)Delegate.Remove(this.ExpChange, value);
		}
	}

	public event DataHandler.Data_Change AttckForceChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.AttckForceChange = (DataHandler.Data_Change)Delegate.Combine(this.AttckForceChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.AttckForceChange = (DataHandler.Data_Change)Delegate.Remove(this.AttckForceChange, value);
		}
	}

	public event DataHandler.Data_Change DefForceChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.DefForceChange = (DataHandler.Data_Change)Delegate.Combine(this.DefForceChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.DefForceChange = (DataHandler.Data_Change)Delegate.Remove(this.DefForceChange, value);
		}
	}

	private void Init()
	{
		this.NetMsgDic[4006] = new DataHandler.OpcodeHandler(SenceHandler.GC_GetMapData);
		this.NetMsgDic[3000] = new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure);
		this.NetMsgDic[3008] = new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure);
		this.NetMsgDic[3004] = new DataHandler.OpcodeHandler(ArmyHandler.GC_ArmsUpdateLevel);
		this.NetMsgDic[6002] = new DataHandler.OpcodeHandler(TechHandler.GC_TechUpdate);
		this.NetMsgDic[2004] = new DataHandler.OpcodeHandler(BuildingHandler.GC_BuildingMoveEnd);
		this.NetMsgDic[2022] = new DataHandler.OpcodeHandler(BuildingHandler.GC_WallListMoveEnd);
		this.NetMsgDic[5000] = new DataHandler.OpcodeHandler(FightHundler.GC_StartFight);
		this.NetMsgDic[5002] = new DataHandler.OpcodeHandler(FightHundler.GC_FinishFight);
		this.NetMsgDic[1200] = new DataHandler.OpcodeHandler(TopTenHandler.GC_TopTenListEnd);
		this.NetMsgDic[1700] = new DataHandler.OpcodeHandler(BuyGoldHandler.SCUseMoneyBuyGold);
		this.NetMsgDic[1802] = new DataHandler.OpcodeHandler(EmailHandler.SCPlayerMailStatus);
		this.NetMsgDic[2100] = new DataHandler.OpcodeHandler(ActivityHandler.SCRefreshActivity);
		this.isInited = true;
	}

	public void AddCallBack(int cmd, DataHandler.OpcodeHandler handler)
	{
		if (this.NetMsgDic.ContainsKey(cmd))
		{
			this.NetMsgDic[cmd] = handler;
		}
		else
		{
			this.NetMsgDic.Add(cmd, handler);
		}
	}

	public void RemoveCallback(int cmd)
	{
		if (this.NetMsgDic.ContainsKey(cmd))
		{
			this.NetMsgDic.Remove(cmd);
		}
	}

	public void ReadData(int downCMD, Opcode opcode)
	{
		Dictionary<int, List<object>> dic = opcode.getDic();
		LogManage.LogError(string.Format(" Down_CMD:{0}", downCMD));
		try
		{
			if (dic.ContainsKey(10002))
			{
				for (int i = 0; i < dic[10002].Count; i++)
				{
					SCPlayerBase sCPlayerBase = dic[10002][i] as SCPlayerBase;
					HeroInfo.GetInstance().userId = sCPlayerBase.id;
					HeroInfo.GetInstance().userName = sCPlayerBase.name;
					HeroInfo.GetInstance().homeInWMapIdx = sCPlayerBase.index;
					HeroInfo.GetInstance().createTime = (long)((float)sCPlayerBase.createTime * 0.001f);
					if (HeroInfo.GetInstance().playerGroupId != 0L && sCPlayerBase.legionId == 0L)
					{
						HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您已被军团移除", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
						if (ArmyManager.ins && ArmyManager.ins.armyPeopleManger.gameObject.activeSelf)
						{
							ArmyManager.ins.CloseThis(null);
						}
					}
					HeroInfo.GetInstance().playerGroupId = sCPlayerBase.legionId;
					HeroInfo.GetInstance().playerGroup = sCPlayerBase.legionId.ToString();
					HeroInfo.GetInstance().topScore = sCPlayerBase.ranking;
					HeroInfo.GetInstance().LegionOutTime = TimeTools.ConvertLongDateTime(sCPlayerBase.legionOutTime);
					if (sCPlayerBase.islandId != 0L)
					{
						HeroInfo.GetInstance().homeMapID = sCPlayerBase.islandId;
					}
					LogManage.LogError("下发是否是第一次" + sCPlayerBase.isFirstLogin);
					GameConst.IsFirstLogin = (sCPlayerBase.isFirstLogin == 1);
					int num = sCPlayerBase.guideId;
					if (sCPlayerBase.guideId == 0)
					{
						num = 1;
					}
					LogManage.Log("===============接收引导ID：" + sCPlayerBase.guideId);
					NewbieGuidePanel.curGuideIndex = ((!UnitConst.GetInstance().newbieGuide.ContainsKey(num)) ? num : UnitConst.GetInstance().newbieGuide[num].toNextTeam);
					if (!NewbieGuideManage._instance.AlreadyPassNewGuide)
					{
						NewbieGuidePanel.guideIdByServer = num;
						if (NewbieGuidePanel.guideIdByServer >= GameSetting.MaxLuaProcess)
						{
							NewbieGuideManage._instance.AlreadyPassNewGuide = true;
						}
					}
					if (sCPlayerBase.taskGuideId == 1)
					{
						NewbieGuideManage._instance.LockTaskByNewBie = true;
					}
					NewbieGuidePanel.TaskGuideID = sCPlayerBase.taskGuideId;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10002);
				}
			}
		}
		catch (Exception var_4_27C)
		{
		}
		try
		{
			if (dic.ContainsKey(10001))
			{
				for (int j = 0; j < dic[10001].Count; j++)
				{
					SCIslandData sCIslandData = dic[10001][j] as SCIslandData;
					SenceInfo.curMap = InfoMgr.GetMapData(sCIslandData);
					SenceManager.inst.OtherIslandExtraArmyList.Clear();
					if (sCIslandData.extraArmyData != null)
					{
						LogManage.LogError("item.extraArmyData:" + sCIslandData.extraArmyData.Count);
						if (SenceInfo.curMap.ID != HeroInfo.GetInstance().homeMapID)
						{
							for (int k = 0; k < sCIslandData.extraArmyData.Count; k++)
							{
								SenceManager.inst.OtherIslandExtraArmyList.Add((int)sCIslandData.extraArmyData[k].id, sCIslandData.extraArmyData[k]);
							}
						}
					}
				}
				LogManage.LogError("下发岛屿数据了·" + SenceInfo.curMap.mapIndex);
				if (this.DataChange != null)
				{
					this.DataChange(10001);
				}
			}
		}
		catch (Exception var_8_3C1)
		{
		}
		try
		{
			if (dic.ContainsKey(10121))
			{
				for (int l = 0; l < dic[10121].Count; l++)
				{
					SCExtraArmy sCExtraArmy = dic[10121][l] as SCExtraArmy;
					if (SenceManager.inst)
					{
						SenceManager.inst.ExtraArmyList.Add((int)sCExtraArmy.id, sCExtraArmy);
					}
					LogManage.LogError("下发特殊兵种数据了·" + sCExtraArmy.id);
				}
				if (MainUIPanelManage._instance)
				{
					SenceManager.inst.ShowExtraArmyInHome();
				}
				if (this.DataChange != null)
				{
					this.DataChange(10121);
				}
			}
		}
		catch (Exception var_11_492)
		{
		}
		try
		{
			if (dic.ContainsKey(10095))
			{
				for (int m = 0; m < dic[10095].Count; m++)
				{
					SCTakeResShow sCTakeResShow = dic[10095][m] as SCTakeResShow;
					ResourcePanelManage.tarId = sCTakeResShow.id;
					ResourcePanelManage.getResNum = (float)sCTakeResShow.value;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10095);
				}
			}
		}
		catch (Exception var_14_520)
		{
		}
		try
		{
			if (dic.ContainsKey(10064))
			{
				for (int n = 0; n < dic[10064].Count; n++)
				{
					SCNoticeList sCNoticeList = dic[10064][n] as SCNoticeList;
					foreach (NoticeData current in sCNoticeList.data)
					{
						HeroInfo.GetInstance().gameAnnouncementData.lastNoticeTime = Math.Max(current.modifyTime, HeroInfo.GetInstance().gameAnnouncementData.lastNoticeTime);
						if (current.noticeType == 3)
						{
							HeroInfo.GetInstance().AllDisplayActives.Clear();
							string[] array = current.content.Split(new char[]
							{
								','
							});
							for (int num2 = 0; num2 < array.Length; num2++)
							{
								string s = array[num2];
								HeroInfo.GetInstance().AllDisplayActives.Add(int.Parse(s));
							}
						}
						else
						{
							if (HeroInfo.GetInstance().gameAnnouncementData.showText.ContainsKey(current.id))
							{
								HeroInfo.GetInstance().gameAnnouncementData.showText[current.id] = current;
							}
							else
							{
								HeroInfo.GetInstance().gameAnnouncementData.showText.Add(current.id, current);
							}
							if (current.noticeType == 2)
							{
								SCSendMessage sCSendMessage = new SCSendMessage();
								StringBuilder stringBuilder = new StringBuilder();
								string[] array2 = current.content.Split(new char[]
								{
									'|'
								});
								for (int num3 = 0; num3 < array2.Length; num3++)
								{
									string text = array2[num3];
									string[] array3 = text.Split(new char[]
									{
										'='
									});
									if (array3[0].Equals("奖励资源") && !string.IsNullOrEmpty(array3[1].Trim()))
									{
										string textByKey = LanguageManage.GetTextByKey(array3[0], "BattleNotice");
										string[] array4 = array3[1].Split(new char[]
										{
											','
										});
										StringBuilder stringBuilder2 = new StringBuilder();
										int num4 = 0;
										while (num4 < array4.Length)
										{
											if (array4[num4].Contains(":"))
											{
												switch (int.Parse(array4[num4].Split(new char[]
												{
													':'
												})[0]))
												{
												case 1:
													stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取金币", "ResIsland"), array4[num4].Split(new char[]
													{
														':'
													})[1]));
													break;
												case 2:
													stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取石油", "ResIsland"), array4[num4].Split(new char[]
													{
														':'
													})[1]));
													break;
												case 3:
													stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钢铁", "ResIsland"), array4[num4].Split(new char[]
													{
														':'
													})[1]));
													break;
												case 4:
													stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取稀矿", "ResIsland"), array4[num4].Split(new char[]
													{
														':'
													})[1]));
													break;
												case 7:
													stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钻石", "ResIsland"), array4[num4].Split(new char[]
													{
														':'
													})[1]));
													break;
												}
											}
											n++;
										}
										stringBuilder.Append(string.Format(textByKey, stringBuilder2.ToString()));
									}
									else if (array3[0].Equals("奖励道具") && !string.IsNullOrEmpty(array3[1].Trim()))
									{
										string textByKey2 = LanguageManage.GetTextByKey(array3[0], "BattleNotice");
										string[] array5 = array3[1].Split(new char[]
										{
											','
										});
										StringBuilder stringBuilder3 = new StringBuilder();
										int num5 = 0;
										while (num5 < array5.Length)
										{
											if (array5[num5].Contains(":"))
											{
												stringBuilder3.Append(string.Format("  {0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[int.Parse(array5[num5].Split(new char[]
												{
													':'
												})[0])].Name, "item"), array5[num5].Split(new char[]
												{
													':'
												})[1]));
											}
											n++;
										}
										stringBuilder.Append(string.Format(textByKey2, stringBuilder3.ToString()));
									}
									else
									{
										bool flag = false;
										string textByKey3 = LanguageManage.GetTextByKey(array3[0], "BattleNotice", ref flag);
										if (array3.Length > 1)
										{
											string[] array6 = array3[1].Split(new char[]
											{
												';'
											});
											try
											{
												stringBuilder.Append(string.Format(textByKey3, array6));
											}
											catch (Exception var_39_A25)
											{
												string text2 = string.Empty;
												string[] array7 = array6;
												for (int num6 = 0; num6 < array7.Length; num6++)
												{
													string str = array7[num6];
													text2 += str;
												}
												LogManage.LogError(string.Format("跑马灯的文字内容是{0} 参数是{1)", textByKey3, text2));
											}
										}
										else if (flag)
										{
											stringBuilder.Append(textByKey3);
										}
										else
										{
											stringBuilder.Append(array3[0]);
										}
									}
								}
								if (sCSendMessage.msg == null)
								{
									sCSendMessage.msg = new msg.ChatMessage();
								}
								sCSendMessage.msg.message = stringBuilder.ToString();
								sCSendMessage.msg.channel = 4;
								sCSendMessage.msg.channel = 4;
								sCSendMessage.id = TimeTools.GetNowTimeSyncServerToLong();
								HeroInfo.GetInstance().chatMessage.ChatData.Add(sCSendMessage);
								HeroInfo.GetInstance().chatMessage.ChatTempData.Add(sCSendMessage);
							}
						}
					}
					foreach (long current2 in sCNoticeList.removed)
					{
						HeroInfo.GetInstance().gameAnnouncementData.showText.Remove(current2);
					}
					HeroInfo.GetInstance().gameAnnouncementData.isHaveNewAnounce = (sCNoticeList.hasNew == 1);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10064);
				}
			}
		}
		catch (Exception var_46_BEF)
		{
		}
		try
		{
			if (dic.ContainsKey(10087))
			{
				for (int num7 = 0; num7 < dic[10087].Count; num7++)
				{
					SCBuyArmyToken sCBuyArmyToken = dic[10087][num7] as SCBuyArmyToken;
					HeroInfo.GetInstance().buyArmyTokenTimes = sCBuyArmyToken.times;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10087);
				}
			}
		}
		catch (Exception var_49_C75)
		{
		}
		try
		{
			if (dic.ContainsKey(10081))
			{
				GetawardPanelShow.getId.Clear();
				for (int num8 = 0; num8 < dic[10081].Count; num8++)
				{
					SCSevenDaysPrize sCSevenDaysPrize = dic[10081][num8] as SCSevenDaysPrize;
					GetawardPanelShow.DayNum = (int)sCSevenDaysPrize.id;
					for (int num9 = 0; num9 < UnitConst.GetInstance().SevenDay.Count; num9++)
					{
						if (num9 < sCSevenDaysPrize.prizeData.Count)
						{
							SevenDayMgr.state[(int)(checked((IntPtr)(unchecked(sCSevenDaysPrize.prizeData[num9].key - 1L))))] = (int)sCSevenDaysPrize.prizeData[num9].value;
							if (sCSevenDaysPrize.prizeData[num9].value == 0L)
							{
								GetawardPanelShow.getId.Add((int)sCSevenDaysPrize.prizeData[num9].key);
							}
						}
						else
						{
							SevenDayMgr.state[num9] = 2;
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10081);
				}
			}
		}
		catch (Exception var_53_DAD)
		{
		}
		try
		{
			if (dic.ContainsKey(10079))
			{
				int key = UnitConst.GetInstance().loadReward.Values.Count<LoadReward>();
				for (int num10 = 0; num10 < dic[10079].Count; num10++)
				{
					SCOnlineRewards sCOnlineRewards = dic[10079][num10] as SCOnlineRewards;
					OnLineAward.laod.id = (int)sCOnlineRewards.id;
					OnLineAward.laod.step = sCOnlineRewards.step;
					OnLineAward.laod.time = TimeTools.ConvertLongDateTime(sCOnlineRewards.nextRewardTime);
					if (OnLineAward.laod.time > TimeTools.GetNowTimeSyncServerToDateTime())
					{
						UnitConst.GetInstance().loadReward[sCOnlineRewards.step].isCanGetOnLine = false;
					}
					if (sCOnlineRewards.step < 0)
					{
						UnitConst.GetInstance().loadReward[key].isCanGetOnLine = false;
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10079);
				}
			}
		}
		catch (Exception var_57_ECA)
		{
		}
		try
		{
			if (dic.ContainsKey(10063))
			{
				for (int num11 = 0; num11 < dic[10063].Count; num11++)
				{
					SCBtnOpen sCBtnOpen = dic[10063][num11] as SCBtnOpen;
					NewbieGuideManage.btnIDList.Clear();
					NewbieGuideManage.btnIDList.AddRange(sCBtnOpen.btnList);
					NewbieGuideManage.btnID.Clear();
					NewbieGuideManage.btnID.AddRange(sCBtnOpen.btnId);
					NewbieGuideManage.isAllOpenbtn = (sCBtnOpen.isAllOpen == 1);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10063);
				}
			}
		}
		catch (Exception var_60_F84)
		{
		}
		try
		{
			if (dic.ContainsKey(10016))
			{
				for (int num12 = 0; num12 < dic[10016].Count; num12++)
				{
					SCFightingEvent sCFightingEvent = dic[10016][num12] as SCFightingEvent;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10016);
				}
			}
		}
		catch (Exception var_63_FF9)
		{
		}
		try
		{
			if (dic.ContainsKey(10073))
			{
				for (int num13 = 0; num13 < dic[10073].Count; num13++)
				{
					SCIslandBeAttackedData sCIslandBeAttackedData = dic[10073][num13] as SCIslandBeAttackedData;
					SenceManager.islandid = sCIslandBeAttackedData.id;
					SenceManager.DestroyId.AddRange(sCIslandBeAttackedData.buildingId);
					SenceManager.IsDestroy = sCIslandBeAttackedData.all;
				}
			}
		}
		catch (Exception var_66_107C)
		{
		}
		try
		{
			if (dic.ContainsKey(10039))
			{
				for (int num14 = 0; num14 < dic[10039].Count; num14++)
				{
					SCResourceLimit sCResourceLimit = dic[10039][num14] as SCResourceLimit;
					for (int num15 = 0; num15 < sCResourceLimit.limits.Count; num15++)
					{
						switch ((int)sCResourceLimit.limits[num15].key)
						{
						case 1:
							HeroInfo.GetInstance().playerRes.maxCoin = (int)sCResourceLimit.limits[num15].value;
							break;
						case 2:
							HeroInfo.GetInstance().playerRes.maxOil = (int)sCResourceLimit.limits[num15].value;
							break;
						case 3:
							HeroInfo.GetInstance().playerRes.maxSteel = (int)sCResourceLimit.limits[num15].value;
							break;
						case 4:
							HeroInfo.GetInstance().playerRes.maxRareEarth = (int)sCResourceLimit.limits[num15].value;
							break;
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10039);
				}
			}
		}
		catch (Exception var_70_11EF)
		{
		}
		try
		{
			if (dic.ContainsKey(10033))
			{
				for (int num16 = 0; num16 < dic[10033].Count; num16++)
				{
					SCRankingList sCRankingList = dic[10033][num16] as SCRankingList;
					TopTenPanelManage.rank.Clear();
					TopTenPanelManage.rank.AddRange(sCRankingList.ranking);
					TopTenPanelManage.topTenRefrshTime = sCRankingList.flushHour;
					HeroInfo.GetInstance().topScore = sCRankingList.ownRank;
					if (MainUIPanelManage._instance)
					{
						MainUIPanelManage._instance.ShowTopTenInfo();
					}
					LogManage.LogError("list.flushHour;" + sCRankingList.flushHour);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10033);
				}
			}
		}
		catch (Exception var_73_12D0)
		{
		}
		try
		{
			if (dic.ContainsKey(10109))
			{
				for (int num17 = 0; num17 < dic[10109].Count; num17++)
				{
					SCProfile sCProfile = dic[10109][num17] as SCProfile;
					if (sCProfile.key == 4)
					{
						HeroInfo.GetInstance().LegionOutTime = TimeTools.ConvertLongDateTime(long.Parse(sCProfile.value));
					}
					if (sCProfile.key == 13)
					{
						HeroInfo.GetInstance().MilitaryRankGift_Time = long.Parse(sCProfile.value);
						if (MilitaryRankGiftPanel._inst)
						{
							MilitaryRankGiftPanel._inst.SetGetMilitaryRankGiftTime();
						}
					}
					if (sCProfile.key == 8)
					{
						HeroInfo.GetInstance().IsBuyChengZhangJiJin = (int.Parse(sCProfile.value) == 1);
					}
					if (sCProfile.key == 15)
					{
						HeroInfo.GetInstance().TechGetRMB_Time = long.Parse(sCProfile.value);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10109);
				}
			}
		}
		catch (Exception var_76_13F4)
		{
		}
		try
		{
			if (dic.ContainsKey(10097))
			{
				for (int num18 = 0; num18 < dic[10097].Count; num18++)
				{
					SCActivityCountsData sCActivityCountsData = dic[10097][num18] as SCActivityCountsData;
					if (!HeroInfo.GetInstance().ActivitiesData_RecieveCountServer.ContainsKey(sCActivityCountsData.activityId))
					{
						HeroInfo.GetInstance().ActivitiesData_RecieveCountServer.Add(sCActivityCountsData.activityId, sCActivityCountsData);
					}
					else
					{
						HeroInfo.GetInstance().ActivitiesData_RecieveCountServer[sCActivityCountsData.activityId] = sCActivityCountsData;
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10097);
				}
			}
		}
		catch (Exception var_79_14B9)
		{
		}
		try
		{
			if (dic.ContainsKey(10103))
			{
				for (int num19 = 0; num19 < dic[10103].Count; num19++)
				{
					SCRanking sCRanking = dic[10103][num19] as SCRanking;
					if (!HeroInfo.GetInstance().ActivitiesData_Ranking.ContainsKey(sCRanking.type))
					{
						HeroInfo.GetInstance().ActivitiesData_Ranking.Add(sCRanking.type, sCRanking);
					}
					else
					{
						HeroInfo.GetInstance().ActivitiesData_Ranking[sCRanking.type] = sCRanking;
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10103);
				}
			}
		}
		catch (Exception var_82_157E)
		{
		}
		try
		{
			if (dic.ContainsKey(10096))
			{
				for (int num20 = 0; num20 < dic[10096].Count; num20++)
				{
					SCActivitiesData sCActivitiesData = dic[10096][num20] as SCActivitiesData;
					for (int num21 = 0; num21 < sCActivitiesData.counts.Count; num21++)
					{
						if (!HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey((int)sCActivitiesData.counts[num21].key))
						{
							HeroInfo.GetInstance().ActivitiesData_RecieveState.Add((int)sCActivitiesData.counts[num21].key, (int)sCActivitiesData.counts[num21].value);
						}
						else
						{
							HeroInfo.GetInstance().ActivitiesData_RecieveState[(int)sCActivitiesData.counts[num21].key] = (int)sCActivitiesData.counts[num21].value;
						}
					}
					for (int num22 = 0; num22 < sCActivitiesData.getPrizeCount.Count; num22++)
					{
						if (!HeroInfo.GetInstance().ActivitiesData_RecieveCount.ContainsKey((int)sCActivitiesData.getPrizeCount[num22].key))
						{
							HeroInfo.GetInstance().ActivitiesData_RecieveCount.Add((int)sCActivitiesData.getPrizeCount[num22].key, (int)sCActivitiesData.getPrizeCount[num22].value);
						}
						else
						{
							HeroInfo.GetInstance().ActivitiesData_RecieveCount[(int)sCActivitiesData.getPrizeCount[num22].key] = (int)sCActivitiesData.getPrizeCount[num22].value;
						}
					}
					HeroInfo.GetInstance().Activety_DayOfDay_HavedID = sCActivitiesData.todayTotalPayLastActId;
					HeroInfo.GetInstance().Activety_DayOfDay_HavedDatetime = TimeTools.ConvertLongDateTime(sCActivitiesData.todayTotalPayLastTime);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10096);
				}
			}
		}
		catch (Exception var_87_1792)
		{
		}
		try
		{
			if (dic.ContainsKey(10117))
			{
				for (int num23 = 0; num23 < dic[10117].Count; num23++)
				{
					SCGetActivitiesList sCGetActivitiesList = dic[10117][num23] as SCGetActivitiesList;
					if (sCGetActivitiesList.type == 1)
					{
						HeroInfo.GetInstance().activityClass.Clear();
					}
					else if (sCGetActivitiesList.type == 2)
					{
						HeroInfo.GetInstance().reChargeClass.Clear();
						HeroInfo.GetInstance().LotteryData = sCGetActivitiesList.lottery;
					}
					else if (sCGetActivitiesList.type == 3)
					{
						HeroInfo.GetInstance().ShouChongChargeClass.Clear();
					}
					else if (sCGetActivitiesList.type == 4)
					{
						HeroInfo.GetInstance().OneYuanGouChargeClass.Clear();
					}
					else if (sCGetActivitiesList.type == 6)
					{
						HeroInfo.GetInstance().BaseGiftClass.Clear();
					}
					for (int num24 = 0; num24 < sCGetActivitiesList.data.Count; num24++)
					{
						DesignActivityData designActivityData = sCGetActivitiesList.data[num24];
						ActivityClass activityClass = new ActivityClass();
						activityClass.type = designActivityData.type;
						activityClass.activityId = designActivityData.activityId;
						activityClass.activityType = designActivityData.activityType;
						activityClass.btnName = designActivityData.name;
						activityClass.startTimeStr = Convert.ToDateTime(designActivityData.startTimeStr);
						activityClass.endTimeStr = Convert.ToDateTime(designActivityData.endTimeStr);
						activityClass.showEndTimeStr = Convert.ToDateTime(designActivityData.showEndTimeStr);
						activityClass.rewardType = designActivityData.rewardType;
						activityClass.rewardCount = designActivityData.rewardCount;
						activityClass.conditionType = designActivityData.condition;
						activityClass.conditionID = designActivityData.conditionId;
						activityClass.conditionName = designActivityData.conditionName;
						activityClass.sort = designActivityData.sort;
						activityClass.repeatCount = designActivityData.repeatPrizeCount;
						activityClass.totalDiscription = designActivityData.totalDiscription;
						activityClass.tittleName = designActivityData.title;
						activityClass.shopID = designActivityData.toShopId;
						for (int num25 = 0; num25 < designActivityData.normalOption.resReward.Count; num25++)
						{
							activityClass.curActivityResReward.Add((ResType)designActivityData.normalOption.resReward[num25].key, (int)designActivityData.normalOption.resReward[num25].value);
						}
						for (int num26 = 0; num26 < designActivityData.normalOption.moneyReward.Count; num26++)
						{
							activityClass.curActivityResReward.Add((ResType)designActivityData.normalOption.moneyReward[num26].key, (int)designActivityData.normalOption.moneyReward[num26].value);
						}
						for (int num27 = 0; num27 < designActivityData.normalOption.itemReward.Count; num27++)
						{
							activityClass.curActivityItemReward.Add((int)designActivityData.normalOption.itemReward[num27].key, (int)designActivityData.normalOption.itemReward[num27].value);
						}
						for (int num28 = 0; num28 < designActivityData.normalOption.skillReward.Count; num28++)
						{
							activityClass.curActivitySkillReward.Add((int)designActivityData.normalOption.skillReward[num28].key, (int)designActivityData.normalOption.skillReward[num28].value);
						}
						for (int num29 = 0; num29 < designActivityData.specialOption.resReward.Count; num29++)
						{
							activityClass.totalActivityResReward.Add((ResType)designActivityData.specialOption.resReward[num29].key, (int)designActivityData.specialOption.resReward[num29].value);
						}
						for (int num30 = 0; num30 < designActivityData.specialOption.moneyReward.Count; num30++)
						{
							activityClass.totalActivityResReward.Add((ResType)designActivityData.specialOption.moneyReward[num30].key, (int)designActivityData.specialOption.moneyReward[num30].value);
						}
						for (int num31 = 0; num31 < designActivityData.specialOption.itemReward.Count; num31++)
						{
							activityClass.totalActivityItemReward.Add((int)designActivityData.specialOption.itemReward[num31].key, (int)designActivityData.specialOption.itemReward[num31].value);
						}
						for (int num32 = 0; num32 < designActivityData.specialOption.skillReward.Count; num32++)
						{
							activityClass.totalActivitySkillReward.Add((int)designActivityData.specialOption.skillReward[num32].key, (int)designActivityData.specialOption.skillReward[num32].value);
						}
						if (activityClass.type == 1)
						{
							if (HeroInfo.GetInstance().activityClass.ContainsKey(activityClass.activityType))
							{
								HeroInfo.GetInstance().activityClass[activityClass.activityType].Add(activityClass);
								HeroInfo.GetInstance().activityClass[activityClass.activityType] = (from a in HeroInfo.GetInstance().activityClass[activityClass.activityType]
								orderby a.sort
								select a).ToList<ActivityClass>();
							}
							else
							{
								HeroInfo.GetInstance().activityClass.Add(activityClass.activityType, new List<ActivityClass>
								{
									activityClass
								});
							}
						}
						else if (activityClass.type == 2)
						{
							if (HeroInfo.GetInstance().reChargeClass.ContainsKey(activityClass.activityType))
							{
								HeroInfo.GetInstance().reChargeClass[activityClass.activityType].Add(activityClass);
								HeroInfo.GetInstance().reChargeClass[activityClass.activityType] = (from a in HeroInfo.GetInstance().reChargeClass[activityClass.activityType]
								orderby a.sort
								select a).ToList<ActivityClass>();
							}
							else
							{
								HeroInfo.GetInstance().reChargeClass.Add(activityClass.activityType, new List<ActivityClass>
								{
									activityClass
								});
							}
						}
						else if (activityClass.type == 3)
						{
							if (HeroInfo.GetInstance().ShouChongChargeClass.ContainsKey(activityClass.activityType))
							{
								HeroInfo.GetInstance().ShouChongChargeClass[activityClass.activityType].Add(activityClass);
								HeroInfo.GetInstance().ShouChongChargeClass[activityClass.activityType] = (from a in HeroInfo.GetInstance().ShouChongChargeClass[activityClass.activityType]
								orderby a.sort
								select a).ToList<ActivityClass>();
							}
							else
							{
								HeroInfo.GetInstance().ShouChongChargeClass.Add(activityClass.activityType, new List<ActivityClass>
								{
									activityClass
								});
							}
						}
						else if (activityClass.type == 4)
						{
							if (HeroInfo.GetInstance().OneYuanGouChargeClass.ContainsKey(activityClass.activityType))
							{
								HeroInfo.GetInstance().OneYuanGouChargeClass[activityClass.activityType].Add(activityClass);
								HeroInfo.GetInstance().OneYuanGouChargeClass[activityClass.activityType] = (from a in HeroInfo.GetInstance().OneYuanGouChargeClass[activityClass.activityType]
								orderby a.sort
								select a).ToList<ActivityClass>();
							}
							else
							{
								HeroInfo.GetInstance().OneYuanGouChargeClass.Add(activityClass.activityType, new List<ActivityClass>
								{
									activityClass
								});
							}
						}
						else if (activityClass.type == 6)
						{
							if (HeroInfo.GetInstance().BaseGiftClass.ContainsKey(activityClass.activityType))
							{
								HeroInfo.GetInstance().BaseGiftClass[activityClass.activityType].Add(activityClass);
								HeroInfo.GetInstance().BaseGiftClass[activityClass.activityType] = (from a in HeroInfo.GetInstance().BaseGiftClass[activityClass.activityType]
								orderby a.conditionID
								select a).ToList<ActivityClass>();
							}
							else
							{
								HeroInfo.GetInstance().BaseGiftClass.Add(activityClass.activityType, new List<ActivityClass>
								{
									activityClass
								});
							}
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10117);
				}
			}
		}
		catch (Exception var_101_2118)
		{
		}
		try
		{
			if (dic.ContainsKey(10003))
			{
				List<SCPlayerResource> list = opcode.Get<SCPlayerResource>(10003);
				for (int num33 = 0; num33 < list.Count; num33++)
				{
					SCPlayerResource sCPlayerResource = list[num33];
					if (sCPlayerResource.type == 1)
					{
						switch (sCPlayerResource.resId)
						{
						case 1:
							if (sCPlayerResource.resVal != HeroInfo.GetInstance().playerRes.resCoin && this.CoinChange != null)
							{
								HeroInfo.GetInstance().playerRes.resCoin = sCPlayerResource.resVal;
								this.CoinChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().playerRes.resCoin = sCPlayerResource.resVal;
							break;
						case 2:
							if (sCPlayerResource.resVal != HeroInfo.GetInstance().playerRes.resOil && this.OilChange != null)
							{
								HeroInfo.GetInstance().playerRes.resOil = sCPlayerResource.resVal;
								this.OilChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().playerRes.resOil = sCPlayerResource.resVal;
							break;
						case 3:
							if (sCPlayerResource.resVal != HeroInfo.GetInstance().playerRes.resSteel && this.SteelChange != null)
							{
								HeroInfo.GetInstance().playerRes.resSteel = sCPlayerResource.resVal;
								this.SteelChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().playerRes.resSteel = sCPlayerResource.resVal;
							break;
						case 4:
							if (sCPlayerResource.resVal != HeroInfo.GetInstance().playerRes.resRareEarth && this.RareEarthChange != null)
							{
								HeroInfo.GetInstance().playerRes.resRareEarth = sCPlayerResource.resVal;
								this.RareEarthChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().playerRes.resRareEarth = sCPlayerResource.resVal;
							break;
						case 5:
							HeroInfo.GetInstance().playerRes.skillPoint = sCPlayerResource.resVal;
							break;
						case 7:
							if (HeroInfo.GetInstance().playerRes.RMBCoin < sCPlayerResource.resVal)
							{
								HeroInfo.GetInstance().playerRes.RMBCoin = sCPlayerResource.resVal;
								if (this.RMBChange != null)
								{
									this.RMBChange(sCPlayerResource.resVal - HeroInfo.GetInstance().playerRes.RMBCoin);
								}
							}
							HeroInfo.GetInstance().playerRes.RMBCoin = sCPlayerResource.resVal;
							break;
						case 9:
							HeroInfo.GetInstance().playerRes.playerExp = sCPlayerResource.resVal;
							break;
						case 10:
							if (HeroInfo.GetInstance().playerlevel != 0 && HeroInfo.GetInstance().playerlevel != sCPlayerResource.resVal && sCPlayerResource.resVal != 1 && this.LevelChange != null)
							{
								this.LevelChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().playerlevel = sCPlayerResource.resVal;
							break;
						case 11:
							HeroInfo.GetInstance().playerRes.playermedal = sCPlayerResource.resVal;
							break;
						case 12:
							HeroInfo.GetInstance().playerRes.junLing = sCPlayerResource.resVal;
							break;
						case 15:
							HeroInfo.GetInstance().playerRes.tanSuoLing = sCPlayerResource.resVal;
							break;
						case 16:
							HeroInfo.GetInstance().playerRes.skillDebris = sCPlayerResource.resVal;
							break;
						case 20:
							HeroInfo.GetInstance().MyLegionBattleData.Vit = sCPlayerResource.resVal;
							break;
						case 21:
							HeroInfo.GetInstance().LotteryDataFreeTimes = sCPlayerResource.resVal;
							break;
						}
					}
					else if (sCPlayerResource.type == 2)
					{
						LogManage.LogError(((ResType)sCPlayerResource.resId).ToString());
						LogManage.LogError(string.Format("获得资源(增量)：{0}   数目:{1}", ((ResType)sCPlayerResource.resId).ToString(), sCPlayerResource.resVal));
						HeroInfo.GetInstance().ClearAddAndSumData();
						switch (sCPlayerResource.resId)
						{
						case 1:
							if (this.CoinAdd != null)
							{
								this.CoinAdd(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 2:
							if (this.OilAdd != null)
							{
								this.OilAdd(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 3:
							if (this.SteelAdd != null)
							{
								this.SteelAdd(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 4:
							if (this.RareEarthAdd != null)
							{
								this.RareEarthAdd(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 7:
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 9:
							if (this.ExpChange != null)
							{
								this.ExpChange(sCPlayerResource.resVal);
							}
							HeroInfo.GetInstance().addExp = sCPlayerResource.resVal;
							HeroInfo.GetInstance().AddResources(sCPlayerResource);
							break;
						case 11:
							HeroInfo.GetInstance().addMedal = sCPlayerResource.resVal;
							break;
						}
					}
					else
					{
						HeroInfo.GetInstance().ClearAddAndSumData();
						HeroInfo.GetInstance().subResouce.Add(sCPlayerResource);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10003);
				}
			}
		}
		catch (Exception var_105_2737)
		{
		}
		try
		{
			if (dic.ContainsKey(10104))
			{
				HeroInfo.GetInstance().armyGroupDataData.armyInfo.Clear();
				for (int num34 = 0; num34 < dic[10104].Count; num34++)
				{
					ArmyGroupInfo armyGroupInfo = new ArmyGroupInfo();
					SCLegionData sCLegionData = dic[10104][num34] as SCLegionData;
					armyGroupInfo.armyId = (int)sCLegionData.legionId;
					armyGroupInfo.armyName = sCLegionData.name;
					armyGroupInfo.logoId = sCLegionData.logo;
					armyGroupInfo.needMinMedal = sCLegionData.needMinMedal;
					armyGroupInfo.openType = sCLegionData.openType;
					armyGroupInfo.armyLevel = sCLegionData.level;
					armyGroupInfo.memeberCount = sCLegionData.memberCount;
					armyGroupInfo.armyExp = sCLegionData.exp;
					armyGroupInfo.rank = sCLegionData.rank;
					armyGroupInfo.medal = sCLegionData.medal;
					armyGroupInfo.level = sCLegionData.level;
					armyGroupInfo.notic = sCLegionData.notice;
					armyGroupInfo.memberLimit = sCLegionData.memberLimit;
					HeroInfo.GetInstance().armyGroupDataData.armyInfo.Add(armyGroupInfo.armyId, armyGroupInfo);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10104);
				}
			}
		}
		catch (Exception var_109_289B)
		{
		}
		try
		{
			if (dic.ContainsKey(10112))
			{
				for (int num35 = 0; num35 < dic[10112].Count; num35++)
				{
					ArmyPeopleInfo armyPeopleInfo = new ArmyPeopleInfo();
					SCLegionMember sCLegionMember = dic[10112][num35] as SCLegionMember;
					armyPeopleInfo.playerId = (int)sCLegionMember.id;
					if ((long)armyPeopleInfo.playerId == HeroInfo.GetInstance().userId)
					{
						HeroInfo.GetInstance().playerGroupJob = sCLegionMember.job;
					}
					armyPeopleInfo.job = sCLegionMember.job;
					armyPeopleInfo.name = sCLegionMember.name;
					armyPeopleInfo.selfMedal = sCLegionMember.medal;
					armyPeopleInfo.contriBution = sCLegionMember.contribution;
					armyPeopleInfo.legionId = (int)sCLegionMember.legionId;
					HeroInfo.GetInstance().armyGroupDataData.armyPeopleInfo[armyPeopleInfo.playerId] = armyPeopleInfo;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10112);
				}
			}
		}
		catch (Exception var_113_29B2)
		{
		}
		try
		{
			if (dic.ContainsKey(10105))
			{
				HeroInfo.GetInstance().armyGroupDataData.armySubmitDic.Clear();
				for (int num36 = 0; num36 < dic[10105].Count; num36++)
				{
					ArmySubmintInfo armySubmintInfo = new ArmySubmintInfo();
					SCLegionApply sCLegionApply = dic[10105][num36] as SCLegionApply;
					armySubmintInfo.id = (int)sCLegionApply.id;
					armySubmintInfo.playerId = (int)sCLegionApply.playerId;
					armySubmintInfo.legionId = (int)sCLegionApply.id;
					armySubmintInfo.level = sCLegionApply.level;
					armySubmintInfo.playerName = sCLegionApply.playerName;
					armySubmintInfo.medal = sCLegionApply.medal;
					HeroInfo.GetInstance().armyGroupDataData.armySubmitDic[armySubmintInfo.id] = armySubmintInfo;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10105);
				}
			}
		}
		catch (Exception var_117_2AB6)
		{
		}
		try
		{
			if (dic.ContainsKey(10107))
			{
				HeroInfo.GetInstance().armyGroupDataData.searchDic.Clear();
				for (int num37 = 0; num37 < dic[10107].Count; num37++)
				{
					SearchArmyInfo searchArmyInfo = new SearchArmyInfo();
					SCSearchLegionData sCSearchLegionData = dic[10107][num37] as SCSearchLegionData;
					searchArmyInfo.legionId = (int)sCSearchLegionData.legionId;
					searchArmyInfo.name = sCSearchLegionData.name;
					searchArmyInfo.level = sCSearchLegionData.level;
					searchArmyInfo.count = sCSearchLegionData.count;
					searchArmyInfo.limit = sCSearchLegionData.limit;
					searchArmyInfo.minMedal = sCSearchLegionData.minMedal;
					searchArmyInfo.rank = sCSearchLegionData.rank;
					searchArmyInfo.scroe = sCSearchLegionData.score;
					HeroInfo.GetInstance().armyGroupDataData.searchDic.Add(searchArmyInfo.legionId, searchArmyInfo);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10107);
				}
			}
		}
		catch (Exception var_121_2BD4)
		{
		}
		try
		{
			if (dic.ContainsKey(10120))
			{
				for (int num38 = 0; num38 < dic[10120].Count; num38++)
				{
					SCLegionPVE sCLegionPVE = dic[10120][num38] as SCLegionPVE;
					HeroInfo.GetInstance().MyLegionBattleData.id = (int)sCLegionPVE.id;
					HeroInfo.GetInstance().MyLegionBattleData.NowBattleId = sCLegionPVE.curBattleId;
					HeroInfo.GetInstance().MyLegionBattleData.NowBattleProgress = sCLegionPVE.times;
					HeroInfo.GetInstance().MyLegionBattleData.BattleRefreshTime = sCLegionPVE.flushTimes;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10120);
				}
			}
		}
		catch (Exception var_124_2CA2)
		{
		}
		try
		{
			if (dic.ContainsKey(10041))
			{
				for (int num39 = 0; num39 < dic[10041].Count; num39++)
				{
					SCPlayerItem sCPlayerItem = dic[10041][num39] as SCPlayerItem;
					if (sCPlayerItem.type == 1)
					{
						if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(sCPlayerItem.itemId))
						{
							HeroInfo.GetInstance().PlayerItemInfo[sCPlayerItem.itemId] = (int)sCPlayerItem.value;
						}
						else
						{
							HeroInfo.GetInstance().PlayerItemInfo.Add(sCPlayerItem.itemId, (int)sCPlayerItem.value);
						}
					}
					else if (sCPlayerItem.type == 2)
					{
						HeroInfo.GetInstance().ClearAddAndSumData();
						HeroInfo.GetInstance().AddItem(sCPlayerItem);
					}
					else
					{
						HeroInfo.GetInstance().ClearAddAndSumData();
						HeroInfo.GetInstance().subItem.Add(new KVStruct_Client(sCPlayerItem.itemId, (int)sCPlayerItem.value));
					}
				}
				if (this.ItemAddChange != null)
				{
					this.ItemAddChange(HeroInfo.GetInstance().addItem);
				}
				if (this.ItemSubChange != null)
				{
					this.ItemSubChange(HeroInfo.GetInstance().subItem);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10041);
				}
			}
		}
		catch (Exception var_127_2E1A)
		{
		}
		try
		{
			if (dic.ContainsKey(10042))
			{
				int[] array8 = UnitConst.GetInstance().DailyTask.Keys.ToArray<int>();
				for (int num40 = 0; num40 < array8.Length; num40++)
				{
					UnitConst.GetInstance().DailyTask[array8[num40]].StepRecord = 0;
					UnitConst.GetInstance().DailyTask[array8[num40]].isReceived = false;
					UnitConst.GetInstance().DailyTask[array8[num40]].isUIShow = false;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10042);
				}
			}
		}
		catch (Exception var_130_2ECE)
		{
		}
		try
		{
			if (dic.ContainsKey(10037))
			{
				for (int num41 = 0; num41 < dic[10037].Count; num41++)
				{
					SCPlayerTaskData sCPlayerTaskData = dic[10037][num41] as SCPlayerTaskData;
					if (UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id].type == 0 && !sCPlayerTaskData.status && this.TaskNotice != null)
					{
						this.TaskNotice(UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id]);
					}
					UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id].isUIShow = true;
					UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id].isTips = true;
					UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id].StepRecord = sCPlayerTaskData.count;
					UnitConst.GetInstance().DailyTask[(int)sCPlayerTaskData.id].isReceived = sCPlayerTaskData.status;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10037);
				}
			}
		}
		catch (Exception var_133_301D)
		{
		}
		try
		{
			if (dic.ContainsKey(10044))
			{
				for (int num42 = 0; num42 < dic[10044].Count; num42++)
				{
					SCAchievementData sCAchievementData = dic[10044][num42] as SCAchievementData;
					UnitConst.GetInstance().AllAchievementConst[(int)sCAchievementData.id].stepRecord = (int)sCAchievementData.count;
					UnitConst.GetInstance().AllAchievementConst[(int)sCAchievementData.id].lastStar = sCAchievementData.lastStar;
					if (sCAchievementData.lastStar < 5 && UnitConst.GetInstance().AllAchievementConst[(int)sCAchievementData.id].stepRecord >= UnitConst.GetInstance().AllAchievementConst[(int)sCAchievementData.id].step.ToArray()[sCAchievementData.lastStar] && this.AchieveNotice != null)
					{
						this.AchieveNotice();
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10044);
				}
			}
		}
		catch (Exception var_136_3146)
		{
		}
		try
		{
			if (dic.ContainsKey(10045))
			{
				for (int num43 = 0; num43 < dic[10045].Count; num43++)
				{
					SCTodayBuyResData sCTodayBuyResData = dic[10045][num43] as SCTodayBuyResData;
					int num44 = 0;
					while (num44 < sCTodayBuyResData.value.Count)
					{
						KVStruct kVStruct = sCTodayBuyResData.value[num44];
						long key2 = kVStruct.key;
						if (key2 >= 1L && key2 <= 4L)
						{
							switch ((int)(key2 - 1L))
							{
							case 0:
								ResourceMgr.totalCoinChanged = (int)kVStruct.value;
								break;
							case 1:
								ResourceMgr.totalOilChanged = (int)kVStruct.value;
								break;
							case 2:
								ResourceMgr.totalSteelChanged = (int)kVStruct.value;
								break;
							case 3:
								ResourceMgr.totalRareEarthChanged = (int)kVStruct.value;
								break;
							}
						}
						IL_3226:
						num44++;
						continue;
						goto IL_3226;
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10045);
				}
			}
		}
		catch (Exception var_141_327C)
		{
		}
		try
		{
			if (dic.ContainsKey(10089))
			{
				for (int num45 = 0; num45 < dic[10089].Count; num45++)
				{
					SCPlayerSoldier sCPlayerSoldier = dic[10089][num45] as SCPlayerSoldier;
					PlayerCommando playerCommando;
					if (HeroInfo.GetInstance().PlayerCommandoes.ContainsKey(sCPlayerSoldier.itemId))
					{
						playerCommando = HeroInfo.GetInstance().PlayerCommandoes[sCPlayerSoldier.itemId];
					}
					else
					{
						playerCommando = new PlayerCommando();
						HeroInfo.GetInstance().PlayerCommandoes.Add(sCPlayerSoldier.itemId, playerCommando);
					}
					playerCommando.id = sCPlayerSoldier.id;
					playerCommando.exp = sCPlayerSoldier.exp;
					playerCommando.index = sCPlayerSoldier.itemId;
					playerCommando.level = sCPlayerSoldier.level;
					playerCommando.skillLevel = sCPlayerSoldier.skillLevel;
					playerCommando.star = sCPlayerSoldier.star;
					SettlementManager.soliderId = sCPlayerSoldier.itemId;
					SettlementManager.isHave = true;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10089);
				}
			}
			if (dic.ContainsKey(10090))
			{
				for (int num46 = 0; num46 < dic[10090].Count; num46++)
				{
					SCSoldierConfigure playerCommandoFuncingOrEnd = dic[10090][num46] as SCSoldierConfigure;
					HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd = playerCommandoFuncingOrEnd;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10090);
				}
			}
		}
		catch (Exception var_147_3423)
		{
		}
		try
		{
			if (dic.ContainsKey(30101))
			{
				for (int num47 = 0; num47 < dic[30101].Count; num47++)
				{
					HeroInfo.GetInstance().chatMessage.ChatData.Add(dic[30101][num47] as SCSendMessage);
					HeroInfo.GetInstance().chatMessage.ChatTempData.Add(dic[30101][num47] as SCSendMessage);
				}
				if (this.DataChange != null)
				{
					this.DataChange(30101);
				}
			}
		}
		catch (Exception var_149_34D5)
		{
		}
		try
		{
			if (dic.ContainsKey(10110))
			{
				for (int num48 = 0; num48 < dic[10110].Count; num48++)
				{
					SCLegionHelpApply sCLegionHelpApply = dic[10110][num48] as SCLegionHelpApply;
					LegionHelpApply legionHelpApply = new LegionHelpApply();
					legionHelpApply.id = sCLegionHelpApply.id;
					legionHelpApply.time = TimeTools.ConvertLongDateTime(sCLegionHelpApply.time);
					legionHelpApply.cdTime = TimeTools.ConvertLongDateTime(sCLegionHelpApply.cdTime);
					legionHelpApply.userId = sCLegionHelpApply.userId;
					legionHelpApply.buildingId = sCLegionHelpApply.buildingId;
					legionHelpApply.userName = sCLegionHelpApply.userName;
					legionHelpApply.buildingIndex = sCLegionHelpApply.buildingIndex;
					if (sCLegionHelpApply.helpers.Count > 0)
					{
						for (int num49 = 0; num49 < sCLegionHelpApply.helpers.Count; num49++)
						{
							legionHelpApply.helper[sCLegionHelpApply.helpers[num49]] = sCLegionHelpApply.helpers[num49];
						}
					}
					HeroInfo.GetInstance().legionApply[legionHelpApply.id] = legionHelpApply;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10110);
				}
			}
		}
		catch (Exception var_154_3630)
		{
		}
		try
		{
			if (dic.ContainsKey(10051))
			{
				HeroInfo.GetInstance().islandResData.IslandResTake.Clear();
				HeroInfo.GetInstance().islandResData.IslandResTmp.Clear();
				for (int num50 = 0; num50 < dic[10051].Count; num50++)
				{
					SCResIslandTake sCResIslandTake = dic[10051][num50] as SCResIslandTake;
					for (int num51 = 0; num51 < sCResIslandTake.takeTime.Count; num51++)
					{
						HeroInfo.GetInstance().islandResData.IslandResTake.Add((ResType)sCResIslandTake.takeTime[num51].key, TimeTools.ConvertLongDateTime(sCResIslandTake.takeTime[num51].value));
					}
					for (int num52 = 0; num52 < sCResIslandTake.resTmp.Count; num52++)
					{
						HeroInfo.GetInstance().islandResData.IslandResTmp.Add((ResType)sCResIslandTake.resTmp[num52].key, (int)sCResIslandTake.resTmp[num52].value);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10051);
				}
			}
		}
		catch (Exception var_159_378B)
		{
		}
		try
		{
			if (dic.ContainsKey(10013))
			{
				LogManage.Log("据点变化数据下传");
				for (int num53 = 0; num53 < dic[10013].Count; num53++)
				{
					SCMapIndexNotice sCMapIndexNotice = dic[10013][num53] as SCMapIndexNotice;
					foreach (KVStruct current3 in sCMapIndexNotice.index)
					{
						if (!T_WMap.newWapIndex.ContainsKey((int)current3.key))
						{
							T_WMap.newWapIndex.Add((int)current3.key, (int)current3.value);
						}
						else
						{
							T_WMap.newWapIndex[(int)current3.key] = (int)current3.value;
						}
					}
					HUDTextTool.WorldMapRedNotice = sCMapIndexNotice.tips;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10013);
				}
			}
		}
		catch (Exception var_164_38A0)
		{
		}
		try
		{
			if (dic.ContainsKey(10012))
			{
				HeroInfo.GetInstance().islandResData.IslandResNum.Clear();
				int num54 = 0;
				while (num54 < dic[10012].Count)
				{
					SCMapEntity sCMapEntity = dic[10012][num54] as SCMapEntity;
					PlayerWMapData playerWMapData;
					if (!HeroInfo.GetInstance().worldMapInfo.playerWMap.ContainsKey(sCMapEntity.index))
					{
						playerWMapData = new PlayerWMapData();
						HeroInfo.GetInstance().worldMapInfo.playerWMap.Add(sCMapEntity.index, playerWMapData);
					}
					else
					{
						playerWMapData = HeroInfo.GetInstance().worldMapInfo.playerWMap[sCMapEntity.index];
					}
					playerWMapData.worldMapId = sCMapEntity.worldMapId;
					playerWMapData.islandId = sCMapEntity.islandId;
					playerWMapData.idx = sCMapEntity.index;
					playerWMapData.ownerType = sCMapEntity.ownerType;
					playerWMapData.ownerId = sCMapEntity.ownerId.ToString();
					playerWMapData.ownerName = sCMapEntity.ownerName;
					playerWMapData.ownerLv = sCMapEntity.ownerLevel.ToString();
					playerWMapData.commandlv = sCMapEntity.commondLevel;
					playerWMapData.replace = sCMapEntity.refresh;
					playerWMapData.reward.Clear();
					for (int num55 = 0; num55 < sCMapEntity.res.Count; num55++)
					{
						SCPlayerResource sCPlayerResource2 = sCMapEntity.res[num55];
						switch ((int)sCPlayerResource2.id)
						{
						case 1:
							if (playerWMapData.reward.ContainsKey(ResType.金币))
							{
								playerWMapData.reward[ResType.金币] = sCPlayerResource2.resVal;
							}
							else
							{
								playerWMapData.reward.Add(ResType.金币, sCPlayerResource2.resVal);
							}
							break;
						case 2:
							if (playerWMapData.reward.ContainsKey(ResType.石油))
							{
								playerWMapData.reward[ResType.石油] = sCPlayerResource2.resVal;
							}
							else
							{
								playerWMapData.reward.Add(ResType.石油, sCPlayerResource2.resVal);
							}
							break;
						case 3:
							if (playerWMapData.reward.ContainsKey(ResType.钢铁))
							{
								playerWMapData.reward[ResType.稀矿] = sCPlayerResource2.resVal;
							}
							else
							{
								playerWMapData.reward.Add(ResType.钢铁, sCPlayerResource2.resVal);
							}
							break;
						case 4:
							if (playerWMapData.reward.ContainsKey(ResType.稀矿))
							{
								playerWMapData.reward[ResType.稀矿] = sCPlayerResource2.resVal;
							}
							else
							{
								playerWMapData.reward.Add(ResType.稀矿, sCPlayerResource2.resVal);
							}
							break;
						}
					}
					switch (T_WMap.IdxGetMapType(sCMapEntity.index))
					{
					case IslandType.oil:
						if (sCMapEntity.ownerType == 1 && sCMapEntity.id != (long)HeroInfo.GetInstance().homeInWMapIdx)
						{
							if (!HeroInfo.GetInstance().islandResData.OilIslandes.ContainsKey(sCMapEntity.islandId))
							{
								HeroInfo.GetInstance().islandResData.OilIslandes.Add(sCMapEntity.islandId, sCMapEntity.commondLevel);
							}
							else
							{
								HeroInfo.GetInstance().islandResData.OilIslandes[sCMapEntity.islandId] = sCMapEntity.commondLevel;
							}
						}
						else if (HeroInfo.GetInstance().islandResData.OilIslandes.ContainsKey(sCMapEntity.islandId))
						{
							HeroInfo.GetInstance().islandResData.OilIslandes.Remove(sCMapEntity.islandId);
						}
						break;
					case IslandType.steel:
						if (sCMapEntity.ownerType == 1 && sCMapEntity.id != (long)HeroInfo.GetInstance().homeInWMapIdx)
						{
							if (!HeroInfo.GetInstance().islandResData.SteelIslandes.ContainsKey(sCMapEntity.islandId))
							{
								HeroInfo.GetInstance().islandResData.SteelIslandes.Add(sCMapEntity.islandId, sCMapEntity.commondLevel);
							}
							else
							{
								HeroInfo.GetInstance().islandResData.SteelIslandes[sCMapEntity.islandId] = sCMapEntity.commondLevel;
							}
						}
						else if (HeroInfo.GetInstance().islandResData.SteelIslandes.ContainsKey(sCMapEntity.islandId))
						{
							HeroInfo.GetInstance().islandResData.SteelIslandes.Remove(sCMapEntity.islandId);
						}
						break;
					case IslandType.rareEarth:
						if (sCMapEntity.ownerType == 1 && sCMapEntity.id != (long)HeroInfo.GetInstance().homeInWMapIdx)
						{
							if (!HeroInfo.GetInstance().islandResData.RareEarthIslandes.ContainsKey(sCMapEntity.islandId))
							{
								HeroInfo.GetInstance().islandResData.RareEarthIslandes.Add(sCMapEntity.islandId, sCMapEntity.commondLevel);
							}
							else
							{
								HeroInfo.GetInstance().islandResData.RareEarthIslandes[sCMapEntity.islandId] = sCMapEntity.commondLevel;
							}
						}
						else if (HeroInfo.GetInstance().islandResData.RareEarthIslandes.ContainsKey(sCMapEntity.islandId))
						{
							HeroInfo.GetInstance().islandResData.RareEarthIslandes.Remove(sCMapEntity.islandId);
						}
						break;
					case IslandType.npc:
						goto IL_3EE3;
					case IslandType.Coin:
						if (sCMapEntity.ownerType == 1 && sCMapEntity.id != (long)HeroInfo.GetInstance().homeInWMapIdx)
						{
							if (!HeroInfo.GetInstance().islandResData.CoinIslandes.ContainsKey(sCMapEntity.islandId))
							{
								HeroInfo.GetInstance().islandResData.CoinIslandes.Add(sCMapEntity.islandId, sCMapEntity.commondLevel);
							}
							else
							{
								HeroInfo.GetInstance().islandResData.CoinIslandes[sCMapEntity.islandId] = sCMapEntity.commondLevel;
							}
						}
						else if (HeroInfo.GetInstance().islandResData.CoinIslandes.ContainsKey(sCMapEntity.islandId))
						{
							HeroInfo.GetInstance().islandResData.CoinIslandes.Remove(sCMapEntity.islandId);
						}
						break;
					default:
						goto IL_3EE3;
					}
					IL_3F72:
					num54++;
					continue;
					IL_3EE3:
					if (sCMapEntity.ownerType == 1 && sCMapEntity.id != (long)HeroInfo.GetInstance().homeInWMapIdx)
					{
						if (!HeroInfo.GetInstance().islandResData.IslandResNum.ContainsKey(ResType.金币))
						{
							HeroInfo.GetInstance().islandResData.IslandResNum.Add(ResType.金币, 0);
						}
						Dictionary<ResType, int> islandResNum;
						Dictionary<ResType, int> expr_3F46 = islandResNum = HeroInfo.GetInstance().islandResData.IslandResNum;
						ResType key3;
						ResType expr_3F4C = key3 = ResType.金币;
						int num56 = islandResNum[key3];
						expr_3F46[expr_3F4C] = num56 + 1;
					}
					goto IL_3F72;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10012);
				}
			}
		}
		catch (Exception var_170_3FAF)
		{
		}
		try
		{
			if (dic.ContainsKey(10054))
			{
				for (int num57 = 0; num57 < dic[10054].Count; num57++)
				{
					SCPlayerVipData sCPlayerVipData = dic[10054][num57] as SCPlayerVipData;
					HeroInfo.GetInstance().vipData.superCard = sCPlayerVipData.superCard;
					HeroInfo.GetInstance().vipData.cardEndTime = sCPlayerVipData.cardEndTime;
					HeroInfo.GetInstance().vipData.cardPrizeTime = sCPlayerVipData.cardPrizeTime;
					HeroInfo.GetInstance().vipData.scardPrizeTime = sCPlayerVipData.scardPrizeTime;
					HeroInfo.GetInstance().vipData.vipPrizeTime = sCPlayerVipData.vipPrizeTime;
					HeroInfo.GetInstance().vipData.money_Buyed = sCPlayerVipData.money;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10054);
				}
			}
		}
		catch (Exception var_173_40A8)
		{
		}
		try
		{
			if (dic.ContainsKey(10055))
			{
				for (int num58 = 0; num58 < dic[10055].Count; num58++)
				{
					SCPlayerMailData sCPlayerMailData = dic[10055][num58] as SCPlayerMailData;
					EmailInfo emailInfo = null;
					for (int num59 = 0; num59 < EmailManager.GetIns().emailList.Count; num59++)
					{
						if (EmailManager.GetIns().emailList[num59].id == sCPlayerMailData.id)
						{
							emailInfo = EmailManager.GetIns().emailList[num59];
							break;
						}
					}
					if (emailInfo == null)
					{
						emailInfo = new EmailInfo();
						EmailManager.GetIns().emailList.Add(emailInfo);
					}
					emailInfo.id = sCPlayerMailData.id;
					emailInfo.isOpened = sCPlayerMailData.isRead;
					emailInfo.isGetReward = sCPlayerMailData.isReceived;
					emailInfo.content = sCPlayerMailData.content;
					emailInfo.time = sCPlayerMailData.time;
					emailInfo.title = sCPlayerMailData.title;
					emailInfo.zuanshiNum = sCPlayerMailData.money;
					emailInfo.items = sCPlayerMailData.items;
					emailInfo.resources = sCPlayerMailData.resources;
				}
				EmailManager.GetIns().SortEmail();
				EmailManager.GetIns().OnEmailChange();
				if (this.DataChange != null)
				{
					this.DataChange(10055);
				}
			}
		}
		catch (Exception var_178_422F)
		{
		}
		try
		{
			if (dic.ContainsKey(10010))
			{
				for (int num60 = 0; num60 < dic[10010].Count; num60++)
				{
					SCArmyData sCArmyData = dic[10010][num60] as SCArmyData;
					if (!HeroInfo.GetInstance().PlayerArmyData.ContainsKey((int)sCArmyData.id))
					{
						HeroInfo.GetInstance().PlayerArmyData.Add((int)sCArmyData.id, new ArmyInfo((int)sCArmyData.id));
					}
					HeroInfo.GetInstance().PlayerArmyData[(int)sCArmyData.id].level = sCArmyData.level;
					HeroInfo.GetInstance().PlayerArmyData[(int)sCArmyData.id].starLevel = sCArmyData.starLevel;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10010);
				}
			}
		}
		catch (Exception var_181_432A)
		{
		}
		try
		{
			if (dic.ContainsKey(10091))
			{
				HeroInfo.GetInstance().PlayerArmy_LandDataCDTime.Clear();
				HeroInfo.GetInstance().PlayerArmy_AirDataCDTime.Clear();
				for (int num61 = 0; num61 < dic[10091].Count; num61++)
				{
					SCArmyCDTimes sCArmyCDTimes = dic[10091][num61] as SCArmyCDTimes;
					foreach (KVStruct current4 in sCArmyCDTimes.cdTimes)
					{
						if (UnitConst.GetInstance().soldierConst[(int)(checked((IntPtr)current4.key))].isCanFly)
						{
							HeroInfo.GetInstance().PlayerArmy_AirDataCDTime.Add(current4);
						}
						else
						{
							HeroInfo.GetInstance().PlayerArmy_LandDataCDTime.Add(current4);
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10091);
				}
			}
		}
		catch (Exception var_186_443B)
		{
		}
		try
		{
			if (dic.ContainsKey(10014))
			{
				for (int num62 = 0; num62 < dic[10014].Count; num62++)
				{
					SenceInfo.SpyPlayerInfo = (dic[10014][num62] as SCSpyIsland);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10014);
				}
			}
		}
		catch (Exception var_188_44B3)
		{
		}
		try
		{
			if (dic.ContainsKey(10057))
			{
				ArmsDealerPanelData.ArmsDealers_Net.Clear();
				for (int num63 = 0; num63 < dic[10057].Count; num63++)
				{
					SCArmsDealerData sCArmsDealerData = dic[10057][num63] as SCArmsDealerData;
					ArmsDealerPanelData.nextRefreshTime = TimeTools.ConvertLongDateTime(sCArmsDealerData.nextRefreshTime);
					ArmsDealerPanelData.useMoneyRefreshTimes = sCArmsDealerData.useMoneyRefreshTimes;
					ArmsDealerPanelData.lastRefreshTimeWithMoney = (int)sCArmsDealerData.lastRefreshTimeWithMoney;
					for (int num64 = 0; num64 < sCArmsDealerData.armsIds.Count; num64++)
					{
						UnitConst.GetInstance().ArmsDealerConst[sCArmsDealerData.armsIds[num64]].isSelled = sCArmsDealerData.isSelled[num64];
						ArmsDealerPanelData.ArmsDealers_Net.Add(UnitConst.GetInstance().ArmsDealerConst[sCArmsDealerData.armsIds[num64]]);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10057);
				}
			}
		}
		catch (Exception var_192_45D4)
		{
		}
		try
		{
			if (dic.ContainsKey(10017))
			{
				for (int num65 = 0; num65 < dic[10017].Count; num65++)
				{
					SCWarBattleData sCWarBattleData = dic[10017][num65] as SCWarBattleData;
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].ID = sCWarBattleData.id;
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].prizes.Clear();
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].prizes.AddRange(sCWarBattleData.starPrize);
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].ClearBattleFightRecord();
					for (int num66 = 0; num66 < sCWarBattleData.battlefields.Count; num66++)
					{
						KVStruct kVStruct2 = sCWarBattleData.battlefields[num66];
						if ((int)kVStruct2.value > 0)
						{
							UnitConst.GetInstance().BattleFieldConst[(int)kVStruct2.key].fightRecord.isFight = true;
						}
						if (sCWarBattleData.completeIds.Contains(UnitConst.GetInstance().BattleFieldConst[(int)kVStruct2.key].id))
						{
							UnitConst.GetInstance().BattleFieldConst[(int)kVStruct2.key].isCompleteUI = true;
						}
						else
						{
							UnitConst.GetInstance().BattleFieldConst[(int)kVStruct2.key].isCompleteUI = false;
						}
						UnitConst.GetInstance().BattleFieldConst[(int)kVStruct2.key].fightRecord.star = (int)kVStruct2.value;
					}
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].EndBattleBoxTime = sCWarBattleData.cdTime;
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].isCanSweep = (sCWarBattleData.isSweep == 1);
					UnitConst.GetInstance().BattleConst[sCWarBattleData.battleId].battleBox = sCWarBattleData.boxId;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10017);
				}
			}
		}
		catch (Exception var_197_482C)
		{
		}
		try
		{
			if (dic.ContainsKey(10058))
			{
				ActivityManager.GetIns().curActData = (dic[10058][0] as SCActivityData);
				if (this.DataChange != null)
				{
					this.DataChange(10058);
				}
			}
		}
		catch (Exception var_198_4883)
		{
		}
		try
		{
			if (dic.ContainsKey(10059))
			{
				for (int num67 = 0; num67 < dic[10059].Count; num67++)
				{
					SCSignInData sCSignInData = dic[10059][num67] as SCSignInData;
					GetawardPanelShow.LastResuNum = sCSignInData.time;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10059);
				}
			}
		}
		catch (Exception var_201_4904)
		{
		}
		try
		{
			if (dic.ContainsKey(10061))
			{
				Mop_UpInfo.RewardData.Clear();
				for (int num68 = 0; num68 < dic[10061].Count; num68++)
				{
					SCSweep sCSweep = dic[10061][num68] as SCSweep;
					for (int num69 = 0; num69 < sCSweep.data.Count; num69++)
					{
						SweepReward sweepReward = new SweepReward();
						Mop_UpInfo.RewardData.Add(sweepReward);
						for (int num70 = 0; num70 < sCSweep.data[num69].res.Count; num70++)
						{
							sweepReward.reses.Add((int)sCSweep.data[num69].res[num70].key, (int)sCSweep.data[num69].res[num70].value);
						}
						for (int num71 = 0; num71 < sCSweep.data[num69].equip.Count; num71++)
						{
							sweepReward.equips.Add((int)sCSweep.data[num69].equip[num71].key, (int)sCSweep.data[num69].equip[num71].value);
						}
						for (int num72 = 0; num72 < sCSweep.data[num69].item.Count; num72++)
						{
							sweepReward.items.Add((int)sCSweep.data[num69].item[num72].key, (int)sCSweep.data[num69].item[num72].value);
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10061);
				}
			}
		}
		catch (Exception var_209_4B22)
		{
		}
		try
		{
			if (dic.ContainsKey(10067))
			{
				RandomEventManage.RandomBoxesByServer.Clear();
				for (int num73 = 0; num73 < dic[10067].Count; num73++)
				{
					SCBattleStart sCBattleStart = dic[10067][num73] as SCBattleStart;
					RandomEventManage.RandomBoxesByServer.AddRange(sCBattleStart.eventMap);
					GameSetting.RandomSeed = (int)sCBattleStart.id;
				}
			}
		}
		catch (Exception var_212_4BA4)
		{
		}
		try
		{
			if (dic.ContainsKey(10060))
			{
				SCVideoData sCVideoData = dic[10060][0] as SCVideoData;
				msg.EventData eventData = null;
				using (MemoryStream memoryStream = new MemoryStream(sCVideoData.videoData))
				{
					eventData = Opcode.Deserialize<msg.EventData>(memoryStream);
				}
				global::EventData eventData2 = new global::EventData();
				eventData2.endTime = (float)eventData.endTime;
				eventData2.eventId = eventData.eventId;
				eventData2.randomSeed = eventData.randomSeed;
				eventData2.tankPoses.AddRange(eventData.tankPoses);
				LogManage.Log("下载随机种子是" + eventData.randomSeed);
				eventData2.operData.AddRange(eventData.operData);
				EventNoteMgr.EvenData = eventData2;
				AdditionData additionData = null;
				using (MemoryStream memoryStream2 = new MemoryStream(sCVideoData.additionData))
				{
					additionData = Opcode.Deserialize<AdditionData>(memoryStream2);
				}
				EventNoteMgr.attackTech.Clear();
				EventNoteMgr.attackArmy.Clear();
				if (additionData != null)
				{
					EventNoteMgr.attackTech.AddRange(additionData.fighterTechData);
					EventNoteMgr.attackArmy.AddRange(additionData.fighterArmyData);
				}
				SCIslandData s_data = null;
				using (MemoryStream memoryStream3 = new MemoryStream(sCVideoData.islandData))
				{
					s_data = Opcode.Deserialize<SCIslandData>(memoryStream3);
				}
				SenceInfo.curMap = InfoMgr.GetMapData(s_data);
				if (EventNoteMgr.EvenData != null)
				{
					SenceHandler.GC_WatchNote2();
				}
			}
		}
		catch (Exception var_221_4D34)
		{
		}
		try
		{
			if (dic.ContainsKey(10066))
			{
				for (int num74 = 0; num74 < dic[10066].Count; num74++)
				{
					SCNPCAttackData sCNPCAttackData = dic[10066][num74] as SCNPCAttackData;
					EnemyNpcAttack enemyNpcAttack = new EnemyNpcAttack();
					enemyNpcAttack.id = (int)sCNPCAttackData.id;
					enemyNpcAttack.waveID = sCNPCAttackData.waveId;
					enemyNpcAttack.name = sCNPCAttackData.name;
					HeroInfo.GetInstance().enemyAttack.Add(enemyNpcAttack);
				}
			}
		}
		catch (Exception var_225_4DD1)
		{
		}
		try
		{
			if (dic.ContainsKey(10016) && SenceManager.inst.fightType == FightingType.Guard)
			{
				for (int num75 = 0; num75 < dic[10016].Count; num75++)
				{
					SCFightingEvent sCFightingEvent2 = dic[10016][num75] as SCFightingEvent;
					EnemyAttackManage.inst.enemyScore.wavePrize.AddRange(sCFightingEvent2.dropItem);
				}
			}
		}
		catch (Exception var_228_4E56)
		{
		}
		try
		{
			if (dic.ContainsKey(10021))
			{
				TechHandler.GetPlayerTechDataByServer(opcode.Get<SCTechData>(10021));
				if (this.DataChange != null)
				{
					this.DataChange(10021);
				}
			}
		}
		catch (Exception var_229_4E9D)
		{
		}
		try
		{
			if (dic.ContainsKey(10030))
			{
				for (int num76 = 0; num76 < dic[10030].Count; num76++)
				{
					SCHeartbeat sCHeartbeat = dic[10030][num76] as SCHeartbeat;
					TimeTools.asnySeverTimeDiff = DateTime.Now - TimeTools.ConvertLongDateTime(sCHeartbeat.id);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10030);
				}
			}
		}
		catch (Exception var_232_4F2D)
		{
		}
		try
		{
			if (dic.ContainsKey(10108))
			{
				for (int num77 = 0; num77 < dic[10108].Count; num77++)
				{
					SCArmyTokenCdTime sCArmyTokenCdTime = dic[10108][num77] as SCArmyTokenCdTime;
					SenceManager.inst.LongToDataTime(sCArmyTokenCdTime.armyTokenCdTime);
				}
			}
		}
		catch (Exception var_235_4F99)
		{
		}
		try
		{
			if (dic.ContainsKey(10018))
			{
				LogManage.Log("====获得战报长度：" + dic[10018].Count);
				for (int num78 = 0; num78 < dic[10018].Count; num78++)
				{
					SCReportData sCReportData = dic[10018][num78] as SCReportData;
					ReportData reportData = new ReportData();
					reportData.id = sCReportData.id;
					reportData.type = (BattleReportType)sCReportData.type;
					reportData.islandIndex = sCReportData.islandIdx;
					reportData.targetChangedMedal = sCReportData.targetChangedMedal;
					reportData.fighterChangedMedal = sCReportData.fighterChangedMedal;
					reportData.time = sCReportData.beginTime;
					reportData.fighterWin = sCReportData.fighterWin;
					reportData.fighterId = sCReportData.fighterId;
					reportData.fighterName = sCReportData.fighterName;
					reportData.fighterLevel = sCReportData.fighterLevel;
					reportData.fighterMedal = sCReportData.fighterMedal;
					reportData.lossRes.AddRange(sCReportData.lossRes);
					reportData.sendArmys.AddRange(sCReportData.sendArmys);
					reportData.destroyArmys.AddRange(sCReportData.destroyArmys);
					reportData.additions.AddRange(sCReportData.additions);
					reportData.targetName = sCReportData.targetName;
					reportData.targetId = sCReportData.targetId;
					reportData.targetLevel = sCReportData.targetLevel;
					reportData.worldMapId = sCReportData.worldMapId;
					reportData.prizeItems.AddRange(sCReportData.addItems);
					reportData.addRes.AddRange(sCReportData.addRes);
					reportData.addEquips.AddRange(sCReportData.addEquips);
					reportData.relation = (RelationType)sCReportData.relation;
					reportData.videoId = sCReportData.videoId;
					reportData.canRevenge = sCReportData.canRevenge;
					reportData.money = sCReportData.money;
					reportData.receiveMoneyTime = sCReportData.receiveMoneyTime;
					reportData.UnRead = sCReportData.unRead;
					if (sCReportData.sendSoldier != 0)
					{
						reportData.sendSoldier = sCReportData.sendSoldier;
					}
					if (sCReportData.lossSoldier != 0)
					{
						reportData.lossSoldier = sCReportData.lossSoldier;
					}
					if (reportData.type == BattleReportType.Base || reportData.type == BattleReportType.LiberateCommandIsland)
					{
						if (HeroInfo.GetInstance().PVPReportDataList.ContainsKey(reportData.id))
						{
							HeroInfo.GetInstance().PVPReportDataList[reportData.id] = reportData;
						}
						else
						{
							HeroInfo.GetInstance().PVPReportDataList.Add(reportData.id, reportData);
						}
						if (sCReportData.unRead)
						{
							HUDTextTool.ReportRedNotice = true;
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10018);
				}
			}
		}
		catch (Exception var_239_5293)
		{
		}
		try
		{
			if (dic.ContainsKey(10019))
			{
				HeroInfo.GetInstance().defTitle.Clear();
				HeroInfo.GetInstance().attrackTitle.Clear();
				LogManage.Log("====获得战报统计长度：" + dic[10019].Count);
				for (int num79 = 0; num79 < dic[10019].Count; num79++)
				{
					SCReportCountData sCReportCountData = dic[10019][num79] as SCReportCountData;
					ReportData reportData2 = new ReportData();
					reportData2.type = BattleReportType.dailyReport;
					reportData2.dailyType = sCReportCountData.type;
					reportData2.spyTimes = sCReportCountData.spyTimes;
					reportData2.beAtkedTimes = sCReportCountData.atkTimes;
					reportData2.beCaptureTimes = sCReportCountData.winTimes;
					reportData2.time = sCReportCountData.countTime;
					if (reportData2.dailyType == 1)
					{
						HeroInfo.GetInstance().defTitle.Add(reportData2);
					}
					else if (reportData2.dailyType == 2)
					{
						HeroInfo.GetInstance().attrackTitle.Add(reportData2);
					}
				}
				BattleReportInfo.ProcessData();
				if (this.DataChange != null)
				{
					this.DataChange(10019);
				}
			}
		}
		catch (Exception var_243_53E6)
		{
		}
		try
		{
			if (dic.ContainsKey(10000))
			{
				for (int num80 = 0; num80 < dic[10000].Count; num80++)
				{
					SCSessionData sCSessionData = dic[10000][num80] as SCSessionData;
					NetMgr.reqid = sCSessionData.requestId + 1;
					NetMgr.session = sCSessionData.session;
					ClientMgr.GetNet().HttpError = false;
				}
			}
		}
		catch (Exception var_246_5465)
		{
		}
		try
		{
			if (dic.ContainsKey(10072))
			{
				for (int num81 = 0; num81 < dic[10072].Count; num81++)
				{
					SCOtherPlayerInfo item = dic[10072][num81] as SCOtherPlayerInfo;
					if (SenceManager.inst)
					{
						SenceManager.inst.oldPlayerList.Add(item);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10072);
				}
			}
		}
		catch (Exception var_249_54FA)
		{
		}
		try
		{
			if (dic.ContainsKey(10006))
			{
				for (int num82 = 0; num82 < dic[10006].Count; num82++)
				{
					SCPlayerBuilding sCPlayerBuilding = dic[10006][num82] as SCPlayerBuilding;
					InfoMgr.GetFinishbuildList(SenceInfo.curMap, new SCPlayerBuilding[]
					{
						sCPlayerBuilding
					});
				}
				if (this.DataChange != null)
				{
					this.DataChange(10006);
				}
			}
		}
		catch (Exception var_252_5585)
		{
		}
		try
		{
			if (dic.ContainsKey(10082))
			{
				for (int num83 = 0; num83 < dic[10082].Count; num83++)
				{
					SCOpenArmy sCOpenArmy = dic[10082][num83] as SCOpenArmy;
					HeroInfo.GetInstance().addArmy_open.AddRange(sCOpenArmy.armys);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10082);
				}
			}
		}
		catch (Exception var_255_5610)
		{
		}
		try
		{
			if (dic.ContainsKey(10116))
			{
				for (int num84 = 0; num84 < dic[10116].Count; num84++)
				{
					SCConfigureArmyData sCConfigureArmyData = dic[10116][num84] as SCConfigureArmyData;
					if (!HeroInfo.GetInstance().AllArmyInfo.ContainsKey(sCConfigureArmyData.id))
					{
						HeroInfo.GetInstance().AllArmyInfo.Add(sCConfigureArmyData.id, new armyInfoInBuilding());
					}
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].buildingID = sCConfigureArmyData.id;
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFuncing.Clear();
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFuncing.AddRange(sCConfigureArmyData.cdTime2ArmyId);
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFunced.Clear();
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFunced.AddRange(sCConfigureArmyData.armyId2Num);
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFuncedToUI.Clear();
					HeroInfo.GetInstance().AllArmyInfo[sCConfigureArmyData.id].armyFuncedToUI.AddRange(sCConfigureArmyData.endArmyId2Num);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10116);
				}
			}
		}
		catch (Exception var_258_57D8)
		{
		}
		try
		{
			if (dic.ContainsKey(10007))
			{
				LogManage.LogError("结束冷却事件");
				for (int num85 = 0; num85 < dic[10007].Count; num85++)
				{
					SCCDEndTime sCCDEndTime = dic[10007][num85] as SCCDEndTime;
					if (SenceInfo.curMap.towerList_Data.ContainsKey(sCCDEndTime.id))
					{
						SenceInfo.curMap.towerList_Data[sCCDEndTime.id].lv = sCCDEndTime.level;
						if (HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(SenceInfo.curMap.towerList_Data[sCCDEndTime.id].buildingIdx))
						{
							HeroInfo.GetInstance().PlayerBuildingLevel[SenceInfo.curMap.towerList_Data[sCCDEndTime.id].buildingIdx] = Math.Max(HeroInfo.GetInstance().PlayerBuildingLevel[SenceInfo.curMap.towerList_Data[sCCDEndTime.id].buildingIdx], sCCDEndTime.level);
						}
						else
						{
							HeroInfo.GetInstance().PlayerBuildingLevel.Add(SenceInfo.curMap.towerList_Data[sCCDEndTime.id].buildingIdx, sCCDEndTime.level);
						}
					}
					if (HeroInfo.GetInstance().BuildingCDEndTime.ContainsKey(sCCDEndTime.id))
					{
						HeroInfo.GetInstance().BuildingCDEndTime[sCCDEndTime.id] = sCCDEndTime.CDEndTime;
					}
					else
					{
						HeroInfo.GetInstance().BuildingCDEndTime.Add(sCCDEndTime.id, sCCDEndTime.CDEndTime);
					}
					if (sCCDEndTime.type == 3)
					{
						if (SenceInfo.curMap.ResRemoveCDTime.ContainsKey(sCCDEndTime.id))
						{
							SenceInfo.curMap.ResRemoveCDTime[sCCDEndTime.id] = sCCDEndTime.CDEndTime;
						}
						else
						{
							SenceInfo.curMap.ResRemoveCDTime.Add(sCCDEndTime.id, sCCDEndTime.CDEndTime);
						}
					}
					else if (sCCDEndTime.type != 4)
					{
						if (SenceInfo.curMap.BuildingInCDTime.ContainsKey(sCCDEndTime.id))
						{
							SenceInfo.curMap.BuildingInCDTime[sCCDEndTime.id] = sCCDEndTime.CDEndTime;
						}
						else
						{
							SenceInfo.curMap.BuildingInCDTime.Add(sCCDEndTime.id, sCCDEndTime.CDEndTime);
						}
					}
					SenceInfo.curMap.ReSetArmy_AirBuildingCD();
					HeroInfo.GetInstance().ReSetArmy_AirBuildingCD();
				}
				if (this.DataChange != null)
				{
					this.DataChange(10007);
				}
			}
		}
		catch (Exception var_261_5ADC)
		{
		}
		try
		{
			if (dic.ContainsKey(10075))
			{
				List<SCSkillData> list2 = opcode.Get<SCSkillData>(10075);
				for (int num86 = 0; num86 < dic[10075].Count; num86++)
				{
					SCSkillData sk = dic[10075][num86] as SCSkillData;
					SkillCarteData skillCarteData = null;
					if ((sk.type == 1 || sk.type == 2) && sk.cdTime == 0L)
					{
						for (int num87 = 0; num87 < HeroInfo.GetInstance().skillCarteList.Count; num87++)
						{
							if (HeroInfo.GetInstance().skillCarteList[num87].id == sk.id)
							{
								skillCarteData = HeroInfo.GetInstance().skillCarteList[num87];
							}
						}
						if (skillCarteData == null)
						{
							skillCarteData = new SkillCarteData();
							skillCarteData.id = sk.id;
							skillCarteData.itemID = sk.itemId;
							skillCarteData.index = 0;
							HeroInfo.GetInstance().skillCarteList.Add(skillCarteData);
						}
						else
						{
							skillCarteData.id = sk.id;
							skillCarteData.itemID = sk.itemId;
							skillCarteData.index = 0;
						}
					}
					else if (sk.type == 3 && sk.cdTime == 0L)
					{
						HeroInfo.GetInstance().ClearAddAndSumData();
						HeroInfo.GetInstance().skillCarteList.RemoveAll((SkillCarteData a) => a.id == sk.id);
						HeroInfo.GetInstance().subSkill.Add(sk);
					}
					if (sk.type == 2 && sk.cdTime == 0L)
					{
						HeroInfo.GetInstance().ClearAddAndSumData();
						HeroInfo.GetInstance().addSkill.Add(sk);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10075);
				}
			}
		}
		catch (Exception var_266_5D7A)
		{
		}
		try
		{
			if (dic.ContainsKey(10085))
			{
				for (int num88 = 0; num88 < HeroInfo.GetInstance().skillCarteList.Count; num88++)
				{
					HeroInfo.GetInstance().skillCarteList[num88].index = 0;
				}
				int num89 = 0;
				for (int num90 = 0; num90 < dic[10085].Count; num90++)
				{
					SCSkillConfigData sCSkillConfigData = dic[10085][num90] as SCSkillConfigData;
					for (int num91 = 0; num91 < sCSkillConfigData.skillId.Count; num91++)
					{
						for (int num92 = 0; num92 < HeroInfo.GetInstance().skillCarteList.Count; num92++)
						{
							if (HeroInfo.GetInstance().skillCarteList[num92].id == sCSkillConfigData.skillId[num91])
							{
								HeroInfo.GetInstance().skillCarteList[num92].index = 1 + num89;
								num89 += UnitConst.GetInstance().skillList[HeroInfo.GetInstance().skillCarteList[num92].itemID].skillVolume;
								break;
							}
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10085);
				}
			}
		}
		catch (Exception var_273_5F25)
		{
		}
		try
		{
			if (dic.ContainsKey(10084))
			{
				for (int num93 = 0; num93 < dic[10084].Count; num93++)
				{
					SCSkillUpData sCSkillUpData = dic[10084][num93] as SCSkillUpData;
					if (HeroInfo.GetInstance().SkillInfo.ContainsKey((int)sCSkillUpData.id))
					{
						HeroInfo.GetInstance().SkillInfo[(int)sCSkillUpData.id] = sCSkillUpData.level;
					}
					else
					{
						HeroInfo.GetInstance().SkillInfo.Add((int)sCSkillUpData.id, sCSkillUpData.level);
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10084);
				}
			}
		}
		catch (Exception var_276_600F)
		{
		}
		try
		{
			if (dic.ContainsKey(10092))
			{
				for (int num94 = 0; num94 < dic[10092].Count; num94++)
				{
					SCSkillPointCd sCSkillPointCd = dic[10092][num94] as SCSkillPointCd;
					SepcialSoliderPanel.skillShowTime.skillRecoverTime = TimeTools.ConvertLongDateTime(sCSkillPointCd.cdTime);
				}
				if (this.DataChange != null)
				{
					this.DataChange(10092);
				}
			}
		}
		catch (Exception var_279_60AA)
		{
		}
		try
		{
			if (dic.ContainsKey(10015))
			{
				for (int num95 = 0; num95 < dic[10015].Count; num95++)
				{
					SCBattleEnd sCBattleEnd = dic[10015][num95] as SCBattleEnd;
					long value = sCBattleEnd.battlefieldStar.value;
					NTollgateManage.isBoFangTexiao = (sCBattleEnd.isFirstBattleField == 1 && sCBattleEnd.win);
					if (UnitConst.GetInstance().BattleFieldConst.ContainsKey(sCBattleEnd.completeId))
					{
						UnitConst.GetInstance().BattleFieldConst[sCBattleEnd.completeId].isCompleteUI = true;
					}
					if (NTollgateManage.isBoFangTexiao)
					{
						NTollgateManage.BattleFightIDToBofangTeXiao = (int)sCBattleEnd.battlefieldStar.key;
					}
				}
			}
		}
		catch (Exception var_283_619D)
		{
		}
		try
		{
			if (dic.ContainsKey(10078))
			{
				BattleFieldBox.battleFieldBoxes.Clear();
				for (int num96 = 0; num96 < dic[10078].Count; num96++)
				{
					SCBattleFieldBox sCBattleFieldBox = dic[10078][num96] as SCBattleFieldBox;
					int num56 = sCBattleFieldBox.type;
					if (num56 != 1)
					{
						if (num56 == 2)
						{
							foreach (KVStruct current5 in sCBattleFieldBox.battleFieldBox)
							{
								if (current5.value > 0L)
								{
									HeroInfo.GetInstance().addBox.Add(new KVStruct_Client((int)current5.key, (int)current5.value));
								}
							}
						}
					}
					else
					{
						foreach (KVStruct current6 in sCBattleFieldBox.battleFieldBox)
						{
							if (current6.value > 0L)
							{
								if (BattleFieldBox.BattleFieldBox_PlannerData[(int)current6.key].quility == 1)
								{
									if (!BattleFieldBox.battleFieldBoxes.ContainsKey(1))
									{
										BattleFieldBox.battleFieldBoxes.Add(1, new List<KVStruct>());
									}
									BattleFieldBox.battleFieldBoxes[1].Add(current6);
								}
								else if (BattleFieldBox.BattleFieldBox_PlannerData[(int)current6.key].quility == 2)
								{
									if (!BattleFieldBox.battleFieldBoxes.ContainsKey(2))
									{
										BattleFieldBox.battleFieldBoxes.Add(2, new List<KVStruct>());
									}
									BattleFieldBox.battleFieldBoxes[2].Add(current6);
								}
								else if (BattleFieldBox.BattleFieldBox_PlannerData[(int)current6.key].quility == 3)
								{
									if (!BattleFieldBox.battleFieldBoxes.ContainsKey(3))
									{
										BattleFieldBox.battleFieldBoxes.Add(3, new List<KVStruct>());
									}
									BattleFieldBox.battleFieldBoxes[3].Add(current6);
								}
							}
						}
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10078);
				}
			}
		}
		catch (Exception var_290_6431)
		{
		}
		try
		{
			if (dic.ContainsKey(10080))
			{
				for (int num97 = 0; num97 < dic[10080].Count; num97++)
				{
					SCBuyBuildingQueue sCBuyBuildingQueue = dic[10080][num97] as SCBuyBuildingQueue;
					BuildingQueue.MaxBuildingQueue = sCBuyBuildingQueue.queueNum;
				}
				if (this.DataChange != null)
				{
					this.DataChange(10080);
				}
			}
		}
		catch (Exception var_293_64C2)
		{
		}
		try
		{
			if (dic.ContainsKey(10115))
			{
				return;
			}
		}
		catch (Exception var_300_64E5)
		{
		}
		try
		{
			if (dic.ContainsKey(10031))
			{
				for (int num98 = 0; num98 < dic[10031].Count; num98++)
				{
					SCTipsData sCTipsData = dic[10031][num98] as SCTipsData;
					if (sCTipsData.id == 9L)
					{
						ShowAwardPanelManger.showAwardList();
					}
				}
				if (this.DataChange != null)
				{
					this.DataChange(10031);
				}
			}
		}
		catch (Exception var_303_657E)
		{
		}
		LogManage.LogError("读取数据结束！");
		if (this.DataReadEnd != null)
		{
			this.DataReadEnd();
		}
		HeroInfo.GetInstance().islandResData.ClearData();
	}

	public void CallBack(int cmd, bool isError, Opcode code)
	{
		if (!this.isInited)
		{
			this.Init();
		}
		if (this.NetMsgDic.ContainsKey(cmd))
		{
			this.NetMsgDic[cmd](isError, code);
		}
		else
		{
			LogManage.Log("不包含" + cmd + "的回调·· ·");
		}
	}
}
