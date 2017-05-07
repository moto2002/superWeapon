using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerWorldMap")]
	[Serializable]
	public class SCPlayerWorldMap : IExtensible
	{
		private long _id = 0L;

		private readonly List<int> _openGroupId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(5, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, Name = "openGroupId", DataFormat = DataFormat.TwosComplement)]
		public List<int> openGroupId
		{
			get
			{
				return this._openGroupId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
