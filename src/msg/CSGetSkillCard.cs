using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSGetSkillCard")]
	[Serializable]
	public class CSGetSkillCard : IExtensible
	{
		private int _type = 0;

		private int _buyTimes = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "buyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyTimes
		{
			get
			{
				return this._buyTimes;
			}
			set
			{
				this._buyTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
