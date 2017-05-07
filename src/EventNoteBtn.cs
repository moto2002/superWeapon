using System;
using UnityEngine;

public class EventNoteBtn : MonoBehaviour
{
	public EventNoteBtnType btnType = EventNoteBtnType.back;

	private void Start()
	{
	}

	private void OnClick()
	{
		EventNoteManager.inst.ButtonEvent(this.btnType);
	}
}
