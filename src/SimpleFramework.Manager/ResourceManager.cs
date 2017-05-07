using System;
using System.IO;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class ResourceManager : View
	{
		private AssetBundle shared;

		public void initialize(Action func)
		{
			if (func != null)
			{
				func();
			}
		}

		public AssetBundle LoadBundle(string name)
		{
			string path = Util.DataPath + name.ToLower() + ".assetbundle";
			byte[] binary = File.ReadAllBytes(path);
			return AssetBundle.CreateFromMemoryImmediate(binary);
		}

		private void OnDestroy()
		{
			if (this.shared != null)
			{
				this.shared.Unload(true);
			}
			LogManage.Log("~ResourceManager was destroy!");
		}
	}
}
