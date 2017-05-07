using System;
using UnityEngine;

public class NetMessagePanel : FuncUIPanel
{
	[SerializeField]
	private GameObject backgournd;

	[SerializeField]
	private UILabel tittle;

	[SerializeField]
	private GameObject btnPrefab;

	[SerializeField]
	private UIGrid btnGrid;

	[SerializeField]
	private UILabel biaoTi;

	public static NetMessagePanel inst;

	private Action CallBack1;

	private Action CallBack2;

	private Action CallBack3;

	public void OnDestroy()
	{
		NetMessagePanel.inst = null;
	}

	public override void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		NetMessagePanel.inst = this;
	}

	public override void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn1Click, new EventManager.VoidDelegate(this.Btn1Click));
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn2Click, new EventManager.VoidDelegate(this.Btn2Click));
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn3Click, new EventManager.VoidDelegate(this.Btn3Click));
		Debug.Log("消除特写摄像机");
		if (Camera_FingerManager.inst && Camera_FingerManager.inst.dragCamera != null)
		{
			UnityEngine.Object.Destroy(Camera_FingerManager.inst.dragCamera.gameObject);
		}
		base.OnEnable();
	}

	private void Btn1Click(GameObject ga)
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (this.CallBack1 != null)
		{
			this.CallBack1();
		}
	}

	private void Btn2Click(GameObject ga)
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (this.CallBack2 != null)
		{
			this.CallBack2();
		}
	}

	private void Btn3Click(GameObject ga)
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (this.CallBack3 != null)
		{
			this.CallBack3();
		}
	}

	public void ShowBtn(string Name, string _tittle, string btnName, Action callBack)
	{
		this.tittle.text = _tittle;
		this.btnGrid.ClearChild();
		GameObject gameObject = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject.GetComponentInChildren<UILabel>().text = btnName;
		gameObject.transform.localPosition = Vector3.zero;
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn1Click] = gameObject;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).eventType = EventManager.EventType.MessageBoxClose_btn1Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).isSendLua = false;
		this.biaoTi.text = Name;
		gameObject.name = "btn1";
		this.CallBack1 = callBack;
	}

	public void ShowBtn(string Name, string _tittle, string btnName1, Action callBack1, string btnName2, Action callBack2)
	{
		this.CallBack1 = callBack1;
		this.CallBack2 = callBack2;
		this.tittle.text = _tittle;
		this.btnGrid.ClearChild();
		GameObject gameObject = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject.GetComponentInChildren<UILabel>().text = btnName1;
		this.biaoTi.text = Name;
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn1Click] = gameObject;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).eventType = EventManager.EventType.MessageBoxClose_btn1Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).isSendLua = false;
		gameObject.name = "btn1";
		GameObject gameObject2 = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject2.GetComponentInChildren<UILabel>().text = btnName2;
		gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn2Click] = gameObject2;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).eventType = EventManager.EventType.MessageBoxClose_btn2Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).isSendLua = false;
		gameObject2.name = "btn2";
		this.btnGrid.Reposition();
	}

	public void ShowBtn(string _tittle, string btnName1, Action callBack1, string btnName2, Action callBack2, string btnName3, Action callBack3)
	{
		this.btnGrid.ClearChild();
		this.CallBack1 = callBack1;
		this.CallBack2 = callBack2;
		this.CallBack3 = callBack3;
		this.tittle.text = _tittle;
		GameObject gameObject = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject.GetComponentInChildren<UILabel>().text = btnName1;
		gameObject.name = "btn1";
		gameObject.transform.localPosition = new Vector3(this.btnGrid.cellWidth * -1f, 0f, 0f);
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn1Click] = gameObject;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).eventType = EventManager.EventType.MessageBoxClose_btn1Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject).isSendLua = false;
		GameObject gameObject2 = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject2.GetComponentInChildren<UILabel>().text = btnName2;
		gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn2Click] = gameObject2;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).eventType = EventManager.EventType.MessageBoxClose_btn2Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject2).isSendLua = false;
		gameObject2.name = "btn2";
		GameObject gameObject3 = NGUITools.AddChild(this.btnGrid.gameObject, this.btnPrefab);
		gameObject3.GetComponentInChildren<UILabel>().text = btnName3;
		gameObject2.transform.localPosition = new Vector3(this.btnGrid.cellWidth, 0f, 0f);
		ButtonClick.AllButtonClick[EventManager.EventType.MessageBoxClose_btn3Click] = gameObject3;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject3).eventType = EventManager.EventType.MessageBoxClose_btn3Click;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject3).newbiBtn = true;
		GameTools.GetCompentIfNoAddOne<ButtonClick>(gameObject3).isSendLua = false;
		gameObject3.name = "btn3";
	}
}
