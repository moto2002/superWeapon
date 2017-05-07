using System;
using UnityEngine;

public class RelationPanel : MonoBehaviour
{
	public GameObject obj;

	public UIToggle tFriend;

	public UIToggle tEnemy;

	public UIScrollView friendContent;

	public UIScrollView enemyContent;

	public UITable friendTable;

	public UITable enemyTable;

	public static RelationPanel ins;

	public void OnDestroy()
	{
		RelationPanel.ins = null;
	}

	private void Awake()
	{
		RelationPanel.ins = this;
	}

	private void OnEnable()
	{
		UIManager.inst.UIInUsed(true);
		this.tFriend.startsActive = true;
		this.tEnemy.startsActive = false;
		this.ShowFriend();
	}

	private void OnDisable()
	{
		UIManager.inst.UIInUsed(false);
	}

	public void ShowFriend()
	{
		this.enemyContent.gameObject.SetActive(false);
		this.friendContent.gameObject.SetActive(true);
		this.friendContent.ResetPosition();
	}

	public void ShowEnemy()
	{
		this.enemyContent.gameObject.SetActive(true);
		this.friendContent.gameObject.SetActive(false);
		this.enemyContent.ResetPosition();
	}
}
