using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSoldierConfigure")]
	[Serializable]
	public class SCSoldierConfigure : IExtensible
	{
		private long _id = 0L;

		private int _soldierItemId = 0;

		private long _cdTime = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "soldierItemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldierItemId
		{
			get
			{
				return this._soldierItemId;
			}
			set
			{
				this._soldierItemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "cdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cdTime
		{
			get
			{
				return this._cdTime;
			}
			set
			{
				this._cdTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
