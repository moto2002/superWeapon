using msg;
using System;

public class HeartbeatHandler
{
	public static void CG_Heartbeat()
	{
		CSHeartbeat cSHeartbeat = new CSHeartbeat();
		cSHeartbeat.tmp = 22L;
		cSHeartbeat.noticeTime = HeroInfo.GetInstance().gameAnnouncementData.lastNoticeTime;
		cSHeartbeat.flushRankHour = TopTenPanelManage.topTenRefrshTime;
		ClientMgr.GetNet().SendHttp(9000, cSHeartbeat, null, null);
	}
}
