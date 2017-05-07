using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSModifyLegionInfo")]
	[Serializable]
	public class CSModifyLegionInfo : IExtensible
	{
		private long _legionId = 0L;

		private readonly List<KVStructStr> _data = new List<KVStructStr>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionId
		{
			get
			{
				return this._legionId;
			}
			set
			{
				this._legionId = value;
			}
		}

		[ProtoMember(2, Name = "data", DataFormat = DataFormat.Default)]
		public List<KVStructStr> data
		{
			get
			{
				return this._data;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
