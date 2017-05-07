using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCLotteryPrize")]
	[Serializable]
	public class SCLotteryPrize : IExtensible
	{
		private long _id = 0L;

		private readonly List<int> _prizeList = new List<int>();

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

		[ProtoMember(2, Name = "prizeList", DataFormat = DataFormat.TwosComplement)]
		public List<int> prizeList
		{
			get
			{
				return this._prizeList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
