using System;

namespace NetWork.Layer
{
	public class PacketNew
	{
		public int cmd;

		public int user;

		public int reqid;

		public string session;

		public object kBody;

		public PacketNew(int _cmd, int _user, int _reqid, string _session, object buff)
		{
			this.cmd = _cmd;
			this.user = _user;
			this.reqid = _reqid;
			this.session = _session;
			this.kBody = buff;
		}
	}
}
