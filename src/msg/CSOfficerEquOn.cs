using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOfficerEquOn")]
	[Serializable]
	public class CSOfficerEquOn : IExtensible
	{
		private long _equId = 0L;

		private long _officerId = 0L;

		private int _equLoc = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "equId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long equId
		{
			get
			{
				return this._equId;
			}
			set
			{
				this._equId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "officerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long officerId
		{
			get
			{
				return this._officerId;
			}
			set
			{
				this._officerId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "equLoc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equLoc
		{
			get
			{
				return this._equLoc;
			}
			set
			{
				this._equLoc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
