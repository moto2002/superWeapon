using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "KVStructS2L")]
	[Serializable]
	public class KVStructS2L : IExtensible
	{
		private string _key = "";

		private long _value = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "key", DataFormat = DataFormat.Default), DefaultValue("")]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
