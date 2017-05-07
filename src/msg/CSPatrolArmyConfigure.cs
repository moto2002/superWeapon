using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSPatrolArmyConfigure")]
	[Serializable]
	public class CSPatrolArmyConfigure : IExtensible
	{
		private long _buildingId = 0L;

		private int _money = 0;

		private int _waves = 0;

		private int _configureType = 0;

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

		[ProtoMember(3, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "waves", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int waves
		{
			get
			{
				return this._waves;
			}
			set
			{
				this._waves = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "configureType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int configureType
		{
			get
			{
				return this._configureType;
			}
			set
			{
				this._configureType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
