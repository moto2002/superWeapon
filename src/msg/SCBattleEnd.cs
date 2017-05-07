using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCBattleEnd")]
	[Serializable]
	public class SCBattleEnd : IExtensible
	{
		private int _id = 0;

		private bool _win = false;

		private KVStruct _battlefieldStar = null;

		private long _npcAttackId = 0L;

		private readonly List<KVStruct> _addItems = new List<KVStruct>();

		private readonly List<KVStruct> _addRes = new List<KVStruct>();

		private readonly List<KVStruct> _addEquips = new List<KVStruct>();

		private int _isFirstBattleField = 0;

		private int _soldierExp = 0;

		private int _completeId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int id
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

		[ProtoMember(2, IsRequired = false, Name = "win", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool win
		{
			get
			{
				return this._win;
			}
			set
			{
				this._win = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "battlefieldStar", DataFormat = DataFormat.Default), DefaultValue(null)]
		public KVStruct battlefieldStar
		{
			get
			{
				return this._battlefieldStar;
			}
			set
			{
				this._battlefieldStar = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "npcAttackId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long npcAttackId
		{
			get
			{
				return this._npcAttackId;
			}
			set
			{
				this._npcAttackId = value;
			}
		}

		[ProtoMember(7, Name = "addItems", DataFormat = DataFormat.Default)]
		public List<KVStruct> addItems
		{
			get
			{
				return this._addItems;
			}
		}

		[ProtoMember(8, Name = "addRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> addRes
		{
			get
			{
				return this._addRes;
			}
		}

		[ProtoMember(9, Name = "addEquips", DataFormat = DataFormat.Default)]
		public List<KVStruct> addEquips
		{
			get
			{
				return this._addEquips;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "isFirstBattleField", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isFirstBattleField
		{
			get
			{
				return this._isFirstBattleField;
			}
			set
			{
				this._isFirstBattleField = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "soldierExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldierExp
		{
			get
			{
				return this._soldierExp;
			}
			set
			{
				this._soldierExp = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "completeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeId
		{
			get
			{
				return this._completeId;
			}
			set
			{
				this._completeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
