using System;
using UnityEngine;

public class ItemTips1 : MonoBehaviour
{
	public UISprite itemIcon;

	public UISprite resIcon;

	public UISprite skillIcon;

	public UISprite quity;

	public UILabel name_Client;

	public UILabel type;

	public UILabel Des;

	public GameObject panel;

	public GameObject[] JianTou;

	public UILabel power;

	private void Start()
	{
	}

	public void OnDown(GameObject ga, int Index, int Type, int JianTouPostion)
	{
		if (Type == 1)
		{
			if (!UnitConst.GetInstance().ItemConst.ContainsKey(Index))
			{
				return;
			}
			base.gameObject.SetActive(true);
			this.power.gameObject.SetActive(false);
			this.resIcon.gameObject.SetActive(false);
			this.skillIcon.gameObject.SetActive(false);
			this.itemIcon.gameObject.SetActive(true);
			this.type.text = LanguageManage.GetTextByKey("道具", "others");
			AtlasManage.SetUiItemAtlas(this.itemIcon, UnitConst.GetInstance().ItemConst[Index].IconId);
			this.name_Client.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[Index].Name, "item");
			this.Des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[Index].Description, "item");
			AtlasManage.SetQuilitySpriteName(this.quity, UnitConst.GetInstance().ItemConst[Index].Quality);
		}
		else if (Type == 2)
		{
			base.gameObject.SetActive(true);
			this.power.gameObject.SetActive(false);
			this.resIcon.gameObject.SetActive(true);
			this.skillIcon.gameObject.SetActive(false);
			this.itemIcon.gameObject.SetActive(false);
			this.type.text = LanguageManage.GetTextByKey("资源", "others");
			AtlasManage.SetResSpriteName(this.resIcon, (ResType)Index);
			this.name_Client.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ResDes[Index].resName, "ResIsland");
			this.Des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ResDes[Index].resDesciption, "ResIsland");
			AtlasManage.SetQuilitySpriteName(this.quity, Quility_ResAndItemAndSkill.绿);
		}
		else if (Type == 3)
		{
			base.gameObject.SetActive(true);
			this.power.gameObject.SetActive(false);
			this.resIcon.gameObject.SetActive(false);
			this.skillIcon.gameObject.SetActive(true);
			this.itemIcon.gameObject.SetActive(false);
			this.type.text = LanguageManage.GetTextByKey("技能", "others");
			AtlasManage.SetSkillSpritName(this.skillIcon, UnitConst.GetInstance().skillList[Index].icon);
			this.name_Client.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillList[Index].name, "skill");
			this.Des.text = UnitConst.GetInstance().skillList[Index].Description;
			AtlasManage.SetQuilitySpriteName(this.quity, UnitConst.GetInstance().skillList[Index].skillQuality);
		}
		Camera camera = NGUITools.FindCameraForLayer(ga.layer);
		Vector3 position = camera.WorldToScreenPoint(ga.transform.position);
		Vector3 a = HUDTextTool.inst.hudtextCamera.ScreenToWorldPoint(position);
		switch (JianTouPostion)
		{
		case 1:
			this.JianTou[0].SetActive(true);
			this.panel.transform.position = a + new Vector3(0f, -0.45f, 0f);
			break;
		case 2:
			this.JianTou[1].SetActive(true);
			this.panel.transform.position = a + new Vector3(0f, 0.45f, 0f);
			break;
		case 3:
			this.JianTou[2].SetActive(true);
			this.panel.transform.position = a + new Vector3(0.6f, 0f, 0f);
			break;
		case 4:
			this.JianTou[3].SetActive(true);
			this.panel.transform.position = a + new Vector3(-0.6f, 0f, 0f);
			break;
		}
	}

	public void OnUp()
	{
		for (int i = 0; i < this.JianTou.Length; i++)
		{
			this.JianTou[i].SetActive(false);
		}
		base.gameObject.SetActive(false);
	}
}
