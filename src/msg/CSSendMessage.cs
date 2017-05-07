using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSendMessage")]
	[Serializable]
	public class CSSendMessage : IExtensible
	{
		private ChatMessage _message = null;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "message", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChatMessage message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
