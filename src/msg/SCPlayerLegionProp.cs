using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCPlayerLegionProp")]
	[Serializable]
	public class SCPlayerLegionProp : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _prizeData = new List<KVStruct>();

		private int _legionHelpApplyTimes = 0;

		private readonly List<KVStruct> _helpBuildingTimes = new List<KVStruct>();

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

		[ProtoMember(2, Name = "prizeData", DataFormat = DataFormat.Default)]
		public List<KVStruct> prizeData
		{
			get
			{
				return this._prizeData;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "legionHelpApplyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int legionHelpApplyTimes
		{
			get
			{
				return this._legionHelpApplyTimes;
			}
			set
			{
				this._legionHelpApplyTimes = value;
			}
		}

		[ProtoMember(4, Name = "helpBuildingTimes", DataFormat = DataFormat.Default)]
		public List<KVStruct> helpBuildingTimes
		{
			get
			{
				return this._helpBuildingTimes;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
