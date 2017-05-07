using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCVideoData")]
	[Serializable]
	public class SCVideoData : IExtensible
	{
		private long _id = 0L;

		private byte[] _islandData = null;

		private byte[] _videoData = null;

		private byte[] _additionData = null;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, IsRequired = false, Name = "islandData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] islandData
		{
			get
			{
				return this._islandData;
			}
			set
			{
				this._islandData = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "videoData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] videoData
		{
			get
			{
				return this._videoData;
			}
			set
			{
				this._videoData = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "additionData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] additionData
		{
			get
			{
				return this._additionData;
			}
			set
			{
				this._additionData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
