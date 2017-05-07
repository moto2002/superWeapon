using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSUseMoneyBuyGold")]
	[Serializable]
	public class CSUseMoneyBuyGold : IExtensible
	{
		private bool _freeBuy = false;

		private int _id = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "freeBuy", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool freeBuy
		{
			get
			{
				return this._freeBuy;
			}
			set
			{
				this._freeBuy = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int id
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
