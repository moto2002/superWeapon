using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSearchLegion")]
	[Serializable]
	public class CSSearchLegion : IExtensible
	{
		private string _legionName = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "legionName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string legionName
		{
			get
			{
				return this._legionName;
			}
			set
			{
				this._legionName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
