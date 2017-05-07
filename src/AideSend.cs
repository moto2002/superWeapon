using System;
using UnityEngine;

public class AideSend : MonoBehaviour
{
	public static AideSend _instance;

	public Transform dispatchBtn;

	public UILabel haveNum;

	public UILabel rayEarthCount;

	public UILabel sendCount;

	public UISprite back;

	public UISprite decomposedIcon;

	public UITexture peopleTexture;

	public UILabel name_Client;

	public UILabel Info;

	public GameObject CallBack;

	public GameObject send;

	public GameObject room;

	public GameObject bg;

	public GameObject close;

	public void OnDestroy()
	{
		AideSend._instance = null;
	}

	private void Awake()
	{
		AideSend._instance = this;
		this.ShowAideSend();
		this.dispatchBtn = base.transform.Find("Sprite");
	}

	public void ShowAideSend()
	{
		this.dispatchBtn = base.transform.FindChild("RightBG/send");
		this.bg = base.transform.FindChild("GameObject/bg").gameObject;
		this.bg.AddComponent<ButtonClick>();
		ButtonClick component = this.bg.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.none;
		this.close = base.transform.FindChild("close").gameObject;
		this.close.AddComponent<ButtonClick>();
		ButtonClick component2 = this.close.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.AdjutantPanle_Close;
		this.haveNum = base.transform.FindChild("haveNum").GetComponent<UILabel>();
		this.rayEarthCount = base.transform.FindChild("RightBG/send/Label2/rayEarthCount").GetComponent<UILabel>();
		this.sendCount = base.transform.FindChild("RightBG/MessageRoom/Label/sendCount").GetComponent<UILabel>();
		this.back = base.transform.FindChild("PeopleBG/PeopleQBG").GetComponent<UISprite>();
		this.decomposedIcon = base.transform.FindChild("RightBG/GetBack/Sprite2").GetComponent<UISprite>();
		this.peopleTexture = base.transform.FindChild("PeopleBG/People").GetComponent<UITexture>();
		this.name_Client = base.transform.FindChild("PeopleBG/botoom/PeopleName").GetComponent<UILabel>();
		this.Info = base.transform.FindChild("RightBG/send/Label2").GetComponent<UILabel>();
		this.send = base.transform.FindChild("RightBG/send/Sprite").gameObject;
		this.send.AddComponent<AdjutantBtn>();
		AdjutantBtn component3 = this.send.GetComponent<AdjutantBtn>();
		component3.btnType = AdjutantPanel.btnType.Send;
		this.room = base.transform.FindChild("RightBG/MessageRoom/Sprite").gameObject;
		this.room.AddComponent<AdjutantBtn>();
		AdjutantBtn component4 = this.room.GetComponent<AdjutantBtn>();
		component4.btnType = AdjutantPanel.btnType.GoInHome;
		this.CallBack = base.transform.FindChild("RightBG/GetBack/Sprite").gameObject;
		this.CallBack.AddComponent<AdjutantBtn>();
		AdjutantBtn component5 = this.CallBack.GetComponent<AdjutantBtn>();
		component5.btnType = AdjutantPanel.btnType.OpenCallBack;
	}

	private void Start()
	{
	}

	public void ShowAideSendData()
	{
		if (AdjutantPanelData.Aide_Send != null)
		{
			this.name_Client.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().AideConst[AdjutantPanelData.Aide_Send.aideId].name, "officer");
			this.Info.text = UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].description;
			AtlasManage.SetUiItemAtlas(this.decomposedIcon, UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().AideConst[AdjutantPanelData.Aide_Send.aideId].itemOfDecomposed].IconId);
			this.sendCount.text = AdjutantPanelData.Aide_ServerList.Count + "/" + UnitConst.GetInstance().buildingConst[14].lvInfos[HeroInfo.GetInstance().PlayerBuildingLevel[14]].outputNum;
			if (AdjutantPanelData.Aide_ServerList.Count >= UnitConst.GetInstance().buildingConst[14].lvInfos[HeroInfo.GetInstance().PlayerBuildingLevel[14]].outputNum)
			{
				FuncUIManager.inst.AdjutantPanel.isCanSend = false;
			}
			else
			{
				FuncUIManager.inst.AdjutantPanel.isCanSend = true;
			}
			HUDTextTool.inst.OnSetTextureIcon(this.peopleTexture, UnitConst.GetInstance().AideConst[AdjutantPanelData.Aide_Send.aideId].bigIcon, "dongyang/OfficerAtlas/Texture/");
			switch (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].abilitity)
			{
			case 1:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 1)
				{
					this.Info.text = LanguageManage.GetTextByKey("主基地金币产量", "officer");
				}
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 2)
				{
					this.Info.text = LanguageManage.GetTextByKey("作战图金币产量", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 2:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 1)
				{
					this.Info.text = LanguageManage.GetTextByKey("主基地石油产量", "officer");
				}
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 2)
				{
					this.Info.text = LanguageManage.GetTextByKey("作战图石油产量", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 3:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 1)
				{
					this.Info.text = LanguageManage.GetTextByKey("主基地钢铁产量", "officer");
				}
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 2)
				{
					this.Info.text = LanguageManage.GetTextByKey("作战图钢铁产量", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 4:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 1)
				{
					this.Info.text = LanguageManage.GetTextByKey("主基地稀矿产量", "officer");
				}
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 2)
				{
					this.Info.text = LanguageManage.GetTextByKey("作战图稀矿产量", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 5:
				this.Info.text = LanguageManage.GetTextByKey("作战图所有资源", "officer");
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 6:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 3)
				{
					this.Info.text = LanguageManage.GetTextByKey("掠夺玩家资源增加", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 7:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 3)
				{
					this.Info.text = LanguageManage.GetTextByKey("资源保护加成", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			case 8:
				if (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].aideType == 3)
				{
					this.Info.text = LanguageManage.GetTextByKey("弹药产量", "officer");
				}
				this.rayEarthCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.Aide_Send.abilityId].value + "%";
				break;
			}
			switch (UnitConst.GetInstance().AideConst[AdjutantPanelData.Aide_Send.aideId].level)
			{
			case 1:
				this.back.spriteName = "品质绿";
				break;
			case 2:
				this.back.spriteName = "品质蓝";
				break;
			case 3:
				this.back.spriteName = "品质紫";
				break;
			}
			this.CallBack.name = AdjutantPanelData.Aide_Send.id.ToString();
		}
	}
}
