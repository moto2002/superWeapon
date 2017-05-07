using System;
using UnityEngine;

public class Dicertion_TowerAuto : MonoBehaviour
{
	public GameObject target;

	public GameObject[] dics;

	public void OnEnable()
	{
		this.dics[0].transform.parent.position = this.target.transform.position + new Vector3(this.target.renderer.bounds.size.z / 4f, 0.5f, 0f);
		this.dics[1].transform.parent.position = this.target.transform.position + new Vector3(0f, 0.5f, this.target.renderer.bounds.size.z / 4f);
		this.dics[2].transform.parent.position = this.target.transform.position + new Vector3(this.target.renderer.bounds.size.z / 4f * -1f, 0.5f, 0f);
		this.dics[3].transform.parent.position = this.target.transform.position + new Vector3(0f, 0.5f, this.target.renderer.bounds.size.z / 4f * -1f);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
