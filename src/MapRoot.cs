using System;
using UnityEngine;

public class MapRoot : MonoBehaviour
{
	public static MapRoot inst;

	private Transform tr;

	private Agent_Scene HomeScene;

	public void OnDestroy()
	{
		MapRoot.inst = null;
	}

	private void Awake()
	{
		MapRoot.inst = this;
		this.tr = base.transform;
		this.HomeScene = GameTools.GetCompentIfNoAddOne<Agent_Scene>(base.gameObject);
		if (ResManager.scene != null)
		{
			ResManager.scene.Unload(false);
		}
		base.gameObject.name = Loading.senceName;
	}
}
