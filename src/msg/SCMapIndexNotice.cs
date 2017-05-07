using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCMapIndexNotice")]
	[Serializable]
	public class SCMapIndexNotice : IExtensible
	{
		private long _id = 0L;

		private long _worldMapId = 0L;

		private readonly List<KVStruct> _index = new List<KVStruct>();

		private bool _tips = false;

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

		[ProtoMember(4, IsRequired = false, Name = "worldMapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, Name = "index", DataFormat = DataFormat.Default)]
		public List<KVStruct> index
		{
			get
			{
				return this._index;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "tips", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool tips
		{
			get
			{
				return this._tips;
			}
			set
			{
				this._tips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
