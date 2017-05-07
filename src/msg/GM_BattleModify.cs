using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_BattleModify")]
	[Serializable]
	public class GM_BattleModify : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2022)]
			CMD = 2022
		}

		private string _userId = "";

		private int _battletype = 0;

		private int _battle = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "userId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "battletype", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battletype
		{
			get
			{
				return this._battletype;
			}
			set
			{
				this._battletype = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "battle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battle
		{
			get
			{
				return this._battle;
			}
			set
			{
				this._battle = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
