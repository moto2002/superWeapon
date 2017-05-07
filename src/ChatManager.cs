using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Chat Manager")]
public class ChatManager : MonoBehaviour
{
	public static ChatManager instance;

	public string[] chatMessages;

	public LookAtTarget cameraLookAt;

	private List<ChatParticipant> mParticipants = new List<ChatParticipant>();

	private int mCurrentChatter;

	private int mCurrentMessage;

	private bool mDisplay;

	private void Awake()
	{
		ChatManager.instance = this;
	}

	private void OnDestroy()
	{
		ChatManager.instance = null;
	}

	public void AddParticipant(ChatParticipant participant)
	{
		this.mParticipants.Add(participant);
	}

	private void Update()
	{
		if (!this.mDisplay && this.chatMessages != null)
		{
			base.StartCoroutine(this.ProgressChat());
		}
	}

	[DebuggerHidden]
	private IEnumerator ProgressChat()
	{
		ChatManager.<ProgressChat>c__Iterator6A <ProgressChat>c__Iterator6A = new ChatManager.<ProgressChat>c__Iterator6A();
		<ProgressChat>c__Iterator6A.<>f__this = this;
		return <ProgressChat>c__Iterator6A;
	}
}
