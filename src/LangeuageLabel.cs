using System;
using UnityEngine;

[RequireComponent(typeof(languageLableKey))]
public class LangeuageLabel : UILabel
{
	private languageLableKey key;

	public languageLableKey Key
	{
		get
		{
			if (this.key == null)
			{
				this.key = base.GetComponent<languageLableKey>();
			}
			return this.key;
		}
		set
		{
			this.key = value;
		}
	}

	public override string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
					base.shouldBeProcessed = true;
					base.ProcessAndRequest();
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				base.shouldBeProcessed = true;
				base.ProcessAndRequest();
			}
			if (this.autoResizeBoxCollider)
			{
				base.ResizeCollider();
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		LanguageManage.LangeChange += new LanguageManage.language(this.LanguageManage_LangeChange);
		if (Application.isPlaying)
		{
			this.ChangeValue();
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public void SetUILableKey(string keyValue)
	{
		this.Key.key = keyValue;
		this.ChangeValue();
	}

	private void LanguageManage_LangeChange()
	{
		this.ChangeValue();
	}

	private void ChangeValue()
	{
		this.text = string.Format("{0}{1}{2}", this.Key.textPre, LanguageManage.GetTextByKey(this.Key.key.Trim(), this.Key.pickName.Trim()), this.Key.textLast);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		LanguageManage.LangeChange -= new LanguageManage.language(this.LanguageManage_LangeChange);
	}
}
