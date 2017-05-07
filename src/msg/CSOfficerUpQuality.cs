using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOfficerUpQuality")]
	[Serializable]
	public class CSOfficerUpQuality : IExtensible
	{
		private long _officerId = 0L;

		private int _money = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "officerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
