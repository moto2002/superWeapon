using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOfficerRecruit")]
	[Serializable]
	public class CSOfficerRecruit : IExtensible
	{
		private int _type = 0;

		private int _recruitType = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "recruitType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int recruitType
		{
			get
			{
				return this._recruitType;
			}
			set
			{
				this._recruitType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
