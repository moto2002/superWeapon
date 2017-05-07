using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSCalcMoney")]
	[Serializable]
	public class CSCalcMoney : IExtensible
	{
		private int _type = 0;

		private int _cdType = 0;

		private int _index = 0;

		private long _id = 0L;

		private int _itemId = 0;

		private readonly List<long> _cityWallIds = new List<long>();

		private int _configureType = 0;

		private int _confNum = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "cdType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cdType
		{
			get
			{
				return this._cdType;
			}
			set
			{
				this._cdType = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(6, Name = "cityWallIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> cityWallIds
		{
			get
			{
				return this._cityWallIds;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "configureType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int configureType
		{
			get
			{
				return this._configureType;
			}
			set
			{
				this._configureType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "confNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int confNum
		{
			get
			{
				return this._confNum;
			}
			set
			{
				this._confNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
