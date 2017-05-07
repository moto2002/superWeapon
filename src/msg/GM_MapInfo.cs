using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_MapInfo")]
	[Serializable]
	public class GM_MapInfo : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2019)]
			CMD = 2019
		}

		private string _userId = "";

		private IExtension extensionObject;

		[ProtoMember(7, IsRequired = false, Name = "userId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
