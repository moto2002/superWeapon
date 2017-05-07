using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSAideIntensify")]
	[Serializable]
	public class CSAideIntensify : IExtensible
	{
		private int _aideID = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "aideID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aideID
		{
			get
			{
				return this._aideID;
			}
			set
			{
				this._aideID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
