using System;
using System.Collections;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class MusicManager : View
	{
		private AudioSource audio_Client;

		private Hashtable sounds = new Hashtable();

		private void Start()
		{
			this.audio_Client = base.GetComponent<AudioSource>();
		}

		private void Add(string key, AudioClip value)
		{
			if (this.sounds[key] != null || value == null)
			{
				return;
			}
			this.sounds.Add(key, value);
		}

		private AudioClip Get(string key)
		{
			if (this.sounds[key] == null)
			{
				return null;
			}
			return this.sounds[key] as AudioClip;
		}

		public AudioClip LoadAudioClip(string path)
		{
			AudioClip audioClip = this.Get(path);
			if (audioClip == null)
			{
				audioClip = (AudioClip)Resources.Load(path, typeof(AudioClip));
				this.Add(path, audioClip);
			}
			return audioClip;
		}

		public bool CanPlayBackSound()
		{
			string key = "SimpleFramework_BackSound";
			int @int = PlayerPrefs.GetInt(key, 1);
			return @int == 1;
		}

		public void PlayBacksound(string name, bool canPlay)
		{
			if (this.audio_Client.clip != null && name.IndexOf(this.audio_Client.clip.name) > -1)
			{
				if (!canPlay)
				{
					this.audio_Client.Stop();
					this.audio_Client.clip = null;
					Util.ClearMemory();
				}
				return;
			}
			if (canPlay)
			{
				this.audio_Client.loop = true;
				this.audio_Client.clip = this.LoadAudioClip(name);
				this.audio_Client.Play();
			}
			else
			{
				this.audio_Client.Stop();
				this.audio_Client.clip = null;
				Util.ClearMemory();
			}
		}

		public bool CanPlaySoundEffect()
		{
			string key = "SimpleFramework_SoundEffect";
			int @int = PlayerPrefs.GetInt(key, 1);
			return @int == 1;
		}

		public void Play(AudioClip clip, Vector3 position)
		{
			if (!this.CanPlaySoundEffect())
			{
				return;
			}
			AudioSource.PlayClipAtPoint(clip, position);
		}
	}
}
