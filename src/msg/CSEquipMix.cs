using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSEquipMix")]
	[Serializable]
	public class CSEquipMix : IExtensible
	{
		private long _equipId = 0L;

		private readonly List<long> _equip = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "equipId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		[ProtoMember(2, Name = "equip", DataFormat = DataFormat.TwosComplement)]
		public List<long> equip
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
