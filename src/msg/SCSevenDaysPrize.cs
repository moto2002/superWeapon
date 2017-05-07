using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSevenDaysPrize")]
	[Serializable]
	public class SCSevenDaysPrize : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _prizeData = new List<KVStruct>();

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

		[ProtoMember(2, Name = "prizeData", DataFormat = DataFormat.Default)]
		public List<KVStruct> prizeData
		{
			get
			{
				return this._prizeData;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
