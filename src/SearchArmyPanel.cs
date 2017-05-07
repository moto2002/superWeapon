using System;
using UnityEngine;

public class SearchArmyPanel : MonoBehaviour
{
	public UIInput inputArmyName;

	public void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_SearchArmy, new EventManager.VoidDelegate(this.SearchArmyBtn));
	}

	private void SearchArmyBtn(GameObject ga)
	{
		if (GameTools.CheckStringlength(this.inputArmyName.value) > 12)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团名称长度不能超过12个字符", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (string.IsNullOrEmpty(this.inputArmyName.value))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军团名称长度不能为0", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		ArmyGroupHandler.CG_CSSearchLegion(this.inputArmyName.value, delegate(bool isError)
		{
			if (!isError)
			{
				ArmyGroupManager.isSearch = true;
				ArmyGroupManager.ins.showPanel(3);
				ArmyGroupManager.ins.ShowArmyGrop();
			}
		});
	}
}
