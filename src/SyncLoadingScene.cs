using System;
using System.Collections;
using System.Diagnostics;

public class SyncLoadingScene
{
	public static void LoadingSceneAsny(string scene)
	{
		CoroutineInstance.DoJob(SyncLoadingScene.StartLoadingSceneAsny(scene));
	}

	[DebuggerHidden]
	private static IEnumerator StartLoadingSceneAsny(string scene)
	{
		SyncLoadingScene.<StartLoadingSceneAsny>c__Iterator9A <StartLoadingSceneAsny>c__Iterator9A = new SyncLoadingScene.<StartLoadingSceneAsny>c__Iterator9A();
		<StartLoadingSceneAsny>c__Iterator9A.scene = scene;
		<StartLoadingSceneAsny>c__Iterator9A.<$>scene = scene;
		return <StartLoadingSceneAsny>c__Iterator9A;
	}
}
