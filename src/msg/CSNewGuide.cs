using ProtoBuf;
using System;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "CSNewGuide")]
	[Serializable]
	public class CSNewGuide : IExtensible
	{
		private int _guideId = 0;

		private int _tmp = 0;

		private int _taskGuideId = 0;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "guideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int guideId
		{
			get
			{
				return this._guideId;
			}
			set
			{
				this._guideId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "tmp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tmp
		{
			get
			{
				return this._tmp;
			}
			set
			{
				this._tmp = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "taskGuideId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskGuideId
		{
			get
			{
				return this._taskGuideId;
			}
			set
			{
				this._taskGuideId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
