using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSArmyLevelUpEnd")]
	[Serializable]
	public class CSArmyLevelUpEnd : IExtensible
	{
		private int _itemId = 0;

		private int _isClear = 0;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "isClear", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isClear
		{
			get
			{
				return this._isClear;
			}
			set
			{
				this._isClear = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
