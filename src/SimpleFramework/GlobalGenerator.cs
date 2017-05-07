using System;
using UnityEngine;

namespace SimpleFramework
{
	public class GlobalGenerator : MonoBehaviour
	{
		private void Awake()
		{
			GameTools.DontDestroyOnLoad(base.gameObject);
		}

		private void Start()
		{
			this.InitGameMangager();
		}

		private void OnGUI()
		{
		}

		public void InitGameMangager()
		{
			string name = "GameManager";
			GameObject gameObject = GameObject.Find(name);
			if (gameObject == null)
			{
				gameObject = new GameObject(name);
				gameObject.name = name;
				AppFacade.Instance.StartUp();
			}
		}
	}
}
