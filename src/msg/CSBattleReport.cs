using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSBattleReport")]
	[Serializable]
	public class CSBattleReport : IExtensible
	{
		private long _reportId = 0L;

		private int _type = 0;

		private bool _video = false;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "reportId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long reportId
		{
			get
			{
				return this._reportId;
			}
			set
			{
				this._reportId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "video", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool video
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
