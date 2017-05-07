using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageMonitor : MonoBehaviour
{
	public static DamageMonitor inst;

	public Dictionary<Vector2, DamageRecord> DamageRecordList = new Dictionary<Vector2, DamageRecord>();

	private int key;

	public void OnDestroy()
	{
		DamageMonitor.inst = null;
	}

	private void Awake()
	{
		DamageMonitor.inst = this;
	}

	public void AddDamageRecord(int skillID, int airIndex, int damage)
	{
		DamageRecord damageRecord = this.DamageRecordList.Values.SingleOrDefault((DamageRecord a) => (a.skillID != 0 && a.skillID == skillID) || (a.airIndex != 0 && a.airIndex == airIndex));
		if (damageRecord != null)
		{
			damageRecord.totalDamage += damage;
		}
		else
		{
			damageRecord = new DamageRecord();
			damageRecord.skillID = skillID;
			damageRecord.airIndex = airIndex;
			damageRecord.totalDamage += damage;
			this.DamageRecordList.Add(new Vector2((float)skillID, (float)airIndex), damageRecord);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			foreach (KeyValuePair<Vector2, DamageRecord> current in this.DamageRecordList)
			{
				if (current.Value.airIndex == 0)
				{
					Debug.Log(string.Concat(new object[]
					{
						"由skillid为",
						current.Value.skillID,
						"的技能造成的累计伤害为：",
						current.Value.totalDamage
					}));
				}
				else if (current.Value.skillID == 0)
				{
					Debug.Log(string.Concat(new object[]
					{
						"由index为",
						current.Value.airIndex,
						"的战机造成的累计伤害为：",
						current.Value.totalDamage
					}));
				}
			}
		}
	}
}
