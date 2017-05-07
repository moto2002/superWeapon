using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSHeartbeat")]
	[Serializable]
	public class CSHeartbeat : IExtensible
	{
		private long _tmp = 0L;

		private long _noticeTime = 0L;

		private long _chatTime = 0L;

		private int _flushRankHour = 0;

		private long _activityTime = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "tmp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long tmp
		{
			get
			{
				return this._tmp;
			}
			set
			{
				this._tmp = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "noticeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long noticeTime
		{
			get
			{
				return this._noticeTime;
			}
			set
			{
				this._noticeTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "chatTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long chatTime
		{
			get
			{
				return this._chatTime;
			}
			set
			{
				this._chatTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "flushRankHour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flushRankHour
		{
			get
			{
				return this._flushRankHour;
			}
			set
			{
				this._flushRankHour = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "activityTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long activityTime
		{
			get
			{
				return this._activityTime;
			}
			set
			{
				this._activityTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
