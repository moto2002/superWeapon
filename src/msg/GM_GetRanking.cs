using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "GM_GetRanking")]
	[Serializable]
	public class GM_GetRanking : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2024)]
			CMD = 2024
		}

		private int _type = 0;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
