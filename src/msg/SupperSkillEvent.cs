using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SupperSkillEvent")]
	[Serializable]
	public class SupperSkillEvent : IExtensible
	{
		private int _skillType = 0;

		private int _skillId = 0;

		private Position _pos = null;

		private readonly List<OperationTanksPos> _tanksPos = new List<OperationTanksPos>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "skillType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillType
		{
			get
			{
				return this._skillType;
			}
			set
			{
				this._skillType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(4, Name = "tanksPos", DataFormat = DataFormat.Default)]
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
