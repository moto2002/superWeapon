using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCSkillConfigData")]
	[Serializable]
	public class SCSkillConfigData : IExtensible
	{
		private long _id = 0L;

		private readonly List<long> _skillId = new List<long>();

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

		[ProtoMember(2, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public List<long> skillId
		{
			get
			{
				return this._skillId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
