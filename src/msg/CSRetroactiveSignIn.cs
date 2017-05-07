using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSRetroactiveSignIn")]
	[Serializable]
	public class CSRetroactiveSignIn : IExtensible
	{
		private int _day = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "day", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int day
		{
			get
			{
				return this._day;
			}
			set
			{
				this._day = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
