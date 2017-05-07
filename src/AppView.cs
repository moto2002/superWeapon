using System;
using System.Collections.Generic;

public class AppView : View
{
	private string message;

	private List<string> MessageList
	{
		get
		{
			return new List<string>
			{
				"UpdateMessage",
				"UpdateExtract",
				"UpdateDownload",
				"UpdateProgress"
			};
		}
	}

	private void Awake()
	{
		base.RemoveMessage(this, this.MessageList);
		base.RegisterMessage(this, this.MessageList);
	}

	public override void OnMessage(IMessage message)
	{
		string name = message.Name;
		object body = message.Body;
		string text = name;
		switch (text)
		{
		case "UpdateMessage":
			this.UpdateMessage(body.ToString());
			break;
		case "UpdateExtract":
			this.UpdateExtract(body.ToString());
			break;
		case "UpdateDownload":
			this.UpdateDownload(body.ToString());
			break;
		case "UpdateProgress":
			this.UpdateProgress(body.ToString());
			break;
		}
	}

	public void UpdateMessage(string data)
	{
		this.message = data;
	}

	public void UpdateExtract(string data)
	{
		this.message = data;
	}

	public void UpdateDownload(string data)
	{
		this.message = data;
	}

	public void UpdateProgress(string data)
	{
		this.message = data;
	}

	private void OnGUI()
	{
	}
}
