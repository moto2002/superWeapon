using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCLegionHelpApply")]
	[Serializable]
	public class SCLegionHelpApply : IExtensible
	{
		private long _id = 0L;

		private long _time = 0L;

		private long _cdTime = 0L;

		private long _userId = 0L;

		private long _buildingId = 0L;

		private string _userName = "";

		private int _buildingIndex = 0;

		private readonly List<long> _helpers = new List<long>();

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

		[ProtoMember(2, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, IsRequired = false, Name = "cdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long cdTime
		{
			get
			{
				return this._cdTime;
			}
			set
			{
				this._cdTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "userId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "buildingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long buildingId
		{
			get
			{
				return this._buildingId;
			}
			set
			{
				this._buildingId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "userName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string userName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "buildingIndex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buildingIndex
		{
			get
			{
				return this._buildingIndex;
			}
			set
			{
				this._buildingIndex = value;
			}
		}

		[ProtoMember(8, Name = "helpers", DataFormat = DataFormat.TwosComplement)]
		public List<long> helpers
		{
			get
			{
				return this._helpers;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
