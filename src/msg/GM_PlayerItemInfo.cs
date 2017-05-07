using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_PlayerItemInfo")]
	[Serializable]
	public class GM_PlayerItemInfo : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2009)]
			CMD = 2009
		}

		private string _id = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.Default), DefaultValue("")]
		public string id
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
