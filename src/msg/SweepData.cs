using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "SweepData")]
	[Serializable]
	public class SweepData : IExtensible
	{
		private readonly List<KVStruct> _res = new List<KVStruct>();

		private readonly List<KVStruct> _item = new List<KVStruct>();

		private readonly List<KVStruct> _equip = new List<KVStruct>();

		private IExtension extensionObject;

		[ProtoMember(2, Name = "res", DataFormat = DataFormat.Default)]
		public List<KVStruct> res
		{
			get
			{
				return this._res;
			}
		}

		[ProtoMember(3, Name = "item", DataFormat = DataFormat.Default)]
		public List<KVStruct> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(4, Name = "equip", DataFormat = DataFormat.Default)]
		public List<KVStruct> equip
		{
			get
			{
				return this._equip;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
