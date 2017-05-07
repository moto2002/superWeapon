using msg;
using System;
using System.Collections.Generic;

public class EventData
{
	public int eventType;

	public long eventId = 111L;

	public float endTime;

	public int randomSeed;

	public List<KVStruct> fighterTech = new List<KVStruct>();

	public List<SCArmyData> fighterArmy = new List<SCArmyData>();

	public List<OperationEventData> operData = new List<OperationEventData>();

	public List<OperationTankPos> tankPoses = new List<OperationTankPos>();
}
