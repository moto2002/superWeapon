using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerMailData")]
	[Serializable]
	public class SCPlayerMailData : IExtensible
	{
		private long _id = 0L;

		private string _content = "";

		private long _time = 0L;

		private bool _isRead = false;

		private bool _isReceived = false;

		private readonly List<KVStruct> _resources = new List<KVStruct>();

		private readonly List<KVStruct> _items = new List<KVStruct>();

		private string _title = "";

		private int _money = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue("")]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "isRead", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isRead
		{
			get
			{
				return this._isRead;
			}
			set
			{
				this._isRead = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "isReceived", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isReceived
		{
			get
			{
				return this._isReceived;
			}
			set
			{
				this._isReceived = value;
			}
		}

		[ProtoMember(6, Name = "resources", DataFormat = DataFormat.Default)]
		public List<KVStruct> resources
		{
			get
			{
				return this._resources;
			}
		}

		[ProtoMember(7, Name = "items", DataFormat = DataFormat.Default)]
		public List<KVStruct> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
