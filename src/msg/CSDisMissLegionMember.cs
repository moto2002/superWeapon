using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSDisMissLegionMember")]
	[Serializable]
	public class CSDisMissLegionMember : IExtensible
	{
		private long _legionId = 0L;

		private long _targetId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionId
		{
			get
			{
				return this._legionId;
			}
			set
			{
				this._legionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "targetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
