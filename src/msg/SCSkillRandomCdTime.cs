using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSkillRandomCdTime")]
	[Serializable]
	public class SCSkillRandomCdTime : IExtensible
	{
		private long _id = 0L;

		private long _normalTime = 0L;

		private long _legendTime = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "normalTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long normalTime
		{
			get
			{
				return this._normalTime;
			}
			set
			{
				this._normalTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "legendTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legendTime
		{
			get
			{
				return this._legendTime;
			}
			set
			{
				this._legendTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
