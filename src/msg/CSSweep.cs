using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSweep")]
	[Serializable]
	public class CSSweep : IExtensible
	{
		private int _battleId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "battleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battleId
		{
			get
			{
				return this._battleId;
			}
			set
			{
				this._battleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
