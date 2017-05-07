using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_PlayerModify")]
	[Serializable]
	public class GM_PlayerModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2002)]
			CMD = 2002
		}

		private long _id = 0L;

		private readonly List<KVStruct> _addRes = new List<KVStruct>();

		private readonly List<KVStruct> _setRes = new List<KVStruct>();

		private readonly List<KVStruct> _subRes = new List<KVStruct>();

		private int _level = 0;

		private int _moneyType = 0;

		private int _money = 0;

		private int _medalType = 0;

		private int _medal = 0;

		private string _name = "";

		private long _forbidChatEndTime = 0L;

		private long _stopEndTime = 0L;

		private int _guideId = 0;

		private long _cardEndTime = 0L;

		private int _superCard = 0;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, Name = "addRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> addRes
		{
			get
			{
				return this._addRes;
			}
		}

		[ProtoMember(4, Name = "setRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> setRes
		{
			get
			{
				return this._setRes;
			}
		}

		[ProtoMember(5, Name = "subRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> subRes
		{
			get
			{
				return this._subRes;
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

		[ProtoMember(7, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moneyType
		{
			get
			{
				return this._moneyType;
			}
			set
			{
				this._moneyType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "medalType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int medalType
		{
			get
			{
				return this._medalType;
			}
			set
			{
				this._medalType = value;
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

		[ProtoMember(11, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(12, IsRequired = false, Name = "forbidChatEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long forbidChatEndTime
		{
			get
			{
				return this._forbidChatEndTime;
			}
			set
			{
				this._forbidChatEndTime = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "stopEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long stopEndTime
		{
			get
			{
				return this._stopEndTime;
			}
			set
			{
				this._stopEndTime = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "guideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guideId
		{
			get
			{
				return this._guideId;
			}
			set
			{
				this._guideId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "cardEndTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cardEndTime
		{
			get
			{
				return this._cardEndTime;
			}
			set
			{
				this._cardEndTime = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "superCard", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int superCard
		{
			get
			{
				return this._superCard;
			}
			set
			{
				this._superCard = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
