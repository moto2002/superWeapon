using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSPayCallback")]
	[Serializable]
	public class CSPayCallback : IExtensible
	{
		private string _userid = "";

		private string _appid = "";

		private string _goodsid = "";

		private string _money = "";

		private string _noteone = "";

		private string _notetwo = "";

		private string _cooorderserial = "";

		private string _rolename = "";

		private string _serverid = "";

		private string _goodscount = "";

		private string _goodsname = "";

		private string _serialnumber = "";

		private string _sign = "";

		private string _time = "";

		private string _token = "";

		private string _appkey = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "userid", DataFormat = DataFormat.Default), DefaultValue("")]
		public string userid
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "appid", DataFormat = DataFormat.Default), DefaultValue("")]
		public string appid
		{
			get
			{
				return this._appid;
			}
			set
			{
				this._appid = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "goodsid", DataFormat = DataFormat.Default), DefaultValue("")]
		public string goodsid
		{
			get
			{
				return this._goodsid;
			}
			set
			{
				this._goodsid = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "money", DataFormat = DataFormat.Default), DefaultValue("")]
		public string money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "noteone", DataFormat = DataFormat.Default), DefaultValue("")]
		public string noteone
		{
			get
			{
				return this._noteone;
			}
			set
			{
				this._noteone = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "notetwo", DataFormat = DataFormat.Default), DefaultValue("")]
		public string notetwo
		{
			get
			{
				return this._notetwo;
			}
			set
			{
				this._notetwo = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "cooorderserial", DataFormat = DataFormat.Default), DefaultValue("")]
		public string cooorderserial
		{
			get
			{
				return this._cooorderserial;
			}
			set
			{
				this._cooorderserial = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rolename", DataFormat = DataFormat.Default), DefaultValue("")]
		public string rolename
		{
			get
			{
				return this._rolename;
			}
			set
			{
				this._rolename = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "serverid", DataFormat = DataFormat.Default), DefaultValue("")]
		public string serverid
		{
			get
			{
				return this._serverid;
			}
			set
			{
				this._serverid = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "goodscount", DataFormat = DataFormat.Default), DefaultValue("")]
		public string goodscount
		{
			get
			{
				return this._goodscount;
			}
			set
			{
				this._goodscount = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "goodsname", DataFormat = DataFormat.Default), DefaultValue("")]
		public string goodsname
		{
			get
			{
				return this._goodsname;
			}
			set
			{
				this._goodsname = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "serialnumber", DataFormat = DataFormat.Default), DefaultValue("")]
		public string serialnumber
		{
			get
			{
				return this._serialnumber;
			}
			set
			{
				this._serialnumber = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "sign", DataFormat = DataFormat.Default), DefaultValue("")]
		public string sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "time", DataFormat = DataFormat.Default), DefaultValue("")]
		public string time
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

		[ProtoMember(15, IsRequired = false, Name = "token", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(16, IsRequired = false, Name = "appkey", DataFormat = DataFormat.Default), DefaultValue("")]
		public string appkey
		{
			get
			{
				return this._appkey;
			}
			set
			{
				this._appkey = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
