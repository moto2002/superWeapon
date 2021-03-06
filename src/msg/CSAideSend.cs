using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSAideSend")]
	[Serializable]
	public class CSAideSend : IExtensible
	{
		private int _aideId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "aideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aideId
		{
			get
			{
				return this._aideId;
			}
			set
			{
				this._aideId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
