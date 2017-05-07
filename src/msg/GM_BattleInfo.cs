using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_BattleInfo")]
	[Serializable]
	public class GM_BattleInfo : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2021)]
			CMD = 2021
		}

		private string _userId = "";

		private int _battletype = 0;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
