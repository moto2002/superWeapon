using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSRefreshActivity")]
	[Serializable]
	public class CSRefreshActivity : IExtensible
	{
		private int _id = 0;

		private int _worldMapId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int id
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

		[ProtoMember(2, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
