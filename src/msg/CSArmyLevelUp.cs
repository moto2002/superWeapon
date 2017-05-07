using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSArmyLevelUp")]
	[Serializable]
	public class CSArmyLevelUp : IExtensible
	{
		private int _armyId = 0;

		private int _money = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
