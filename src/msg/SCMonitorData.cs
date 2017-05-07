using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCMonitorData")]
	[Serializable]
	public class SCMonitorData : IExtensible
	{
		private long _id = 0L;

		private int _serverId = 0;

		private int _areaId = 0;

		private string _data = "";

		private string _uniqueId = "";

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

		[ProtoMember(2, IsRequired = false, Name = "serverId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverId
		{
			get
			{
				return this._serverId;
			}
			set
			{
				this._serverId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "areaId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int areaId
		{
			get
			{
				return this._areaId;
			}
			set
			{
				this._areaId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "data", DataFormat = DataFormat.Default), DefaultValue("")]
		public string data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "uniqueId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				this._uniqueId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
