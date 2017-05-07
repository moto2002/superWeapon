using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "VFDeadData")]
	[Serializable]
	public class VFDeadData : IExtensible
	{
		private int _attType = 0;

		private long _attId = 0L;

		private float _attDis = 0f;

		private int _attIndex = 0;

		private int _attCharType = 0;

		private int _attLV = 0;

		private int _attStar = 0;

		private int _attLife = 0;

		private float _attFrequency = 0f;

		private float _shootFrequency = 0f;

		private int _life = 0;

		private int _lifeAddition = 0;

		private int _breakArmor = 0;

		private int _breakArmorAddition = 0;

		private int _defBreak = 0;

		private int _defBreakAddition = 0;

		private int _defHit = 0;

		private int _defHitAddition = 0;

		private int _crit = 0;

		private int _critAddition = 0;

		private int _resist = 0;

		private int _resistAddition = 0;

		private int _critHR = 0;

		private int _critHRAddition = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "attType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attType
		{
			get
			{
				return this._attType;
			}
			set
			{
				this._attType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "attId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long attId
		{
			get
			{
				return this._attId;
			}
			set
			{
				this._attId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "attDis", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attDis
		{
			get
			{
				return this._attDis;
			}
			set
			{
				this._attDis = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "attIndex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attIndex
		{
			get
			{
				return this._attIndex;
			}
			set
			{
				this._attIndex = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "attCharType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attCharType
		{
			get
			{
				return this._attCharType;
			}
			set
			{
				this._attCharType = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "attLV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attLV
		{
			get
			{
				return this._attLV;
			}
			set
			{
				this._attLV = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "attStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attStar
		{
			get
			{
				return this._attStar;
			}
			set
			{
				this._attStar = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "attLife", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attLife
		{
			get
			{
				return this._attLife;
			}
			set
			{
				this._attLife = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "attFrequency", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attFrequency
		{
			get
			{
				return this._attFrequency;
			}
			set
			{
				this._attFrequency = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "shootFrequency", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float shootFrequency
		{
			get
			{
				return this._shootFrequency;
			}
			set
			{
				this._shootFrequency = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "life", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int life
		{
			get
			{
				return this._life;
			}
			set
			{
				this._life = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "lifeAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lifeAddition
		{
			get
			{
				return this._lifeAddition;
			}
			set
			{
				this._lifeAddition = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "breakArmor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int breakArmor
		{
			get
			{
				return this._breakArmor;
			}
			set
			{
				this._breakArmor = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "breakArmorAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int breakArmorAddition
		{
			get
			{
				return this._breakArmorAddition;
			}
			set
			{
				this._breakArmorAddition = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "defBreak", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defBreak
		{
			get
			{
				return this._defBreak;
			}
			set
			{
				this._defBreak = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "defBreakAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defBreakAddition
		{
			get
			{
				return this._defBreakAddition;
			}
			set
			{
				this._defBreakAddition = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "defHit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defHit
		{
			get
			{
				return this._defHit;
			}
			set
			{
				this._defHit = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "defHitAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defHitAddition
		{
			get
			{
				return this._defHitAddition;
			}
			set
			{
				this._defHitAddition = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "crit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int crit
		{
			get
			{
				return this._crit;
			}
			set
			{
				this._crit = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "critAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int critAddition
		{
			get
			{
				return this._critAddition;
			}
			set
			{
				this._critAddition = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "resist", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resist
		{
			get
			{
				return this._resist;
			}
			set
			{
				this._resist = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "resistAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resistAddition
		{
			get
			{
				return this._resistAddition;
			}
			set
			{
				this._resistAddition = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "critHR", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int critHR
		{
			get
			{
				return this._critHR;
			}
			set
			{
				this._critHR = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "critHRAddition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int critHRAddition
		{
			get
			{
				return this._critHRAddition;
			}
			set
			{
				this._critHRAddition = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
