using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCIslandData")]
	[Serializable]
	public class SCIslandData : IExtensible
	{
		private int _islandId = 0;

		private int _index = 0;

		private readonly List<int> _cutTrees = new List<int>();

		private readonly List<KVStruct> _CDEndTime = new List<KVStruct>();

		private long _id = 0L;

		private int _baseResId = 0;

		private int _terrainTypeId = 0;

		private readonly List<SCPlayerBuilding> _playerBuildings = new List<SCPlayerBuilding>();

		private readonly List<SCGrowableItemData> _growableItem = new List<SCGrowableItemData>();

		private readonly List<SCIslandOfficerData> _officers = new List<SCIslandOfficerData>();

		private readonly List<KVStruct> _tech = new List<KVStruct>();

		private readonly List<KVStruct> _baseRes = new List<KVStruct>();

		private int _dropListId = 0;

		private int _medal = 0;

		private readonly List<KVStruct> _resources = new List<KVStruct>();

		private long _ownerId = 0L;

		private int _ownerLevel = 0;

		private string _ownerName = "";

		private readonly List<KVStruct> _removeCdTime = new List<KVStruct>();

		private readonly List<SCArmyData> _targetArmy = new List<SCArmyData>();

		private readonly List<KVStruct> _armyCDTime = new List<KVStruct>();

		private readonly List<SCConfigureArmyData> _targetConfArmys = new List<SCConfigureArmyData>();

		private readonly List<SCExtraArmy> _extraArmyData = new List<SCExtraArmy>();

		private readonly List<KVStruct> _dropItems = new List<KVStruct>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "islandId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int islandId
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

		[ProtoMember(2, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, Name = "cutTrees", DataFormat = DataFormat.TwosComplement)]
		public List<int> cutTrees
		{
			get
			{
				return this._cutTrees;
			}
		}

		[ProtoMember(4, Name = "CDEndTime", DataFormat = DataFormat.Default)]
		public List<KVStruct> CDEndTime
		{
			get
			{
				return this._CDEndTime;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(6, IsRequired = false, Name = "baseResId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int baseResId
		{
			get
			{
				return this._baseResId;
			}
			set
			{
				this._baseResId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "terrainTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int terrainTypeId
		{
			get
			{
				return this._terrainTypeId;
			}
			set
			{
				this._terrainTypeId = value;
			}
		}

		[ProtoMember(8, Name = "playerBuildings", DataFormat = DataFormat.Default)]
		public List<SCPlayerBuilding> playerBuildings
		{
			get
			{
				return this._playerBuildings;
			}
		}

		[ProtoMember(9, Name = "growableItem", DataFormat = DataFormat.Default)]
		public List<SCGrowableItemData> growableItem
		{
			get
			{
				return this._growableItem;
			}
		}

		[ProtoMember(10, Name = "officers", DataFormat = DataFormat.Default)]
		public List<SCIslandOfficerData> officers
		{
			get
			{
				return this._officers;
			}
		}

		[ProtoMember(11, Name = "tech", DataFormat = DataFormat.Default)]
		public List<KVStruct> tech
		{
			get
			{
				return this._tech;
			}
		}

		[ProtoMember(12, Name = "baseRes", DataFormat = DataFormat.Default)]
		public List<KVStruct> baseRes
		{
			get
			{
				return this._baseRes;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "dropListId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropListId
		{
			get
			{
				return this._dropListId;
			}
			set
			{
				this._dropListId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "medal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, Name = "resources", DataFormat = DataFormat.Default)]
		public List<KVStruct> resources
		{
			get
			{
				return this._resources;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "ownerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long ownerId
		{
			get
			{
				return this._ownerId;
			}
			set
			{
				this._ownerId = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "ownerLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownerLevel
		{
			get
			{
				return this._ownerLevel;
			}
			set
			{
				this._ownerLevel = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "ownerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ownerName
		{
			get
			{
				return this._ownerName;
			}
			set
			{
				this._ownerName = value;
			}
		}

		[ProtoMember(20, Name = "removeCdTime", DataFormat = DataFormat.Default)]
		public List<KVStruct> removeCdTime
		{
			get
			{
				return this._removeCdTime;
			}
		}

		[ProtoMember(21, Name = "targetArmy", DataFormat = DataFormat.Default)]
		public List<SCArmyData> targetArmy
		{
			get
			{
				return this._targetArmy;
			}
		}

		[ProtoMember(22, Name = "armyCDTime", DataFormat = DataFormat.Default)]
		public List<KVStruct> armyCDTime
		{
			get
			{
				return this._armyCDTime;
			}
		}

		[ProtoMember(23, Name = "targetConfArmys", DataFormat = DataFormat.Default)]
		public List<SCConfigureArmyData> targetConfArmys
		{
			get
			{
				return this._targetConfArmys;
			}
		}

		[ProtoMember(24, Name = "extraArmyData", DataFormat = DataFormat.Default)]
		public List<SCExtraArmy> extraArmyData
		{
			get
			{
				return this._extraArmyData;
			}
		}

		[ProtoMember(25, Name = "dropItems", DataFormat = DataFormat.Default)]
		public List<KVStruct> dropItems
		{
			get
			{
				return this._dropItems;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
