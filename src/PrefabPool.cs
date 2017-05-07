using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class PrefabPool
{
	public Transform prefab;

	internal GameObject prefabGO;

	public int preloadAmount = 1;

	public bool limitInstances;

	public int limitAmount = 100;

	public bool cullDespawned;

	public int cullAbove = 50;

	public int cullDelay = 60;

	public int cullMaxPerPass = 5;

	public bool _logMessages;

	private bool forceLoggingSilent;

	internal SpawnPool spawnPool;

	private bool cullingActive;

	internal List<Transform> spawned = new List<Transform>();

	internal List<Transform> despawned = new List<Transform>();

	private bool _preloaded;

	private bool logMessages
	{
		get
		{
			if (this.forceLoggingSilent)
			{
				return false;
			}
			if (this.spawnPool.logMessages)
			{
				return this.spawnPool.logMessages;
			}
			return this._logMessages;
		}
	}

	internal int totalCount
	{
		get
		{
			int num = 0;
			num += this.spawned.Count;
			return num + this.despawned.Count;
		}
	}

	internal bool preloaded
	{
		get
		{
			return this._preloaded;
		}
		private set
		{
			this._preloaded = value;
		}
	}

	public PrefabPool(Transform prefab)
	{
		this.prefab = prefab;
		this.prefabGO = prefab.gameObject;
	}

	public PrefabPool()
	{
	}

	internal void inspectorInstanceConstructor()
	{
		this.prefabGO = this.prefab.gameObject;
		this.spawned = new List<Transform>();
		this.despawned = new List<Transform>();
	}

	public void SelfDestruct()
	{
		this.prefab = null;
		this.prefabGO = null;
		this.spawnPool = null;
		foreach (Transform current in this.despawned)
		{
			UnityEngine.Object.Destroy(current);
		}
		foreach (Transform current2 in this.spawned)
		{
			UnityEngine.Object.Destroy(current2);
		}
		this.spawned.Clear();
		this.despawned.Clear();
	}

	internal bool DespawnInstance(Transform xform)
	{
		if (this.logMessages)
		{
			UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Despawning '{2}'", this.spawnPool.poolName, this.prefab.name, xform.name));
		}
		this.spawned.Remove(xform);
		this.despawned.Add(xform);
		xform.gameObject.BroadcastMessage("OnDespawned", SendMessageOptions.DontRequireReceiver);
		xform.gameObject.SetActiveRecursively(false);
		if (!this.cullingActive && this.cullDespawned && this.totalCount > this.cullAbove)
		{
			this.cullingActive = true;
			this.spawnPool.StartCoroutine(this.CullDespawned());
		}
		return true;
	}

	[DebuggerHidden]
	internal IEnumerator CullDespawned()
	{
		PrefabPool.<CullDespawned>c__Iterator6 <CullDespawned>c__Iterator = new PrefabPool.<CullDespawned>c__Iterator6();
		<CullDespawned>c__Iterator.<>f__this = this;
		return <CullDespawned>c__Iterator;
	}

	internal Transform SpawnInstance(Vector3 pos, Quaternion rot)
	{
		Transform transform;
		if (this.despawned.Count == 0)
		{
			transform = this.SpawnNew(pos, rot);
		}
		else
		{
			transform = this.despawned[0];
			this.despawned.RemoveAt(0);
			this.spawned.Add(transform);
			if (transform == null)
			{
				string message = "Make sure you didn't delete a despawned instance directly.";
				throw new MissingReferenceException(message);
			}
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): respawning '{2}'.", this.spawnPool.poolName, this.prefab.name, transform.name));
			}
			transform.position = pos;
			transform.rotation = rot;
			transform.gameObject.SetActiveRecursively(true);
		}
		if (transform != null)
		{
			transform.gameObject.BroadcastMessage("OnSpawned", SendMessageOptions.DontRequireReceiver);
		}
		return transform;
	}

	internal Transform SpawnNew(Vector3 pos, Quaternion rot)
	{
		if (this.limitInstances && this.totalCount >= this.limitAmount)
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): LIMIT REACHED! Not creating new instances!", this.spawnPool.poolName, this.prefab.name));
			}
			return null;
		}
		if (pos == Vector3.zero)
		{
			pos = this.spawnPool.group.position;
		}
		if (rot == Quaternion.identity)
		{
			rot = this.spawnPool.group.rotation;
		}
		Transform transform = (Transform)UnityEngine.Object.Instantiate(this.prefab, pos, rot);
		this.nameInstance(transform);
		transform.parent = this.spawnPool.group;
		this.spawned.Add(transform);
		if (this.logMessages)
		{
			UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Spawned new instance '{2}'.", this.spawnPool.poolName, this.prefab.name, transform.name));
		}
		return transform;
	}

	internal void AddUnpooled(Transform inst, bool despawn)
	{
		this.nameInstance(inst);
		if (despawn)
		{
			inst.gameObject.SetActiveRecursively(false);
			this.despawned.Add(inst);
		}
		else
		{
			this.spawned.Add(inst);
		}
	}

	internal List<Transform> PreloadInstances()
	{
		List<Transform> list = new List<Transform>();
		if (this.preloaded)
		{
			UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Already preloaded! You cannot preload twice. If you are running this through code, make sure it isn't also defined in the Inspector.", this.spawnPool.poolName, this.prefab.name));
			return list;
		}
		if (this.prefab == null)
		{
			UnityEngine.Debug.LogError(string.Format("SpawnPool {0} ({1}): Prefab cannot be null.", this.spawnPool.poolName, this.prefab.name));
			return list;
		}
		this.forceLoggingSilent = true;
		while (this.totalCount < this.preloadAmount)
		{
			Transform transform = this.SpawnNew(Vector3.zero, Quaternion.identity);
			if (transform == null)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0} ({1}): You turned ON 'Limit Instances' and entered a 'Limit Amount' greater than the 'Preload Amount'!", this.spawnPool.poolName, this.prefab.name));
			}
			else
			{
				this.DespawnInstance(transform);
				list.Add(transform);
			}
		}
		this.forceLoggingSilent = false;
		if (this.cullDespawned && this.totalCount > this.cullAbove)
		{
			UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0} ({1}): You turned ON Culling and entered a 'Cull Above' threshold greater than the 'Preload Amount'! This will cause the culling feature to trigger immediatly, which is wrong conceptually. Only use culling for extreme situations. See the docs.", this.spawnPool.poolName, this.prefab.name));
		}
		return list;
	}

	private void nameInstance(Transform instance)
	{
		instance.name += (this.totalCount + 1).ToString("#000");
	}
}
