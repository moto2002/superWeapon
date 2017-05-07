using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCGetOrderId")]
	[Serializable]
	public class SCGetOrderId : IExtensible
	{
		private long _id = 0L;

		private string _orderId = "";

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

		[ProtoMember(2, IsRequired = false, Name = "orderId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string orderId
		{
			get
			{
				return this._orderId;
			}
			set
			{
				this._orderId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
