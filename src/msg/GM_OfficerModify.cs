using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_OfficerModify")]
	[Serializable]
	public class GM_OfficerModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2006)]
			CMD = 2006
		}

		private int _type = 0;

		private int _itemId = 0;

		private long _id = 0L;

		private int _level = 0;

		private int _status = 0;

		private long _userId = 0L;

		private long _dismissTime = 0L;

		private long _unfreezeTime = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(4, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "userId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
