using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSSkillExchange")]
	[Serializable]
	public class CSSkillExchange : IExtensible
	{
		private int _index = 0;

		private int _nouse = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "index", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "nouse", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nouse
		{
			get
			{
				return this._nouse;
			}
			set
			{
				this._nouse = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
