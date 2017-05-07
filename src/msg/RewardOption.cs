using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "RewardOption")]
	[Serializable]
	public class RewardOption : IExtensible
	{
		private readonly List<KVStruct> _resReward = new List<KVStruct>();

		private readonly List<KVStruct> _itemReward = new List<KVStruct>();

		private readonly List<KVStruct> _moneyReward = new List<KVStruct>();

		private readonly List<KVStruct> _skillReward = new List<KVStruct>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "resReward", DataFormat = DataFormat.Default)]
		public List<KVStruct> resReward
		{
			get
			{
				return this._resReward;
			}
		}

		[ProtoMember(2, Name = "itemReward", DataFormat = DataFormat.Default)]
		public List<KVStruct> itemReward
		{
			get
			{
				return this._itemReward;
			}
		}

		[ProtoMember(3, Name = "moneyReward", DataFormat = DataFormat.Default)]
		public List<KVStruct> moneyReward
		{
			get
			{
				return this._moneyReward;
			}
		}

		[ProtoMember(4, Name = "skillReward", DataFormat = DataFormat.Default)]
		public List<KVStruct> skillReward
		{
			get
			{
				return this._skillReward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
