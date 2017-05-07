using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_Mail")]
	[Serializable]
	public class GM_Mail : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2003)]
			CMD = 2003
		}

		private readonly List<long> _id = new List<long>();

		private int _type = 0;

		private string _title = "";

		private string _content = "";

		private readonly List<KVStruct> _res = new List<KVStruct>();

		private readonly List<KVStruct> _item = new List<KVStruct>();

		private int _money = 0;

		private long _mailId = 0L;

		private int _mailType = 0;

		private IExtension extensionObject;

		[ProtoMember(2, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public List<long> id
		{
			get
			{
				return this._id;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(1, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, Name = "res", DataFormat = DataFormat.Default)]
		public List<KVStruct> res
		{
			get
			{
				return this._res;
			}
		}

		[ProtoMember(4, Name = "item", DataFormat = DataFormat.Default)]
		public List<KVStruct> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "money", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "mailId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long mailId
		{
			get
			{
				return this._mailId;
			}
			set
			{
				this._mailId = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "mailType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mailType
		{
			get
			{
				return this._mailType;
			}
			set
			{
				this._mailType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
