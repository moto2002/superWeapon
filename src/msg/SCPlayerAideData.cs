using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerAideData")]
	[Serializable]
	public class SCPlayerAideData : IExtensible
	{
		private long _id = 0L;

		private readonly List<int> _aideIds = new List<int>();

		private readonly List<int> _abilitys = new List<int>();

		private readonly List<int> _itemId = new List<int>();

		private readonly List<long> _time = new List<long>();

		private int _needSendAideId = 0;

		private long _endTime = 0L;

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

		[ProtoMember(2, Name = "aideIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> aideIds
		{
			get
			{
				return this._aideIds;
			}
		}

		[ProtoMember(3, Name = "abilitys", DataFormat = DataFormat.TwosComplement)]
		public List<int> abilitys
		{
			get
			{
				return this._abilitys;
			}
		}

		[ProtoMember(4, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(5, Name = "time", DataFormat = DataFormat.TwosComplement)]
		public List<long> time
		{
			get
			{
				return this._time;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "needSendAideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needSendAideId
		{
			get
			{
				return this._needSendAideId;
			}
			set
			{
				this._needSendAideId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
