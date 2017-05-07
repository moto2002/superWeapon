using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSBattleStart")]
	[Serializable]
	public class CSBattleStart : IExtensible
	{
		private long _id = 0L;

		private int _from = 0;

		private int _money = 0;

		private long _reportId = 0L;

		private int _npcId = 0;

		private int _serverId = 0;

		private int _areaId = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "from", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int from
		{
			get
			{
				return this._from;
			}
			set
			{
				this._from = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "reportId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long reportId
		{
			get
			{
				return this._reportId;
			}
			set
			{
				this._reportId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "npcId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int npcId
		{
			get
			{
				return this._npcId;
			}
			set
			{
				this._npcId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "serverId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverId
		{
			get
			{
				return this._serverId;
			}
			set
			{
				this._serverId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "areaId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int areaId
		{
			get
			{
				return this._areaId;
			}
			set
			{
				this._areaId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
