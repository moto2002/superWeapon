using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCFlushSkill")]
	[Serializable]
	public class SCFlushSkill : IExtensible
	{
		private long _id = 0L;

		private int _debris = 0;

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

		[ProtoMember(2, IsRequired = false, Name = "debris", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int debris
		{
			get
			{
				return this._debris;
			}
			set
			{
				this._debris = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
