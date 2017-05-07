using ProtoBuf;
using System;

namespace msg
{
	[ProtoContract(Name = "GM_ShowServerInfo")]
	[Serializable]
	public class GM_ShowServerInfo : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2025)]
			CMD = 2025
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
