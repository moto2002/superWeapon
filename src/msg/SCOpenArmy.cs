using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOpenArmy")]
	[Serializable]
	public class SCOpenArmy : IExtensible
	{
		private long _id = 0L;

		private readonly List<int> _armys = new List<int>();

		private long _buildingId = 0L;

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

		[ProtoMember(2, Name = "armys", DataFormat = DataFormat.TwosComplement)]
		public List<int> armys
		{
			get
			{
				return this._armys;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
