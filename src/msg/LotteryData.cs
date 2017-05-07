using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "LotteryData")]
	[Serializable]
	public class LotteryData : IExtensible
	{
		private int _tenPrice = 0;

		private readonly List<RewardOption> _option = new List<RewardOption>();

		private int _onePrice = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "tenPrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tenPrice
		{
			get
			{
				return this._tenPrice;
			}
			set
			{
				this._tenPrice = value;
			}
		}

		[ProtoMember(2, Name = "option", DataFormat = DataFormat.Default)]
		public List<RewardOption> option
		{
			get
			{
				return this._option;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "onePrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int onePrice
		{
			get
			{
				return this._onePrice;
			}
			set
			{
				this._onePrice = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
