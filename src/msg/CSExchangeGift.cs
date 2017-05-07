using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSExchangeGift")]
	[Serializable]
	public class CSExchangeGift : IExtensible
	{
		private string _giftCode = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "giftCode", DataFormat = DataFormat.Default), DefaultValue("")]
		public string giftCode
		{
			get
			{
				return this._giftCode;
			}
			set
			{
				this._giftCode = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
