using msg;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NoticeMarquee : MonoBehaviour
{
	public static NoticeMarquee Inst;

	public GameObject marqueeTextPrefab;

	public Transform marqueeTextPrefabParent;

	public int marqeeNum;

	public float marqeeFirstY;

	public float marqeeHeight;

	private Dictionary<long, NoticeMarqueeItem> AllNoticeMarquee = new Dictionary<long, NoticeMarqueeItem>();

	public void OnDestroy()
	{
		NoticeMarquee.Inst = null;
	}

	public void Awake()
	{
		NoticeMarquee.Inst = this;
	}

	public void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		this.ShowMarquee();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10064)
		{
			this.ShowMarquee();
		}
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void ShowMarquee()
	{
		foreach (KeyValuePair<long, NoticeData> current in HeroInfo.GetInstance().gameAnnouncementData.showText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (current.Value.noticeType == 2 && TimeTools.ConvertLongDateTime(current.Value.endTime) > TimeTools.GetNowTimeSyncServerToDateTime() && TimeTools.ConvertLongDateTime(current.Value.startTime) < TimeTools.GetNowTimeSyncServerToDateTime())
			{
				string[] array = current.Value.content.Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string[] array2 = text.Split(new char[]
					{
						'='
					});
					if (array2[0].Equals("奖励资源") && !string.IsNullOrEmpty(array2[1].Trim()))
					{
						string textByKey = LanguageManage.GetTextByKey(array2[0], "BattleNotice");
						string[] array3 = array2[1].Split(new char[]
						{
							','
						});
						StringBuilder stringBuilder2 = new StringBuilder();
						for (int j = 0; j < array3.Length; j++)
						{
							if (array3[j].Contains(":"))
							{
								switch (int.Parse(array3[j].Split(new char[]
								{
									':'
								})[0]))
								{
								case 1:
									stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取金币", "ResIsland"), array3[j].Split(new char[]
									{
										':'
									})[1]));
									break;
								case 2:
									stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取石油", "ResIsland"), array3[j].Split(new char[]
									{
										':'
									})[1]));
									break;
								case 3:
									stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钢铁", "ResIsland"), array3[j].Split(new char[]
									{
										':'
									})[1]));
									break;
								case 4:
									stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取稀矿", "ResIsland"), array3[j].Split(new char[]
									{
										':'
									})[1]));
									break;
								case 7:
									stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钻石", "ResIsland"), array3[j].Split(new char[]
									{
										':'
									})[1]));
									break;
								}
							}
						}
						stringBuilder.Append(string.Format(textByKey, stringBuilder2.ToString()));
					}
					else if (array2[0].Equals("奖励道具") && !string.IsNullOrEmpty(array2[1].Trim()))
					{
						string textByKey2 = LanguageManage.GetTextByKey(array2[0], "BattleNotice");
						string[] array4 = array2[1].Split(new char[]
						{
							','
						});
						StringBuilder stringBuilder3 = new StringBuilder();
						for (int k = 0; k < array4.Length; k++)
						{
							if (array4[k].Contains(":"))
							{
								stringBuilder3.Append(string.Format("  {0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[int.Parse(array4[k].Split(new char[]
								{
									':'
								})[0])].Name, "item"), array4[k].Split(new char[]
								{
									':'
								})[1]));
							}
						}
						stringBuilder.Append(string.Format(textByKey2, stringBuilder3.ToString()));
					}
					else
					{
						bool flag = false;
						string textByKey3 = LanguageManage.GetTextByKey(array2[0], "BattleNotice", ref flag);
						if (array2.Length > 1)
						{
							string[] array5 = array2[1].Split(new char[]
							{
								';'
							});
							try
							{
								stringBuilder.Append(string.Format(textByKey3, array5));
							}
							catch (Exception var_18_413)
							{
								string text2 = string.Empty;
								string[] array6 = array5;
								for (int l = 0; l < array6.Length; l++)
								{
									string str = array6[l];
									text2 += str;
								}
								Debug.LogError(string.Format("跑马灯的文字内容是{0} 参数是{1)", textByKey3, text2));
							}
						}
						else if (flag)
						{
							stringBuilder.Append(textByKey3);
						}
						else
						{
							stringBuilder.Append(array2[0]);
						}
					}
				}
			}
			if (stringBuilder.Length > 0 && !this.AllNoticeMarquee.ContainsKey(current.Key))
			{
				GameObject gameObject = NGUITools.AddChild(this.marqueeTextPrefabParent.gameObject, this.marqueeTextPrefab);
				NoticeMarqueeItem component = gameObject.GetComponent<NoticeMarqueeItem>();
				component.EndTime = current.Value.endTime;
				component.interTime = (long)current.Value.interval;
				component.text_Conent.text = stringBuilder.ToString();
				component.ID = current.Key;
				component.ID_Display = (int)current.Key;
				component.SetInfo();
				this.AllNoticeMarquee.Add(current.Key, component);
			}
		}
	}
}
