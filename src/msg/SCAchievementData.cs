using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCAchievementData")]
	[Serializable]
	public class SCAchievementData : IExtensible
	{
		private long _id = 0L;

		private long _count = 0L;

		private int _lastStar = 0;

		private int _currentStar = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lastStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastStar
		{
			get
			{
				return this._lastStar;
			}
			set
			{
				this._lastStar = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "currentStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int currentStar
		{
			get
			{
				return this._currentStar;
			}
			set
			{
				this._currentStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
