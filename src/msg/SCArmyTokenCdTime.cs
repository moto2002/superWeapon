using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCArmyTokenCdTime")]
	[Serializable]
	public class SCArmyTokenCdTime : IExtensible
	{
		private long _id = 0L;

		private long _armyTokenCdTime = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "armyTokenCdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long armyTokenCdTime
		{
			get
			{
				return this._armyTokenCdTime;
			}
			set
			{
				this._armyTokenCdTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
