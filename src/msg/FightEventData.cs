using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "FightEventData")]
	[Serializable]
	public class FightEventData : IExtensible
	{
		private int _deadType = 0;

		private long _deadId = 0L;

		private int _deadIdx = 0;

		private long _deadContid = 0L;

		private float _poxX = 0f;

		private float _poxZ = 0f;

		private int _randomSeed = 0;

		private int _skillPoint = 0;

		private int _num = 0;

		private int _buyArmyMoney = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "deadType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deadType
		{
			get
			{
				return this._deadType;
			}
			set
			{
				this._deadType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "deadId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long deadId
		{
			get
			{
				return this._deadId;
			}
			set
			{
				this._deadId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "deadIdx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deadIdx
		{
			get
			{
				return this._deadIdx;
			}
			set
			{
				this._deadIdx = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "deadContid", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long deadContid
		{
			get
			{
				return this._deadContid;
			}
			set
			{
				this._deadContid = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "poxX", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float poxX
		{
			get
			{
				return this._poxX;
			}
			set
			{
				this._poxX = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "poxZ", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float poxZ
		{
			get
			{
				return this._poxZ;
			}
			set
			{
				this._poxZ = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "randomSeed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int randomSeed
		{
			get
			{
				return this._randomSeed;
			}
			set
			{
				this._randomSeed = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "skillPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillPoint
		{
			get
			{
				return this._skillPoint;
			}
			set
			{
				this._skillPoint = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "buyArmyMoney", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyArmyMoney
		{
			get
			{
				return this._buyArmyMoney;
			}
			set
			{
				this._buyArmyMoney = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
