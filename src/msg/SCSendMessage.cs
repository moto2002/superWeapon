using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSendMessage")]
	[Serializable]
	public class SCSendMessage : IExtensible
	{
		private long _id = 0L;

		private ChatMessage _msg = null;

		private int _code = 0;

		private int _tagKey = 0;

		private string _tagValue = "";

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

		[ProtoMember(1, IsRequired = false, Name = "msg", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChatMessage msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "code", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "tagKey", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tagKey
		{
			get
			{
				return this._tagKey;
			}
			set
			{
				this._tagKey = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "tagValue", DataFormat = DataFormat.Default), DefaultValue("")]
		public string tagValue
		{
			get
			{
				return this._tagValue;
			}
			set
			{
				this._tagValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
