using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCRankingPrizeData")]
	[Serializable]
	public class SCRankingPrizeData : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _res = new List<KVStruct>();

		private readonly List<KVStruct> _items = new List<KVStruct>();

		private bool _prize = false;

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

		[ProtoMember(2, Name = "res", DataFormat = DataFormat.Default)]
		public List<KVStruct> res
		{
			get
			{
				return this._res;
			}
		}

		[ProtoMember(3, Name = "items", DataFormat = DataFormat.Default)]
		public List<KVStruct> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "prize", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool prize
		{
			get
			{
				return this._prize;
			}
			set
			{
				this._prize = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
