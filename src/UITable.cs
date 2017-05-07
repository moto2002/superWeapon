using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	public enum Direction
	{
		Down,
		Up
	}

	public enum Sorting
	{
		None,
		Alphabetic,
		Horizontal,
		Vertical,
		Custom
	}

	public delegate void OnReposition();

	public int columns;

	public UITable.Direction direction;

	public UITable.Sorting sorting;

	public bool hideInactive = true;

	public bool keepWithinPanel;

	public Vector2 padding = Vector2.zero;

	public UITable.OnReposition onReposition;

	public GameObject ItemModel;

	protected UIPanel mPanel;

	protected bool mInitDone;

	protected bool mReposition;

	protected List<Transform> mChildren = new List<Transform>();

	[HideInInspector, SerializeField]
	private bool sorted;

	public bool isResInStart = true;

	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	public List<Transform> children
	{
		get
		{
			if (this.mChildren.Count == 0)
			{
				Transform transform = base.transform;
				this.mChildren.Clear();
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (child && child.gameObject && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
					{
						this.mChildren.Add(child);
					}
				}
				if (this.sorting != UITable.Sorting.None || this.sorted)
				{
					if (this.sorting == UITable.Sorting.Alphabetic)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
					}
					else if (this.sorting == UITable.Sorting.Horizontal)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
					}
					else if (this.sorting == UITable.Sorting.Vertical)
					{
						this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
					}
					else
					{
						this.Sort(this.mChildren);
					}
				}
			}
			return this.mChildren;
		}
	}

	public List<Transform> GetChildren(bool isHideChildren)
	{
		if (!isHideChildren)
		{
			return this.children;
		}
		this.mChildren.Clear();
		if (base.transform != null)
		{
			Transform transform = base.transform;
			this.mChildren.Clear();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child && child.gameObject)
				{
					this.mChildren.Add(child);
				}
			}
			if (this.sorting != UITable.Sorting.None || this.sorted)
			{
				if (this.sorting == UITable.Sorting.Alphabetic)
				{
					this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortByName));
				}
				else if (this.sorting == UITable.Sorting.Horizontal)
				{
					this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
				}
				else if (this.sorting == UITable.Sorting.Vertical)
				{
					this.mChildren.Sort(new Comparison<Transform>(UIGrid.SortVertical));
				}
				else
				{
					this.Sort(this.mChildren);
				}
			}
			return this.mChildren;
		}
		return this.children;
	}

	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(new Comparison<Transform>(UIGrid.SortByName));
	}

	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + (bounds2.extents.y - bounds2.center.y);
				localPosition.y -= (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.mReposition = true;
			return;
		}
		if (!this.mInitDone)
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		this.mChildren.Clear();
		List<Transform> children = this.children;
		if (children.Count > 0)
		{
			this.RepositionVariableSize(children);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	[DebuggerHidden]
	public IEnumerator RespositionInNexFrame()
	{
		UITable.<RespositionInNexFrame>c__Iterator15 <RespositionInNexFrame>c__Iterator = new UITable.<RespositionInNexFrame>c__Iterator15();
		<RespositionInNexFrame>c__Iterator.<>f__this = this;
		return <RespositionInNexFrame>c__Iterator;
	}

	protected virtual void Start()
	{
		this.Init();
		if (this.isResInStart)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	public void DestoryChildren(bool all)
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			if (this.children[i])
			{
				this.children[i].transform.parent = null;
				UnityEngine.Object.Destroy(this.children[i].gameObject);
			}
		}
		this.children.Clear();
		if (all)
		{
			int j = 0;
			while (j < base.transform.childCount)
			{
				Transform child = base.transform.GetChild(j);
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
	}

	public void HideAllChildren()
	{
		List<Transform> children = this.GetChildren(true);
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i].gameObject.activeSelf)
			{
				NGUITools.SetActive(children[i].gameObject, false);
			}
		}
	}

	public GameObject CreateChildren(string gaName, bool isCoverOldGa = true)
	{
		if (this.ItemModel == null)
		{
			LogManage.LogError("Table's ItemModel is Null ");
			return null;
		}
		if (!isCoverOldGa)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.ItemModel, Vector3.zero, Quaternion.identity) as GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(true);
			gameObject.name = gaName;
			return gameObject;
		}
		Transform transform = this.GetChildren(true).FirstOrDefault((Transform a) => a.gameObject.name.Equals(gaName));
		if (transform != null)
		{
			if (!transform.gameObject.activeSelf)
			{
				transform.gameObject.SetActive(true);
			}
			return transform.gameObject;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(this.ItemModel, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject2.transform.parent = base.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localScale = Vector3.one;
		gameObject2.SetActive(true);
		gameObject2.name = gaName;
		return gameObject2;
	}
}
