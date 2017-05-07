using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCWarBattleData")]
	[Serializable]
	public class SCWarBattleData : IExtensible
	{
		private long _id = 0L;

		private int _battleId = 0;

		private int _zoneId = 0;

		private readonly List<KVStruct> _battlefields = new List<KVStruct>();

		private readonly List<int> _starPrize = new List<int>();

		private long _cdTime = 0L;

		private int _isSweep = 0;

		private int _boxId = 0;

		private readonly List<int> _completeIds = new List<int>();

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

		[ProtoMember(5, IsRequired = false, Name = "battleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battleId
		{
			get
			{
				return this._battleId;
			}
			set
			{
				this._battleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "zoneId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int zoneId
		{
			get
			{
				return this._zoneId;
			}
			set
			{
				this._zoneId = value;
			}
		}

		[ProtoMember(3, Name = "battlefields", DataFormat = DataFormat.Default)]
		public List<KVStruct> battlefields
		{
			get
			{
				return this._battlefields;
			}
		}

		[ProtoMember(4, Name = "starPrize", DataFormat = DataFormat.TwosComplement)]
		public List<int> starPrize
		{
			get
			{
				return this._starPrize;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "cdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cdTime
		{
			get
			{
				return this._cdTime;
			}
			set
			{
				this._cdTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "isSweep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isSweep
		{
			get
			{
				return this._isSweep;
			}
			set
			{
				this._isSweep = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "boxId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int boxId
		{
			get
			{
				return this._boxId;
			}
			set
			{
				this._boxId = value;
			}
		}

		[ProtoMember(10, Name = "completeIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> completeIds
		{
			get
			{
				return this._completeIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
