using System;
using System.Text;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	public delegate void VoidDelegate(params UnityEngine.Object[] args);

	public UnityEngine.Object[] OnEndArgs;

	private TalkManager.VoidDelegate OnEnd;

	public UILabel contentLabel;

	public UILabel talkerName;

	public UISprite talkerIcon;

	private float waitTime = 5f;

	public float readSpeed = 0.02f;

	public static TalkManager ins;

	private bool startTalking;

	private TalkItem curItem;

	private float curTime;

	private float curWaitTime;

	private bool curEnd;

	private int curIndex;

	private StringBuilder sb;

	private StringBuilder SB
	{
		get
		{
			if (this.sb == null)
			{
				this.sb = new StringBuilder();
			}
			return this.sb;
		}
	}

	public void OnDestroy()
	{
		TalkManager.ins = null;
	}

	private void Awake()
	{
		TalkManager.ins = this;
	}

	private void Update()
	{
		if (this.startTalking)
		{
			if (this.curEnd)
			{
				this.curWaitTime += Time.deltaTime;
				if (this.curWaitTime >= this.waitTime)
				{
					this.curWaitTime = 0f;
					this.ReadNext();
				}
				else
				{
					this.contentLabel.text = this.GetContent();
				}
			}
			else
			{
				this.curTime += Time.deltaTime;
				if (this.curTime >= this.readSpeed)
				{
					this.curTime = 0f;
					this.contentLabel.text = this.GetContent();
				}
			}
		}
	}

	public void Talk(int id, TalkManager.VoidDelegate callBack, params UnityEngine.Object[] args)
	{
		TalkItem talkItemById = TalkManager.GetTalkItemById(id);
		if (talkItemById != null)
		{
			this.curItem = talkItemById;
			this.OnEnd = callBack;
			this.OnEndArgs = args;
			this.startTalking = true;
			this.curTime = 0f;
			this.curWaitTime = 0f;
			this.curEnd = false;
			this.curIndex = 0;
			this.talkerName.text = this.curItem.talkerName;
			this.talkerIcon.spriteName = this.curItem.talkerIcon;
		}
		else
		{
			this.OnEnd = callBack;
			this.OnEndArgs = args;
			this.startTalking = false;
			this.curTime = 0f;
			this.curWaitTime = 0f;
			this.curEnd = false;
			this.curIndex = 0;
			if (this.OnEnd != null)
			{
				this.OnEnd(this.OnEndArgs);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void ReadNext()
	{
		this.startTalking = false;
		this.SB.Length = 0;
		this.curIndex = 0;
		this.curTime = 0f;
		this.curWaitTime = 0f;
		this.curEnd = false;
		TalkItem talkItemById = TalkManager.GetTalkItemById(this.curItem.nextId);
		if (talkItemById != null)
		{
			this.curItem = talkItemById;
			this.talkerName.text = this.curItem.talkerName;
			this.talkerIcon.spriteName = this.curItem.talkerIcon;
			this.startTalking = true;
		}
		else
		{
			if (this.OnEnd != null)
			{
				this.OnEnd(this.OnEndArgs);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			if (this.curEnd)
			{
				this.ReadNext();
			}
			else
			{
				this.ForceEnd();
			}
		}
		SenceType senceType = Loading.senceType;
		if (senceType != SenceType.Island)
		{
			if (senceType == SenceType.WorldMap)
			{
				if (WMap_DragManager.inst)
				{
					WMap_DragManager.inst.btnInUse = isPressed;
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
				DragMgr.inst.BtnInUse = isPressed;
				DragMgr.inst.isInScrollViewDrag = isPressed;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}

	private void ForceEnd()
	{
		this.SB.Length = 0;
		this.sb.Append(this.curItem.content);
		this.curIndex = this.curItem.content.Length;
		this.curEnd = true;
	}

	private string GetContent()
	{
		if (this.curItem.content.Length > this.curIndex)
		{
			this.SB.Append(this.curItem.content[this.curIndex]);
			this.curIndex++;
		}
		else
		{
			this.curEnd = true;
		}
		return this.SB.ToString();
	}

	private static TalkItem GetTalkItemById(int id)
	{
		TalkItem result = null;
		if (UnitConst.GetInstance().TalkItemConst.ContainsKey(id))
		{
			result = UnitConst.GetInstance().TalkItemConst[id];
		}
		return result;
	}
}
