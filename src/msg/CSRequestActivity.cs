using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSRequestActivity")]
	[Serializable]
	public class CSRequestActivity : IExtensible
	{
		private int _worldMapId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int worldMapId
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
