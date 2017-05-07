using System;
using UnityEngine;

public class UIGridReverse : UIGrid
{
	public override void Reposition()
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
		BetterList<Transform> childList = base.GetChildList();
		this.ResetPositionReverse(childList);
		if (this.keepWithinPanel)
		{
			base.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	protected void ResetPositionReverse(BetterList<Transform> list)
	{
		this.mReposition = false;
		int num = this.maxPerLine;
		int num2 = (this.maxPerLine <= 0) ? 0 : (list.size / this.maxPerLine);
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int i = 0;
		int size = list.size;
		while (i < size)
		{
			Transform transform2 = list[i];
			float z = transform2.localPosition.z;
			Vector3 vector = (this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z);
			if (this.animateSmoothly && Application.isPlaying)
			{
				SpringPosition.Begin(transform2.gameObject, vector, 15f).updateScrollView = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (--num <= 0 && this.maxPerLine > 0)
			{
				num = num3;
				num2--;
			}
			i++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child = transform.GetChild(j);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					SpringPosition expr_1EE_cp_0 = component;
					expr_1EE_cp_0.target.x = expr_1EE_cp_0.target.x - num5;
					SpringPosition expr_203_cp_0 = component;
					expr_203_cp_0.target.y = expr_203_cp_0.target.y - num6;
				}
				else
				{
					Vector3 localPosition = child.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					child.localPosition = localPosition;
				}
			}
		}
	}
}
