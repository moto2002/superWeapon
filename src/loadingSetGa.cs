using System;
using UnityEngine;

public class loadingSetGa : MonoBehaviour
{
	public UITexture BG;

	public GameObject ga;

	public void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetLoadingGa()
	{
		this.BG.alpha = 255f;
	}
}
