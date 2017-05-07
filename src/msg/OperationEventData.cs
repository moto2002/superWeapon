using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "OperationEventData")]
	[Serializable]
	public class OperationEventData : IExtensible
	{
		private int _time = 0;

		private int _OperationType = 0;

		private ContainerData _send = null;

		private OperationMove _move = null;

		private OperationFoceAttack _foceAtt = null;

		private SupperSkillEvent _superSkill = null;

		private DieEvent _die = null;

		private bool _IsAutoFight = false;

		private OperationControlArmyID _armyControl = null;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "OperationType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int OperationType
		{
			get
			{
				return this._OperationType;
			}
			set
			{
				this._OperationType = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "send", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ContainerData send
		{
			get
			{
				return this._send;
			}
			set
			{
				this._send = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "move", DataFormat = DataFormat.Default), DefaultValue(null)]
		public OperationMove move
		{
			get
			{
				return this._move;
			}
			set
			{
				this._move = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "foceAtt", DataFormat = DataFormat.Default), DefaultValue(null)]
		public OperationFoceAttack foceAtt
		{
			get
			{
				return this._foceAtt;
			}
			set
			{
				this._foceAtt = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "superSkill", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SupperSkillEvent superSkill
		{
			get
			{
				return this._superSkill;
			}
			set
			{
				this._superSkill = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "die", DataFormat = DataFormat.Default), DefaultValue(null)]
		public DieEvent die
		{
			get
			{
				return this._die;
			}
			set
			{
				this._die = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "IsAutoFight", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool IsAutoFight
		{
			get
			{
				return this._IsAutoFight;
			}
			set
			{
				this._IsAutoFight = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "armyControl", DataFormat = DataFormat.Default), DefaultValue(null)]
		public OperationControlArmyID armyControl
		{
			get
			{
				return this._armyControl;
			}
			set
			{
				this._armyControl = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
