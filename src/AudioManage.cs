using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
	public static AudioManage inst;

	public AudioSource audioPlay;

	public AudioSource audioBackgruond;

	public List<AudioSource> PlayAudioList = new List<AudioSource>();

	public List<AudioClip> audioList = new List<AudioClip>();

	private bool isOpenMusic = true;

	private bool isOpenPlayAudioBackGround = true;

	private TweenVolume tween;

	public string UnFindAudio;

	private int PlayAudioList_No;

	private bool isPlayAudio_3D = true;

	private List<AudioSource> Audio_3D = new List<AudioSource>();

	public bool IsOpenMusic
	{
		get
		{
			return this.isOpenMusic;
		}
		set
		{
			this.isOpenMusic = value;
		}
	}

	public bool IsOpenPlayAudioBackGround
	{
		get
		{
			return this.isOpenPlayAudioBackGround;
		}
		set
		{
			if (value)
			{
				if (!AudioManage.inst.audioBackgruond.isPlaying)
				{
					AudioManage.inst.audioBackgruond.Play();
				}
			}
			else if (AudioManage.inst.audioBackgruond.isPlaying)
			{
				AudioManage.inst.audioBackgruond.Stop();
			}
			this.isOpenPlayAudioBackGround = value;
		}
	}

	public bool IsPlayAudio_3D
	{
		get
		{
			return this.isPlayAudio_3D;
		}
		set
		{
			if (value)
			{
				this.Audio_3D.Clear();
			}
			else
			{
				for (int i = 0; i < this.Audio_3D.Count; i++)
				{
					if (this.Audio_3D[i] != null)
					{
						this.Audio_3D[i].Stop();
					}
				}
			}
			this.isPlayAudio_3D = value;
		}
	}

	public void OnDestroy()
	{
		AudioManage.inst = null;
	}

	private void Awake()
	{
		AudioManage.inst = this;
		this.PlayAudioList_Init();
	}

	public void PlayAudioBackground(string audioName, bool isloop = true)
	{
		if (this.audioBackgruond.audio.clip != null && !this.audioBackgruond.audio.clip.name.Equals(audioName) && this.audioBackgruond.isPlaying)
		{
			this.audioBackgruond.Stop();
		}
		if (this.audioBackgruond.audio.clip == null || !this.audioBackgruond.audio.clip.name.Trim().ToUpper().Equals(audioName.Trim().ToUpper()))
		{
			for (int i = 0; i < this.audioList.Count; i++)
			{
				if (this.audioList[i].name.Trim().ToUpper().Equals(audioName.Trim().ToUpper()))
				{
					this.audioBackgruond.audio.clip = this.audioList[i];
				}
			}
		}
		if (this.isOpenPlayAudioBackGround && this.audioBackgruond.audio.clip != null)
		{
			this.audioBackgruond.Play();
			this.audioBackgruond.loop = isloop;
			if ((double)this.audioBackgruond.volume < 0.5)
			{
				this.tween = TweenVolume.Begin(this.audioBackgruond.gameObject, 0.6f, 0.8f);
				this.tween.onFinished.Clear();
			}
		}
	}

	public void StopAudioBackground()
	{
		if (this.audioBackgruond.audio.clip != null && this.audioBackgruond.isPlaying)
		{
			this.tween = TweenVolume.Begin(this.audioBackgruond.gameObject, 2f, 0.1f);
			this.tween.SetOnFinished(new EventDelegate(delegate
			{
				this.tween.onFinished.Clear();
				this.audioBackgruond.Stop();
			}));
		}
	}

	public void PauseAudioBackground()
	{
		if (this.audioBackgruond.audio.clip != null && this.audioBackgruond.isPlaying)
		{
			this.tween = TweenVolume.Begin(this.audioBackgruond.gameObject, 2f, 0.1f);
			this.tween.SetOnFinished(new EventDelegate(delegate
			{
				this.tween.onFinished.Clear();
				this.audioBackgruond.Pause();
			}));
		}
	}

	public void StopAudio()
	{
		this.audioBackgruond.Stop();
		this.audioPlay.Stop();
	}

	public void PlayAuido(string audioName, bool isLoop = false)
	{
		if (this.isOpenMusic)
		{
			if (this.audioPlay.isPlaying)
			{
				this.audioPlay.Stop();
			}
			for (int i = 0; i < this.audioList.Count; i++)
			{
				if (this.audioList[i].name.Trim().ToUpper().Equals(audioName.Trim().ToUpper()))
				{
					this.audioPlay.audio.clip = this.audioList[i];
					if (this.audioBackgruond.audio.clip != null)
					{
						this.SetPlayAudiList(this.audioList[i]);
						this.audioPlay.loop = isLoop;
					}
					return;
				}
			}
			LogManage.LogError("该音效文件不存在：" + audioName);
			this.UnFindAudio = this.UnFindAudio + "   " + audioName;
		}
	}

	private void PlayAudioList_Init()
	{
		for (int i = 0; i < 3; i++)
		{
			this.PlayAudioList.Add(null);
		}
		for (int j = 0; j < this.PlayAudioList.Count; j++)
		{
			this.PlayAudioList[j] = (UnityEngine.Object.Instantiate(this.audioPlay) as AudioSource);
			this.PlayAudioList[j].transform.parent = this.audioPlay.transform.parent;
			this.PlayAudioList[j].transform.name = this.audioPlay.transform.name + ":" + j;
		}
		this.PlayAudioList_No = 0;
	}

	private void SetPlayAudiList(AudioClip audipClip)
	{
		this.PlayAudioList[this.PlayAudioList_No].audio.clip = audipClip;
		this.PlayAudioList[this.PlayAudioList_No].Play();
		this.PlayAudioList[this.PlayAudioList_No].loop = false;
		this.PlayAudioList[this.PlayAudioList_No].volume = 0f;
		if (audipClip.name == "winbaozha")
		{
			this.PlayAudioList[this.PlayAudioList_No].volume = UnityEngine.Random.Range(0.6f, 1f);
		}
		else
		{
			while (this.PlayAudioList[this.PlayAudioList_No].volume < 1f)
			{
				this.PlayAudioList[this.PlayAudioList_No].volume += 0.25f * Time.deltaTime;
			}
		}
		this.PlayAudioList_No++;
		if (this.PlayAudioList_No > 2)
		{
			this.PlayAudioList_No = 0;
		}
	}

	public void PlayAuidoBySelf_3D(string audioName, GameObject ga, bool IsLoop = false, ulong delay = 0uL)
	{
		if (this.isOpenMusic && this.isPlayAudio_3D)
		{
			AudioSource compentIfNoAddOne;
			if (ga != null)
			{
				compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<AudioSource>(ga);
			}
			else
			{
				compentIfNoAddOne = this.audioPlay;
			}
			this.Audio_3D.Add(compentIfNoAddOne);
			if (compentIfNoAddOne.isPlaying)
			{
				compentIfNoAddOne.Stop();
			}
			for (int i = 0; i < this.audioList.Count; i++)
			{
				if (this.audioList[i].name.Trim().ToUpper().Equals(audioName.Trim().ToUpper()))
				{
					compentIfNoAddOne.audio.clip = this.audioList[i];
				}
			}
			if (compentIfNoAddOne.audio.clip != null)
			{
				if (delay > 0uL)
				{
					compentIfNoAddOne.Play(delay);
				}
				else
				{
					compentIfNoAddOne.Play();
				}
				compentIfNoAddOne.loop = IsLoop;
			}
		}
		else if (ga && ga.GetComponent<AudioSource>())
		{
			UnityEngine.Object.Destroy(ga.GetComponent<AudioSource>());
		}
	}

	public void PlayAuidoBySelf_2D(string audioName, GameObject ga, bool IsLoop = false, ulong delay = 0uL)
	{
		if (this.isOpenMusic)
		{
			AudioSource compentIfNoAddOne;
			if (ga != null)
			{
				compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<AudioSource>(ga);
			}
			else
			{
				compentIfNoAddOne = this.audioPlay;
			}
			if (compentIfNoAddOne.isPlaying)
			{
				compentIfNoAddOne.Stop();
			}
			for (int i = 0; i < this.audioList.Count; i++)
			{
				if (this.audioList[i].name.Trim().ToUpper().Equals(audioName.Trim().ToUpper()))
				{
					compentIfNoAddOne.audio.clip = this.audioList[i];
				}
			}
			if (compentIfNoAddOne.audio.clip != null)
			{
				if (delay > 0uL)
				{
					compentIfNoAddOne.Play(delay);
				}
				else
				{
					compentIfNoAddOne.Play();
				}
				compentIfNoAddOne.loop = IsLoop;
			}
		}
		else if (ga && ga.GetComponent<AudioSource>())
		{
			UnityEngine.Object.Destroy(ga.GetComponent<AudioSource>());
		}
	}
}
