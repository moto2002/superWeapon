using msg;
using System;
using System.Collections.Generic;

public class EmailHandler
{
	private static Action<bool> getAwardEmail;

	public static void CSReadMail(long emailId)
	{
		CSReadMail cSReadMail = new CSReadMail();
		cSReadMail.id = emailId;
		ClientMgr.GetNet().SendHttp(1800, cSReadMail, null, null);
	}

	public static void CSReceiveMailAttachment(long emailId, Action<bool> func = null)
	{
		EmailHandler.getAwardEmail = func;
		CSReceiveMailAttachment cSReceiveMailAttachment = new CSReceiveMailAttachment();
		cSReceiveMailAttachment.id = emailId;
		ClientMgr.GetNet().SendHttp(1802, cSReceiveMailAttachment, new DataHandler.OpcodeHandler(EmailHandler.OnHettpNode), null);
	}

	public static void SCPlayerMailStatus(bool isError, Opcode opcode)
	{
		List<SCPlayerMailStatus> list = opcode.Get<SCPlayerMailStatus>(10056);
		foreach (SCPlayerMailStatus current in list)
		{
			EmailManager.GetIns().ChangeMailState(current.id, current.isReceived);
		}
	}

	public static void OnHettpNode(bool Error, Opcode opcode)
	{
		if (EmailHandler.getAwardEmail != null)
		{
			EmailHandler.getAwardEmail(Error);
		}
	}
}
