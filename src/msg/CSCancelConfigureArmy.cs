using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSCancelConfigureArmy")]
	[Serializable]
	public class CSCancelConfigureArmy : IExtensible
	{
		private int _armyId = 0;

		private long _buildingId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "armyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int armyId
		{
			get
			{
				return this._armyId;
			}
			set
			{
				this._armyId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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
