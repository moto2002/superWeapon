using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "EventData")]
	[Serializable]
	public class EventData : IExtensible
	{
		private int _eventType = 0;

		private long _eventId = 0L;

		private long _endTime = 0L;

		private int _randomSeed = 0;

		private readonly List<OperationEventData> _operData = new List<OperationEventData>();

		private readonly List<OperationTankPos> _tankPoses = new List<OperationTankPos>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "eventType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int eventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				this._eventType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "eventId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long eventId
		{
			get
			{
				return this._eventId;
			}
			set
			{
				this._eventId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "randomSeed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, Name = "operData", DataFormat = DataFormat.Default)]
		public List<OperationEventData> operData
		{
			get
			{
				return this._operData;
			}
		}

		[ProtoMember(6, Name = "tankPoses", DataFormat = DataFormat.Default)]
		public List<OperationTankPos> tankPoses
		{
			get
			{
				return this._tankPoses;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
