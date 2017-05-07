using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCActivityData")]
	[Serializable]
	public class SCActivityData : IExtensible
	{
		private long _id = 0L;

		private readonly List<int> _dailyNpcIds = new List<int>();

		private int _refreshDailyActivityTimes = 0;

		private long _dailyActivityEndTime = 0L;

		private int _attackDailyActivityTimes = 0;

		private int _weekNpcId = 0;

		private long _weekActivityEndTime = 0L;

		private long _resetTimes = 0L;

		private int _weekActivityLastNpcId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
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

		[ProtoMember(2, Name = "dailyNpcIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> dailyNpcIds
		{
			get
			{
				return this._dailyNpcIds;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "refreshDailyActivityTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshDailyActivityTimes
		{
			get
			{
				return this._refreshDailyActivityTimes;
			}
			set
			{
				this._refreshDailyActivityTimes = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dailyActivityEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long dailyActivityEndTime
		{
			get
			{
				return this._dailyActivityEndTime;
			}
			set
			{
				this._dailyActivityEndTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "attackDailyActivityTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attackDailyActivityTimes
		{
			get
			{
				return this._attackDailyActivityTimes;
			}
			set
			{
				this._attackDailyActivityTimes = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "weekNpcId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weekNpcId
		{
			get
			{
				return this._weekNpcId;
			}
			set
			{
				this._weekNpcId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "weekActivityEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long weekActivityEndTime
		{
			get
			{
				return this._weekActivityEndTime;
			}
			set
			{
				this._weekActivityEndTime = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "resetTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long resetTimes
		{
			get
			{
				return this._resetTimes;
			}
			set
			{
				this._resetTimes = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "weekActivityLastNpcId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weekActivityLastNpcId
		{
			get
			{
				return this._weekActivityLastNpcId;
			}
			set
			{
				this._weekActivityLastNpcId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
