using System;
using UnityEngine;

public class AdjutantPanel : FuncUIPanel
{
	public enum btnType
	{
		Send,
		CallBack,
		GoInHome,
		Close,
		Intensify,
		OpenCallBack
	}

	public GameObject aideCompound;

	public GameObject aideList;

	public GameObject aideSend;

	public GameObject aideRecover;

	public static AdjutantPanel ins;

	[HideInInspector]
	public bool isCanSend;

	public static bool isOk;

	[HideInInspector]
	public static bool isCanOpen = true;

	public void OnDestroy()
	{
		AdjutantPanel.ins = null;
	}

	public override void Awake()
	{
		AdjutantPanel.ins = this;
		this.aideCompound = base.transform.FindChild("副官合成").gameObject;
		this.aideCompound.AddComponent<AideCompound>();
		this.aideList = base.transform.FindChild("副官").gameObject;
		this.aideList.AddComponent<AideList>();
		this.aideSend = base.transform.FindChild("副官派遣").gameObject;
		this.aideSend.AddComponent<AideSend>();
		this.aideRecover = base.transform.FindChild("副官回收").gameObject;
		this.aideRecover.AddComponent<AideRecover>();
		EventManager.Instance.AddEvent(EventManager.EventType.AidePanel_Cancle, new EventManager.VoidDelegate(this.CancleCallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.AdjutantPanle_Close, new EventManager.VoidDelegate(this.CloseAdjutant));
	}

	public void CancleCallBack(GameObject go)
	{
		this.aideRecover.gameObject.SetActive(false);
		this.aideList.gameObject.SetActive(true);
		AideList._ins.ShowAideData_Server();
	}

	public void CloseAdjutant(GameObject go)
	{
		FuncUIManager.inst.HideFuncUI("AdjutantPanel");
	}

	public void ShowAideData(bool isGoIn)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		UIManager.inst.UIInUsed(true);
		this.DisplayAll();
		if (isGoIn)
		{
			this.aideList.SetActive(true);
			this.aideList.GetComponent<AideList>().ShowAideData_Server();
		}
		else if (AdjutantPanelData.Aide_Send != null)
		{
			this.aideSend.SetActive(true);
			this.aideList.SetActive(false);
			this.aideSend.GetComponent<AideSend>().ShowAideSendData();
		}
		else
		{
			this.aideCompound.SetActive(true);
			this.aideCompound.GetComponent<AideCompound>().ShowAideCompound();
		}
	}

	private void DisplayAll()
	{
		this.aideCompound.SetActive(false);
		this.aideList.SetActive(false);
		this.aideSend.SetActive(false);
		this.aideRecover.SetActive(false);
	}

	public void EventBtnType(GameObject ga, AdjutantPanel.btnType type)
	{
		switch (type)
		{
		case AdjutantPanel.btnType.Send:
			if (this.isCanSend)
			{
				AideHandler.CG_AideSend();
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("当前已达上限，升级情报分析室可派遣更多副官哦!", "officer"), HUDTextTool.TextUITypeEnum.Num5);
			}
			break;
		case AdjutantPanel.btnType.CallBack:
			AideHandler.CG_AideRecycle(int.Parse(ga.name));
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("召回副官获得", "officer") + LanguageManage.GetTextByKey(AideRecover._ins.itemLabel.text, "item"), HUDTextTool.TextUITypeEnum.Num1);
			if (AdjutantPanelData.Aide_Send != null)
			{
				this.ShowAideData(false);
			}
			else
			{
				this.ShowAideData(true);
			}
			break;
		case AdjutantPanel.btnType.GoInHome:
			this.ShowAideData(true);
			break;
		case AdjutantPanel.btnType.Close:
			this.aideList.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
			break;
		case AdjutantPanel.btnType.Intensify:
			AdjutantPanel.isOk = false;
			if (ga.GetComponentInParent<AideData_Server>().isCanIntensify)
			{
				AideHandler.CG_AideIntensify(int.Parse(ga.name));
				AdjutantPanel.isOk = true;
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您对副官", "officer") + LanguageManage.GetTextByKey(ga.GetComponentInParent<AideData_Server>().name_Client.text, "officer") + LanguageManage.GetTextByKey("使用了强化剂 资源加成效果加强", "officer"), HUDTextTool.TextUITypeEnum.Num1);
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您的强化剂数量不足!", "item"), HUDTextTool.TextUITypeEnum.Num5);
			}
			break;
		case AdjutantPanel.btnType.OpenCallBack:
			this.DisplayAll();
			this.aideRecover.SetActive(true);
			if (AdjutantPanelData.Aide_ServerList.ContainsKey(int.Parse(ga.name)))
			{
				this.aideRecover.GetComponent<AideRecover>().ShowAideRecoer(AdjutantPanelData.Aide_ServerList[int.Parse(ga.name)]);
			}
			else
			{
				this.aideRecover.GetComponent<AideRecover>().ShowAideRecoer(AdjutantPanelData.Aide_Send);
			}
			break;
		}
	}

	public override void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10028)
		{
			if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(AdjutantPanelData.endTime)) <= 0.0)
			{
				if (AdjutantPanelData.Aide_Send != null && !AideData_Server._ins.isCanIntensify)
				{
					this.ShowAideData(false);
				}
				else if (AdjutantPanelData.Aide_Send != null)
				{
					this.ShowAideData(false);
				}
				else
				{
					this.ShowAideData(true);
				}
			}
			return;
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}
}
