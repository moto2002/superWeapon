using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SkillShowManage : MonoBehaviour
{
	public SkillCarteItem skill;

	public GameObject CardAni;

	public GameObject CardShow;

	public UITable zhanshiTable;

	public Transform[] kapais;

	public Transform[] leftCard;

	public Transform[] rightCard;

	private DieBall effectSkill;

	private DieBall EffectShowCard;

	private float firstTime = 0.3f;

	private float twiceTime = 0.2f;

	private float playTime = 0.1f;

	private bool play;

	private void Awake()
	{
		this.Initialize();
		this.zhanshiTable.isResInStart = false;
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_CloseSkillShow, new EventManager.VoidDelegate(this.CloseSkillShow));
	}

	private void Initialize()
	{
		this.skill.GetComponent<UISprite>().color = new Color(this.skill.GetComponent<UISprite>().color.r, this.skill.GetComponent<UISprite>().color.g, this.skill.GetComponent<UISprite>().color.b, 0f);
	}

	public void ShowSkill()
	{
		Transform[] array = this.kapais;
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform = array[i];
			transform.localScale = new Vector3(0f, 0.6f, 0.6f);
		}
		if (!this.EffectShowCard)
		{
			this.EffectShowCard = PoolManage.Ins.CreatEffect("jinengkachouqv", base.transform.position, Quaternion.identity, base.transform);
			this.EffectShowCard.tr.localPosition = Vector3.zero;
			this.EffectShowCard.tr.localScale = Vector3.one;
			Transform[] componentsInChildren = this.EffectShowCard.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				Transform transform2 = componentsInChildren[j];
				transform2.gameObject.layer = 8;
			}
		}
		this.CardAni.SetActive(true);
		this.CardShow.SetActive(false);
		TweenAlpha.Begin(this.zhanshiTable.gameObject, 0f, 1f);
		base.StartCoroutine(this.PlaySkillCardAni());
	}

	private void DoDisplayCard()
	{
		if (this.play)
		{
			return;
		}
		this.play = true;
		this.CardAni.SetActive(false);
		this.CardShow.SetActive(true);
		base.StartCoroutine(this.CreateEffect());
		AudioManage.inst.PlayAuido("extractskill", false);
		TweenAlpha.Begin(this.skill.ga, 0f, 1f);
		this.skill.ShowItem(HeroInfo.GetInstance().addSkill[0].itemId);
	}

	[DebuggerHidden]
	private IEnumerator PlaySkillCardAni()
	{
		SkillShowManage.<PlaySkillCardAni>c__Iterator93 <PlaySkillCardAni>c__Iterator = new SkillShowManage.<PlaySkillCardAni>c__Iterator93();
		<PlaySkillCardAni>c__Iterator.<>f__this = this;
		return <PlaySkillCardAni>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator CreateEffect()
	{
		SkillShowManage.<CreateEffect>c__Iterator94 <CreateEffect>c__Iterator = new SkillShowManage.<CreateEffect>c__Iterator94();
		<CreateEffect>c__Iterator.<>f__this = this;
		return <CreateEffect>c__Iterator;
	}

	public void CloseSkillShow(GameObject ga)
	{
		this.play = false;
		ga.GetComponent<ButtonClick>().isSendLua = false;
		base.StartCoroutine(this.HidePanel(0.5f));
	}

	[DebuggerHidden]
	private IEnumerator HidePanel(float time)
	{
		SkillShowManage.<HidePanel>c__Iterator95 <HidePanel>c__Iterator = new SkillShowManage.<HidePanel>c__Iterator95();
		<HidePanel>c__Iterator.time = time;
		<HidePanel>c__Iterator.<$>time = time;
		<HidePanel>c__Iterator.<>f__this = this;
		return <HidePanel>c__Iterator;
	}
}
