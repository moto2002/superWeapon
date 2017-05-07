using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCExtraArmy")]
	[Serializable]
	public class SCExtraArmy : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _itemId2Level = new List<KVStruct>();

		private readonly List<KVStruct> _itemId2Num = new List<KVStruct>();

		private long _espireTime = 0L;

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

		[ProtoMember(2, Name = "itemId2Level", DataFormat = DataFormat.Default)]
		public List<KVStruct> itemId2Level
		{
			get
			{
				return this._itemId2Level;
			}
		}

		[ProtoMember(3, Name = "itemId2Num", DataFormat = DataFormat.Default)]
		public List<KVStruct> itemId2Num
		{
			get
			{
				return this._itemId2Num;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "espireTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long espireTime
		{
			get
			{
				return this._espireTime;
			}
			set
			{
				this._espireTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
