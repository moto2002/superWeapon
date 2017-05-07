using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCBtnOpen")]
	[Serializable]
	public class SCBtnOpen : IExtensible
	{
		private long _id = 0L;

		private int _isAllOpen = 0;

		private readonly List<int> _btnList = new List<int>();

		private readonly List<int> _btnId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isAllOpen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isAllOpen
		{
			get
			{
				return this._isAllOpen;
			}
			set
			{
				this._isAllOpen = value;
			}
		}

		[ProtoMember(3, Name = "btnList", DataFormat = DataFormat.TwosComplement)]
		public List<int> btnList
		{
			get
			{
				return this._btnList;
			}
		}

		[ProtoMember(4, Name = "btnId", DataFormat = DataFormat.TwosComplement)]
		public List<int> btnId
		{
			get
			{
				return this._btnId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
