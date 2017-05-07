using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_AideModify")]
	[Serializable]
	public class GM_AideModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2018)]
			CMD = 2018,
			[ProtoEnum(Name = "ADD", Value = 1)]
			ADD = 1,
			[ProtoEnum(Name = "DEL", Value = 2)]
			DEL,
			[ProtoEnum(Name = "MOD", Value = 3)]
			MOD
		}

		private long _id = 0L;

		private int _aideId = 0;

		private int _type = 0;

		private int _abilityId = 0;

		private int _itemId = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "aideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aideId
		{
			get
			{
				return this._aideId;
			}
			set
			{
				this._aideId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "abilityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int abilityId
		{
			get
			{
				return this._abilityId;
			}
			set
			{
				this._abilityId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
