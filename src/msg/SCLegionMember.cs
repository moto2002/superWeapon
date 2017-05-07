using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCLegionMember")]
	[Serializable]
	public class SCLegionMember : IExtensible
	{
		private long _id = 0L;

		private int _level = 0;

		private string _name = "";

		private int _job = 0;

		private int _contribution = 0;

		private int _medal = 0;

		private long _legionId = 0L;

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

		[ProtoMember(2, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "contribution", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int contribution
		{
			get
			{
				return this._contribution;
			}
			set
			{
				this._contribution = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "medal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int medal
		{
			get
			{
				return this._medal;
			}
			set
			{
				this._medal = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionId
		{
			get
			{
				return this._legionId;
			}
			set
			{
				this._legionId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
