using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCBuyBuildingQueue")]
	[Serializable]
	public class SCBuyBuildingQueue : IExtensible
	{
		private long _id = 0L;

		private int _queueNum = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "queueNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int queueNum
		{
			get
			{
				return this._queueNum;
			}
			set
			{
				this._queueNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
