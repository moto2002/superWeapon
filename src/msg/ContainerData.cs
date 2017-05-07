using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "ContainerData")]
	[Serializable]
	public class ContainerData : IExtensible
	{
		private long _containerId = 0L;

		private long _id = 0L;

		private int _index = 0;

		private int _soldierLV = 0;

		private Position _containPos = null;

		private int _soldierSkillLV = 0;

		private int _soldierType = 0;

		private int _soldieStar = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "containerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long containerId
		{
			get
			{
				return this._containerId;
			}
			set
			{
				this._containerId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(3, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "soldierLV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldierLV
		{
			get
			{
				return this._soldierLV;
			}
			set
			{
				this._soldierLV = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "containPos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Position containPos
		{
			get
			{
				return this._containPos;
			}
			set
			{
				this._containPos = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "soldierSkillLV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldierSkillLV
		{
			get
			{
				return this._soldierSkillLV;
			}
			set
			{
				this._soldierSkillLV = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "soldierType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldierType
		{
			get
			{
				return this._soldierType;
			}
			set
			{
				this._soldierType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "soldieStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soldieStar
		{
			get
			{
				return this._soldieStar;
			}
			set
			{
				this._soldieStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
