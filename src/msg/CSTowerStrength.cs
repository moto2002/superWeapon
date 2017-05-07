using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSTowerStrength")]
	[Serializable]
	public class CSTowerStrength : IExtensible
	{
		private long _buildingId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long buildingId
		{
			get
			{
				return this._buildingId;
			}
			set
			{
				this._buildingId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
