using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCConfigureArmyData")]
	[Serializable]
	public class SCConfigureArmyData : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _armyId2Num = new List<KVStruct>();

		private readonly List<KVStruct> _cdTime2ArmyId = new List<KVStruct>();

		private readonly List<KVStruct> _endArmyId2Num = new List<KVStruct>();

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

		[ProtoMember(2, Name = "armyId2Num", DataFormat = DataFormat.Default)]
		public List<KVStruct> armyId2Num
		{
			get
			{
				return this._armyId2Num;
			}
		}

		[ProtoMember(3, Name = "cdTime2ArmyId", DataFormat = DataFormat.Default)]
		public List<KVStruct> cdTime2ArmyId
		{
			get
			{
				return this._cdTime2ArmyId;
			}
		}

		[ProtoMember(4, Name = "endArmyId2Num", DataFormat = DataFormat.Default)]
		public List<KVStruct> endArmyId2Num
		{
			get
			{
				return this._endArmyId2Num;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
