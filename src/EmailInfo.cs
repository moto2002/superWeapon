using msg;
using System;
using System.Collections.Generic;

public class EmailInfo
{
	public long id;

	public long time;

	public bool isOpened;

	public string title;

	public string content;

	public bool isGetReward;

	public List<KVStruct> resources = new List<KVStruct>();

	public List<KVStruct> items = new List<KVStruct>();

	public int zuanshiNum;

	public static EmailInfo _Instance;

	private void Awake()
	{
		EmailInfo._Instance = this;
	}
}
