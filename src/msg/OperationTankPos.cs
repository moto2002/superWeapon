using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "OperationTankPos")]
	[Serializable]
	public class OperationTankPos : IExtensible
	{
		private long _tankID = 0L;

		private Position _curPos = null;

		private Position _moveTargetPos = null;

		private bool _isMove = false;

		private int _time = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "tankID", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long tankID
		{
			get
			{
				return this._tankID;
			}
			set
			{
				this._tankID = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "curPos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Position curPos
		{
			get
			{
				return this._curPos;
			}
			set
			{
				this._curPos = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "moveTargetPos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Position moveTargetPos
		{
			get
			{
				return this._moveTargetPos;
			}
			set
			{
				this._moveTargetPos = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "isMove", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isMove
		{
			get
			{
				return this._isMove;
			}
			set
			{
				this._isMove = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
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
