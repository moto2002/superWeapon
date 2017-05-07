using System;

public class TimeEndCallBackStruct
{
	private int itemID;

	private DateTime timeEnd;

	private Action<int> callBack;

	public int ItemID
	{
		get
		{
			return this.itemID;
		}
		private set
		{
			this.itemID = value;
		}
	}

	public DateTime TimeEnd
	{
		get
		{
			return this.timeEnd;
		}
		private set
		{
			this.timeEnd = value;
		}
	}

	public Action<int> CallBack
	{
		get
		{
			return this.callBack;
		}
		private set
		{
			this.callBack = value;
		}
	}

	public TimeEndCallBackStruct(int _itemID, DateTime _timeEnd, Action<int> _CallBack)
	{
		this.itemID = _itemID;
		this.timeEnd = _timeEnd;
		this.callBack = _CallBack;
	}
}
