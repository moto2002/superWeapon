using System;

public struct LanguageCategory
{
	private string _id;

	private string xmlName;

	private string uiName;

	public string Id
	{
		get
		{
			return this._id;
		}
		set
		{
			this._id = value;
		}
	}

	public string XmlName
	{
		get
		{
			return this.xmlName;
		}
		set
		{
			this.xmlName = value;
		}
	}

	public string UiName
	{
		get
		{
			return this.uiName;
		}
		set
		{
			this.uiName = value;
		}
	}
}
