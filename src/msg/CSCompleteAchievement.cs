using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSCompleteAchievement")]
	[Serializable]
	public class CSCompleteAchievement : IExtensible
	{
		private int _achievementId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "achievementId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int achievementId
		{
			get
			{
				return this._achievementId;
			}
			set
			{
				this._achievementId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
