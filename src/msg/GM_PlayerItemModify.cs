using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_PlayerItemModify")]
	[Serializable]
	public class GM_PlayerItemModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2010)]
			CMD = 2010
		}

		private long _id = 0L;

		private int _num = 0;

		private int _numWay = 0;

		private int _itemId = 0;

		private long _userId = 0L;

		private int _type = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "numWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int numWay
		{
			get
			{
				return this._numWay;
			}
			set
			{
				this._numWay = value;
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

		[ProtoMember(3, IsRequired = false, Name = "userId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
