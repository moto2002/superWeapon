using System;
using System.Collections.Generic;
using UnityEngine;

public class HintPanel : MonoBehaviour
{
	public GameObject hintInfo;

	public T_Tank Postion;

	public T_Tower TowerPostion;

	private GameObject hint;

	private List<GameObject> hintArr = new List<GameObject>();

	private List<Vector3> hintPos = new List<Vector3>();

	public static int num;

	private bool oneAudio = true;

	private string personSound = string.Empty;

	private float lastPersonSoundTime = -10f;

	private Vector3 Tpostion;

	private void Start()
	{
	}

	public void OnHintInfo(string text, Vector3 postion, bool isTextShow)
	{
		if (NewbieGuidePanel.isEnemyAttck)
		{
			for (int i = 1; i < UnitConst.GetInstance().hintPanel.Count; i++)
			{
				if (UnitConst.GetInstance().hintPanel[i].hintinfo == text)
				{
					AudioManage.inst.audioPlay.Stop();
					if (UnitConst.GetInstance().hintPanel[i].Sound != null)
					{
						string sound = UnitConst.GetInstance().hintPanel[i].Sound;
						if (!this.personSound.Equals(sound) && Time.time - this.lastPersonSoundTime > 2f)
						{
							AudioManage.inst.PlayAuido(sound, false);
							this.lastPersonSoundTime = Time.time;
							this.personSound = sound;
						}
					}
					break;
				}
			}
		}
		else if (this.oneAudio)
		{
			for (int j = 1; j < UnitConst.GetInstance().hintPanel.Count; j++)
			{
				if (UnitConst.GetInstance().hintPanel[j].hintinfo == text)
				{
					this.oneAudio = false;
					AudioManage.inst.audioPlay.Stop();
					if (UnitConst.GetInstance().hintPanel[j].Sound != null)
					{
						string sound2 = UnitConst.GetInstance().hintPanel[j].Sound;
						if (!this.personSound.Equals(sound2) || Time.time - this.lastPersonSoundTime > 2f)
						{
							AudioManage.inst.PlayAuido(sound2, false);
							this.lastPersonSoundTime = Time.time;
							this.personSound = sound2;
						}
					}
					break;
				}
			}
		}
	}
}
