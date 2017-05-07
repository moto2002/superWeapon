using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSRequestChitChatData")]
	[Serializable]
	public class CSRequestChitChatData : IExtensible
	{
		private long _time = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
