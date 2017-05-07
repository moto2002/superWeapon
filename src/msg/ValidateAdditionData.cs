using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "ValidateAdditionData")]
	[Serializable]
	public class ValidateAdditionData : IExtensible
	{
		private readonly List<SCIslandOfficerData> _fighterOfficer = new List<SCIslandOfficerData>();

		private readonly List<SCIslandOfficerData> _targetOfficer = new List<SCIslandOfficerData>();

		private readonly List<KVStruct> _fighterTech = new List<KVStruct>();

		private readonly List<KVStruct> _targetTech = new List<KVStruct>();

		private readonly List<SCArmyData> _fighterArmy = new List<SCArmyData>();

		private readonly List<SCArmyData> _targetArmy = new List<SCArmyData>();

		private IExtension extensionObject;

		[ProtoMember(2, Name = "fighterOfficer", DataFormat = DataFormat.Default)]
		public List<SCIslandOfficerData> fighterOfficer
		{
			get
			{
				return this._fighterOfficer;
			}
		}

		[ProtoMember(3, Name = "targetOfficer", DataFormat = DataFormat.Default)]
		public List<SCIslandOfficerData> targetOfficer
		{
			get
			{
				return this._targetOfficer;
			}
		}

		[ProtoMember(4, Name = "fighterTech", DataFormat = DataFormat.Default)]
		public List<KVStruct> fighterTech
		{
			get
			{
				return this._fighterTech;
			}
		}

		[ProtoMember(5, Name = "targetTech", DataFormat = DataFormat.Default)]
		public List<KVStruct> targetTech
		{
			get
			{
				return this._targetTech;
			}
		}

		[ProtoMember(6, Name = "fighterArmy", DataFormat = DataFormat.Default)]
		public List<SCArmyData> fighterArmy
		{
			get
			{
				return this._fighterArmy;
			}
		}

		[ProtoMember(7, Name = "targetArmy", DataFormat = DataFormat.Default)]
		public List<SCArmyData> targetArmy
		{
			get
			{
				return this._targetArmy;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
