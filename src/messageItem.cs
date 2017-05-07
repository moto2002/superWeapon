using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class messageItem : MonoBehaviour
{
	public static Queue<messageItem> systemLimit = new Queue<messageItem>();

	public static Queue<messageItem> famliyLimit = new Queue<messageItem>();

	public static Queue<messageItem> worldLimit = new Queue<messageItem>();

	public static int maxInt = 50;

	public GameObject lv;

	public UILabel name_Clinet;

	public UILabel text;

	public UILabel time;

	public SCSendMessage curChatData;

	private void Start()
	{
	}

	public void Show(SCSendMessage chatData)
	{
		this.curChatData = chatData;
		if (chatData.msg.channel == 4)
		{
			this.name_Clinet.text = LanguageManage.GetTextByKey("系统消息", "others");
			this.name_Clinet.color = Color.red;
			this.text.text = chatData.msg.message;
			this.text.color = Color.green;
			this.time.text = TimeTools.ConvertLongDateTime(chatData.id).ToString();
			messageItem.systemLimit.Enqueue(this);
			if (messageItem.systemLimit.Count > messageItem.maxInt)
			{
				messageItem messageItem = messageItem.systemLimit.Dequeue();
				if (messageItem != null)
				{
					UnityEngine.Object.Destroy(messageItem.gameObject);
				}
			}
			if (MessagePanel.messageState != 4)
			{
				base.gameObject.SetActive(false);
			}
		}
		else if (chatData.msg.channel == 0)
		{
			this.name_Clinet.text = chatData.msg.senderName;
			this.text.text = chatData.msg.message;
			this.time.text = TimeTools.ConvertLongDateTime(chatData.id).ToString();
			if (chatData.msg.senderId == HeroInfo.GetInstance().userId)
			{
				this.name_Clinet.text = "您";
				this.name_Clinet.color = Color.green;
				this.text.color = Color.green;
			}
			messageItem.worldLimit.Enqueue(this);
			if (messageItem.worldLimit.Count > messageItem.maxInt)
			{
				messageItem messageItem2 = messageItem.worldLimit.Dequeue();
				if (messageItem2 != null)
				{
					UnityEngine.Object.Destroy(messageItem2.gameObject);
				}
			}
			if (MessagePanel.messageState != 0)
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			this.name_Clinet.text = chatData.msg.senderName;
			this.text.text = chatData.msg.message;
			this.time.text = TimeTools.ConvertLongDateTime(chatData.id).ToString();
			if (chatData.msg.senderId == HeroInfo.GetInstance().userId)
			{
				this.name_Clinet.text = "您";
				this.name_Clinet.color = Color.green;
				this.text.color = Color.green;
			}
			messageItem.famliyLimit.Enqueue(this);
			if (messageItem.famliyLimit.Count > messageItem.maxInt)
			{
				messageItem messageItem3 = messageItem.famliyLimit.Dequeue();
				if (messageItem3 != null)
				{
					UnityEngine.Object.Destroy(messageItem3.gameObject);
				}
			}
			if (MessagePanel.messageState != 1)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
