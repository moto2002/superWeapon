using msg;
using System;
using System.Collections.Generic;
using System.Linq;

public class armyInfoInBuilding
{
	public long buildingID;

	public List<KVStruct> armyFunced = new List<KVStruct>();

	public List<KVStruct> armyFuncedToUI = new List<KVStruct>();

	public List<KVStruct> armyFuncing = new List<KVStruct>();

	public KVStruct GetArmyNearestDataFuncing()
	{
		if (this.armyFuncing.Count > 0)
		{
			return (from a in this.armyFuncing
			orderby a.key
			select a).First<KVStruct>();
		}
		return null;
	}

	public void RemoveArmyFuncingData(long key)
	{
		if (this.armyFuncing.Any((KVStruct a) => a.key == key))
		{
			this.armyFuncing.Remove(this.armyFuncing.Single((KVStruct a) => a.key == key));
		}
	}
}
