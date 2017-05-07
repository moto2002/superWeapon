using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCRecruitInfoData")]
	[Serializable]
	public class SCRecruitInfoData : IExtensible
	{
		private long _flushTime = 0L;

		private int _remainTimes = 0;

		private long _id = 0L;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "flushTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long flushTime
		{
			get
			{
				return this._flushTime;
			}
			set
			{
				this._flushTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "remainTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTimes
		{
			get
			{
				return this._remainTimes;
			}
			set
			{
				this._remainTimes = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
