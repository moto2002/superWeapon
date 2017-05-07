using LuaInterface;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class PanelManager : View
	{
		private Transform parent;

		private Transform Parent
		{
			get
			{
				return this.parent;
			}
		}

		public void CreatePanel(string name, LuaFunction func = null)
		{
			AssetBundle assetBundle = base.ResManager.LoadBundle(name);
			base.StartCoroutine(this.StartCreatePanel(name, assetBundle, func));
			LogManage.LogWarning(string.Concat(new object[]
			{
				"CreatePanel::>> ",
				name,
				" ",
				assetBundle
			}));
		}

		[DebuggerHidden]
		private IEnumerator StartCreatePanel(string name, AssetBundle bundle, LuaFunction func = null)
		{
			PanelManager.<StartCreatePanel>c__Iterator9D <StartCreatePanel>c__Iterator9D = new PanelManager.<StartCreatePanel>c__Iterator9D();
			<StartCreatePanel>c__Iterator9D.name = name;
			<StartCreatePanel>c__Iterator9D.bundle = bundle;
			<StartCreatePanel>c__Iterator9D.func = func;
			<StartCreatePanel>c__Iterator9D.<$>name = name;
			<StartCreatePanel>c__Iterator9D.<$>bundle = bundle;
			<StartCreatePanel>c__Iterator9D.<$>func = func;
			<StartCreatePanel>c__Iterator9D.<>f__this = this;
			return <StartCreatePanel>c__Iterator9D;
		}
	}
}
