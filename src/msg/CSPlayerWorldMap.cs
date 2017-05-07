using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSPlayerWorldMap")]
	[Serializable]
	public class CSPlayerWorldMap : IExtensible
	{
		private long _worldMapId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long worldMapId
		{
			get
			{
				return this._worldMapId;
			}
			set
			{
				this._worldMapId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
