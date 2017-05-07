using System;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
	public GameObject ray;

	public UISpriteAnimation winAnimation;

	public GameObject[] starArray;

	private void Start()
	{
	}

	public void OnRayShow()
	{
		this.ray.gameObject.SetActive(true);
	}

	public void OnWinAnimationShow()
	{
		this.winAnimation.enabled = true;
	}

	public void OnStarRay1()
	{
		this.starArray[0].SetActive(true);
	}

	public void OnStarRay2()
	{
		this.starArray[1].SetActive(true);
	}

	public void OnStarRay3()
	{
		this.starArray[2].SetActive(true);
	}
}
