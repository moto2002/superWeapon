using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCLegionPVE")]
	[Serializable]
	public class SCLegionPVE : IExtensible
	{
		private long _id = 0L;

		private int _curBattleId = 0;

		private int _times = 0;

		private long _flushTimes = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "curBattleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curBattleId
		{
			get
			{
				return this._curBattleId;
			}
			set
			{
				this._curBattleId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "flushTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long flushTimes
		{
			get
			{
				return this._flushTimes;
			}
			set
			{
				this._flushTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
