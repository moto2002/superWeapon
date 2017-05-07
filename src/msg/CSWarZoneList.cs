using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSWarZoneList")]
	[Serializable]
	public class CSWarZoneList : IExtensible
	{
		private int _zoneId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "zoneId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int zoneId
		{
			get
			{
				return this._zoneId;
			}
			set
			{
				this._zoneId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
