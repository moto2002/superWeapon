using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCCityWallBatchMove")]
	[Serializable]
	public class SCCityWallBatchMove : IExtensible
	{
		private long _id = 0L;

		private readonly List<SCBuildingMove> _cityWalls = new List<SCBuildingMove>();

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

		[ProtoMember(2, Name = "cityWalls", DataFormat = DataFormat.Default)]
		public List<SCBuildingMove> cityWalls
		{
			get
			{
				return this._cityWalls;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
