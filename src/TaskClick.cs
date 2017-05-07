using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TaskClick : MonoBehaviour
{
	public Animation TaskAnimation;

	public GameObject res;

	public GameObject Item;

	public GameObject Label;

	public UITexture boxPicture;

	public UITable table;

	public UITable itemTable;

	public UILabel TaskDis;

	public static TaskClick _inst;

	private List<GameObject> allItem = new List<GameObject>();

	private bool isClose = true;

	private DailyTask Task;

	private bool isCanclick = true;

	private bool isCanDisplay;

	private void Awake()
	{
		TaskClick._inst = this;
		this.TaskDis = base.transform.FindChild("TaskDis").GetComponent<UILabel>();
	}

	private void SetBody()
	{
	}

	private void OnEnable()
	{
		UIManager.inst.UIInUsed(true);
		this.boxPicture.GetComponent<TweenPosition>().enabled = true;
		this.boxPicture.GetComponent<TweenPosition>().PlayForward();
		base.StartCoroutine(this.Show());
	}

	[DebuggerHidden]
	private IEnumerator ShowBox()
	{
		TaskClick.<ShowBox>c__Iterator8B <ShowBox>c__Iterator8B = new TaskClick.<ShowBox>c__Iterator8B();
		<ShowBox>c__Iterator8B.<>f__this = this;
		return <ShowBox>c__Iterator8B;
	}

	[DebuggerHidden]
	private IEnumerator Show()
	{
		TaskClick.<Show>c__Iterator8C <Show>c__Iterator8C = new TaskClick.<Show>c__Iterator8C();
		<Show>c__Iterator8C.<>f__this = this;
		return <Show>c__Iterator8C;
	}

	protected void OnPress(bool isPress)
	{
		SenceType senceType = Loading.senceType;
		if (senceType != SenceType.Island)
		{
			if (senceType == SenceType.WorldMap)
			{
				if (WMap_DragManager.inst)
				{
					WMap_DragManager.inst.btnInUse = isPress;
				}
				if (DragMgr.inst)
				{
					DragMgr.inst.BtnInUse = false;
					DragMgr.inst.isInScrollViewDrag = false;
				}
			}
		}
		else
		{
			if (DragMgr.inst)
			{
				DragMgr.inst.BtnInUse = isPress;
				DragMgr.inst.isInScrollViewDrag = isPress;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}

	public string OnSetQuatry(int leve)
	{
		switch (leve)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}

	private void OnDisable()
	{
		if (NewarmyInfo.inst)
		{
			NGUITools.SetActive(NewarmyInfo.inst.gameObject, true);
		}
	}

	private void Update()
	{
		if (this.isClose && NewbieGuidePanel._instance && NewbieGuidePanel._instance.gameObject.activeSelf)
		{
			NewbieGuidePanel._instance.HideSelf();
		}
	}

	public void OnClick()
	{
		if (this.isCanclick)
		{
			this.isCanclick = false;
			this.isCanDisplay = !this.isCanDisplay;
			if (this.isCanDisplay)
			{
				this.Task = UnitConst.GetInstance().CanRecieveXinShouMainlineTask;
				if (this.Task != null)
				{
					LogManage.Log("~~~~~~~~~~~~~~~~~~" + this.Task.id);
					TaskAndAchievementHandler.CG_CSCompleteTask(this.Task.id, new Action<bool>(this.RecieveTaskCallBack));
				}
			}
			else
			{
				this.Label.transform.localPosition = new Vector3(1200f, this.Label.transform.localPosition.y, this.Label.transform.localPosition.z);
				base.StartCoroutine(this.Show());
			}
		}
	}

	private void ShowRes()
	{
		this.Label.transform.DOLocalMoveX(100f, 0.3f, false).SetDelay(0.2f);
		for (int i = 0; i < this.allItem.Count; i++)
		{
			if (i == this.allItem.Count - 1)
			{
				Tweener t = this.allItem[i].transform.DOLocalMoveX(-945f, 0.2f, false).SetDelay((float)i * 0.2f + 0.2f);
				t.OnComplete(delegate
				{
					this.isCanclick = true;
				});
			}
			else
			{
				Tweener t = this.allItem[i].transform.DOLocalMoveX(-966f, 0.2f, false).SetDelay((float)i * 0.2f + 0.2f);
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator RecieveTask()
	{
		TaskClick.<RecieveTask>c__Iterator8D <RecieveTask>c__Iterator8D = new TaskClick.<RecieveTask>c__Iterator8D();
		<RecieveTask>c__Iterator8D.<>f__this = this;
		return <RecieveTask>c__Iterator8D;
	}

	private void RecieveTaskCallBack(bool isError)
	{
		base.StartCoroutine(this.RecieveTask());
	}

	public void SetUITexture(UITexture texture, string path, string name)
	{
		texture.mainTexture = (Resources.Load(path + name) as Texture);
	}
}
