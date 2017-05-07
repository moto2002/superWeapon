using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCCalcMoney")]
	[Serializable]
	public class SCCalcMoney : IExtensible
	{
		private long _id = 0L;

		private int _money = 0;

		private readonly List<KVStruct> _res = new List<KVStruct>();

		private long _timeDiff = 0L;

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, Name = "res", DataFormat = DataFormat.Default)]
		public List<KVStruct> res
		{
			get
			{
				return this._res;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "timeDiff", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long timeDiff
		{
			get
			{
				return this._timeDiff;
			}
			set
			{
				this._timeDiff = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
