using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSCancelCDTime")]
	[Serializable]
	public class CSCancelCDTime : IExtensible
	{
		private long _buildingId = 0L;

		private int _money = 0;

		private int _tmp = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long buildingId
		{
			get
			{
				return this._buildingId;
			}
			set
			{
				this._buildingId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "tmp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tmp
		{
			get
			{
				return this._tmp;
			}
			set
			{
				this._tmp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
