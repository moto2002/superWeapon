using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCMapEntity")]
	[Serializable]
	public class SCMapEntity : IExtensible
	{
		private long _id = 0L;

		private long _islandId = 0L;

		private int _index = 0;

		private int _ownerType = 0;

		private long _ownerId = 0L;

		private string _ownerName = "";

		private int _ownerLevel = 0;

		private bool _refresh = false;

		private readonly List<SCPlayerResource> _res = new List<SCPlayerResource>();

		private int _commondLevel = 0;

		private long _worldMapId = 0L;

		private IExtension extensionObject;

		[ProtoMember(9, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long islandId
		{
			get
			{
				return this._islandId;
			}
			set
			{
				this._islandId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ownerType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownerType
		{
			get
			{
				return this._ownerType;
			}
			set
			{
				this._ownerType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ownerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long ownerId
		{
			get
			{
				return this._ownerId;
			}
			set
			{
				this._ownerId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "ownerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ownerName
		{
			get
			{
				return this._ownerName;
			}
			set
			{
				this._ownerName = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "ownerLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownerLevel
		{
			get
			{
				return this._ownerLevel;
			}
			set
			{
				this._ownerLevel = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "refresh", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool refresh
		{
			get
			{
				return this._refresh;
			}
			set
			{
				this._refresh = value;
			}
		}

		[ProtoMember(8, Name = "res", DataFormat = DataFormat.Default)]
		public List<SCPlayerResource> res
		{
			get
			{
				return this._res;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "commondLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commondLevel
		{
			get
			{
				return this._commondLevel;
			}
			set
			{
				this._commondLevel = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long worldMapId
		{
			get
			{
				return this._worldMapId;
			}
			set
			{
				this._worldMapId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
