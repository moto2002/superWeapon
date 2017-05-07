using LitJson;
using msg;
using System;
using UnityEngine;

public class HDSDKInit : MonoBehaviour
{
	public GameObject backTexture;

	private static string sdkInfo_Json;

	public static bool isInitSDKEnd;

	public TweenAlpha TweenHealthTip;

	public static bool isLoginEnd;

	public void Update()
	{
		if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
		{
			HDSDKInit.ExitGame();
		}
	}

	public void Awake()
	{
		GameTools.DontDestroyOnLoad(base.gameObject);
		if (!GameSetting.isUseSDK)
		{
			this.InitSDKCallBack();
		}
		else if (HDSDKInit.isInitSDKEnd)
		{
			this.TweenHealthTip.enabled = true;
		}
	}

	public void SwitchAccount()
	{
		HDSDKInit.isLoginEnd = false;
		HttpMgr.ReStartGame();
	}

	public static void InitSDK()
	{
		if (!HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("InitSDK", new object[0]);
				}
			}
		}
	}

	public void StartSDK()
	{
		this.backTexture.SetActive(true);
		if (!HDSDKInit.isLoginEnd && GameSetting.isUseSDK)
		{
			HDSDKInit.InitSDK_Login();
		}
		else
		{
			Application.LoadLevel(1);
		}
	}

	public static void InitSDK_Login()
	{
		if (HDSDKInit.isLoginEnd)
		{
			HttpMgr.ReStartGame();
		}
		else
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						@static.Call("initSDK", new object[0]);
					}
				}
			}
			catch (Exception ex)
			{
				LogManage.LogError(ex.ToString());
			}
		}
	}

	public static void ExitGame()
	{
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("logout", new object[0]);
				}
			}
		}
		else
		{
			Application.Quit();
		}
	}

	public static void IsHideSDKView(bool isHide)
	{
	}

	public static void Pay(string payInfo)
	{
		LogManage.LogError(payInfo);
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("pay", new object[]
					{
						payInfo
					});
				}
			}
		}
	}

	public static void SendCreatePlayerInfo(string info)
	{
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("sendPlayerInfo", new object[]
					{
						info
					});
				}
			}
		}
	}

	public static void SendLevel(string level)
	{
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("setLevel", new object[]
					{
						level
					});
				}
			}
		}
	}

	public static void SendRole(string roleInfo)
	{
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("setRole", new object[]
					{
						roleInfo
					});
				}
			}
		}
	}

	public static void UpLoadPlayerInfo(string roleInfo)
	{
		if (HDSDKInit.isLoginEnd)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					@static.Call("UpLoadPlayerInfo", new object[]
					{
						roleInfo
					});
				}
			}
		}
	}

	private void sdkInfo(string sdkInfo)
	{
		HDSDKInit.sdkInfo_Json = sdkInfo;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				@static.Call("showLoginView", new object[0]);
			}
		}
	}

	public void InitSDKCallBack()
	{
		HDSDKInit.isInitSDKEnd = true;
		this.TweenHealthTip.enabled = true;
	}

	private void loginCallBack(string loginCallBack)
	{
		HDSDKInit.isLoginEnd = true;
		Debug.LogError("SDK : loginCallBack-----------" + loginCallBack);
		JsonData jsonData = JsonMapper.ToObject(loginCallBack);
		GameSetting.ChannelId = jsonData["channel"].ToString();
		GameSetting.Token = jsonData["token"].ToString();
		GameSetting.userid = jsonData["userid"].ToString();
		GameSetting.appid = jsonData["appid"].ToString();
		HttpMgr.ReStartGame();
	}

	private void payCallBack(string payCallBack)
	{
		NGUIDebug.Log(new object[]
		{
			"payCallBack" + payCallBack
		});
		if (!payCallBack.Equals("1"))
		{
			CSGetOrderId cSGetOrderId = new CSGetOrderId();
			cSGetOrderId.goodsId = int.Parse(ShopHandler.json["paramProductId"].ToString());
			cSGetOrderId.goodsCount = 1;
			cSGetOrderId.orderId = long.Parse(ShopHandler.json["paramBillNo"].ToString());
			cSGetOrderId.appleProductId = ShopHandler.json["AppleProductId"].ToString();
			ClientMgr.GetNet().SendHttp(2502, cSGetOrderId, null, null);
		}
	}
}
