using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "OperationTanksPos")]
	[Serializable]
	public class OperationTanksPos : IExtensible
	{
		private long _id = 0L;

		private Position _pos = null;

		private int _moveID = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Position pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "moveID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moveID
		{
			get
			{
				return this._moveID;
			}
			set
			{
				this._moveID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
