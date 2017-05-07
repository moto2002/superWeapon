using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOperation")]
	[Serializable]
	public class SCOperation : IExtensible
	{
		private long _id = 0L;

		private int _type = 0;

		private bool _isSuccess = false;

		private long _dataId = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "isSuccess", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isSuccess
		{
			get
			{
				return this._isSuccess;
			}
			set
			{
				this._isSuccess = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dataId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long dataId
		{
			get
			{
				return this._dataId;
			}
			set
			{
				this._dataId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
