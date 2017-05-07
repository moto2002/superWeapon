using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCLegionData")]
	[Serializable]
	public class SCLegionData : IExtensible
	{
		private long _id = 0L;

		private long _legionId = 0L;

		private string _name = "";

		private int _logo = 0;

		private int _needMinMedal = 0;

		private int _openType = 0;

		private int _level = 0;

		private int _memberLimit = 0;

		private int _exp = 0;

		private int _rank = 0;

		private int _medal = 0;

		private int _memberCount = 0;

		private string _notice = "";

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

		[ProtoMember(13, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, IsRequired = false, Name = "logo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int logo
		{
			get
			{
				return this._logo;
			}
			set
			{
				this._logo = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "needMinMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needMinMedal
		{
			get
			{
				return this._needMinMedal;
			}
			set
			{
				this._needMinMedal = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "openType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openType
		{
			get
			{
				return this._openType;
			}
			set
			{
				this._openType = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "memberLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int memberLimit
		{
			get
			{
				return this._memberLimit;
			}
			set
			{
				this._memberLimit = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
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

		[ProtoMember(10, IsRequired = false, Name = "medal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int medal
		{
			get
			{
				return this._medal;
			}
			set
			{
				this._medal = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "memberCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int memberCount
		{
			get
			{
				return this._memberCount;
			}
			set
			{
				this._memberCount = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
		public string notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this._notice = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
