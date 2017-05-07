using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class MeshCombineTest : MonoBehaviour
{
	private void Start()
	{
		MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		Material[] array = new Material[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			array[i] = componentsInChildren[i].sharedMaterial;
		}
		MeshFilter[] componentsInChildren2 = base.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] array2 = new CombineInstance[componentsInChildren2.Length];
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			array2[j].mesh = componentsInChildren2[j].sharedMesh;
			array2[j].transform = componentsInChildren2[j].transform.localToWorldMatrix;
			componentsInChildren2[j].gameObject.SetActive(false);
		}
		base.transform.GetComponent<MeshFilter>().mesh = new Mesh();
		base.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(array2, false);
		base.transform.gameObject.SetActive(true);
		base.transform.GetComponent<MeshRenderer>().sharedMaterials = array;
	}
}
