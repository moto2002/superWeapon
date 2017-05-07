using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSAgreeLegionApply")]
	[Serializable]
	public class CSAgreeLegionApply : IExtensible
	{
		private long _applyId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "applyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long applyId
		{
			get
			{
				return this._applyId;
			}
			set
			{
				this._applyId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
