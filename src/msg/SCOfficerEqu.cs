using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOfficerEqu")]
	[Serializable]
	public class SCOfficerEqu : IExtensible
	{
		private long _id = 0L;

		private int _equId = 0;

		private long _officerId = 0L;

		private int _type = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "equId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equId
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

		[ProtoMember(3, IsRequired = false, Name = "officerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
