using System;
using UnityEngine;

public class NewbiSkip : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnClick()
	{
		MessagePanelManage messagePanel = MessageBox.GetMessagePanel();
		messagePanel.messageBoxCamera.depth = 10f;
		messagePanel.gameObject.SetActive(false);
		messagePanel.gameObject.SetActive(true);
		messagePanel.ShowBtn(string.Empty, LanguageManage.GetTextByKey("引导", "others"), LanguageManage.GetTextByKey("确定", "others"), new Action(this.SkipNewBi), LanguageManage.GetTextByKey("取消", "others"), null);
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn1Click].GetComponent<ButtonClick>().isSendLua = false;
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn2Click].GetComponent<ButtonClick>().isSendLua = false;
	}

	private void SkipNewBi()
	{
		NewbieGuidePanel.guideIdByServer = 10000;
		NewbieGuideManage._instance.CS_NewGuide(GameSetting.MaxLuaProcess + 1);
		HUDTextTool.inst.CloseNewbiLua();
		if (!NewbieGuidePanel.isEnemyAttck && NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.GoHome();
		}
		if (string.IsNullOrEmpty(HeroInfo.GetInstance().userName) || HeroInfo.GetInstance().userName.Equals(HeroInfo.GetInstance().userName_Default))
		{
			FuncUIManager.inst.OpenFuncUI("RandomNamePanel", SenceType.Island);
		}
	}
}
