using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSLegionJobUpOrDown")]
	[Serializable]
	public class CSLegionJobUpOrDown : IExtensible
	{
		private long _legionId = 0L;

		private long _targetId = 0L;

		private int _type = 0;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
