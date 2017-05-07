using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class MeshCombine : MonoBehaviour
{
	private void Start()
	{
		MeshFilter[] componentsInChildren = base.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] array = new CombineInstance[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			array[i].mesh = componentsInChildren[i].sharedMesh;
			array[i].transform = componentsInChildren[i].transform.localToWorldMatrix;
			componentsInChildren[i].renderer.enabled = false;
		}
		base.transform.GetComponent<MeshFilter>().mesh = new Mesh();
		base.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(array);
		base.transform.renderer.enabled = true;
		base.transform.gameObject.SetActive(true);
	}
}
