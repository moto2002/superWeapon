using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("Path-o-logical/PoolManager/SpawnPool")]
public sealed class SpawnPool : MonoBehaviour, IList<Transform>, ICollection<Transform>, IEnumerable<Transform>, IEnumerable
{
	public string poolName = string.Empty;

	public bool dontDestroyOnLoad;

	public bool logMessages;

	public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();

	public Dictionary<object, bool> prefabsFoldOutStates = new Dictionary<object, bool>();

	[HideInInspector]
	public float maxParticleDespawnTime = 60f;

	public PrefabsDict prefabs = new PrefabsDict();

	public Dictionary<object, bool> _editorListItemStates = new Dictionary<object, bool>();

	private List<PrefabPool> _prefabPools = new List<PrefabPool>();

	private List<Transform> _spawned = new List<Transform>();

	public Transform group
	{
		get;
		private set;
	}

	public Transform this[int index]
	{
		get
		{
			return this._spawned[index];
		}
		set
		{
			throw new NotImplementedException("Read-only.");
		}
	}

	public int Count
	{
		get
		{
			return this._spawned.Count;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	[DebuggerHidden]
	IEnumerator IEnumerable.GetEnumerator()
	{
		SpawnPool.GetEnumerator>c__Iterator1 getEnumerator>c__Iterator = new SpawnPool.GetEnumerator>c__Iterator1();
		getEnumerator>c__Iterator.<>f__this = this;
		return getEnumerator>c__Iterator;
	}

	bool ICollection<Transform>.Remove(Transform item)
	{
		throw new NotImplementedException();
	}

	private void Awake()
	{
		if (this.dontDestroyOnLoad)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		this.group = base.transform;
		if (this.poolName == string.Empty)
		{
			this.poolName = this.group.name.Replace("Pool", string.Empty);
			this.poolName = this.poolName.Replace("(Clone)", string.Empty);
		}
		if (this.logMessages)
		{
			UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Initializing..", this.poolName));
		}
		foreach (PrefabPool current in this._perPrefabPoolOptions)
		{
			if (current.prefab == null)
			{
				UnityEngine.Debug.LogWarning(string.Format("Initialization Warning: Pool '{0}' contains a PrefabPool with no prefab reference. Skipping.", this.poolName));
			}
			else
			{
				current.inspectorInstanceConstructor();
				this.CreatePrefabPool(current);
			}
		}
		PoolManager.Pools.Add(this);
	}

	private void OnDestroy()
	{
		if (this.logMessages)
		{
			UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Destroying...", this.poolName));
		}
		PoolManager.Pools.Remove(this);
		base.StopAllCoroutines();
		this._spawned.Clear();
		foreach (PrefabPool current in this._prefabPools)
		{
			current.SelfDestruct();
		}
		this._prefabPools.Clear();
		this.prefabs._Clear();
	}

	public List<Transform> CreatePrefabPool(PrefabPool prefabPool)
	{
		if (this.GetPrefab(prefabPool.prefab) == null)
		{
			prefabPool.spawnPool = this;
			this._prefabPools.Add(prefabPool);
			this.prefabs._Add(prefabPool.prefab.name, prefabPool.prefab);
		}
		List<Transform> list = new List<Transform>();
		if (!prefabPool.preloaded)
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Preloading {1} {2}", this.poolName, prefabPool.preloadAmount, prefabPool.prefab.name));
			}
			list.AddRange(prefabPool.PreloadInstances());
		}
		return list;
	}

