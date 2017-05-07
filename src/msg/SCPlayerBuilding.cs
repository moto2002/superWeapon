using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerBuilding")]
	[Serializable]
	public class SCPlayerBuilding : IExtensible
	{
		private long _id = 0L;

		private int _index = 0;

		private int _itemId = 0;

		private int _level = 0;

		private int _protductNum = 0;

		private long _takeTime = 0L;

		private long _islandId = 0L;

		private int _upGradeLevel = 0;

		private int _strengthLevel = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "protductNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int protductNum
		{
			get
			{
				return this._protductNum;
			}
			set
			{
				this._protductNum = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "takeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long takeTime
		{
			get
			{
				return this._takeTime;
			}
			set
			{
				this._takeTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long islandId
		{
			get
			{
				return this._islandId;
			}
			set
			{
				this._islandId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "upGradeLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int upGradeLevel
		{
			get
			{
				return this._upGradeLevel;
			}
			set
			{
				this._upGradeLevel = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "strengthLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int strengthLevel
		{
			get
			{
				return this._strengthLevel;
			}
			set
			{
				this._strengthLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
