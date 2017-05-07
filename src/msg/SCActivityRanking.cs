using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCActivityRanking")]
	[Serializable]
	public class SCActivityRanking : IExtensible
	{
		private long _id = 0L;

		private long _activityId = 0L;

		private string _userName = "";

		private long _userId = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long activityId
		{
			get
			{
				return this._activityId;
			}
			set
			{
				this._activityId = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
