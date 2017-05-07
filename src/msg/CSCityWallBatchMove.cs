using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSCityWallBatchMove")]
	[Serializable]
	public class CSCityWallBatchMove : IExtensible
	{
		private readonly List<KVStruct> _CityWall = new List<KVStruct>();

		private long _islandId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "CityWall", DataFormat = DataFormat.Default)]
		public List<KVStruct> CityWall
		{
			get
			{
				return this._CityWall;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
