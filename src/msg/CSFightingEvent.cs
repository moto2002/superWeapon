using ProtoBuf;
using System;
using System.Collections.Generic;

namespace msg
{
	[ProtoContract(Name = "CSFightingEvent")]
	[Serializable]
	public class CSFightingEvent : IExtensible
	{
		private readonly List<FightEventData> _data = new List<FightEventData>();

		private readonly List<VFDeadData> _vfData = new List<VFDeadData>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "data", DataFormat = DataFormat.Default)]
		public List<FightEventData> data
		{
			get
			{
				return this._data;
			}
		}

		[ProtoMember(2, Name = "vfData", DataFormat = DataFormat.Default)]
		public List<VFDeadData> vfData
		{
			get
			{
				return this._vfData;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
