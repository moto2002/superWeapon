using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "OperationFoceAttack")]
	[Serializable]
	public class OperationFoceAttack : IExtensible
	{
		private long _id = 0L;

		private readonly List<OperationTanksPos> _tanksPos = new List<OperationTanksPos>();

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

		[ProtoMember(2, Name = "tanksPos", DataFormat = DataFormat.Default)]
		public List<OperationTanksPos> tanksPos
		{
			get
			{
				return this._tanksPos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
