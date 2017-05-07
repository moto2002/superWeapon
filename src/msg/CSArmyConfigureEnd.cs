using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSArmyConfigureEnd")]
	[Serializable]
	public class CSArmyConfigureEnd : IExtensible
	{
		private long _buildingId = 0L;

		private int _armyId = 0;

		private int _isRightNow = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "armyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "isRightNow", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isRightNow
		{
			get
			{
				return this._isRightNow;
			}
			set
			{
				this._isRightNow = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
