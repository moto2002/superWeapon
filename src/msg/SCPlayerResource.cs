using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerResource")]
	[Serializable]
	public class SCPlayerResource : IExtensible
	{
		private long _id = 0L;

		private int _resId = 0;

		private int _resVal = 0;

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

		[ProtoMember(4, IsRequired = false, Name = "resId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resId
		{
			get
			{
				return this._resId;
			}
			set
			{
				this._resId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "resVal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resVal
		{
			get
			{
				return this._resVal;
			}
			set
			{
				this._resVal = value;
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
