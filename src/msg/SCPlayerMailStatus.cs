using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerMailStatus")]
	[Serializable]
	public class SCPlayerMailStatus : IExtensible
	{
		private long _id = 0L;

		private bool _isReceived = false;

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

		[ProtoMember(2, IsRequired = false, Name = "isReceived", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isReceived
		{
			get
			{
				return this._isReceived;
			}
			set
			{
				this._isReceived = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
