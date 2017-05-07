using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_TaskModify")]
	[Serializable]
	public class GM_TaskModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2014)]
			CMD = 2014
		}

		private int _way = 0;

		private int _itemId = 0;

		private long _userId = 0L;

		private int _count = 0;

		private IExtension extensionObject;

		[ProtoMember(6, IsRequired = false, Name = "way", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int way
		{
			get
			{
				return this._way;
			}
			set
			{
				this._way = value;
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

		[ProtoMember(4, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
