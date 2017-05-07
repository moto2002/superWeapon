using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSBattleEnd")]
	[Serializable]
	public class CSBattleEnd : IExtensible
	{
		private bool _win = false;

		private byte[] _video = null;

		private int _star = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "win", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool win
		{
			get
			{
				return this._win;
			}
			set
			{
				this._win = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "video", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] video
		{
			get
			{
				return this._video;
			}
			set
			{
				this._video = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
