using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCActivitiesData")]
	[Serializable]
	public class SCActivitiesData : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _counts = new List<KVStruct>();

		private readonly List<KVStruct> _getPrizeCount = new List<KVStruct>();

		private int _todayTotalPayLastActId = 0;

		private long _todayTotalPayLastTime = 0L;

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

		[ProtoMember(2, Name = "counts", DataFormat = DataFormat.Default)]
		public List<KVStruct> counts
		{
			get
			{
				return this._counts;
			}
		}

		[ProtoMember(3, Name = "getPrizeCount", DataFormat = DataFormat.Default)]
		public List<KVStruct> getPrizeCount
		{
			get
			{
				return this._getPrizeCount;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "todayTotalPayLastActId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayTotalPayLastActId
		{
			get
			{
				return this._todayTotalPayLastActId;
			}
			set
			{
				this._todayTotalPayLastActId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "todayTotalPayLastTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long todayTotalPayLastTime
		{
			get
			{
				return this._todayTotalPayLastTime;
			}
			set
			{
				this._todayTotalPayLastTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
