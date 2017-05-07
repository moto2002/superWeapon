using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSShareBattleVideo")]
	[Serializable]
	public class CSShareBattleVideo : IExtensible
	{
		private long _battleReportId = 0L;

		private long _time = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "battleReportId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long battleReportId
		{
			get
			{
				return this._battleReportId;
			}
			set
			{
				this._battleReportId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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
