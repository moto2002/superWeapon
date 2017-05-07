using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStartNotice : MonoBehaviour
{
	public static GameStartNotice _inst;

	public UITable table;

	public UIScrollView scrollView;

	public GameObject prefab;

	public GameObject close;

	public GameObject back;

	public void OnDestroy()
	{
		GameStartNotice._inst = null;
	}

	private void Awake()
	{
		GameStartNotice._inst = this;
		UIEventListener.Get(this.close).onClick = delegate(GameObject ga)
		{
			HUDTextTool.isCloseGameStartNotice = true;
			base.gameObject.SetActive(false);
		};
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		if (LoginEnterGame._instance)
		{
			LoginEnterGame._instance.EnterGame.GetComponent<BoxCollider>().enabled = false;
			LoginEnterGame._instance.ChangeUserGa.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void InitData(List<string> AllNotice)
	{
		this.table.DestoryChildren(true);
		foreach (string current in AllNotice)
		{
			GameObject gameObject = NGUITools.AddChild(this.table.gameObject, this.prefab);
			GmaeAnnounceItem component = gameObject.GetComponent<GmaeAnnounceItem>();
			component.showinfo.text = current;
		}
		this.table.Reposition();
		this.scrollView.ResetPosition();
		this.table.transform.localPosition = new Vector3(this.table.transform.localPosition.x, 440f, 0f);
	}
}
