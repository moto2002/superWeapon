using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCReportData")]
	[Serializable]
	public class SCReportData : IExtensible
	{
		private int _type = 0;

		private int _islandIdx = 0;

		private long _beginTime = 0L;

		private bool _fighterWin = false;

		private long _fighterId = 0L;

		private string _fighterName = "";

		private int _fighterLevel = 0;

		private int _fighterMedal = 0;

		private readonly List<KVStruct> _lossRes = new List<KVStruct>();

		private readonly List<KVStruct> _sendArmys = new List<KVStruct>();

		private readonly List<KVStruct> _destroyArmys = new List<KVStruct>();

		private readonly List<KVStruct> _additions = new List<KVStruct>();

		private string _targetName = "";

		private long _targetId = 0L;

		private int _targetLevel = 0;

		private long _worldMapId = 0L;

		private readonly List<KVStruct> _addItems = new List<KVStruct>();

		private int _relation = 0;

		private long _videoId = 0L;

		private long _id = 0L;

		private bool _canRevenge = false;

		private int _money = 0;

		private long _receiveMoneyTime = 0L;

		private readonly List<KVStruct> _addRes = new List<KVStruct>();

		private readonly List<KVStruct> _addEquips = new List<KVStruct>();

		private bool _unRead = false;

		private int _sendSoldier = 0;

		private int _lossSoldier = 0;

		private int _fighterChangedMedal = 0;

		private int _targetChangedMedal = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "islandIdx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int islandIdx
		{
			get
			{
				return this._islandIdx;
			}
			set
			{
				this._islandIdx = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "beginTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long beginTime
		{
			get
			{
				return this._beginTime;
			}
			set
			{
				this._beginTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "fighterWin", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool fighterWin
		{
			get
			{
				return this._fighterWin;
			}
			set
			{
				this._fighterWin = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "fighterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fighterId
		{
			get
			{
				return this._fighterId;
			}
			set
			{
				this._fighterId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "fighterName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string fighterName
		{
			get
			{
				return this._fighterName;
			}
			set
			{
				this._fighterName = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "fighterLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fighterLevel
		{
			get
			{
				return this._fighterLevel;
			}
			set
			{
				this._fighterLevel = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "fighterMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fighterMedal
		{
			get
			{
				return this._fighterMedal;
			}
			set
			{
				this._fighterMedal = value;
			}
		}

		[ProtoMember(10, Name = "lossRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> lossRes
		{
			get
			{
				return this._lossRes;
			}
		}

		[ProtoMember(11, Name = "sendArmys", DataFormat = DataFormat.Default)]
		public List<KVStruct> sendArmys
		{
			get
			{
				return this._sendArmys;
			}
		}

		[ProtoMember(12, Name = "destroyArmys", DataFormat = DataFormat.Default)]
		public List<KVStruct> destroyArmys
		{
			get
			{
				return this._destroyArmys;
			}
		}

		[ProtoMember(13, Name = "additions", DataFormat = DataFormat.Default)]
		public List<KVStruct> additions
		{
			get
			{
				return this._additions;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "targetName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string targetName
		{
			get
			{
				return this._targetName;
			}
			set
			{
				this._targetName = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "targetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "targetLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetLevel
		{
			get
			{
				return this._targetLevel;
			}
			set
			{
				this._targetLevel = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(18, Name = "addItems", DataFormat = DataFormat.Default)]
		public List<KVStruct> addItems
		{
			get
			{
				return this._addItems;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "relation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				this._relation = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "videoId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long videoId
		{
			get
			{
				return this._videoId;
			}
			set
			{
				this._videoId = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(22, IsRequired = false, Name = "canRevenge", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canRevenge
		{
			get
			{
				return this._canRevenge;
			}
			set
			{
				this._canRevenge = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(24, IsRequired = false, Name = "receiveMoneyTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long receiveMoneyTime
		{
			get
			{
				return this._receiveMoneyTime;
			}
			set
			{
				this._receiveMoneyTime = value;
			}
		}

		[ProtoMember(25, Name = "addRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> addRes
		{
			get
			{
				return this._addRes;
			}
		}

		[ProtoMember(26, Name = "addEquips", DataFormat = DataFormat.Default)]
		public List<KVStruct> addEquips
		{
			get
			{
				return this._addEquips;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "unRead", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool unRead
		{
			get
			{
				return this._unRead;
			}
			set
			{
				this._unRead = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "sendSoldier", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sendSoldier
		{
			get
			{
				return this._sendSoldier;
			}
			set
			{
				this._sendSoldier = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "lossSoldier", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lossSoldier
		{
			get
			{
				return this._lossSoldier;
			}
			set
			{
				this._lossSoldier = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "fighterChangedMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fighterChangedMedal
		{
			get
			{
				return this._fighterChangedMedal;
			}
			set
			{
				this._fighterChangedMedal = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "targetChangedMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetChangedMedal
		{
			get
			{
				return this._targetChangedMedal;
			}
			set
			{
				this._targetChangedMedal = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
