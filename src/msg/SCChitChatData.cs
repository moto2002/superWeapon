using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCChitChatData")]
	[Serializable]
	public class SCChitChatData : IExtensible
	{
		private long _id = 0L;

		private long _userId = 0L;

		private string _userName = "";

		private string _message = "";

		private int _type = 0;

		private int _militaryRank = 0;

		private int _contentType = 0;

		private long _time = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "userId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, IsRequired = false, Name = "userName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(4, IsRequired = false, Name = "message", DataFormat = DataFormat.Default), DefaultValue("")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "militaryRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int militaryRank
		{
			get
			{
				return this._militaryRank;
			}
			set
			{
				this._militaryRank = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "contentType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int contentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				this._contentType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
