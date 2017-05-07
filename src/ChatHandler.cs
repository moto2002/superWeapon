using msg;
using System;

public class ChatHandler
{
	public static void CG_Chat(string message, int type)
	{
		LogManage.Log("发送了· · ·  · ·" + DateTime.Now);
		CSSendMessage cSSendMessage = new CSSendMessage();
		cSSendMessage.message = new msg.ChatMessage();
		cSSendMessage.message.channel = type;
		cSSendMessage.message.message = message;
		cSSendMessage.message.senderId = HeroInfo.GetInstance().userId;
		ClientMgr.GetNet().SendSorcket(30100, cSSendMessage);
	}
}
