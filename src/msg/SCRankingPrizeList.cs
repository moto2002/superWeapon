using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCRankingPrizeList")]
	[Serializable]
	public class SCRankingPrizeList : IExtensible
	{
		private long _id = 0L;

		private int _prizeDay = 0;

		private int _prizeHour = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "prizeDay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int prizeDay
		{
			get
			{
				return this._prizeDay;
			}
			set
			{
				this._prizeDay = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "prizeHour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int prizeHour
		{
			get
			{
				return this._prizeHour;
			}
			set
			{
				this._prizeHour = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
