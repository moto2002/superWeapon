using System;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Chat Participant")]
public class ChatParticipant : MonoBehaviour
{
	public GameObject prefab;

	public Transform lookAt;

	private HUDText mText;

	public HUDText hudText
	{
		get
		{
			return this.mText;
		}
	}

	private void Start()
	{
		if (HUDRoot.go == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		GameObject gameObject = NGUITools.AddChild(HUDRoot.go, this.prefab);
		this.mText = gameObject.GetComponentInChildren<HUDText>();
		gameObject.AddComponent<UIFollowTarget>().target = base.transform;
		if (ChatManager.instance != null)
		{
			ChatManager.instance.AddParticipant(this);
		}
	}
}
