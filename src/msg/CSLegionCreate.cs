using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSLegionCreate")]
	[Serializable]
	public class CSLegionCreate : IExtensible
	{
		private string _name = "";

		private int _logo = 0;

		private int _needMinMedal = 0;

		private int _openType = 0;

		private string _notice = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "logo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int logo
		{
			get
			{
				return this._logo;
			}
			set
			{
				this._logo = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "needMinMedal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needMinMedal
		{
			get
			{
				return this._needMinMedal;
			}
			set
			{
				this._needMinMedal = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "openType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openType
		{
			get
			{
				return this._openType;
			}
			set
			{
				this._openType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
		public string notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this._notice = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
