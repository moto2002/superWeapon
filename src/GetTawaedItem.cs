using System;

public class GetTawaedItem : IMonoBehaviour
{
	public UILabel count;

	public UILabel DayNumber;

	public UISprite isGetSign;

	public int state;

	public UISprite haveGet;

	public UISprite cantGgt;

	public UISprite QualitSprite;

	public UISprite bg;

	public UILabel itemName;

	public UISprite itemIcon;

	public static GetTawaedItem _ins;

	public DieBall effect;

	public DieBall effect1;

	public void OnDestroy()
	{
		GetTawaedItem._ins = null;
	}

	public override void Awake()
	{
		base.Awake();
		GetTawaedItem._ins = this;
	}
}
