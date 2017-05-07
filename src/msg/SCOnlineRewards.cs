using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOnlineRewards")]
	[Serializable]
	public class SCOnlineRewards : IExtensible
	{
		private long _id = 0L;

		private int _step = 0;

		private long _nextRewardTime = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nextRewardTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long nextRewardTime
		{
			get
			{
				return this._nextRewardTime;
			}
			set
			{
				this._nextRewardTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
