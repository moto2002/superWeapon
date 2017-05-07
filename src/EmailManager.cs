using System;
using System.Collections.Generic;
using System.Linq;

public class EmailManager
{
	public List<EmailInfo> emailList = new List<EmailInfo>();

	private static EmailManager ins;

	public static EmailManager GetIns()
	{
		if (EmailManager.ins == null)
		{
			EmailManager.ins = HeroInfo.GetInstance().emilData;
		}
		return EmailManager.ins;
	}

	public static void ClearIns()
	{
		EmailManager.ins = null;
	}

	public void SortEmail()
	{
		this.emailList = (from p in this.emailList
		orderby p.time descending
		select p).ToList<EmailInfo>();
		if (this.emailList.Count > 50)
		{
			this.emailList.RemoveRange(50, this.emailList.Count - 50);
		}
	}

	public void ChangeMailState(long emailId, bool state)
	{
		EmailInfo emailById = this.GetEmailById(emailId);
		if (emailById != null)
		{
			emailById.isGetReward = state;
			emailById.isOpened = state;
			if (EmailPanel.ins != null)
			{
				EmailPanel.ins.RefreshAttachMentState(emailId);
			}
		}
	}

	public EmailInfo GetEmailById(long id)
	{
		EmailInfo result = null;
		for (int i = 0; i < this.emailList.Count; i++)
		{
			if (this.emailList[i].id == id)
			{
				result = this.emailList[i];
				break;
			}
		}
		return result;
	}

	public string GetTimeStr(EmailInfo thisEmail)
	{
		TimeSpan timeSpan = TimeTools.GetNowTimeSyncServerToDateTime() - TimeTools.ConvertLongDateTime(thisEmail.time);
		string result = string.Empty;
		if (timeSpan.TotalHours < 1.0)
		{
			result = LanguageManage.GetTextByKey("刚刚", "others");
		}
		else if (timeSpan.TotalHours < 24.0)
		{
			result = timeSpan.Hours + LanguageManage.GetTextByKey("小时前", "others");
		}
		else
		{
			result = timeSpan.Days + LanguageManage.GetTextByKey("天前", "others");
		}
		return result;
	}

	public bool ShowOneKeyReward()
	{
		bool result = false;
		for (int i = 0; i < this.emailList.Count; i++)
		{
			if ((this.emailList[i].resources.Count > 0 || this.emailList[i].items.Count > 0) && !this.emailList[i].isGetReward)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void OnEmailChange()
	{
		if (EmailPanel.ins != null && EmailPanel.ins.gameObject.activeInHierarchy)
		{
			EmailPanel.ins.Refresh();
		}
	}

	public bool HaveNotice()
	{
		bool result = false;
		for (int i = 0; i < this.emailList.Count; i++)
		{
			if (!this.emailList[i].isOpened)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
