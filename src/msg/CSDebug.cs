using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "CSDebug")]
	[Serializable]
	public class CSDebug : IExtensible
	{
		private readonly List<int> _id = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public List<int> id
		{
			get
			{
				return this._id;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
