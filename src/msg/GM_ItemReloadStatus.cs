using ProtoBuf;
using System;

namespace msg
{
	[ProtoContract(Name = "GM_ItemReloadStatus")]
	[Serializable]
	public class GM_ItemReloadStatus : IExtensible
	{
		[ProtoContract(Name = "TYPE")]
		public enum TYPE
		{
			[ProtoEnum(Name = "CMD", Value = 2023)]
			CMD = 2023
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
