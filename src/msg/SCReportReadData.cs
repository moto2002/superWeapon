using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCReportReadData")]
	[Serializable]
	public class SCReportReadData : IExtensible
	{
		private long _id = 0L;

		private int _isReaded = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "isReaded", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isReaded
		{
			get
			{
				return this._isReaded;
			}
			set
			{
				this._isReaded = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
