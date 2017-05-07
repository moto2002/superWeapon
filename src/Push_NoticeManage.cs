using System;
using UnityEngine;

public class Push_NoticeManage : MonoBehaviour
{
	private DateTime Time_Home;

	public static Push_NoticeManage Instance;

	public void OnDestroy()
	{
		Push_NoticeManage.Instance = null;
	}

	public void OnApplicationQuit()
	{
		if (GameSetting.isUseSDK && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
		{
			TalkingDataGA.OnEnd();
		}
	}

	public void OnApplicationFocus(bool focus)
	{
	}

	public void OnApplicationPause(bool pause)
	{
		if (GameSetting.isUseSDK && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
		{
			if (pause)
			{
				TalkingDataGA.OnEnd();
				this.Time_Home = DateTime.Now;
			}
			else if ((DateTime.Now - this.Time_Home).TotalMinutes > 10.0)
			{
				MessageBox.GetNetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("提示", "others"), LanguageManage.GetTextByKey("指挥官，您已经与基地失去联系，请重新登陆", "others"), LanguageManage.GetTextByKey("确认", "Battle"), delegate
				{
					HttpMgr.ReStartGame();
				});
			}
			else
			{
				TalkingDataGA.OnStart(GameSetting.TalkingdataAppID, GameSetting.ChannelId);
			}
		}
	}

	public void Awake()
	{
		Push_NoticeManage.Instance = this;
	}

	public void recvMessage(string str)
	{
	}

	private void Start()
	{
		if (GameSetting.isUseSDK && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
		{
			TalkingDataGA.OnStart(GameSetting.TalkingdataAppID, GameSetting.ChannelId);
		}
	}
}
