using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCBattleStart")]
	[Serializable]
	public class SCBattleStart : IExtensible
	{
		private long _id = 0L;

		private bool _start = false;

		private int _battleType = 0;

		private int _from = 0;

		private readonly List<KVStruct> _eventMap = new List<KVStruct>();

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, IsRequired = false, Name = "start", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "battleType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "from", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int from
		{
			get
			{
				return this._from;
			}
			set
			{
				this._from = value;
			}
		}

		[ProtoMember(5, Name = "eventMap", DataFormat = DataFormat.Default)]
		public List<KVStruct> eventMap
		{
			get
			{
				return this._eventMap;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
