using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCNoticeList")]
	[Serializable]
	public class SCNoticeList : IExtensible
	{
		private long _id = 0L;

		private readonly List<NoticeData> _data = new List<NoticeData>();

		private int _hasNew = 0;

		private readonly List<long> _removed = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(1, Name = "data", DataFormat = DataFormat.Default)]
		public List<NoticeData> data
		{
			get
			{
				return this._data;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hasNew", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hasNew
		{
			get
			{
				return this._hasNew;
			}
			set
			{
				this._hasNew = value;
			}
		}

		[ProtoMember(4, Name = "removed", DataFormat = DataFormat.TwosComplement)]
		public List<long> removed
		{
			get
			{
				return this._removed;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
