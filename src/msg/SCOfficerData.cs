using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOfficerData")]
	[Serializable]
	public class SCOfficerData : IExtensible
	{
		private long _id = 0L;

		private int _itemId = 0;

		private int _level = 0;

		private int _exp = 0;

		private int _type = 0;

		private long _dismissTime = 0L;

		private long _unfreezeTime = 0L;

		private readonly List<KVStruct> _equ = new List<KVStruct>();

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

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "dismissTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long dismissTime
		{
			get
			{
				return this._dismissTime;
			}
			set
			{
				this._dismissTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "unfreezeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long unfreezeTime
		{
			get
			{
				return this._unfreezeTime;
			}
			set
			{
				this._unfreezeTime = value;
			}
		}

		[ProtoMember(9, Name = "equ", DataFormat = DataFormat.Default)]
		public List<KVStruct> equ
		{
			get
			{
				return this._equ;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
