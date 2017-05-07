using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSGetOrderId")]
	[Serializable]
	public class CSGetOrderId : IExtensible
	{
		private int _goodsId = 0;

		private int _goodsCount = 0;

		private int _activityId = 0;

		private string _appleProductId = "";

		private long _orderId = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "goodsId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsId
		{
			get
			{
				return this._goodsId;
			}
			set
			{
				this._goodsId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "goodsCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsCount
		{
			get
			{
				return this._goodsCount;
			}
			set
			{
				this._goodsCount = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityId
		{
			get
			{
				return this._activityId;
			}
			set
			{
				this._activityId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "appleProductId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string appleProductId
		{
			get
			{
				return this._appleProductId;
			}
			set
			{
				this._appleProductId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "orderId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long orderId
		{
			get
			{
				return this._orderId;
			}
			set
			{
				this._orderId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
