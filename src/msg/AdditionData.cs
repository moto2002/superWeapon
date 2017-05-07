using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "AdditionData")]
	[Serializable]
	public class AdditionData : IExtensible
	{
		private readonly List<KVStruct> _fighterTechData = new List<KVStruct>();

		private readonly List<KVStruct> _fighterArmyData = new List<KVStruct>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "fighterTechData", DataFormat = DataFormat.Default)]
		public List<KVStruct> fighterTechData
		{
			get
			{
				return this._fighterTechData;
			}
		}

		[ProtoMember(2, Name = "fighterArmyData", DataFormat = DataFormat.Default)]
		public List<KVStruct> fighterArmyData
		{
			get
			{
				return this._fighterArmyData;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
