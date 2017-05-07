using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSpyIsland")]
	[Serializable]
	public class SCSpyIsland : IExtensible
	{
		private long _id = 0L;

		private bool _canAttack = false;

		private long _ownerId = 0L;

		private int _ownerLevel = 0;

		private string _ownerName = "";

		private int _medal = 0;

		private readonly List<KVStruct> _targetCurRes = new List<KVStruct>();

		private int _battleType = 0;

		private int _vip = 0;

		private int _exp = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "canAttack", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canAttack
		{
			get
			{
				return this._canAttack;
			}
			set
			{
				this._canAttack = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ownerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(4, IsRequired = false, Name = "ownerLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "medal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, Name = "targetCurRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> targetCurRes
		{
			get
			{
				return this._targetCurRes;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "battleType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battleType
		{
			get
			{
				return this._battleType;
			}
			set
			{
				this._battleType = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "vip", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vip
		{
			get
			{
				return this._vip;
			}
			set
			{
				this._vip = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
