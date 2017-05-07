using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "OperationMove")]
	[Serializable]
	public class OperationMove : IExtensible
	{
		private Position _pos = null;

		private readonly List<OperationTanksPos> _tanksPos = new List<OperationTanksPos>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(2, Name = "tanksPos", DataFormat = DataFormat.Default)]
		public List<OperationTanksPos> tanksPos
		{
			get
			{
				return this._tanksPos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
