using msg;
using System;
using System.Collections.Generic;

public class GameAnnouncementData
{
	public Dictionary<long, NoticeData> showText = new Dictionary<long, NoticeData>();

	public bool isHaveNewAnounce;

	public long lastNoticeTime;
}
