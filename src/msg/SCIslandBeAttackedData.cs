using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCIslandBeAttackedData")]
	[Serializable]
	public class SCIslandBeAttackedData : IExtensible
	{
		private long _id = 0L;

		private readonly List<long> _buildingId = new List<long>();

		private bool _all = false;

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

		[ProtoMember(2, Name = "buildingId", DataFormat = DataFormat.TwosComplement)]
		public List<long> buildingId
		{
			get
			{
				return this._buildingId;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "all", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool all
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
