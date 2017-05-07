using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "NoticeData")]
	[Serializable]
	public class NoticeData : IExtensible
	{
		private long _id = 0L;

		private string _content = "";

		private long _startTime = 0L;

		private long _endTime = 0L;

		private int _serverId = 0;

		private readonly List<int> _areas = new List<int>();

		private int _interval = 0;

		private long _modifyTime = 0L;

		private int _noticeType = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue("")]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "startTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "serverId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, Name = "areas", DataFormat = DataFormat.TwosComplement)]
		public List<int> areas
		{
			get
			{
				return this._areas;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "interval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				this._interval = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "modifyTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long modifyTime
		{
			get
			{
				return this._modifyTime;
			}
			set
			{
				this._modifyTime = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "noticeType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int noticeType
		{
			get
			{
				return this._noticeType;
			}
			set
			{
				this._noticeType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
