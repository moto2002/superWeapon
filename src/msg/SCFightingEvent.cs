using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCFightingEvent")]
	[Serializable]
	public class SCFightingEvent : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _dropItem = new List<KVStruct>();

		private readonly List<KVStruct> _dropEquip = new List<KVStruct>();

		private readonly List<KVStruct> _buyArmyTimes = new List<KVStruct>();

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

		[ProtoMember(2, Name = "dropItem", DataFormat = DataFormat.Default)]
		public List<KVStruct> dropItem
		{
			get
			{
				return this._dropItem;
			}
		}

		[ProtoMember(3, Name = "dropEquip", DataFormat = DataFormat.Default)]
		public List<KVStruct> dropEquip
		{
			get
			{
				return this._dropEquip;
			}
		}

		[ProtoMember(4, Name = "buyArmyTimes", DataFormat = DataFormat.Default)]
		public List<KVStruct> buyArmyTimes
		{
			get
			{
				return this._buyArmyTimes;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
