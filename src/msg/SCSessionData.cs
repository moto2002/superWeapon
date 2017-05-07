using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSessionData")]
	[Serializable]
	public class SCSessionData : IExtensible
	{
		private long _id = 0L;

		private string _session = "";

		private int _requestId = 0;

		private IExtension extensionObject;

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

		[ProtoMember(3, IsRequired = false, Name = "requestId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int requestId
		{
			get
			{
				return this._requestId;
			}
			set
			{
				this._requestId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
