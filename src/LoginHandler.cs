using DG.Tweening;
using LitJson;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler
{
	public static void CG_H_Connect()
	{
		CSLogin cSLogin = new CSLogin();
		cSLogin.platformId = HeroInfo.GetInstance().platformId;
		cSLogin.imei = SystemInfo.deviceUniqueIdentifier;
		cSLogin.equModel = SystemInfo.deviceModel;
		cSLogin.token = HeroInfo.GetInstance().token;
		cSLogin.time = long.Parse(HeroInfo.GetInstance().time);
		cSLogin.channelId = HeroInfo.GetInstance().channelId;
		cSLogin.sdk = HDSDKInit.isLoginEnd;
		cSLogin.clientVersion = GameSetting.Version;
		LogManage.LogError(string.Format("告诉服务器  服务器IP：{0}  platformId ：{1}   channelID: {2}  isSdk : {3}  httpError :{4} ", new object[]
		{
			HttpMgr.inst.httpSession.StrURL,
			HeroInfo.GetInstance().platformId,
			cSLogin.channelId,
			cSLogin.sdk,
			ClientMgr.GetNet().HttpError
		}));
		if (ClientMgr.GetNet() == null)
		{
			LogManage.LogError("ClientMgr.GetNet() == null");
		}
		ClientMgr.GetNet().SendHttp(1000, cSLogin, new DataHandler.OpcodeHandler(LoginHandler.GC_H_Connect), null);
	}

	private static void InitData()
	{
		if (CameraControl.inst)
		{
			CameraControl.inst.CloseBuildingState();
		}
	}

	public static void GC_H_Connect(bool isError, Opcode opcode)
	{
		LogManage.LogError("告诉服务器   isError:" + isError);
		if (!isError)
		{
			LoginHandler.InitData();
			LoginPanelManager._instance.LogionSucess();
			if (GameSetting.isUseSDK && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
			{
				if (!string.IsNullOrEmpty(User.GetUserID()) && !User.GetUserID().Equals(HeroInfo.GetInstance().userId.ToString()))
				{
					LogManage.LogError("取消推送 Alias ：" + User.GetUserID());
					User.SetUserID(HeroInfo.GetInstance().userId.ToString());
				}
				LogManage.LogError("设置推送 Alias ：" + HeroInfo.GetInstance().userId);
			}
			List<SCSessionData> list = opcode.Get<SCSessionData>(10000);
			List<SCIslandData> list2 = opcode.Get<SCIslandData>(10001);
			if (list.Count > 0)
			{
				NetMgr.session = list[0].session;
			}
			else
			{
				LogManage.Log("SessionDataList .count ==0");
			}
			if (list2.Count > 0)
			{
				LoginHandler.CreateHomeIsland(HeroInfo.GetInstance().homeInWMapIdx);
				SenceInfo.curMap = InfoMgr.GetMapData(list2[0]);
			}
			else
			{
				LogManage.Log("IslandDataList .count ==0");
			}
			DOTween.ClearCachedTweens();
			if (CameraInitMove.inst)
			{
				CameraInitMove.inst.IsFirst = true;
			}
			if (GameConst.IsFirstLogin && Init.inst)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/OpeningDialogPanel")) as GameObject;
				gameObject.transform.parent = Init.inst.transform.parent;
				gameObject.transform.localScale = Vector3.one;
			}
			else
			{
				LoginHandler.GoHome();
			}
		}
	}

	public static void GoHome()
	{
		PlayerHandle.EnterSence("island");
		LogManage.Log("请求登录数据了······");
		CSLoginData cSLoginData = new CSLoginData();
		cSLoginData.id = 1;
		ClientMgr.GetNet().SendHttp(9004, cSLoginData, new DataHandler.OpcodeHandler(LoginHandler.Loain1DataCallback), null);
	}

	private static void Loain1DataCallback(bool isError, Opcode code)
	{
		if (!isError)
		{
			CSLoginData cSLoginData = new CSLoginData();
			cSLoginData.id = 2;
			ClientMgr.GetNet().SendHttp(9004, cSLoginData, new DataHandler.OpcodeHandler(LoginHandler.Loain2DataCallback), null);
		}
	}

	private static void Loain2DataCallback(bool isError, Opcode code)
	{
		if (!isError)
		{
			HeartBeat.inst.StartHeartbeat();
			HUDTextTool.inst.noticeMarqee.gameObject.SetActive(true);
			JsonData jsonData = new JsonData();
			jsonData["accountId"] = HeroInfo.GetInstance().platformId;
			jsonData["level"] = HeroInfo.GetInstance().playerlevel.ToString();
			jsonData["serverId"] = User.GetServerName().ToString();
			jsonData["userid"] = HeroInfo.GetInstance().userId.ToString();
			jsonData["serverName"] = User.GetServerName().ToString();
			jsonData["roleName"] = HeroInfo.GetInstance().userName;
			jsonData["upType"] = "2";
			jsonData["serverTime"] = HeroInfo.GetInstance().createTime;
			HDSDKInit.UpLoadPlayerInfo(jsonData.ToJson());
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TDGAAccount tDGAAccount = TDGAAccount.SetAccount(HeroInfo.GetInstance().platformId);
				tDGAAccount.SetLevel(HeroInfo.GetInstance().playerlevel);
				tDGAAccount.SetAccountName(HeroInfo.GetInstance().userName);
				tDGAAccount.SetGameServer(User.GetServerName().ToString());
			}
		}
	}

	private static void CreateHomeIsland(int idx)
	{
		LogManage.Log("CreateHomeIsland  " + idx);
		PlayerWMapData playerWMapData = new PlayerWMapData();
		playerWMapData.idx = idx;
		playerWMapData.ownerType = 1;
		playerWMapData.ownerName = "测试怪798";
		if (HeroInfo.GetInstance().worldMapInfo.playerWMap.ContainsKey(playerWMapData.idx))
		{
			HeroInfo.GetInstance().worldMapInfo.playerWMap[playerWMapData.idx] = playerWMapData;
		}
		else
		{
			HeroInfo.GetInstance().worldMapInfo.playerWMap.Add(playerWMapData.idx, playerWMapData);
		}
	}
}
