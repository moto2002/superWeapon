using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCEliteBoxStatus")]
	[Serializable]
	public class SCEliteBoxStatus : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _boxStatus = new List<KVStruct>();

		private bool _showTips = false;

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

		[ProtoMember(2, Name = "boxStatus", DataFormat = DataFormat.Default)]
		public List<KVStruct> boxStatus
		{
			get
			{
				return this._boxStatus;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "showTips", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool showTips
		{
			get
			{
				return this._showTips;
			}
			set
			{
				this._showTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
