using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSOfficerSkillUp")]
	[Serializable]
	public class CSOfficerSkillUp : IExtensible
	{
		private long _officerId = 0L;

		private int _skillId = 0;

		private int _type = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "officerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long officerId
		{
			get
			{
				return this._officerId;
			}
			set
			{
				this._officerId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
