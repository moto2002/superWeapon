using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSBuildingRemoveEnd")]
	[Serializable]
	public class CSBuildingRemoveEnd : IExtensible
	{
		private int _index = 0;

		private long _islandId = 0L;

		private int _money = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
