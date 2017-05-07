using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class AideList : MonoBehaviour
{
	public UITable aideDataUITable;

	public GameObject aideServer;

	public GameObject closeThis;

	public GameObject tips;

	public static AideList _ins;

	public void OnDestroy()
	{
		AideList._ins = null;
	}

	private void Start()
	{
	}

	private void Awake()
	{
		AideList._ins = this;
		this.aideDataUITable = base.transform.FindChild("Scroll View/Table").GetComponent<UITable>();
		this.tips = base.transform.FindChild("tip").gameObject;
		this.aideServer = base.transform.FindChild("aideData_Server").gameObject;
		this.closeThis = base.transform.FindChild("close").gameObject;
		this.closeThis.AddComponent<ButtonClick>();
		ButtonClick component = this.closeThis.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.AdjutantPanle_Close;
	}

	private void OnEnable()
	{
		this.aideDataUITable.Reposition();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10028)
		{
			this.ShowAideData_Server();
		}
		if (opcodeCMD == 10041)
		{
			this.ShowAideData_Server();
		}
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	public void ShowAideData_Server()
	{
		this.aideDataUITable.DestoryChildren(true);
		base.StartCoroutine(this.showAideListC());
		this.aideDataUITable.Reposition();
		if (this.aideDataUITable.children.Count == 0)
		{
			this.tips.gameObject.SetActive(true);
		}
		else
		{
			this.tips.gameObject.SetActive(false);
		}
	}

	[DebuggerHidden]
	private IEnumerator showAideListC()
	{
		AideList.<showAideListC>c__Iterator66 <showAideListC>c__Iterator = new AideList.<showAideListC>c__Iterator66();
		<showAideListC>c__Iterator.<>f__this = this;
		return <showAideListC>c__Iterator;
	}
}
