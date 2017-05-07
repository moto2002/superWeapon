using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	public enum InputType
	{
		Standard,
		AutoCorrect,
		Password
	}

	public enum Validation
	{
		None,
		Integer,
		Float,
		Alphanumeric,
		Username,
		Name
	}

	public enum KeyboardType
	{
		Default,
		ASCIICapable,
		NumbersAndPunctuation,
		URL,
		NumberPad,
		PhonePad,
		NamePhonePad,
		EmailAddress,
		HiddenInput
	}

	public enum OnReturnKey
	{
		Default,
		Submit,
		NewLine
	}

	public delegate char OnValidate(string text, int charIndex, char addedChar);

	public static UIInput current;

	public static UIInput selection;

	public UILabel label;

	public UIInput.InputType inputType;

	public UIInput.OnReturnKey onReturnKey;

	public UIInput.KeyboardType keyboardType;

	public UIInput.Validation validation;

	public int characterLimit;

	public string savedAs;

	public GameObject selectOnTab;

	public Color activeTextColor = Color.white;

	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	public List<EventDelegate> onChange = new List<EventDelegate>();

	public UIInput.OnValidate onValidate;

	[HideInInspector, SerializeField]
	protected string mValue;

	[NonSerialized]
	protected string mDefaultText = string.Empty;

	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	[NonSerialized]
	protected float mPosition;

	[NonSerialized]
	protected bool mDoInit = true;

	[NonSerialized]
	protected UIWidget.Pivot mPivot;

	[NonSerialized]
	protected bool mLoadSavedValue = true;

	protected static int mDrawStart;

	protected static string mLastIME = string.Empty;

	protected static TouchScreenKeyboard mKeyboard;

	[NonSerialized]
	protected int mSelectionStart;

	[NonSerialized]
	protected int mSelectionEnd;

	[NonSerialized]
	protected UITexture mHighlight;

	[NonSerialized]
	protected UITexture mCaret;

	[NonSerialized]
	protected Texture2D mBlankTex;

	[NonSerialized]
	protected float mNextBlink;

	[NonSerialized]
	protected float mLastAlpha;

	[NonSerialized]
	protected string mCached = string.Empty;

	[NonSerialized]
	protected int mSelectMe = -1;

	public string defaultText
	{
		get
		{
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			UIInput.mDrawStart = 0;
			if (Application.platform == RuntimePlatform.BB10Player)
			{
				value = value.Replace("\\b", "\b");
			}
			value = this.Validate(value);
			if (this.isSelected && UIInput.mKeyboard != null && this.mCached != value)
			{
				UIInput.mKeyboard.text = value;
				this.mCached = value;
			}
			if (this.mValue != value)
			{
				this.mValue = value;
				this.mLoadSavedValue = false;
				if (this.isSelected)
				{
					if (string.IsNullOrEmpty(value))
					{
						this.mSelectionStart = 0;
						this.mSelectionEnd = 0;
					}
					else
					{
						this.mSelectionStart = value.Length;
						this.mSelectionEnd = this.mSelectionStart;
					}
				}
				else
				{
					this.SaveToPlayerPrefs(value);
				}
				this.UpdateLabel();
				this.ExecuteOnChange();
			}
		}
	}

	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	public int cursorPosition
	{
		get
		{
			if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
			{
				return this.value.Length;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
				{
					return;
				}
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	public int selectionStart
	{
		get
		{
			if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
			{
				return 0;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
				{
					return;
				}
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	public int selectionEnd
	{
		get
		{
			if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
			{
				return this.value.Length;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !TouchScreenKeyboard.hideInput)
				{
					return;
				}
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		for (int i = 0; i < val.Length; i++)
		{
			char c = val[i];
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	private void Start()
	{
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
	}

	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.label.supportEncoding = false;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mPivot = this.label.pivot;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
			}
			else
			{
				PlayerPrefs.SetString(this.savedAs, val);
			}
		}
	}

	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			this.OnSelectEvent();
		}
		else
		{
			this.OnDeselectEvent();
		}
	}

	protected void OnSelectEvent()
	{
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.mKeyboard != null)
			{
				UIInput.mKeyboard.active = false;
				UIInput.mKeyboard = null;
			}
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			this.RestoreLabelPivot();
		}
		UIInput.selection = null;
		this.UpdateLabel();
	}

	private void Update()
	{
		if (this.isSelected)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
			{
				this.mSelectMe = -1;
				this.label.color = this.activeTextColor;
				if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.BB10Player)
				{
					TouchScreenKeyboardType touchScreenKeyboardType;
					string text;
					if (this.keyboardType == UIInput.KeyboardType.HiddenInput)
					{
						TouchScreenKeyboard.hideInput = true;
						touchScreenKeyboardType = TouchScreenKeyboardType.Default;
						text = "|";
					}
					else if (this.inputType == UIInput.InputType.Password)
					{
						TouchScreenKeyboard.hideInput = false;
						touchScreenKeyboardType = TouchScreenKeyboardType.Default;
						text = this.mValue;
					}
					else
					{
						TouchScreenKeyboard.hideInput = false;
						touchScreenKeyboardType = (TouchScreenKeyboardType)this.keyboardType;
						text = this.mValue;
					}
					UIInput.mKeyboard = ((this.inputType != UIInput.InputType.Password) ? TouchScreenKeyboard.Open(text, touchScreenKeyboardType, this.inputType == UIInput.InputType.AutoCorrect, this.label.multiLine, false, false, this.defaultText) : TouchScreenKeyboard.Open(text, touchScreenKeyboardType, false, false, true));
				}
				else
				{
					Vector2 compositionCursorPos = (!(UICamera.current != null) || !(UICamera.current.cachedCamera != null)) ? this.label.worldCorners[0] : UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]);
					compositionCursorPos.y = (float)Screen.height - compositionCursorPos.y;
					Input.imeCompositionMode = IMECompositionMode.On;
					Input.compositionCursorPos = compositionCursorPos;
					this.mSelectionStart = 0;
					this.mSelectionEnd = ((!string.IsNullOrEmpty(this.mValue)) ? this.mValue.Length : 0);
					UIInput.mDrawStart = 0;
				}
				this.UpdateLabel();
			}
			if (UIInput.mKeyboard != null)
			{
				string text2 = UIInput.mKeyboard.text;
				if (TouchScreenKeyboard.hideInput)
				{
					if (text2 != "|")
					{
						if (!string.IsNullOrEmpty(text2))
						{
							this.Insert(text2.Substring(1));
						}
						else
						{
							this.DoBackspace();
						}
						UIInput.mKeyboard.text = "|";
					}
				}
				else if (this.mCached != text2)
				{
					this.mCached = text2;
					this.value = text2;
				}
				if (UIInput.mKeyboard.done || !UIInput.mKeyboard.active)
				{
					if (!UIInput.mKeyboard.wasCanceled)
					{
						this.Submit();
					}
					UIInput.mKeyboard = null;
					this.isSelected = false;
					this.mCached = string.Empty;
				}
			}
			else
			{
				if (this.selectOnTab != null && Input.GetKeyDown(KeyCode.Tab))
				{
					UICamera.selectedObject = this.selectOnTab;
					return;
				}
				string compositionString = Input.compositionString;
				if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
				{
					string inputString = Input.inputString;
					for (int i = 0; i < inputString.Length; i++)
					{
						char c = inputString[i];
						if (c >= ' ')
						{
							if (c != '')
							{
								if (c != '')
								{
									if (c != '')
									{
										if (c != '')
										{
											this.Insert(c.ToString());
										}
									}
								}
							}
						}
					}
				}
				if (UIInput.mLastIME != compositionString)
				{
					this.mSelectionEnd = ((!string.IsNullOrEmpty(compositionString)) ? (this.mValue.Length + compositionString.Length) : this.mSelectionStart);
					UIInput.mLastIME = compositionString;
					this.UpdateLabel();
					this.ExecuteOnChange();
				}
			}
			if (this.mCaret != null && this.mNextBlink < RealTime.time)
			{
				this.mNextBlink = RealTime.time + 0.5f;
				this.mCaret.enabled = !this.mCaret.enabled;
			}
			if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
			{
				this.UpdateLabel();
			}
		}
	}

	private void OnGUI()
	{
		if (this.isSelected && Event.current.rawType == EventType.KeyDown)
		{
			this.ProcessEvent(Event.current);
		}
	}

	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert(string.Empty);
		}
	}

	protected virtual bool ProcessEvent(Event ev)
	{
		if (this.label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.OSXWebPlayer;
		bool flag2 = (!flag) ? ((ev.modifiers & EventModifiers.Control) != EventModifiers.None) : ((ev.modifiers & EventModifiers.Command) != EventModifiers.None);
		bool flag3 = (ev.modifiers & EventModifiers.Shift) != EventModifiers.None;
		KeyCode keyCode = ev.keyCode;
		switch (keyCode)
		{
		case KeyCode.KeypadEnter:
			goto IL_4B7;
		case KeyCode.KeypadEquals:
		case KeyCode.Insert:
			IL_A6:
			switch (keyCode)
			{
			case KeyCode.A:
				if (flag2)
				{
					ev.Use();
					this.mSelectionStart = 0;
					this.mSelectionEnd = this.mValue.Length;
					this.UpdateLabel();
				}
				return true;
			case KeyCode.B:
				IL_BC:
				switch (keyCode)
				{
				case KeyCode.V:
					if (flag2)
					{
						ev.Use();
						this.Insert(NGUITools.clipboard);
					}
					return true;
				case KeyCode.W:
					IL_D2:
					if (keyCode == KeyCode.Backspace)
					{
						ev.Use();
						this.DoBackspace();
						return true;
					}
					if (keyCode == KeyCode.Return)
					{
						goto IL_4B7;
					}
					if (keyCode != KeyCode.Delete)
					{
						return false;
					}
					ev.Use();
					if (!string.IsNullOrEmpty(this.mValue))
					{
						if (this.mSelectionStart == this.mSelectionEnd)
						{
							if (this.mSelectionStart >= this.mValue.Length)
							{
								return true;
							}
							this.mSelectionEnd++;
						}
						this.Insert(string.Empty);
					}
					return true;
				case KeyCode.X:
					if (flag2)
					{
						ev.Use();
						NGUITools.clipboard = this.GetSelection();
						this.Insert(string.Empty);
					}
					return true;
				}
				goto IL_D2;
			case KeyCode.C:
				if (flag2)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
				}
				return true;
			}
			goto IL_BC;
		case KeyCode.UpArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.UpArrow);
				if (this.mSelectionEnd != 0)
				{
					this.mSelectionEnd += UIInput.mDrawStart;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.DownArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.DownArrow);
				if (this.mSelectionEnd != this.label.processedText.Length)
				{
					this.mSelectionEnd += UIInput.mDrawStart;
				}
				else
				{
					this.mSelectionEnd = this.mValue.Length;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.RightArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = Mathf.Min(this.mSelectionEnd + 1, this.mValue.Length);
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.LeftArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = Mathf.Max(this.mSelectionEnd - 1, 0);
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.Home:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				if (this.label.multiLine)
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.Home);
				}
				else
				{
					this.mSelectionEnd = 0;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.End:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				if (this.label.multiLine)
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.End);
				}
				else
				{
					this.mSelectionEnd = this.mValue.Length;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.PageUp:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = 0;
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.PageDown:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.mValue.Length;
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		}
		goto IL_A6;
		IL_4B7:
		ev.Use();
		bool flag4 = this.onReturnKey == UIInput.OnReturnKey.NewLine || (this.onReturnKey == UIInput.OnReturnKey.Default && this.label.multiLine && !flag2 && this.label.overflowMethod != UILabel.Overflow.ClampContent && this.validation == UIInput.Validation.None);
		if (flag4)
		{
			this.Insert("\n");
		}
		else
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.currentKey = ev.keyCode;
			this.Submit();
			UICamera.currentKey = KeyCode.None;
		}
		return true;
	}

	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
			{
				break;
			}
			char c = text[i];
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	protected string GetLeftText()
	{
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		return (!string.IsNullOrEmpty(this.mValue) && num >= 0) ? this.mValue.Substring(0, num) : string.Empty;
	}

	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length) ? this.mValue.Substring(num) : string.Empty;
	}

	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return string.Empty;
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
		float distance;
		return (!plane.Raycast(currentRay, out distance)) ? 0 : (UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(distance)));
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				this.selectionStart = this.mSelectionEnd;
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

	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	private void OnDisable()
	{
		this.Cleanup();
	}

	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((!flag || isSelected) ? this.activeTextColor : this.mDefaultColor);
			string text;
			if (flag)
			{
				text = ((!isSelected) ? this.mDefaultText : string.Empty);
				this.RestoreLabelPivot();
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = string.Empty;
					string str = "*";
					if (this.label.bitmapFont != null && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = (!isSelected) ? 0 : Mathf.Min(text.Length, this.cursorPosition);
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.RestoreLabelPivot();
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.SetPivotToLeft();
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.SetPivotToLeft();
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.SetPivotToRight();
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.RestoreLabelPivot();
				}
			}
			this.label.text = text;
			if (isSelected && (UIInput.mKeyboard == null || TouchScreenKeyboard.hideInput))
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
			}
			else
			{
				this.Cleanup();
			}
		}
	}

	protected void SetPivotToLeft()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 0f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	protected void SetPivotToRight()
	{
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
		pivotOffset.x = 1f;
		this.label.pivot = NGUIMath.GetPivot(pivotOffset);
	}

	protected void RestoreLabelPivot()
	{
		if (this.label != null && this.label.pivot != this.mPivot)
		{
			this.label.pivot = this.mPivot;
		}
	}

	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)];
			char c2 = (text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
		if (this.characterLimit > 0)
		{
			while (GameTools.CheckStringlength(this.value) > this.characterLimit)
			{
				this.value = this.value.Remove(this.value.Length - 1, 1);
			}
		}
	}

	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs) && PlayerPrefs.HasKey(this.savedAs))
		{
			this.value = PlayerPrefs.GetString(this.savedAs);
		}
	}
}
