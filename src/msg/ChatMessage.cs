using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "ChatMessage")]
	[Serializable]
	public class ChatMessage : IExtensible
	{
		private long _atPlayerId = 0L;

		private string _atPlayerName = "";

		private int _channel = 0;

		private string _message = "";

		private long _senderId = 0L;

		private string _senderName = "";

		private int _msgType = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "atPlayerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long atPlayerId
		{
			get
			{
				return this._atPlayerId;
			}
			set
			{
				this._atPlayerId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "atPlayerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string atPlayerName
		{
			get
			{
				return this._atPlayerName;
			}
			set
			{
				this._atPlayerName = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "channel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int channel
		{
			get
			{
				return this._channel;
			}
			set
			{
				this._channel = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "message", DataFormat = DataFormat.Default), DefaultValue("")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "senderId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long senderId
		{
			get
			{
				return this._senderId;
			}
			set
			{
				this._senderId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "senderName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string senderName
		{
			get
			{
				return this._senderName;
			}
			set
			{
				this._senderName = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "msgType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int msgType
		{
			get
			{
				return this._msgType;
			}
			set
			{
				this._msgType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
