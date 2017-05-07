using System;
using UnityEngine;

public class FightPanel_SkillAndSoliderUIItem : IMonoBehaviour
{
	public enum FightPanel_UIItemType
	{
		none,
		skill,
		solider
	}

	public FightPanel_SkillAndSoliderUIItem.FightPanel_UIItemType buttonType;

	public SoliderButtonState btnState;

	private void Start()
	{
	}

	protected virtual void OnDrag(Vector2 vc)
	{
	}

	public virtual void SetInfo()
	{
	}

	public virtual void OnClickEvent(bool isPress)
	{
	}

	public virtual void ResetSelect(bool isSelect)
	{
	}

	public virtual void DoPress()
	{
	}
}
