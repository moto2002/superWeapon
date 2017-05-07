using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSLogin")]
	[Serializable]
	public class CSLogin : IExtensible
	{
		private string _platformId = "";

		private string _imei = "";

		private string _equModel = "";

		private string _token = "";

		private long _time = 0L;

		private string _channelId = "";

		private bool _sdk = false;

		private string _clientVersion = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "platformId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string platformId
		{
			get
			{
				return this._platformId;
			}
			set
			{
				this._platformId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "imei", DataFormat = DataFormat.Default), DefaultValue("")]
		public string imei
		{
			get
			{
				return this._imei;
			}
			set
			{
				this._imei = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "equModel", DataFormat = DataFormat.Default), DefaultValue("")]
		public string equModel
		{
			get
			{
				return this._equModel;
			}
			set
			{
				this._equModel = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "token", DataFormat = DataFormat.Default), DefaultValue("")]
		public string token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "channelId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string channelId
		{
			get
			{
				return this._channelId;
			}
			set
			{
				this._channelId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "sdk", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool sdk
		{
			get
			{
				return this._sdk;
			}
			set
			{
				this._sdk = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "clientVersion", DataFormat = DataFormat.Default), DefaultValue("")]
		public string clientVersion
		{
			get
			{
				return this._clientVersion;
			}
			set
			{
				this._clientVersion = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
