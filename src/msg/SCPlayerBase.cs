using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerBase")]
	[Serializable]
	public class SCPlayerBase : IExtensible
	{
		private long _id = 0L;

		private string _name = "";

		private int _medal = 0;

		private int _guideId = 0;

		private int _index = 0;

		private long _islandId = 0L;

		private long _legionId = 0L;

		private string _legionName = "";

		private int _isFirstLogin = 0;

		private int _ranking = 0;

		private long _legionOutTime = 0L;

		private int _taskGuideId = 0;

		private long _createTime = 0L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, IsRequired = false, Name = "medal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int medal
		{
			get
			{
				return this._medal;
			}
			set
			{
				this._medal = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "guideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guideId
		{
			get
			{
				return this._guideId;
			}
			set
			{
				this._guideId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long islandId
		{
			get
			{
				return this._islandId;
			}
			set
			{
				this._islandId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "legionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionId
		{
			get
			{
				return this._legionId;
			}
			set
			{
				this._legionId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "legionName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(9, IsRequired = false, Name = "isFirstLogin", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isFirstLogin
		{
			get
			{
				return this._isFirstLogin;
			}
			set
			{
				this._isFirstLogin = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "ranking", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ranking
		{
			get
			{
				return this._ranking;
			}
			set
			{
				this._ranking = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "legionOutTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long legionOutTime
		{
			get
			{
				return this._legionOutTime;
			}
			set
			{
				this._legionOutTime = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "taskGuideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskGuideId
		{
			get
			{
				return this._taskGuideId;
			}
			set
			{
				this._taskGuideId = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "createTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long createTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				this._createTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
