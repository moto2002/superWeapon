using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerVipData")]
	[Serializable]
	public class SCPlayerVipData : IExtensible
	{
		private long _id = 0L;

		private int _superCard = 0;

		private long _cardEndTime = 0L;

		private long _cardPrizeTime = 0L;

		private long _vipPrizeTime = 0L;

		private int _money = 0;

		private long _scardPrizeTime = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "superCard", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int superCard
		{
			get
			{
				return this._superCard;
			}
			set
			{
				this._superCard = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "cardEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cardEndTime
		{
			get
			{
				return this._cardEndTime;
			}
			set
			{
				this._cardEndTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "cardPrizeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cardPrizeTime
		{
			get
			{
				return this._cardPrizeTime;
			}
			set
			{
				this._cardPrizeTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "vipPrizeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long vipPrizeTime
		{
			get
			{
				return this._vipPrizeTime;
			}
			set
			{
				this._vipPrizeTime = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "scardPrizeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long scardPrizeTime
		{
			get
			{
				return this._scardPrizeTime;
			}
			set
			{
				this._scardPrizeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
