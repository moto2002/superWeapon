using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSearchLegionData")]
	[Serializable]
	public class SCSearchLegionData : IExtensible
	{
		private long _id = 0L;

		private long _legionId = 0L;

		private string _name = "";

		private int _score = 0;

		private int _level = 0;

		private int _count = 0;

		private int _limit = 0;

		private int _minMedal = 0;

		private int _rank = 0;

		private IExtension extensionObject;

		[ProtoMember(8, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionId
		{
			get
			{
				return this._legionId;
			}
			set
			{
				this._legionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, IsRequired = false, Name = "score", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
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

		[ProtoMember(6, IsRequired = false, Name = "limit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int limit
		{
			get
			{
				return this._limit;
			}
			set
			{
				this._limit = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "minMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minMedal
		{
			get
			{
				return this._minMedal;
			}
			set
			{
				this._minMedal = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
