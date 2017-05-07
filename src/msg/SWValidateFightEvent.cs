using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SWValidateFightEvent")]
	[Serializable]
	public class SWValidateFightEvent : IExtensible
	{
		private readonly List<FightEventData> _eventData = new List<FightEventData>();

		private readonly List<VFDeadData> _vfdata = new List<VFDeadData>();

		private ValidateAdditionData _addition = null;

		private long _id = 0L;

		private SCIslandData _targetIslandData = null;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "eventData", DataFormat = DataFormat.Default)]
		public List<FightEventData> eventData
		{
			get
			{
				return this._eventData;
			}
		}

		[ProtoMember(8, Name = "vfdata", DataFormat = DataFormat.Default)]
		public List<VFDeadData> vfdata
		{
			get
			{
				return this._vfdata;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "addition", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ValidateAdditionData addition
		{
			get
			{
				return this._addition;
			}
			set
			{
				this._addition = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(10, IsRequired = false, Name = "targetIslandData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SCIslandData targetIslandData
		{
			get
			{
				return this._targetIslandData;
			}
			set
			{
				this._targetIslandData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
