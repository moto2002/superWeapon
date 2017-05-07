using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSReconnection")]
	[Serializable]
	public class CSReconnection : IExtensible
	{
		private string _session = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "session", DataFormat = DataFormat.Default), DefaultValue("")]
		public string session
		{
			get
			{
				return this._session;
			}
			set
			{
				this._session = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
