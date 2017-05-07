using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCActivityCountsData")]
	[Serializable]
	public class SCActivityCountsData : IExtensible
	{
		private long _id = 0L;

		private int _activityId = 0;

		private int _count = 0;

		private string _ext = "";

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

		[ProtoMember(2, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityId
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

		[ProtoMember(3, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ext", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ext
		{
			get
			{
				return this._ext;
			}
			set
			{
				this._ext = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
