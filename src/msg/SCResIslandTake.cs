using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCResIslandTake")]
	[Serializable]
	public class SCResIslandTake : IExtensible
	{
		private long _id = 0L;

		private readonly List<KVStruct> _takeTime = new List<KVStruct>();

		private readonly List<KVStruct> _resTmp = new List<KVStruct>();

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

		[ProtoMember(2, Name = "takeTime", DataFormat = DataFormat.Default)]
		public List<KVStruct> takeTime
		{
			get
			{
				return this._takeTime;
			}
		}

		[ProtoMember(3, Name = "resTmp", DataFormat = DataFormat.Default)]
		public List<KVStruct> resTmp
		{
			get
			{
				return this._resTmp;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}