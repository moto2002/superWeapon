using Game.Network;
using LitJson;
using SimpleFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoginInitUI : MonoBehaviour
{
	public GlobalGenerator globalGenerator;

	public Camera uicam;

	public static LoginInitUI inst;

	private bool indown;

	private float wwwProcess;

	private string strState = string.Empty;

	private bool isUpdateRes = true;

	public void OnDestroy()
	{
		LoginInitUI.inst = null;
	}

	private void Awake()
	{
		LoginInitUI.inst = this;
	}

	public void GetVerByServer()
	{
		if (Init.inst)
		{
			Init.inst.noProcessGa.GetComponent<UILabel>().text = "开始检查资源更新";
		}
		ClientMgr.GetNet().HttpError = false;
		HttpMgr.inst.httpSession = new HTTPSession(GameSetting.VersionByServer);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("clientVersion", GameSetting.Version);
		wWWForm.AddField("channelId", GameSetting.ChannelId);
		HttpMgr.inst.httpSession.onDataError = new Action<string, Action>(this.Eror);
		HttpMgr.inst.httpSession.SendPOST(wWWForm, new Action<Dictionary<string, string>, string>(this.CallBack), new IHttpSession.DownLoadTimeWaiting(HttpMgr.inst.DownLoadTimeWaiting), new IHttpSession.DownLoadTimeOut(this.DownLoadTimeOut));
	}

	private void OnInitFinish(Exception err)
	{
		if (err == null)
		{
			ResmgrNative.Instance.taskState.Clear();
			LogManage.LogError("检查资源完成");
			this.strState = "检查资源完成";
			if (ResmgrNative.Instance.ClientVerErr)
			{
				Init.inst.ResVersionText.color = Color.red;
			}
			else
			{
				Init.inst.ResVersionText.color = new Color(1f, 0.6862745f, 0.211764708f);
			}
			List<string> list = new List<string>();
			list.Add(ResManager.artFilePlatform);
			IEnumerable<LocalVersion.ResInfo> needDownloadRes = ResmgrNative.Instance.GetNeedDownloadRes(list);
			foreach (LocalVersion.ResInfo current in needDownloadRes)
			{
				LogManage.LogError(string.Format("下载资源{0}", current.FileName));
				current.Download(delegate(LocalVersion.ResInfo Res, Exception Ex)
				{
					Debug.LogError(string.Format("下载资源完成{0}", Res.FileName));
					if (Res != null && Ex == null && Res.FileName.Contains(".zts"))
					{
						Res.BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
						{
							if (bundle != null)
							{
								UnityEngine.Object[] array = bundle.LoadAll(typeof(TextAsset));
								for (int i = 0; i < array.Length; i++)
								{
									UnityEngine.Object @object = array[i];
									TextAsset textAsset = @object as TextAsset;
									GameTools.CreateFile(Util.DataPath + "/PlannerDataXMl/", textAsset.name + ".xml", textAsset.text);
								}
								bundle.Unload(false);
							}
						});
					}
					if (Res != null && Ex != null)
					{
						Debug.LogError(string.Format("下载资源{0} 错误是{1}", Res.FileName, Ex.ToString()));
					}
				}, true);
			}
			ResmgrNative.Instance.WaitForTaskFinish(new Action(this.DownLoadFinish));
			this.indown = true;
		}
		else
		{
			LogManage.LogError(err.ToString());
			this.strState = null;
			ResmgrNative.Instance.taskState.Clear();
			LogManage.LogError("检查资源失败");
			this.strState = "检查资源失败";
			Init.inst.ResVersionText.color = Color.red;
			this.DownLoadFinish();
		}
	}

	private void DownLoadFinish()
	{
		ResmgrNative.Instance.taskState.Clear();
		this.indown = false;
		this.strState = "加载中 ......";
		Init.inst.ResVersionText.text = ResmgrNative.Instance.verLocal.ver.ToString();
		Init.inst.InitBundle();
	}

	private void LateUpdate()
	{
		if (ResmgrNative.Instance != null)
		{
			ResmgrNative.Instance.Update(ref this.wwwProcess);
			if (ResmgrNative.Instance.taskState.taskcount > 0)
			{
				if (this.indown)
				{
					this.strState = string.Format("{0}:{1}/{2}", LanguageManage.GetTextByKey("加载中", "client_Res"), ResmgrNative.Instance.taskState.downloadcount, ResmgrNative.Instance.taskState.taskcount - 1);
					if (Init.inst)
					{
						Init.inst.SetProcessForDown(this.wwwProcess, this.strState);
					}
				}
				else if (Init.inst && ResmgrNative.Instance.taskState.taskcount - ResmgrNative.Instance.taskState.downloadcount > 0)
				{
					Init.inst.SetProcess(this.wwwProcess, this.strState);
				}
			}
		}
	}

	private void DownLoadTimeOut(Action Resend)
	{
		ClientMgr.GetNet().HttpError = true;
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveFalse();
		}
		MessageBox.GetNetMessagePanel().ShowBtn(string.Empty, LanguageManage.GetTextByKey("您当前的网络环境不太好，请重试或者重新登陆游戏！", "client_Res"), LanguageManage.GetTextByKey("重新登陆", "client_Res"), new Action(HttpMgr.ReStartGame));
	}

	private void Eror(string content, Action Resend)
	{
		ClientMgr.GetNet().HttpError = true;
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveFalse();
		}
		MessageBox.GetNetMessagePanel().ShowBtn(string.Empty, LanguageManage.GetTextByKey("您当前的网络环境不太好，请重试或者重新登陆游戏！", "client_Res"), LanguageManage.GetTextByKey("重新登陆", "client_Res"), new Action(HttpMgr.ReStartGame));
	}

	private void CallBack(Dictionary<string, string> aa, string opcode)
	{
		Debug.Log("获取服务器数据 CallBack");
		if (Init.inst)
		{
			Init.inst.noProcessGa.GetComponent<UILabel>().text = "检查资源更新完成";
		}
		if (ClientMgr.GetNet() && ClientMgr.GetNet().WaitingCommunication)
		{
			ClientMgr.GetNet().WaitingCommunication.SetActiveFalse();
		}
		LogManage.LogError(opcode);
		string text = string.Empty;
		string value = string.Empty;
		string remotoURL = string.Empty;
		string text2 = string.Empty;
		JsonData jsonData = JsonMapper.ToObject(opcode);
		try
		{
			text = jsonData["latestVersion"].ToString();
		}
		catch (Exception var_5_A1)
		{
			Debug.LogError("不包含 latestVersion");
		}
		try
		{
			value = jsonData["forceUpdate"].ToString();
		}
		catch (Exception var_6_C9)
		{
			Debug.LogError("不包含 forceUpdate");
		}
		try
		{
			string s = jsonData["resUpdate"].ToString();
			this.isUpdateRes = (int.Parse(s) == 1);
		}
		catch (Exception var_8_102)
		{
			Debug.LogError("不包含 resUpdate");
		}
		try
		{
			remotoURL = jsonData["resUrl"].ToString();
		}
		catch (Exception var_9_12A)
		{
			Debug.LogError("不包含 resUrl");
		}
		try
		{
			text2 = jsonData["code"].ToString();
		}
		catch (Exception var_10_152)
		{
			Debug.LogError("不包含 code");
		}
		if (bool.Parse(value))
		{
		}
		if (text2 == "200")
		{
			List<string> list = new List<string>();
			list.Add(ResManager.artFilePlatform);
			LogManage.LogError("==========:" + ResManager.artFilePlatform);
			if (this.isUpdateRes)
			{
				ResmgrNative.Instance.BeginInit(remotoURL, new Action<Exception>(this.OnInitFinish), list, 1, true);
				this.strState = "检查资源";
				LogManage.LogError("开始检查更新 --- 当前时间：" + Time.time);
			}
			else
			{
				ResmgrNative.Instance.BeginInit(remotoURL, new Action<Exception>(this.OnInitFinish), list, 1, false);
			}
		}
		else
		{
			Debug.LogError("code is not 200" + text2);
		}
	}
}
