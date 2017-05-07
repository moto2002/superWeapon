using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace msg
{
	[ProtoContract(Name = "SCOfficerTenRecruitData")]
	[Serializable]
	public class SCOfficerTenRecruitData : IExtensible
	{
		private long _id = 0L;

		private readonly List<OfficerTenRecruitData> _officerTenRecruitData = new List<OfficerTenRecruitData>();

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

		[ProtoMember(2, Name = "officerTenRecruitData", DataFormat = DataFormat.Default)]
		public List<OfficerTenRecruitData> officerTenRecruitData
		{
			get
			{
				return this._officerTenRecruitData;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
