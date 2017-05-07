using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSocketRegister")]
	[Serializable]
	public class CSSocketRegister : IExtensible
	{
		private long _playerId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "playerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long playerId
		{
			get
			{
				return this._playerId;
			}
			set
			{
				this._playerId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
