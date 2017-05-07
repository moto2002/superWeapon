using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_GIF : MonoBehaviour
{
	private UITexture ThisTexture;

	public string NPC_GIF_Folder_Name;

	public string NPC_GIF_Texture_Name;

	public int Frames;

	public bool PassFrames;

	private int texture_num;

	private float time;

	public string choose_texture_name;

	public Texture choose_texture;

	public List<Texture> GIF_Texture_List;

	public bool Readly;

	private void OnEnable()
	{
		this.ThisTexture = base.gameObject.GetComponent<UITexture>();
		this.ThisTexture.enabled = false;
		this.ThisTexture.enabled = true;
	}

	public void Init(string set_NPC_GIF_Folder_Name, string set_NPC_GIF_Texture_Name, int set_Frames, bool set_PassFrames)
	{
		this.GIF_Texture_List = new List<Texture>();
		this.ThisTexture = base.gameObject.GetComponent<UITexture>();
		this.ThisTexture.enabled = false;
		this.NPC_GIF_Folder_Name = set_NPC_GIF_Folder_Name;
		this.NPC_GIF_Texture_Name = set_NPC_GIF_Texture_Name;
		this.Frames = set_Frames;
		this.PassFrames = set_PassFrames;
		for (int i = 0; i < 1000; i++)
		{
			this.choose_texture_name = string.Concat(new object[]
			{
				"GuideDialogue/",
				this.NPC_GIF_Folder_Name,
				"/",
				this.NPC_GIF_Texture_Name,
				"_",
				i
			});
			this.choose_texture = (Resources.Load(this.choose_texture_name) as Texture);
			if (this.PassFrames)
			{
				i++;
			}
			if (!(this.choose_texture != null))
			{
				break;
			}
			this.GIF_Texture_List.Add(this.choose_texture);
		}
		this.ThisTexture.enabled = true;
		this.Readly = true;
	}

	private void Update()
	{
		if (!this.Readly)
		{
			return;
		}
		if (this.GIF_Texture_List.Count > 0)
		{
			this.time += Time.deltaTime;
			if (this.time >= 1f / (float)this.Frames)
			{
				this.time = 0f;
				this.texture_num++;
				if (this.texture_num >= this.GIF_Texture_List.Count)
				{
					this.texture_num = 0;
				}
				this.ThisTexture.mainTexture = this.GIF_Texture_List[this.texture_num];
			}
		}
	}
}
