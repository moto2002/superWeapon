using DicForUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AideRecover : MonoBehaviour
{
	public UISprite ItemSprite;

	public UISprite itemBg;

	public UISprite peoBg;

	public UITexture peopleTexture;

	public UILabel itemLabel;

	public UILabel peopleName;

	public GameObject CallBack;

	public GameObject cancle;

	public GameObject close;

	public static AideRecover _ins;

	public List<Aide> aideList = new List<Aide>();

	public List<Item> itemList = new List<Item>();

	public void OnDestroy()
	{
		AideRecover._ins = null;
	}

	private void Awake()
	{
		AideRecover._ins = this;
		this.ShowAideRecover();
	}

	private void Start()
	{
	}

	public void ShowAideRecover()
	{
		this.ItemSprite = base.transform.FindChild("GameObject/Sprite2/QualityBg/item").GetComponent<UISprite>();
		this.itemBg = base.transform.FindChild("GameObject/Sprite2/QualityBg").GetComponent<UISprite>();
		this.peoBg = base.transform.FindChild("Sprite/PeopleQualitySprite").GetComponent<UISprite>();
		this.peopleTexture = base.transform.FindChild("Sprite/Sprite").GetComponent<UITexture>();
		this.itemLabel = base.transform.FindChild("GameObject/Sprite2/QualityBg/item/Label").GetComponent<UILabel>();
		this.peopleName = base.transform.FindChild("Sprite/playerNameBG/name").GetComponent<UILabel>();
		this.CallBack = base.transform.FindChild("回收").gameObject;
		this.CallBack.AddComponent<AdjutantBtn>();
		AdjutantBtn component = this.CallBack.GetComponent<AdjutantBtn>();
		component.btnType = AdjutantPanel.btnType.CallBack;
		this.cancle = base.transform.FindChild("取消").gameObject;
		this.cancle.AddComponent<ButtonClick>();
		ButtonClick component2 = this.cancle.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.AidePanel_Cancle;
		this.close = base.transform.FindChild("close").gameObject;
		this.close.AddComponent<ButtonClick>();
		ButtonClick component3 = this.close.GetComponent<ButtonClick>();
		component3.eventType = EventManager.EventType.AdjutantPanle_Close;
	}

	public void ShowAideRecoer(AdjutantPanelData.AideData aide)
	{
		this.CallBack.name = aide.id.ToString();
		HUDTextTool.inst.OnSetTextureIcon(this.peopleTexture, UnitConst.GetInstance().AideConst[aide.aideId].bigIcon, "dongyang/OfficerAtlas/Texture/");
		AtlasManage.SetUiItemAtlas(this.ItemSprite, UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().AideConst[aide.aideId].itemOfDecomposed].IconId);
		this.ItemSprite.gameObject.AddComponent<ItemTipsShow2>();
		this.ItemSprite.GetComponent<ItemTipsShow2>().Index = UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().AideConst[aide.aideId].itemOfDecomposed].Id;
		this.ItemSprite.GetComponent<ItemTipsShow2>().Type = 1;
		this.ItemSprite.GetComponent<ItemTipsShow2>().JianTouPostion = 4;
		this.itemLabel.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().AideConst[aide.aideId].itemOfDecomposed].Name, "item");
		this.peopleName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().AideConst[aide.aideId].name, "officer");
		this.itemBg.spriteName = this.SetQuSprite((int)UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().AideConst[aide.aideId].itemOfDecomposed].Quality);
		switch (UnitConst.GetInstance().AideConst[aide.aideId].level)
		{
		case 1:
			this.peoBg.spriteName = "品质绿";
			break;
		case 2:
			this.peoBg.spriteName = "品质蓝";
			break;
		case 3:
			this.peoBg.spriteName = "品质紫";
			break;
		}
		DicForU.GetValues<int, Item>(UnitConst.GetInstance().ItemConst, this.itemList);
		DicForU.GetValues<int, Aide>(UnitConst.GetInstance().AideConst, this.aideList);
	}

	public string SetQuSprite(int quaty)
	{
		switch (quaty)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}
}
