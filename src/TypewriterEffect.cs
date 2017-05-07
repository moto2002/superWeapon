using System;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Typewriter Effect"), RequireComponent(typeof(UILabel))]
public class TypewriterEffect : MonoBehaviour
{
	public int charsPerSecond = 20;

	public float delayOnPeriod;

	public float delayOnNewLine;

	public UIScrollView scrollView;

	private UILabel mLabel;

	private string mText;

	private int mOffset;

	private float mNextChar;

	private bool mReset = true;

	private bool isFist = true;

	public Action EndCallBack;

	public bool isEnd;

	private void OnEnable()
	{
		this.mReset = true;
		this.isEnd = false;
	}

	private void Update()
	{
		if (this.mReset)
		{
			this.mOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mText = this.mLabel.processedText;
		}
		if (this.mOffset < this.mText.Length && this.mNextChar <= RealTime.time)
		{
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			float num = 1f / (float)this.charsPerSecond;
			char c = this.mText[this.mOffset];
			if (c == '.')
			{
				if (this.mOffset + 2 < this.mText.Length && this.mText[this.mOffset + 1] == '.' && this.mText[this.mOffset + 2] == '.')
				{
					num += this.delayOnPeriod * 3f;
					this.mOffset += 2;
				}
				else
				{
					num += this.delayOnPeriod;
				}
			}
			else if (c == '!' || c == '?')
			{
				num += this.delayOnPeriod;
			}
			else if (c == '\n')
			{
				num += this.delayOnNewLine;
			}
			NGUIText.ParseSymbol(this.mText, ref this.mOffset);
			this.mNextChar = RealTime.time + num;
			this.mLabel.text = this.mText.Substring(0, ++this.mOffset);
			if (this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
		}
		if (this.mOffset >= this.mText.Length && !this.isEnd)
		{
			this.isEnd = true;
			if (this.EndCallBack != null)
			{
				this.EndCallBack();
			}
		}
	}

	public void ToEnd()
	{
		this.isEnd = true;
		this.mOffset = this.mText.Length;
		this.mLabel.text = this.mText;
	}
}
