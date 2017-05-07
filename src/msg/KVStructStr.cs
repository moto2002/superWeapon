using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "KVStructStr")]
	[Serializable]
	public class KVStructStr : IExtensible
	{
		private string _key = "";

		private string _value = "";

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

		[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
		public string value
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
