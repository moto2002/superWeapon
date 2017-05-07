using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class User
{
	public static Dictionary<string, string> GetUserNames()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] array = null;
		string value = (!PlayerPrefs.HasKey("userNames")) ? string.Empty : PlayerPrefs.GetString("userNames");
		string value2 = (!PlayerPrefs.HasKey("passWords")) ? string.Empty : PlayerPrefs.GetString("passWords");
		if (!string.IsNullOrEmpty(value))
		{
			array = PlayerPrefs.GetString("userNames").Split(new char[]
			{
				'&'
			});
		}
		if (!string.IsNullOrEmpty(value2))
		{
			string[] array2 = PlayerPrefs.GetString("passWords").Split(new char[]
			{
				'&'
			});
		}
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				dictionary.Add(array[i], "123");
			}
			return dictionary;
		}
		return null;
	}

	public static int GetLanguageSetting()
	{
		if (PlayerPrefs.HasKey("LanguageSetting"))
		{
			return PlayerPrefs.GetInt("LanguageSetting");
		}
		return 0;
	}

	public static void SetLanguegeSetting(int languege_setting)
	{
		PlayerPrefs.SetInt("LanguageSetting", languege_setting);
	}

	public static int GetJY()
	{
		if (PlayerPrefs.HasKey("JY"))
		{
			return PlayerPrefs.GetInt("JY");
		}
		return 0;
	}

	public static void SetJY(int jy)
	{
		PlayerPrefs.SetInt("JY", jy);
	}

	public static string GetVer()
	{
		if (PlayerPrefs.HasKey("Ver"))
		{
			return PlayerPrefs.GetString("Ver");
		}
		return "XX";
	}

	public static void SetVer(string ver)
	{
		PlayerPrefs.SetString("Ver", ver);
	}

	public static string GetUserName()
	{
		if (PlayerPrefs.HasKey("userName"))
		{
			return PlayerPrefs.GetString("userName");
		}
		return string.Empty;
	}

	public static void SetUserName(string userName, string password)
	{
		PlayerPrefs.SetString("userName", userName);
		PlayerPrefs.SetString("passWord", password);
		Dictionary<string, string> userNames = User.GetUserNames();
		if (userNames == null)
		{
			PlayerPrefs.SetString("userNames", userName);
			PlayerPrefs.SetString("passWords", password);
		}
		else if (!userNames.ContainsKey(userName))
		{
			string[] array = PlayerPrefs.GetString("userNames").Split(new char[]
			{
				'&'
			});
			string[] array2 = PlayerPrefs.GetString("passWords").Split(new char[]
			{
				'&'
			});
			int num = array.Length;
			if (num > 9)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = num - 9; i < num; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append("&");
					stringBuilder2.Append(array2[i]);
					stringBuilder2.Append("&");
				}
				stringBuilder.Append(userName);
				stringBuilder2.Append(password);
				PlayerPrefs.SetString("userNames", stringBuilder.ToString());
				PlayerPrefs.SetString("passWords", stringBuilder2.ToString());
			}
			else
			{
				PlayerPrefs.SetString("userNames", PlayerPrefs.GetString("userNames") + "&" + userName);
				PlayerPrefs.SetString("passWords", PlayerPrefs.GetString("passWords") + "&" + password);
			}
		}
	}

	public static string GetUserID()
	{
		if (PlayerPrefs.HasKey("UserID"))
		{
			return PlayerPrefs.GetString("UserID");
		}
		return string.Empty;
	}

	public static void SetUserID(string userID)
	{
		PlayerPrefs.SetString("UserID", userID);
	}

	public static string GetPassword()
	{
		if (PlayerPrefs.HasKey("passWord"))
		{
			return PlayerPrefs.GetString("passWord");
		}
		return string.Empty;
	}

	public static void RemoveUserName(string name)
	{
		Dictionary<string, string> userNames = User.GetUserNames();
		if (userNames != null && userNames.ContainsKey(name))
		{
			userNames.Remove(name);
			if (User.GetUserName().Equals(name))
			{
				PlayerPrefs.SetString("userName", string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (KeyValuePair<string, string> current in userNames)
			{
				stringBuilder.Append(current.Key);
				stringBuilder.Append("&");
				stringBuilder2.Append(current.Value);
				stringBuilder2.Append("&");
			}
			PlayerPrefs.SetString("userNames", stringBuilder.ToString().TrimEnd(new char[]
			{
				'&'
			}));
			PlayerPrefs.SetString("passWords", stringBuilder2.ToString().TrimEnd(new char[]
			{
				'&'
			}));
			return;
		}
	}

	public static string GetIP()
	{
		return HeroInfo.GetInstance().IP;
	}

	public static void SetIP(string ip)
	{
		LogManage.LogError(ip);
		HeroInfo.GetInstance().IP = ip;
	}

	public static bool HasIP()
	{
		return !string.IsNullOrEmpty(HeroInfo.GetInstance().IP);
	}

	public static int GetServerName()
	{
		if (User.HasServerName())
		{
			int serverID = HeroInfo.GetInstance().ServerID;
			LogManage.Log(serverID);
			return serverID;
		}
		return 0;
	}

	public static int GetMusicState()
	{
		return PlayerPrefs.GetInt("Music");
	}

	public static int GetSoundEffectState()
	{
		return PlayerPrefs.GetInt("Sound");
	}

	public static bool HasServerName()
	{
		bool flag = HeroInfo.GetInstance().ServerID != -1;
		LogManage.Log("HasServerName~~~~~~~~~~~~~" + flag);
		return flag;
	}

	public static void SetServerName(int ServerName)
	{
		HeroInfo.GetInstance().ServerID = ServerName;
	}
}
