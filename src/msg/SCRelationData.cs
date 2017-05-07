using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCRelationData")]
	[Serializable]
	public class SCRelationData : IExtensible
	{
		private long _id = 0L;

		private long _relationId = 0L;

		private int _type = 0;

		private long _userId = 0L;

		private int _level = 0;

		private string _name = "";

		private string _teamName = "";

		private int _medalNum = 0;

		private readonly List<KVStruct> _lostReses = new List<KVStruct>();

		private long _reportId = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "relationId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long relationId
		{
			get
			{
				return this._relationId;
			}
			set
			{
				this._relationId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "userId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "teamName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string teamName
		{
			get
			{
				return this._teamName;
			}
			set
			{
				this._teamName = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "medalNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int medalNum
		{
			get
			{
				return this._medalNum;
			}
			set
			{
				this._medalNum = value;
			}
		}

		[ProtoMember(9, Name = "lostReses", DataFormat = DataFormat.Default)]
		public List<KVStruct> lostReses
		{
			get
			{
				return this._lostReses;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "reportId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
