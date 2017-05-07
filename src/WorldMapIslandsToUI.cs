using DG.Tweening;
using System;
using UnityEngine;

public class WorldMapIslandsToUI : IMonoBehaviour
{
	public static WorldMapIslandsToUI instance;

	public GameObject Blue;

	public GameObject Red;

	public GameObject openMiniMap;

	public GameObject closeMiniMap;

	public GameObject itemParent;

	public void OnDestroy()
	{
		WorldMapIslandsToUI.instance = null;
	}

	public override void Awake()
	{
		WorldMapIslandsToUI.instance = this;
		base.Awake();
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_OpenMiniMap, new EventManager.VoidDelegate(this.OpenMiniMap));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_CloseMiniMap, new EventManager.VoidDelegate(this.CloseMiniMap));
	}

	private void OpenMiniMap(GameObject o)
	{
		NGUITools.SetActive(this.openMiniMap, false);
		TweenPosition.Begin(this.ga, 0.6f, new Vector3(-144f, -144f, 0f)).SetOnFinished(new EventDelegate(delegate
		{
			this.tr.DOShakePosition(0.3f, new Vector3(-10f, -10f, 0f), 20, 90f, false);
			NGUITools.SetActive(this.closeMiniMap, true);
		}));
	}

	private void CloseMiniMap(GameObject o)
	{
		NGUITools.SetActive(this.closeMiniMap, false);
		TweenPosition.Begin(this.ga, 0.6f, new Vector3(144f, 144f, 0f)).SetOnFinished(new EventDelegate(delegate
		{
			NGUITools.SetActive(this.openMiniMap, true);
			this.tr.DOShakePosition(0.3f, new Vector3(10f, 10f, 0f), 20, 90f, false);
		}));
	}

	public GameObject CreateIsland(T_Island island)
	{
		if (island.OwnerType == OwnerType.user)
		{
			GameObject gameObject = NGUITools.AddChild(this.itemParent, this.Blue);
			if (island.uiType == WMapTipsType.myHome)
			{
				gameObject.GetComponent<UISprite>().spriteName = "位置标记";
			}
			gameObject.transform.localPosition = new Vector3(island.tr.localPosition.x * 280f / 24f - 140f, island.tr.localPosition.z * 280f / 24f - 140f);
			return gameObject;
		}
		GameObject gameObject2 = NGUITools.AddChild(this.itemParent, this.Red);
		gameObject2.transform.localPosition = new Vector3(island.tr.localPosition.x * 280f / 24f - 140f, island.tr.localPosition.z * 280f / 24f - 140f);
		return gameObject2;
	}
}
