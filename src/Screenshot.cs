using System;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
	private Vector2 oldPosition02;

	private bool sign;

	private bool shoted;

	private void OnMouseDown()
	{
		this.sign = false;
		if (Input.touchCount > 2)
		{
			this.oldPosition02 = Input.GetTouch(1).position;
		}
		this.shoted = false;
	}

	private void OnMouseDrag()
	{
		if (Input.touchCount > 2 && !this.shoted)
		{
			Vector2 position = Input.GetTouch(1).position;
			float num = Vector2.Distance(this.oldPosition02, position);
			Screenshot.CaptureCamera(Camera.main);
			this.shoted = true;
		}
	}

	public static Texture2D CaptureCamera(Camera camera)
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
		camera.targetTexture = renderTexture;
		camera.Render();
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		Rect source = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		texture2D.ReadPixels(source, 0, 0);
		texture2D.Apply();
		ShotBtn.inst.la.text = "1";
		camera.targetTexture = null;
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		byte[] bytes = texture2D.EncodeToPNG();
		string arg = ResManager.DataPathURL("/Pic/") + "Screenshot_.png";
		LogManage.Log("截屏了--------------------   :  " + ResManager.DataPathURL("/Pic/"));
		ShotBtn.inst.la.text = "2";
		LogManage.Log(string.Concat(new object[]
		{
			"截屏了--------------------   :  ",
			Application.dataPath,
			Time.time,
			".png"
		}));
		LogManage.Log(string.Format("截屏了一张照片: {0}", arg));
		ShotBtn.inst.la.text = "3";
		string persistentDataPath = Application.persistentDataPath;
		File.WriteAllBytes(string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/www",
			Time.time,
			"www.png"
		}), bytes);
		LogManage.Log(string.Concat(new object[]
		{
			"截屏了--------------------   :  ",
			Application.persistentDataPath,
			"/www",
			Time.time,
			"www.png"
		}));
		ShotBtn.inst.la.text = string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/www",
			Time.time,
			"www.png"
		});
		return texture2D;
	}
}
