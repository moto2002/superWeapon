using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "CSCityWallBatchUpLevel")]
	[Serializable]
	public class CSCityWallBatchUpLevel : IExtensible
	{
		private readonly List<long> _cityWallIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "cityWallIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> cityWallIds
		{
			get
			{
				return this._cityWallIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
