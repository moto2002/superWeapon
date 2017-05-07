using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "DownMessage")]
	[Serializable]
	public class DownMessage : IExtensible
	{
		private readonly List<int> _dataId = new List<int>();

		private readonly List<byte[]> _data = new List<byte[]>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "dataId", DataFormat = DataFormat.TwosComplement)]
		public List<int> dataId
		{
			get
			{
				return this._dataId;
			}
		}

		[ProtoMember(2, Name = "data", DataFormat = DataFormat.Default)]
		public List<byte[]> data
		{
			get
			{
				return this._data;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
