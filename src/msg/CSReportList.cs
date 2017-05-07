using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSReportList")]
	[Serializable]
	public class CSReportList : IExtensible
	{
		private int _listType = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "listType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listType
		{
			get
			{
				return this._listType;
			}
			set
			{
				this._listType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
