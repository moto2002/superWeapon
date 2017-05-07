using System;
using System.Collections.Generic;
using UnityEngine;

public class AniMgr : MonoBehaviour
{
	[Serializable]
	public class AnimationSet
	{
		public AnimationClip animation;

		public float speedAnimation = 1f;
	}

	public static List<AniData> anis = new List<AniData>();

	private Transform tr;

	private Animation ani;

	public float actTime;

	public AniMgr.AnimationSet show;

	public AniMgr.AnimationSet idle;

	public AniMgr.AnimationSet combat;

	public AniMgr.AnimationSet walk;

	public AniMgr.AnimationSet run;

	public AniMgr.AnimationSet ride;

	public AniMgr.AnimationSet jumpup;

	public AniMgr.AnimationSet jump;

	public AniMgr.AnimationSet jumpdown;

	public AniMgr.AnimationSet attack1;

	public AniMgr.AnimationSet attack2;

	public AniMgr.AnimationSet attack3;

	public AniMgr.AnimationSet throwfast;

	public AniMgr.AnimationSet throwslow;

	public AniMgr.AnimationSet hitground;

	public AniMgr.AnimationSet xuanfengzhan;

	public AniMgr.AnimationSet rush;

	public AniMgr.AnimationSet injured;

	public AniMgr.AnimationSet falldown;

	public AniMgr.AnimationSet death;

	public AniMgr.AnimationSet blowFly;

	public AniMgr.AnimationSet standUp;

	public AniMgr.AnimationSet gather;

	public AniMgr.AnimationSet chant;

	public AniMgr.AnimationSet guide;

	public AniMgr.AnimationSet morph;

	public AniMgr.AnimationSet levelup;

	public AniMgr.AnimationSet cut;

	public AniMgr.AnimationSet chop;

	public AniMgr.AnimationSet piercing;

	public AniMgr.AnimationSet bellow;

	public AniMgr.AnimationSet push;

	public AniMgr.AnimationSet liao;

	public AniMgr.AnimationSet strike;

	public AniMgr.AnimationSet charge;

	public AniMgr.AnimationSet special;

	public AniMgr.AnimationSet yawn;

	public AniMgr.AnimationSet win;

	public AniMgr.AnimationSet empty;

	public AniMgr.AnimationSet stun;

	public AniMgr.AnimationSet pull;

	private AniMgr.AnimationSet act;

	private T_Tank cm;

	public bool changeAct;

	public Transform body;

	private float a = 0.5f;

	public float b;

	public float c;

	public float d = 1f;

	private float changeTime = 0.4f;

	private bool changeColor;

	private Material myBody;

	public Material myBody2;

	public string InitAnimation;

	private string bugId;

	private void Awake()
	{
		this.tr = base.transform;
		this.ani = this.tr.GetComponent<Animation>();
	}
}
