using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSocketHeat")]
	[Serializable]
	public class CSSocketHeat : IExtensible
	{
		private long _playerid = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "playerid", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long playerid
		{
			get
			{
				return this._playerid;
			}
			set
			{
				this._playerid = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
