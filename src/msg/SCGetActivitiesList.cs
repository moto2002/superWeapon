using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCGetActivitiesList")]
	[Serializable]
	public class SCGetActivitiesList : IExtensible
	{
		private long _id = 0L;

		private readonly List<DesignActivityData> _data = new List<DesignActivityData>();

		private int _type = 0;

		private LotteryData _lottery = null;

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

		[ProtoMember(2, Name = "data", DataFormat = DataFormat.Default)]
		public List<DesignActivityData> data
		{
			get
			{
				return this._data;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "lottery", DataFormat = DataFormat.Default), DefaultValue(null)]
		public LotteryData lottery
		{
			get
			{
				return this._lottery;
			}
			set
			{
				this._lottery = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
