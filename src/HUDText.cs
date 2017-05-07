using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/HUD Text"), ExecuteInEditMode]
public class HUDText : MonoBehaviour
{
	protected class Entry
	{
		public float time;

		public float stay;

		public float offset;

		public float val;

		public UILabel label;

		public float movementStart
		{
			get
			{
				return this.time + this.stay;
			}
		}
	}

	[HideInInspector, SerializeField]
	private UIFont font;

	public UIFont bitmapFont;

	public Font trueTypeFont;

	public int fontSize = 16;

	public FontStyle fontStyle;

	public bool applyGradient;

	public Color gradientTop = Color.white;

	public Color gradienBottom = new Color(0.7f, 0.7f, 0.7f);

	public UILabel.Effect effect;

	public Color effectColor = Color.black;

	public AnimationCurve offsetCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(3f, 40f)
	});

	public AnimationCurve alphaCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(1f, 1f),
		new Keyframe(3f, 0f)
	});

	public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(0.25f, 1f)
	});

	private List<HUDText.Entry> mList = new List<HUDText.Entry>();

	private List<HUDText.Entry> mUnused = new List<HUDText.Entry>();

	private int counter;

	private HUDText.Entry ne;

	private bool mUseDynamicFont;

	private bool movie;

	private Vector3 pos0;

	public Transform Target;

	public bool isVisible
	{
		get
		{
			return this.mList.Count != 0;
		}
	}

	public UnityEngine.Object ambigiousFont
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.trueTypeFont;
			}
			if (this.bitmapFont != null)
			{
				return this.bitmapFont;
			}
			return this.font;
		}
		set
		{
			if (value is Font)
			{
				this.trueTypeFont = (value as Font);
				this.bitmapFont = null;
				this.font = null;
			}
			else if (value is UIFont)
			{
				this.bitmapFont = (value as UIFont);
				this.trueTypeFont = null;
				this.font = null;
			}
		}
	}

	private static int Comparison(HUDText.Entry a, HUDText.Entry b)
	{
		if (a.movementStart < b.movementStart)
		{
			return -1;
		}
		if (a.movementStart > b.movementStart)
		{
			return 1;
		}
		return 0;
	}

	private HUDText.Entry Create()
	{
		if (this.mUnused.Count > 0)
		{
			HUDText.Entry entry = this.mUnused[this.mUnused.Count - 1];
			this.mUnused.RemoveAt(this.mUnused.Count - 1);
			entry.time = Time.realtimeSinceStartup;
			entry.label.depth = NGUITools.CalculateNextDepth(base.gameObject);
			NGUITools.SetActive(entry.label.gameObject, true);
			entry.offset = 0f;
			this.mList.Add(entry);
			return entry;
		}
		HUDText.Entry entry2 = new HUDText.Entry();
		entry2.time = Time.realtimeSinceStartup;
		entry2.label = NGUITools.AddWidget<UILabel>(base.gameObject);
		entry2.label.name = this.counter.ToString();
		entry2.label.ambigiousFont = this.ambigiousFont;
		entry2.label.fontSize = this.fontSize;
		entry2.label.fontStyle = this.fontStyle;
		entry2.label.applyGradient = this.applyGradient;
		entry2.label.gradientTop = this.gradientTop;
		entry2.label.gradientBottom = this.gradienBottom;
		entry2.label.effectStyle = this.effect;
		entry2.label.effectColor = this.effectColor;
		entry2.label.overflowMethod = UILabel.Overflow.ResizeFreely;
		entry2.label.cachedTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		this.mList.Add(entry2);
		this.counter++;
		return entry2;
	}

	private void Delete(HUDText.Entry ent)
	{
		this.mList.Remove(ent);
		this.mUnused.Add(ent);
		NGUITools.SetActive(ent.label.gameObject, false);
	}

	public void Add(object obj, Color c, float stayDuration)
	{
		if (!base.enabled)
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		bool flag = false;
		float num = 0f;
		if (obj is float)
		{
			flag = true;
			num = (float)obj;
		}
		else if (obj is int)
		{
			flag = true;
			num = (float)((int)obj);
		}
		if (flag)
		{
			if (num == 0f)
			{
				return;
			}
			int i = this.mList.Count;
			while (i > 0)
			{
				HUDText.Entry entry = this.mList[--i];
				if (entry.time + 1f >= realtimeSinceStartup)
				{
					if (entry.val != 0f)
					{
						if (entry.val < 0f && num < 0f)
						{
							entry.val += num;
							entry.label.text = Mathf.RoundToInt(entry.val).ToString();
							return;
						}
						if (entry.val > 0f && num > 0f)
						{
							entry.val += num;
							entry.label.text = "+" + Mathf.RoundToInt(entry.val);
							return;
						}
					}
				}
			}
		}
		this.ne = this.Create();
		this.ne.stay = stayDuration;
		this.ne.label.color = c;
		this.ne.val = num;
		if (flag)
		{
			this.ne.label.text = ((num >= 0f) ? ("+" + Mathf.RoundToInt(this.ne.val)) : Mathf.RoundToInt(this.ne.val).ToString());
		}
		else
		{
			this.ne.label.text = obj.ToString();
		}
		this.mList.Sort(new Comparison<HUDText.Entry>(HUDText.Comparison));
	}

	private void OnEnable()
	{
		if (this.font != null)
		{
			if (this.font.isDynamic)
			{
				this.trueTypeFont = this.font.dynamicFont;
				this.fontStyle = this.font.dynamicFontStyle;
				this.mUseDynamicFont = true;
			}
			else if (this.bitmapFont == null)
			{
				this.bitmapFont = this.font;
				this.mUseDynamicFont = false;
			}
			this.font = null;
		}
	}

	private void OnValidate()
	{
		Font x = this.trueTypeFont;
		UIFont uIFont = this.bitmapFont;
		this.bitmapFont = null;
		this.trueTypeFont = null;
		if (x != null && (uIFont == null || !this.mUseDynamicFont))
		{
			this.bitmapFont = null;
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
		else if (uIFont != null)
		{
			if (uIFont.isDynamic)
			{
				this.trueTypeFont = uIFont.dynamicFont;
				this.fontStyle = uIFont.dynamicFontStyle;
				this.fontSize = uIFont.defaultSize;
				this.mUseDynamicFont = true;
			}
			else
			{
				this.bitmapFont = uIFont;
				this.mUseDynamicFont = false;
			}
		}
		else
		{
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
	}

	private void OnDisable()
	{
		int i = this.mList.Count;
		while (i > 0)
		{
			HUDText.Entry entry = this.mList[--i];
			if (entry.label != null)
			{
				entry.label.enabled = false;
			}
			else
			{
				this.mList.RemoveAt(i);
			}
		}
	}

	public void CreateHurt()
	{
		this.movie = true;
		float num = 2f;
		base.transform.localScale = new Vector3(num, num, num);
		this.ne.label.color = new Color(this.ne.label.color.r, this.ne.label.color.g, this.ne.label.color.b, 0f);
		this.pos0 = base.transform.position;
	}

	private void Update()
	{
		float time = RealTime.time;
		Keyframe[] keys = this.offsetCurve.keys;
		Keyframe[] keys2 = this.alphaCurve.keys;
		Keyframe[] keys3 = this.scaleCurve.keys;
		float time2 = keys[keys.Length - 1].time;
		float time3 = keys2[keys2.Length - 1].time;
		float time4 = keys3[keys3.Length - 1].time;
		float num = Mathf.Max(time4, Mathf.Max(time2, time3));
		int i = this.mList.Count;
		while (i > 0)
		{
			HUDText.Entry entry = this.mList[--i];
			float num2 = time - entry.movementStart;
			entry.offset = this.offsetCurve.Evaluate(num2);
			entry.label.alpha = this.alphaCurve.Evaluate(num2);
			float num3 = this.scaleCurve.Evaluate(time - entry.time);
			if (num3 < 0.001f)
			{
				num3 = 0.001f;
			}
			entry.label.cachedTransform.localScale = new Vector3(num3, num3, num3);
			if (num2 > num)
			{
				this.Delete(entry);
			}
			else
			{
				entry.label.enabled = true;
			}
		}
		float num4 = 0f;
		int j = this.mList.Count;
		while (j > 0)
		{
			HUDText.Entry entry2 = this.mList[--j];
			num4 = Mathf.Max(num4, entry2.offset);
			entry2.label.cachedTransform.localPosition = new Vector3(0f, num4, 0f);
			num4 += Mathf.Round(entry2.label.cachedTransform.localScale.y * (float)entry2.label.fontSize);
		}
		if (this.Target != null)
		{
			Camera camera = NGUITools.FindCameraForLayer(this.Target.gameObject.layer);
			if (camera)
			{
				Vector3 position = camera.WorldToScreenPoint(this.Target.position);
				position.z = 0f;
				if (FuncUIManager.inst)
				{
					Camera camera2 = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer);
					if (camera2)
					{
						base.transform.position = camera2.ScreenToWorldPoint(position);
					}
					else
					{
						UnityEngine.Object.Destroy(base.gameObject);
					}
				}
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (this.movie)
		{
			float num5 = 0.7f;
			if (base.transform.localScale.x >= num5)
			{
				base.transform.localScale = new Vector3(Mathf.Max(num5, base.transform.localScale.x - 2f * Time.deltaTime), base.transform.localScale.x, base.transform.localScale.x);
				this.ne.label.alpha = 1f - (base.transform.localScale.x - num5) / (1f - num5);
				this.ne.label.effectColor = new Color(this.ne.label.effectColor.r, this.ne.label.effectColor.g, this.ne.label.effectColor.b, 1f - (base.transform.localScale.x - num5) / (1f - num5));
			}
		}
	}
}
