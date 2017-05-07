using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCArmsDealerData")]
	[Serializable]
	public class SCArmsDealerData : IExtensible
	{
		private long _id = 0L;

		private int _useMoneyRefreshTimes = 0;

		private long _lastRefreshTimeWithMoney = 0L;

		private long _nextRefreshTime = 0L;

		private readonly List<int> _armsIds = new List<int>();

		private readonly List<bool> _isSelled = new List<bool>();

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

		[ProtoMember(2, IsRequired = false, Name = "useMoneyRefreshTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int useMoneyRefreshTimes
		{
			get
			{
				return this._useMoneyRefreshTimes;
			}
			set
			{
				this._useMoneyRefreshTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lastRefreshTimeWithMoney", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long lastRefreshTimeWithMoney
		{
			get
			{
				return this._lastRefreshTimeWithMoney;
			}
			set
			{
				this._lastRefreshTimeWithMoney = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "nextRefreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long nextRefreshTime
		{
			get
			{
				return this._nextRefreshTime;
			}
			set
			{
				this._nextRefreshTime = value;
			}
		}

		[ProtoMember(5, Name = "armsIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> armsIds
		{
			get
			{
				return this._armsIds;
			}
		}

		[ProtoMember(6, Name = "isSelled", DataFormat = DataFormat.Default)]
		public List<bool> isSelled
		{
			get
			{
				return this._isSelled;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
