using System;
using UnityEngine;

public class EditLegionTipPanel : MonoBehaviour
{
	public UIInput editLegionTip;

	public GameObject btnSure;

	public GameObject bg;

	public GameObject btnCancle;

	public void Start()
	{
		UIEventListener.Get(this.btnSure).onClick = delegate(GameObject ga)
		{
			if (GameTools.CheckStringlength(this.editLegionTip.value) > 140)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团公告长度不能超过140个字符", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			ArmyGroupHandler.CG_CSModifyLegionInfo(HeroInfo.GetInstance().playerGroupId, 3, this.editLegionTip.value, delegate(bool isError)
			{
				if (!isError)
				{
					base.gameObject.SetActive(false);
				}
			});
		};
		this.BtnCancleClcik();
		this.BtnBackClick();
	}

	private void BtnCancleClcik()
	{
		UIEventListener.Get(this.btnCancle).onClick = delegate(GameObject ga)
		{
			base.gameObject.SetActive(false);
		};
	}

	private void BtnBackClick()
	{
		UIEventListener.Get(this.bg).onClick = delegate(GameObject ga)
		{
			base.gameObject.SetActive(false);
		};
	}
}
