using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSTakeResource")]
	[Serializable]
	public class CSTakeResource : IExtensible
	{
		private long _buildingId = 0L;

		private long _takeTime = 0L;

		private int _resId = 0;

		private int _takeNum = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "takeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long takeTime
		{
			get
			{
				return this._takeTime;
			}
			set
			{
				this._takeTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "resId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resId
		{
			get
			{
				return this._resId;
			}
			set
			{
				this._resId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "takeNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int takeNum
		{
			get
			{
				return this._takeNum;
			}
			set
			{
				this._takeNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
