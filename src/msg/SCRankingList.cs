using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCRankingList")]
	[Serializable]
	public class SCRankingList : IExtensible
	{
		private long _id = 0L;

		private readonly List<RankingData> _ranking = new List<RankingData>();

		private int _ownRank = 0;

		private int _flushHour = 0;

		private int _beforeBest = 0;

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

		[ProtoMember(2, Name = "ranking", DataFormat = DataFormat.Default)]
		public List<RankingData> ranking
		{
			get
			{
				return this._ranking;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ownRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownRank
		{
			get
			{
				return this._ownRank;
			}
			set
			{
				this._ownRank = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "flushHour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flushHour
		{
			get
			{
				return this._flushHour;
			}
			set
			{
				this._flushHour = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "beforeBest", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beforeBest
		{
			get
			{
				return this._beforeBest;
			}
			set
			{
				this._beforeBest = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
