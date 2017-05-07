using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOpenEliteBox")]
	[Serializable]
	public class CSOpenEliteBox : IExtensible
	{
		private int _boxId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "boxId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int boxId
		{
			get
			{
				return this._boxId;
			}
			set
			{
				this._boxId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
