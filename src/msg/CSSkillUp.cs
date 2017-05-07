using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSkillUp")]
	[Serializable]
	public class CSSkillUp : IExtensible
	{
		private int _skillType = 0;

		private readonly List<long> _skills = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "skillType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillType
		{
			get
			{
				return this._skillType;
			}
			set
			{
				this._skillType = value;
			}
		}

		[ProtoMember(2, Name = "skills", DataFormat = DataFormat.TwosComplement)]
		public List<long> skills
		{
			get
			{
				return this._skills;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
