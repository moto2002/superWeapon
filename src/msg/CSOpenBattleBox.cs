using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOpenBattleBox")]
	[Serializable]
	public class CSOpenBattleBox : IExtensible
	{
		private int _itemId = 0;

		private int _money = 0;

		private int _battleId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "battleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battleId
		{
			get
			{
				return this._battleId;
			}
			set
			{
				this._battleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
