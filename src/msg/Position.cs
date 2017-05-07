using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "Position")]
	[Serializable]
	public class Position : IExtensible
	{
		private int _x = 0;

		private int _y = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "x", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int x
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "y", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
