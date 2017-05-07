using LitJson;
using msg;
using System;
using UnityEngine;

public class RandomPanelManager : FuncUIPanel
{
	public static RandomPanelManager ins;

	public UIInput inputValue;

	public void OnDestroy()
	{
		RandomPanelManager.ins = null;
	}

	public new void OnEnable()
	{
		this.showName();
		base.OnEnable();
	}

	public override void Awake()
	{
		RandomPanelManager.ins = this;
		this.init();
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.RandomNamePanel_RandomName, new EventManager.VoidDelegate(this.RandomNameClick));
		EventManager.Instance.AddEvent(EventManager.EventType.RandomNamePanel_RandomNameSure, new EventManager.VoidDelegate(this.RandomNameSure));
	}

	public void RandomNameClick(GameObject ga)
	{
		int num = UnityEngine.Random.Range(0, UnitConst.GetInstance().ReName_Name.Length);
		int num2 = UnityEngine.Random.Range(0, UnitConst.GetInstance().ReName_Name1.Length);
		this.inputValue.value = string.Format("{0}{1}", UnitConst.GetInstance().ReName_Name[num], UnitConst.GetInstance().ReName_Name1[num2]);
	}

	public void showName()
	{
		int num = UnityEngine.Random.Range(0, UnitConst.GetInstance().ReName_Name.Length);
		int num2 = UnityEngine.Random.Range(0, UnitConst.GetInstance().ReName_Name1.Length);
		this.inputValue.value = string.Format("{0}{1}", UnitConst.GetInstance().ReName_Name[num], UnitConst.GetInstance().ReName_Name1[num2]);
	}

	private void RandomNameSure(GameObject ga)
	{
		if (GameTools.CheckStringlength(this.inputValue.value) > 12)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("昵称长度不能超过12个字符", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
		else if (string.IsNullOrEmpty(this.inputValue.value))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("昵称长度不能为0", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			GameTools.CheckString(this.inputValue.value, out num, out num2, out num3, out num4, out num5);
			if (num2 == 0 && num == 0 && num4 == 0)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("名字必须包含数字或者字母", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
			else
			{
				CSReName cSReName = new CSReName();
				cSReName.name = this.inputValue.value;
				ClientMgr.GetNet().SendHttp(1012, cSReName, new DataHandler.OpcodeHandler(this.ReNameCallBack), null);
			}
		}
	}

	private void ReNameCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			JsonData jsonData = new JsonData();
			jsonData["accountId"] = HeroInfo.GetInstance().platformId;
			jsonData["level"] = HeroInfo.GetInstance().playerlevel.ToString();
			jsonData["serverId"] = User.GetServerName().ToString();
			jsonData["userid"] = HeroInfo.GetInstance().userId.ToString();
			jsonData["serverName"] = User.GetServerName().ToString();
			jsonData["roleName"] = HeroInfo.GetInstance().userName;
			jsonData["upType"] = "1";
			jsonData["serverTime"] = HeroInfo.GetInstance().createTime;
			HDSDKInit.UpLoadPlayerInfo(jsonData.ToJson());
			FuncUIManager.inst.DestoryFuncUI("RandomNamePanel");
			if (HUDTextTool.inst)
			{
				HUDTextTool.inst.StartCoroutine(HUDTextTool.inst.NextLuaCall_IE("删除重命名", new object[0]));
			}
		}
	}
}
