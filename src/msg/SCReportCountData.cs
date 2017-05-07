using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCReportCountData")]
	[Serializable]
	public class SCReportCountData : IExtensible
	{
		private long _countTime = 0L;

		private int _spyTimes = 0;

		private int _atkTimes = 0;

		private int _winTimes = 0;

		private int _type = 0;

		private long _id = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "countTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long countTime
		{
			get
			{
				return this._countTime;
			}
			set
			{
				this._countTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "spyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int spyTimes
		{
			get
			{
				return this._spyTimes;
			}
			set
			{
				this._spyTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "atkTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int atkTimes
		{
			get
			{
				return this._atkTimes;
			}
			set
			{
				this._atkTimes = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "winTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int winTimes
		{
			get
			{
				return this._winTimes;
			}
			set
			{
				this._winTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
