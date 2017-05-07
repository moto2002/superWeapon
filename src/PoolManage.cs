using SimpleFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class PoolManage : MonoBehaviour
{
	public SpawnPool bundleModelPool;

	public SpawnPool localModelPool;

	public bool loadTankEnd;

	public bool loadTowerEnd;

	public bool loadResEnd;

	public bool loadDataEnd;

	public bool loadAudioEnd;

	public bool loadEffectEnd;

	public bool loadLoginEnd;

	public GameObject tmpModel;

	public bool isFirst = true;

	private Transform dieRes;

	private Transform resRes;

	private Transform towerRes;

	private Transform tankRes;

	private Transform bulletRes;

	private Transform containerBoat;

	private Transform containerSanBing;

	private Transform qizi;

	private Transform _1JModel;

	private Transform _2JModel;

	private Transform _3JModel;

	public Transform Tmp;

	public Transform TmpPool_Del;

	public static bool IsInitEnd;

	private Dictionary<string, Transform> Building_Army_Trees_Effect_Mirror = new Dictionary<string, Transform>();

	private static PoolManage ins;

	private Dictionary<string, int> allBundleObject = new Dictionary<string, int>();

	private Dictionary<SenceType, GameObject> senceMapPool = new Dictionary<SenceType, GameObject>();

	public static PoolManage Ins
	{
		get
		{
			return PoolManage.ins;
		}
	}

	public void OnDestroy()
	{
		PoolManage.ins = null;
	}

	public void Start()
	{
		base.gameObject.AddComponent<Push_NoticeManage>();
	}

	public void RegisterLua()
	{
		if (GameManager.Instance != null && GameManager.Instance.GetLuaManage() != null)
		{
			NewbieGuideWrap.Register(GameManager.Instance.GetLuaManage().GetL());
			objectWrap.Register(GameManager.Instance.GetLuaManage().GetL());
			GameStartWrap.Register(GameManager.Instance.GetLuaManage().GetL());
		}
		else
		{
			UnityEngine.Debug.LogError("GameManager.Instance.GetLuaManage()   is   null  ");
		}
	}

	[DebuggerHidden]
	public IEnumerator AddSenceManage()
	{
		PoolManage.<AddSenceManage>c__Iterator49 <AddSenceManage>c__Iterator = new PoolManage.<AddSenceManage>c__Iterator49();
		<AddSenceManage>c__Iterator.<>f__this = this;
		return <AddSenceManage>c__Iterator;
	}

	private void LocalModelInPool(Transform tr)
	{
		PrefabPool prefabPool = new PrefabPool(tr);
		prefabPool.preloadAmount = 0;
		prefabPool.cullDespawned = true;
		prefabPool.cullDelay = 60;
		prefabPool.cullMaxPerPass = 10;
		this.localModelPool._perPrefabPoolOptions.Add(prefabPool);
		this.localModelPool.CreatePrefabPool(prefabPool);
	}

	private void AddTmpGameObject()
	{
		this.dieRes = Resources.Load<Transform>(ResManager.F_EffectRes_Path + "Die_Ball");
		this.LocalModelInPool(this.dieRes);
		this.resRes = Resources.Load<Transform>(ResManager.UnitRes_Path + "Res");
		this.LocalModelInPool(this.resRes);
		this.towerRes = Resources.Load<Transform>(ResManager.UnitTower_Path + "Tower");
		this.LocalModelInPool(this.towerRes);
		this.tankRes = Resources.Load<Transform>(ResManager.UnitTank_Path + "Tank");
		this.LocalModelInPool(this.tankRes);
		this.bulletRes = Resources.Load<Transform>(ResManager.BulletRes_Path + "BulletNew");
		this.LocalModelInPool(this.bulletRes);
		this.containerBoat = Resources.Load<Transform>(ResManager.UnitBoat_Path + "Container");
		this.LocalModelInPool(this.containerBoat);
		this.containerSanBing = Resources.Load<Transform>(ResManager.UnitBoat_Path + "SanBing");
		this.LocalModelInPool(this.containerSanBing);
		this.qizi = Resources.Load<Transform>(ResManager.UnitTank_Path + "targetObj");
		this.LocalModelInPool(this.qizi);
		this._1JModel = Resources.Load<Transform>("1JModel");
		this.LocalModelInPool(this._1JModel);
		this._2JModel = Resources.Load<Transform>("2JModel");
		this.LocalModelInPool(this._2JModel);
		this._3JModel = Resources.Load<Transform>("3JModel");
		this.LocalModelInPool(this._3JModel);
		this.LocalModelInPool(this.Tmp);
	}

	public void Awake()
	{
		if (PoolManage.ins != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		PoolManage.ins = this;
		Application.targetFrameRate = GameSetting.FrameNum;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void WriteBundleRecord()
	{
		if (this.allBundleObject.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> current in this.allBundleObject)
			{
				stringBuilder.Append(current.Key);
				stringBuilder.Append(":");
				stringBuilder.Append(current.Value);
				stringBuilder.Append("\r\n");
			}
			Optimization.WriteBundle(stringBuilder.ToString());
		}
	}

	public Body_Model GetModelByBundleByName(string _name, Transform parent = null)
	{
		GameObject gameObject = null;
		if (this.Building_Army_Trees_Effect_Mirror.ContainsKey(_name.Trim()))
		{
			gameObject = this.bundleModelPool.Spawn(this.Building_Army_Trees_Effect_Mirror[_name.Trim()]).gameObject;
		}
		if (gameObject)
		{
			gameObject.name = _name;
			Body_Model compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Body_Model>(gameObject);
			if (parent != null)
			{
				compentIfNoAddOne.tr.parent = parent;
				compentIfNoAddOne.tr.localRotation = parent.localRotation;
			}
			else
			{
				compentIfNoAddOne.tr.parent = this.TmpPool_Del;
			}
			compentIfNoAddOne.tr.localRotation = Quaternion.identity;
			compentIfNoAddOne.tr.localPosition = Vector3.zero;
			compentIfNoAddOne.SetActive(true);
			return compentIfNoAddOne;
		}
		LogManage.LogError(string.Format("{0}模型为空", _name));
		return null;
	}

	public void DesSpawn_bundleModelPool(Transform tar)
	{
		tar.parent = base.transform;
		this.bundleModelPool.Despawn(tar);
		GameTools.RemoveComponent<MonoBehaviour>(tar.gameObject);
	}

	public void DesSpawn_localModelPool(Transform tar)
	{
		tar.parent = base.transform;
		this.localModelPool.Despawn(tar);
	}

	public GameObject GetModelByResourceByName(string name, Transform parent = null)
	{
		GameObject gameObject = null;
		if (name.Equals("1JModel"))
		{
			gameObject = this.localModelPool.Spawn(this._1JModel, Vector3.zero, Quaternion.identity).gameObject;
		}
		else if (name.Equals("2JModel"))
		{
			gameObject = this.localModelPool.Spawn(this._2JModel, Vector3.zero, Quaternion.identity).gameObject;
		}
		else if (name.Equals("3JModel"))
		{
			gameObject = this.localModelPool.Spawn(this._3JModel, Vector3.zero, Quaternion.identity).gameObject;
		}
		if (gameObject)
		{
			gameObject.name = name;
			if (parent != null)
			{
				gameObject.transform.parent = parent;
				gameObject.transform.localRotation = parent.localRotation;
			}
			else
			{
				gameObject.transform.parent = this.TmpPool_Del;
			}
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = Vector3.zero;
			return gameObject;
		}
		LogManage.LogError(string.Format("{0}模型为空", name));
		return null;
	}

	public GameObject GetOtherModelByName(string name, Transform parent = null)
	{
		GameObject gameObject = null;
		if (name.Equals("Container"))
		{
			gameObject = this.localModelPool.Spawn(this.containerBoat, Vector3.zero, Quaternion.identity).gameObject;
		}
		else if (name.Equals("SanBing"))
		{
			gameObject = this.localModelPool.Spawn(this.containerSanBing, Vector3.zero, Quaternion.identity).gameObject;
		}
		else if (name.Equals("Qizi"))
		{
			gameObject = this.localModelPool.Spawn(this.qizi, Vector3.zero, Quaternion.identity).gameObject;
		}
		gameObject.name = name;
		if (parent != null)
		{
			gameObject.transform.parent = parent;
			gameObject.transform.localRotation = parent.localRotation;
		}
		else
		{
			gameObject.transform.parent = this.TmpPool_Del;
		}
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localPosition = Vector3.zero;
		return gameObject;
	}

	public UnityEngine.Object GetObjectByBundle(AssetBundle bundle, string name)
	{
		if (bundle)
		{
			UnityEngine.Object[] array = bundle.LoadAll(typeof(GameObject));
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i].name.Trim().Equals(name.Trim()))
				{
					return array[i];
				}
			}
		}
		return null;
	}

	public void ObjectInMirrorByBundle(AssetBundle bundle)
	{
		if (bundle)
		{
			UnityEngine.Object[] array = bundle.LoadAll(typeof(GameObject));
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (this.Building_Army_Trees_Effect_Mirror.ContainsKey(array[i].name.Trim()))
				{
					LogManage.Log("已包含~~" + array[i].name.Trim());
				}
				this.Building_Army_Trees_Effect_Mirror.Add(array[i].name.Trim(), (array[i] as GameObject).transform);
				this.CreateModelPool((array[i] as GameObject).transform);
			}
		}
	}

	private void CreateModelPool(Transform prefab)
	{
		PrefabPool prefabPool = new PrefabPool(prefab);
		prefabPool.preloadAmount = 0;
		prefabPool.cullDespawned = true;
		prefabPool.cullAbove = 2;
		prefabPool.cullDelay = 60;
		prefabPool.cullMaxPerPass = 10;
		this.bundleModelPool._perPrefabPoolOptions.Add(prefabPool);
		this.bundleModelPool.CreatePrefabPool(prefabPool);
	}

	public void AddMirrorByBundle(Transform trans)
	{
		this.Building_Army_Trees_Effect_Mirror.Add(trans.name.Trim(), trans);
		PrefabPool prefabPool = new PrefabPool(trans);
		prefabPool.preloadAmount = 0;
		prefabPool.cullDespawned = true;
		prefabPool.cullAbove = 2;
		prefabPool.cullDelay = 60;
		prefabPool.cullMaxPerPass = 10;
		this.bundleModelPool._perPrefabPoolOptions.Add(prefabPool);
		this.bundleModelPool.CreatePrefabPool(prefabPool);
	}

	public Body_Model GetEffectByName(string _name, Transform parent = null)
	{
		Body_Model body_Model = null;
		if (body_Model != null)
		{
			if (parent != null)
			{
				body_Model.transform.parent = parent;
			}
			body_Model.transform.localRotation = Quaternion.identity;
			body_Model.transform.localPosition = Vector3.zero;
			body_Model.transform.localScale = Vector3.one;
			body_Model.SetActive(true);
			return body_Model;
		}
		if (!this.Building_Army_Trees_Effect_Mirror.ContainsKey(_name.Trim()))
		{
			LogManage.Log(string.Format("特效{0}为空", _name));
			return null;
		}
		GameObject gameObject = this.bundleModelPool.Spawn(this.Building_Army_Trees_Effect_Mirror[_name.Trim()]).gameObject;
		if (gameObject)
		{
			gameObject.name = _name;
			Body_Model compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<Body_Model>(gameObject);
			if (parent != null)
			{
				gameObject.transform.parent = parent;
			}
			else
			{
				gameObject.transform.parent = this.TmpPool_Del;
			}
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			return compentIfNoAddOne;
		}
		UnityEngine.Debug.Log(string.Format("特效{0}为空", _name));
		return null;
	}

	public GameObject GetGameTemp()
	{
		GameObject gameObject = this.localModelPool.Spawn(this.Tmp).gameObject;
		gameObject.transform.parent = this.TmpPool_Del;
		return gameObject;
	}

	public GameObject GetMapSence(SenceType sceneName)
	{
		if (this.senceMapPool.ContainsKey(sceneName))
		{
			return this.senceMapPool[sceneName];
		}
		LogManage.Log(string.Format("地图池·· 不包含{0}的地图资源", sceneName));
		return null;
	}

	public bool IsContainSence(SenceType sceneName)
	{
		return this.senceMapPool.ContainsKey(sceneName);
	}

	public string GetSenceName(SenceType sceneName)
	{
		if (this.IsContainSence(sceneName))
		{
			return this.senceMapPool[sceneName].name;
		}
		return string.Empty;
	}

	public void RemoveSence(SenceType sceneName)
	{
		if (this.senceMapPool.ContainsKey(sceneName))
		{
			this.senceMapPool.Remove(sceneName);
		}
	}

	public T_BulletNew GetBullet(Vector3 postion, Quaternion rotation, Transform parent = null)
	{
		GameObject gameObject = this.localModelPool.Spawn(this.bulletRes, postion, rotation).gameObject;
		T_BulletNew compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<T_BulletNew>(gameObject);
		if (parent != null)
		{
			compentIfNoAddOne.tr.parent = parent;
		}
		else
		{
			compentIfNoAddOne.tr.parent = this.TmpPool_Del;
		}
		return compentIfNoAddOne;
	}

	public T GetTank<T>(Vector3 postion, Quaternion rotation, Transform parent = null) where T : Character
	{
		T t = (T)((object)null);
		GameObject gameObject = this.localModelPool.Spawn(this.tankRes, postion, rotation).gameObject;
		t = GameTools.GetCompentIfNoAddOne<T>(gameObject);
		if (parent != null)
		{
			t.tr.parent = parent;
		}
		else
		{
			t.tr.parent = this.TmpPool_Del;
		}
		return t;
	}

	public T GetTower<T>(Vector3 postion, Quaternion rotation, Transform parent = null) where T : Character
	{
		T t = (T)((object)null);
		GameObject gameObject = this.localModelPool.Spawn(this.towerRes, postion, rotation).gameObject;
		t = GameTools.GetCompentIfNoAddOne<T>(gameObject);
		if (parent != null)
		{
			t.tr.parent = parent;
		}
		else
		{
			t.tr.parent = this.TmpPool_Del;
		}
		return t;
	}

	public T_Res GetRes(Vector3 postion, Quaternion rotation, Transform parent = null)
	{
		GameObject gameObject = this.localModelPool.Spawn(this.resRes, postion, rotation).gameObject;
		T_Res compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<T_Res>(gameObject);
		if (parent != null)
		{
			compentIfNoAddOne.tr.parent = parent;
		}
		else
		{
			compentIfNoAddOne.tr.parent = this.TmpPool_Del;
		}
		return compentIfNoAddOne;
	}

	private DieBall GetDieBall(Vector3 postion, Quaternion rotation, Transform parent = null)
	{
		DieBall dieBall = null;
		if (this.dieRes != null)
		{
			Transform transform = this.localModelPool.Spawn(this.dieRes, postion, rotation);
			GameObject gameObject = transform.gameObject;
			dieBall = GameTools.GetCompentIfNoAddOne<DieBall>(gameObject);
			if (parent != null)
			{
				dieBall.tr.parent = parent;
			}
			else
			{
				dieBall.tr.parent = this.TmpPool_Del;
			}
		}
		return dieBall;
	}

	public DieBall CreatEffect(string resName, Vector3 position, Quaternion rota, Transform parent = null)
	{
		if (resName != string.Empty)
		{
			DieBall dieBall = this.GetDieBall(position, rota, parent);
			if (dieBall)
			{
				dieBall.effectRes = resName;
				if (parent == null)
				{
					dieBall.tr.parent = SenceManager.inst.bulletPool;
				}
				else
				{
					dieBall.tr.parent = parent;
				}
				dieBall.SetInfo();
			}
			return dieBall;
		}
		return null;
	}
}
