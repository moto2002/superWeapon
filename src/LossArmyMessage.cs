using System;
using System.Collections.Generic;
using UnityEngine;

public class LossArmyMessage : MonoBehaviour
{
	public GameObject ThisGa;

	public UISprite ThisBG;

	public static LossArmyMessage _inst;

	public UILabel Title;

	public UILabel Des1;

	public UILabel Des2;

	public GameObject LossArmyPrefab;

	public UIGrid Grid1;

	public UIGrid Grid2;

	public Dictionary<int, int> LossArmyList = new Dictionary<int, int>();

	private bool _2DLock;

	public void OnEnable()
	{
		this._2DLock = ButtonClick.newbiLock;
		NewbieGuidePanel._instance.HideSelf();
		HUDTextTool.inst.GUIGameObject.SetActive(false);
		ButtonClick.newbiLock = false;
	}

	public void OnDisable()
	{
		ButtonClick.newbiLock = this._2DLock;
	}

	private void OnDestroy()
	{
		HUDTextTool.inst.GUIGameObject.SetActive(true);
		NewbieGuidePanel.CallLuaByStart();
		LossArmyMessage._inst = null;
	}

	private void Awake()
	{
		LossArmyMessage._inst = this;
	}

	public void SetInfo()
	{
		int num = 0;
		foreach (KeyValuePair<int, int> current in this.LossArmyList)
		{
			num++;
			GameObject gameObject = UnityEngine.Object.Instantiate(this.LossArmyPrefab) as GameObject;
			if (num <= 6)
			{
				gameObject.transform.parent = this.Grid1.transform;
			}
			else
			{
				gameObject.transform.parent = this.Grid2.transform;
			}
			gameObject.transform.localScale = Vector3.one;
			if (current.Key == 1001)
			{
				gameObject.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "baolisi";
			}
			else if (current.Key == 1002)
			{
				gameObject.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "tanya";
			}
			else
			{
				AtlasManage.SetArmyIconSpritName(gameObject.transform.FindChild("Icon").GetComponent<UISprite>(), current.Key);
			}
			gameObject.transform.Find("Label").GetComponent<UILabel>().text = "-" + current.Value;
		}
		this.Grid1.Reposition();
		this.Grid2.Reposition();
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_LossArmyMessageClose, new EventManager.VoidDelegate(this.MainPanel_LossArmyMessageClose));
		this.ThisGa.transform.localScale = Vector3.zero;
		TweenScale.Begin(this.ThisGa, 0.2f, Vector3.one);
	}

	private void MainPanel_LossArmyMessageClose(GameObject ga)
	{
		UnityEngine.Object.Destroy(base.gameObject);
		MainUIPanelManage._instance.CheckPVPNewMessage();
	}

	private void Update()
	{
	}
}
