using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class WorldMapSence : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.name = "WorldMap";
		base.StartCoroutine(this.AddWorldMap());
	}

	[DebuggerHidden]
	private IEnumerator AddWorldMap()
	{
		WorldMapSence.<AddWorldMap>c__Iterator23 <AddWorldMap>c__Iterator = new WorldMapSence.<AddWorldMap>c__Iterator23();
		<AddWorldMap>c__Iterator.<>f__this = this;
		return <AddWorldMap>c__Iterator;
	}
}
