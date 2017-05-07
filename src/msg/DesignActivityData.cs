using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "DesignActivityData")]
	[Serializable]
	public class DesignActivityData : IExtensible
	{
		private int _activityId = 0;

		private int _activityType = 0;

		private string _startTimeStr = "";

		private string _endTimeStr = "";

		private string _showEndTimeStr = "";

		private int _rewardType = 0;

		private int _rewardCount = 0;

		private int _condition = 0;

		private int _conditionId = 0;

		private int _repeatPrizeCount = 0;

		private int _type = 0;

		private int _sort = 0;

		private string _name = "";

		private string _title = "";

		private string _conditionName = "";

		private RewardOption _normalOption = null;

		private RewardOption _specialOption = null;

		private string _totalDiscription = "";

		private int _toShopId = 0;

		private int _needMoney = 0;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "activityType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityType
		{
			get
			{
				return this._activityType;
			}
			set
			{
				this._activityType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "startTimeStr", DataFormat = DataFormat.Default), DefaultValue("")]
		public string startTimeStr
		{
			get
			{
				return this._startTimeStr;
			}
			set
			{
				this._startTimeStr = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "endTimeStr", DataFormat = DataFormat.Default), DefaultValue("")]
		public string endTimeStr
		{
			get
			{
				return this._endTimeStr;
			}
			set
			{
				this._endTimeStr = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "showEndTimeStr", DataFormat = DataFormat.Default), DefaultValue("")]
		public string showEndTimeStr
		{
			get
			{
				return this._showEndTimeStr;
			}
			set
			{
				this._showEndTimeStr = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rewardType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardType
		{
			get
			{
				return this._rewardType;
			}
			set
			{
				this._rewardType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rewardCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardCount
		{
			get
			{
				return this._rewardCount;
			}
			set
			{
				this._rewardCount = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int condition
		{
			get
			{
				return this._condition;
			}
			set
			{
				this._condition = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "conditionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int conditionId
		{
			get
			{
				return this._conditionId;
			}
			set
			{
				this._conditionId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "repeatPrizeCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int repeatPrizeCount
		{
			get
			{
				return this._repeatPrizeCount;
			}
			set
			{
				this._repeatPrizeCount = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "sort", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this._sort = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(15, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "conditionName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string conditionName
		{
			get
			{
				return this._conditionName;
			}
			set
			{
				this._conditionName = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "normalOption", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RewardOption normalOption
		{
			get
			{
				return this._normalOption;
			}
			set
			{
				this._normalOption = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "specialOption", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RewardOption specialOption
		{
			get
			{
				return this._specialOption;
			}
			set
			{
				this._specialOption = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "totalDiscription", DataFormat = DataFormat.Default), DefaultValue("")]
		public string totalDiscription
		{
			get
			{
				return this._totalDiscription;
			}
			set
			{
				this._totalDiscription = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "toShopId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int toShopId
		{
			get
			{
				return this._toShopId;
			}
			set
			{
				this._toShopId = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "needMoney", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needMoney
		{
			get
			{
				return this._needMoney;
			}
			set
			{
				this._needMoney = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
