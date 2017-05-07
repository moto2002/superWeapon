using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSocketRegister")]
	[Serializable]
	public class SCSocketRegister : IExtensible
	{
		private bool _success = false;

		private long _id = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "success", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool success
		{
			get
			{
				return this._success;
			}
			set
			{
				this._success = value;
			}
		}

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
