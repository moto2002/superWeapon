using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSBuildingMove")]
	[Serializable]
	public class CSBuildingMove : IExtensible
	{
		private long _buildingId = 0L;

		private int _itemId = 0;

		private int _index = 0;

		private long _islandId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long buildingId
		{
			get
			{
				return this._buildingId;
			}
			set
			{
				this._buildingId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
