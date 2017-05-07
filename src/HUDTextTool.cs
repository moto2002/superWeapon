using LitJson;
using SimpleFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class HUDTextTool : MonoBehaviour
{
	public enum TextUITypeEnum
	{
		Num1,
		Num2,
		Num3,
		Num4,
		Num5
	}

	private struct TextTmp
	{
		public string text;

		public string value;

		public HUDTextTool.TextUITypeEnum txtUIType;
	}

	public static HUDTextTool inst;

	public static bool isSkillEquipment;

	public int ElectricityNum;

	public Color tiShiColor = new Color(158f, 240f, 15f, 1f);

	public Color jingShiColor = new Color(255f, 34f, 34f);

	public UITable TextTable;

	public ResTips restip;

	public Transform hudtextResourcePanel;

	public ItemTips1 itemTips;

	public TextTips TextTips;

	public bool istrue;

	public GameObject marquee;

	public BattleField isEndTollgate;

	public static int BuildingResType;

	public bool isLoading = true;

	public Vector3 hedanBoom;

	public static bool WorldMapRedNotice;

	public static bool ReportRedNotice;

	public static string serverName;

	public Camera hudtextCamera;

	public GameObject GUIGameObject;

	public NoticeMarquee noticeMarqee;

	public static bool isGetActivitiesAward = true;

	public static bool isNewActivitie = true;

	public static bool isCloseGameStartNotice;

	public static bool isGetChareAward = true;

	public bool Kaiqixinshouyindao;

	public static float ResProduceSpeedByPower;

	private GameObject aa;

	private string lastText = string.Empty;

	private float lastTextTime;

	private float dianjiJianGe = 0.6f;

	private Queue<HUDTextTool.TextTmp> AllTextInScreen = new Queue<HUDTextTool.TextTmp>();

	private bool IsCanDisplayText = true;

	public bool isCenterPos;

	public Vector3 centerPos;

	private bool LockLua
	{
		get
		{
			return Loading.senceType == SenceType.Login || (ShowPlayerLevelManager.ins != null && ShowPlayerLevelManager.ins.gameObject.activeInHierarchy) || (ShowAwardPanelManger._ins != null && ShowAwardPanelManger._ins.gameObject.activeInHierarchy) || (HomeUpOpenBuilding.inst != null && HomeUpOpenBuilding.inst.gameObject.activeInHierarchy) || (NewarmyInfo.inst != null && NewarmyInfo.inst.gameObject.activeInHierarchy) || (TopTenPanelManage._ins != null && TopTenPanelManage._ins.gameObject.activeInHierarchy);
		}
	}

	private void Awake()
	{
		if (HUDTextTool.inst != null)
		{
			UnityEngine.Object.Destroy(HUDTextTool.inst.gameObject);
		}
		HUDTextTool.inst = this;
		this.TextTable = base.GetComponentInChildren<UITable>();
		GameTools.DontDestroyOnLoad(base.gameObject);
	}

	private void OnEnable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.LevelChange += new DataHandler.Data_Change(this.DataHandler_LevelChange);
			ClientMgr.GetNet().NetDataHandler.ExpChange += new DataHandler.Data_Change(this.DataHandler_ExpChange);
			ClientMgr.GetNet().NetDataHandler.ItemSubChange += new DataHandler.KVData_Change(this.DataHandler_ItemSubChange);
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
		GameTools.DontDestroyOnLoad(base.gameObject);
		this.RefreshSound();
	}

	public void RefreshSound()
	{
		if (User.GetMusicState() == 1)
		{
			AudioManage.inst.IsOpenMusic = true;
		}
		if (User.GetMusicState() == 2)
		{
			AudioManage.inst.IsOpenMusic = false;
		}
		if (User.GetSoundEffectState() == 1)
		{
			AudioManage.inst.IsOpenPlayAudioBackGround = true;
		}
		if (User.GetSoundEffectState() == 2)
		{
			AudioManage.inst.IsOpenPlayAudioBackGround = false;
		}
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.LevelChange -= new DataHandler.Data_Change(this.DataHandler_LevelChange);
			ClientMgr.GetNet().NetDataHandler.ExpChange -= new DataHandler.Data_Change(this.DataHandler_ExpChange);
			ClientMgr.GetNet().NetDataHandler.ItemSubChange -= new DataHandler.KVData_Change(this.DataHandler_ItemSubChange);
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10006 || opcodeCMD == 10007)
		{
			this.Powerhouse();
		}
	}

	private void DataHandler_ItemSubChange(List<KVStruct_Client> key_Value)
	{
		for (int i = 0; i < key_Value.Count; i++)
		{
			if (AdjutantPanel.isOk)
			{
				return;
			}
			this.SetResText(string.Format("{0}{1} X", LanguageManage.GetTextByKey("使用", "item"), LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[key_Value[i].Key].Name, "item")), key_Value[i].Value.ToString());
		}
	}

	public void OnSetTextureIcon(UITexture texture, string iconId, string path)
	{
		if (iconId == "meinv")
		{
			if (!texture.gameObject.GetComponent<NPC_GIF>())
			{
				texture.gameObject.AddComponent<NPC_GIF>();
				texture.gameObject.GetComponent<NPC_GIF>().Init("natasha-idle", "natasha-idle", 18, true);
			}
		}
		else
		{
			if (texture.gameObject.GetComponent<NPC_GIF>())
			{
				UnityEngine.Object.Destroy(texture.gameObject.GetComponent<NPC_GIF>());
			}
			texture.mainTexture = (Resources.Load(path + iconId) as Texture);
		}
	}

	public void ShowBuyMoney()
	{
		LogManage.LogError("ShowButMoney");
		ShopPanelManage.ShowHelp_NoRMB(null, null);
	}

	public void ShowBuyCoin()
	{
		MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("消费提醒", "others"), LanguageManage.GetTextByKey("金币不足请前往商城购买", "others"), LanguageManage.GetTextByKey("购买", "others"), delegate
		{
			FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
			if (!ShopPanelManage.shop.gameObject.activeSelf)
			{
				ShopPanelManage.shop.gameObject.SetActive(true);
			}
		}, LanguageManage.GetTextByKey("取消", "others"), null);
	}

	public void Powerhouse()
	{
		if (BuilingStatePanel.inst)
		{
			BuilingStatePanel.inst.Powerhouse();
		}
		MainUIPanelManage.power = 0;
		MainUIPanelManage.electricUse = 0;
		HUDTextTool.ResProduceSpeedByPower = 1f;
		this.ElectricityNum = 0;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i].index == 62)
			{
				this.ElectricityNum++;
				if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].outputLimit.ContainsKey(ResType.电力))
				{
					MainUIPanelManage.power += UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].outputLimit[ResType.电力];
				}
			}
			else
			{
				MainUIPanelManage.electricUse += UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].electricUse;
			}
		}
		if (UIManager.curState == SenceState.Attacking && FightPanelManager.inst)
		{
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, FightPanelManager.inst.powerelectricity);
		}
		if (UIManager.curState == SenceState.Home && MainUIPanelManage._instance)
		{
			MainUIPanelManage.power_Home = MainUIPanelManage.power;
			MainUIPanelManage.electricUse_Home = MainUIPanelManage.electricUse;
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, MainUIPanelManage._instance.electricityPow);
		}
		if (UIManager.curState == SenceState.Spy && SpyPanelManager.inst && SpyPanelManager.inst.powerTiao)
		{
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, SpyPanelManager.inst.powerTiao);
		}
	}

	private void DataHandler_ExpChange(int opcodeCMD)
	{
		this.SetResText(LanguageManage.GetTextByKey("角色经验", "others"), "+ " + opcodeCMD.ToString());
	}

	private void DataHandler_LevelChange(int level)
	{
		LogManage.LogError("角色当前等级为：" + level);
		if (level > 1)
		{
			this.ShowPlayerLevel(level);
		}
		JsonData jsonData = new JsonData();
		jsonData["accountId"] = HeroInfo.GetInstance().platformId;
		jsonData["level"] = level.ToString();
		jsonData["serverId"] = User.GetServerName().ToString();
		jsonData["userid"] = HeroInfo.GetInstance().userId.ToString();
		jsonData["serverName"] = User.GetServerName().ToString();
		jsonData["roleName"] = HeroInfo.GetInstance().userName;
		jsonData["upType"] = "3";
		jsonData["serverTime"] = HeroInfo.GetInstance().createTime;
		HDSDKInit.UpLoadPlayerInfo(jsonData.ToJson());
	}

	public void ShowPlayerLevel(int playerLevel)
	{
		FuncUIManager.inst.OpenFuncUI("ShowPlayerLevelPanel", Loading.senceType);
		ShowPlayerLevelManager.ins.level.text = string.Concat(new object[]
		{
			LanguageManage.GetTextByKey("恭喜您升到", "others"),
			playerLevel,
			LanguageManage.GetTextByKey("级", "others"),
			"！"
		});
	}

	private bool GetbattleField(NewbieGuide bie)
	{
		return bie.battleField == 0 || UnitConst.GetInstance().BattleFieldConst[bie.battleField].fightRecord.isFight;
	}

	public void Xiayibu()
	{
		NewbieGuidePanel.curGuideIndex++;
		NewbieGuidePanel._instance.HideSelf();
	}

	private void FixedUpdate()
	{
		if (!NewbieGuidePanel.isEnemyAttck)
		{
			UIManager.inst.UIInUsed(true);
		}
		if (this.AllTextInScreen.Count > 0 && this.IsCanDisplayText)
		{
			this.IsCanDisplayText = false;
			HUDTextTool.TextTmp textTmp = this.AllTextInScreen.Dequeue();
			if (!string.IsNullOrEmpty(textTmp.value))
			{
				base.StartCoroutine(this.SetTextIE(textTmp.text, textTmp.value));
			}
			else
			{
				base.StartCoroutine(this.SetTextIE(textTmp.text, textTmp.txtUIType));
			}
		}
	}

	private void DataHandler_RMBChange(int opcodeCMD)
	{
		this.SetResText(LanguageManage.GetTextByKey("获得钻石", "others"), opcodeCMD.ToString());
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		this.istrue = true;
	}

	public void SetColorChange(GameObject ga)
	{
		TweenAlpha.Begin(ga, 0.1f, 0.627451f).SetOnFinished(new EventDelegate(delegate
		{
			TweenAlpha.Begin(ga, 0.1f, 1f).onFinished.Clear();
		}));
	}

	public void SetText(string text, HUDTextTool.TextUITypeEnum txtUIType)
	{
		if (this.lastText.Equals(text) && Time.time - this.lastTextTime < this.dianjiJianGe)
		{
			return;
		}
		this.lastText = text;
		this.lastTextTime = Time.time;
		HUDTextTool.TextTmp item = default(HUDTextTool.TextTmp);
		item.text = text;
		item.txtUIType = txtUIType;
		this.AllTextInScreen.Enqueue(item);
	}

	public void ClearTextQueue()
	{
		this.AllTextInScreen.Clear();
	}

	public void SetResText(string text, string value)
	{
		if (this.lastText.Equals(text) && Time.time - this.lastTextTime < 2f)
		{
			return;
		}
		this.lastText = text;
		this.lastTextTime = Time.time;
		HUDTextTool.TextTmp item = default(HUDTextTool.TextTmp);
		item.text = text;
		item.value = value;
		this.AllTextInScreen.Enqueue(item);
	}

	[DebuggerHidden]
	private IEnumerator SetTextIE(string text, HUDTextTool.TextUITypeEnum txtUIType)
	{
		HUDTextTool.<SetTextIE>c__Iterator6B <SetTextIE>c__Iterator6B = new HUDTextTool.<SetTextIE>c__Iterator6B();
		<SetTextIE>c__Iterator6B.txtUIType = txtUIType;
		<SetTextIE>c__Iterator6B.text = text;
		<SetTextIE>c__Iterator6B.<$>txtUIType = txtUIType;
		<SetTextIE>c__Iterator6B.<$>text = text;
		<SetTextIE>c__Iterator6B.<>f__this = this;
		return <SetTextIE>c__Iterator6B;
	}

	[DebuggerHidden]
	private IEnumerator SetTextIE(string text, string value)
	{
		HUDTextTool.<SetTextIE>c__Iterator6C <SetTextIE>c__Iterator6C = new HUDTextTool.<SetTextIE>c__Iterator6C();
		<SetTextIE>c__Iterator6C.text = text;
		<SetTextIE>c__Iterator6C.value = value;
		<SetTextIE>c__Iterator6C.<$>text = text;
		<SetTextIE>c__Iterator6C.<$>value = value;
		<SetTextIE>c__Iterator6C.<>f__this = this;
		return <SetTextIE>c__Iterator6C;
	}

	public void SetText(string text, Transform tar)
	{
		try
		{
			if (!this.lastText.Equals(text) || Time.time - this.lastTextTime >= this.dianjiJianGe)
			{
				this.lastText = text;
				this.lastTextTime = Time.time;
				GameObject gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load(ResManager.FuncUI_Path + "HUDText") as GameObject);
				Camera camera = NGUITools.FindCameraForLayer(tar.gameObject.layer);
				Vector3 position = camera.WorldToScreenPoint(tar.position);
				position.z = 0f;
				gameObject.transform.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(position);
				gameObject.GetComponent<HUDText>().Add(text, Color.red, 0.5f);
				gameObject.GetComponent<HUDText>().Target = tar;
				gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
				base.StartCoroutine(this.AAA(gameObject, 3f));
			}
		}
		catch (Exception var_3_11B)
		{
		}
	}

	public void SetBaoJiText(string text, Transform tar)
	{
		try
		{
			GameObject gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load(ResManager.FuncUI_Path + "BaoJi") as GameObject);
			Camera camera = NGUITools.FindCameraForLayer(tar.gameObject.layer);
			Vector3 position = camera.WorldToScreenPoint(tar.position);
			position.z = 0f;
			gameObject.transform.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(position);
			gameObject.transform.localPosition += new Vector3(0f, 5f, 0f);
			gameObject.GetComponentInChildren<UILabel>().text = text;
			UnityEngine.Object.Destroy(gameObject, 0.3f);
		}
		catch (Exception var_3_C9)
		{
		}
	}

	public GameObject SetOverText(string text, Transform tar, HUDTextTool.TextUITypeEnum txtType)
	{
		GameObject gameObject = null;
		switch (txtType)
		{
		case HUDTextTool.TextUITypeEnum.Num1:
			gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load("Num1Text") as GameObject);
			break;
		case HUDTextTool.TextUITypeEnum.Num2:
			gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load("Num2Text") as GameObject);
			break;
		case HUDTextTool.TextUITypeEnum.Num5:
			gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load("Num5Text") as GameObject);
			break;
		}
		gameObject.GetComponent<UILabel>().text = text;
		gameObject.AddComponent<OverTarget>().Target = tar;
		return gameObject;
	}

	public void SetText(string text, Transform tar, Color textColor, float times = 0.8f, int fontSize = 40)
	{
		if (!FightHundler.FightEnd)
		{
			if (this.lastText.Equals(text) && Time.time - this.lastTextTime < this.dianjiJianGe)
			{
				return;
			}
			this.lastText = text;
			this.lastTextTime = Time.time;
			GameObject gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load(ResManager.FuncUI_Path + "HUDText") as GameObject);
			Camera camera = NGUITools.FindCameraForLayer(tar.gameObject.layer);
			if (camera)
			{
				Vector3 position = camera.WorldToScreenPoint(tar.position);
				position.z = 0f;
				gameObject.transform.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(position);
				gameObject.GetComponent<HUDText>().fontSize = fontSize;
				gameObject.GetComponent<HUDText>().Add(text, textColor, 0.5f);
				gameObject.GetComponent<HUDText>().Target = tar;
				gameObject.GetComponent<HUDText>().CreateHurt();
				base.StartCoroutine(this.AAA(gameObject, times));
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public void SetText(string text, Transform tar, Color textColor, bool hurt)
	{
		if (!FightHundler.FightEnd)
		{
			GameObject gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load(ResManager.FuncUI_Path + "HUDText") as GameObject);
			Camera camera = NGUITools.FindCameraForLayer(tar.gameObject.layer);
			if (camera)
			{
				Vector3 position = camera.WorldToScreenPoint(tar.position);
				position.z = 0f;
				gameObject.transform.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(position);
				gameObject.GetComponent<HUDText>().fontSize = 40;
				gameObject.GetComponent<HUDText>().Add(text, textColor, 0.5f);
				gameObject.GetComponent<HUDText>().Target = tar;
				gameObject.GetComponent<HUDText>().CreateHurt();
				base.StartCoroutine(this.AAA(gameObject, 0.5f));
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public void SetResIconText(string text, Transform tar, float times, ResType resType)
	{
		if (!FightHundler.FightEnd)
		{
			if (this.lastText.Equals(text) && Time.time - this.lastTextTime < this.dianjiJianGe)
			{
				return;
			}
			this.lastText = text;
			this.lastTextTime = Time.time;
			GameObject gameObject = NGUITools.AddChild(this.TextTable.transform.parent.gameObject, Resources.Load("Num6Text") as GameObject);
			Camera camera = NGUITools.FindCameraForLayer(tar.gameObject.layer);
			if (camera)
			{
				Vector3 position = camera.WorldToScreenPoint(tar.position);
				position.z = 0f;
				gameObject.transform.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(position);
				gameObject.GetComponent<TipResIcon>().testLabel.text = text;
				switch (resType)
				{
				case ResType.金币:
					gameObject.GetComponent<TipResIcon>().resIcon.spriteName = "新金矿";
					break;
				case ResType.石油:
					gameObject.GetComponent<TipResIcon>().resIcon.spriteName = "新石油";
					break;
				case ResType.钢铁:
					gameObject.GetComponent<TipResIcon>().resIcon.spriteName = "新钢铁";
					break;
				case ResType.稀矿:
					gameObject.GetComponent<TipResIcon>().resIcon.spriteName = "新稀矿";
					break;
				}
				base.StartCoroutine(this.AAA(gameObject, times));
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator AAA(GameObject bb, float times)
	{
		HUDTextTool.<AAA>c__Iterator6D <AAA>c__Iterator6D = new HUDTextTool.<AAA>c__Iterator6D();
		<AAA>c__Iterator6D.bb = bb;
		<AAA>c__Iterator6D.times = times;
		<AAA>c__Iterator6D.<$>bb = bb;
		<AAA>c__Iterator6D.<$>times = times;
		return <AAA>c__Iterator6D;
	}

	public Vector3 GetCameraMoveEndPos(Vector3 tower, Vector3 camera, float angle)
	{
		float num = (40f - angle) * 0.18f + 9f;
		return new Vector3(tower.x + CameraControl.inst.Tr.position.y / Mathf.Tan(6.28f / num) / 1.414f, CameraControl.inst.Tr.position.y, tower.z + CameraControl.inst.Tr.position.y / Mathf.Tan(6.28f / num) / 1.414f);
	}

	public Vector3 GetCameraMoveEndPos2(Vector3 tower, Vector3 camera, float angle)
	{
		float num = (40f - angle) * 0.18f + 9f;
		return new Vector3(tower.x + CameraControl.inst.Tr.position.y / Mathf.Tan(6.28f / num) / 1.414f, CameraControl.inst.Tr.position.y, tower.z + CameraControl.inst.Tr.position.y / Mathf.Tan(6.28f / num) / 1.414f);
	}

	public void GetCenterPos()
	{
		this.isCenterPos = true;
		Vector3 position = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
		if (Camera.main)
		{
			Ray ray = Camera.main.ScreenPointToRay(position);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				this.centerPos = new Vector3((float)Mathf.RoundToInt(raycastHit.point.x), 0f, (float)Mathf.RoundToInt(raycastHit.point.z));
			}
		}
	}

	public void CreateHeTanUIPanbel(float time)
	{
		base.StartCoroutine(this.CreateHeTanUIPanbel_IE(time));
	}

	[DebuggerHidden]
	private IEnumerator CreateHeTanUIPanbel_IE(float time)
	{
		HUDTextTool.<CreateHeTanUIPanbel_IE>c__Iterator6E <CreateHeTanUIPanbel_IE>c__Iterator6E = new HUDTextTool.<CreateHeTanUIPanbel_IE>c__Iterator6E();
		<CreateHeTanUIPanbel_IE>c__Iterator6E.time = time;
		<CreateHeTanUIPanbel_IE>c__Iterator6E.<$>time = time;
		return <CreateHeTanUIPanbel_IE>c__Iterator6E;
	}

	public void CloseNewbiLua()
	{
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.HideSelf();
		}
		Camera_FingerManager.newbiLock = false;
		ButtonClick.newbiLock = false;
	}

	public void NextLuaCall(string CallLuaFrom, params object[] args)
	{
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.ClearDes();
		}
		if (GameSetting.isEditor)
		{
			NGUIDebug.Log(new object[]
			{
				string.Format("{0}调Lua 了·, NewbieGuideWrap.nextNewBi=={1} ,下一步 若引导是{2} 当前组ID：{3}", new object[]
				{
					CallLuaFrom,
					NewbieGuideWrap.nextNewBi,
					NewbieGuideWrap.optionNextNewBi,
					NewbieGuidePanel.guideIdByServer
				})
			});
		}
		LogManage.Log(string.Format("{0}调Lua 了·, NewbieGuideWrap.nextNewBi=={1} ,下一步 若引导是{2} 当前组ID：{3}", new object[]
		{
			CallLuaFrom,
			NewbieGuideWrap.nextNewBi,
			NewbieGuideWrap.optionNextNewBi,
			NewbieGuidePanel.guideIdByServer
		}));
		if (!string.IsNullOrEmpty(NewbieGuideWrap.nextNewBi) && GameManager.Instance.GetLuaManage() != null)
		{
			if (NewbieGuidePanel._instance)
			{
				NewbieGuidePanel._instance.HideSelf();
			}
			if (NewbieGuidePanel.curGuideIndex == -1)
			{
				GameManager.Instance.GetLuaManage().CallLuaFunction("StartFireLua." + NewbieGuideWrap.nextNewBi, args);
			}
			else
			{
				GameManager.Instance.GetLuaManage().CallLuaFunction("NewBi." + NewbieGuideWrap.nextNewBi, args);
			}
		}
	}

	[DebuggerHidden]
	public IEnumerator NextLuaCall_IE(string CallLuaFrom, params object[] args)
	{
		HUDTextTool.<NextLuaCall_IE>c__Iterator6F <NextLuaCall_IE>c__Iterator6F = new HUDTextTool.<NextLuaCall_IE>c__Iterator6F();
		<NextLuaCall_IE>c__Iterator6F.CallLuaFrom = CallLuaFrom;
		<NextLuaCall_IE>c__Iterator6F.args = args;
		<NextLuaCall_IE>c__Iterator6F.<$>CallLuaFrom = CallLuaFrom;
		<NextLuaCall_IE>c__Iterator6F.<$>args = args;
		return <NextLuaCall_IE>c__Iterator6F;
	}

	public static void CallLua(string LuaFunc, params object[] args)
	{
		if (GameManager.Instance.GetLuaManage() != null)
		{
			GameManager.Instance.GetLuaManage().CallLuaFunction(LuaFunc, args);
		}
	}

	public void NextLuaCallByIsEnemyAttck(string CallLuaFrom, params object[] args)
	{
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.ClearDes();
		}
		LogManage.Log(string.Format("{0}调Lua 了·, NewbieGuideWrap.nextNewBi=={1} ,下一步 若引导是{2} 当前组ID：{3}", new object[]
		{
			CallLuaFrom,
			NewbieGuideWrap.nextNewBi,
			NewbieGuideWrap.optionNextNewBi,
			NewbieGuidePanel.guideIdByServer
		}));
		if (GameSetting.isEditor)
		{
			NGUIDebug.Log(new object[]
			{
				string.Format("{0}调Lua 了·, NewbieGuideWrap.nextNewBi=={1} ,下一步 若引导是{2} 当前组ID：{3}", new object[]
				{
					CallLuaFrom,
					NewbieGuideWrap.nextNewBi,
					NewbieGuideWrap.optionNextNewBi,
					NewbieGuidePanel.guideIdByServer
				})
			});
		}
		if (!string.IsNullOrEmpty(NewbieGuideWrap.nextNewBi) && GameManager.Instance.GetLuaManage() != null)
		{
			if (NewbieGuidePanel._instance)
			{
				NewbieGuidePanel._instance.HideSelf();
			}
			if (NewbieGuidePanel.curGuideIndex == -1)
			{
				GameManager.Instance.GetLuaManage().CallLuaFunction("StartFireLua." + NewbieGuideWrap.nextNewBi, args);
			}
			else
			{
				GameManager.Instance.GetLuaManage().CallLuaFunction("NewBi." + NewbieGuideWrap.nextNewBi, args);
			}
		}
	}

	[DebuggerHidden]
	public IEnumerator NextLuaCallByIsEnemyAttck_IE(string CallLuaFrom, params object[] args)
	{
		HUDTextTool.<NextLuaCallByIsEnemyAttck_IE>c__Iterator70 <NextLuaCallByIsEnemyAttck_IE>c__Iterator = new HUDTextTool.<NextLuaCallByIsEnemyAttck_IE>c__Iterator70();
		<NextLuaCallByIsEnemyAttck_IE>c__Iterator.CallLuaFrom = CallLuaFrom;
		<NextLuaCallByIsEnemyAttck_IE>c__Iterator.args = args;
		<NextLuaCallByIsEnemyAttck_IE>c__Iterator.<$>CallLuaFrom = CallLuaFrom;
		<NextLuaCallByIsEnemyAttck_IE>c__Iterator.<$>args = args;
		return <NextLuaCallByIsEnemyAttck_IE>c__Iterator;
	}

	public void CreateNPCAttackUIPanbel(float time)
	{
		base.StartCoroutine(this.CreateNPCAttackUIPanbel_IE(time));
	}

	[DebuggerHidden]
	private IEnumerator CreateNPCAttackUIPanbel_IE(float time)
	{
		HUDTextTool.<CreateNPCAttackUIPanbel_IE>c__Iterator71 <CreateNPCAttackUIPanbel_IE>c__Iterator = new HUDTextTool.<CreateNPCAttackUIPanbel_IE>c__Iterator71();
		<CreateNPCAttackUIPanbel_IE>c__Iterator.time = time;
		<CreateNPCAttackUIPanbel_IE>c__Iterator.<$>time = time;
		<CreateNPCAttackUIPanbel_IE>c__Iterator.<>f__this = this;
		return <CreateNPCAttackUIPanbel_IE>c__Iterator;
	}
}
