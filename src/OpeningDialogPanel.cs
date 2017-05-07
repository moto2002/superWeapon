using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class OpeningDialogPanel : MonoBehaviour
{
	public TypewriterEffect textWrite;

	public GameObject dialogeText;

	public GameObject Welcome;

	public GameObject BackTexture;

	public GameObject black;

	private DieBall callBack;

	private bool isStartGame;

	public void Awake()
	{
		this.callBack = PoolManage.Ins.CreatEffect("comeback", Vector3.zero, Quaternion.identity, base.transform);
		this.callBack.ga.AddComponent<RenderQueueEdit>();
		this.callBack.LifeTime = 0f;
		UIEventListener.Get(this.BackTexture).onClick = delegate(GameObject ga)
		{
			if (this.textWrite.isEnd)
			{
				this.StartGame();
			}
			else
			{
				this.textWrite.ToEnd();
			}
		};
	}

	private void StartGame()
	{
		if (!this.isStartGame)
		{
			this.isStartGame = true;
			base.StartCoroutine_Auto(this.GoHome());
		}
	}

	[DebuggerHidden]
	private IEnumerator GoHome()
	{
		OpeningDialogPanel.<GoHome>c__Iterator48 <GoHome>c__Iterator = new OpeningDialogPanel.<GoHome>c__Iterator48();
		<GoHome>c__Iterator.<>f__this = this;
		return <GoHome>c__Iterator;
	}
}
