using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSArmyList")]
	[Serializable]
	public class CSArmyList : IExtensible
	{
		private int _typeId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "typeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