	public void Add(Transform instance, string prefabName, bool despawn, bool parent)
	{
		foreach (PrefabPool current in this._prefabPools)
		{
			if (current.prefabGO == null)
			{
				UnityEngine.Debug.LogError("Unexpected Error: PrefabPool.prefabGO is null");
				return;
			}
			if (current.prefabGO.name == prefabName)
			{
				current.AddUnpooled(instance, despawn);
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Adding previously unpooled instance {1}", this.poolName, instance.name));
				}
				if (parent)
				{
					instance.parent = this.group;
				}
				if (!despawn)
				{
					this._spawned.Add(instance);
				}
				return;
			}
		}
		UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool {1} not found.", this.poolName, prefabName));
	}

	public void Add(Transform item)
	{
		string message = "Use SpawnPool.Spawn() to properly add items to the pool.";
		throw new NotImplementedException(message);
	}

	public void Remove(Transform item)
	{
		string message = "Use Despawn() to properly manage items that should remain in the pool but be deactivated.";
		throw new NotImplementedException(message);
	}

	public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
	{
		if (prefab == null)
		{
			return null;
		}
		Transform transform;
		foreach (PrefabPool current in this._prefabPools)
		{
			if (current.prefabGO == prefab.gameObject)
			{
				transform = current.SpawnInstance(pos, rot);
				Transform result;
				if (transform == null)
				{
					result = null;
					return result;
				}
				if (transform.parent != this.group)
				{
					transform.parent = this.group;
				}
				this._spawned.Add(transform);
				result = transform;
				return result;
			}
		}
		PrefabPool prefabPool = new PrefabPool(prefab);
		this.CreatePrefabPool(prefabPool);
		transform = prefabPool.SpawnInstance(pos, rot);
		transform.parent = this.group;
		this._spawned.Add(transform);
		return transform;
	}

	public Transform Spawn(Transform prefab)
	{
		return this.Spawn(prefab, Vector3.zero, Quaternion.identity);
	}

	public ParticleEmitter Spawn(ParticleEmitter prefab, Vector3 pos, Quaternion quat)
	{
		Transform transform = this.Spawn(prefab.transform, pos, quat);
		if (transform == null)
		{
			return null;
		}
		ParticleAnimator component = transform.GetComponent<ParticleAnimator>();
		if (component != null)
		{
			component.autodestruct = false;
		}
		ParticleEmitter component2 = transform.GetComponent<ParticleEmitter>();
		component2.emit = true;
		base.StartCoroutine(this.ListenForEmitDespawn(component2));
		return component2;
	}

	public ParticleSystem Spawn(ParticleSystem prefab, Vector3 pos, Quaternion quat)
	{
		Transform transform = this.Spawn(prefab.transform, pos, quat);
		ParticleSystem component = transform.GetComponent<ParticleSystem>();
		base.StartCoroutine(this.ListenForEmitDespawn(component));
		return component;
	}

	public ParticleEmitter Spawn(ParticleEmitter prefab, Vector3 pos, Quaternion quat, string colorPropertyName, Color color)
	{
		Transform transform = this.Spawn(prefab.transform, pos, quat);
		if (transform == null)
		{
			return null;
		}
		ParticleAnimator component = transform.GetComponent<ParticleAnimator>();
		if (component != null)
		{
			component.autodestruct = false;
		}
		ParticleEmitter component2 = transform.GetComponent<ParticleEmitter>();
		component2.renderer.material.SetColor(colorPropertyName, color);
		component2.emit = true;
		base.StartCoroutine(this.ListenForEmitDespawn(component2));
		return component2;
	}

	public void Despawn(Transform xform)
	{
		bool flag = false;
		foreach (PrefabPool current in this._prefabPools)
		{
			if (current.spawned.Contains(xform))
			{
				flag = current.DespawnInstance(xform);
				break;
			}
			if (current.despawned.Contains(xform))
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} has already been despawned. You cannot despawn something more than once!", this.poolName, xform.name));
				current.despawned.Remove(xform);
				UnityEngine.Object.Destroy(xform.gameObject);
				return;
			}
		}
		if (!flag)
		{
			UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} not found in SpawnPool", this.poolName, xform.name));
			UnityEngine.Object.Destroy(xform.gameObject);
			return;
		}
		this._spawned.Remove(xform);
	}

	public void Despawn(Transform instance, float seconds)
	{
		base.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds));
	}

	[DebuggerHidden]
	private IEnumerator DoDespawnAfterSeconds(Transform instance, float seconds)
	{
		SpawnPool.<DoDespawnAfterSeconds>c__Iterator2 <DoDespawnAfterSeconds>c__Iterator = new SpawnPool.<DoDespawnAfterSeconds>c__Iterator2();
		<DoDespawnAfterSeconds>c__Iterator.seconds = seconds;
		<DoDespawnAfterSeconds>c__Iterator.instance = instance;
		<DoDespawnAfterSeconds>c__Iterator.<$>seconds = seconds;
		<DoDespawnAfterSeconds>c__Iterator.<$>instance = instance;
		<DoDespawnAfterSeconds>c__Iterator.<>f__this = this;
		return <DoDespawnAfterSeconds>c__Iterator;
	}

	public void DespawnAll()
	{
		List<Transform> list = new List<Transform>(this._spawned);
		foreach (Transform current in list)
		{
			this.Despawn(current);
		}
	}

	public void ClearDespawnAll()
	{
		for (int i = 0; i < this._prefabPools.Count; i++)
		{
			if (!object.ReferenceEquals(this._prefabPools[i], null))
			{
				for (int j = 0; j < this._prefabPools[i].despawned.Count; j++)
				{
					if (!object.ReferenceEquals(this._prefabPools[i].despawned[j], null))
					{
						UnityEngine.Object.Destroy(this._prefabPools[i].despawned[j].gameObject);
					}
				}
				this._prefabPools[i].despawned.Clear();
			}
		}
	}

	public bool IsSpawned(Transform instance)
	{
		return this._spawned.Contains(instance);
	}

	public Transform GetPrefab(Transform prefab)
	{
		foreach (PrefabPool current in this._prefabPools)
		{
			if (current.prefabGO == null)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
			}
			if (current.prefabGO == prefab.gameObject)
			{
				return current.prefab;
			}
		}
		return null;
	}

	public GameObject GetPrefab(GameObject prefab)
	{
		foreach (PrefabPool current in this._prefabPools)
		{
			if (current.prefabGO == null)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
			}
			if (current.prefabGO == prefab)
			{
				return current.prefabGO;
			}
		}
		return null;
	}

	[DebuggerHidden]
	private IEnumerator ListenForEmitDespawn(ParticleEmitter emitter)
	{
		SpawnPool.<ListenForEmitDespawn>c__Iterator3 <ListenForEmitDespawn>c__Iterator = new SpawnPool.<ListenForEmitDespawn>c__Iterator3();
		<ListenForEmitDespawn>c__Iterator.emitter = emitter;
		<ListenForEmitDespawn>c__Iterator.<$>emitter = emitter;
		<ListenForEmitDespawn>c__Iterator.<>f__this = this;
		return <ListenForEmitDespawn>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator ListenForEmitDespawn(ParticleSystem emitter)
	{
		SpawnPool.<ListenForEmitDespawn>c__Iterator4 <ListenForEmitDespawn>c__Iterator = new SpawnPool.<ListenForEmitDespawn>c__Iterator4();
		<ListenForEmitDespawn>c__Iterator.emitter = emitter;
		<ListenForEmitDespawn>c__Iterator.<$>emitter = emitter;
		<ListenForEmitDespawn>c__Iterator.<>f__this = this;
		return <ListenForEmitDespawn>c__Iterator;
	}

	public bool Contains(Transform item)
	{
		string message = "Use IsSpawned(Transform instance) instead.";
		throw new NotImplementedException(message);
	}

	public void CopyTo(Transform[] array, int arrayIndex)
	{
		this._spawned.CopyTo(array, arrayIndex);
	}

	[DebuggerHidden]
	public IEnumerator<Transform> GetEnumerator()
	{
		SpawnPool.<GetEnumerator>c__Iterator5 <GetEnumerator>c__Iterator = new SpawnPool.<GetEnumerator>c__Iterator5();
		<GetEnumerator>c__Iterator.<>f__this = this;
		return <GetEnumerator>c__Iterator;
	}

	public int IndexOf(Transform item)
	{
		throw new NotImplementedException();
	}

	public void Insert(int index, Transform item)
	{
		throw new NotImplementedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}
}
