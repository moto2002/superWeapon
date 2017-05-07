using System;
using UnityEngine;

public class T_CommanderRoad : MonoBehaviour
{
	public static T_CommanderRoad inst;

	public Transform[] T_CommanderRoad_tr = new Transform[6];

	private bool Enable;

	private T_Tank commander;

	private float time1;

	private float time0;

	private int tr_count;

	private Transform commander_mb;

	private int mb;

	private Transform this_commander;

	public void OnDestroy()
	{
		T_CommanderRoad.inst = null;
	}

	private void Awake()
	{
		T_CommanderRoad.inst = this;
	}

	private void Start()
	{
		this.tr_count = this.T_CommanderRoad_tr.Length;
		this.time0 = 3f;
	}

	private void CreateCommando(int index)
	{
		if (this.this_commander != null)
		{
			UnityEngine.Object.Destroy(this.this_commander.gameObject);
		}
		GameObject gameObject = new GameObject("Commander");
		gameObject.transform.parent = base.transform;
		gameObject.AddComponent<T_CommanderHome>();
		T_CommanderHome component = gameObject.GetComponent<T_CommanderHome>();
		component.TC_Road = this;
		component.Init(index);
		this.this_commander = gameObject.transform;
	}

	private void Update()
	{
	}
}
